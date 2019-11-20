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
    public class TradeRateRsp
    {
        /// <summary>
        /// 评价总列表
        /// </summary>
      [JsonProperty("rates")]
      public Rates Rates
        {
            get;
            set;
        }
      /// <summary>
      ///  搜索到的评价总数
      /// </summary>
      [JsonProperty("totalResults")]
      public int TotalResults
      {
          get;
          set;
      }
        /// <summary>
      /// 交易ID
        /// </summary>
      [JsonProperty("tid")]
      public string Tid
      {
          get;
          set;
      }
        /// <summary>
      /// 子订单ID 
        /// </summary>
      [JsonProperty("oid")]
      public string Oid
      {
          get;
          set;
      }
        /// <summary>
      /// 评价时间
        /// </summary>
      [JsonProperty("created")]
      public string Created
      {
          get;
          set;
      }

    }
    [Serializable]
    [JsonObject]
    public class Rates
    {
        /// <summary>
        /// 评价列表
        /// </summary>
        [JsonProperty("rate")]
        public TradeRate[] TradeRate 
        
        {
            get; 
            set; 
        }
    }

    
}

