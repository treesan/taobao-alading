using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 产品属性图片
    /// </summary>
    [Serializable]
    [JsonObject("productPropImg")]
    [XmlRoot("productPropImg")]
    public class ProductPropImg : BaseObject
    {
        /// <summary>
        /// 产品属性图片编号。
        /// </summary>
        [JsonProperty("pic_id")]
        [XmlElement("pic_id")]
        public string ImgId { get; set; }

        /// <summary>
        /// 图片绝对地址。
        /// </summary>
        [JsonProperty("url")]
        [XmlElement("url")]
        public string ImgUrl { get; set; }

        /// <summary>
        /// 图片所属产品的编号。
        /// </summary>
        [JsonProperty("product_id")]
        [XmlElement("product_id")]
        public string ProductId { get; set; }

        /// <summary>
        /// 属性串。
        /// </summary>
        [JsonProperty("props")]
        [XmlElement("props")]
        public string Props { get; set; }

        /// <summary>
        /// 图片序号。
        /// </summary>
        [JsonProperty("position")]
        [XmlElement("position")]
        public int Position { get; set; }
    }
}
