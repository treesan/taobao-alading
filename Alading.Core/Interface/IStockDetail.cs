using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IStockDetail
    {       
        ReturnType AddStockDetail(StockDetail stockdetail);
       
        ReturnType AddStockDetail(List<StockDetail> stockdetailList);
        
        ReturnType RemoveAllStockDetail();
       
        ReturnType RemoveStockDetail(Func<StockDetail, bool> func);
              
        ReturnType RemoveStockDetail(string stockdetailCode);
        
        ReturnType RemoveStockDetail(List<string> stockdetailCodeList);
       
        ReturnType UpdateStockDetail(StockDetail stockdetail);
       
        ReturnType UpdateStockDetail(string stockdetailCode,StockDetail stockdetail);
       
        List<StockDetail> GetAllStockDetail();
      
        List<StockDetail> GetStockDetail(Func<StockDetail, bool> func);
      
        List<StockDetail> GetStockDetail(List<string> stockdetailCodeList);
       
        List<StockDetail> GetStockDetail(int pageIndex, int pageSize, out int rowCount);
        
        List<StockDetail> GetStockDetail(Func<StockDetail, bool> func, int pageIndex, int pageSize, out int rowCount);

        #region 历史明细

        List<HistoryStockDetail> GetHistoryDetail(string inoutCode);

        List<HistoryStockDetail> GetHistoryDetail(Func<HistoryStockDetail, bool> func);

        #endregion

        /*        
        ReturnType RemoveStockDetail(int stockdetailID);
        
        ReturnType RemoveStockDetail(List<int> stockdetailIDList);
        
        ReturnType UpdateStockDetail(int stockdetailID,StockDetail stockdetail);
        
        List<StockDetail> GetStockDetail(List<int> stockdetailIDList);
        */

        #region 盘点部分

        ReturnType AddStockCheck(StockCheck stockCheck);

        ReturnType Check(int num, List<StockDetail> stockDetailList, StockDetail checkDetail, StockCheckDetail stockCheckDetail);

        #endregion
    }
}
