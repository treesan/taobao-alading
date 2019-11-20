using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alading.Web.Bussiness;

namespace Alading.Web
{
    public partial class EditPwd : System.Web.UI.Page
    {
        string userCode = string.Empty;
        private Alading.Web.Entity.User user = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            EditPwdControl1.OnClick += new EventHandler(EditPwdControl1_OnClick);

            if (Request.QueryString["UserCode"]!=null)
            {
                userCode = Request.QueryString["UserCode"];
                user = Alading.Web.Bussiness.UserService.GetUser(userCode);
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        void EditPwdControl1_OnClick(object sender, EventArgs e)
        {
            if (user != null && HashProvider.GetHash(EditPwdControl1.Password) == user.Password)
            {
                user.Password = HashProvider.GetHash(EditPwdControl1.NewPwd);
                UserService.UpdateUser(user);
                Response.Redirect("~/UserInfo.aspx");
            }
            else
            {
                string msg = "<script type='text/javascript'>alert('原密码错误')</script>";
                Response.Write(msg);
            }
        }
    }
}
