using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 属性图片
    /// </summary>
    [Serializable]
    [JsonObject("propImg")]
    [XmlRoot("propImg")]
    public class PropImg : BaseObject
    {
        /// <summary>
        /// 属性图片的编号
        /// </summary>
        [JsonProperty("propimg_id")]
        [XmlElement("propimg_id")]
        public string ImgId { get; set; }

        /// <summary>
        /// 属性图片链接地址
        /// </summary>
        [JsonProperty("url")]
        [XmlElement("url")]
        public string ImgUrl { get; set; }

        /// <summary>
        /// 属性图片所对应的属性组合的字符串
        /// </summary>
        [JsonProperty("properties")]
        [XmlElement("properties")]
        public string Props { get; set; }

        /// <summary>
        /// 属性图片放在第几张（多图时可设置）
        /// </summary>
        [JsonProperty("position")]
        [XmlElement("position")]
        public int Position { get; set; }
    }
}
