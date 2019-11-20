using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class TradeService
    {

        public static ReturnType AddTrade(Trade trade)
        {
            return DataProviderClass.Instance().AddTrade(trade);
        }

        public static ReturnType AddTradeOrderBuyer(Alading.Taobao.Entity.Trade trade)
        {
            return DataProviderClass.Instance().AddTradeOrderBuyer(trade);
        }

        public static ReturnType AddTrade(List<Trade> tradeList)
        {
            return DataProviderClass.Instance().AddTrade(tradeList);
        }

        public static ReturnType RemoveAllTrade()
        {
            return DataProviderClass.Instance().RemoveAllTrade();
        }

        public static ReturnType RemoveTrade(Func<Trade, bool> func)
        {
            return DataProviderClass.Instance().RemoveTrade(func);
        }

        public static ReturnType RemoveTrade(string tradeCode)
        {
            return DataProviderClass.Instance().RemoveTrade(tradeCode);
        }

        /*
        public static ReturnType RemoveTrade(int tradeID)
        {
            return DataProviderClass.Instance().RemoveTrade(tradeID);
        }
        */

        public static ReturnType RemoveTrade(List<string> tradeCodeList)
        {
            return DataProviderClass.Instance().RemoveTrade(tradeCodeList);
        }

        /*
        public static ReturnType RemoveTrade(List<int> tradeIDList)
        {
            return DataProviderClass.Instance().RemoveTrade(tradeIDList);
        }
        */

        public static ReturnType UpdateTrade(Trade trade)
        {
            return DataProviderClass.Instance().UpdateTrade(trade);
        }

        public static ReturnType UpdateTrade(string tradeCode, Trade trade)
        {
            return DataProviderClass.Instance().UpdateTrade(tradeCode, trade);
        }

        public static ReturnType UpdateTrade(List<Trade> tradeList)
        {
            return DataProviderClass.Instance().UpdateTrade(tradeList);
        }

        public static ReturnType UpdateTrade(List<string> CombineTradeList, List<string> TradeCustomList)
        {
            return DataProviderClass.Instance().UpdateTrade(CombineTradeList, TradeCustomList);
        }

        public static ReturnType UpdateTradeLocalStatus(string CustomTid, string LocalStatus)
        {
            return DataProviderClass.Instance().UpdateTradeLocalStatus(CustomTid, LocalStatus);
        }

        public static ReturnType UpdateTradeLocalStatus(List<string> customTidList, string LocalStatus)
        {
            return DataProviderClass.Instance().UpdateTradeLocalStatus(customTidList, LocalStatus);
        }

        public static ReturnType UpdateTradeLocalStatus(string customTidList, string LocalStatus, string shippingCode)
        {
            return DataProviderClass.Instance().UpdateTradeLocalStatus(customTidList, LocalStatus, shippingCode);
        }

        public static ReturnType UpdateTradeLocalStatus(string userCode, string userName, List<string> customTidList, string LocalStatus, List<TradeOrder> OrderList)
        {
            return DataProviderClass.Instance().UpdateTradeLocalStatus(userCode, userName, customTidList, LocalStatus, OrderList);
        }
        /*
        public static ReturnType UpdateTrade(int tradeID, Trade trade)
        {
            return DataProviderClass.Instance().UpdateTrade(tradeID, trade);
        }
        */

        public static List<Trade> GetAllTrade()
        {
            return DataProviderClass.Instance().GetAllTrade();
        }

        public static List<Trade> GetTrade(Func<Trade, bool> func)
        {
            return DataProviderClass.Instance().GetTrade(func);
        }

        public static Trade GetTrade(string CustomTid)
        {
            return DataProviderClass.Instance().GetTrade(CustomTid);
        }

        /*
        public static Trade GetTrade(int tradeID)
        {
            return DataProviderClass.Instance().GetTrade(tradeID);
        }
        */

        public static List<Trade> GetTrade(List<string> customTidList)
        {
            return DataProviderClass.Instance().GetTrade(customTidList);
        }

        /*
        public static List<Trade> GetTrade(List<int> tradeIDList)
        {
            return DataProviderClass.Instance().GetTrade(tradeIDList);
        }
        */

        public static List<Trade> GetTrade(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetTrade(pageIndex, pageSize, out rowCount);
        }

        public static List<Trade> GetTradesByStatus(string localStatus, string status)
        {
            return DataProviderClass.Instance().GetTradesByStatus(localStatus, status);
        }

        public static List<Trade> GetTrade(Func<Trade, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetTrade(func, pageIndex, pageSize, out rowCount);
        }

        public static ReturnType TradeOrderConsumer(List<Alading.Entity.Trade> tradeAddFullInfoList, List<Alading.Entity.Trade> tradeUpdateFullInfoList
           , List<Alading.Entity.TradeOrder> orderAddList, List<Alading.Entity.TradeOrder> orderUpdateList
           , List<Alading.Entity.Consumer> consumerAddList, List<Alading.Entity.Consumer> consumerUpdateList, List<string> customtidList)
        {
            return DataProviderClass.Instance().TradeOrderConsumer(tradeAddFullInfoList, tradeUpdateFullInfoList, orderAddList, orderUpdateList
                , consumerAddList, consumerUpdateList, customtidList);
        }

        public static List<Trade> GetTrade(string status, bool seller_rate)
        {
            return DataProviderClass.Instance().GetTrade(status, seller_rate);
        }

        //通过传CustomTid的方法在存储过程中实现交易的提交
        public static int SummitCommonTrade(string customTid, string oldStatus, string newStatus, string lockUser)
        {
            return DataProviderClass.Instance().SummitCommonTrade(customTid, oldStatus, newStatus, lockUser);
        }

        //通过传CustomTid的方法在存储过程中实现交易的取消
        public static int RebackCommonTrade(string customTid, string oldStatus, string newStatus)
        {
            return DataProviderClass.Instance().RebackCommonTrade(customTid, oldStatus, newStatus);
        }

        public static int SummitCombineTrade(string customTidList, string oldStatus, string combineCode, string specialStatus, string lockUser)
        {
            return DataProviderClass.Instance().SummitCombineTrade(customTidList, oldStatus, combineCode, specialStatus, lockUser);
        }

        /// <summary>
        /// ReturnValue:1:更新成功  2:物流单号已经被占用 3：交易状态已经改变
        /// </summary>
        //通过传CustomTid的方法在存储过程中实现交易的提交
        public static int SummitShippingCode(string customTid, string oldStatus, string newStatus, string shippingCode)
        {
            return DataProviderClass.Instance().SummitShippingCode(customTid, oldStatus, newStatus, shippingCode);
        }
    }
}
