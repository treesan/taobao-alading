using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 产品销售属性
    /// </summary>
    [Serializable]
    [JsonObject("sku")]
    [XmlRoot("sku")]
    public class Sku : BaseObject
    {
        /// <summary>
        /// SKU的编号
        /// </summary>
        [JsonProperty("sku_id")]
        [XmlElement("sku_id")]
        public string SkuId { get; set; }

        /// <summary>
        /// SKU所属商品编号
        /// </summary>
        [JsonProperty("iid")]
        [XmlElement("iid")]
        public string Iid { get; set; }

        /// <summary>
        /// SKU的销售属性组合字符串
        /// </summary>
        [JsonProperty("properties")]
        [XmlElement("properties")]
        public string SkuProps { get; set; }

        /// <summary>
        /// 属于这个SKU的商品的数量
        /// </summary>
        [JsonProperty("quantity")]
        [XmlElement("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// 属于这个SKU的商品的价格取值范围
        /// </summary>
        [JsonProperty("price")]
        [XmlElement("price")]
        public string Price { get; set; }

        /// <summary>
        /// 商家设置的外部编号
        /// </summary>
        [JsonProperty("outer_id")]
        [XmlElement("outer_id")]
        public string OuterId { get; set; }

        /// <summary>
        /// SKU状态
        /// </summary>
        [JsonProperty("status")]
        [XmlElement("status")]
        public string Status { get; set; }
    }
}
