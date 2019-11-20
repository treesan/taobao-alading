using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;
using System.Data;

namespace Alading.Core.Interface
{
    public interface IView_TradeStock
    {
        ReturnType AddTradeOrderSqlBulkCopy(DataTable dataTable);

        DataSet GetView_TradeStockDataSet(string localStatus, string status, DateTime startTime, DateTime endTime);

        DataSet GetView_TradeStockNormalDataSet(string localStatus, string status, DateTime startTime, DateTime endTime);

        DataSet GetView_TradeDetailItemsDataSet(string customTid);
        List<View_TradeStock> GetAllView_TradeStock();

        ReturnType Update_TradeOrder(List<View_TradeStock> _parentOrderList, List<View_TradeStock> _childOrderList, Trade ParentTrade, Trade ChildTrade);

        ReturnType Update_TradeOrder(string customtid,Alading.Entity.Trade trade, List<Alading.Entity.TradeOrder> trade_order_list);

        ReturnType Update_TradeOrder( string combineCode);

        ReturnType Update_TradeOrder(Alading.Entity.Trade trade, List<TradeOrder> orderList,List<Trade>tradeList);

        List<View_TradeStock> GetView_TradeStock(Func<View_TradeStock, bool> func);
      
        List<View_TradeStock> GetView_TradeStock(List<string> view_tradestockCodeList);
       
        List<View_TradeStock> GetView_TradeStock(int pageIndex, int pageSize, out int rowCount);
        
        List<View_TradeStock> GetView_TradeStock(Func<View_TradeStock, bool> func, int pageIndex, int pageSize, out int rowCount);

        bool CheckStockProduct(string customTid);
        
        /*        
        ReturnType RemoveView_TradeStock(int view_tradestockID);
        
        ReturnType RemoveView_TradeStock(List<int> view_tradestockIDList);
        
        ReturnType UpdateView_TradeStock(int view_tradestockID,View_TradeStock view_tradestock);
        
        List<View_TradeStock> GetView_TradeStock(List<int> view_tradestockIDList);
        */
    }
}
