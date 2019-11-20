using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 买家收货地址
    /// </summary>
    [Serializable]
    [JsonObject("shipping")]
    [XmlRoot("shipping")]
    public class Shipping : BaseObject
    {
        /// <summary> 
        /// 运单号
        /// </summary>
        [JsonProperty("out_sid")]
        [XmlElement("out_sid")]
        public string OutSid { get; set; }

        /// <summary> 
        /// 卖家昵称
        /// </summary>
        [JsonProperty("seller_nick")]
        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        /// <summary> 
        ///买家昵称
        /// </summary>
        [JsonProperty("buyer_nick")]
        [XmlElement("buyer_nick")]
        public string BuyerNick { get; set; }

        /// <summary> 
        ///预约取货开始时间 
        /// </summary>
        [JsonProperty("delivery_start")]
        [XmlElement("delivery_start")]
        public string DeliveryStart { get; set; }

        /// <summary> 
        ///预约取货结束时间
        /// </summary>
        [JsonProperty("delivery_end")]
        [XmlElement("delivery_end")]
        public string DeliveryEnd { get; set; }

        /// <summary> 
        ///收件人姓名 
        /// </summary>
        [JsonProperty("receiver_name")]
        [XmlElement("receiver_name")]
        public string ReceiverName { get; set; }

        /// <summary> 
        ///收件人电话  
        /// </summary>
        [JsonProperty("receiver_phone")]
        [XmlElement("receiver_phone")]
        public string ReceiverPhone { get; set; }

        /// <summary> 
        ///货物名称 
        /// </summary>
        [JsonProperty("item_title")]
        [XmlElement("item_title")]
        public string ItemTitle { get; set; }

        /// <summary> 
        ///收件人手机号码
        /// </summary>
        [JsonProperty("receiver_mobile")]
        [XmlElement("receiver_mobile")]
        public string ReceiverMobile { get; set; }

        /// <summary> 
        ///收件人地址信息 
        /// </summary>
        [JsonProperty("receiver_location")]
        [XmlElement("receiver_location")]
        public string ReceiverLocation { get; set; }

        /// <summary> 
        ///物流订单状态
        /// </summary>
        [JsonProperty("status")]
        [XmlElement("status")]
        public string Status { get; set; }

        /// <summary> 
        ///物流方式
        /// </summary>
        [JsonProperty("type")]
        [XmlElement("type")]
        public string Type { get; set; }

        /// <summary> 
        ///谁承担运费
        /// </summary>
        [JsonProperty("freight_payer")]
        [XmlElement("freight_payer")]
        public string FreightPayer{ get; set; }

        /// <summary> 
        ///卖家是否确认发货
        /// </summary>
        [JsonProperty("seller_confirm")]
        [XmlElement("seller_confirm")]
        public string SellerConfirm { get; set; }


        /// <summary> 
        ///物流公司名称 
        /// </summary>
        [JsonProperty("company_name")]
        [XmlElement("company_name")]
        public string CompanyName { get; set; }

        /// <summary>
        /// 交易编号
        /// </summary>
        [JsonProperty("tid")]
        [XmlElement("tid")]
        public int  Tid { get; set; }
    }
}
