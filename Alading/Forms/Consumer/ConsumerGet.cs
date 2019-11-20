using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Alading.Utils;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Taobao.Entity.Extend;
using Alading.Taobao.API;
using Alading.Business;
using Alading.Core.Enum;

namespace Alading.Forms.Consumer
{
    public partial class ConsumerGet : DevExpress.XtraEditors.XtraForm
    {
        class ReportState
        {
            public string Message { get; set; }
            public int Current { get; set; }
            public int Total { get; set; }
            public int Persentage
            {
                get
                {
                    if (Total == 0) return 0;
                    else if (Current >= Total) return 100;
                    return Convert.ToInt32((double)Current / (double)Total * 100);
                }
            }
        }

        int threadCount = 0;
        int sub_total = 0;
        int sub_done = 0;

        BackgroundWorker mainWorker = new BackgroundWorker();
        List<BackgroundWorker> threadList = new List<BackgroundWorker>();
        List<SycTaskArgs> taskList = new List<SycTaskArgs>();

        int ready = 0;
        object tlock = new object(); // thread lock
        object vlock = new object(); // value lock
        AutoResetEvent signal = new AutoResetEvent(false);

        int typeIndex = 0;

        List<Alading.Entity.Shop> allShopList = new List<Alading.Entity.Shop>(); // all shop

        List<Alading.Entity.Shop> downShopList = new List<Alading.Entity.Shop>(); // selected shop for synchronizing

        public ConsumerGet()
        {
            InitializeComponent();
        }

        private void ConsumerGet_Load(object sender, EventArgs e)
        {
            this.dateEditEnd.DateTime = DateTime.Now;
            this.dateEditBegin.DateTime = DateTime.Now.AddDays(-15);

            allShopList.AddRange(SystemHelper.ShopList);

            // init shop drawdown lsit
            foreach (var s in allShopList)
            {
                checkedShopList.Properties.Items.Add(s.nick);
            }

            mainWorker.WorkerReportsProgress = true;
            mainWorker.WorkerSupportsCancellation = true;
            mainWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(mainWorker_RunWorkerCompleted);
            mainWorker.ProgressChanged += new ProgressChangedEventHandler(mainWorker_ProgressChanged);
            mainWorker.DoWork += new DoWorkEventHandler(mainWorker_DoWork);
        }

        #region Mainworker event

        void mainWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                //threadCount = Convert.ToInt32(spinEditThreadCount.Value);//这里不必要了，在点击按钮时已经赋值
                int pageSize = 40;

