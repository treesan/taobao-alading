using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AladingWeb
{
    public partial class DelShop : System.Web.UI.Page
    {
        private string shopCode = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ShopCode"]!=null)
            {
                shopCode = Request.QueryString["ShopCode"];
                Alading.Web.Bussiness.ShopService.RemoveShop(shopCode);
                Response.Redirect("~/UserInfo.aspx");
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
    }
}
