using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 物流公司结构
    /// </summary>
    [Serializable]
    [JsonObject("logistic_company")]
    [XmlRoot("logistic_company")]
    public class LogisticsCompany : BaseObject
    {
        /// <summary>
        /// 物流公司标识
        /// </summary>
        [JsonProperty("id")]
        [XmlElement("id")]
        public string CompanyId { get; set; }

        /// <summary>
        /// 物流公司代码
        /// </summary>
        [JsonProperty("code")]
        [XmlElement("code")]
        public string CompanyCode { get; set; }

        /// <summary>
        /// 物流公司简称
        /// </summary>
        [JsonProperty("name")]
        [XmlElement("name")]
        public string CompanyName { get; set; }
    }
}
