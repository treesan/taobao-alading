using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alading.Web.Bussiness;

namespace Alading.Web
{
    public partial class EditUser : System.Web.UI.Page
    {
        private string userCode = string.Empty;
        private Alading.Web.Entity.User user = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            EditUserControl1.OnSubmitButtonClick += new EventHandler(EditUserControl1_OnSubmitButtonClick);

            if (Request.QueryString["UserCode"]!=null)
            { 
                userCode = Request.QueryString["UserCode"];
                user = Alading.Web.Bussiness.UserService.GetUser(userCode);
                if (!IsPostBack && user != null)
                {
                    EditUserControl1.Address = user.Address;
                    EditUserControl1.UserName = user.UserName;
                    //EditUserControl1.Password = user.Password;
                    EditUserControl1.Tel = user.Tel;
                    EditUserControl1.Mobile = user.Mobile;
                    EditUserControl1.Company = user.Company;
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        void EditUserControl1_OnSubmitButtonClick(object sender, EventArgs e)
        {
            if (userCode != string.Empty && user != null)
            {
                user.Address = EditUserControl1.Address;
                user.UserName = EditUserControl1.UserName;                
                user.Tel = EditUserControl1.Tel;
                user.Mobile = EditUserControl1.Mobile;
                user.Company = EditUserControl1.Company;
                
                Alading.Web.Bussiness.UserService.UpdateUser(user);
                Response.Redirect("~/UserInfo.aspx");
            }
        }
    }
}
