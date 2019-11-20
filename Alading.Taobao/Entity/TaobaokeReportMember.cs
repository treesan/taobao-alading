using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 淘宝客报表成员
    /// </summary>
    [Serializable]
    [JsonObject("taobaokeReportMember")]
    [XmlRoot("taobaokeReportMember")]
    public class TaobaokeReportMember : BaseObject
    {
        /// <summary>
        /// 应用授权码
        /// </summary>
        [JsonProperty("app_key")]
        [XmlElement("app_key")]
        public string AppKey { get; set; }

        /// <summary>
        /// 推广渠道
        /// </summary>
        [JsonProperty("outer_code")]
        [XmlElement("outer_code")]
        public string OuterCode { get; set; }

        /// <summary>
        /// 淘宝交易号
        /// </summary>
        [JsonProperty("trade_id")]
        [XmlElement("trade_id")]
        public long TradeId { get; set; }

        /// <summary>
        /// 成交时间
        /// </summary>
        [JsonProperty("pay_time")]
        [XmlElement("pay_time")]
        public string PayTime { get; set; }

        /// <summary>
        /// 成交价格
        /// </summary>
        [JsonProperty("pay_price")]
        [XmlElement("pay_price")]
        public double PayPrice { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        [JsonProperty("auction_id")]
        [XmlElement("auction_id")]
        public long ItemId { get; set; }

        /// <summary>
        /// 商品标题
        /// </summary>
        [JsonProperty("auction_title")]
        [XmlElement("auction_title")]
        public string ItemTitle { get; set; }

        /// <summary>
        /// 商品成交数量
        /// </summary>
        [JsonProperty("auction_number")]
        [XmlElement("auction_number")]
        public long ItemNum { get; set; }

        /// <summary>
        /// 所购买商品的类目编号
        /// </summary>
        [JsonProperty("category_id")]
        [XmlElement("category_id")]
        public long CategoryId { get; set; }

        /// <summary>
        /// 所购买商品的类目名称
        /// </summary>
        [JsonProperty("category_name")]
        [XmlElement("category_name")]
        public string CategoryName { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        [JsonProperty("shop_title")]
        [XmlElement("shop_title")]
        public string ShopTitle { get; set; }

        /// <summary>
        /// 佣金比例
        /// </summary>
        [JsonProperty("discount")]
        [XmlElement("discount")]
        public double Discount { get; set; }

        /// <summary>
        /// 用户获得的佣金
        /// </summary>
        [JsonProperty("taoke_amount")]
        [XmlElement("taoke_amount")]
        public double TaokeAmount { get; set; }
    }
}