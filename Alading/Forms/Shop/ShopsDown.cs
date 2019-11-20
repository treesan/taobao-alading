using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Utils;
using Alading.Taobao.Entity.Extend;
using Alading.Taobao.API;
using Alading.Business;
using Alading.Taobao;
using DevExpress.XtraEditors.Controls;

namespace Alading.Forms.Shop
{
    public partial class ShopsDown : DevExpress.XtraEditors.XtraForm
    {
        //全局变量
        private BackgroundWorker worker = null;

        public ShopsDown()
        {
            InitializeComponent();
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

        }

        private void ShopsDown_Load(object sender, EventArgs e)
        {
            List<Alading.Entity.Shop> shoplist = SystemHelper.ShopList;
            foreach (Alading.Entity.Shop shop in shoplist)
            {
                this.cmbNick.Properties.Items.Add(shop.nick);
            }
        }

        private void btnDowmShops_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbNick.Text == string.Empty)
                {
                    XtraMessageBox.Show("请先选择店铺！");
                    return;
                }

                //下载按钮不可用
                this.btnDowmShops.Enabled = false;
               
                List<string> nickList = new List<string>();
                foreach (CheckedListBoxItem item in cmbNick.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        nickList.Add(item.Value.ToString());
                    }
                    else
                    {
                        continue;
                    }
                }

                listBoxDetail.Items.Insert(0, string.Format("一共需要下载{0}个店铺的信息", nickList.Count));
                listBoxDetail.Items.Insert(0, "开始下载……");
                worker.RunWorkerAsync(nickList);
            }
            catch (System.Exception ex)
            {
                this.btnDowmShops.Enabled = true;
                listBoxDetail.Items.Insert(0, ex.Message);
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> nicklist = (List<string>)e.Argument;
            int n = nicklist.Count;
            int temp = 0;//作用是避免进度值propgress没有改变时得重复报告
            BackgroundWorker worker = (BackgroundWorker)sender;
            for (int i = 0; i < n; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                try
                {
                    string session = SystemHelper.GetSessionKey(nicklist[i]);
                    ShopRsp shopRsp = TopService.ShopGet(nicklist[i]);
                    UserRsp userRsp = TopService.UserGet(session, nicklist[i], string.Empty);
                    if (shopRsp != null || userRsp != null)
                    {
                        Alading.Entity.Shop shop = new Alading.Entity.Shop();
                        UIHelper.ShopCopyData(shop, shopRsp.Shop, userRsp.User);
                        ShopService.UpdateShop(shop);
                    }

                    //进度报告
                    int propgress = (int)((float)(i + 1) / n * 100);
                    if (propgress > temp)
                    {
                        MyInfo myInfo = new MyInfo();
                        myInfo.nick = nicklist[i];
                        myInfo.isSucceed = true;
                        worker.ReportProgress(propgress, myInfo);
                    }
                    temp = propgress;
                }
                catch (System.Exception ex)
                {
                    MyInfo myInfo = new MyInfo();
                    myInfo.nick = nicklist[i];
                    myInfo.isSucceed = false;
                    myInfo.msg = ex.Message;
                    worker.ReportProgress(0, myInfo);
                }
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            MyInfo myInfo = (MyInfo)e.UserState;
            if (myInfo.isSucceed)
            {
                this.progressBarTotal.Position = e.ProgressPercentage;
                listBoxDetail.Items.Insert(0, string.Format("成功下载昵称为：{0} 的店铺信息", myInfo.nick));
            }
            else
            {
                listBoxDetail.Items.Insert(0, string.Format("昵称为：{0} 的店铺信息下载错误，错误信息：{1}", myInfo.nick, myInfo.msg));
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.btnDowmShops.Enabled = true;
            if (e.Cancelled)
            {
                XtraMessageBox.Show("已取消下载！");
            }
            else if (e.Error != null)
            {
                XtraMessageBox.Show(String.Format("下载出错！错误信息{0}", e.Error.Message));
            }
            else
            {
                XtraMessageBox.Show("下载成功！"); 
            }
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (worker !=null&&worker.IsBusy)
            {
                worker.CancelAsync();
            }
        }

        private class MyInfo
        {
            /// <summary>
            /// 是否操作成功
            /// </summary>
            public bool isSucceed { get; set; }

            /// <summary>
            /// 异常商品的nick
            /// </summary>
            public string nick { get; set; }

            /// <summary>
            /// 异常信息
            /// </summary>
            public string msg { get; set; }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbNick_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbNick.Text))
            {
                this.btnDowmShops.Enabled = false;
                this.btnCancel.Enabled = false;
            }
            else
            {
                this.btnDowmShops.Enabled = true;
                this.btnCancel.Enabled = true;
            }
        }
    }
}