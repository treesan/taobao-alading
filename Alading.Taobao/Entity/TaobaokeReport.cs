using System;
using System.Xml.Serialization;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 淘宝客报表
    /// </summary>
    [Serializable]
    [JsonObject("taobaokeReport")]
    [XmlRoot("taobaokeReport")]
    public class TaobaokeReport : BaseObject
    {
        /// <summary>
        /// 淘宝客报表成员列表
        /// </summary>
        [JsonProperty("members")]
        [XmlArray("members")]
        [XmlArrayItem("member")]
        public List<TaobaokeReportMember> MemberList { get; set; }
    }
}
