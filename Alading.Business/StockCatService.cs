using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class StockCatService
    {
        public static ReturnType UpdateStockCat(string stockcatCode, string catName)
        {
            return DataProviderClass.Instance().UpdateStockCat(stockcatCode, catName);
        }

        public static ReturnType AddStockCat(StockCat stockcat)
        {
            return DataProviderClass.Instance().AddStockCat(stockcat);
        }

        public static ReturnType AddStockCat(List<StockCat> stockcatList)
        {
            return DataProviderClass.Instance().AddStockCat(stockcatList);
        }
    
        public static ReturnType RemoveAllStockCat()
        {
            return DataProviderClass.Instance().RemoveAllStockCat();
        }
    
        public static ReturnType RemoveStockCat(Func<StockCat, bool> func)
        {
            return DataProviderClass.Instance().RemoveStockCat(func);
        }

        public static ReturnType RemoveStockCat(string stockCid)
        {
            return DataProviderClass.Instance().RemoveStockCat(stockCid);
        }       
        
        /*
        public static ReturnType RemoveStockCat(int stockcatID)
        {
            return DataProviderClass.Instance().RemoveStockCat(stockcatID);
        }
        */

        public static ReturnType RemoveStockCat(List<string> stockcatCodeList)
        {
            return DataProviderClass.Instance().RemoveStockCat(stockcatCodeList);
        }        
        
        /*
        public static ReturnType RemoveStockCat(List<int> stockcatIDList)
        {
            return DataProviderClass.Instance().RemoveStockCat(stockcatIDList);
        }
        */
    
        public static ReturnType UpdateStockCat(StockCat stockcat)
        {
            return DataProviderClass.Instance().UpdateStockCat(stockcat);
        }

        public static ReturnType UpdateStockCat(string stockcatCode, StockCat stockcat)
        {
            return DataProviderClass.Instance().UpdateStockCat(stockcatCode, stockcat);
        }
        
        /*
        public static ReturnType UpdateStockCat(int stockcatID, StockCat stockcat)
        {
            return DataProviderClass.Instance().UpdateStockCat(stockcatID, stockcat);
        }
        */
    
        public static List<StockCat> GetAllStockCat()
        {
            return DataProviderClass.Instance().GetAllStockCat();
        }
    
        public static List<StockCat> GetStockCat(Func<StockCat, bool> func)
        {
            return DataProviderClass.Instance().GetStockCat(func);
        }

        public static StockCat GetStockCat(string stockcatCode)
        {
            return DataProviderClass.Instance().GetStockCat(stockcatCode);
        }
        
        /*
        public static StockCat GetStockCat(int stockcatID)
        {
            return DataProviderClass.Instance().GetStockCat(stockcatID);
        }
        */

        public static List<StockCat> GetStockCat(List<string> stockcatCodeList)
        {
            return DataProviderClass.Instance().GetStockCat(stockcatCodeList);
        }
        
        /*
        public static List<StockCat> GetStockCat(List<int> stockcatIDList)
        {
            return DataProviderClass.Instance().GetStockCat(stockcatIDList);
        }
        */
    
        public static List<StockCat> GetStockCat(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockCat(pageIndex, pageSize, out rowCount);
        }
        
        public static List<StockCat> GetStockCat(Func<StockCat, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockCat(func, pageIndex, pageSize, out rowCount);
        }

        public static ReturnType DeleteStockCat(string cid)
        {
            return DataProviderClass.Instance().DeleteStockCat(cid);
        }
    }
}
