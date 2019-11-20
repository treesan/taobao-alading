using System;
using System.Xml.Serialization;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 运费模板结构
    /// </summary>
    [Serializable]
    [JsonObject("postage")]
    [XmlRoot("postage")]
    public class Postage : BaseObject
    {
        /// <summary>
        /// 运费模板编号
        /// </summary>
        [JsonProperty("postage_id")]
        [XmlElement("postage_id")]
        public string Id { get; set; }

        /// <summary>
        /// 运费模板名称
        /// </summary>
        [JsonProperty("name")]
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty("memo")]
        [XmlElement("memo")]
        public string Memo { get; set; }

        /// <summary>
        /// 平邮收费
        /// </summary>
        [JsonProperty("post_price")]
        [XmlElement("post_price")]
        public string PostPrice { get; set; }

        /// <summary>
        /// 平邮加件收费
        /// </summary>
        [JsonProperty("post_increase")]
        [XmlElement("post_increase")]
        public string PostIncrease { get; set; }

        /// <summary>
        /// 快递收费
        /// </summary>
        [JsonProperty("express_price")]
        [XmlElement("express_price")]
        public string ExpressPrice { get; set; }

        /// <summary>
        /// 快递加件费用
        /// </summary>
        [JsonProperty("express_increase")]
        [XmlElement("express_increase")]
        public string ExpressIncrease { get; set; }

        /// <summary>
        /// EMS收费
        /// </summary>
        [JsonProperty("ems_price")]
        [XmlElement("ems_price")]
        public string EmsPrice { get; set; }

        /// <summary>
        /// EMS加件费用
        /// </summary>
        [JsonProperty("ems_increase")]
        [XmlElement("ems_increase")]
        public string EmsIncrease { get; set; }

        /// <summary>
        /// 运费方式模板收费方式
        /// </summary>
        [JsonProperty("postage_modes")]
        [XmlArray("postage_modes")]
        [XmlArrayItem("postage_modes")]
        public PostageModes PostageModes { get; set; }
    }

    [Serializable]
    [JsonObject]
    public class PostageModes
    {
        /// <summary>
        /// postage_mode列表
        /// </summary>
        [JsonProperty("postage_mode")]
        public PostageMode[] PostageMode { get; set; }
    }

    
}
