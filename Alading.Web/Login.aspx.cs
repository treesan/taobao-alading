using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alading.Web.Entity;
using Alading.Web.Bussiness;

namespace AladingWeb
{
    public partial class Login : System.Web.UI.Page
    {
        private string account = string.Empty;
        private User mainUser = null;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txUserAccount.Text))
            {
                account = txUserAccount.Text;
                mainUser = Alading.Web.Bussiness.UserService.GetUserByAccount(account);
                if (!string.IsNullOrEmpty(txPassword.Text)  && mainUser!=null)
                {
                    if (mainUser.Password == HashProvider.GetHash(txPassword.Text))
                    {
                        Session.Add("UserCode", mainUser.UserCode);
                        Response.Redirect("~/UserInfo.aspx");
                    }
                }
            }
        }
    }
}
