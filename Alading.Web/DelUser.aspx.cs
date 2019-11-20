using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alading.Web.Entity;

namespace AladingWeb
{
    public partial class Del : System.Web.UI.Page
    {
        private string userCode = string.Empty;
        private User mainUser = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["UserCode"]!=null)
            {
                userCode = Request.QueryString["UserCode"];
                Alading.Web.Bussiness.UserService.RemoveUser(userCode);
                Response.Redirect("~/UserInfo.aspx");
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
    }
}
