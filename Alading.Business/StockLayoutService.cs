using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class StockLayoutService
    {
        public static View_HouseLayout GetViewHouseLayout(string layoutCode)
        {
            return DataProviderClass.Instance().GetViewHouseLayout(layoutCode);
        }
        public static ReturnType AddStockLayout(StockLayout stocklayout)
        {
            return DataProviderClass.Instance().AddStockLayout(stocklayout);
        }

        public static ReturnType AddStockLayout(List<StockLayout> stocklayoutList)
        {
            return DataProviderClass.Instance().AddStockLayout(stocklayoutList);
        }
    
        public static ReturnType RemoveAllStockLayout()
        {
            return DataProviderClass.Instance().RemoveAllStockLayout();
        }
    
        public static ReturnType RemoveStockLayout(Func<StockLayout, bool> func)
        {
            return DataProviderClass.Instance().RemoveStockLayout(func);
        }
        
        public static ReturnType RemoveStockLayout(string stocklayoutCode)
        {
            return DataProviderClass.Instance().RemoveStockLayout(stocklayoutCode);
        }       
        
        /*
        public static ReturnType RemoveStockLayout(int stocklayoutID)
        {
            return DataProviderClass.Instance().RemoveStockLayout(stocklayoutID);
        }
        */
    
        public static ReturnType RemoveStockLayout(List<string> stocklayoutCodeList)
        {
            return DataProviderClass.Instance().RemoveStockLayout(stocklayoutCodeList);
        }        
        
        /*
        public static ReturnType RemoveStockLayout(List<int> stocklayoutIDList)
        {
            return DataProviderClass.Instance().RemoveStockLayout(stocklayoutIDList);
        }
        */
    
        public static ReturnType UpdateStockLayout(StockLayout stocklayout)
        {
            return DataProviderClass.Instance().UpdateStockLayout(stocklayout);
        }
    
        public static ReturnType UpdateStockLayout(string stocklayoutCode, StockLayout stocklayout)
        {
            return DataProviderClass.Instance().UpdateStockLayout(stocklayoutCode, stocklayout);
        }
        
        /*
        public static ReturnType UpdateStockLayout(int stocklayoutID, StockLayout stocklayout)
        {
            return DataProviderClass.Instance().UpdateStockLayout(stocklayoutID, stocklayout);
        }
        */
    
        public static List<StockLayout> GetAllStockLayout()
        {
            return DataProviderClass.Instance().GetAllStockLayout();
        }
    
        public static List<StockLayout> GetStockLayout(Func<StockLayout, bool> func)
        {
            return DataProviderClass.Instance().GetStockLayout(func);
        }
    
        public static StockLayout GetStockLayout(string stocklayoutCode)
        {
            return DataProviderClass.Instance().GetStockLayout(stocklayoutCode);
        }
        
        /*
        public static StockLayout GetStockLayout(int stocklayoutID)
        {
            return DataProviderClass.Instance().GetStockLayout(stocklayoutID);
        }
        */
    
        public static List<StockLayout> GetStockLayout(List<string> stocklayoutCodeList)
        {
            return DataProviderClass.Instance().GetStockLayout(stocklayoutCodeList);
        }
        
        /*
        public static List<StockLayout> GetStockLayout(List<int> stocklayoutIDList)
        {
            return DataProviderClass.Instance().GetStockLayout(stocklayoutIDList);
        }
        */
    
        public static List<StockLayout> GetStockLayout(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockLayout(pageIndex, pageSize, out rowCount);
        }
        
        public static List<StockLayout> GetStockLayout(Func<StockLayout, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockLayout(func, pageIndex, pageSize, out rowCount);
        }
    }
}
