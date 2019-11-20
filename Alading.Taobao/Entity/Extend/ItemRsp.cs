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
    public class ItemRsp
    {
        /// <summary>
        ///  记录总数
        /// </summary>
        [JsonProperty("total_results")]
        public int TotalResults
        {   
            get; set;
        }
        
        /// <summary>
        /// 返回所有的items
        /// </summary>
        [JsonProperty("items")]
        public Items Items
        {
            get;
            set;
        }

        /// <summary>
        /// 返回单个item
        /// </summary>
        [JsonProperty("item")]
        public Item Item
        {
            get;
            set;
        }

        /// <summary>
        /// 返回单个item_img
        /// </summary>
        [JsonProperty("item_img")]
        public ItemImg ItemImg
        {
            get;
            set;
        }
        /// <summary>
        /// 返回所有的skus
        /// </summary>
        [JsonProperty("skus")]
        public Skus Skus
        {
            get;
            set;
        }

        /// <summary>
        /// 单个Sku
        /// </summary>
        [JsonProperty("sku")]
        public Sku Sku { get; set; }

        /// <summary>
        /// 返回所有的postages
        /// </summary>
        [JsonProperty("postages")]
        public Postages Postages
        {
            get;
            set;
        }

        /// <summary>
        /// 返回postage
        /// </summary>
        [JsonProperty("postage")]
        public Postage Postage
        {
            get;
            set;
        }

        /// <summary>
        ///  搜索到的商品，具体字段根据权限和设定的fields决定
        /// </summary>
        [JsonProperty("item_search")]
        public ItemSearch ItemSearch
        {
            get;
            set;
        }
    }




    [Serializable]
    [JsonObject]
    public class Items
    {
        /// <summary>
        /// Item列表
        /// </summary>
        [JsonProperty("item")]
        public Item[] Item 
        { 
            get;
            set;
        }
       
        
    }

    [Serializable]
    [JsonObject]
    public class Skus
    {
        /// <summary>
        /// Sku列表
        /// </summary>
        [JsonProperty("sku")]
        public Sku[] Sku { get; set; }
    }

    [Serializable]
    [JsonObject]
    public class Postages
    {
        /// <summary>
        /// Postages列表
        /// </summary>
        [JsonProperty("postage")]
        public Postage[] Postage { get; set; }
    }
}
