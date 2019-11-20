using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 基础对象
    /// </summary>
    [Serializable]
    public abstract class BaseObject
    {
        /// <summary>
        /// 对象创建时间（格式：yyyy-MM-dd HH:mm:ss）
        /// </summary>
        [JsonProperty("created")]
        [XmlElement("created")]
        public string Created { get; set; }

        /// <summary>
        /// 对象修改时间（格式：yyyy-MM-dd HH:mm:ss）
        /// </summary>
        [JsonProperty("modified")]
        [XmlElement("modified")]
        public string Modified { get; set; }
    }
}
