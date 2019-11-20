using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IStockPropValue
    {       
        ReturnType AddStockPropValue(StockPropValue stockpropvalue);
       
        ReturnType AddStockPropValue(List<StockPropValue> stockpropvalueList);
        
        ReturnType RemoveAllStockPropValue();
       
        ReturnType RemoveStockPropValue(Func<StockPropValue, bool> func);

        ReturnType RemoveStockPropValue(StockPropValue propvalue);
        
        ReturnType RemoveStockPropValue(List<string> stockpropvalueCodeList);
       
        ReturnType UpdateStockPropValue(StockPropValue stockpropvalue);
       
        ReturnType UpdateStockPropValue(string stockpropvalueCode,StockPropValue stockpropvalue);
       
        List<StockPropValue> GetAllStockPropValue();
      
        List<StockPropValue> GetStockPropValue(Func<StockPropValue, bool> func);
      
        List<StockPropValue> GetStockPropValue(List<string> stockpropvalueCodeList);
       
        List<StockPropValue> GetStockPropValue(int pageIndex, int pageSize, out int rowCount);
        
        List<StockPropValue> GetStockPropValue(Func<StockPropValue, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveStockPropValue(int stockpropvalueID);
        
        ReturnType RemoveStockPropValue(List<int> stockpropvalueIDList);
        
        ReturnType UpdateStockPropValue(int stockpropvalueID,StockPropValue stockpropvalue);
        
        List<StockPropValue> GetStockPropValue(List<int> stockpropvalueIDList);
        */
    }
}
