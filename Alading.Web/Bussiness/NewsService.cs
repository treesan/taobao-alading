using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alading.Web.Entity;

namespace Alading.Web.Bussiness
{
    public class NewsService
    {
        public static List<NewsCat> GetNewsCat()
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                return context.NewsCat.ToList();
            }
        }

        public static NewsCat GetNewsCat(int id)
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                return context.NewsCat.FirstOrDefault(c => c.Id == id);
            }
        }

        public static List<Alading.Web.Entity.News> GetNewsList(int cid)
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                return context.News.Where(c => c.Cat == cid).ToList();
            }
        }

        public static Alading.Web.Entity.News GetNews(int id)
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                return context.News.FirstOrDefault(c => c.Id == id);
            }
        }
    }
}
