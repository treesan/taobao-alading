using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class View_TradeStockService
    {
        public static ReturnType AddTradeOrderSqlBulkCopy(System.Data.DataTable dataTable)
        {
            return DataProviderClass.Instance().AddTradeOrderSqlBulkCopy(dataTable);
        }

        public static List<View_TradeStock> GetAllView_TradeStock()
        {
            return DataProviderClass.Instance().GetAllView_TradeStock();
        }
        
         public static ReturnType  Update_TradeOrder(List<View_TradeStock> _parentOrderList, List<View_TradeStock> _childOrderList, Trade ParentTrade, Trade ChildTrade)
         {
             return DataProviderClass.Instance().Update_TradeOrder(_parentOrderList, _childOrderList, ParentTrade, ChildTrade);
         }

         public static ReturnType Update_TradeOrder(string customtid,Alading.Entity.Trade trade, List<Alading.Entity.TradeOrder> trade_order_list)
         {
             return DataProviderClass.Instance().Update_TradeOrder(customtid,trade, trade_order_list);
         }

        public  static ReturnType Update_TradeOrder(string combineCode)
        {
            return DataProviderClass.Instance().Update_TradeOrder(combineCode);
        }

        public static ReturnType Update_TradeOrder(Alading.Entity.Trade trade, List<TradeOrder> orderList, List<Trade> tradeList)
        {
            return DataProviderClass.Instance().Update_TradeOrder(trade, orderList,tradeList);
        }

        public static List<View_TradeStock> GetView_TradeStock(Func<View_TradeStock, bool> func)
        {
            return DataProviderClass.Instance().GetView_TradeStock(func);
        }
    
        public static View_TradeStock GetView_TradeStock(string view_tradestockCode)
        {
            return DataProviderClass.Instance().GetView_TradeStock(view_tradestockCode);
        }
        
        /*
        public static View_TradeStock GetView_TradeStock(int view_tradestockID)
        {
            return DataProviderClass.Instance().GetView_TradeStock(view_tradestockID);
        }
        */
    
        public static List<View_TradeStock> GetView_TradeStock(List<string> view_tradestockCodeList)
        {
            return DataProviderClass.Instance().GetView_TradeStock(view_tradestockCodeList);
        }
        
        /*
        public static List<View_TradeStock> GetView_TradeStock(List<int> view_tradestockIDList)
        {
            return DataProviderClass.Instance().GetView_TradeStock(view_tradestockIDList);
        }
        */
    
        public static List<View_TradeStock> GetView_TradeStock(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetView_TradeStock(pageIndex, pageSize, out rowCount);
        }
        
        public static List<View_TradeStock> GetView_TradeStock(Func<View_TradeStock, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetView_TradeStock(func, pageIndex, pageSize, out rowCount);
        }

        public static bool CheckStockProduct(string customTid)
        {
            return DataProviderClass.Instance().CheckStockProduct(customTid);
        }
    }
}