                for (int k = 0; k < downShopList.Count; k++)
                {
                    var shop = downShopList[k];

                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    ReportState state = new ReportState()
                    {
                        Current = k + 1,
                        Total = downShopList.Count,
                        Message = string.Format("正在同步店铺 {0} 的客户...", shop.title),
                    };

                    worker.ReportProgress(1, state);

                    #region initialize query time

                    DateTime begin = DateTime.Now;
                    DateTime end = DateTime.Now;

                    switch (typeIndex)
                    {
                        // 15 days
                        case 0:
                            begin = DateTime.Now.AddDays(-15);
                            break;
                        // customize
                        case 1:
                            begin = this.dateEditBegin.DateTime;
                            end = this.dateEditEnd.DateTime;
                            break;
                        // since shop created
                        case 2:
                            begin = shop.created;
                            break;
                    }

                    #endregion

                    #region Get total trade count

                    TradeReq request = new TradeReq();
                    request.Status = null;
                    request.PageSize = pageSize;
                    request.PageNo = 1;
                    request.StartCreated = begin.ToString("yyyy-MM-dd HH:mm:ss");
                    request.EndCreated = end.ToString("yyyy-MM-dd HH:mm:ss");

                    string sessionkey = SystemHelper.GetSessionKey(shop.nick);
                    TradeRsp response = TopService.TradesSoldGet(sessionkey, request);

                    sub_done = 0;
                    sub_total = response.TotalResults;

                    #endregion

                    if (sub_total == 0) continue;

                    #region Dispatch task to thread

                    threadList.Clear();

                    if (sub_total <= pageSize)
                    {
                        #region total count < page size

                        SycTaskArgs task = new SycTaskArgs
                                    {
                                        ShopNick = shop.nick,
                                        PageSize = pageSize,
                                        QueryBeginTime = begin,
                                        QueryEndTime = end,
                                    };
                        task.PageNoList.Add(1);

                        BackgroundWorker w = new BackgroundWorker
                        {
                            WorkerReportsProgress = true,
                            WorkerSupportsCancellation = true,
                        };
                        w.RunWorkerCompleted += new RunWorkerCompletedEventHandler(w_RunWorkerCompleted);
                        w.ProgressChanged += new ProgressChangedEventHandler(w_ProgressChanged);
                        w.DoWork += new DoWorkEventHandler(w_DoWork);

                        taskList.Add(task);
                        threadList.Add(w);

                        threadCount = 1;

                        #endregion
                    }
                    else
                    {
                        int pageCount = (sub_total % pageSize == 0) ? (sub_total / pageSize) : (sub_total / pageSize + 1);

                        if (threadCount < pageCount)
                        {
                            #region page count < thread count

                            threadCount = pageCount;

                            for (int i = 0; i < threadCount; i++)
                            {
                                SycTaskArgs task = new SycTaskArgs
                                {
                                    ShopNick = shop.nick,
                                    PageSize = 40,
                                    QueryBeginTime = begin,
                                    QueryEndTime = end,
                                };
                                task.PageNoList.Add(i + 1);

                                BackgroundWorker w = new BackgroundWorker
                                {
                                    WorkerReportsProgress = true,
                                    WorkerSupportsCancellation = true,
                                };

                                w.RunWorkerCompleted += new RunWorkerCompletedEventHandler(w_RunWorkerCompleted);
                                w.ProgressChanged += new ProgressChangedEventHandler(w_ProgressChanged);
                                w.DoWork += new DoWorkEventHandler(w_DoWork);

                                taskList.Add(task);
                                threadList.Add(w);
                            }

                            #endregion
                        }
                        else
                        {
                            #region page count > thread count

                            for (int i = 0; i < threadCount; i++)
                            {
                                SycTaskArgs task = new SycTaskArgs
                                {
                                    ShopNick = shop.nick,
                                    PageSize = 40,
                                    QueryBeginTime = begin,
                                    QueryEndTime = end,
                                };

                                BackgroundWorker w = new BackgroundWorker
                                {
                                    WorkerReportsProgress = true,
                                    WorkerSupportsCancellation = true,
                                };

                                w.RunWorkerCompleted += new RunWorkerCompletedEventHandler(w_RunWorkerCompleted);
                                w.ProgressChanged += new ProgressChangedEventHandler(w_ProgressChanged);
                                w.DoWork += new DoWorkEventHandler(w_DoWork);

                                taskList.Add(task);
                                threadList.Add(w);
                            }

                            int index = 0;
                            for (int i = 0; i < pageCount; i++, index++)
                            {
                                if (index >= taskList.Count) index = 0;
                                taskList[i].PageNoList.Add(i + 1);
                            }

                            #endregion
                        }
                    }

                    #endregion

                    #region run tasks

                    signal = new AutoResetEvent(false);

                    for (int i = 0; i < threadCount; i++)
                    {
                        threadList[i].RunWorkerAsync(taskList[i]);
                    }

                    signal.WaitOne();

                    #endregion
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }        

        void mainWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //主线程报告不必用代理方法
            ReportState state = e.UserState as ReportState;
            string message = string.Format("{0}  {1}", DateTime.Now.ToString("HH:mm:ss"), state.Message);
            progressBarTotal.Position = state.Persentage;
            ConsumerDown.Items.Insert(0, message);
        }

