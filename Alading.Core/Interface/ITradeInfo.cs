using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;
using System.Data;

namespace Alading.Core.Interface
{
    public interface ITradeInfo
    {       
        ReturnType AddTradeInfo(TradeInfo tradeinfo);

        ReturnType AddTradeInfoSqlBulkCopy(DataTable dataTable);

        ReturnType AddTradeInfo(List<TradeInfo> tradeinfoList);
        
        ReturnType RemoveAllTradeInfo();
       
        ReturnType RemoveTradeInfo(Func<TradeInfo, bool> func);
              
        ReturnType RemoveTradeInfo(string tradeinfoCode);
        
        ReturnType RemoveTradeInfo(List<string> tradeinfoCodeList);
       
        ReturnType UpdateTradeInfo(TradeInfo tradeinfo);
       
        ReturnType UpdateTradeInfo(string tradeinfoCode,TradeInfo tradeinfo);
       
        List<TradeInfo> GetAllTradeInfo();
      
        List<TradeInfo> GetTradeInfo(Func<TradeInfo, bool> func);
      
        List<TradeInfo> GetTradeInfo(List<string> tradeinfoCodeList);
       
        List<TradeInfo> GetTradeInfo(int pageIndex, int pageSize, out int rowCount);
        
        List<TradeInfo> GetTradeInfo(Func<TradeInfo, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveTradeInfo(int tradeinfoID);
        
        ReturnType RemoveTradeInfo(List<int> tradeinfoIDList);
        
        ReturnType UpdateTradeInfo(int tradeinfoID,TradeInfo tradeinfo);
        
        List<TradeInfo> GetTradeInfo(List<int> tradeinfoIDList);
        */
    }
}
