using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class StockHouseService
    {
        #region StockHouse部分

        public static List<StockHouseProduct> GetAllStockHouseProduct()
        {
            return DataProviderClass.Instance().GetAllStockHouseProduct();
        }

        public static ReturnType AddStockHouse(StockHouse stockhouse)
        {
            return DataProviderClass.Instance().AddStockHouse(stockhouse);
        }

        public static ReturnType AddStockHouse(List<StockHouse> stockhouseList)
        {
            return DataProviderClass.Instance().AddStockHouse(stockhouseList);
        }
    
        public static ReturnType RemoveAllStockHouse()
        {
            return DataProviderClass.Instance().RemoveAllStockHouse();
        }
    
        public static ReturnType RemoveStockHouse(Func<StockHouse, bool> func)
        {
            return DataProviderClass.Instance().RemoveStockHouse(func);
        }
        
        public static ReturnType RemoveStockHouse(string stockhouseCode)
        {
            return DataProviderClass.Instance().RemoveStockHouse(stockhouseCode);
        }       
        
        /*
        public static ReturnType RemoveStockHouse(int stockhouseID)
        {
            return DataProviderClass.Instance().RemoveStockHouse(stockhouseID);
        }
        */
    
        public static ReturnType RemoveStockHouse(List<string> stockhouseCodeList)
        {
            return DataProviderClass.Instance().RemoveStockHouse(stockhouseCodeList);
        }        
        
        /*
        public static ReturnType RemoveStockHouse(List<int> stockhouseIDList)
        {
            return DataProviderClass.Instance().RemoveStockHouse(stockhouseIDList);
        }
        */
    
        public static ReturnType UpdateStockHouse(StockHouse stockhouse)
        {
            return DataProviderClass.Instance().UpdateStockHouse(stockhouse);
        }
    
        public static ReturnType UpdateStockHouse(string stockhouseCode, StockHouse stockhouse)
        {
            return DataProviderClass.Instance().UpdateStockHouse(stockhouseCode, stockhouse);
        }
        
        /*
        public static ReturnType UpdateStockHouse(int stockhouseID, StockHouse stockhouse)
        {
            return DataProviderClass.Instance().UpdateStockHouse(stockhouseID, stockhouse);
        }
        */
    
        public static List<StockHouse> GetAllStockHouse()
        {
            return DataProviderClass.Instance().GetAllStockHouse();
        }
    
        public static List<StockHouse> GetStockHouse(Func<StockHouse, bool> func)
        {
            return DataProviderClass.Instance().GetStockHouse(func);
        }
    
        public static StockHouse GetStockHouse(string stockhouseCode)
        {
            return DataProviderClass.Instance().GetStockHouse(stockhouseCode);
        }
        
        /*
        public static StockHouse GetStockHouse(int stockhouseID)
        {
            return DataProviderClass.Instance().GetStockHouse(stockhouseID);
        }
        */
    
        public static List<StockHouse> GetStockHouse(List<string> stockhouseCodeList)
        {
            return DataProviderClass.Instance().GetStockHouse(stockhouseCodeList);
        }
        
        /*
        public static List<StockHouse> GetStockHouse(List<int> stockhouseIDList)
        {
            return DataProviderClass.Instance().GetStockHouse(stockhouseIDList);
        }
        */
    
        public static List<StockHouse> GetStockHouse(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockHouse(pageIndex, pageSize, out rowCount);
        }
        
        public static List<StockHouse> GetStockHouse(Func<StockHouse, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockHouse(func, pageIndex, pageSize, out rowCount);
        }

        #endregion

        #region StockHouseProduct部分
        public static int GetQuantity(string skuOuterID, string houseCode, string layoutCode)
        {
            return DataProviderClass.Instance().GetQuantity(skuOuterID, houseCode, layoutCode);
        }

        public static ReturnType AddStockHouseProduct(StockHouseProduct stockHouseProduct)
        {
            return DataProviderClass.Instance().AddStockHouseProduct(stockHouseProduct);
        }

        public static ReturnType AddStockHouseProduct(List<StockHouseProduct> StockHouseProductList)
        {
            return DataProviderClass.Instance().AddStockHouseProduct(StockHouseProductList);
        }

        public static List<StockHouseProduct> GetStockHouseProduct(Func<StockHouseProduct, bool> func)
        {
            return DataProviderClass.Instance().GetStockHouseProduct(func);
        }

        public static List<StockHouseProduct> GetStockHouseProduct(List<string> StockHouseProductCodeList)
        {
            return DataProviderClass.Instance().GetStockHouseProduct(StockHouseProductCodeList);
        }

        public static StockHouseProduct GetStockHouseProduct(string SkuOuterID)
        {
            return DataProviderClass.Instance().GetStockHouseProduct(SkuOuterID);
        }

        public static List<StockHouseProduct> GetSHProBySkuOuterID(List<string> SkuOuterIDList)
        {
            return DataProviderClass.Instance().GetSHProBySkuOuterID(SkuOuterIDList);
        }
        #endregion
    }
}
