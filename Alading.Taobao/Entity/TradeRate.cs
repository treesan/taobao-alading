using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 交易评价信息
    /// </summary>
    [Serializable]
    [JsonObject("rate")]
    [XmlRoot("rate")]
    public class TradeRate : BaseObject
    {
        /// <summary>
        /// 交易编号
        /// </summary>
        [JsonProperty("tid")]
        [XmlElement("tid")]
        public string Tid { get; set; }

        /// <summary>
        /// 子订单编号
        /// </summary>
        [JsonProperty("oid")]
        [XmlElement("oid")]
        public string Oid { get; set; }

        /// <summary>
        /// 评价者角色
        /// </summary>
        [JsonProperty("role")]
        [XmlElement("role")]
        public string Role { get; set; }

        /// <summary>
        /// 评价者昵称
        /// </summary>
        [JsonProperty("nick")]
        [XmlElement("nick")]
        public string RaterNick { get; set; }

        /// <summary>
        /// 被评价者昵称
        /// </summary>
        [JsonProperty("rated_nick")]
        [XmlElement("rated_nick")]
        public string RatedNick { get; set; }

        /// <summary>
        /// 评价结果
        /// </summary>
        [JsonProperty("result")]
        [XmlElement("result")]
        public string Result { get; set; }

        /// <summary>
        /// 商品标题
        /// </summary>
        [JsonProperty("item_title")]
        [XmlElement("item_title")]
        public string ItemTitle { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        [JsonProperty("item_price")]
        [XmlElement("item_price")]
        public string ItemPrice { get; set; }

        /// <summary>
        /// 评价内容
        /// </summary>
        [JsonProperty("content")]
        [XmlElement("content")]
        public string Content { get; set; }

        /// <summary>
        /// 评价解释
        /// </summary>
        [JsonProperty("reply")]
        [XmlElement("reply")]
        public string Reply { get; set; }
    }
}
