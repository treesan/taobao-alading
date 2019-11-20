using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class TradeRefundService
    {

        public static ReturnType AddTradeRefund(TradeRefund traderefund)
        {
            return DataProviderClass.Instance().AddTradeRefund(traderefund);
        }

        public static ReturnType AddTradeRefund(List<TradeRefund> traderefundList)
        {
            return DataProviderClass.Instance().AddTradeRefund(traderefundList);
        }
    
        public static ReturnType RemoveAllTradeRefund()
        {
            return DataProviderClass.Instance().RemoveAllTradeRefund();
        }
    
        public static ReturnType RemoveTradeRefund(Func<TradeRefund, bool> func)
        {
            return DataProviderClass.Instance().RemoveTradeRefund(func);
        }
        
        public static ReturnType RemoveTradeRefund(string traderefundCode)
        {
            return DataProviderClass.Instance().RemoveTradeRefund(traderefundCode);
        }       
        
        /*
        public static ReturnType RemoveTradeRefund(int traderefundID)
        {
            return DataProviderClass.Instance().RemoveTradeRefund(traderefundID);
        }
        */
    
        public static ReturnType RemoveTradeRefund(List<string> traderefundCodeList)
        {
            return DataProviderClass.Instance().RemoveTradeRefund(traderefundCodeList);
        }        
        
        /*
        public static ReturnType RemoveTradeRefund(List<int> traderefundIDList)
        {
            return DataProviderClass.Instance().RemoveTradeRefund(traderefundIDList);
        }
        */
    
        public static ReturnType UpdateTradeRefund(TradeRefund traderefund)
        {
            return DataProviderClass.Instance().UpdateTradeRefund(traderefund);
        }
    
        public static ReturnType UpdateTradeRefund(string traderefundCode, TradeRefund traderefund)
        {
            return DataProviderClass.Instance().UpdateTradeRefund(traderefundCode, traderefund);
        }

        public static ReturnType UpdateTradeRefund(List<TradeRefund> tradeRefundList)
        {
            return DataProviderClass.Instance().UpdateTradeRefund(tradeRefundList);
        }

        public static ReturnType UpdateTradeRefund(List<TradeRefund> tradeRefundList, List<StockProduct> stockProductList
            , List<StockHouseProduct> houseProList, List<StockInOut> stockInOutList, List<StockDetail> stockDetailList, PayCharge payCharge
            , List<string> refundIdList, List<string> outerSkuIdList, List<string> outerIdList)
        {
            return DataProviderClass.Instance().UpdateTradeRefund(tradeRefundList, stockProductList,houseProList, stockInOutList, stockDetailList
                ,payCharge, refundIdList, outerSkuIdList, outerIdList);
        }
        
        /*
        public static ReturnType UpdateTradeRefund(int traderefundID, TradeRefund traderefund)
        {
            return DataProviderClass.Instance().UpdateTradeRefund(traderefundID, traderefund);
        }
        */
    
        public static List<TradeRefund> GetAllTradeRefund()
        {
            return DataProviderClass.Instance().GetAllTradeRefund();
        }
    
        public static List<TradeRefund> GetTradeRefund(Func<TradeRefund, bool> func)
        {
            return DataProviderClass.Instance().GetTradeRefund(func);
        }
    
        public static TradeRefund GetTradeRefund(string traderefundCode)
        {
            return DataProviderClass.Instance().GetTradeRefund(traderefundCode);
        }
        
        /*
        public static TradeRefund GetTradeRefund(int traderefundID)
        {
            return DataProviderClass.Instance().GetTradeRefund(traderefundID);
        }
        */
    
        public static List<TradeRefund> GetTradeRefund(List<string> traderefundCodeList)
        {
            return DataProviderClass.Instance().GetTradeRefund(traderefundCodeList);
        }
        
        /*
        public static List<TradeRefund> GetTradeRefund(List<int> traderefundIDList)
        {
            return DataProviderClass.Instance().GetTradeRefund(traderefundIDList);
        }
        */
    
        public static List<TradeRefund> GetTradeRefund(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetTradeRefund(pageIndex, pageSize, out rowCount);
        }
        
        public static List<TradeRefund> GetTradeRefund(Func<TradeRefund, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetTradeRefund(func, pageIndex, pageSize, out rowCount);
        }

        public static List<View_RefundTradeStock> GetRefundTradeStock(List<string> oidList)
        {
            return DataProviderClass.Instance().GetRefundTradeStock(oidList);
        }

        public static List<View_RefundTradeStock> GetTradeRefundByView(Func<View_RefundTradeStock, bool> func)
        {
            return DataProviderClass.Instance().GetTradeRefundByView(func);
        }
    }
}
