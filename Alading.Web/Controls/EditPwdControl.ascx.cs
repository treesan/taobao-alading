using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Alading.Web.Controls
{
    public partial class EditPwdControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnOK.Click += new EventHandler(btnOK_Click);
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            if (OnClick != null)
            {
                OnClick(sender, e);
            }
        }

        public string Password
        {
            get { return txPassword.Text; }
            set { txPassword.Text = value; }
        }

        public string NewPwd
        {
            get { return txNewPwd.Text; }
            set { txNewPwd.Text = value; }
        }

        public event EventHandler OnClick;
    }
}