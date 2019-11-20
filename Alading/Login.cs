using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Business;
using Alading.Utils;

using System.Linq;

namespace Alading
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        #region 属性
        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin
        {
            get;
            set;
        }

        public string Account
        {
            get { return txAccount.Text; }
            set { txAccount.Text = value; }
        }

        public string Password
        {
            get { return txPassword.Text; }
        }

        #endregion

        

        public Login()
        {
            InitializeComponent();
            //string s = "ompGddvmPzLptc1rViMaLc1tItn0XY8c";
            //SystemHelper.TripleDESEncrypt("admin");
            //MessageBox.Show(s);
            //MessageBox.Show(SystemHelper.TripleDESDecrypt(s));
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            ////验证用户名和密码正确
            //this.IsLogin = true;

            ////加载相应的角色和所管辖的店铺
            ////SystemHelper.ShopList = ShopService.GetAllShop();
            //LoginValidator(txAccount.Text, txPassword.Text);
            ////关闭当前窗口，启动主窗口
            //this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Application.Exit();
        }
        

        private void requiredField_EditValueChanged(object sender, EventArgs e)
        {
            btnLogin.Enabled = (!string.IsNullOrEmpty(txAccount.Text)) && (!string.IsNullOrEmpty(txPassword.Text));
        }

        private void txPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void hyperLinkEdit1_MouseClick(object sender, MouseEventArgs e)
        {
            HyperLinkEdit editor = sender as HyperLinkEdit;
            editor.ShowBrowser("http://www.aldsoft.cc/Register.aspx");
        }

        private void hyperLinkEdit2_MouseClick(object sender, MouseEventArgs e)
        {
            HyperLinkEdit editor = sender as HyperLinkEdit;
            editor.ShowBrowser("http://www.aldsoft.cc");
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            ConfigForm configForm = new ConfigForm();
            configForm.ShowDialog();
        }
    }
}