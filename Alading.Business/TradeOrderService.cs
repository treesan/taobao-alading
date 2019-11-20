using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;
using System.Data;

namespace Alading.Business
{
    public static class TradeOrderService
    {
        //获得数据库交易及订单数据，通过存储过程查询，返回两张表，提高性能
        //如果想查询多个
        public static DataSet GetView_TradeStockDataSet(string localStatus, string status, DateTime startTime, DateTime endTime)
        {
            return DataProviderClass.Instance().GetView_TradeStockDataSet(localStatus, status, startTime,endTime);
        }

        public static DataSet GetView_TradeStockNormalDataSet(string localStatus, string status, DateTime startTime, DateTime endTime)
        {
            return DataProviderClass.Instance().GetView_TradeStockNormalDataSet(localStatus, status, startTime, endTime);
        }

       public static DataSet GetView_TradeDetailItemsDataSet(string customTid)
       {
           return DataProviderClass.Instance().GetView_TradeDetailItemsDataSet(customTid);
       }

        public static ReturnType UpdateTradeOrderPicCode(string tradeOrderCode, string pictureCode)
        {
            return DataProviderClass.Instance().UpdateTradeOrderPicCode(tradeOrderCode,pictureCode);
        }

        public static ReturnType AddTradeOrder(TradeOrder tradeorder)
        {
            return DataProviderClass.Instance().AddTradeOrder(tradeorder);
        }

        public static ReturnType AddTradeOrder(List<TradeOrder> tradeorderList)
        {
            return DataProviderClass.Instance().AddTradeOrder(tradeorderList);
        }
    
        public static ReturnType RemoveAllTradeOrder()
        {
            return DataProviderClass.Instance().RemoveAllTradeOrder();
        }
    
        public static ReturnType RemoveTradeOrder(Func<TradeOrder, bool> func)
        {
            return DataProviderClass.Instance().RemoveTradeOrder(func);
        }
        
        public static ReturnType RemoveTradeOrder(string tradeorderCode)
        {
            return DataProviderClass.Instance().RemoveTradeOrder(tradeorderCode);
        }       
        
        /*
        public static ReturnType RemoveTradeOrder(int tradeorderID)
        {
            return DataProviderClass.Instance().RemoveTradeOrder(tradeorderID);
        }
        */
    
        public static ReturnType RemoveTradeOrder(List<string> tradeorderCodeList)
        {
            return DataProviderClass.Instance().RemoveTradeOrder(tradeorderCodeList);
        }        
        
        /*
        public static ReturnType RemoveTradeOrder(List<int> tradeorderIDList)
        {
            return DataProviderClass.Instance().RemoveTradeOrder(tradeorderIDList);
        }
        */
    
        public static ReturnType UpdateTradeOrder(TradeOrder tradeorder)
        {
            return DataProviderClass.Instance().UpdateTradeOrder(tradeorder);
        }
    
        public static ReturnType UpdateTradeOrder(string tradeorderCode, TradeOrder tradeorder)
        {
            return DataProviderClass.Instance().UpdateTradeOrder(tradeorderCode, tradeorder);
        }
        
        /*
        public static ReturnType UpdateTradeOrder(int tradeorderID, TradeOrder tradeorder)
        {
            return DataProviderClass.Instance().UpdateTradeOrder(tradeorderID, tradeorder);
        }
        */
    
        public static List<TradeOrder> GetAllTradeOrder()
        {
            return DataProviderClass.Instance().GetAllTradeOrder();
        }
    
        public static List<TradeOrder> GetTradeOrder(Func<TradeOrder, bool> func)
        {
            return DataProviderClass.Instance().GetTradeOrder(func);
        }
    
        public static TradeOrder GetTradeOrder(string tradeorderCode)
        {
            return DataProviderClass.Instance().GetTradeOrder(tradeorderCode);
        }
        
        /*
        public static TradeOrder GetTradeOrder(int tradeorderID)
        {
            return DataProviderClass.Instance().GetTradeOrder(tradeorderID);
        }
        */
    
        public static List<TradeOrder> GetTradeOrder(List<string> tradeorderCodeList)
        {
            return DataProviderClass.Instance().GetTradeOrder(tradeorderCodeList);
        }

        public static List<TradeOrder> GetTradeOrderByCTid(List<string> customTidList)//zxl
        {
            return DataProviderClass.Instance().GetTradeOrderByCTid(customTidList);
        }
        /*
        public static List<TradeOrder> GetTradeOrder(List<int> tradeorderIDList)
        {
            return DataProviderClass.Instance().GetTradeOrder(tradeorderIDList);
        }
        */
    
        public static List<TradeOrder> GetTradeOrder(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetTradeOrder(pageIndex, pageSize, out rowCount);
        }
        
        public static List<TradeOrder> GetTradeOrder(Func<TradeOrder, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetTradeOrder(func, pageIndex, pageSize, out rowCount);
        }

    }
}
