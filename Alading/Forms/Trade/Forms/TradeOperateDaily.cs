using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Entity;
using Alading.Business;

namespace Alading.Forms.Trade.Forms
{
    public partial class TradeOperateDaily : DevExpress.XtraEditors.XtraForm
    {
        public TradeOperateDaily(string customTid)
        {
            InitializeComponent();
            _customTid = customTid;
            txtCustomTid.Text = _customTid;
            txtTradeTitle.Text = TradeService.GetTrade(_customTid).title;
            txtOperaterName.Text = Alading.Utils.SystemHelper.User.nick;
        }

        private string _customTid = string.Empty;

        //检验输入内容的正确
        private bool  TestErrorInput()
        {
            if(txtTitle.Text==string.Empty)
            {
                dxErrorProvider1.SetError(txtTitle, "请填入标题内容");
            }
            if(txtOperaterName.Text==string.Empty)
            {
                dxErrorProvider1.SetError(txtOperaterName, "请填入操作人姓名");
            }
            if (txtContent.Text == string.Empty)
            {
                dxErrorProvider1.SetError(txtContent, "请填入操作内容");
            }
            if (txtDepartment.Text == string.Empty)
            {
                dxErrorProvider1.SetError(txtDepartment, "请填入操作环节");
            }
            if(dxErrorProvider1.HasErrors)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            if(TestErrorInput()==false)
            {
                return;
            }
            TradeInfo info = new TradeInfo();
            info.TradeInfoCode = Guid.NewGuid().ToString();
            info.Title = txtTitle.Text;
            info.AppendUserName = txtOperaterName.Text;
            info.Content = txtContent.Text;
            info.CustomTid = _customTid;
            info.AppendDepartment = txtDepartment.SelectedItem.ToString();
            info.AppendTime = DateTime.Now;
            info.AppendUserCode = "testLogin";//系统账号
            TradeInfoService.AddTradeInfo(info);
            this.Close();
        }

        private void canel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}