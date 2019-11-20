using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Entity;

namespace Alading.Business
{
    public static class View_StockItemProductService
    {
        public static List<View_StockItemProduct> GetView_StockItemProductByType(int stockItemType)
        {
            return DataProviderClass.Instance().GetView_StockItemProductByType(stockItemType);
        }

        public static View_StockItemProduct GetView_StockItemProductBySkuOuterID(string skuOuterID)
        {
            return DataProviderClass.Instance().GetView_StockItemProductBySkuOuterId(skuOuterID);
        }

        public static List<View_StockItemProduct> GetView_StockItemProduct(List<string> skuOuterIDList)
        {
            return DataProviderClass.Instance().GetView_StockItemProduct(skuOuterIDList);
        }

        public static List<View_StockItemProduct> GetView_StockItemProduct(Func<View_StockItemProduct, bool> func)
        {
            return DataProviderClass.Instance().GetView_StockItemProduct(func);
        }

        public static List<View_StockItemProduct> GetView_StockItemProduct(Func<View_StockItemProduct, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetView_StockItemProduct(func,pageIndex,pageSize,out rowCount);
        }

        public static List<StockItem> GetView_StockItemProduct(int funcType, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetView_StockItemProduct(funcType, pageIndex, pageSize, out rowCount);
        }

        public static List<View_StockItemUnit> GetView_StockItemProducts(int funcType, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetView_StockItemProducts(funcType, pageIndex, pageSize, out rowCount);
        }

        public static List<View_StockItemProduct> GetView_StockItemProduct(string StockCid, int currentIndex, int dataPerPage, ref int allIndex)
        {
            return DataProviderClass.Instance().GetView_StockItemProduct(StockCid,currentIndex,dataPerPage,ref allIndex);
        }

        public static List<View_StockProductHouse> GetView_StockProductHouse(string StockCid, string HouseCode, int currentIndex, int dataPerPage, ref int allIndex)
        {
            return DataProviderClass.Instance().GetView_StockProductHouse(StockCid, HouseCode, currentIndex, dataPerPage, ref allIndex);
        }

        public static List<View_StockItemProduct> GetView_StockItemProductByOuterId(string outer_id)
         {
             return DataProviderClass.Instance().GetView_StockItemProductByOuterId(outer_id);
         }

        public static View_StockItemProduct GetView_StockItemProductBySkuOuterId(string skuOuterID)
        {
            return DataProviderClass.Instance().GetView_StockItemProductBySkuOuterId(skuOuterID);
        }

        public static IEnumerable<View_StockItemProduct> GetAllView_StockItemProduct()
        {
            return DataProviderClass.Instance().GetAllView_StockItemProduct();
        }

        public static List<View_StockItemProduct> GetView_StockItemProductItem(string outer_id)
        {
            return DataProviderClass.Instance().GetView_StockItemProductItem(outer_id);
        }
    }
}
