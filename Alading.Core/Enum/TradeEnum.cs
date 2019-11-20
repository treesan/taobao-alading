using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Alading.Core.Enum
{
    public static class TradeEnum
    {
         /// <summary>
        /// 没有创建支付宝交易
        /// </summary>
        public const string TRADE_NO_CREATE_PAY="TRADE_NO_CREATE_PAY";

        /// <summary>
        /// (等待买家付款) 
        /// </summary>
        public const string WAIT_BUYER_PAY = "WAIT_BUYER_PAY";

        /// <summary>
        /// (等待卖家发货,即:买家已付款) 
        /// </summary>
        public const string WAIT_SELLER_SEND_GOODS = "WAIT_SELLER_SEND_GOODS";

        /// <summary>
        /// (等待买家确认收货,即:卖家已发货) 
        /// </summary>
        public const string WAIT_BUYER_CONFIRM_GOODS = "WAIT_BUYER_CONFIRM_GOODS";

        /// <summary>
        /// (买家已签收,货到付款专用) 
        /// </summary>
        public const string TRADE_BUYER_SIGNED = "TRADE_BUYER_SIGNED";

        /// <summary>
        /// (交易成功)
        /// </summary>
        public const string TRADE_FINISHED = "TRADE_FINISHED";

        /// <summary>
        /// (交易关闭) 
        /// </summary>
        public const string TRADE_CLOSED = "TRADE_CLOSED";

        /// <summary>
        /// (交易被淘宝关闭) 
        /// </summary>
        public const string TRADE_CLOSED_BY_TAOBAO = "TRADE_CLOSED_BY_TAOBAO";

        /// <summary>
        /// (包含：WAIT_BUYER_PAY、TRADE_NO_CREATE_PAY) 
        /// </summary>
        public const string ALL_WAIT_PAY = "ALL_WAIT_PAY";

        /// <summary>
        /// (包含：TRADE_CLOSED、TRADE_CLOSED_BY_TAOBAO)
        /// </summary>
        public const string ALL_CLOSED = "ALL_CLOSED";
        
        /// <summary>
        /// 退货状态
        /// </summary>
        public const string REFUND_STATUS ="REFUND_STATUS";
    }

    /// <summary>
    /// 订单商品类型
    /// </summary>
    public static class emumOrderType
    {
        public const string SellGoods = "销售品";
        public const string GiftGoods = "赠品";
    }

    public static class RefundStatus
    {
        /// <summary>
        /// (无退款)
        /// </summary>
        public const string NO_REFUND = "NO_REFUND";
        /// <summary>
        /// (买家已经申请退款，等待卖家同意) 
        /// </summary>
        public const string WAIT_SELLER_AGREE = "WAIT_SELLER_AGREE";
        /// <summary>
        /// (卖家已经同意退款，等待买家退货)
        /// </summary>
        public const string WAIT_BUYER_RETURN_GOODS = "WAIT_BUYER_RETURN_GOODS";
        /// <summary>
        /// (买家已经退货，等待卖家确认收货) 
        /// </summary>
        public const string WAIT_SELLER_CONFIRM_GOODS = "WAIT_SELLER_CONFIRM_GOODS";
        /// <summary>
        /// (卖家拒绝退款) 
        /// </summary>
        public const string SELLER_REFUSE_BUYER = "SELLER_REFUSE_BUYER";

        /// <summary>
        /// (退款关闭) 
        /// </summary>
        public const string CLOSED = "CLOSED";
        /// <summary>
        /// (退款成功) 
        /// </summary>
        public const string SUCCESS = "SUCCESS";

    }

    /// <summary>
    /// 交易订单来源 区别不同平台
    /// </summary>
    public static class TradeSourceType
    {
        /// <summary>
        /// 淘宝API获取数据
        /// </summary>
        public const string TAOBAOAPI = "TAOBAOAPI";
        /// <summary>
        /// 淘宝新建交易订单
        /// </summary>
        public const string TAOBAO = "TAOBAO";
        /// <summary>
        /// 有啊新建交易订单
        /// </summary>
        public const string YOUA = "YOUA";
        /// <summary>
        /// 拍拍新建交易订单
        /// </summary>
        public const string PAIPAI = "PAIPAI";
        /// <summary>
        /// 易趣新建交易订单
        /// </summary>
        public const string EBAY = "EBAY";
        /// <summary>
        /// 新建交易订单  暂时备用
        /// </summary>
        public const string NEWCREATE = "NEWCREATE";
    }

    public static class LocalTradeStatus
    {
        /// <summary>
        /// 没有提交
        /// </summary>
        public const string HasNotSummit = "HasNotSummit";

        /// <summary>
        /// 提交等待打印
        /// </summary>
        public const string SummitNotPrint = "SummitNotPrint";

        /// <summary>
        /// 合单 专用LocalTradeStatus
        /// </summary>
        public const string CombineTrade = "CombineTrade";
        /// <summary>
        ///已打印
        /// </summary>
        public const string Printed = "Printed";

        /// <summary>
        ///等待配货
        /// </summary>
        public const string WaitAssort = "WaitAssort";

        /// <summary>
        /// 已配货等待发货
        /// </summary>
        public const string AssortedNotSent = "AssortedNotSent";

        /// <summary>
        /// 已发货未评价
        /// </summary>
        public const string SentNotRate = "SentNotRate";

        /// <summary>
        /// 交易完结
        /// </summary>
        public const string TradeFinish = "TradeFinish";

        /// <summary>
        /// 退货状态
        /// </summary>
        public const string REFUND_STATUS = "REFUND_STATUS";
        }

    public static class LackProductOrNot
    {
        /// <summary>
        ///交易不存在
        /// </summary>
        public const string NotExist = "不存在";
        /// <summary>
        /// 交易缺货
        /// </summary>
        public const string Lack = "缺货";
        /// <summary>
        /// 交易部分缺货
        /// </summary>
        public const string PartLack = "部分缺货";
        /// <summary>
        /// 交易正常，不缺货
        /// </summary>
        public const string Normal = "正常";
        /// <summary>
        /// 未建立库存
        /// </summary>
        public const string NotBuildStock = "未关联库存";
    }


    /// <summary>
    /// 合并方式
    /// </summary>
    public static class combineStyle
    {
        public const string ConbineTradeFlg = "ConbineTradeFlg";
            public const string ConbinePrintFlg = "ConbinePrintFlg";
    }

    /// <summary>
    /// 邮递方式
    /// </summary>
    public static class ShippingType
    {
        public static string free = "free";
        public static string express = "express";
        public static string ems = "ems";
        public static string post = "post";
    }

    public static class SummitReturnType
    {
        /// <summary>
        /// 提交成功
        /// </summary>
        public const int Success = 1;
        /// <summary>
        /// 缺货状态
        /// </summary>
        public const int GoodsLacked = 2;
        /// <summary>
        /// 交易状态已经改变
        /// </summary>
        public const int StatusChanged = 3;
        /// <summary>
        /// 物流单号没有填入
        /// </summary>
        public const int TemplateNeeded = 4;


    }

    /// <summary>
    /// 未锁定
    /// </summary>
    public static class  UNLOCKED
    {
        public const string VALUE = "未锁定";
    }

    /// <summary>
    /// 商品类型
    /// </summary>
    public static class ItemType
    {
        /// <summary>
        /// 包装商品
        /// </summary>
        public const string BagProduct = "包装商品";
        /// <summary>
        /// 组合商品
        /// </summary>
        public const string ConbineProduct = "组合商品";
        /// <summary>
        /// 普通商品
        /// </summary>
        public const string CommonProduct = "普通商品";
    }

    public static class SellerShopType
    {
        /// <summary>
        /// C店
        /// </summary>
        public const string cShop = "c";
        /// <summary>
        /// B店
        /// </summary>
        public const string bShop = "b";
    }
}

