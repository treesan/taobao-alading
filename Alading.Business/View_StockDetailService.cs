using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Entity;

namespace Alading.Business
{
    public static class View_StockDetailService
    {
        public static List<View_StockDetail> GetAllView_StockDetail()
        {
            return DataProviderClass.Instance().GetAllView_StockDetail();
        }

        public static List<View_StockDetail> GetView_StockDetail(Func<View_StockDetail, bool> func)
        {
            return DataProviderClass.Instance().GetView_StockDetail(func);
        }         
    }
}
