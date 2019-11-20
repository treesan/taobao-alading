using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 地址区域结构
    /// </summary>
    [Serializable]
    [JsonObject("area")]
    [XmlRoot("area")]
    public class Area : BaseObject
    {
        /// <summary>
        /// 标准行政区域代码
        /// </summary>
        [JsonProperty("id")]
        [XmlElement("id")]
        public string AreaId { get; set; }

        /// <summary>
        /// 区域类型
        /// </summary>
        [JsonProperty("type")]
        [XmlElement("type")]
        public string AreaType { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        [JsonProperty("name")]
        [XmlElement("name")]
        public string AreaName { get; set; }

        /// <summary>
        /// 父节点区域标识
        /// </summary>
        [JsonProperty("parent_id")]
        [XmlElement("parent_id")]
        public string ParentId { get; set; }

        /// <summary>
        /// 具体一个地区的邮编
        /// </summary>
        [JsonProperty("zip")]
        [XmlElement("zip")]
        public string Zip { get; set; }
    }
}
