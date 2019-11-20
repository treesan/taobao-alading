using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Utils;
using Alading.Taobao.API;
using Alading.Taobao.Entity.Extend;

namespace Alading.Forms.Item
{
    public partial class ItemBatchUpdate : DevExpress.XtraEditors.XtraForm
    {
        private SortedList<string, List<string>> sortItemList;

        /// <summary>
        /// 成功商品列表
        /// </summary>
        public List<string> iidlist = new List<string>();
       

        public ItemBatchUpdate()
        {
            InitializeComponent();
        }

        public ItemBatchUpdate(SortedList<string, List<string>> sortItemList)
        {
            InitializeComponent();
            this.sortItemList = sortItemList;
        }

        private void backWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                foreach (string nick in sortItemList.Keys)
                {
                    if (backWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                    if (sortItemList[nick].Count == 0)
                    {
                        continue;
                    }
                    BeginInvoke(new Action(() =>
                        {
                            this.progressBarControl1.Properties.Maximum = sortItemList[nick].Count;
                        }
                        ));
                    string session = SystemHelper.GetSessionKey(nick);
                    for (int i = 0; i < sortItemList[nick].Count; i++)
                    {
                        if (backWorker.CancellationPending)
                        {
                            e.Cancel = true;
                            break;
                        }
                        string iid = sortItemList[nick][i];
                        ItemRsp rsp = TopService.ItemUpdateListing(session, iid);
                        if (rsp != null && rsp.Item != null)
                        {
                            //加入成功列表
                            iidlist.Add(iid);
                        }
                        backWorker.ReportProgress(i + 1, string.Format("正在更新店铺{0}的宝贝", nick));
                    }
                }
            }
            catch (Exception ex)
            {
               e.Result=ex.Message;
            }
        }

     

        private void backWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBarControl1.Position = e.ProgressPercentage;
            this.barStaticItemTask.Caption = e.UserState.ToString();
        }

        private void backWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
            if (e.Cancelled)
            {
                XtraMessageBox.Show("已取消更新！");
            }
            else if (e.Error != null)
            {
                XtraMessageBox.Show(String.Format("更新出错！错误信息{0}", e.Error.Message));
            }
            else if(e.Result!=null)
            {
                XtraMessageBox.Show(e.Result.ToString());
            }
            else
            {
                XtraMessageBox.Show("更新成功!");
            }
        }

        private void ItemBatchUpdate_Load(object sender, EventArgs e)
        {
            this.barStaticItemTask.Caption = sortItemList.Keys[0];
            backWorker.RunWorkerAsync();
        }

        private void ItemBatchUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backWorker.IsBusy)
            {
                backWorker.CancelAsync();
            }
        }
    }
}