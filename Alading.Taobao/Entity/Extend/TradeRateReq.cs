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
       public class TradeRateReq:TradeRate
      {
        /// <summary>
        /// 评价类型。可选值:get(得到),give(给出) 
        /// </summary>
       [JsonProperty("rate_type")]
       [XmlElement("rate_type")]
       public string RateType 
       { 
           get; 
           set; 
       }

       /// <summary>
       /// 页码。取值范围:大于零的整数; 默认值:1 
       /// </summary>
       [JsonProperty("page_no")]
       [XmlElement("page_no")]
       public int PageNo 
       { 
           get; 
           set; 
       }
       /// <summary>
       /// 每页条数。取值范围:大于零的整数; 默认值:40;最大值:200 
       /// </summary>
       [JsonProperty("page_size")]
       [XmlElement("page_size")]
       public int PageSize 
       { 
           get;
           set; 
       }
       /// <summary>
       /// 是否匿名，卖家评不能匿名。可选值:true(匿名),false(非匿名)。 注意：输入非可选值将会自动转为false； 
       /// </summary>
       [JsonProperty("anony")]
       [XmlElement("anony")]
       public string Anony
       { 
           get; 
           set; 
       }
       /// <summary>
       /// 子订单ID
       /// </summary>
       [JsonProperty("oid")]
       [XmlElement("oid")]
       public string OrderId
       {
           get;
           set;
       }
    }
}
