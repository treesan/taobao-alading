using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class StockDetailService
    {

        public static ReturnType AddStockDetail(StockDetail stockdetail)
        {
            return DataProviderClass.Instance().AddStockDetail(stockdetail);
        }

        public static ReturnType AddStockDetail(List<StockDetail> stockdetailList)
        {
            return DataProviderClass.Instance().AddStockDetail(stockdetailList);
        }
    
        public static ReturnType RemoveAllStockDetail()
        {
            return DataProviderClass.Instance().RemoveAllStockDetail();
        }
    
        public static ReturnType RemoveStockDetail(Func<StockDetail, bool> func)
        {
            return DataProviderClass.Instance().RemoveStockDetail(func);
        }
        
        public static ReturnType RemoveStockDetail(string stockdetailCode)
        {
            return DataProviderClass.Instance().RemoveStockDetail(stockdetailCode);
        }       
        
        /*
        public static ReturnType RemoveStockDetail(int stockdetailID)
        {
            return DataProviderClass.Instance().RemoveStockDetail(stockdetailID);
        }
        */
    
        public static ReturnType RemoveStockDetail(List<string> stockdetailCodeList)
        {
            return DataProviderClass.Instance().RemoveStockDetail(stockdetailCodeList);
        }        
        
        /*
        public static ReturnType RemoveStockDetail(List<int> stockdetailIDList)
        {
            return DataProviderClass.Instance().RemoveStockDetail(stockdetailIDList);
        }
        */
    
        public static ReturnType UpdateStockDetail(StockDetail stockdetail)
        {
            return DataProviderClass.Instance().UpdateStockDetail(stockdetail);
        }
    
        public static ReturnType UpdateStockDetail(string stockdetailCode, StockDetail stockdetail)
        {
            return DataProviderClass.Instance().UpdateStockDetail(stockdetailCode, stockdetail);
        }
        
        /*
        public static ReturnType UpdateStockDetail(int stockdetailID, StockDetail stockdetail)
        {
            return DataProviderClass.Instance().UpdateStockDetail(stockdetailID, stockdetail);
        }
        */
    
        public static List<StockDetail> GetAllStockDetail()
        {
            return DataProviderClass.Instance().GetAllStockDetail();
        }
    
        public static List<StockDetail> GetStockDetail(Func<StockDetail, bool> func)
        {
            return DataProviderClass.Instance().GetStockDetail(func);
        }
    
        public static StockDetail GetStockDetail(string stockdetailCode)
        {
            return DataProviderClass.Instance().GetStockDetail(stockdetailCode);
        }
        
        /*
        public static StockDetail GetStockDetail(int stockdetailID)
        {
            return DataProviderClass.Instance().GetStockDetail(stockdetailID);
        }
        */
    
        public static List<StockDetail> GetStockDetail(List<string> stockdetailCodeList)
        {
            return DataProviderClass.Instance().GetStockDetail(stockdetailCodeList);
        }
        
        /*
        public static List<StockDetail> GetStockDetail(List<int> stockdetailIDList)
        {
            return DataProviderClass.Instance().GetStockDetail(stockdetailIDList);
        }
        */
    
        public static List<StockDetail> GetStockDetail(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockDetail(pageIndex, pageSize, out rowCount);
        }
        
        public static List<StockDetail> GetStockDetail(Func<StockDetail, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockDetail(func, pageIndex, pageSize, out rowCount);
        }

        #region 历史明细部分

        public static List<HistoryStockDetail> GetHistoryDetail(string inoutCode)
        {
            return DataProviderClass.Instance().GetHistoryDetail(inoutCode);
        }

        public static List<HistoryStockDetail> GetHistoryDetail(Func<HistoryStockDetail, bool> func)
        {
            return DataProviderClass.Instance().GetHistoryDetail(func);
        }

        #endregion

        #region 盘点部分

        public static ReturnType AddStockCheck(StockCheck stockCheck)
        {
            return DataProviderClass.Instance().AddStockCheck(stockCheck);
        }

        public static ReturnType Check(int num, List<StockDetail> stockDetailList, StockDetail checkDetail, StockCheckDetail stockCheckDetail)
        {
            return DataProviderClass.Instance().Check(num, stockDetailList, checkDetail, stockCheckDetail);
        }

        #endregion
    }
}
