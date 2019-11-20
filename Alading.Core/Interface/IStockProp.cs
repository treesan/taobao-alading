using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IStockProp
    {       
        ReturnType AddStockProp(StockProp stockprop);
       
        ReturnType AddStockProp(List<StockProp> stockpropList);
        
        ReturnType RemoveAllStockProp();
       
        ReturnType RemoveStockProp(Func<StockProp, bool> func);
              
        ReturnType RemoveStockProp(string stockpropCode);
        
        ReturnType RemoveStockProp(List<string> stockpropCodeList);
       
        ReturnType UpdateStockProp(StockProp stockprop);
       
        ReturnType UpdateStockProp(string stockpropCode,StockProp stockprop);
       
        List<StockProp> GetAllStockProp();
      
        List<StockProp> GetStockProp(Func<StockProp, bool> func);
      
        List<StockProp> GetStockProp(List<string> stockpropCodeList);
       
        List<StockProp> GetStockProp(int pageIndex, int pageSize, out int rowCount);
        
        List<StockProp> GetStockProp(Func<StockProp, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveStockProp(int stockpropID);
        
        ReturnType RemoveStockProp(List<int> stockpropIDList);
        
        ReturnType UpdateStockProp(int stockpropID,StockProp stockprop);
        
        List<StockProp> GetStockProp(List<int> stockpropIDList);
        */
        ReturnType DeleteStockPropAndValue(StockProp stockProp);
    }
}
