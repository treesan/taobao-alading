using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface ITradeOrder
    {       
        ReturnType AddTradeOrder(TradeOrder tradeorder);
       
        ReturnType AddTradeOrder(List<TradeOrder> tradeorderList);
        
        ReturnType RemoveAllTradeOrder();
       
        ReturnType RemoveTradeOrder(Func<TradeOrder, bool> func);
              
        ReturnType RemoveTradeOrder(string tradeorderCode);
        
        ReturnType RemoveTradeOrder(List<string> tradeorderCodeList);
       
        ReturnType UpdateTradeOrder(TradeOrder tradeorder);

        ReturnType UpdateTradeOrderPicCode(string tradeOrderCode,string pictureCode);
       
        ReturnType UpdateTradeOrder(string tradeorderCode,TradeOrder tradeorder);
       
        List<TradeOrder> GetAllTradeOrder();
      
        List<TradeOrder> GetTradeOrder(Func<TradeOrder, bool> func);
      
        List<TradeOrder> GetTradeOrder(List<string> tradeorderCodeList);
       
        List<TradeOrder> GetTradeOrder(int pageIndex, int pageSize, out int rowCount);
        
        List<TradeOrder> GetTradeOrder(Func<TradeOrder, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveTradeOrder(int tradeorderID);
        
        ReturnType RemoveTradeOrder(List<int> tradeorderIDList);
        
        ReturnType UpdateTradeOrder(int tradeorderID,TradeOrder tradeorder);
        
        List<TradeOrder> GetTradeOrder(List<int> tradeorderIDList);
        */
    }
}
