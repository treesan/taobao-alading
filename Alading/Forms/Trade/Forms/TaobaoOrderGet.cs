using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Taobao.Entity.Extend;
using Alading.Taobao.API;
using Alading.Core.Enum;
using Alading.Utils;
using System.Linq;
using System.Collections;
using Alading.Business;
using System.Threading;
using System.Diagnostics;

namespace Alading.Forms.Trade.Forms
{
    public partial class TaobaoOrderGet : DevExpress.XtraEditors.XtraForm
    {
        #region  全局变量
        //初始化店铺列表
        List<Alading.Entity.Shop> initShopList = new List<Alading.Entity.Shop>();

        //选择的店铺列表
        List<Alading.Entity.Shop> shop_List = new List<Alading.Entity.Shop>();

        //要获取的交易状态
        string TradeStatus = null;

        //要开启的子线程数量
        int threadCount = 0;

        #endregion

        //构造函数
        public TaobaoOrderGet()
        {
            InitializeComponent();
        }

        public TaobaoOrderGet(string staus)
        {
            InitializeComponent();
            try
            {
                //初始化获取状态
                TradeStatus = staus;

                //初始化店铺列表
                initShopList = SystemHelper.ShopList.Where(q => q.type == SellerShopType.bShop || q.type == SellerShopType.bShop.ToUpper()
                                                                                                            || q.type == SellerShopType.cShop || q.type == SellerShopType.cShop.ToUpper()).ToList();

                //初始化店铺控件
                foreach (Alading.Entity.Shop shopObj in initShopList)
                {
                    checkedShopList.Properties.Items.Add(shopObj.nick);
                }
            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 窗体下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaobaoOrderGet_Load(object sender, EventArgs e)
        {

            this.dateEditEnd.EditValue = DateTime.Now;
            this.dateEditBegin.EditValue = DateTime.Now.AddDays(-15);
            this.btnCancel.Enabled = false;
            this.btnDownOrders.Enabled = false;
            BtnFailedOrder.Enabled = false;
        }

        /// <summary>
        /// 获取被选择的店铺列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSelectTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.cmbSelectTime.SelectedIndex)
            {
                case 0:
                    this.dateEditBegin.Enabled = false;
                    this.dateEditEnd.Enabled = false;
                    this.dateEditEnd.EditValue = DateTime.Now;
                    this.dateEditBegin.EditValue = DateTime.Now.AddDays(-15);
                    break;
                case 1:
                    this.dateEditBegin.Enabled = true;
                    this.dateEditEnd.Enabled = true;
                    this.dateEditEnd.EditValue = null;
                    this.dateEditBegin.EditValue = null;
                    break;
                case 2:
                    this.dateEditBegin.Enabled = false;
                    this.dateEditEnd.Enabled = false;
                    this.dateEditEnd.EditValue = string.Empty;
                    this.dateEditBegin.EditValue = string.Empty;
                    break;

                //case 1:
                //    this.dateEditBegin.Enabled = false;
                //    this.dateEditEnd.Enabled = false;
                //    this.dateEditEnd.EditValue = DateTime.Now;
                //    this.dateEditBegin.EditValue = DateTime.Now.AddMonths(-1);
                //    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// checkedShopList为空 按钮不可用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedShopList_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(checkedShopList.Text))
            {
                /*初始化被选择的店铺列表*/
                shop_List.Clear();
                string[] ShopNickList = checkedShopList.EditValue.ToString().Split(',');
                foreach (string shop_nick in ShopNickList)
                {
                    Alading.Entity.Shop shop = initShopList.Where(q => q.nick == shop_nick.Trim()).FirstOrDefault();
                    shop_List.Add(shop);
                }

                this.btnCancel.Enabled = true;
                this.btnDownOrders.Enabled = true;
            }
            else
            {
                this.btnCancel.Enabled = false;
                this.btnDownOrders.Enabled = false;
            }
        }

        /// <summary>
        /// cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            checkedShopList.Enabled = true;
            if (mainWorker != null && mainWorker.IsBusy)
            {
                mainWorker.CancelAsync();

                //子线程取消
                foreach (BackgroundWorker worker in workerList)
                {
                    if (worker != null && worker.IsBusy)
                    {
                        worker.CancelAsync();
                    }
                }
            }
        }

        #region  所需变量
        /// <summary>
        /// 主线程
        /// </summary>
        BackgroundWorker mainWorker = new BackgroundWorker();

        /// <summary>
        /// 锁对象
        /// </summary>
        static object lockObject = new object();

        /// <summary>
        /// 若为true则非阻塞状态，为false为阻塞状态。AutoResetEvent一次只唤醒一个线程，ManualResetEvent则可以唤醒多个线程。
        /// </summary>
        AutoResetEvent autoReset = new AutoResetEvent(false);

        /// <summary>
        /// 总的交易数量
        /// </summary>
        int TotalProessNum = 0;

        /// <summary>
        /// 当前的交易量
        /// </summary>
        int currentProessNum = 1;

        /// <summary>
        /// 总交易的进度记录
        /// </summary>
        int ProcessRecord = 1;

        /// <summary>
        /// 总的线程数量,则totalThreadCount=threadCount
        /// </summary>
        int totalThreadCount = 0;

        /// <summary>
        /// key:shopNick; value:tradeNum
        /// </summary>
        SortedList<string, int> shopNumList = new SortedList<string, int>();

        /// <summary>
        /// 子线程列表
        /// </summary>
        List<BackgroundWorker> workerList = new List<BackgroundWorker>();

        /// <summary>
        /// 记录下载失败的订单列表
        /// </summary>
        FailedTrade failedTrade = new FailedTrade();
        #endregion

        /// <summary>
        ///多线程下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownOrders_Click(object sender, EventArgs e)
        {
            try
            {
                /*缺填查询开始时间*/
                if (dateEditBegin.EditValue == null)
                {
                    XtraMessageBox.Show("请选择查询开始时间!", "出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                /*查询截止时间*/
                if (dateEditEnd.EditValue == null)
                {
                    XtraMessageBox.Show("请选择查询截止时间!", "出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                /*查询截止时间>查询开始时间*/
                if (dateEditEnd.DateTime < dateEditBegin.DateTime)
                {
                    XtraMessageBox.Show("查询截止时间不能小于查询开始时间!", "出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                threadCount = Convert.ToInt32(spinEditThreadCount.Value);
                //下载按钮不可用
                btnDownOrders.Enabled = false;
                checkedShopList.Enabled = false;
                this.btnCancel.Enabled = true;
                OrderDown.Items.Clear();
                OrderDown.Items.Insert(0, string.Format("{0}   正在获取店铺订单信息...", DateTime.Now.ToString("HH:mm:ss")));
                if (mainWorker == null)
                {
                    mainWorker = new BackgroundWorker();
                }
                mainWorker.WorkerSupportsCancellation = true;
                mainWorker.WorkerReportsProgress = true;
                mainWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(mainWorker_RunWorkerCompleted);
                mainWorker.DoWork += new DoWorkEventHandler(mainWorker_DoWork);
                mainWorker.ProgressChanged += new ProgressChangedEventHandler(mainWorker_ProgressChanged);
                mainWorker.RunWorkerAsync();

            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "提示");
                btnDownOrders.Enabled = true;
            }
        }

        void mainWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UserState state = e.UserState as UserState;
            if (e.ProgressPercentage == 0)
            {

                if (state.message.Contains("Remote services error"))
                {
                    string msg = string.Format("{0}   店铺{1}的订单{2}", DateTime.Now.ToString("HH:mm:ss"), state.shopNick, state.message);
                    OrderDown.Items.Insert(0, msg);
                }
                else
                {
                    string msg = string.Format("{0}   店铺{1}的订单{2}", DateTime.Now.ToString("HH:mm:ss"), state.shopNick, state.message);
                    OrderDown.Items.Insert(0, msg);
                }
            }
        }

        void mainWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            StringBuilder str_fail = new StringBuilder();

            TradeReq tradeReq = new TradeReq();
            //获取状态
            tradeReq.Status = TradeStatus;

            //总交易量清空
            TotalProessNum = 0;
            //错误列表清空
            failedTrade = new FailedTrade();

            tradeReq.PageSize = 100;//每页总数
            tradeReq.PageNo = 1;//页数

            /*计算总的交易量  即总进度条计算*/
            for (int j = 0; j < shop_List.Count; j++)
            {
                try
                {
                    #region 时间设置
                    /*选择近十五天*/
                    if (cmbSelectTime.SelectedIndex == 0)
                    {
                        tradeReq.StartCreated = this.dateEditBegin.DateTime.ToString("yyyy-MM-dd HH:mm:ss");
                        tradeReq.EndCreated = this.dateEditEnd.DateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else/*开店至今*/
                    {
                        tradeReq.StartCreated = shop_List[j].created.ToString("yyyy-MM-dd HH:mm:ss");
                        tradeReq.EndCreated = DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    #endregion
                    //获取sessionkey
                    string sessionkey = SystemHelper.GetSessionKey(shop_List[j].nick);
                    //从淘宝上获取数据
                    TradeRsp tradeRsp = TopService.TradesSoldGet(sessionkey, tradeReq);
                    //订单获取失败
                    if (tradeRsp != null || tradeRsp.Trades != null)
                    {
                        TotalProessNum += tradeRsp.TotalResults;
                    }
                }
                catch (System.Exception ex)
                {
                    BeginInvoke(new Action(() => { OrderDown.Items.Insert(0, string.Format("{0}   {1}", DateTime.Now.ToString("HH:mm:ss"), ex.Message)); }));
                    return;
                }
            }

            /*各个店铺的交易处理*/
            for (int j = 0; j < shop_List.Count; j++)
            {
                //清空线程列表
                workerList.Clear();
                //清空总线程量
                totalThreadCount = 0;
                //当前交易量
                currentProessNum = 0;

                /*信息*/
                UserState DoState = new UserState();
                DoState.shopNick = shop_List[j].nick;
                DoState.message = "开始处理!";
                mainWorker.ReportProgress(0, DoState);
                try
                {
                    //交易列表
                    List<string> tidList = new List<string>();

                    #region 时间设置
                    /*选择近十五天*/
                    if (cmbSelectTime.SelectedIndex == 0)
                    {
                        tradeReq.StartCreated = this.dateEditBegin.DateTime.ToString("yyyy-MM-dd HH:mm:ss");
                        tradeReq.EndCreated = this.dateEditEnd.DateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else/*开店至今*/
                    {
                        tradeReq.StartCreated = shop_List[j].created.ToString("yyyy-MM-dd HH:mm:ss");
                        tradeReq.EndCreated = DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    #endregion

                    //获取sessionkey
                    string sessionkey = SystemHelper.GetSessionKey(shop_List[j].nick);
                    //从淘宝上获取数据
                    TradeRsp tradeRsp = TopService.TradesSoldGet(sessionkey, tradeReq);
                    //订单获取失败
                    if (tradeRsp == null || tradeRsp.Trades == null)
                    {
                        //异步操作
                        BeginInvoke(new Action(() => { OrderDown.Items.Insert(0, string.Format("{0}   网络异常,{1}店铺的订单下载失败！", DateTime.Now.ToString("HH:mm:ss"), shop_List[j].nick)); }));
                        str_fail.Append(shop_List[j].nick);
                        str_fail.Append(" ");
                        continue;
                    }
                    if (tradeRsp != null)
                    {
                        foreach (Alading.Taobao.Entity.Trade tradeObj in tradeRsp.Trades.Trade)
                        {
                            tidList.Add(tradeObj.Tid);
                        }/*foreach*/
                    }/*if*/

                    //店铺的总交易量
                    int totalResults = tradeRsp.TotalResults;
                    BeginInvoke(new Action(() =>
                    {
                        OrderDown.Items.Insert(0, string.Format("{0}   店铺{1}共{2}订单!", DateTime.Now.ToString("HH:mm:ss"), shop_List[j].nick, totalResults));
                    }));
                    #region 计算是否需要分页获取
                    int PageTotalNum = 0;/*总页数*/
                    int PageCurrentNo = 0;/*当前页数*/

                    //计算总页数
                    PageTotalNum = (tradeRsp.TotalResults % tradeReq.PageSize == 0) ? (tradeRsp.TotalResults / tradeReq.PageSize) : (tradeRsp.TotalResults / tradeReq.PageSize + 1);

                    /*有多余两页的订单*/
                    if (PageTotalNum >= 2)
                    {
                        for (PageCurrentNo = 2; PageCurrentNo < PageTotalNum + 1; PageCurrentNo++)
                        {
                            tradeReq.PageNo = PageCurrentNo;
                            tradeRsp = TopService.TradesSoldGet(sessionkey, tradeReq);
                            if (tradeRsp != null && tradeRsp.Trades != null)
                            {
                                foreach (Taobao.Entity.Trade tradeObj in tradeRsp.Trades.Trade)
                                {
                                    if (!tidList.Contains(tradeObj.Tid))
                                    {
                                        tidList.Add(tradeObj.Tid);
                                    }
                                }
                            }//if
                            else/*获取失败*/
                            {
                                if (!str_fail.ToString().Contains(shop_List[j].nick))
                                {
                                    str_fail.Append(shop_List[j].nick);
                                    str_fail.Append(" ");
                                    BeginInvoke(new Action(() => { OrderDown.Items.Insert(0, string.Format("{0}   网络异常,{1}店铺的订单下载失败！", DateTime.Now.ToString("HH:mm:ss"), shop_List[j].nick)); }));
                                }
                                break;
                            }
                        }//for
                    }//if
                    #endregion

                    /*总交易数小于线程数*/
                    if (totalResults < threadCount)
                    {
                        threadCount = totalResults;
                    }
                    //每个线程交易下载量
                    int tradePageSize = (totalResults % threadCount == 0) ? (totalResults / threadCount) : (totalResults / threadCount + 1);
                    #region 开启多线程
                    for (int i = 0; i < threadCount; i++)
                    {
                        //获取分给每个线程的tid列表
                        List<string> TradeTidList = tidList.Skip(i * tradePageSize).Take(tradePageSize).ToList();

                        /*线程配置*/
                        BackgroundWorker TradeWorker = new BackgroundWorker();
                        TradeWorker.WorkerReportsProgress = true;
                        TradeWorker.WorkerSupportsCancellation = true;
                        TradeWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                        TradeWorker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
                        TradeWorker.DoWork += new DoWorkEventHandler(worker_DoWork);

                        //添加到线程列表
                        workerList.Add(TradeWorker);

                        /*参数传递*/
                        TradeTask tradeTask = new TradeTask();
                        tradeTask.shopNic = shop_List[j].nick;
                        tradeTask.ShopTradeList = TradeTidList;
                        tradeTask.threadIndex = i + 1;
                        tradeTask.sessionKey = sessionkey;
                        tradeTask.totalNum = totalResults;
                        TradeWorker.RunWorkerAsync(tradeTask);
                    }
                    #endregion
                }
                catch (System.Exception ex)
                {
                    BeginInvoke(new Action(() => {
                        if (ex.Message.Contains("Remote service error"))
                        {
                            OrderDown.Items.Insert(0, string.Format("{0}   {1}", DateTime.Now.ToString("HH:mm:ss"), "不能连接远程服务器,请多尝试几次下载!".ToString())); 
                        }
                    }));
                    /*进度记录*/
                    ProcessRecord = 0;
                    TotalProessNum = 0;
                    TotalProessNum = 0;
                    BeginInvoke(new Action(() => { btnDownOrders.Enabled = true; }));
                    return;
                }
                //信号量，阻塞当前线程，这里阻塞的是下载主线程
                autoReset.WaitOne();
            }/*foreach 对店铺遍历*/
        }

        void mainWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnDownOrders.Enabled = true;
            checkedShopList.Enabled = true;
            /*进度记录*/
            ProcessRecord = 0;
            TotalProessNum = 0;
            TotalProessNum = 0;

            if (failedTrade.tidList != null )
            {
                //提示是否要重新下载
                string message = "您所有店铺一共有" + failedTrade.tidList.Count.ToString() + "个订单没有下载成功,是否要重新下载?";
                if (XtraMessageBox.Show(message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    BtnFailedOrder.Enabled = true;
                }
                failedTrade = null;
                mainWorker = null;
            }
            
            string msg = string.Format("{0}   {1}", DateTime.Now.ToString("HH:mm:ss"), "订单处理完成!");
            OrderDown.Items.Insert(0, msg);

            mainWorker = null;
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                UserState state = e.UserState as UserState;
                if (e.ProgressPercentage == -1)
                {
                    string msg = string.Format("{0}   子线程{1},第{2}个订单下载失败！交易id为{3},错误信息:{4}", DateTime.Now.ToString("HH:mm:ss"),
                                                                                                                                                                                    state.taskIndex, state.tidIndex, state.tid, state.message);
                    BeginInvoke(new Action(() => { OrderDown.Items.Insert(0, msg); }));
                }
                else
                {
                    BeginInvoke(new Action(() =>
                    {
                        if (state.taskIndex != 0)
                        {
                            progressBarCurrent.Position = state.currentProessNum;
                            progressBarTotal.Position = state.totalProcessNum;
                            OrderDown.Items.Insert(0, string.Format("{0}   子线程{1},第{2}个订单下载成功，交易id为：{3}。", DateTime.Now.ToString("HH:mm:ss"), state.taskIndex, state.tidIndex, state.tid));
                        }
                    }
                        ));
                }
            }
            catch (System.Exception ex)
            {

            }


        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AddThreadCount();
            //下载按钮复用
            BeginInvoke(new Action(() => { btnDownOrders.Enabled = true; }));

            if (e.Cancelled)
            {
                if (totalThreadCount == threadCount)
                {
                    BeginInvoke(new Action(() => {
                        progressBarCurrent.Position = 0;
                        progressBarTotal.Position = 0;
                        OrderDown.Items.Insert(0, string.Format("{0}   {1}", DateTime.Now.ToString("HH:mm:ss"), "取消下载!"));
                    }));
                    
                }
            }
            else
            {
                string msg = string.Format("{0}   子线程{1}下载完毕!", DateTime.Now.ToString("HH:mm:ss"), e.Result);
                BeginInvoke(new Action(() => { OrderDown.Items.Insert(0, msg); }));
            }            
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            TradeTask tradeTask = (TradeTask)e.Argument;

            int total_num = tradeTask.totalNum;
            string shopNick = tradeTask.shopNic;
            string sessionKey = tradeTask.sessionKey;
            List<string> tidList = tradeTask.ShopTradeList;

            int CurrentNum = 0;
            BackgroundWorker worker = (BackgroundWorker)sender;

            //用于记录下载失败的订单信息
            failedTrade = new FailedTrade();
            //买家昵称列表
            List<string> buyerNickList = new List<string>();

            for (int i = 0; i < tidList.Count; i++)
            {
                /*线程取消*/
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
              
                try
                {
                    TradeRsp fullinfoTraderspReturn = TopService.TradeFullinfoGet(sessionKey, tidList[i]);
                     
                    if (fullinfoTraderspReturn == null || fullinfoTraderspReturn.Trade == null)
                    {
                        //加锁
                        lock (lockObject)
                        {
                            /*记录下载失败的交易*/
                            failedTrade.tidList.Add(tidList[i]);
                            failedTrade.sessionKey = sessionKey;
                            failedTrade.shopNick = shopNick;
                        }

                        UserState stateFail = new UserState();
                        stateFail.taskIndex = tradeTask.threadIndex;
                        stateFail.tidIndex = i + 1;
                        stateFail.tid = tidList[i];
                        ProcessRecord++;
                        currentProessNum++;
                        stateFail.message = "交易获取失败!";
                        /*订单下载失败*/
                        worker.ReportProgress(-1, stateFail);/*下载失败*/
                        continue;
                    }

                    UserState stateSuccess = new UserState();
                    stateSuccess.taskIndex = tradeTask.threadIndex;
                    stateSuccess.tidIndex = i + 1;
                    currentProessNum++;

                    stateSuccess.tid = tidList[i];
                    stateSuccess.currentProessNum = (int)((currentProessNum * 100) / (float)total_num);
                    ProcessRecord++;
                    stateSuccess.totalProcessNum = (int)((ProcessRecord * 100) / (float)TotalProessNum);
                    worker.ReportProgress(stateSuccess.currentProessNum, stateSuccess);/*下载成功*/

                    Alading.Taobao.Entity.Trade fullinfoTradersp = fullinfoTraderspReturn.Trade;

                    if (TradeService.AddTradeOrderBuyer(fullinfoTradersp) != ReturnType.Success)
                    {
                        UserState SaveFailState = new UserState();
                        SaveFailState.shopNick = shopNick;
                        SaveFailState.message = "保存失败!";
                        worker.ReportProgress(1, SaveFailState);/*参数2：表示保存失败*/
                    }
                }/*try*/
                 catch (System.Exception ex)
                {
                    ProcessRecord++;
                    currentProessNum++;
                    //加锁
                    lock (lockObject)
                    {
                        /*记录下载失败的交易*/
                        failedTrade.tidList.Add(tidList[i]);
                        failedTrade.sessionKey = sessionKey;
                        failedTrade.shopNick = shopNick;
                    }

                    UserState exceptionState = new UserState();
                    exceptionState.message = ex.Message.ToString();
                    exceptionState.taskIndex = tradeTask.threadIndex;
                    exceptionState.tidIndex = i + 1;
                    exceptionState.tid = tidList[i];
                    worker.ReportProgress(-1, exceptionState);
                }
            }/*for 对tid遍历*/

            e.Result = tradeTask.threadIndex;
        }

        /// <summary>
        /// 重新下载失败的订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFailedOrder_Click(object sender, EventArgs e)
        {
            threadCount = Convert.ToInt32(spinEditThreadCount.Value);
            //清空总线程量
            totalThreadCount = 0;
            //当前交易量
            currentProessNum = 0;

            if (failedTrade.tidList != null)
            {
                string inforMsg = string.Format("{0}   {1}", DateTime.Now.ToString("HH:mm:ss"), "重新下载失败的订单!");
                OrderDown.Items.Insert(0, inforMsg);

                /*再开线程处理*/
                BackgroundWorker workerExtent = new BackgroundWorker();
                workerExtent.WorkerReportsProgress = true;
                workerExtent.WorkerSupportsCancellation = true;
                workerExtent.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                workerExtent.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
                workerExtent.DoWork += new DoWorkEventHandler(worker_DoWork);

                /*参数传递*/
                TradeTask tradeTask = new TradeTask();
                tradeTask.shopNic = failedTrade.shopNick;
                tradeTask.ShopTradeList = failedTrade.tidList;
                tradeTask.sessionKey = failedTrade.sessionKey;
                tradeTask.totalNum = failedTrade.tidList.Count;
                tradeTask.threadIndex = 1;
                //线程开启
                workerExtent.RunWorkerAsync(tradeTask);
            }
        }

        public class TradeTask
        {
            public List<string> ShopTradeList { get; set; }
            public int threadIndex { get; set; }
            public string shopNic { get; set; }
            public string sessionKey { get; set; }
            public int totalNum { get; set; }
        }

        public class UserState
        {
            public string tid { get; set; }
            public int taskIndex { get; set; }
            public string message { get; set; }
            public int tidIndex { get; set; }
            public int currentProessNum { get; set; }
            public int totalProcessNum { get; set; }
            public string shopNick { get; set; }
        }

        public class FailedTrade
        {
            public string shopNick { get; set; }
            public List<string> tidList { get; set; }
            public string sessionKey { get; set; }
        }

        /// <summary>
        /// 累加总线程数量
        /// </summary>
        private void AddThreadCount()
        {
            lock (lockObject)
            {
                totalThreadCount++;

                //如果所有的线程都完毕，则让主线程继续工作
                if (totalThreadCount == threadCount)
                {
                    //信号量恢复，下载主线程继续工作
                    autoReset.Set();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        ///// <summary>
        /////绘色
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void OrderDown_DrawItem(object sender, DrawItemEventArgs e)
        //{
        //    Brush FontBrush = null;
        //    ListBox listBox = sender as ListBox;
        //    //listBox.DrawMode = DrawMode.OwnerDrawFixed;
        //    if (e.Index > -1)
        //    {
        //        if (listBox.Items[e.Index].ToString().Contains("失败")
        //            || listBox.Items[e.Index].ToString().Contains("错误")
        //            || listBox.Items[e.Index].ToString().Contains("异常")
        //            || listBox.Items[e.Index].ToString().Contains("网络异常")
        //            )
        //        {
        //            FontBrush = Brushes.Red;
        //        }
        //        else

        //        {
        //            FontBrush = Brushes.Black;
        //        }
        //        e.DrawBackground();
        //        e.Graphics.DrawString(listBox.Items[e.Index].ToString(), e.Font, FontBrush, e.Bounds);
        //        e.DrawFocusRectangle();
        //    }
        //}
    }
}