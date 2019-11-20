using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alading.Web.Bussiness;
using System.Web.Security;

namespace Alading.Web
{
    public partial class NewSite : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(top_account.Text)) && (!String.IsNullOrEmpty(top_password.Text)))
            {
                string account = top_account.Text;
                string password = HashProvider.GetHash(top_password.Text);

                //只有主号可以登录
                var user = UserService.GetUser(c => c.Account == account && (!c.Account.Contains(":")) && c.Password == password).FirstOrDefault();

                if (user != null)
                {
                    Session["UserCode"] = user.UserCode;
                    Session["UserName"] = user.UserName;

                    Response.Redirect("UserInfo.aspx");
                }
                else
                {
                    Session["UserName"] = null;
                    Session["UserCode"] = null;
                }
            }
        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            Session["UserName"] = null;
            Session["UserCode"] = null;

            Response.Redirect("Default.aspx");
        }
    }
}
