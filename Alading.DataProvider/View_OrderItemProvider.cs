using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core;

namespace Alading.DataProvider
{
    public partial class DataProviderClass  
    {
        public List<View_OrderItem> GetView_OrderItem(Func<View_OrderItem, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_OrderItem> list = alading.View_OrderItem.Where(func).ToList();
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
