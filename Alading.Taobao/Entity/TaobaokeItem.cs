using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 淘宝客商品
    /// </summary>
    [Serializable]
    [JsonObject("taobaokeItem")]
    [XmlRoot("taobaokeItem")]
    public class TaobaokeItem : BaseObject
    {
        /// <summary>
        /// 淘宝客商品编号
        /// </summary>
        [JsonProperty("id")]
        [XmlElement("iid")]
        public string Iid { get; set; }

        /// <summary>
        /// 宝贝名称
        /// </summary>
        [JsonProperty("title")]
        [XmlElement("title")]
        public string Title { get; set; }

        /// <summary>
        /// 卖家昵称
        /// </summary>
        [JsonProperty("nick")]
        [XmlElement("nick")]
        public string Nick { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [JsonProperty("pict_url")]
        [XmlElement("pic_url")]
        public string PicUrl { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        [JsonProperty("price")]
        [XmlElement("price")]
        public string Price { get; set; }

        /// <summary>
        /// 推广点击网址
        /// </summary>
        [JsonProperty("click_url")]
        [XmlElement("click_url")]
        public string ClickUrl { get; set; }

        /// <summary>
        /// 淘宝客佣金
        /// </summary>
        [JsonProperty("commission")]
        [XmlElement("commission")]
        public string Commission { get; set; }

        /// <summary>
        /// 淘宝客佣金比率
        /// </summary>
        [JsonProperty("commission_rate")]
        [XmlElement("commission_rate")]
        public string CommissionRate { get; set; }

        /// <summary>
        /// 累计成交量
        /// </summary>
        [JsonProperty("commission_num")]
        [XmlElement("commission_num")]
        public string CommissionNum { get; set; }

        /// <summary>
        /// 累计总支出佣金量
        /// </summary>
        [JsonProperty("commission_volume")]
        [XmlElement("commission_volume")]
        public string CommissionVolume { get; set; }
    }
}
