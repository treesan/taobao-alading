using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface ITrade
    {       
        ReturnType AddTrade(Trade trade);
       
        ReturnType AddTrade(List<Trade> tradeList);

        ReturnType AddTradeOrderBuyer(Alading.Taobao.Entity.Trade trade);
        
        ReturnType RemoveAllTrade();
       
        ReturnType RemoveTrade(Func<Trade, bool> func);
              
        ReturnType RemoveTrade(string tradeCode);
        
        ReturnType RemoveTrade(List<string> tradeCodeList);
       
        ReturnType UpdateTrade(Trade trade);
       
        ReturnType UpdateTrade(string tradeCode,Trade trade);

        ReturnType UpdateTrade(List<Trade> tradeList);

        ReturnType UpdateTrade(List<string> CombineTradeList, List<string> TradeCustomList);

        ReturnType UpdateTradeLocalStatus(string CustomTid, string LocalStatus);

        ReturnType UpdateTradeLocalStatus(List<string> customTidList, string LocalStatus);

        ReturnType UpdateTradeLocalStatus(string userCode, string userName, List<string> customTidList, string LocalStatus, List<TradeOrder> OrderList);
       
        List<Trade> GetAllTrade();
      
        List<Trade> GetTrade(Func<Trade, bool> func);
      
        List<Trade> GetTrade(List<string> tradeCodeList);
       
        List<Trade> GetTrade(int pageIndex, int pageSize, out int rowCount);
        
        List<Trade> GetTrade(Func<Trade, bool> func, int pageIndex, int pageSize, out int rowCount);

        List<Trade> GetTradesByStatus(string localStatus, string status);
        
         //通过传CustomTid的方法在存储过程中实现交易的取消
        int RebackCommonTrade(string customTid, string oldStatus, string newStatus);

        int SummitCommonTrade(string customTid, string oldStatus, string newStatus,string userNick);

        int SummitCombineTrade(string customTidList, string oldStatus, string combineCode, string specialStatus, string lockUser);

        int SummitShippingCode(string customTid, string oldStatus, string newStatus, string shippingCode);
        /*        
        ReturnType RemoveTrade(int tradeID);
        
        ReturnType RemoveTrade(List<int> tradeIDList);
        
        ReturnType UpdateTrade(int tradeID,Trade trade);
        
        List<Trade> GetTrade(List<int> tradeIDList);
        */
    }
}
