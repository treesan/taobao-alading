using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Business;
using Alading.Taobao.Entity.Extend;
using System.Linq;
using Alading.Utils;
using Alading.Taobao.API;

namespace Alading.Forms.Item
{
    public partial class UpdateItem : DevExpress.XtraEditors.XtraForm
    {
        private SortedList<string, Dictionary<string, string>> AutoListDic;
        private BackgroundWorker backWorker=new BackgroundWorker();

        public UpdateItem()
        {
            InitializeComponent();
        }
        public UpdateItem(SortedList<string, Dictionary<string, string>> AutoListDic)
        {
            InitializeComponent();
            this.AutoListDic = AutoListDic;
            backWorker.WorkerReportsProgress = true;
            backWorker.WorkerSupportsCancellation = true;
            backWorker.DoWork += new DoWorkEventHandler(backWorker_DoWork);
            backWorker.ProgressChanged += new ProgressChangedEventHandler(backWorker_ProgressChanged);
            backWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backWorker_RunWorkerCompleted);
        }

        void backWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (string nick in AutoListDic.Keys)
            {
                if (backWorker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                if (AutoListDic[nick].Count==0)
                {
                    continue;
                }
                backWorker.ReportProgress(0, string.Format("正在更新店铺{0}的宝贝", nick));
                Dictionary<string, string> itemdic = UpdateListPlan(nick, AutoListDic[nick], backWorker, e);
                ItemService.UpdateItemsListTime(itemdic);
            }
        }

        void backWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState==null)
            {
                this.progressBarControl1.Position = e.ProgressPercentage;
            }
            else if (e.UserState!=null)
            {
                this.barStaticItemTask.Caption = e.UserState.ToString();
            }
        }

        void backWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
            if (e.Cancelled)
            {
                XtraMessageBox.Show("已取消更新！");
            }
            else if (e.Error != null)
            {
                XtraMessageBox.Show(String.Format("更新出错！错误信息{0}",e.Error.Message));
            }
            else
            {
                XtraMessageBox.Show("更新成功!");
            }
        }

        /// <summary>
        /// 将上架计划列表中的宝贝更新到淘宝，返回成功宝贝的iid
        /// </summary>
        /// <param name="itemdic"></param>
        /// <returns></returns>
        private Dictionary<string, string> UpdateListPlan(string nick, Dictionary<string, string> itemdic, BackgroundWorker backWorker, DoWorkEventArgs e)
        {
            Dictionary<string, string> successitems = new Dictionary<string, string>();
            List<string> iidlist = itemdic.Keys.ToList();
            ItemReq req = new ItemReq();
            ItemRsp response = new ItemRsp();
            string session = SystemHelper.GetSessionKey(nick);
            float n = iidlist.Count;
            int temp = 0;//作用是避免进度值propgress没有改变时得重复报告
            for (int i = 0; i < n; i++)
            {
                if (backWorker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                string iid =iidlist[i];
                req.Iid = iid;
                req.ApproveStatus = "onsale";
                req.ListTime = itemdic.SingleOrDefault(it => it.Key == iid).Value;
                try
                {
                    response = TopService.ItemUpdate(session, req);
                    if (response.Item!=null)
                    {
                        successitems.Add(req.Iid, req.ListTime);
                    }
                }
                catch (Exception)
                {
                    continue;
                }

                //进度报告
                int propgress = (int)((float)(i + 1) / n * 100);
                if (propgress > temp)
                {
                    backWorker.ReportProgress(propgress, null);
                }
                temp = propgress;
            }
            return successitems;
        }

        private void UpdateItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backWorker.IsBusy)
            {
                backWorker.CancelAsync();
            }
        }

        private void UpdateItem_Load(object sender, EventArgs e)
        {
            if (this.AutoListDic.Count == 0)
            {
                return;
            }
            backWorker.RunWorkerAsync();
        }

    }
}