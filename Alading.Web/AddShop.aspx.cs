using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alading.Web.Entity;
using Alading.Web.Bussiness;

namespace Alading.Web
{
    public partial class AddShop : System.Web.UI.Page
    {
        private string userCode = string.Empty;

        private User mainUser = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            AddShopControl1.AddButtonClick += new EventHandler(AddShopControl1_AddButtonClick);

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

        void AddShopControl1_AddButtonClick(object sender, EventArgs e)
        {
            SaveShop();
            Response.Redirect("~/UserInfo.aspx");
        }
        

        private void SaveShop()
        {
            if (mainUser.MaxShop > mainUser.HasShop)
            {
                Alading.Web.Entity.Shop shop = new Alading.Web.Entity.Shop();
                shop.ShopCode = System.Guid.NewGuid().ToString();
                shop.ShopNick = AddShopControl1.ShopNick;
                shop.UserCode = userCode;
                shop.Sign = HashProvider.GetHash(shop.UserCode, shop.ShopNick);
                shop.ShopType = AddShopControl1.ShopType;
                shop.ShopTypeName = AddShopControl1.ShopTypeName;

                shop.Province = AddShopControl1.Province;
                shop.City = AddShopControl1.City;
                shop.Area = AddShopControl1.Area;
                shop.Address = AddShopControl1.Address;
                shop.Tel = AddShopControl1.Tel;

                Alading.Web.Bussiness.ShopService.AddShop(shop);
            }
        }
    }
}
