using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IStockProduct
    {
        List<View_CheckDetail> GetViewCheckDetail(Func<View_CheckDetail, bool> func);

        List<View_CheckDetail> GetViewCheckDetail(string stockCheckCode);

        List<string> GetSkuOuterID(string stockHouseProduct_HouseCode);
       
        ReturnType AddStockCheckAndDetails(StockCheck stockCheck, List<StockCheckDetail> CheckDetails);

        List<View_StockCheck> GetAllViewStockCheck();

        ReturnType AddStockProduct(StockProduct stockproduct);
       
        ReturnType AddStockProduct(List<StockProduct> stockproductList);
        
        ReturnType RemoveAllStockProduct();
       
        ReturnType RemoveStockProduct(Func<StockProduct, bool> func);

        ReturnType RemoveStockProduct(string skuOuterID, string outerID);
              
        ReturnType RemoveStockProduct(string skuOutID);
        
        ReturnType RemoveStockProduct(List<string> skuOutIDList);
       
        ReturnType UpdateStockProduct(StockProduct stockproduct);
       
        ReturnType UpdateStockProduct(string stockproductCode,StockProduct stockproduct);
       
        List<StockProduct> GetAllStockProduct();

        List<StockProduct> GetStockProductByOuterId(string outer_id);

        List<StockProduct> GetStockProduct(Func<StockProduct, bool> func);
      
        List<StockProduct> GetStockProduct(List<string> skuOutIDList);
       
        List<StockProduct> GetStockProduct(int pageIndex, int pageSize, out int rowCount);
        
        List<StockProduct> GetStockProduct(Func<StockProduct, bool> func, int pageIndex, int pageSize, out int rowCount);

        List<View_StockProductHouse> GetViewStockProduct(string outerID);

        ReturnType SkuOutIdIsOnly(List<string> skuOutIdList);
     
        /*        
        ReturnType RemoveStockProduct(int stockproductID);
        
        ReturnType RemoveStockProduct(List<int> stockproductIDList);
        
        ReturnType UpdateStockProduct(int stockproductID,StockProduct stockproduct);
        
        List<StockProduct> GetStockProduct(List<int> stockproductIDList);
        */
    }
}
