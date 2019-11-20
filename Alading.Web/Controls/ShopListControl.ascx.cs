using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AladingWeb.Controls
{
    public partial class ShopListControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public object DataSource
        {
            get { return RepShop.DataSource; }
            set { RepShop.DataSource = value; }
        }
    }
}