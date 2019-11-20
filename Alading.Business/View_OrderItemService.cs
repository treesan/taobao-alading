using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.DataProvider;

namespace Alading.Business
{
    public static class View_OrderItemService
    {
        public static List<View_OrderItem> GetView_OrderItem(Func<View_OrderItem, bool> func)
        {
            return DataProviderClass.Instance().GetView_OrderItem(func);
        }
    }
}
