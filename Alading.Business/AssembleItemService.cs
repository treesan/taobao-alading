using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.DataProvider;

namespace Alading.Business
{
    public static class AssembleItemService
    {
        public static List<AssembleItem> GetAssembleItem(Func<AssembleItem, bool> func)
        {
            return DataProviderClass.Instance().GetAssembleItem(func);
        }

        public static List<View_AssembleProduct> GetView_AssembleProduct(Func<View_AssembleProduct, bool> func)
        {
            return DataProviderClass.Instance().GetView_AssembleProduct(func);
        }
    }
}
