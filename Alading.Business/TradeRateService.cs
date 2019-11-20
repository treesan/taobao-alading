using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class TradeRateService
    {

        public static ReturnType AddTradeRate(TradeRate traderate)
        {
            return DataProviderClass.Instance().AddTradeRate(traderate);
        }

        public static ReturnType AddTradeRate(List<TradeRate> traderateList)
        {
            return DataProviderClass.Instance().AddTradeRate(traderateList);
        }
    
        public static ReturnType RemoveAllTradeRate()
        {
            return DataProviderClass.Instance().RemoveAllTradeRate();
        }
    
        public static ReturnType RemoveTradeRate(Func<TradeRate, bool> func)
        {
            return DataProviderClass.Instance().RemoveTradeRate(func);
        }
        
        public static ReturnType RemoveTradeRate(string traderateCode)
        {
            return DataProviderClass.Instance().RemoveTradeRate(traderateCode);
        }       
        
        /*
        public static ReturnType RemoveTradeRate(int traderateID)
        {
            return DataProviderClass.Instance().RemoveTradeRate(traderateID);
        }
        */
    
        public static ReturnType RemoveTradeRate(List<string> traderateCodeList)
        {
            return DataProviderClass.Instance().RemoveTradeRate(traderateCodeList);
        }        
        
        /*
        public static ReturnType RemoveTradeRate(List<int> traderateIDList)
        {
            return DataProviderClass.Instance().RemoveTradeRate(traderateIDList);
        }
        */
    
        public static ReturnType UpdateTradeRate(TradeRate traderate)
        {
            return DataProviderClass.Instance().UpdateTradeRate(traderate);
        }
    
        public static ReturnType UpdateTradeRate(string traderateCode, TradeRate traderate)
        {
            return DataProviderClass.Instance().UpdateTradeRate(traderateCode, traderate);
        }
        
        /*
        public static ReturnType UpdateTradeRate(int traderateID, TradeRate traderate)
        {
            return DataProviderClass.Instance().UpdateTradeRate(traderateID, traderate);
        }
        */
    
        public static List<TradeRate> GetAllTradeRate()
        {
            return DataProviderClass.Instance().GetAllTradeRate();
        }
    
        public static List<TradeRate> GetTradeRate(Func<TradeRate, bool> func)
        {
            return DataProviderClass.Instance().GetTradeRate(func);
        }
    
        public static TradeRate GetTradeRate(string traderateCode)
        {
            return DataProviderClass.Instance().GetTradeRate(traderateCode);
        }
        
        /*
        public static TradeRate GetTradeRate(int traderateID)
        {
            return DataProviderClass.Instance().GetTradeRate(traderateID);
        }
        */
    
        public static List<TradeRate> GetTradeRate(List<string> traderateCodeList)
        {
            return DataProviderClass.Instance().GetTradeRate(traderateCodeList);
        }

        public static List<TradeRate> GetTradeRateByTid(List<string> tidList)
        {
            return DataProviderClass.Instance().GetTradeRateByTid(tidList);
        }
        
        /*
        public static List<TradeRate> GetTradeRate(List<int> traderateIDList)
        {
            return DataProviderClass.Instance().GetTradeRate(traderateIDList);
        }
        */
    
        public static List<TradeRate> GetTradeRate(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetTradeRate(pageIndex, pageSize, out rowCount);
        }
        
        public static List<TradeRate> GetTradeRate(Func<TradeRate, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetTradeRate(func, pageIndex, pageSize, out rowCount);
        }
    }
}
