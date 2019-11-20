using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alading.Web.Bussiness;

namespace Alading.Web
{
    public partial class NewsList : System.Web.UI.Page
    {
        public int NewsCatID { get; set; }
        public string CatName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string q = Request.QueryString["cid"];
            int cid = 1;
            try
            {
                if (q == null) cid = 1;
                else cid = Convert.ToInt32(q);
            }
            catch (Exception)
            {
                cid = 1;
            }

            NewsCatID = cid;

            var cats = NewsService.GetNewsCat();
            CatRepeater.DataSource = cats;

            var cat = NewsService.GetNewsCat(cid);
            CatName = cat.Name;

            var news = NewsService.GetNewsList(cid);
            NewsRepeater.DataSource = news;

            DataBind();
        }
    }
}
