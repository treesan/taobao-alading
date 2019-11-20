using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Alading.Taobao.Entity;
using Alading.Taobao.Entity.Extend;
using Alading.Taobao.API.Common;

namespace Alading.Taobao.API
{
     public partial class TopService 
    {
         /// <summary>
         /// rate_type,role必须
        /// 搜索评价信息
         /// </summary>
         public static TradeRateRsp TradeRatesGet(string session, TradeRateReq traderatereq)
         {
             try
             {
                 TopDictionary paramsTable = new TopDictionary();
                 paramsTable.Add("method", "taobao.traderates.get");
                 paramsTable.Add("fields", "content,tid,oid,role,created,item_price,nick,item_title,reated_nick,result,reply");   
                 paramsTable.Add("rate_type", traderatereq.RateType);
                 paramsTable.Add("role", traderatereq.Role);             
                 paramsTable.Add("result", traderatereq.Result);
                 paramsTable.Add("page_no", traderatereq.PageNo);
                 paramsTable.Add("page_size", traderatereq.PageSize);
                 paramsTable.Add("session", session);
                 return TopUtils.DeserializeObject<TradeRateRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
             }
             catch (System.Exception ex)
             {
                 throw ex;
             }
            
         
         }
         /// <summary>
         /// 新增单个评价
         ///tid,result,role必须
         /// </summary>
         public static TradeRateRsp TradeRateAdd(string session, TradeRateReq traderatereq)
         {
             try
             {
                 TopDictionary paramsTable = new TopDictionary();
                 paramsTable.Add("method", "taobao.traderate.add");
                 paramsTable.Add("tid", traderatereq.Tid);
                 paramsTable.Add("oid", traderatereq.OrderId);
                 paramsTable.Add("result", traderatereq.Result);
                 paramsTable.Add("role", traderatereq.Role);
                 paramsTable.Add("content", traderatereq.Content);
                 paramsTable.Add("anony", traderatereq.Anony);
                 paramsTable.Add("session", session);
                 return TopUtils.DeserializeObject<TradeRateRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
             }
             catch (System.Exception ex)
             {
                 throw ex;
             }
             
         }
         /// <summary>
         /// 针对父子订单新增批量评价
         /// tid,oid,role必须
         /// </summary>
         public static TradeRateRsp TradeRateListAdd(string session, TradeRateReq traderatereq)
         {
             try
             {
                 TopDictionary paramsTable = new TopDictionary();
                 paramsTable.Add("method", "taobao.traderate.list.add");
                 paramsTable.Add("tid", traderatereq.Tid);
                 paramsTable.Add("role", traderatereq.Role);
                 paramsTable.Add("content", traderatereq.Content);
                 paramsTable.Add("anony", traderatereq.Anony);
                 paramsTable.Add("result", traderatereq.Result);
                 paramsTable.Add("session", session);
                 return TopUtils.DeserializeObject<TradeRateRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
             }
             catch (System.Exception ex)
             {
                 throw ex;
             }
             
         }
    }
}
