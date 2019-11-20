using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 店铺内卖家自定义商品类目
    /// </summary>
    [Serializable]
    [JsonObject("seller_cat")]
    [XmlRoot("seller_cat")]
    public class SellerCat : BaseObject
    {
        /// <summary>
        /// 卖家自定义商品类目编号。
        /// </summary>
        [JsonProperty("cid")]
        [XmlElement("cid")]
        public string Cid { get; set; }

        /// <summary>
        /// 父商品类目编号。
        /// </summary>
        [JsonProperty("parent_cid")]
        [XmlElement("parent_cid")]
        public string ParentCid { get; set; }

        /// <summary>
        /// 卖家自定义商品类目名称。
        /// </summary>
        [JsonProperty("name")]
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// 该类目的链接图片地址。
        /// </summary>
        [JsonProperty("pict_url")]
        [XmlElement("pict_url")]
        public string PicUrl { get; set; }

        /// <summary>
        /// 该类目在页面上的排序位置。
        /// </summary>
        [JsonProperty("sort_order")]
        [XmlElement("sort_order")]
        public int SortOrder { get; set; }
    }
}