        void mainWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //主线程报告不必用代理方法
            this.progressBarTotal.Position = 100;
            //this.BtnCompled.Enabled = true;
            this.btnDownConsumer.Enabled = true;
            this.checkedShopList.Enabled = true;
          
        } 

        #endregion

        #region SubThread worker events

        void w_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            SycTaskArgs task = e.Argument as SycTaskArgs;

            foreach (var p in task.PageNoList)
            {
                #region get trade information
                //这里改成mainWorker，即主线程取消子线程工作全部停止
                if (mainWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                TradeReq request = new TradeReq();
                TradeRsp response = null;
                request.Status = null;
                request.PageSize = task.PageSize;
                request.PageNo = p;
                request.StartCreated = task.QueryBeginTime.ToString("yyyy-MM-dd HH:mm:ss");
                request.EndCreated = task.QueryEndTime.ToString("yyyy-MM-dd HH:mm:ss");
                string sessionkey = SystemHelper.GetSessionKey(task.ShopNick);
                response = TopService.TradesSoldGet(sessionkey, request);            

                #endregion

                #region if getting trade failed do next

                if (response == null || response.Trades == null)
                {
                    lock (vlock)
                    {
                        sub_done += task.PageSize;
                    }
                    continue;
                } 

                #endregion

                foreach (var t in response.Trades.Trade)
                {
                    //if (worker.CancellationPending)
                    //这里改成mainWorker，即主线程取消子线程工作全部停止
                    if (mainWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    lock (vlock)
                    {
                        sub_done++;
                    }

                    TradeRsp fulltrade = TopService.TradeFullinfoGet(sessionkey, t.Tid);

                    #region if getting trade detail failed do next

                    if (fulltrade == null || fulltrade.Trade == null)
                    {
                        ReportState state = new ReportState
                        {
                            Total = sub_total,
                            Current = sub_done,
                            Message = string.Format("同步客户 {0} 失败：无法获取交易信息！", t.BuyerNick),
                        };
                        worker.ReportProgress(0, state);
                        continue;
                    } 

                    #endregion

                    UserRsp buyer = TopService.UserGet(sessionkey, fulltrade.Trade.BuyerNick, fulltrade.Trade.AlipayNo);

                    #region if getting buyer detail failed do next

                    if (buyer == null || buyer.User == null)
                    {
                        ReportState state = new ReportState
                        {
                            Total = sub_total,
                            Current = sub_done,
                            Message = string.Format("同步客户 {0} 失败：无法获取买家信息！", t.BuyerNick),
                        };
                        worker.ReportProgress(0, state);
                        continue;
                    }

                    #endregion

                    Alading.Entity.Consumer consumer = Alading.Business.ConsumerService.GetConsumer(fulltrade.Trade.BuyerNick);

                    if (consumer == null)
                    {
                        #region Consumer is not existed and save it

                        consumer = new Alading.Entity.Consumer();

                        consumer.nick = buyer.User.Nick;
                        consumer.sex = buyer.User.Sex;
                        consumer.buyer_zip = buyer.User.Location.Zip;
                        consumer.location_city = buyer.User.Location.City;
                        consumer.location_state = buyer.User.Location.State;
                        consumer.location_district = buyer.User.Location.District;
                        consumer.location_address = buyer.User.Location.Address;
                        consumer.location_country = buyer.User.Location.Address;
                        consumer.birthday = buyer.User.Birthday;
                        consumer.credit = buyer.User.BuyerCredit.ToString();
                        consumer.level = buyer.User.BuyerCredit.Level;
                        consumer.score = buyer.User.BuyerCredit.Score;
                        consumer.status = buyer.User.Status;
                        consumer.created = DateTime.Parse(buyer.User.Created);
                        consumer.last_visit = buyer.User.LastVisit;
                        consumer.mobilephone = fulltrade.Trade.ReceiverMobile;
                        consumer.phone = fulltrade.Trade.ReceiverPhone;
                        consumer.email = fulltrade.Trade.BuyerEmail;
                        consumer.buyer_name = fulltrade.Trade.ReceiverName;
                        consumer.alipay = fulltrade.Trade.AlipayNo;

                        ReturnType result = Alading.Business.ConsumerService.AddConsumer(consumer);

                        if (result == ReturnType.Success)
                        {
                            ReportState state = new ReportState
                            {
                                Total = sub_total,
                                Current = sub_done,
                                Message = string.Format("同步客户 {0} 信息成功！", consumer.nick),
                            };
                            worker.ReportProgress(1, state);

                            Alading.Entity.ConsumerAddress addr = Alading.Business.ConsumerAddressService.GetConsumerAddress(
                                c => c.tid == fulltrade.Trade.Tid).FirstOrDefault();

                            if (addr == null)
                            {
                                addr = new Alading.Entity.ConsumerAddress
                                {
                                    buyer_nick = fulltrade.Trade.BuyerNick,
                                    location_address = fulltrade.Trade.ReceiverAddress,
                                    location_city = fulltrade.Trade.ReceiverCity,
                                    location_country = buyer.User.Location.Country,
                                    location_district = fulltrade.Trade.ReceiverDistrict,
                                    location_state = fulltrade.Trade.ReceiverState,
                                    location_zip = fulltrade.Trade.ReceiverZip,
                                    receiver_mobile = fulltrade.Trade.ReceiverMobile,
                                    receiver_name = fulltrade.Trade.ReceiverName,
                                    receiver_phone = fulltrade.Trade.ReceiverPhone,
                                    receiver_zip = fulltrade.Trade.ReceiverZip,
                                    tid = fulltrade.Trade.Tid,
                                };

                                result = Alading.Business.ConsumerAddressService.AddConsumerAddress(addr);

                                if (result == ReturnType.Success)
                                {
                                    state = new ReportState
                                    {
                                        Total = sub_total,
                                        Current = sub_done,
                                        Message = string.Format("同步客户 {0} 地址信息成功！", consumer.nick),
                                    };
                                    worker.ReportProgress(1, state);
                                }
                                else
                                {
                                    state = new ReportState
                                    {
                                        Total = sub_total,
                                        Current = sub_done,
                                        Message = string.Format("同步客户 {0} 地址信息失败：数据保存失败！", consumer.nick),
                                    };
                                    worker.ReportProgress(1, state);
                                }
                            }                         
                        }
                        else
                        {
                            ReportState state = new ReportState
                            {
                                Total = sub_total,
                                Current = sub_done,
                                Message = string.Format("同步客户 {0} 信息失败：数据保存失败！", consumer.nick),
                            };
                            worker.ReportProgress(1, state);
                            continue;
                        }
                        #endregion
                    }
                    else
                    {
                        #region Consumer existed and update address information

                        ReportState state0 = new ReportState
                        {
                            Total = sub_total,
                            Current = sub_done,
                            Message = string.Format("客户 {0} 已存在！", consumer.nick),
                        };
                        worker.ReportProgress(0, state0);

                        Alading.Entity.ConsumerAddress addr = Alading.Business.ConsumerAddressService.GetConsumerAddress(
                                                c => c.tid == fulltrade.Trade.Tid).FirstOrDefault();

                        if (addr == null)
                        {
                            addr = new Alading.Entity.ConsumerAddress
                            {
                                buyer_nick = fulltrade.Trade.BuyerNick,
                                location_address = fulltrade.Trade.ReceiverAddress,
                                location_city = fulltrade.Trade.ReceiverCity,
                                location_country = buyer.User.Location.Country,
                                location_district = fulltrade.Trade.ReceiverDistrict,
                                location_state = fulltrade.Trade.ReceiverState,
                                location_zip = fulltrade.Trade.ReceiverZip,
                                receiver_mobile = fulltrade.Trade.ReceiverMobile,
                                receiver_name = fulltrade.Trade.ReceiverName,
                                receiver_phone = fulltrade.Trade.ReceiverPhone,
                                receiver_zip = fulltrade.Trade.ReceiverZip,
                                tid = fulltrade.Trade.Tid,
                            };

                            ReturnType result = Alading.Business.ConsumerAddressService.AddConsumerAddress(addr);

                            if (result == ReturnType.Success)
                            {
                                ReportState state = new ReportState
                                {
                                    Total = sub_total,
                                    Current = sub_done,
                                    Message = string.Format("同步客户 {0} 地址信息成功！", consumer.nick),
                                };
                                worker.ReportProgress(1, state);
                            }
                            else
                            {
                                ReportState state = new ReportState
                                {
                                    Total = sub_total,
                                    Current = sub_done,
                                    Message = string.Format("同步客户 {0} 地址信息失败：数据保存失败！", consumer.nick),
                                };
                                worker.ReportProgress(1, state);
                            }
                        }  

                        #endregion
                    }
                }
            }
        }

        void w_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ReportState state = e.UserState as ReportState;
            string message = string.Format("{0}  {1}", DateTime.Now.ToString("HH:mm:ss"), state.Message);
            BeginInvoke(new Action(() =>
            {
                progressBarCurrent.Position = state.Persentage;
                ConsumerDown.Items.Insert(0, message);
            }));
        }

        void w_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            signal.Set();
        }

        #endregion

        private void cmbSelectTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            typeIndex = cmbSelectTime.SelectedIndex;

            switch (typeIndex)
            {
                    // 最近15天
                case 0:
                    this.dateEditBegin.Properties.ReadOnly = true;
                    this.dateEditEnd.Properties.ReadOnly = true;
                    this.dateEditEnd.DateTime = DateTime.Now;
                    this.dateEditBegin.DateTime = DateTime.Now.AddDays(-15);
                    break;
                    // 自定义
                case 1:
                    this.dateEditBegin.Properties.ReadOnly = false;
                    this.dateEditEnd.Properties.ReadOnly = false;
                    this.dateEditEnd.DateTime = DateTime.Now;
                    this.dateEditBegin.DateTime = DateTime.Now;
                    break;
                case 2:
                    // 从开店至今
                    this.dateEditBegin.Properties.ReadOnly = true;
                    this.dateEditEnd.Properties.ReadOnly = true;
                    this.dateEditEnd.EditValue = null;
                    this.dateEditBegin.EditValue = null;
                    break;
                default:
                    break;
            }
        }

        private void checkedShopList_EditValueChanged(object sender, EventArgs e)
        {
            downShopList.Clear();
            if (!string.IsNullOrEmpty(checkedShopList.Text))
            {
                // init download shop list
                string[] nicks = checkedShopList.EditValue.ToString().Split(',');
                foreach (var sn in nicks)
                {
                    var shop = allShopList.FirstOrDefault(c => c.nick == sn.Trim());
                    if (shop != null) downShopList.Add(shop);
                }
            }

            bool enable = downShopList.Count > 0;
            btnCancel.Enabled = btnDownConsumer.Enabled = enable;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (mainWorker!=null&&mainWorker.IsBusy)
           {
               mainWorker.CancelAsync();      
           }
        }

        private void btnDownOrders_Click(object sender, EventArgs e)
        {
            threadCount = Convert.ToInt32(spinEditThreadCount.Value);

            // can not download again
            btnDownConsumer.Enabled = false;
            checkedShopList.Enabled = false;
            this.btnCancel.Enabled = true;

            ConsumerDown.Items.Clear();
            ConsumerDown.Items.Insert(0, string.Format("{0}  开始同步店铺客户信息...", DateTime.Now.ToString("HH:mm:ss")));

            // run mainWorker            
            mainWorker.RunWorkerAsync();
        }

        private void dateEditBegin_EditValueChanged(object sender, EventArgs e)
        {
            if (dateEditBegin.EditValue == null)
            {
                dateEditEnd.EditValue = null;
            }
            else if (dateEditBegin.DateTime > dateEditEnd.DateTime)
            {
                dateEditEnd.DateTime = dateEditBegin.DateTime;
            }
        }

        private void AddReadyCount()
        {
            lock (tlock)
            {
                ready++;
                if (ready == threadCount)
                {
                    signal.Set();
                }
            }
        }

        class SycTaskArgs
        {
            public string ShopNick { get; set; }
            public DateTime QueryBeginTime { get; set; }
            public DateTime QueryEndTime { get; set; }
            public List<int> PageNoList { get; private set; }
            public int PageSize { get; set; }

            public SycTaskArgs()
            {
                PageNoList = new List<int>();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
