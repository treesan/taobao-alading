using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alading.Web.Bussiness;

namespace Alading.Web
{
    public partial class HelpList : System.Web.UI.Page
    {
        public string CatName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            int cid = 1;
            string q = Request.QueryString["cid"];            
            try
            {
                if (q == null) cid = 1;
                else cid = Convert.ToInt32(q);
            }
            catch (Exception)
            {
                cid = 1;
            }

            var cats = HelpTopicService.GetHelpCat();
            CatRepeater.DataSource = cats;

            var cat = HelpTopicService.GetHelpCat(cid);

            if (cat != null)
            {
                CatName = cat.Name;

                var topics = HelpTopicService.GetHelpTopicList(cid);
                TopicRepeater.DataSource = topics;
            }           

            DataBind();
        }
    }
}
