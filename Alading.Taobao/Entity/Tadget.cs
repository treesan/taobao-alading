using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 淘宝应用
    /// </summary>
    [Serializable]
    [JsonObject("tadget")]
    [XmlRoot("tadget")]
    public class Tadget : BaseObject
    {
        /// <summary>
        /// 应用编号
        /// </summary>
        [JsonProperty("app_key")]
        [XmlElement("app_key")]
        public string AppKey { get; set; }

        /// <summary>
        /// 应用密钥
        /// </summary>
        [JsonProperty("app_secret")]
        [XmlElement("app_secret")]
        public string AppSecret { get; set; }
    }
}
