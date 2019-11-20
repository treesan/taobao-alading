using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 用户地址
    /// </summary>
    [Serializable]
    [JsonObject("location")]
    [XmlRoot("location")]
    public class Location : BaseObject
    {
        /// <summary>
        /// 邮政编码
        /// </summary>
        [JsonProperty("zip")]
        [XmlElement("zip")]
        public string Zip { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [JsonProperty("address")]
        [XmlElement("address")]
        public string Address { get; set; }

        /// <summary>
        /// 所在城市（中文名称）
        /// </summary>
        [JsonProperty("city")]
        [XmlElement("city")]
        public string City { get; set; }

        /// <summary>
        /// 所在省份（中文名称）
        /// </summary>
        [JsonProperty("state")]
        [XmlElement("state")]
        public string State { get; set; }

        /// <summary>
        /// 国家名称
        /// </summary>
        [JsonProperty("country")]
        [XmlElement("country")]
        public string Country { get; set; }

        /// <summary>
        /// 区/县（只适用于物流API）
        /// </summary>
        [JsonProperty("district")]
        [XmlElement("district")]
        public string District { get; set; }
    }
}
