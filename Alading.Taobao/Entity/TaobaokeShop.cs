using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 淘宝客店铺
    /// </summary>
    [Serializable]
    [JsonObject("taobaokeShop")]
    [XmlRoot("taobaokeShop")]
    public class TaobaokeShop : BaseObject
    {
        /// <summary>
        /// 店铺用户名
        /// </summary>
        [JsonProperty("user_id")]
        [XmlElement("user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        [JsonProperty("shop_title")]
        [XmlElement("shop_title")]
        public string ShopTitle { get; set; }

        /// <summary>
        /// 店铺推广地址
        /// </summary>
        [JsonProperty("click_url")]
        [XmlElement("click_url")]
        public string ClickUrl { get; set; }

        /// <summary>
        /// 淘宝客店铺佣金比率
        /// </summary>
        [JsonProperty("shop_commission_rate")]
        [XmlElement("shop_commission.rate")]
        public string CommissionRate { get; set; }
    }
}
