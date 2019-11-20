using System;
using System.Xml.Serialization;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 交易结构
    /// </summary>
    [Serializable]
    [JsonObject("trade")]
    [XmlRoot("trade")]
    public class Trade : BaseObject
    {
        /// <summary>
        /// 淘宝交易编号（父订单的交易编号）
        /// </summary>
        [JsonProperty("tid")]
        [XmlElement("tid")]
        public string Tid { get; set; }

        /// <summary>
        /// 交易标题
        /// </summary>
        [JsonProperty("title")]
        [XmlElement("title")]
        public string Title { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        [JsonProperty("type")]
        [XmlElement("type")]
        public string Type { get; set; }

        /// <summary>
        /// 卖家昵称
        /// </summary>
        [JsonProperty("seller_nick")]
        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        /// <summary>
        /// 买家昵称
        /// </summary>
        [JsonProperty("buyer_nick")]
        [XmlElement("buyer_nick")]
        public string BuyerNick { get; set; }

        /// <summary>
        /// 买家留言
        /// </summary>
        [JsonProperty("buyer_message")]
        [XmlElement("buyer_message")]
        public string BuyerMessage { get; set; }

        /// <summary>
        /// 商品的编号
        /// </summary>
        [JsonProperty("iid")]
        [XmlElement("iid")]
        public string Iid { get; set; }

        /// <summary>
        /// 商品路径
        /// </summary>
        [JsonProperty("item_url")]
        [XmlElement("item_url")]
        public string ItemUrl { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        [JsonProperty("price")]
        [XmlElement("price")]
        public float ItemPrice { get; set; }

        /// <summary>
        /// 商品图片绝对地址
        /// </summary>
        [JsonProperty("pic_path")]
        [XmlElement("pic_path")]
        public string ItemImgUrl { get; set; }

        /// <summary>
        /// 商品购买数量
        /// </summary>
        [JsonProperty("num")]
        [XmlElement("num")]
        public int ItemNum { get; set; }

        /// <summary>
        /// 物流编号
        /// </summary>
        [JsonProperty("sid")]
        [XmlElement("sid")]
        public string Sid { get; set; }

        /// <summary>
        /// 创建交易时的物流方式
        /// </summary>
        [JsonProperty("shipping_type")]
        [XmlElement("shipping_type")]
        public string ShippingType { get; set; }

        /// <summary>
        /// 支付宝交易号
        /// </summary>
        [JsonProperty("alipay_no")]
        [XmlElement("alipay_no")]
        public string AlipayNo { get; set; }

        /// <summary>
        /// 买家实付金额（获取到的Order中的payment字段在单笔子订单时包含物流费用，多笔子订单时不包含物流费用）
        /// </summary>
        [JsonProperty("payment")]
        [XmlElement("payment")]
        public float Payment { get; set; }

        /// <summary>
        /// 系统优惠金额
        /// </summary>
        [JsonProperty("discount_fee")]
        [XmlElement("discount_fee")]
        public float DiscountFee { get; set; }

        /// <summary>
        /// 卖家优惠金额
        /// </summary>
        [JsonProperty("adjust_fee")]
        [XmlElement("adjust_fee")]
        public float AdjustFee { get; set; }

        /// <summary>
        /// 交易快照信息
        /// </summary>
        [JsonProperty("snapshot")]
        [XmlElement("snapshot")]
        public string Snapshot { get; set; }

        /// <summary>
        /// 交易快照地址
        /// </summary>
        [JsonProperty("snapshot_url")]
        [XmlElement("snapshot_url")]
        public string SnapshotUrl { get; set; }

        /// <summary>
        /// 交易状态
        /// </summary>
        [JsonProperty("status")]
        [XmlElement("status")]
        public string Status { get; set; }

        /// <summary>
        /// 卖家是否已评价
        /// </summary>
        [JsonProperty("seller_rate")]
        [XmlElement("seller_rate")]
        public bool SellerRate { get; set; }

        /// <summary>
        /// 买家是否已评价
        /// </summary>
        [JsonProperty("buyer_rate")]
        [XmlElement("buyer_rate")]
        public bool BuyerRate { get; set; }

        /// <summary>
        /// 买家备注
        /// </summary>
        [JsonProperty("buyer_memo")]
        [XmlElement("buyer_memo")]
        public string BuyerMemo { get; set; }

        /// <summary>
        /// 卖家备注
        /// </summary>
        [JsonProperty("seller_memo")]
        [XmlElement("seller_memo")]
        public string SellerMemo { get; set; }

        /// <summary>
        /// 付款时间
        /// </summary>
        [JsonProperty("pay_time")]
        [XmlElement("pay_time")]
        public string PayTime { get; set; }

        /// <summary>
        /// 交易成功时间
        /// </summary>
        [JsonProperty("end_time")]
        [XmlElement("end_time")]
        public string EndTime { get; set; }

        /// <summary>
        /// 买家获得积分，返点的积分
        /// </summary>
        [JsonProperty("buyer_obtain_point_fee")]
        [XmlElement("buyer_obtain_point_fee")]
        public string BuyerObtainPointFee { get; set; }

        /// <summary>
        /// 买家使用积分
        /// </summary>
        [JsonProperty("point_fee")]
        [XmlElement("point_fee")]
        public float BuyerUsePointFee { get; set; }

        /// <summary>
        /// 买家实际使用积分
        /// </summary>
        [JsonProperty("real_point_fee")]
        [XmlElement("real_point_fee")]
        public string BuyerRealPointFee { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [JsonProperty("total_fee")]
        [XmlElement("total_fee")]
        public float TotalFee { get; set; }

        /// <summary>
        /// 邮费
        /// </summary>
        [JsonProperty("post_fee")]
        [XmlElement("post_fee")]
        public float PostFee { get; set; }

        /// <summary>
        /// 买家支付宝账号
        /// </summary>
        [JsonProperty("buyer_alipay_no")]
        [XmlElement("buyer_alipay_no")]
        public string BuyerAlipayNo { get; set; }

        /// <summary>
        /// 收货人的姓名
        /// </summary>
        [JsonProperty("receiver_name")]
        [XmlElement("receiver_name")]
        public string ReceiverName { get; set; }

        /// <summary>
        /// 收货人的所在省份
        /// </summary>
        [JsonProperty("receiver_state")]
        [XmlElement("receiver_state")]
        public string ReceiverState { get; set; }

        /// <summary>
        /// 收货人的所在城市
        /// </summary>
        [JsonProperty("receiver_city")]
        [XmlElement("receiver_city")]
        public string ReceiverCity { get; set; }

        /// <summary>
        /// 收货人的所在地区
        /// </summary>
        [JsonProperty("receiver_district")]
        [XmlElement("receiver_district")]
        public string ReceiverDistrict { get; set; }

        /// <summary>
        /// 收货人的详细地址
        /// </summary>
        [JsonProperty("receiver_address")]
        [XmlElement("receiver_address")]
        public string ReceiverAddress { get; set; }

        /// <summary>
        /// 收货人的邮编
        /// </summary>
        [JsonProperty("receiver_zip")]
        [XmlElement("receiver_zip")]
        public string ReceiverZip { get; set; }

        /// <summary>
        /// 收货人的手机号码
        /// </summary>
        [JsonProperty("receiver_mobile")]
        [XmlElement("receiver_mobile")]
        public string ReceiverMobile { get; set; }

        /// <summary>
        /// 收货人的电话号码
        /// </summary>
        [JsonProperty("receiver_phone")]
        [XmlElement("receiver_phone")]
        public string ReceiverPhone { get; set; }

        /// <summary>
        /// 卖家发货时间
        /// </summary>
        [JsonProperty("consign_time")]
        [XmlElement("consign_time")]
        public string ConsignTime { get; set; }

        /// <summary>
        /// 买家邮箱
        /// </summary>
        [JsonProperty("buyer_email")]
        [XmlElement("buyer_email")]
        public string BuyerEmail { get; set; }

        /// <summary>
        /// 交易佣金
        /// </summary>
        [JsonProperty("commission_fee")]
        [XmlElement("commission_fee")]
        public string CommissionFee { get; set; }

        /// <summary>
        /// 卖家支付宝账号
        /// </summary>
        [JsonProperty("seller_alipay_no")]
        [XmlElement("seller_alipay_no")]
        public string SellerAlipayNo { get; set; }

        /// <summary>
        /// 卖家手机
        /// </summary>
        [JsonProperty("seller_mobile")]
        [XmlElement("seller_mobile")]
        public string SellerMobile { get; set; }

        /// <summary>
        /// 卖家电话
        /// </summary>
        [JsonProperty("seller_phone")]
        [XmlElement("seller_phone")]
        public string SellerPhone { get; set; }

        /// <summary>
        /// 卖家姓名
        /// </summary>
        [JsonProperty("seller_name")]
        [XmlElement("seller_name")]
        public string SellerName { get; set; }

        /// <summary>
        /// 卖家Email
        /// </summary>
        [JsonProperty("seller_email")]
        [XmlElement("seller_email")]
        public string SellerEmail { get; set; }

        /// <summary>
        /// 能够确认收货的实付款
        /// </summary>
        [JsonProperty("available_confirm_fee")]
        [XmlElement("available_confirm_fee")]
        public string AvailableConfirmFee { get; set; }

        /// <summary>
        /// 是否包含邮费
        /// </summary>
        [JsonProperty("has_post_fee")]
        [XmlElement("has_post_fee")]
        public bool HasPostFee { get; set; }

        /// <summary>
        /// 卖家实际收到的支付宝打款金额
        /// </summary>
        [JsonProperty("received_payment")]
        [XmlElement("received_payment")]
        public float ReceivedPayment { get; set; }

        /// <summary>
        /// 货到付款服务费
        /// </summary>
        [JsonProperty("cod_fee")]
        [XmlElement("cod_fee")]
        public float CodFee { get; set; }

        /// <summary>
        /// 货到付款状态
        /// </summary>
        [JsonProperty("cod_status")]
        [XmlElement("cod_status")]
        public string CodStatus { get; set; }

        /// <summary>
        /// 交易备注
        /// </summary>
        [JsonProperty("trade_memo")]
        [XmlElement("trade_memo")]
        public string TradeMemo { get; set; }

        /// <summary>
        /// 交易路径
        /// </summary>
        [JsonProperty("trade_url")]
        [XmlElement("trade_url")]
        public string TradeUrl { get; set; }

        /// <summary>
        /// 超时到期时间
        /// </summary>
        [JsonProperty("timeout_action_time")]
        [XmlElement("timeout_action_time")]
        public string Timeout { get; set; }

        /// <summary>
        /// 订单列表
        /// </summary>
        [JsonProperty("orders")]
        public Orders Orders {get; set; }
    }

    [Serializable]
    [JsonObject]
    public class Orders
    {
        /// <summary>
        /// 订单列表
        /// </summary>
        [JsonProperty("order")]
        public Order[] Order { get; set; }
    }
}
