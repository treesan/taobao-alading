using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;

namespace Alading.Core.Interface
{
    public interface IView_StockItemProduct
    {

        View_StockItemProduct GetView_StockItemProductByType(int stockItemType);

        View_StockItemProduct GetView_StockItemProductByType(string skuOuterID);

        List<View_StockItemProduct> GetView_StockItemProduct(List<string> skuOuterIDList);
        
        List<View_StockItemProduct> GetView_StockItemProduct(Func<View_StockItemProduct, bool> func);

        List<View_TradeStock> GetView_TradeStock(Func<View_TradeStock, bool> func, int pageIndex, int pageSize, out int rowCount);

        List<StockItem> GetView_StockItemProduct(int funcType, int pageIndex, int pageSize, out int rowCount);

        List<View_StockItemProduct> GetView_StockItemProduct(string StockCid,int currentIndex,int dataPerPage,ref int allIndex);

        List<View_StockProductHouse> GetView_StockProductHouse(string StockCid, string HouseCode, int currentIndex, int dataPerPage, ref int allIndex);

        List<View_StockItemUnit> GetView_StockItemProducts(int funcType, int pageIndex, int pageSize, out int rowCount);

        List<View_StockItemProduct> GetView_StockItemProductByOuterId(string outer_id);

        View_StockItemProduct GetView_StockItemProductBySkuOuterId(string skuOuterID);

        IEnumerable<View_StockItemProduct> GetAllView_StockItemProduct();
    }

}
