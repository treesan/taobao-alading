using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class View_TradeShopService
    {
        public static List<View_TradeShop> GetAllView_TradeShop()
        {
            return DataProviderClass.Instance().GetAllView_TradeShop();
        }

        public static List<View_TradeShop> GetView_TradeShop(Func<View_TradeShop, bool> func)
        {
            return DataProviderClass.Instance().GetView_TradeShop(func);
        }

        public static View_TradeShop GetView_TradeShop(string view_tradeshopCode)
        {
            return DataProviderClass.Instance().GetView_TradeShop(view_tradeshopCode);
        }

        /*
        public static View_TradeShop GetView_TradeShop(int view_tradeshopID)
        {
            return DataProviderClass.Instance().GetView_TradeShop(view_tradeshopID);
        }
        */

        public static List<View_TradeShop> GetView_TradeShop(List<string> view_tradeshopCodeList)
        {
            return DataProviderClass.Instance().GetView_TradeShop(view_tradeshopCodeList);
        }

        /*
        public static List<View_TradeShop> GetView_TradeShop(List<int> view_tradeshopIDList)
        {
            return DataProviderClass.Instance().GetView_TradeShop(view_tradeshopIDList);
        }
        */

        public static List<View_TradeShop> GetView_TradeShop(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetView_TradeShop(pageIndex, pageSize, out rowCount);
        }

        public static List<View_TradeShop> GetView_TradeShop(Func<View_TradeShop, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetView_TradeShop(func, pageIndex, pageSize, out rowCount);
        }
    }
}
