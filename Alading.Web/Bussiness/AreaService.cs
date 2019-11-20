using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alading.Web.Entity;

namespace Alading.Web.Bussiness
{
    public class AreaService
    {
        public static List<Area> GetArea(Func<Area, bool> func)
        {
            try
            {
                using (AladingWebEntities alading = ContextProvider.DataContext(ConnectionHelper.ConnectionString) )
                {
                    List<Area> list = alading.Area.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
