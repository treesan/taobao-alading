using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using Alading.Utils;
using Alading.Taobao.Entity.Extend;
using Alading.Taobao.API;
using System.IO;
using System.Net;
using System.Linq;
using DevExpress.XtraTreeList.Nodes;
using Alading.Taobao;
using System.Collections;
using Alading.Core.Enum;
using Alading.Business;

namespace Alading.Forms.Init.Control
{
    public partial class InitShopControl : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 子线程列表
        /// </summary>
        List<BackgroundWorker> workerList = new List<BackgroundWorker>();

        /// <summary>
        /// Item列表，供图片下载线程
        /// </summary>
        List<Taobao.Entity.Item> itemList = new List<Alading.Taobao.Entity.Item>();

        /// <summary>
        /// 锁对象
        /// </summary>
        static object lockObject = new object();

        /// <summary>
        /// 若为true则非阻塞状态，为false为阻塞状态。AutoResetEvent一次只唤醒一个线程，ManualResetEvent则可以唤醒多个线程。
        /// </summary>
        AutoResetEvent autoReset = new AutoResetEvent(false);

        /// <summary>
        /// 总的线程数量，由于下载图片的线程与Item相同，则totalThreadCount=threadCount*2
        /// </summary>
        int totalThreadCount = 0;

        /// <summary>
        /// 记录当前进度
        /// </summary>
        int progressCurrent = 0;

        /// <summary>
        /// 子线程的数量
        /// </summary>
        int threadCount = 0;

        int pageSize = 200;
        int pageNo = 1;
        int itemPageSize = 0;
        ItemRsp itemRsp = null;
        string fields = "approve_status,iid,num_iid,title,nick,type,cid,pic_url,num,props,valid_thru,list_time,price,has_discount,has_invoice,has_warranty,has_showcase, modified,delist_time,postage_id,seller_cids,outer_id";
        BackgroundWorker mainWorker = new BackgroundWorker();

        public List<Alading.Entity.Shop> ShopList { get; set; }

