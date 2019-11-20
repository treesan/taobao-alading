using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IView_OrderItemStockProduct
    {
        List<View_OrderItemStockProduct> GetAllView_OrderItemStockProduct();

        List<View_OrderItemStockProduct> GetView_OrderItemStockProduct(Func<View_OrderItemStockProduct, bool> func);

        List<View_OrderItemStockProduct> GetView_OrderItemStockProduct(Func<View_OrderItemStockProduct, bool> func, int pageIndex, int pageSize, out int rowCount);
    }
}
