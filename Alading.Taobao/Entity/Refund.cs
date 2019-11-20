using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 退款结构
    /// </summary>
    [Serializable]
    [JsonObject("refund")]
    [XmlRoot("refund")]
    public class Refund : BaseObject
    {
        /// <summary>
        /// 退款单号
        /// </summary>
        [JsonProperty("refund_id")]
        [XmlElement("refund_id")]
        public string Rid { get; set; }

        /// <summary>
        /// 淘宝交易单号
        /// </summary>
        [JsonProperty("tid")]
        [XmlElement("tid")]
        public string Tid { get; set; }

        /// <summary>
        /// 子订单号
        /// </summary>
        [JsonProperty("oid")]
        [XmlElement("oid")]
        public string Oid { get; set; }

        /// <summary>
        /// 支付宝交易号
        /// </summary>
        [JsonProperty("alipay_no")]
        [XmlElement("alipay_no")]
        public string AlipayNo { get; set; }

        /// <summary>
        /// 交易总金额
        /// </summary>
        [JsonProperty("total_fee")]
        [XmlElement("total_fee")]
        public string TotalFee { get; set; }

        /// <summary>
        /// 买家昵称
        /// </summary>
        [JsonProperty("buyer_nick")]
        [XmlElement("buyer_nick")]
        public string BuyerNick { get; set; }

        /// <summary>
        /// 卖家昵称
        /// </summary>
        [JsonProperty("seller_nick")]
        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        /// <summary>
        /// 退款对应的订单交易状态
        /// </summary>
        [JsonProperty("order_status")]
        [XmlElement("order_status")]
        public string OrderStatus { get; set; }

        /// <summary>
        /// 退款申请时间
        /// </summary>
        [JsonProperty("created")]
        [XmlElement("created")]
        public string Created { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [JsonProperty("modified")]
        [XmlElement("modified")]
        public string Modified { get; set; }

        /// <summary>
        /// 退款状态
        /// </summary>
        [JsonProperty("status")]
        [XmlElement("status")]
        public string RefundStatus { get; set; }

        /// <summary>
        /// 货物状态
        /// </summary>
        [JsonProperty("good_status")]
        [XmlElement("good_status")]
        public string GoodStatus { get; set; }

        /// <summary>
        /// 买家是否需要退货
        /// </summary>
        [JsonProperty("has_good_return")]
        [XmlElement("has_good_return")]
        public bool HasGoodReturn { get; set; }

        /// <summary>
        /// 退还金额
        /// </summary>
        [JsonProperty("refund_fee")]
        [XmlElement("refund_fee")]
        public string RefundFee { get; set; }

        /// <summary>
        /// 支付给卖家的金额
        /// </summary>
        [JsonProperty("payment")]
        [XmlElement("payment")]
        public string Payment { get; set; }

        /// <summary>
        /// 退款原因
        /// </summary>
        [JsonProperty("reason")]
        [XmlElement("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// 退款说明
        /// </summary>
        [JsonProperty("desc")]
        [XmlElement("desc")]
        public string Description { get; set; }

        /// <summary>
        /// 申请退款的商品编号
        /// </summary>
        [JsonProperty("iid")]
        [XmlElement("iid")]
        public string Iid { get; set; }

        /// <summary>
        /// 商品标题
        /// </summary>
        [JsonProperty("title")]
        [XmlElement("title")]
        public string ItemTitle { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        [JsonProperty("price")]
        [XmlElement("price")]
        public string ItemPrice { get; set; }

        /// <summary>
        /// 商品购买数量
        /// </summary>
        [JsonProperty("num")]
        [XmlElement("num")]
        public int ItemNum { get; set; }

        /// <summary>
        /// 退货时间
        /// </summary>
        [JsonProperty("good_return_time")]
        [XmlElement("good_return_time")]
        public string GoodReturnTime { get; set; }

        /// <summary>
        /// 物流方式
        /// </summary>
        [JsonProperty("shipping_type")]
        [XmlElement("shipping_type")]
        public string LogisticsType { get; set; }

        /// <summary>
        /// 物流公司名称
        /// </summary>
        [JsonProperty("company_name")]
        [XmlElement("company_name")]
        public string LogisticsCompany { get; set; }

        /// <summary>
        /// 退货运单号
        /// </summary>
        [JsonProperty("sid")]
        [XmlElement("sid")]
        public string Sid { get; set; }

        /// <summary>
        /// 卖家收货地址
        /// </summary>
        [JsonProperty("address")]
        [XmlElement("address")]
        public string SellerAddress { get; set; }

        /// <summary>
        /// 退款超时时间
        /// </summary>
        [JsonProperty("refund_remind_timeout")]
        [XmlElement("refund_remind_timeout")]
        public RefundTimeout Timeout { get; set; }
    }
}
