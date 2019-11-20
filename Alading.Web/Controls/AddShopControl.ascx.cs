using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alading.Web.Entity;

namespace Alading.Web.Controls
{
    public partial class AddShopControl : System.Web.UI.UserControl
    {
        private List<Area> provinceList = new List<Area>();
        private List<Area> cityList = new List<Area>();
        private List<Area> areaList = new List<Area>();

        protected void Page_Load(object sender, EventArgs e)
        {
            btnAddShop.Click += new EventHandler(btnAddShop_Click);

            if (!IsPostBack)
            {
                DataBindProvince();
            }
        }

        void btnAddShop_Click(object sender, EventArgs e)
        {
            if (AddButtonClick != null)
            {
                AddButtonClick(sender, e);
            }
        }        

        public string ShopNick
        {
            get { return txShopNick.Text; }
            set { txShopNick.Text = value; }
        }

        public int ShopType
        {
            get { return Convert.ToInt32(ddlShopType.SelectedValue); }
            set
            {
                int i = 0;
                foreach (ListItem l in ddlShopType.Items)
                {
                    if (l.Value == value.ToString())
                    {
                        ddlShopType.SelectedIndex = i;
                        break;
                    }
                    i++;
                }
            }
        }

        public string ShopTypeName
        {
            get { return ddlShopType.SelectedItem.Text; }
            set 
            {
                int i = 0;
                foreach (ListItem l in ddlShopType.Items)
                {
                    if (l.Text == value)
                    {
                        ddlShopType.SelectedIndex = i;
                        break;
                    }
                    i++;
                }
            }
        }

        public string Address
        {
            get { return txAddress.Text; }
            set { txAddress.Text = value; }
        }

        public string Tel
        {
            get { return txTel.Text; }
            set { txTel.Text = value; }
        }

        public string Province
        {
            get 
            {
                if (ddlProvince.SelectedItem != null)
                {
                    return ddlProvince.SelectedItem.Text;
                }
                else
                {
                    return string.Empty;
                }
            }
            set 
            {
                DataBindProvince();

                int index = 0;
                foreach (ListItem li in ddlProvince.Items)
                {
                    if (li.Text == value)
                    {
                        ddlProvince.SelectedIndex = index;
                        break;
                    }
                    index++;
                }
            }
        }

        public string City
        {
            get
            {
                if (ddlCity.SelectedItem != null)
                {
                    return ddlCity.SelectedItem.Text;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                DataBindCity(ddlProvince.SelectedValue);
                
                int index = 0;
                foreach (ListItem li in ddlCity.Items)
                {
                    if (li.Text == value)
                    {
                        ddlCity.SelectedIndex = index;
                        break;
                    }
                    index++;
                }
            }
        }

        public string Area
        {
            get 
            {
                if (ddlArea.SelectedItem != null)
                {
                    return ddlArea.SelectedItem.Text;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                DataBindArea(ddlCity.SelectedValue);
                int index = 0;
                foreach (ListItem li in ddlArea.Items)
                {
                    if (li.Text == value)
                    {
                        ddlArea.SelectedIndex = index;
                        break;
                    }
                    index++;
                }
            }
 
        }

        public event EventHandler AddButtonClick;

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataBindCity(ddlProvince.SelectedValue);
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataBindArea(ddlCity.SelectedValue);
        }

        private void DataBindProvince()
        {
            provinceList = Alading.Web.Bussiness.AreaService.GetArea(p => p.parent_id == "1");
            ddlProvince.DataSource = provinceList;
            ddlProvince.DataTextField = "name";
            ddlProvince.DataValueField = "id";

            if (provinceList != null && provinceList.Count > 0)
            {
                var p0 = provinceList[0];
                cityList = Alading.Web.Bussiness.AreaService.GetArea(c => c.parent_id == p0.id);
                ddlCity.DataSource = cityList;
                ddlCity.DataTextField = "name";
                ddlCity.DataValueField = "id";
                ddlCity.DataBind();

                if (cityList != null && cityList.Count > 0)
                {
                    var c0 = cityList[0];
                    areaList = Alading.Web.Bussiness.AreaService.GetArea(a => a.parent_id == c0.id);
                    ddlArea.DataSource = areaList;
                    ddlArea.DataTextField = "name";
                    ddlArea.DataValueField = "id";
                    ddlArea.DataBind();
                }
            }

            ddlProvince.DataBind();
        }

        private void DataBindCity(string x)
        {
            cityList = Alading.Web.Bussiness.AreaService.GetArea(p => p.parent_id == x);

            ddlCity.DataSource = cityList;
            ddlCity.DataTextField = "name";
            ddlCity.DataValueField = "id";

            if (cityList != null && cityList.Count > 0)
            {
                var c0 = cityList[0];
                areaList = Alading.Web.Bussiness.AreaService.GetArea(a => a.parent_id == c0.id);
                ddlArea.DataSource = areaList;
                ddlArea.DataTextField = "name";
                ddlArea.DataValueField = "id";
                ddlArea.DataBind();
            }

            ddlCity.DataBind();
        }

        private void DataBindArea(string x)
        {
            areaList = Alading.Web.Bussiness.AreaService.GetArea(p => p.parent_id == x);

            ddlArea.DataSource = areaList;
            ddlArea.DataTextField = "name";
            ddlArea.DataValueField = "id";
            ddlArea.DataBind();
        }
    }
}
