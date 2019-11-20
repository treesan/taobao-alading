using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface ITradeRefund
    {       
        ReturnType AddTradeRefund(TradeRefund traderefund);
       
        ReturnType AddTradeRefund(List<TradeRefund> traderefundList);
        
        ReturnType RemoveAllTradeRefund();
       
        ReturnType RemoveTradeRefund(Func<TradeRefund, bool> func);
              
        ReturnType RemoveTradeRefund(string traderefundCode);
        
        ReturnType RemoveTradeRefund(List<string> traderefundCodeList);
       
        ReturnType UpdateTradeRefund(TradeRefund traderefund);
       
        ReturnType UpdateTradeRefund(string traderefundCode,TradeRefund traderefund);
       
        List<TradeRefund> GetAllTradeRefund();
      
        List<TradeRefund> GetTradeRefund(Func<TradeRefund, bool> func);
      
        List<TradeRefund> GetTradeRefund(List<string> traderefundCodeList);
       
        List<TradeRefund> GetTradeRefund(int pageIndex, int pageSize, out int rowCount);
        
        List<TradeRefund> GetTradeRefund(Func<TradeRefund, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveTradeRefund(int traderefundID);
        
        ReturnType RemoveTradeRefund(List<int> traderefundIDList);
        
        ReturnType UpdateTradeRefund(int traderefundID,TradeRefund traderefund);
        
        List<TradeRefund> GetTradeRefund(List<int> traderefundIDList);
        */
    }
}
