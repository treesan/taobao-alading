using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IStockInOut
    {
        List<View_StockDetailInOut> GetStockDetailInOut(Func<View_StockDetailInOut, bool> func);

        ReturnType AddStockInOutDetail(List<Alading.Entity.StockInOut> stockInOutList, List<StockDetail> stockDetailList);

        ReturnType RemoveStockInOutDetail(string stockInOutCode);

        ReturnType AddStockInOut(StockInOut stockinout);
       
        ReturnType AddStockInOut(List<StockInOut> stockinoutList);
        
        ReturnType RemoveAllStockInOut();
       
        ReturnType RemoveStockInOut(Func<StockInOut, bool> func);
              
        ReturnType RemoveStockInOut(string stockinoutCode);
        
        ReturnType RemoveStockInOut(List<string> stockinoutCodeList);
       
        ReturnType UpdateStockInOut(StockInOut stockinout);
       
        ReturnType UpdateStockInOut(string stockinoutCode,StockInOut stockinout);
       
        List<StockInOut> GetAllStockInOut();
      
        List<StockInOut> GetStockInOut(Func<StockInOut, bool> func);
      
        List<StockInOut> GetStockInOut(List<string> stockinoutCodeList);
       
        List<StockInOut> GetStockInOut(int pageIndex, int pageSize, out int rowCount);
        
        List<StockInOut> GetStockInOut(Func<StockInOut, bool> func, int pageIndex, int pageSize, out int rowCount);

        List<View_InOutDetailProduct> GetViewInOutDetailProduct(Func<View_InOutDetailProduct, bool> func, int pageIndex, int pageSize, out int rowCount);

        ReturnType AddInOutAndDetails(StockInOut stockInOut, PayCharge payChage, List<StockDetail> sdList, List<StockHouseProduct> shpList, List<View_StockItemProduct> vsipList);

        #region View_InOutDetailProduct

        List<View_InOutDetailProduct> GetAllView_InOutDetailProducts();

        List<View_InOutDetailProduct> GetView_InOutDetailProduct(Func<View_InOutDetailProduct, bool> func);

        ReturnType RemoveInOutAndDetails(string inOutCode);

        #endregion
        /*        
        ReturnType RemoveStockInOut(int stockinoutID);
        
        ReturnType RemoveStockInOut(List<int> stockinoutIDList);
        
        ReturnType UpdateStockInOut(int stockinoutID,StockInOut stockinout);
        
        List<StockInOut> GetStockInOut(List<int> stockinoutIDList);
        */

        #region 配货部分

        /// <summary>
        /// 标记为配货并出库
        /// </summary>
        /// <param name="num"></param>
        /// <param name="skuOuterID"></param>
        /// <param name="houseCode"></param>
        /// <param name="layoutCode"></param>
        /// <returns></returns>
        ReturnType AllocationAndOutput(List<StockHouseProduct> houseProductList,string customtid);

        //ReturnType GetOrderItem(List<TradeOrder> tradeOrderList);

        #endregion
    }
}
