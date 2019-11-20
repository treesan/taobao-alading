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
using Alading.Business;
using Newtonsoft.Json;
using Alading.Core.Enum;
using System.Web;
using Alading.Taobao;
using Alading.Utils;
using System.Linq;

namespace Alading.Forms.Item
{
    public partial class ItemsDown : DevExpress.XtraEditors.XtraForm
    {
        
        /// <summary>
        /// 记录下载失败的item
        /// </summary>
        FailDownloadeItem failedDownload = new FailDownloadeItem();

        /// <summary>
        /// 下载线程是否被取消
        /// </summary>
        private bool isCancel = false;

        /// <summary>
        /// 已下载商品总数
        /// </summary>
        private int itemCompleteNum = 0;

        /// <summary>
        /// 开启线程总数
        /// </summary>
        private int threadnum = 0;

        /// <summary>
        /// 线程完成总数
        /// </summary>
        private int threadCompleteNum = 0;


        public ItemsDown()
        {
            InitializeComponent();
        }

        private void btnDowmItems_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbNick.Text==string.Empty)
                {
                    XtraMessageBox.Show("请先选择店铺！","提示");
                    return;
                }
                this.btnDowmItems.Enabled = false;//下载按钮不可用
                this.btnRetry.Enabled = false;
                itemCompleteNum = 0;//下载完成数置0
                threadCompleteNum = 0;//完成线程数置0
                int pageSize =200;
                int pageNo = 1;
                string fields = "iid,cid";
                ItemRsp myrsp = null;
                string session = SystemHelper.GetSessionKey(cmbNick.Text);
                myrsp = TopService.ItemsAllGet(session, fields, 1, pageSize);
                if (myrsp.TotalResults==0)
                {
                    this.btnDowmItems.Enabled = true;
                    XtraMessageBox.Show("无宝贝数据！");
                    return;
                }
                listBoxDetail.Items.Insert(0,string.Format("检测到店铺内宝贝数{0}个", myrsp.TotalResults));
                listBoxDetail.Items.Insert(0, "开始下载……");
                List<string> totalIidList = new List<string>();
                /*把iid加入到iidlist中*/
                if (myrsp.Items != null && myrsp.Items.Item != null)
                {
                    foreach (Taobao.Entity.Item item in myrsp.Items.Item)
                    {
                        totalIidList.Add(item.Iid);
                    }
                }

                #region 计算是否需要分页获取
                if (myrsp.TotalResults % pageSize==0)
                {
                    pageNo = myrsp.TotalResults / pageSize;
                }
                else
                {
                    pageNo = myrsp.TotalResults / pageSize + 1;
                }
                if (pageNo >= 2)
                {
                    for (int i = 2; i <= pageNo; i++)
                    {
                        myrsp = TopService.ItemsAllGet(session,"iid",  i,pageSize);
                        if (myrsp.Items != null && myrsp.Items.Item != null)
                        {
                            foreach (Taobao.Entity.Item item in myrsp.Items.Item)
                            {
                                if (!totalIidList.Contains(item.Iid))
                                {
                                    totalIidList.Add(item.Iid);
                                }
                            }
                        }
                    }//for
                }//if
                #endregion
                //进度条最大值赋值
                this.progressBarTotal.Properties.Maximum = totalIidList.Count;

