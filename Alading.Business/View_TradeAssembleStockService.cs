using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;
using System.Data;

namespace Alading.Business
{
    public static class View_TradeAssembleStockService
    {
        public static List<View_TradeAssembleStock> GetAllView_TradeAssembleStock()
        {
            return DataProviderClass.Instance().GetAllView_TradeAssembleStock();
        }

        public static List<View_TradeAssembleStock> GetView_TradeAssembleStock(Func<View_TradeAssembleStock, bool> func)
        {
            return DataProviderClass.Instance().GetView_TradeAssembleStock(func);
        }

        public static View_TradeAssembleStock GetView_TradeAssembleStock(string view_tradeassemblestockCode)
        {
            return DataProviderClass.Instance().GetView_TradeAssembleStock(view_tradeassemblestockCode);
        }

        public static DataSet GetViewTradeAssembleDataSet(string tradeOrderCode)
         {
             return DataProviderClass.Instance().GetViewTradeAssembleDataSet(tradeOrderCode);
         }
    }
}
