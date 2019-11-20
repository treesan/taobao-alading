using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alading.Web.Bussiness;

namespace Alading.Web
{
    public partial class HelpTopic : System.Web.UI.Page
    {
        public string CatName { get; set; }
        public string TopicTitle { get; set; }
        public string TopicContent { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string q = Request.QueryString["id"];
                int id = Convert.ToInt32(q);
                if (id == 0) throw new Exception();

                var topic = HelpTopicService.GetHelpTopic(id);
                if (topic == null) throw new Exception();                

                var cats = HelpTopicService.GetHelpCat();
                CatRepeater.DataSource = cats;

                var cat = cats.FirstOrDefault(c => c.Id == topic.Cat);
                if (cat == null) throw new Exception();

                TopicTitle = topic.Title;
                TopicContent = topic.Content;

                DataBind();
            }
            catch (Exception)
            {
                Response.Redirect("HelpList.aspx");
            }
        }
    }
}
