using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 商品图片
    /// </summary>
    [Serializable]
    [JsonObject("itemImg")]
    [XmlRoot("itemImg")]
    public class ItemImg : BaseObject
    {
        /// <summary>
        /// 商品图片的编号
        /// </summary>
        [JsonProperty("id")]
        [XmlElement("id")]
        public string Id { get; set; }

        /// <summary>
        /// 商品图片链接地址
        /// </summary>
        [JsonProperty("url")]
        [XmlElement("url")]
        public string ImgUrl { get; set; }

        /// <summary>
        /// 图片放在第几张(多图时可设置)
        /// </summary>
        [JsonProperty("position")]
        [XmlElement("position")]
        public int Position { get; set; }
    }
}
