using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alading.Web.Bussiness;

namespace Alading.Web
{
    public partial class NewsDetail : System.Web.UI.Page
    {
        public int NewsCatID { get; set; }
        public string CatName { get; set; }
        public string NewsTitle { get; set; }
        public string NewsContent { get; set; }
        public string NewsTime { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string q = Request.QueryString["id"];
                int id = Convert.ToInt32(q);
                if (id == 0) throw new Exception();

                var news = NewsService.GetNews(id);
                if (news == null) throw new Exception();

                var cats = NewsService.GetNewsCat();
                CatRepeater.DataSource = cats;

                var cat = cats.FirstOrDefault(c => c.Id == news.Cat);
                if (cat == null) throw new Exception();

                NewsTitle = news.Title;
                NewsContent = news.Content;
                NewsCatID = news.Cat;
                NewsTime = news.PostTime.ToString("yyyy/MM/dd HH:mm");

                DataBind();
            }
            catch (Exception)
            {
                Response.Redirect("NewsList.aspx");
            }
        }
    }
}
