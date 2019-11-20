using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class StockPropValueService
    {

        public static ReturnType AddStockPropValue(StockPropValue stockpropvalue)
        {
            return DataProviderClass.Instance().AddStockPropValue(stockpropvalue);
        }

        public static ReturnType AddStockPropValue(List<StockPropValue> stockpropvalueList)
        {
            return DataProviderClass.Instance().AddStockPropValue(stockpropvalueList);
        }
    
        public static ReturnType RemoveAllStockPropValue()
        {
            return DataProviderClass.Instance().RemoveAllStockPropValue();
        }
    
        public static ReturnType RemoveStockPropValue(Func<StockPropValue, bool> func)
        {
            return DataProviderClass.Instance().RemoveStockPropValue(func);
        }

        public static ReturnType RemoveStockPropValue(StockPropValue propvalue)
        {
            return DataProviderClass.Instance().RemoveStockPropValue(propvalue);
        }       
        
        /*
        public static ReturnType RemoveStockPropValue(int stockpropvalueID)
        {
            return DataProviderClass.Instance().RemoveStockPropValue(stockpropvalueID);
        }
        */
    
        public static ReturnType RemoveStockPropValue(List<string> stockpropvalueCodeList)
        {
            return DataProviderClass.Instance().RemoveStockPropValue(stockpropvalueCodeList);
        }        
        
        /*
        public static ReturnType RemoveStockPropValue(List<int> stockpropvalueIDList)
        {
            return DataProviderClass.Instance().RemoveStockPropValue(stockpropvalueIDList);
        }
        */
    
        public static ReturnType UpdateStockPropValue(StockPropValue stockpropvalue)
        {
            return DataProviderClass.Instance().UpdateStockPropValue(stockpropvalue);
        }
    
        public static ReturnType UpdateStockPropValue(string stockpropvalueCode, StockPropValue stockpropvalue)
        {
            return DataProviderClass.Instance().UpdateStockPropValue(stockpropvalueCode, stockpropvalue);
        }
        
        /*
        public static ReturnType UpdateStockPropValue(int stockpropvalueID, StockPropValue stockpropvalue)
        {
            return DataProviderClass.Instance().UpdateStockPropValue(stockpropvalueID, stockpropvalue);
        }
        */
    
        public static List<StockPropValue> GetAllStockPropValue()
        {
            return DataProviderClass.Instance().GetAllStockPropValue();
        }
    
        public static List<StockPropValue> GetStockPropValue(Func<StockPropValue, bool> func)
        {
            return DataProviderClass.Instance().GetStockPropValue(func);
        }
    
        public static StockPropValue GetStockPropValue(string stockpropvalueCode)
        {
            return DataProviderClass.Instance().GetStockPropValue(stockpropvalueCode);
        }
        
        /*
        public static StockPropValue GetStockPropValue(int stockpropvalueID)
        {
            return DataProviderClass.Instance().GetStockPropValue(stockpropvalueID);
        }
        */
    
        public static List<StockPropValue> GetStockPropValue(List<string> stockpropvalueCodeList)
        {
            return DataProviderClass.Instance().GetStockPropValue(stockpropvalueCodeList);
        }
        
        /*
        public static List<StockPropValue> GetStockPropValue(List<int> stockpropvalueIDList)
        {
            return DataProviderClass.Instance().GetStockPropValue(stockpropvalueIDList);
        }
        */
    
        public static List<StockPropValue> GetStockPropValue(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockPropValue(pageIndex, pageSize, out rowCount);
        }
        
        public static List<StockPropValue> GetStockPropValue(Func<StockPropValue, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockPropValue(func, pageIndex, pageSize, out rowCount);
        }
    }
}
