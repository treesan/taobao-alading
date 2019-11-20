using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class StockProductService
    {
        public static List<View_CheckDetail> GetViewCheckDetail(string stockCheckCode)
        {
            return DataProviderClass.Instance().GetViewCheckDetail(stockCheckCode);
        }
        public static List<string> GetSkuOuterID(string stockHouseProduct_HouseCode)
        {
            return DataProviderClass.Instance().GetSkuOuterID(stockHouseProduct_HouseCode);
        }

        public static ReturnType AddStockCheckAndDetails(StockCheck stockCheck, List<StockCheckDetail> CheckDetails)
        {
            return DataProviderClass.Instance().AddStockCheckAndDetails(stockCheck, CheckDetails);
        }
        public static List<View_CheckDetail> GetViewCheckDetail(Func<View_CheckDetail, bool> func)
        {
            return DataProviderClass.Instance().GetViewCheckDetail(func);
        }
        public static List<View_StockCheck> GetAllViewStockCheck()
        {
            return DataProviderClass.Instance().GetAllViewStockCheck();
        }
        public static ReturnType SkuOutIdIsOnly(List<string> skuOutIdList)
        {
            return DataProviderClass.Instance().SkuOutIdIsOnly(skuOutIdList);
        }

        public static ReturnType AddStockProduct(StockProduct stockproduct)
        {
            return DataProviderClass.Instance().AddStockProduct(stockproduct);
        }

        public static ReturnType AddStockProduct(List<StockProduct> stockproductList)
        {
            return DataProviderClass.Instance().AddStockProduct(stockproductList);
        }
    
        public static ReturnType RemoveAllStockProduct()
        {
            return DataProviderClass.Instance().RemoveAllStockProduct();
        }
    
        public static ReturnType RemoveStockProduct(Func<StockProduct, bool> func)
        {
            return DataProviderClass.Instance().RemoveStockProduct(func);
        }
        
        public static ReturnType RemoveStockProduct(string stockproductCode)
        {
            return DataProviderClass.Instance().RemoveStockProduct(stockproductCode);
        }


        public static ReturnType RemoveStockProduct(string skuOuterID,string outerID)
        {
            return DataProviderClass.Instance().RemoveStockProduct(skuOuterID,outerID);
        }  
        /*
        public static ReturnType RemoveStockProduct(int stockproductID)
        {
            return DataProviderClass.Instance().RemoveStockProduct(stockproductID);
        }
        */
    
        public static ReturnType RemoveStockProduct(List<string> stockproductCodeList)
        {
            return DataProviderClass.Instance().RemoveStockProduct(stockproductCodeList);
        }        
        
        /*
        public static ReturnType RemoveStockProduct(List<int> stockproductIDList)
        {
            return DataProviderClass.Instance().RemoveStockProduct(stockproductIDList);
        }
        */
    
        public static ReturnType UpdateStockProduct(StockProduct stockproduct)
        {
            return DataProviderClass.Instance().UpdateStockProduct(stockproduct);
        }
    
        public static ReturnType UpdateStockProduct(string stockproductCode, StockProduct stockproduct)
        {
            return DataProviderClass.Instance().UpdateStockProduct(stockproductCode, stockproduct);
        }

        public static ReturnType UpdateStock(List<StockProduct> stockProductList, List<StockHouseProduct> stockHouseProList, List<StockInOut> stockInOutList
            , List<StockDetail> stockDetailList,PayCharge payCharge, List<string> outerSkuIdList, List<string> outerIdList)//zxl
        {
            return DataProviderClass.Instance().UpdateStock(stockProductList,stockHouseProList, stockInOutList, stockDetailList
                , payCharge, outerSkuIdList, outerIdList);
        }
        /*
        public static ReturnType UpdateStockProduct(int stockproductID, StockProduct stockproduct)
        {
            return DataProviderClass.Instance().UpdateStockProduct(stockproductID, stockproduct);
        }
        */
    
        public static List<StockProduct> GetAllStockProduct()
        {
            return DataProviderClass.Instance().GetAllStockProduct();
        }
    
        public static List<StockProduct> GetStockProduct(Func<StockProduct, bool> func)
        {
            return DataProviderClass.Instance().GetStockProduct(func);
        }

        public static List<View_StockProductHouse> GetViewStockProduct(string outerID)
        {
            return DataProviderClass.Instance().GetViewStockProduct(outerID);
        }
       
        public static StockProduct GetStockProduct(string skuOutID)
        {
            return DataProviderClass.Instance().GetStockProduct(skuOutID);
        }

        public static List<StockProduct> GetStockProductByOuterId(string OuterId)
         {
             return DataProviderClass.Instance().GetStockProductByOuterId(OuterId);
         }
        /*
        public static StockProduct GetStockProduct(int stockproductID)
        {
            return DataProviderClass.Instance().GetStockProduct(stockproductID);
        }
        */

        public static List<StockProduct> GetStockProduct(List<string> skuOuterIDList)
        {
            return DataProviderClass.Instance().GetStockProduct(skuOuterIDList);
        }
        
        /*
        public static List<StockProduct> GetStockProduct(List<int> stockproductIDList)
        {
            return DataProviderClass.Instance().GetStockProduct(stockproductIDList);
        }
        */
    
        public static List<StockProduct> GetStockProduct(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockProduct(pageIndex, pageSize, out rowCount);
        }
        
        public static List<StockProduct> GetStockProduct(Func<StockProduct, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockProduct(func, pageIndex, pageSize, out rowCount);
        }
    }
}
