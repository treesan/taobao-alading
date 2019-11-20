using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class StockPriceService
    {

        public static ReturnType AddStockPrice(StockPrice stockprice)
        {
            return DataProviderClass.Instance().AddStockPrice(stockprice);
        }

        public static ReturnType AddStockPrice(List<StockPrice> stockpriceList)
        {
            return DataProviderClass.Instance().AddStockPrice(stockpriceList);
        }
    
        public static ReturnType RemoveAllStockPrice()
        {
            return DataProviderClass.Instance().RemoveAllStockPrice();
        }
    
        public static ReturnType RemoveStockPrice(Func<StockPrice, bool> func)
        {
            return DataProviderClass.Instance().RemoveStockPrice(func);
        }
        
        public static ReturnType RemoveStockPrice(string stockpriceCode)
        {
            return DataProviderClass.Instance().RemoveStockPrice(stockpriceCode);
        }       
        
        /*
        public static ReturnType RemoveStockPrice(int stockpriceID)
        {
            return DataProviderClass.Instance().RemoveStockPrice(stockpriceID);
        }
        */
    
        public static ReturnType RemoveStockPrice(List<string> stockpriceCodeList)
        {
            return DataProviderClass.Instance().RemoveStockPrice(stockpriceCodeList);
        }        
        
        /*
        public static ReturnType RemoveStockPrice(List<int> stockpriceIDList)
        {
            return DataProviderClass.Instance().RemoveStockPrice(stockpriceIDList);
        }
        */
    
        public static ReturnType UpdateStockPrice(StockPrice stockprice)
        {
            return DataProviderClass.Instance().UpdateStockPrice(stockprice);
        }
    
        public static ReturnType UpdateStockPrice(string stockpriceCode, StockPrice stockprice)
        {
            return DataProviderClass.Instance().UpdateStockPrice(stockpriceCode, stockprice);
        }
        
        /*
        public static ReturnType UpdateStockPrice(int stockpriceID, StockPrice stockprice)
        {
            return DataProviderClass.Instance().UpdateStockPrice(stockpriceID, stockprice);
        }
        */
    
        public static List<StockPrice> GetAllStockPrice()
        {
            return DataProviderClass.Instance().GetAllStockPrice();
        }
    
        public static List<StockPrice> GetStockPrice(Func<StockPrice, bool> func)
        {
            return DataProviderClass.Instance().GetStockPrice(func);
        }
    
        public static StockPrice GetStockPrice(string stockpriceCode)
        {
            return DataProviderClass.Instance().GetStockPrice(stockpriceCode);
        }
        
        /*
        public static StockPrice GetStockPrice(int stockpriceID)
        {
            return DataProviderClass.Instance().GetStockPrice(stockpriceID);
        }
        */
    
        public static List<StockPrice> GetStockPrice(List<string> stockpriceCodeList)
        {
            return DataProviderClass.Instance().GetStockPrice(stockpriceCodeList);
        }
        
        /*
        public static List<StockPrice> GetStockPrice(List<int> stockpriceIDList)
        {
            return DataProviderClass.Instance().GetStockPrice(stockpriceIDList);
        }
        */
    
        public static List<StockPrice> GetStockPrice(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockPrice(pageIndex, pageSize, out rowCount);
        }
        
        public static List<StockPrice> GetStockPrice(Func<StockPrice, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockPrice(func, pageIndex, pageSize, out rowCount);
        }
    }
}
