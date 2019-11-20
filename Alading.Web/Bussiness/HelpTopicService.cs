using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alading.Web.Entity;

namespace Alading.Web.Bussiness
{
    public class HelpTopicService
    {
        public static List<HelpCat> GetHelpCat()
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                return context.HelpCat.ToList();
            }
        }

        public static HelpCat GetHelpCat(int id)
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                return context.HelpCat.FirstOrDefault(c => c.Id == id);
            }
        }

        public static List<Alading.Web.Entity.HelpTopic> GetHelpTopicList(int cid)
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                return context.HelpTopic.Where(c => c.Cat == cid).ToList();
            }
        }

        public static Alading.Web.Entity.HelpTopic GetHelpTopic(int id)
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                return context.HelpTopic.FirstOrDefault(c => c.Id == id);
            }
        }
    }
}
