using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Alading.Web.Controls
{
    public partial class UserRegisterControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
        }

        //void txUserAccount_TextChanged(object sender, EventArgs e)
        //{
        //    if (OnTextChanged != null)
        //    {
        //        OnTextChanged(sender, e);
        //    }
        //}

        void btnSubmit_Click(object sender, EventArgs e)
        {
            if (OnSubmitButtonClick != null)
            {
                OnSubmitButtonClick(sender, null);
            }
        }

        public string UserName
        {
            get { return txUserName.Text; }
            set { txUserName.Text = value; }
        }

        public string Password
        {
            get { return txPassword.Text; }
            set { txPassword.Text = value; }
        }

        public string Account
        {
            get { return txUserAccount.Text; }
            set { txUserAccount.Text = value; }
        }

        public string Tel
        {
            get { return txTel.Text; }
            set { txTel.Text = value; }
        }

        public string Mobile
        {
            get { return txMobile.Text; }
            set { txMobile.Text = value; }
        }

        public string Company
        {
            get { return txCompany.Text; }
            set { txCompany.Text = value; }
        }

        public string Address
        {
            get { return txAddress.Text; }
            set { txAddress.Text = value; }
        }

        public bool isMainAccount
        {
            get;
            set;
        }

        public string accountValidator
        {
            get { return RequiredFieldValidator2.ErrorMessage; }
            set { RequiredFieldValidator2.ErrorMessage = value; }
        }

        public event EventHandler OnSubmitButtonClick;
    }
}