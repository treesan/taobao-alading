using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;
using System.Data;

namespace Alading.Business
{
    public static class StockItemService
    {
        public static bool IsOuterIDExisted(string outerID)
        {
            return DataProviderClass.Instance().IsOuterIDExisted(outerID);
        }
        public static StockItem GetStockItemByOutId(string outId)
        {
            return DataProviderClass.Instance().GetStockItemByOutId(outId);
        }

        public static List<ItemCat> GetStockItemCid(Func<StockItem, bool> func)
        {
            return DataProviderClass.Instance().GetStockItemCid(func);
        }

        public static List<ItemCat> GetAllStockItemCid() 
        {
            return DataProviderClass.Instance().GetAllStockItemCid();
        }

        public static ReturnType AddStockItemProducts(StockItem item, List<StockProduct> products, List<StockDetail> sdList, List<StockHouseProduct> shpList)
        {
            return DataProviderClass.Instance().AddStockItemProducts(item, products, sdList, shpList);
        }

        public static ReturnType AddStockItem(StockItem stockitem)
        {
            return DataProviderClass.Instance().AddStockItem(stockitem);
        }

        public static ReturnType AddStockItem(List<StockItem> stockitemList)
        {
            return DataProviderClass.Instance().AddStockItem(stockitemList);
        }
    
        public static ReturnType RemoveAllStockItem()
        {
            return DataProviderClass.Instance().RemoveAllStockItem();
        }
    
        public static ReturnType RemoveStockItem(Func<StockItem, bool> func)
        {
            return DataProviderClass.Instance().RemoveStockItem(func);
        }
        
        public static ReturnType RemoveStockItem(string stockitemCode)
        {
            return DataProviderClass.Instance().RemoveStockItem(stockitemCode);
        }       
        
        /*
        public static ReturnType RemoveStockItem(int stockitemID)
        {
            return DataProviderClass.Instance().RemoveStockItem(stockitemID);
        }
        */
    
        public static ReturnType RemoveStockItem(List<string> stockitemCodeList)
        {
            return DataProviderClass.Instance().RemoveStockItem(stockitemCodeList);
        }        
        
        /*
        public static ReturnType RemoveStockItem(List<int> stockitemIDList)
        {
            return DataProviderClass.Instance().RemoveStockItem(stockitemIDList);
        }
        */
    
        public static ReturnType UpdateStockItem(StockItem stockitem)
        {
            return DataProviderClass.Instance().UpdateStockItem(stockitem);
        }
    
        public static ReturnType UpdateStockItem(string stockitemCode, StockItem stockitem)
        {
            return DataProviderClass.Instance().UpdateStockItem(stockitemCode, stockitem);
        }

        /// <summary>
        /// 更新StockItem的StockProduct
        /// </summary>
        /// <param name="stockitemCode"></param>
        /// <param name="productList"></param>
        /// <returns></returns>
        public static ReturnType UpdateStockItem(string outerID, List<StockProduct> productList)
        {
            return DataProviderClass.Instance().UpdateStockItem(outerID, productList);
        }

        public static ReturnType UpdateStockItem(string stockitemCode, View_StockItemUnit stockitem)
        {
            return DataProviderClass.Instance().UpdateStockItem(stockitemCode, stockitem);
        }

        public static ReturnType UpdateStockItemProps(string stockitemCode, string props, string inputPids, string inputStr)
        {
            return DataProviderClass.Instance().UpdateStockItemProps(stockitemCode, props,inputPids,inputStr);
        }
        
        /*
        public static ReturnType UpdateStockItem(int stockitemID, StockItem stockitem)
        {
            return DataProviderClass.Instance().UpdateStockItem(stockitemID, stockitem);
        }
        */
    
        public static List<StockItem> GetAllStockItem()
        {
            return DataProviderClass.Instance().GetAllStockItem();
        }
    
        public static List<StockItem> GetStockItem(Func<StockItem, bool> func)
        {
            return DataProviderClass.Instance().GetStockItem(func);
        }
    
        public static StockItem GetStockItem(string outer_id)
        {
            return DataProviderClass.Instance().GetStockItem(outer_id);
        }


        public static List<StockItem> GetStockItemLocal(List<string> outeridList)
        {
            return DataProviderClass.Instance().GetStockItemLocal(outeridList);
        }
        
        /*
        public static StockItem GetStockItem(int stockitemID)
        {
            return DataProviderClass.Instance().GetStockItem(stockitemID);
        }
        */
    
        public static List<StockItem> GetStockItem(List<string> stockitemCodeList)
        {
            return DataProviderClass.Instance().GetStockItem(stockitemCodeList);
        }

        public static List<StockItem> GetStockItem(List<string> cidList, bool IsCidTrueOrStockCid)
        {
            return DataProviderClass.Instance().GetStockItem(cidList, IsCidTrueOrStockCid);
        }
        /*
        public static List<StockItem> GetStockItem(List<int> stockitemIDList)
        {
            return DataProviderClass.Instance().GetStockItem(stockitemIDList);
        }
        */
    
        public static List<StockItem> GetStockItem(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockItem(pageIndex, pageSize, out rowCount);
        }
        
        public static List<StockItem> GetStockItem(Func<StockItem, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockItem(func, pageIndex, pageSize, out rowCount);
        }

        public static List<View_StockItemUnit> GetStockItems(Func<View_StockItemUnit, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockItems(func, pageIndex, pageSize, out rowCount);
        }

        public static ReturnType InitInput(StockItem stockItem, List<StockProduct> stockProductList, List<StockDetail> stockDetailList, List<StockHouseProduct> houseProductList)
        {
            return DataProviderClass.Instance().InitInput(stockItem, stockProductList, stockDetailList, houseProductList);
        }

        public static List<View_StockItemProduct> GetLocalStockItem()
        {
            return DataProviderClass.Instance().GetLocalStockItem();
        }

        public static List<string> GetWhereInOuterIds(List<string> outerIDList)
        {
            return DataProviderClass.Instance().GetWhereInOuterIds(outerIDList);
        }
    }
}
