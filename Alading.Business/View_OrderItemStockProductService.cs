using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class View_OrderItemStockProductService
    {
        public static List<View_OrderItemStockProduct> GetAllView_OrderItemStockProduct()
        {
            return DataProviderClass.Instance().GetAllView_OrderItemStockProduct();
        }

        public static List<View_OrderItemStockProduct> GetView_OrderItemStockProduct(Func<View_OrderItemStockProduct, bool> func)
        {
            return DataProviderClass.Instance().GetView_OrderItemStockProduct(func);
        }

        public static List<View_OrderItemStockProduct> GetView_OrderItemStockProduct(Func<View_OrderItemStockProduct, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetView_OrderItemStockProduct(func, pageIndex, pageSize, out rowCount);
        }
    }
}
