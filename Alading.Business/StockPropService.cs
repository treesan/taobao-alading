using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class StockPropService
    {

        public static ReturnType AddStockProp(StockProp stockprop)
        {
            return DataProviderClass.Instance().AddStockProp(stockprop);
        }

        public static ReturnType AddStockProp(List<StockProp> stockpropList)
        {
            return DataProviderClass.Instance().AddStockProp(stockpropList);
        }
    
        public static ReturnType RemoveAllStockProp()
        {
            return DataProviderClass.Instance().RemoveAllStockProp();
        }
    
        public static ReturnType RemoveStockProp(Func<StockProp, bool> func)
        {
            return DataProviderClass.Instance().RemoveStockProp(func);
        }
        
        public static ReturnType RemoveStockProp(string stockpropCode)
        {
            return DataProviderClass.Instance().RemoveStockProp(stockpropCode);
        }       
        
        /*
        public static ReturnType RemoveStockProp(int stockpropID)
        {
            return DataProviderClass.Instance().RemoveStockProp(stockpropID);
        }
        */
    
        public static ReturnType RemoveStockProp(List<string> stockpropCodeList)
        {
            return DataProviderClass.Instance().RemoveStockProp(stockpropCodeList);
        }        
        
        /*
        public static ReturnType RemoveStockProp(List<int> stockpropIDList)
        {
            return DataProviderClass.Instance().RemoveStockProp(stockpropIDList);
        }
        */
    
        public static ReturnType UpdateStockProp(StockProp stockprop)
        {
            return DataProviderClass.Instance().UpdateStockProp(stockprop);
        }
    
        public static ReturnType UpdateStockProp(string stockpropCode, StockProp stockprop)
        {
            return DataProviderClass.Instance().UpdateStockProp(stockpropCode, stockprop);
        }
        
        /*
        public static ReturnType UpdateStockProp(int stockpropID, StockProp stockprop)
        {
            return DataProviderClass.Instance().UpdateStockProp(stockpropID, stockprop);
        }
        */
    
        public static List<StockProp> GetAllStockProp()
        {
            return DataProviderClass.Instance().GetAllStockProp();
        }
    
        public static List<StockProp> GetStockProp(Func<StockProp, bool> func)
        {
            return DataProviderClass.Instance().GetStockProp(func);
        }
    
        public static StockProp GetStockProp(string stockpropCode)
        {
            return DataProviderClass.Instance().GetStockProp(stockpropCode);
        }
        
        /*
        public static StockProp GetStockProp(int stockpropID)
        {
            return DataProviderClass.Instance().GetStockProp(stockpropID);
        }
        */
    
        public static List<StockProp> GetStockProp(List<string> stockpropCodeList)
        {
            return DataProviderClass.Instance().GetStockProp(stockpropCodeList);
        }
        
        /*
        public static List<StockProp> GetStockProp(List<int> stockpropIDList)
        {
            return DataProviderClass.Instance().GetStockProp(stockpropIDList);
        }
        */
    
        public static List<StockProp> GetStockProp(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockProp(pageIndex, pageSize, out rowCount);
        }
        
        public static List<StockProp> GetStockProp(Func<StockProp, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockProp(func, pageIndex, pageSize, out rowCount);
        }

        public static ReturnType DeleteStockPropAndValue(StockProp stockProp)
        {
            return DataProviderClass.Instance().DeleteStockPropAndValue(stockProp);
        }
    }
}
