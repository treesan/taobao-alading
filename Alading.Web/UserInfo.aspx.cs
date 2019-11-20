using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alading.Web.Entity;

namespace Alading.Web
{
    public partial class UserInfo : System.Web.UI.Page
    {
        private string account = string.Empty;
        public string userCode { get; set; }

        private List<Shop> shopList = new List<Shop>();
        private List<User> userList = new List<User>();

        public User mainUser = new User();

        public bool visibleAddShop { get; set; }
        public bool visibleAddUser { get; set; }
        public int userRemain { get; set; }
        public int shopRemain { get; set; }
        public string userName { get; set; }
        

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserCode"] != null && Session["UserName"] != null)
                {
                    userCode = Session["UserCode"].ToString();
                    InitData();
                    ShopListControl1.DataSource = shopList;
                    ShopListControl1.DataBind();
                    UserListControl1.DataSource = userList;
                    UserListControl1.DataBind();

                    if (mainUser.MaxShop > mainUser.HasShop)
                    {
                        visibleAddShop = true;
                    }
                    else
                    {
                        visibleAddShop = false;
                    }

                    if (mainUser.MaxUser > mainUser.HasUser)
                    {
                        visibleAddUser = true;
                    }
                    else
                    {
                        visibleAddUser = false;
                    }
                    DataBind();
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }   
            }
            catch (Exception)
            {
                Response.Redirect("~/Default.aspx");
            }                     
        }

        private void InitData()
        {
            if (!string.IsNullOrEmpty(userCode))
            {
                shopList = Alading.Web.Bussiness.ShopService.GetShopList(userCode);                
                mainUser = Alading.Web.Bussiness.UserService.GetUser(userCode);
                userList = Alading.Web.Bussiness.UserService.GetUsers(mainUser.Account);
                
                shopRemain = mainUser.MaxShop - mainUser.HasShop;
                userRemain = mainUser.MaxUser - mainUser.HasUser;
                userName = mainUser.UserName;
            }
        }
    }
}
