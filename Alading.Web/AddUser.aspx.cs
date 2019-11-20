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
    public partial class AddUser : System.Web.UI.Page
    {
        private string userCode = string.Empty;
        private User mainUser = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            UserRegisterControl1.OnSubmitButtonClick += new EventHandler(UserRegisterControl1_OnSubmitButtonClick);
            if (Session["UserCode"] != null)
            {
                userCode = Session["UserCode"].ToString();
                mainUser = Alading.Web.Bussiness.UserService.GetUser(userCode);
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        void UserRegisterControl1_OnSubmitButtonClick(object sender, EventArgs e)
        {
            string account = UserRegisterControl1.Account;
            if (account.Contains(":"))
            {
                string msg = "<script type='text/javascript'>alert('用户帐号不能包含(:)冒号')</script>";
                Response.Write(msg);
                return;
            }

            Alading.Web.Entity.User x = Alading.Web.Bussiness.UserService.GetUserByAccount(mainUser.Account + ":" + account);
            if (x != null)
            {
                string msg = "<script type='text/javascript'>alert('用户帐号重复')</script>";
                Response.Write(msg);
            }
            else
            {

                if (mainUser != null && mainUser.MaxUser > mainUser.HasUser)
                {
                    Alading.Web.Entity.User user = new Alading.Web.Entity.User();
                    user.UserName = UserRegisterControl1.UserName;
                    user.Account = mainUser.Account + ":" + UserRegisterControl1.Account;
                    user.Address = UserRegisterControl1.Address;
                    user.Company = UserRegisterControl1.Company;
                    user.Mobile = UserRegisterControl1.Mobile;
                    user.Password = HashProvider.GetHash(UserRegisterControl1.Password);
                    user.Tel = UserRegisterControl1.Tel;
                    user.UserCode = System.Guid.NewGuid().ToString();
                    user.MaxShop = 0;
                    user.MaxUser = 0;
                    user.FirstRun = true;
                    user.Approve = true;
                    Alading.Web.Bussiness.UserService.AddUser(user);

                    Response.Redirect("~/UserInfo.aspx");
                }
            }
        }
    }
}
