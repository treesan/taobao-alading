using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 属性值
    /// </summary>
    [Serializable]
    [JsonObject("prop_value")]
    [XmlRoot("prop_value")]
    public class PropValue : BaseObject
    {
        /// <summary>
        /// 类目编号
        /// </summary>
        [JsonProperty("cid")]
        [XmlElement("cid")]
        public string Cid { get; set; }

        /// <summary>
        /// 属性编号
        /// </summary>
        [JsonProperty("pid")]
        [XmlElement("pid")]
        public string Pid { get; set; }

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
        public int Vid { get; set; }

        /// <summary>
        /// 属性值名称
        /// </summary>
        [JsonProperty("name")]
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// 属性值别名
        /// </summary>
        [JsonProperty("name_alias")]
        [XmlElement("name_alias")]
        public string NameAlias { get; set; }

        /// <summary>
        /// 属性值状态
        /// </summary>
        [JsonProperty("status")]
        [XmlElement("status")]
        public string Status { get; set; }

        /// <summary>
        /// 排列序号
        /// </summary>
        [JsonProperty("sort_order")]
        [XmlElement("sort_order")]
        public int SortOrder { get; set; }

        /// <summary>
        /// 是否为父类目属性
        /// </summary>
        [JsonProperty("is_parent")]
        [XmlElement("is_parent")]
        public bool IsParent { get; set; }
    }
}
