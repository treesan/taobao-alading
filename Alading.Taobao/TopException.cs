using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Alading.Taobao
{
    /// <summary>
    /// TOP客户端异常。
    /// </summary>
    public class TopException : Exception
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        [JsonProperty("code")]
        [XmlElement("code")]
        public string Code { get; set; }

        /// <summary>
        /// 错误子代码
        /// </summary>
        [JsonProperty("sub_code")]
        [XmlElement("sub_code")]
        public string SubCode { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        [JsonProperty("msg")]
        [XmlElement("msg")]
        public string Msg { get; set; }

        /// <summary>
        /// 错误子消息
        /// </summary>
        [JsonProperty("sub_msg")]
        [XmlElement("sub_msg")]
        public string SubMsg { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        [JsonProperty("args")]
        [XmlElement("args")]
        public Args Args
        {
            get;
            set;
        }

        public TopException()
            : base()
        {
        }

        public TopException(string message)
            : base(message)
        {
        }

        protected TopException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public TopException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public TopException(string code, string msg, string subcode, string submsg)
            : base(code + ":" + msg + "\n" + subcode + ":" + submsg)
        {
            this.Code = code;
            this.Msg = msg;
            this.SubCode = subcode;
            this.SubMsg = submsg;
        }        
    }

    [Serializable]
    [JsonObject]
    public class Args
    {
        /// <summary>
        /// Arg列表
        /// </summary>
        [JsonProperty("arg")]
        [XmlElement("arg")]
        public List<Arg> Arg { get; set; }
    }

    [Serializable]
    [JsonObject]
    public class Arg
    {
        /// <summary>
        /// 错误键
        /// </summary>
        [JsonProperty("key")]
        [XmlElement("key")]
        public string Key { get; set; }

        /// <summary>
        /// 错误值
        /// </summary>
        [JsonProperty("value")]
        [XmlElement("value")]
        public string Value { get; set; }
    }
}