        public InitShopControl()
        {
            InitializeComponent();
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
                if (totalThreadCount == threadCount * 2)
                {
                    //信号量恢复，下载主线程继续工作
                    autoReset.Set();
                }
            }
        }

        public void Initialize()
        {
            List<ShopEntity> shopEntityList = null;
            if (this.ShopList != null)
            {
                shopEntityList = new List<ShopEntity>();
                foreach (var shop in this.ShopList)
                {
                    ShopEntity shopEntity = new ShopEntity();
                    shopEntity.IsSelected = true;
                    shopEntity.DownloadStatus = "等待同步";
                    SystemHelper.AutoCopyData(shopEntity, shop);
                    shopEntityList.Add(shopEntity);
                }
            }
            //绑定店铺
            this.treeListShop.DataSource = shopEntityList;
        }

        private void InitShopControl_Load(object sender, EventArgs e)
        {
            mainWorker.WorkerSupportsCancellation = true;
            mainWorker.WorkerReportsProgress = true;
            mainWorker.DoWork += new DoWorkEventHandler(mainWorker_DoWork);
            mainWorker.ProgressChanged += new ProgressChangedEventHandler(mainWorker_ProgressChanged);
            mainWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(mainWorker_RunWorkerCompleted);
        }

        void mainWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                listBoxCtrl.Items.Insert(0, DateTime.Now.ToString(Constants.DATE_TIME_FORMAT) + "下载被取消！");
                progressCurrent = 0;
                progressBarCtrl.Position = 0;
            }
            /*主线程置空*/
            if (mainWorker != null)
            {
                mainWorker = null;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                //listbox清空
                listBoxCtrl.Items.Clear();
                if (mainWorker == null)
                {
                    mainWorker = new BackgroundWorker();
                    mainWorker.WorkerSupportsCancellation = true;
                    mainWorker.WorkerReportsProgress = true;
                    mainWorker.DoWork += new DoWorkEventHandler(mainWorker_DoWork);
                    mainWorker.ProgressChanged += new ProgressChangedEventHandler(mainWorker_ProgressChanged);
                    mainWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(mainWorker_RunWorkerCompleted);
                }
                //开启下载主线程            
                mainWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void mainWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BeginInvoke(new Action(() => { listBoxCtrl.Items.Insert(0, e.UserState.ToString()); }));
        }

        #region 下载任务主线程
        void mainWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            threadCount = Convert.ToInt32(spinEditThreadCount.Text);
            string nick = string.Empty;
            string title = string.Empty;
            string sid = string.Empty;
            bool isSelected;
            TreeListNode node = null;

            //循环shop树，获得选中的店铺
            IEnumerator treeListEnumerator = this.treeListShop.Nodes.GetEnumerator();
            while (treeListEnumerator.MoveNext())
            {
                if (mainWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                try
                {
                    node = treeListEnumerator.Current as TreeListNode;
                    isSelected = Convert.ToBoolean(node[treeListShop.Columns["IsSelected"].AbsoluteIndex]);

                    if (node[treeListShop.Columns["nick"].AbsoluteIndex] != null)
                    {
                        nick = node[treeListShop.Columns["nick"].AbsoluteIndex].ToString();
                    }
                    if (node[treeListShop.Columns["title"].AbsoluteIndex] != null)
                    {
                        title = node[treeListShop.Columns["title"].AbsoluteIndex].ToString();
                    }

                    if (node[treeListShop.Columns["sid"].AbsoluteIndex] != null)
                    {
                        sid = node[treeListShop.Columns["sid"].AbsoluteIndex].ToString();
                    }

                    if (isSelected)
                    {
                        //线程总数量设为0
                        totalThreadCount = 0;

                        //清空itemList
                        itemList.Clear();

                        //清空子线程列表
                        workerList.Clear();

                        worker.ReportProgress(1, DateTime.Now.ToString(Constants.DATE_TIME_FORMAT) + " 正在获取店铺" + title + "的宝贝信息......");

                        //获取选中店铺所有的item
                        itemRsp = TopService.ItemsAllGet(SystemHelper.GetSessionKey(nick), fields, 1, pageSize);
                        if (itemRsp == null)
                        {
                            worker.ReportProgress(1, title + "店铺宝贝获取失败。");
                        }
                        else
                        {
                            /*把item加入到itemlist中*/
                            if (itemRsp.Items != null && itemRsp.Items.Item != null)
                            {
                                foreach (Taobao.Entity.Item item in itemRsp.Items.Item)
                                {
                                    itemList.Add(item);
                                }
                            }

                            worker.ReportProgress(1, string.Format("检测到店铺内宝贝{0}个。", itemRsp.TotalResults));

                            //计算页数
                            pageNo = itemRsp.TotalResults % pageSize == 0 ? (itemRsp.TotalResults / pageSize) : ((itemRsp.TotalResults / pageSize) + 1);

                            //如果超过一页，即超过200条
                            if (pageNo > 1)
                            {
                                //获取所有的item列表
                                worker.ReportProgress(1, "正在获取店铺宝贝列表......");

                                //下载其它页
                                for (int i = 2; i <= pageNo; i++)
                                {
                                    itemRsp = TopService.ItemsAllGet(SystemHelper.GetSessionKey(nick), fields, i, pageSize);
                                    if (itemRsp.Items != null && itemRsp.Items.Item != null)
                                    {
                                        foreach (Taobao.Entity.Item item in itemRsp.Items.Item)
                                        {
                                            itemList.Add(item);
                                        }
                                    }
                                }
                                worker.ReportProgress(1, "店铺宝贝列表获取成功。");
                            }

                            worker.ReportProgress(1, "系统正在启动多线程下载宝贝详细信息......");

                            //如果商品的数量小于
                            if (itemRsp.TotalResults < threadCount)
                            {
                                threadCount = itemRsp.TotalResults;
                            }

                            //计算每个子线程应该分配多少item去下载,itemPageSize
                            itemPageSize = itemRsp.TotalResults % threadCount == 0 ? (itemRsp.TotalResults / threadCount) : ((itemRsp.TotalResults / threadCount) + 1);

                            #region 开启下载子线程
                            //开启Item下载子线程
                            for (int i = 0; i < threadCount; i++)
                            {
                                //分页item，分配给每一个子线程。
                                List<Taobao.Entity.Item> items = itemList.Skip(i * itemPageSize).Take(itemPageSize).ToList();

                                //启动线程
                                BackgroundWorker itemWorker = new BackgroundWorker();
                                itemWorker.WorkerReportsProgress = true;
                                itemWorker.WorkerSupportsCancellation = true;
                                itemWorker.DoWork += new DoWorkEventHandler(worker_DoWork);
                                //任务进行时，报告进度
                                itemWorker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
                                //任务完称时要做的，比如提示等等
                                itemWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

                                workerList.Add(itemWorker);

                                //Item任务对象
                                ItemTask itemTask = new ItemTask();
                                itemTask.ItemList = items;
                                itemTask.ShopSid = sid;
                                itemTask.Index = i + 1;
                                itemTask.totalCount = itemRsp.TotalResults;
                                //每一个子线程传入相关的下载列表信息。
                                itemWorker.RunWorkerAsync(itemTask);

                                //开启图片下载子线程
                                BackgroundWorker picWorker = new BackgroundWorker();
                                picWorker.WorkerReportsProgress = true;
                                picWorker.WorkerSupportsCancellation = true;
                                picWorker.DoWork += new DoWorkEventHandler(picWorker_DoWork);
                                picWorker.ProgressChanged += new ProgressChangedEventHandler(picWorker_ProgressChanged);
                                picWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(picWorker_RunWorkerCompleted);

                                workerList.Add(picWorker);

                                //ItemPic任务对象
                                ItemTask itemPicTask = new ItemTask();
                                itemPicTask.ItemList = items;
                                itemPicTask.ShopSid = sid;
                                picWorker.RunWorkerAsync(itemPicTask);
                            }
                            #endregion
                        }

                        progressCurrent = 0;
                        //信号量，阻塞当前线程，这里阻塞的是下载主线程
                        autoReset.WaitOne();
                    }
                }
                catch (Exception ex)
                {
                    BeginInvoke(new Action(() => { listBoxCtrl.Items.Insert(0, ex.Message); }));
                    return;
                }                
            }
        }
        #endregion

        #region 下载图片子线程
        void picWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BeginInvoke(new Action(() => { listBoxCtrl.Items.Insert(0, DateTime.Now.ToString(Constants.DATE_TIME_FORMAT) + " 图片下载完毕。"); }));
            AddThreadCount();
        }

        void picWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BeginInvoke(new Action(() => { listBoxCtrl.Items.Insert(0, DateTime.Now.ToString(Constants.DATE_TIME_FORMAT) + e.UserState.ToString()); }));
        }

        void picWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ItemTask itemTask = e.Argument as ItemTask;
            List<Taobao.Entity.Item> items = itemTask.ItemList;
            BackgroundWorker worker = (BackgroundWorker)sender;
            //下载图片
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            int count = 0;
            long totalBytes = 0;
            int bufSize = 8192;
            //程序运行根目录
            StringBuilder dirBuilder = new StringBuilder();
            dirBuilder.Append(Application.StartupPath);
            dirBuilder.Append("\\ItemPics\\");
            dirBuilder.Append(itemTask.ShopSid);
            dirBuilder.Append("\\");
            string picDirectory = dirBuilder.ToString();
            //先检查是否存在该文件夹，没有则创建。再判断是否存在文件。
            if (!Directory.Exists(picDirectory))
            {
                Directory.CreateDirectory(picDirectory);
            }



            for (int i = 0; i < items.Count; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                //检查是否存在图片
                string picPath = string.Empty;
                if (!string.IsNullOrEmpty(items[i].PicUrl))
                {
                    picPath = picDirectory + Path.GetFileName(items[i].PicUrl);
                    //如果不存在图片文件，则下载
                    if (!File.Exists(picPath))
                    {
                        try
                        {
                            request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(items[i].PicUrl);
                            response = (System.Net.HttpWebResponse)request.GetResponse();

                            //文件总长度
                            totalBytes = response.ContentLength;
                            System.IO.Stream stream = response.GetResponseStream();

                            //用于存储全部的数据
                            byte[] bytes = new byte[totalBytes];

                            //用于每次读取
                            byte[] buf = new byte[bufSize];
                            int totalCount = 0;

                            do
                            {
                                //填充数据，count表示读取了多少，并非一次全部读取成功
                                count = stream.Read(buf, 0, buf.Length);

                                //将本次读取的数据放入bytes数组中
                                if (count != 0)
                                {
                                    Buffer.BlockCopy(buf, 0, bytes, totalCount, count);
                                }

                                //当前读取到的数据总长度
                                totalCount += count;
                            }
                            //是否还需要继续读取数据，如果count>0表示没有读取完成
                            while (count > 0);

                            // 把 byte[] 写入文件 
                            FileStream fs = new FileStream(picPath, FileMode.Create);

                            //BinaryWriter最后一次将bytes全部写入文件,减少i/o操作
                            BinaryWriter bw = new BinaryWriter(fs);
                            bw.Write(bytes);
                            bw.Close();
                            fs.Close();
                            stream.Close();
                            //关闭流
                            response.Close();
                        }
                        catch (Exception ex)
                        {
                            worker.ReportProgress(0, ex.Message);
                            continue;
                        }
                        finally
                        {
                            if (response != null)
                            {
                                response.Close();
                            }

                            //防止线程被阻塞
                            if (request != null)
                            {
                                request.Abort();
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 下载Item子线程
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AddThreadCount();
            if (!e.Cancelled)
            {
                BeginInvoke(new Action(() => { listBoxCtrl.Items.Insert(0, string.Format(DateTime.Now.ToString(Constants.DATE_TIME_FORMAT) + " 子线程{0}下载完毕。", e.Result)); }));
            }
        }
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UserState state = e.UserState as UserState;
            if (e.ProgressPercentage == -1)
            {
                string msg = string.Format("子线程{0},第{1}个宝贝下载错误，编号为：{2}。错误信息：{3}。", state.ItemTaskIndex, state.Index, state.Iid, state.Message);
                BeginInvoke(new Action(() => { listBoxCtrl.Items.Insert(0, msg); }));
            }
            else
            {
                BeginInvoke(new Action(() =>
                {
                    this.progressBarCtrl.Position = e.ProgressPercentage;
                    listBoxCtrl.Items.Insert(0, string.Format("子线程{0},第{1}个宝贝下载成功，编号为：{2}。", state.ItemTaskIndex, progressCurrent, state.Iid));
                }));
            }

        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            ItemTask itemTask = e.Argument as ItemTask;
            List<Taobao.Entity.Item> items = itemTask.ItemList;
            BackgroundWorker worker = (BackgroundWorker)sender;
            int totalCount = itemTask.totalCount;
            for (int i = 0; i < items.Count; i++)
            {

                lock (lockObject)
                {
                    //进度递增
                  progressCurrent++;
                }

                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                try
                {
                    ItemRsp myrsp = TopService.ItemGet(SystemHelper.GetSessionKey(items[i].Nick), items[i].Nick, items[i].Iid, string.Empty);
                    if (myrsp != null && myrsp.Item != null)
                    {
                        Alading.Entity.Item item = new Alading.Entity.Item();
                        UIHelper.ItemCopyData(item, myrsp.Item);
                        //添加到数据库，如果存在则更新，否则添加
                        ReturnType result = ItemService.AddItem(item);

                        //计算进度条值
                        int progress = (int)((float)progressCurrent / (float)totalCount * 100);
                        if (result == ReturnType.Success)
                        {
                            UserState state = new UserState();
                            state.Index = i + 1;
                            state.Iid = items[i].Iid;
                            state.ItemTaskIndex = itemTask.Index;
                            state.Message = "下载成功!";
                            worker.ReportProgress(progress, state);
                        }
                        else
                        {
                            UserState state = new UserState();
                            state.Message = "数据库存储发生错误。";
                            state.Index = i + 1;
                            state.Iid = items[i].Iid;
                            state.ItemTaskIndex = itemTask.Index;

                            //发生数据库错误的报告
                            worker.ReportProgress(-1, state);
                        }
                    }
                }
                catch (Exception ex)
                {
                    UserState state = new UserState();
                    state.Message = ex.Message;
                    state.Index = i + 1;
                    state.Iid = items[i].Iid;
                    state.ItemTaskIndex = itemTask.Index;

                    //发生下载错误的报告
                    worker.ReportProgress(-1, state);
                }
            }


            e.Result = itemTask.Index;
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {

            if (mainWorker != null && mainWorker.IsBusy)
            {
                mainWorker.CancelAsync();
            }

            for (int i = 0; i < workerList.Count; i++)
            {
                if (workerList[i] != null && workerList[i].IsBusy)
                {
                    workerList[i].CancelAsync();
                }
            }
        }
    }
}
