using System;
using System.Xml.Serialization;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 留言/凭证数据结构
    /// </summary>
    [Serializable]
    [JsonObject("refundMessage")]
    [XmlRoot("refundMessage")]
    public class RefundMessage : BaseObject
    {
        /// <summary>
        /// 留言编号
        /// </summary>
        [JsonProperty("message_id")]
        [XmlElement("message_id")]
        public string MsgId { get; set; }

        /// <summary>
        /// 留言内容
        /// </summary>
        [JsonProperty("content")]
        [XmlElement("content")]
        public string MsgContent { get; set; }

        /// <summary>
        /// 留言类型。
        /// </summary>
        [JsonProperty("message_type")]
        [XmlElement("message_type")]
        public string MsgType { get; set; }

        /// <summary>
        /// 凭证附件地址（图片）
        /// </summary>
        [JsonProperty("picture_urls")]
        [XmlArray("picture_urls")]
        [XmlArrayItem("picture_url")]
        public List<PicUrl> PicUrls { get; set; }

        /// <summary>
        /// 退款编号
        /// </summary>
        [JsonProperty("refund_id")]
        [XmlElement("refund_id")]
        public string RefundId { get; set; }

        /// <summary>
        /// 留言者编号
        /// </summary>
        [JsonProperty("owner_id")]
        [XmlElement("owner_id")]
        public string OwnerId { get; set; }

        /// <summary>
        /// 留言者昵称
        /// </summary>
        [JsonProperty("owner_nick")]
        [XmlElement("owner_nick")]
        public string OwnerNick { get; set; }

        /// <summary>
        /// 留言者身份
        /// </summary>
        [JsonProperty("owner_role")]
        [XmlElement("owner_role")]
        public string OwnerRole { get; set; }
    }

    /// <summary>
    /// 图片地址结构
    /// </summary>
    [Serializable]
    [JsonObject]
    [XmlRoot("picture_url")]
    public class PicUrl
    {
        /// <summary>
        /// 图片地址。
        /// </summary>
        [JsonProperty("url")]
        [XmlElement("url")]
        public string Url { get; set; }
    }
}
