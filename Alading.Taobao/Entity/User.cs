using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Serializable]
    [JsonObject("user")]
    [XmlRoot("user")]
    public class User : BaseObject
    {
        /// <summary>
        /// 用户编号。
        /// </summary>
        [JsonProperty("user_id")]
        [XmlElement("user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// 用户昵称。
        /// </summary>
        [JsonProperty("nick")]
        [XmlElement("nick")]
        public string Nick { get; set; }

        /// <summary>
        /// 用户繁体昵称。
        /// </summary>
        [JsonProperty("t_nick")]
        [XmlElement("t_nick")]
        public string TNick { get; set; }

        /// <summary>
        /// 用户性别。
        /// </summary>
        [JsonProperty("sex")]
        [XmlElement("sex")]
        public string Sex { get; set; }

        /// <summary>
        /// 卖家信用。
        /// </summary>
        [JsonProperty("seller_credit")]
        [XmlElement("seller_credit")]
        public UserCredit SellerCredit { get; set; }

        /// <summary>
        /// 买家信用。
        /// </summary>
        [JsonProperty("buyer_credit")]
        [XmlElement("buyer_credit")]
        public UserCredit BuyerCredit { get; set; }

        /// <summary>
        /// 用户当前居住地公开信息。
        /// </summary>
        [JsonProperty("location")]
        [XmlElement("location")]
        public Location Location { get; set; }

        /// <summary>
        /// 最近登陆时间。
        /// </summary>
        [JsonProperty("last_visit")]
        [XmlElement("last_visit")]
        public DateTime LastVisit { get; set; }

        /// <summary>
        /// 用户生日日期。
        /// </summary>
        [JsonProperty("birthday")]
        [XmlElement("birthday")]
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 用户类型。
        /// </summary>
        [JsonProperty("type")]
        [XmlElement("type")]
        public string Type { get; set; }

        /// <summary>
        /// 是否购买多图服务。
        /// </summary>
        [JsonProperty("has_more_pic")]
        [XmlElement("has_more_pic")]
        public bool HasMorePic { get; set; }

        /// <summary>
        /// 可上传商品图片数量。
        /// </summary>
        [JsonProperty("item_img_num")]
        [XmlElement("item_img_num")]
        public int ItemImgNum { get; set; }

        /// <summary>
        /// 单张商品图片最大容量。
        /// </summary>
        [JsonProperty("item_img_size")]
        [XmlElement("item_img_size")]
        public int ItemImgSize { get; set; }

        /// <summary>
        /// 可上传属性图片数量。
        /// </summary>
        [JsonProperty("prop_img_num")]
        [XmlElement("prop_img_num")]
        public int PropImgNum { get; set; }

        /// <summary>
        /// 单张销售属性图片最大容量。
        /// </summary>
        [JsonProperty("prop_img_size")]
        [XmlElement("prop_img_size")]
        public int PropImgSize { get; set; }

        /// <summary>
        /// 是否受限制。
        /// </summary>
        [JsonProperty("auto_repost")]
        [XmlElement("auto_repost")]
        public string AutoRepost { get; set; }

        /// <summary>
        /// 有无实名认证。
        /// </summary>
        [JsonProperty("promoted_type")]
        [XmlElement("promoted_type")]
        public string PromotedType { get; set; }

        /// <summary>
        /// 用户账号状态。
        /// </summary>
        [JsonProperty("status")]
        [XmlElement("status")]
        public string Status { get; set; }

        /// <summary>
        /// 有无绑定支付宝。
        /// </summary>
        [JsonProperty("alipay_bind")]
        [XmlElement("alipay_bind")]
        public string AlipayBind { get; set; }

        /// <summary>
        /// 支付宝数字编号。
        /// </summary>
        [JsonProperty("alipay_no")]
        [XmlElement("alipay_no")]
        public string AlipayNo { get; set; }

        /// <summary>
        /// 支付宝邮件账号。
        /// </summary>
        [JsonProperty("alipay_account")]
        [XmlElement("alipay_account")]
        public string AlipayAccount { get; set; }

        /// <summary>
        /// 是否参加消保。
        /// </summary>
        [JsonProperty("consumer_protection")]
        [XmlElement("consumer_protection")]
        public bool ConsumerProtection { get; set; }
    }
}
