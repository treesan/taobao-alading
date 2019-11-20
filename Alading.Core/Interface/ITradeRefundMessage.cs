using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface ITradeRefundMessage
    {       
        ReturnType AddTradeRefundMessage(TradeRefundMessage traderefundmessage);
       
        ReturnType AddTradeRefundMessage(List<TradeRefundMessage> traderefundmessageList);
        
        ReturnType RemoveAllTradeRefundMessage();
       
        ReturnType RemoveTradeRefundMessage(Func<TradeRefundMessage, bool> func);
              
        ReturnType RemoveTradeRefundMessage(int refundid);
        
        ReturnType RemoveTradeRefundMessage(List<int> refundidList);
       
        ReturnType UpdateTradeRefundMessage(TradeRefundMessage traderefundmessage);
       
        ReturnType UpdateTradeRefundMessage(int refundid,TradeRefundMessage traderefundmessage);
       
        List<TradeRefundMessage> GetAllTradeRefundMessage();
      
        List<TradeRefundMessage> GetTradeRefundMessage(Func<TradeRefundMessage, bool> func);
      
        List<TradeRefundMessage> GetTradeRefundMessage(List<int> refundidList);
       
        List<TradeRefundMessage> GetTradeRefundMessage(int pageIndex, int pageSize, out int rowCount);
        
        List<TradeRefundMessage> GetTradeRefundMessage(Func<TradeRefundMessage, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveTradeRefundMessage(int traderefundmessageID);
        
        ReturnType RemoveTradeRefundMessage(List<int> traderefundmessageIDList);
        
        ReturnType UpdateTradeRefundMessage(int traderefundmessageID,TradeRefundMessage traderefundmessage);
        
        List<TradeRefundMessage> GetTradeRefundMessage(List<int> traderefundmessageIDList);
        */
    }
}
