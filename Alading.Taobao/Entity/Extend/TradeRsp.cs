using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Alading.Taobao.Entity.Extend
{
    [Serializable]
    [JsonObject]
    public class TradeRsp
    {
        /// <summary>
        /// 搜索到的交易信息总数 
        /// </summary>
        [JsonProperty("total_results")]
        public int TotalResults
        {
            get;
            set;
        }
        /// <summary>
        /// 搜索到的交易信息列表
        /// </summary>
        [JsonProperty("trades")]
        public Trades Trades
        {
            get;
            set;
        }

        /// <summary>
        /// 单个交易信息
        /// </summary>
        [JsonProperty("trade")]
        public Trade Trade
        {
            get;
            set;
        }

        /// <summary>
        /// 单个交易信息
        /// </summary>
        [JsonProperty("refund")]
        public Refund Refund
        {
            get;
            set;
        }

        /// <summary>
        /// 关闭交易时间
        /// </summary
        [JsonProperty("modified")]
        public string Modified
        {
            get;
            set;
        }
        /// <summary>
        /// 交易编号 
        /// </summary>
        [JsonProperty("tid")]
        public string Tid
        {
            get;
            set;
        }
        /// <summary>
        /// 备注添加时间
        /// </summary>
        [JsonProperty("created")]
        public string  Created 
        {
            get;
            set;
        }
        /// <summary>
        /// 搜索到的退款信息列表
        /// </summary>
        [JsonProperty("refunds")]
        public Refunds Refunds 
        {
            get;
            set;
        }
        /// <summary>
        /// 留言凭证编号
        /// </summary>
        [JsonProperty("id")]
        public int  Id 
        {
            get;
            set;
        }
        
        /// <summary>
        /// 搜索到的留言凭证信息列表
        /// </summary>
        [JsonProperty("refund_ressages")]
        public RefundRessages RefundRessages 
        {
            get;
            set;
        } 
        /// <summary>
        /// 子订单编号 
        /// </summary>
        [JsonProperty("oid")]
        public int Oid  
        {
            get;
            set;
        }
     }



    [Serializable]
    [JsonObject]
    public class Trades
    {
        /// <summary>
        /// Trade列表
        /// </summary>
        [JsonProperty("trade")]
        public Trade[] Trade{ get; set; }
    }

    [Serializable]
    [JsonObject]
    public class Refunds 
    {
        /// <summary>
        /// Refund列表
        /// </summary>
        [JsonProperty("refund")]
        public Refund[] Refund{ get; set; }
    }

     public class RefundRessages 
    {
        /// <summary>
        /// Refund_ressage列表
        /// </summary>
         [JsonProperty("refund_ressage")]
        public RefundMessage[] RefundMessage{ get; set; }
    }
}

