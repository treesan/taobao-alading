using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 运费方式模板收费方式
    /// </summary>
    [Serializable]
    [JsonObject("postageMode")]
    [XmlRoot("postageMode")]
    public class PostageMode : BaseObject
    {
        /// <summary>
        /// 运费模板编号
        /// </summary>
        [JsonProperty("postage_id")]
        [XmlElement("mode_postage_id")]
        public string PostageId { get; set; }

        /// <summary>
        /// 运费方式项编号
        /// </summary>
        [JsonProperty("id")]
        [XmlElement("id")]
        public string Id { get; set; }

        /// <summary>
        /// 运费方式
        /// </summary>
        [JsonProperty("type")]
        [XmlElement("type")]
        public string Type { get; set; }

        /// <summary>
        /// 邮费子项涉及的地区，多个地区用逗号连接数量串
        /// </summary>
        [JsonProperty("dests")]
        [XmlElement("dests")]
        public string Dests { get; set; }

        /// <summary>
        /// 运费单价
        /// </summary>
        [JsonProperty("price")]
        [XmlElement("price")]
        public string Price { get; set; }

        /// <summary>
        /// 运费增价
        /// </summary>
        [JsonProperty("increase")]
        [XmlElement("increase")]
        public string Increase { get; set; }
    }
}
