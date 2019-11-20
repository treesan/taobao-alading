using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface ITradeRate
    {       
        ReturnType AddTradeRate(TradeRate traderate);
       
        ReturnType AddTradeRate(List<TradeRate> traderateList);
        
        ReturnType RemoveAllTradeRate();
       
        ReturnType RemoveTradeRate(Func<TradeRate, bool> func);
              
        ReturnType RemoveTradeRate(string traderateCode);
        
        ReturnType RemoveTradeRate(List<string> traderateCodeList);
       
        ReturnType UpdateTradeRate(TradeRate traderate);
       
        ReturnType UpdateTradeRate(string traderateCode,TradeRate traderate);
       
        List<TradeRate> GetAllTradeRate();
      
        List<TradeRate> GetTradeRate(Func<TradeRate, bool> func);
      
        List<TradeRate> GetTradeRate(List<string> traderateCodeList);
       
        List<TradeRate> GetTradeRate(int pageIndex, int pageSize, out int rowCount);
        
        List<TradeRate> GetTradeRate(Func<TradeRate, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveTradeRate(int traderateID);
        
        ReturnType RemoveTradeRate(List<int> traderateIDList);
        
        ReturnType UpdateTradeRate(int traderateID,TradeRate traderate);
        
        List<TradeRate> GetTradeRate(List<int> traderateIDList);
        */
    }
}
