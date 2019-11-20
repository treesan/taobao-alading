using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IStockItem
    {
        bool IsOuterIDExisted(string outerID);

        ReturnType AddStockItem(StockItem stockitem);
       
        ReturnType AddStockItem(List<StockItem> stockitemList);

        ReturnType AddStockItemProducts(StockItem item, List<StockProduct> products,List<StockDetail> sdList,List<StockHouseProduct> shpList);
        
        ReturnType RemoveAllStockItem();
       
        ReturnType RemoveStockItem(Func<StockItem, bool> func);
              
        ReturnType RemoveStockItem(string stockitemCode);
        
        ReturnType RemoveStockItem(List<string> stockitemCodeList);
       
        ReturnType UpdateStockItem(StockItem stockitem);
       
        ReturnType UpdateStockItem(string stockitemCode,StockItem stockitem);

        ReturnType UpdateStockItemProps(string stockitemCode, string props, string inputPids, string inputStr);

        /// <summary>
        /// 更新StockItem的StockProduct
        /// </summary>
        /// <param name="stockitemCode"></param>
        /// <param name="productList"></param>
        /// <returns></returns>
        ReturnType UpdateStockItem(string outerID, List<StockProduct> productList);

        ReturnType UpdateStockItem(string stockitemCode, View_StockItemUnit stockitem);
       
        List<StockItem> GetAllStockItem();
      
        List<StockItem> GetStockItem(Func<StockItem, bool> func);
      
        List<StockItem> GetStockItem(List<string> stockitemCodeList);

        List<StockItem> GetStockItem(List<string> cidList,bool IsCidOrStockCid);
       
        List<StockItem> GetStockItem(int pageIndex, int pageSize, out int rowCount);
        
        List<StockItem> GetStockItem(Func<StockItem, bool> func, int pageIndex, int pageSize, out int rowCount);

        List<View_StockItemUnit> GetStockItems(Func<View_StockItemUnit, bool> func, int pageIndex, int pageSize, out int rowCount);

        List<ItemCat> GetAllStockItemCid();

        List<ItemCat> GetStockItemCid(Func<StockItem, bool> func);

        StockItem GetStockItemByOutId(string outId);
        /*        
        ReturnType RemoveStockItem(int stockitemID);
        
        ReturnType RemoveStockItem(List<int> stockitemIDList);
        
        ReturnType UpdateStockItem(int stockitemID,StockItem stockitem);
        
        List<StockItem> GetStockItem(List<int> stockitemIDList);
        */

        ReturnType InitInput(StockItem stockItem, List<StockProduct> stockProductList, List<StockDetail> stockDetailList, List<StockHouseProduct> houseProductList);

        List<string> GetWhereInOuterIds(List<string> outerIDList);
    }
}
