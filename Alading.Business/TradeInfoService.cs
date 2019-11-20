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
    public static class TradeInfoService
    {

        public static ReturnType AddTradeInfo(TradeInfo tradeinfo)
        {
            return DataProviderClass.Instance().AddTradeInfo(tradeinfo);
        }
        public static ReturnType AddTradeInfoSqlBulkCopy(DataTable dataTable)
        {
            return DataProviderClass.Instance().AddTradeInfoSqlBulkCopy(dataTable);
        }
        public static ReturnType AddTradeInfo(List<TradeInfo> tradeinfoList)
        {
            return DataProviderClass.Instance().AddTradeInfo(tradeinfoList);
        }
    
        public static ReturnType RemoveAllTradeInfo()
        {
            return DataProviderClass.Instance().RemoveAllTradeInfo();
        }
    
        public static ReturnType RemoveTradeInfo(Func<TradeInfo, bool> func)
        {
            return DataProviderClass.Instance().RemoveTradeInfo(func);
        }
        
        public static ReturnType RemoveTradeInfo(string tradeinfoCode)
        {
            return DataProviderClass.Instance().RemoveTradeInfo(tradeinfoCode);
        }       
        
        /*
        public static ReturnType RemoveTradeInfo(int tradeinfoID)
        {
            return DataProviderClass.Instance().RemoveTradeInfo(tradeinfoID);
        }
        */
    
        public static ReturnType RemoveTradeInfo(List<string> tradeinfoCodeList)
        {
            return DataProviderClass.Instance().RemoveTradeInfo(tradeinfoCodeList);
        }        
        
        /*
        public static ReturnType RemoveTradeInfo(List<int> tradeinfoIDList)
        {
            return DataProviderClass.Instance().RemoveTradeInfo(tradeinfoIDList);
        }
        */
    
        public static ReturnType UpdateTradeInfo(TradeInfo tradeinfo)
        {
            return DataProviderClass.Instance().UpdateTradeInfo(tradeinfo);
        }
    
        public static ReturnType UpdateTradeInfo(string tradeinfoCode, TradeInfo tradeinfo)
        {
            return DataProviderClass.Instance().UpdateTradeInfo(tradeinfoCode, tradeinfo);
        }
        
        /*
        public static ReturnType UpdateTradeInfo(int tradeinfoID, TradeInfo tradeinfo)
        {
            return DataProviderClass.Instance().UpdateTradeInfo(tradeinfoID, tradeinfo);
        }
        */
    
        public static List<TradeInfo> GetAllTradeInfo()
        {
            return DataProviderClass.Instance().GetAllTradeInfo();
        }
    
        public static List<TradeInfo> GetTradeInfo(Func<TradeInfo, bool> func)
        {
            return DataProviderClass.Instance().GetTradeInfo(func);
        }
    
        public static TradeInfo GetTradeInfo(string tradeinfoCode)
        {
            return DataProviderClass.Instance().GetTradeInfo(tradeinfoCode);
        }
        
        /*
        public static TradeInfo GetTradeInfo(int tradeinfoID)
        {
            return DataProviderClass.Instance().GetTradeInfo(tradeinfoID);
        }
        */
    
        public static List<TradeInfo> GetTradeInfo(List<string> tradeinfoCodeList)
        {
            return DataProviderClass.Instance().GetTradeInfo(tradeinfoCodeList);
        }
        
        /*
        public static List<TradeInfo> GetTradeInfo(List<int> tradeinfoIDList)
        {
            return DataProviderClass.Instance().GetTradeInfo(tradeinfoIDList);
        }
        */
    
        public static List<TradeInfo> GetTradeInfo(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetTradeInfo(pageIndex, pageSize, out rowCount);
        }
        
        public static List<TradeInfo> GetTradeInfo(Func<TradeInfo, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetTradeInfo(func, pageIndex, pageSize, out rowCount);
        }
    }
}
