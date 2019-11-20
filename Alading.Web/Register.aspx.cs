using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alading.Web.Bussiness;

namespace Alading.Web
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {           
            UserRegisterControl1.OnSubmitButtonClick += new EventHandler(UserRegisterControl1_OnSubmitButtonClick);
            UserRegisterControl1.isMainAccount = true;            
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
            Alading.Web.Entity.User x = Alading.Web.Bussiness.UserService.GetUserByAccount(account);
            if (UserRegisterControl1.isMainAccount == true)
            {
                if (x != null)  //用户帐号重复
                {
                    string msg = "<script type='text/javascript'>alert('用户帐号重复')</script>";
                    Response.Write(msg);
                }
                else
                {                    
                    Alading.Web.Entity.User user = new Alading.Web.Entity.User();
                    user.UserName = UserRegisterControl1.UserName;
                    user.Account = UserRegisterControl1.Account;
                    user.Address = UserRegisterControl1.Address;
                    user.Company = UserRegisterControl1.Company;
                    user.Mobile = UserRegisterControl1.Mobile;
                    user.Password = HashProvider.GetHash(UserRegisterControl1.Password);
                    user.Tel = UserRegisterControl1.Tel;
                    user.UserCode = System.Guid.NewGuid().ToString();
                    user.MaxShop = 5;
                    user.MaxUser = 5;
                    user.FirstRun = true;
                    user.Approve = true;
                    Bussiness.UserService.AddUser(user);
                    Session["UserCode"] = user.UserCode;
                    Session["UserName"] = user.UserName;
                    Response.Redirect("~/UserInfo.aspx");
                }
            }            
        }
    }
}
