using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Alading.Forms.Email
{
    public partial class EmailManage : DevExpress.XtraEditors.XtraForm
    {
        private int pageSize = 20;

        private LoadEmailStrategy loadStrategy=null;

        private Alading.Entity.ConsumerVisit selectedEmail = null;

        private List<Alading.Entity.ConsumerVisit> emailList = new List<Alading.Entity.ConsumerVisit>();
        private LoadEmailStrategy LoadStrategy
        {
            get { return loadStrategy; }
            set {
                if (value != null)
                {
                    loadStrategy = value;
                    loadStrategy.PageIndex = 1;
                    loadStrategy.PageSize = pageSize;
                    LoadEmail();
                }
            }
        }

        private void LoadAllEmail()
        {
            LoadStrategy = new SearchAll();
        }

        private void LoadEmail()
        {
            if (loadStrategy != null)
            {
                loadStrategy.PageSize = pageSize;
                gcEmailList.DataSource = null;
                emailList = loadStrategy.LoadEmail();
                gcEmailList.DataSource = emailList;

                btPrevPage.Enabled = loadStrategy.PageIndex > 1;
                btNextPage.Enabled = loadStrategy.PageIndex < loadStrategy.PageCount;

            }
        }
        public EmailManage()
        {
            InitializeComponent();
        }

        private void EmailManage_Load(object sender, EventArgs e)
        {
            LoadAllEmail();
        }

        private void btFirstPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (loadStrategy != null)
            {
                //首页邮件表
                loadStrategy.PageIndex = 1;
                loadStrategy.PageSize = pageSize;
                LoadEmail();
            }
        }

        private void btPrevPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //上一页邮件表
            if (loadStrategy != null && loadStrategy.PageIndex > 1)
            {
                loadStrategy.PageIndex--;
                LoadEmail();
            }
        }

        private void btNextPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //下一页邮件表
            if (loadStrategy != null && loadStrategy.PageIndex < loadStrategy.PageCount)
            {
                loadStrategy.PageIndex++;
                LoadEmail();
            }
        }

        private void btLastPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (loadStrategy != null && loadStrategy.PageCount > 1)
            {
                loadStrategy.PageIndex = loadStrategy.PageCount;
                loadStrategy.PageSize = pageSize;
                LoadEmail();
            }
        }

        private void btRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadEmail();
        }
       
        private void btAllMail_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            // 查看所有邮件
            LoadAllEmail();
        }

        private void btToSend_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看待发邮件
            LoadStrategy =new LoadTypeEmail()
            {
                State="待发送"
            };
        }

        private void btSend_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看已发邮件
            LoadStrategy = new LoadTypeEmail()
            {
                State = "已发送"
            };
        }

        private void btFailed_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //查看已发邮件
            LoadStrategy = new LoadTypeEmail()
            {
                State = "发送失败"
            };
        }

        private void gvEmailList_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            int[] rows = gvEmailList.GetSelectedRows();
            selectedEmail=emailList[rows[0]];
            FillEmailInfo();
        }

        private void FillEmailInfo()
        {
            txConsumer.Text = selectedEmail.ConsumerNick;
            txEmail.Text = selectedEmail.Receiver;
            txTime.Text = Convert.ToString(selectedEmail.VisitTime);
            txStatus.Text = selectedEmail.Status;
            txSubject.Text = selectedEmail.Subject;
            htxContent.HtmlText = selectedEmail.Content;

        }


    }
}