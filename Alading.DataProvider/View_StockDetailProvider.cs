using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Interface;
using Alading.Core;

namespace Alading.DataProvider
{
    public partial class DataProviderClass : IAlading
    {
        public List<View_StockDetail> GetAllView_StockDetail()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    return alading.View_StockDetail.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public List<View_StockDetail> GetView_StockDetail(Func<View_StockDetail, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var result = alading.View_StockDetail.Where(func);
                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
    }
}
