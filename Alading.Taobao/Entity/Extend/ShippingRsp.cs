using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Alading.Taobao.Entity.Extend
{
    [Serializable]
    [JsonObject]
    public class ShippingRsp
    {
        /// <summary>
        /// 返回所有的areas
        /// </summary>
        [JsonProperty("areas")]
        public Areas Areas
        {
            get;
            set;
        }
        /// <summary>
        /// 返回所有的物流公司信息
        /// </summary>
        [JsonProperty("logistics_companies")]
        public LogisticsCompanys LogisticsCompanys
        {
            get;
            set;
        }
        /// <summary>
        ///  搜索到的物流订单列表总数
        /// </summary>
        [JsonProperty("total_results")]
        public int TotalResults
        {
            get;
            set;
        }
        /// <summary>
        /// 返回获取的物流订单详情列表 
        /// </summary>
        [JsonProperty("ship")]
        public Ship Ship
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("shippings")]
        public Shippings Shippings
        {
            get;
            set;
        }
    }


    [Serializable]
    [JsonObject]
    public class Areas
    {
        /// <summary>
        /// Area列表
        /// </summary>
        [JsonProperty("area")]
        public Area[] Area{ get; set; }
    }

  
  
      
   

    [Serializable]
    [JsonObject]
    public class Ship
    {
        /// <summary>
        /// 返回获取的物流订单详情列表 
        /// </summary>
        [JsonProperty("ship")]
        public Shipping[] Shipping { get; set; }
    }

    [Serializable]
    [JsonObject]
    public class Shippings
    {
        /// <summary>
        /// 返回获取的物流订单详情列表 
        /// </summary>
        [JsonProperty("shippings")]
        public Shipping[] Shipping { get; set; }
    }

     public class LogisticsCompanys
     {
         [JsonProperty("logistics_company")]
         public LogisticsCompany[] LogisticsCompany { get; set; }
     }
}
