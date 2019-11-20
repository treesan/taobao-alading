using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class TradeRefundMessageService
    {

        public static ReturnType AddTradeRefundMessage(TradeRefundMessage traderefundmessage)
        {
            return DataProviderClass.Instance().AddTradeRefundMessage(traderefundmessage);
        }

        public static ReturnType AddTradeRefundMessage(List<TradeRefundMessage> traderefundmessageList)
        {
            return DataProviderClass.Instance().AddTradeRefundMessage(traderefundmessageList);
        }
    
        public static ReturnType RemoveAllTradeRefundMessage()
        {
            return DataProviderClass.Instance().RemoveAllTradeRefundMessage();
        }
    
        public static ReturnType RemoveTradeRefundMessage(Func<TradeRefundMessage, bool> func)
        {
            return DataProviderClass.Instance().RemoveTradeRefundMessage(func);
        }

        public static ReturnType RemoveTradeRefundMessage(int traderefundmessageCode)
        {
            return DataProviderClass.Instance().RemoveTradeRefundMessage(traderefundmessageCode);
        }       
        
        /*
        public static ReturnType RemoveTradeRefundMessage(int traderefundmessageID)
        {
            return DataProviderClass.Instance().RemoveTradeRefundMessage(traderefundmessageID);
        }
        */
    
        public static ReturnType RemoveTradeRefundMessage(List<int> traderefundmessageCodeList)
        {
            return DataProviderClass.Instance().RemoveTradeRefundMessage(traderefundmessageCodeList);
        }        
        
        /*
        public static ReturnType RemoveTradeRefundMessage(List<int> traderefundmessageIDList)
        {
            return DataProviderClass.Instance().RemoveTradeRefundMessage(traderefundmessageIDList);
        }
        */
    
        public static ReturnType UpdateTradeRefundMessage(TradeRefundMessage traderefundmessage)
        {
            return DataProviderClass.Instance().UpdateTradeRefundMessage(traderefundmessage);
        }

        public static ReturnType UpdateTradeRefundMessage(int traderefundmessageCode, TradeRefundMessage traderefundmessage)
        {
            return DataProviderClass.Instance().UpdateTradeRefundMessage(traderefundmessageCode, traderefundmessage);
        }
        
        /*
        public static ReturnType UpdateTradeRefundMessage(int traderefundmessageID, TradeRefundMessage traderefundmessage)
        {
            return DataProviderClass.Instance().UpdateTradeRefundMessage(traderefundmessageID, traderefundmessage);
        }
        */
    
        public static List<TradeRefundMessage> GetAllTradeRefundMessage()
        {
            return DataProviderClass.Instance().GetAllTradeRefundMessage();
        }
    
        public static List<TradeRefundMessage> GetTradeRefundMessage(Func<TradeRefundMessage, bool> func)
        {
            return DataProviderClass.Instance().GetTradeRefundMessage(func);
        }

        public static TradeRefundMessage GetTradeRefundMessage(int traderefundmessageCode)
        {
            return DataProviderClass.Instance().GetTradeRefundMessage(traderefundmessageCode);
        }
        
        /*
        public static TradeRefundMessage GetTradeRefundMessage(int traderefundmessageID)
        {
            return DataProviderClass.Instance().GetTradeRefundMessage(traderefundmessageID);
        }
        */

        public static List<TradeRefundMessage> GetTradeRefundMessage(List<int> traderefundmessageCodeList)
        {
            return DataProviderClass.Instance().GetTradeRefundMessage(traderefundmessageCodeList);
        }
        
        /*
        public static List<TradeRefundMessage> GetTradeRefundMessage(List<int> traderefundmessageIDList)
        {
            return DataProviderClass.Instance().GetTradeRefundMessage(traderefundmessageIDList);
        }
        */
    
        public static List<TradeRefundMessage> GetTradeRefundMessage(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetTradeRefundMessage(pageIndex, pageSize, out rowCount);
        }
        
        public static List<TradeRefundMessage> GetTradeRefundMessage(Func<TradeRefundMessage, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetTradeRefundMessage(func, pageIndex, pageSize, out rowCount);
        }
    }
}
