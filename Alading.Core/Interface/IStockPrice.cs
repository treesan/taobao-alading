using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IStockPrice
    {       
        ReturnType AddStockPrice(StockPrice stockprice);
       
        ReturnType AddStockPrice(List<StockPrice> stockpriceList);
        
        ReturnType RemoveAllStockPrice();
       
        ReturnType RemoveStockPrice(Func<StockPrice, bool> func);
              
        ReturnType RemoveStockPrice(string stockpriceCode);
        
        ReturnType RemoveStockPrice(List<string> stockpriceCodeList);
       
        ReturnType UpdateStockPrice(StockPrice stockprice);
       
        ReturnType UpdateStockPrice(string stockpriceCode,StockPrice stockprice);
       
        List<StockPrice> GetAllStockPrice();
      
        List<StockPrice> GetStockPrice(Func<StockPrice, bool> func);
      
        List<StockPrice> GetStockPrice(List<string> stockpriceCodeList);
       
        List<StockPrice> GetStockPrice(int pageIndex, int pageSize, out int rowCount);
        
        List<StockPrice> GetStockPrice(Func<StockPrice, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveStockPrice(int stockpriceID);
        
        ReturnType RemoveStockPrice(List<int> stockpriceIDList);
        
        ReturnType UpdateStockPrice(int stockpriceID,StockPrice stockprice);
        
        List<StockPrice> GetStockPrice(List<int> stockpriceIDList);
        */
    }
}
