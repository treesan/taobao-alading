using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alading.Web.Bussiness;

namespace AladingWeb
{
    public partial class EditShop : System.Web.UI.Page
    {
        private string shopCode = string.Empty;
        private Alading.Web.Entity.Shop shop = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            AddShopControl1.AddButtonClick += new EventHandler(AddShopControl1_AddButtonClick);

            if (Request.QueryString["ShopCode"]!=null)
            {
                shopCode = Request.QueryString["ShopCode"];
                shop = Alading.Web.Bussiness.ShopService.GetShop(shopCode);
                if (!IsPostBack && shop != null)
                {
                    AddShopControl1.ShopNick = shop.ShopNick;
                    AddShopControl1.Address = shop.Address;
                    AddShopControl1.Province = shop.Province;
                    AddShopControl1.City = shop.City;
                    AddShopControl1.Area = shop.Area;
                    AddShopControl1.Tel = shop.Tel;
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        void AddShopControl1_AddButtonClick(object sender, EventArgs e)
        {
            if (shopCode != string.Empty && shop != null)
            {
                shop.ShopNick = AddShopControl1.ShopNick;
                shop.Sign = HashProvider.GetHash(shop.UserCode, shop.ShopNick);
                shop.ShopType = AddShopControl1.ShopType;
                shop.ShopTypeName = AddShopControl1.ShopTypeName;
                shop.Province = AddShopControl1.Province;
                shop.City = AddShopControl1.City;
                shop.Area = AddShopControl1.Area;
                shop.Address = AddShopControl1.Address;
                shop.Tel = AddShopControl1.Tel;
                Alading.Web.Bussiness.ShopService.UpdateShop(shop);

                Response.Redirect("~/UserInfo.aspx");

            }
        }
    }
}
