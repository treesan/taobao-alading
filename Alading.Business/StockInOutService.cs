using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class StockInOutService
    {
        public static ReturnType RemoveStockInOutDetail(string stockInOutCode)
        {
            return DataProviderClass.Instance().RemoveStockInOutDetail(stockInOutCode);
        }
        public static List<View_StockDetailInOut> GetStockDetailInOut(Func<View_StockDetailInOut, bool> func)
        {
            return DataProviderClass.Instance().GetStockDetailInOut(func);
        }
        public static ReturnType AddStockInOutDetail(List<Alading.Entity.StockInOut> stockInOutList, List<StockDetail> stockDetailList)
        {
            return DataProviderClass.Instance().AddStockInOutDetail(stockInOutList, stockDetailList);
        }
        public static ReturnType AddStockInOut(StockInOut stockinout)
        {
            return DataProviderClass.Instance().AddStockInOut(stockinout);
        }

        public static ReturnType AddStockInOut(List<StockInOut> stockinoutList)
        {
            return DataProviderClass.Instance().AddStockInOut(stockinoutList);
        }
    
        public static ReturnType RemoveAllStockInOut()
        {
            return DataProviderClass.Instance().RemoveAllStockInOut();
        }
    
        public static ReturnType RemoveStockInOut(Func<StockInOut, bool> func)
        {
            return DataProviderClass.Instance().RemoveStockInOut(func);
        }
        
        public static ReturnType RemoveStockInOut(string stockinoutCode)
        {
            return DataProviderClass.Instance().RemoveStockInOut(stockinoutCode);
        }       
        
        /*
        public static ReturnType RemoveStockInOut(int stockinoutID)
        {
            return DataProviderClass.Instance().RemoveStockInOut(stockinoutID);
        }
        */
    
        public static ReturnType RemoveStockInOut(List<string> stockinoutCodeList)
        {
            return DataProviderClass.Instance().RemoveStockInOut(stockinoutCodeList);
        }        
        
        /*
        public static ReturnType RemoveStockInOut(List<int> stockinoutIDList)
        {
            return DataProviderClass.Instance().RemoveStockInOut(stockinoutIDList);
        }
        */
    
        public static ReturnType UpdateStockInOut(StockInOut stockinout)
        {
            return DataProviderClass.Instance().UpdateStockInOut(stockinout);
        }
    
        public static ReturnType UpdateStockInOut(string stockinoutCode, StockInOut stockinout)
        {
            return DataProviderClass.Instance().UpdateStockInOut(stockinoutCode, stockinout);
        }
        
        /*
        public static ReturnType UpdateStockInOut(int stockinoutID, StockInOut stockinout)
        {
            return DataProviderClass.Instance().UpdateStockInOut(stockinoutID, stockinout);
        }
        */
    
        public static List<StockInOut> GetAllStockInOut()
        {
            return DataProviderClass.Instance().GetAllStockInOut();
        }
    
        public static List<StockInOut> GetStockInOut(Func<StockInOut, bool> func)
        {
            return DataProviderClass.Instance().GetStockInOut(func);
        }
    
        public static StockInOut GetStockInOut(string stockinoutCode)
        {
            return DataProviderClass.Instance().GetStockInOut(stockinoutCode);
        }
        
        /*
        public static StockInOut GetStockInOut(int stockinoutID)
        {
            return DataProviderClass.Instance().GetStockInOut(stockinoutID);
        }
        */
    
        public static List<StockInOut> GetStockInOut(List<string> stockinoutCodeList)
        {
            return DataProviderClass.Instance().GetStockInOut(stockinoutCodeList);
        }
        
        /*
        public static List<StockInOut> GetStockInOut(List<int> stockinoutIDList)
        {
            return DataProviderClass.Instance().GetStockInOut(stockinoutIDList);
        }
        */
    
        public static List<StockInOut> GetStockInOut(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockInOut(pageIndex, pageSize, out rowCount);
        }
        
        public static List<StockInOut> GetStockInOut(Func<StockInOut, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockInOut(func, pageIndex, pageSize, out rowCount);
        }

        public static List<View_InOutDetailProduct> GetViewInOutDetailProduct(Func<View_InOutDetailProduct, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetViewInOutDetailProduct(func, pageIndex, pageSize, out rowCount);
        }

        public static ReturnType AddInOutAndDetails(StockInOut stockInOut, PayCharge payChage, List<StockDetail> sdList, List<StockHouseProduct> shpList, List<View_StockItemProduct> vsipList)
        {
            return DataProviderClass.Instance().AddInOutAndDetails(stockInOut,payChage, sdList, shpList, vsipList);
        }

        #region View_InOutDetailProduct

        public static List<View_InOutDetailProduct> GetAllView_InOutDetailProducts()
        {
            return DataProviderClass.Instance().GetAllView_InOutDetailProducts();
        }

        public static ReturnType RemoveInOutAndDetails(string inOutCode)
        {
            return DataProviderClass.Instance().RemoveInOutAndDetails(inOutCode);
        }

        public static List<View_InOutDetailProduct> GetView_InOutDetailProduct(Func<View_InOutDetailProduct, bool> func)
        {
            return DataProviderClass.Instance().GetView_InOutDetailProduct(func);
        }

        #endregion

        public static ReturnType AllocationAndOutput(List<StockHouseProduct> houseProductList, string customtid)
        {
            return DataProviderClass.Instance().AllocationAndOutput(houseProductList, customtid);
        }
    }
}
