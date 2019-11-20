using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 退款超时结构
    /// </summary>
    [Serializable]
    [JsonObject("refundRemindTimeout")]
    [XmlRoot("refundRemindTimeout")]
    public class RefundTimeout : BaseObject
    {
        /// <summary>
        /// 提醒的类型
        /// </summary>
        [JsonProperty("remind_type")]
        [XmlElement("remind_type")]
        public int RemindType { get; set; }

        /// <summary>
        /// 是否存在超时
        /// </summary>
        [JsonProperty("exist_timeout")]
        [XmlElement("exist_timeout")]
        public bool HasTimeout { get; set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        [JsonProperty("timeout")]
        [XmlElement("timeout")]
        public string TimeoutTime { get; set; }
    }
}
