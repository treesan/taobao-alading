using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 品牌结构
    /// </summary>
    [Serializable]
    [JsonObject("brand")]
    [XmlRoot("brand")]
    public class Brand : BaseObject
    {
        /// <summary>
        /// 属性编号
        /// </summary>
        [JsonProperty("pid")]
        [XmlElement("pid")]
        public string PropId { get; set; }

        /// <summary>
        /// 属性名
        /// </summary>
        [JsonProperty("prop_name")]
        [XmlElement("prop_name")]
        public string PropName { get; set; }

        /// <summary>
        /// 属性值编号
        /// </summary>
        [JsonProperty("vid")]
        [XmlElement("vid")]
        public string ValueId { get; set; }

        /// <summary>
        /// 属性值名称
        /// </summary>
        [JsonProperty("name")]
        [XmlElement("name")]
        public string ValueName { get; set; }
    }
}
