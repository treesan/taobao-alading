using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Business;
using Alading.Core.Enum;

namespace Alading.Forms.Trade
{
    /// <summary>
    /// 订单模块为其他模块提供的接口，外界通过调用接口函数来获得数据
    /// </summary>
    public static class TradeCooperateHelper
    {
        /// <summary>
        /// 获得当前的打印列表，并以List<TradePrintTee>形式返回
        /// </summary>
        /// <returns>打印列表</returns>
        public static  List<TradePrintTee> GetPrintList()
        {
            List<TradePrintTee> printList = new List<TradePrintTee>();
            string localStatus = LocalTradeStatus.SummitNotPrint;
            string status        = TradeEnum.WAIT_SELLER_SEND_GOODS;
            foreach(Alading.Entity.Trade trade in TradeService.GetTrade(p=>p.status==localStatus&&p.LocalStatus == status))
            {
                printList.Add(new TradePrintTee(trade));//新建一个打印专用类,并加入到PrintList   List<TradePrintTee>
            }
            return printList;
        }
    }
}