                //线程数，全局变量
                 threadnum = (int)spinEditThreadCount.Value;
                int size = 0;
                if (totalIidList.Count < threadnum)
                {
                    threadnum = totalIidList.Count;
                    size = 1;
                }
                else
                {
                    size = totalIidList.Count % threadnum == 0 ? totalIidList.Count / threadnum : totalIidList.Count / threadnum + 1;
                }
                for (int i = 0; i < threadnum; i++)
                {
                    List<string> iidlist = totalIidList.Skip(i * size).Take(size).ToList();
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.WorkerReportsProgress = true;
                    worker.WorkerSupportsCancellation = true;
                    worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                    worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                    listBoxDetail.Items.Insert(0, string.Format("{0}线程{1}开始下载……", DateTime.Now.ToString(Constants.DATE_TIME_FORMAT), i + 1));
                    ItemTask itemTask = new ItemTask();
                    itemTask.iidList = iidlist;
                    itemTask.threadIndex = i + 1;
                    itemTask.sessionKey = session;
                    itemTask.shopNic = cmbNick.Text;
                    worker.RunWorkerAsync(itemTask);
                }

                
            }
            catch (System.Exception ex)
            {
                this.btnDowmItems.Enabled = true;
                listBoxDetail.Items.Insert(0,ex.Message);
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            ItemTask itemTask = e.Argument as ItemTask;
            List<string> iidlist = itemTask.iidList;
            BackgroundWorker worker = (BackgroundWorker)sender;
            int n = iidlist.Count;
            for (int i=0;i<n;i++)
            {
                //完成数加1
                itemCompleteNum++;
                if (isCancel)
                {
                    e.Cancel = true;
                    break;
                }
                string iid= iidlist[i];
                ReturnType returntype=ItemService.IsItemExisted(iid);
                if (checkEditIsUpdate.CheckState != CheckState.Checked && returntype == ReturnType.PropertyExisted)
                {
                    /*跳过本次下载*/
                    worker.ReportProgress(-2, string.Format("线程号{0},数据库已存在第{1}个宝贝，跳过下载",itemTask.threadIndex, i + 1));
                    continue;
                }
                string session = itemTask.sessionKey;
                string nick = itemTask.shopNic;
                try
                {
                    ItemRsp myrsp = TopService.ItemGet(session, nick, iid, string.Empty);
                    if (myrsp != null && myrsp.Item != null)
                    {
                        Alading.Entity.Item item = new Alading.Entity.Item();
                        UIHelper.ItemCopyData(item, myrsp.Item);
                        ItemService.AddItem(item, checkEditIsUpdate.Checked);
                    }
                    else
                    {
                        if (failedDownload.iidList==null)
                        {
                            failedDownload.iidList = new List<string>();
                        }
                        failedDownload.iidList.Add(iid);
                        failedDownload.shopNick = nick;
                        failedDownload.sessionKey = session;
                    }
                    //int progress = (int)((float)(i + 1) / (float)n * 100);
                    worker.ReportProgress(itemCompleteNum, string.Format("{0}已成功下载第{1}个宝贝,线程号{2}", DateTime.Now.ToString(Constants.DATE_TIME_FORMAT), itemCompleteNum, itemTask.threadIndex));
                }
                catch (System.Exception ex)
                {
                    MyException myexc = new MyException();
                    myexc.position = i + 1;
                    myexc.iid = iid;
                    myexc.msg = ex.Message;
                    myexc.threadIndex = itemTask.threadIndex;
                    worker.ReportProgress(-1, myexc);

                    if (failedDownload.iidList == null)
                    {
                        failedDownload.iidList = new List<string>();
                    }
                    failedDownload.iidList.Add(iid);
                    failedDownload.shopNick = nick;
                    failedDownload.sessionKey = session;
                }
            }
            e.Result = itemTask.threadIndex;
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage==-1)
            {
                MyException myexc=(MyException)e.UserState;
                listBoxDetail.Items.Insert(0, string.Format("线程号{0},第{1}个宝贝下载错误，iid为{2}，错误信息：{3}", myexc.threadIndex, myexc.position, myexc.iid, myexc.msg));
            }
            else if (e.ProgressPercentage==-2)
            {
                listBoxDetail.Items.Insert(0, e.UserState.ToString());
            }
            else
            {
                this.progressBarTotal.Position = e.ProgressPercentage;
                listBoxDetail.Items.Insert(0,  e.UserState.ToString());
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            threadCompleteNum++;
            if (threadCompleteNum == threadnum)
            {
                this.btnDowmItems.Enabled = true;
            }
            if (e.Cancelled)
            {
                listBoxDetail.Items.Insert(0, "下载被取消!");
                if (threadCompleteNum == threadnum)
                {
                    XtraMessageBox.Show("下载被取消！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (e.Error != null)
            {
                listBoxDetail.Items.Insert(0, e.Error.Message);
                if (threadCompleteNum == threadnum)
                {
                    XtraMessageBox.Show("下载出错!", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                listBoxDetail.Items.Insert(0, string.Format("{0}线程号{1}下载成功!",DateTime.Now.ToString(Constants.DATE_TIME_FORMAT), e.Result ?? string.Empty));
                if (threadCompleteNum == threadnum)
                {
                    XtraMessageBox.Show("下载成功！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (threadCompleteNum == threadnum && failedDownload.iidList != null && failedDownload.iidList.Count>0)
            {
                this.btnRetry.Enabled = true;
                //提示是否要重新下载
                string message =string.Format("您的店铺{0}共有{1}个宝贝没有下载成功,是否要重新下载?",failedDownload.shopNick,failedDownload.iidList.Count) ;
                if (XtraMessageBox.Show(message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    this.btnRetry.Enabled = false;
                    threadnum --;//线程数必须减1
                    itemCompleteNum = 0;//完成数置0

                    string inforMsg = string.Format("{0}   {1}", DateTime.Now.ToString("HH:mm:ss"), "重新下载失败的宝贝!");
                    listBoxDetail.Items.Insert(0, inforMsg);

                    /*再开线程处理*/
                    BackgroundWorker workerExtent = new BackgroundWorker();
                    workerExtent.WorkerReportsProgress = true;
                    workerExtent.WorkerSupportsCancellation = true;
                    workerExtent.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                    workerExtent.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
                    workerExtent.DoWork += new DoWorkEventHandler(worker_DoWork);

                    /*参数传递*/
                    ItemTask tradeTask = new ItemTask();
                    tradeTask.shopNic = failedDownload.shopNick;
                    tradeTask.iidList = failedDownload.iidList;
                    tradeTask.sessionKey = failedDownload.sessionKey;
                    tradeTask.totalNum = failedDownload.iidList.Count;
                    tradeTask.threadIndex = 1;
                    //重新设定进度条最大值
                     this.progressBarTotal.Properties.Maximum = tradeTask.totalNum;
                    //重新实例化
                    failedDownload = new FailDownloadeItem();
                    //线程开启
                    workerExtent.RunWorkerAsync(tradeTask);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            isCancel = true;
        }        

        private class MyException
        {
            /// <summary>
            /// 错误商品位置
            /// </summary>
            public int position { get; set; }

            /// <summary>
            /// 异常商品的iid
            /// </summary>
            public string iid { get; set; }

            /// <summary>
            /// 异常信息
            /// </summary>
            public string msg { get; set; }

            /// <summary>
            /// 线程序号
            /// </summary>
            public int threadIndex { get; set; }
        }

        private class FailDownloadeItem
        {
            public List<string> iidList { get; set; }
            public string sessionKey { get; set; }
            public string shopNick { get; set; }
        }

        public class ItemTask
        {
            public List<string> iidList { get; set; }
            public int threadIndex { get; set; }
            public string shopNic { get; set; }
            public string sessionKey { get; set; }
            public int totalNum { get; set; }
        }

        private void ItemsDown_Load(object sender, EventArgs e)
        {
            List<Alading.Entity.Shop> shoplist = SystemHelper.ShopList;
            foreach (Alading.Entity.Shop shop in shoplist)
            {
                this.cmbNick.Properties.Items.Add(shop.nick);
            }
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            this.btnRetry.Enabled = false;
            threadnum--;//线程数必须减1
            itemCompleteNum = 0;//完成数置0

            string inforMsg = string.Format("{0}   {1}", DateTime.Now.ToString("HH:mm:ss"), "重新下载失败的宝贝!");
            listBoxDetail.Items.Insert(0, inforMsg);

            /*再开线程处理*/
            BackgroundWorker workerExtent = new BackgroundWorker();
            workerExtent.WorkerReportsProgress = true;
            workerExtent.WorkerSupportsCancellation = true;
            workerExtent.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            workerExtent.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            workerExtent.DoWork += new DoWorkEventHandler(worker_DoWork);

            /*参数传递*/
            ItemTask tradeTask = new ItemTask();
            tradeTask.shopNic = failedDownload.shopNick;
            tradeTask.iidList = failedDownload.iidList;
            tradeTask.sessionKey = failedDownload.sessionKey;
            tradeTask.totalNum = failedDownload.iidList.Count;
            tradeTask.threadIndex = 1;
            //重新设定进度条最大值
            this.progressBarTotal.Properties.Maximum = tradeTask.totalNum; 
            //重新实例化
            failedDownload = new FailDownloadeItem();
            //线程开启
            workerExtent.RunWorkerAsync(tradeTask);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbNick_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbNick.Text))
            {
                this.btnDowmItems.Enabled = false;
                this.btnCancel.Enabled = false;
            }
            else
            {
                this.btnDowmItems.Enabled = true;
                this.btnCancel.Enabled = true;
            }
        }

    }
}