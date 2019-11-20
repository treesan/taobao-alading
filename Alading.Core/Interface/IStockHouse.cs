using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IStockHouse
    {
        #region StockHouse部分

        ReturnType AddStockHouse(StockHouse stockhouse);
       
        ReturnType AddStockHouse(List<StockHouse> stockhouseList);
        
        ReturnType RemoveAllStockHouse();
       
        ReturnType RemoveStockHouse(Func<StockHouse, bool> func);
              
        ReturnType RemoveStockHouse(string stockhouseCode);
        
        ReturnType RemoveStockHouse(List<string> stockhouseCodeList);
       
        ReturnType UpdateStockHouse(StockHouse stockhouse);
       
        ReturnType UpdateStockHouse(string stockhouseCode,StockHouse stockhouse);
       
        List<StockHouse> GetAllStockHouse();
      
        List<StockHouse> GetStockHouse(Func<StockHouse, bool> func);
      
        List<StockHouse> GetStockHouse(List<string> stockhouseCodeList);
       
        List<StockHouse> GetStockHouse(int pageIndex, int pageSize, out int rowCount);
        
        List<StockHouse> GetStockHouse(Func<StockHouse, bool> func, int pageIndex, int pageSize, out int rowCount);

        #endregion


        /*        
        ReturnType RemoveStockHouse(int stockhouseID);
        
        ReturnType RemoveStockHouse(List<int> stockhouseIDList);
        
        ReturnType UpdateStockHouse(int stockhouseID,StockHouse stockhouse);
        
        List<StockHouse> GetStockHouse(List<int> stockhouseIDList);
        */

        #region StockHouseProduct部分
        int GetQuantity(string skuOuterID, string houseCode, string layoutCode);

        List<StockHouseProduct> GetAllStockHouseProduct();

        ReturnType AddStockHouseProduct(StockHouseProduct stockHouseProduct);

        ReturnType AddStockHouseProduct(List<StockHouseProduct> StockHouseProductList);

        List<StockHouseProduct> GetStockHouseProduct(Func<StockHouseProduct, bool> func);

        List<StockHouseProduct> GetStockHouseProduct(List<string> StockHouseProductCodeList);

        #endregion
    }
}
