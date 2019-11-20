using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alading.Core.Enum
{
    /// <summary>
    /// 系统角色类别
    /// </summary>
    public enum RoleType
    { 
        /// <summary>
        /// 老板
        /// </summary>
        Boss = 1,
        /// <summary>
        /// 财务
        /// </summary>
        Accounting,
        /// <summary>
        /// 出纳
        /// </summary>
        Cashier,
        /// <summary>
        /// 客服（销售员）
        /// </summary>
        Seller,
        /// <summary>
        /// 采购员
        /// </summary>
        Buyer,
        /// <summary>
        /// 仓管员
        /// </summary>
        Warehouse,
        /// <summary>
        /// 代理商
        /// </summary>
        Agent,
        /// <summary>
        /// 其它
        /// </summary>
        Other
    }


    public enum ReturnType
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success,

        /// <summary>
        /// 没有保存成功
        /// </summary>
        SaveFailed,

        /// <summary>
        /// 数据库连接失败
        /// </summary>
        ConnFailed,//数据库连接失败

        /// <summary>
        /// 属性存在
        /// </summary>
        PropertyExisted,//属性存在

        /// <summary>
        /// 并发冲突
        /// </summary>
        Conflicted,//并发冲突

        /// <summary>
        /// 元素不存在
        /// </summary>
        NotExisted,//元素不存在

        /// <summary>
        /// 其他错误
        /// </summary>
        OthersError//其他错误
    }


    #region TradeStatus And LocalTradeStatus 已注销
    ///// <summary>
    ///// 货物淘宝状态
    ///// </summary>
    //public enum tradeStatus
    //{
    //    /// <summary>
    //    /// 没有创建支付宝交易
    //    /// </summary>
    //    TRADE_NO_CREATE_PAY=0,

    //    /// <summary>
    //    /// (等待买家付款) 
    //    /// </summary>
    //    WAIT_BUYER_PAY,

    //    /// <summary>
    //    /// (等待卖家发货,即:买家已付款) 
    //    /// </summary>
    //    WAIT_SELLER_SEND_GOODS,

    //    /// <summary>
    //    /// (等待买家确认收货,即:卖家已发货) 
    //    /// </summary>
    //    WAIT_BUYER_CONFIRM_GOODS,

    //    /// <summary>
    //    /// (买家已签收,货到付款专用) 
    //    /// </summary>
    //    TRADE_BUYER_SIGNED,

    //    /// <summary>
    //    /// (交易成功)
    //    /// </summary>
    //    TRADE_FINISHED, 

    //    /// <summary>
    //    /// (交易关闭) 
    //    /// </summary>
    //    TRADE_CLOSED,

    //    /// <summary>
    //    /// (交易被淘宝关闭) 
    //    /// </summary>
    //    TRADE_CLOSED_BY_TAOBAO,

    //    /// <summary>
    //    /// (包含：WAIT_BUYER_PAY、TRADE_NO_CREATE_PAY) 
    //    /// </summary>
    //    ALL_WAIT_PAY,

    //    /// <summary>
    //    /// (包含：TRADE_CLOSED、TRADE_CLOSED_BY_TAOBAO)
    //    /// </summary>
    //    ALL_CLOSED
    // }

    //public enum LocalStatus
    //{
    //   /// <summary>
    //   /// 没有提交
    //   /// </summary>
    //    HasNotSumit=0,

    //    /// <summary>
    //    /// 全缺货
    //    /// </summary>
    //    AllLackProduct,

    //    /// <summary>
    //    /// 部分缺货
    //    /// </summary>
    //    PartLackProduct,

    //    /// <summary>
    //    /// 提交等待打印
    //    /// </summary>
    //    SumitNOtPrint,

    //    /// <summary>
    //    /// 打印等待配货
    //    /// </summary>
    //    PrintNotAllocate,

    //    /// <summary>
    //    /// 已配货等待发货
    //    /// </summary>
    //    AllocateNotSent,

    //    /// <summary>
    //    /// 已发货未评价
    //    /// </summary>
    //    SentNotRate,

    //    /// <summary>
    //    /// 交易完结
    //    /// </summary>
    //    TradeFinish

    //}
    #endregion

    /// <summary>
    /// 商品类别
    /// </summary>
    public enum StockItemType /*一旦改动相关存储过程均要改变！！！*/
    {
        /// <summary>
        /// 产成品
        /// </summary>
        FinishGoods=1,

        /// <summary>
        /// 原材料
        /// </summary>
        RawMaterial=2,

        /// <summary>
        /// 服务类
        /// </summary>
        ServiceGoods=3,

        /// <summary>
        /// 在产品
        /// </summary>
        InProducts=4,

        /// <summary>
        /// 赠品
        /// </summary>
        GiftGoods=5
    }

    public enum TradeFlowMsgType
    {
        /// <summary>
        ///从淘宝上面下载交易，在本地被创建
        /// </summary>
        TradeCreate=0,
       
         /// <summary>
         /// 收货人信息修改
         /// </summary>
        ReceiverMessgeChanged,

        /// <summary>
        /// 订单详细信息修改
        /// </summary>
        OrderDetailChanged,

        /// <summary>
        /// 交易提交打印
        /// </summary>
        TradeCommited,

        /// <summary>
        /// 订单打印
        /// </summary>
        PrintTrade,

        /// <summary>
        /// 交易由库存发送出去
        /// </summary>
        TradeSended,

        /// <summary>
        /// 订单被拆分,对于父交易
        /// </summary>
        TradeSpilted,

        /// <summary>
        /// 由于拆分后创建的新单，对于子交易
        /// </summary>
        TradeCreateAsSpilted,

        /// <summary>
        /// 订单被合并
        /// </summary>
        TradeWasCombined,

        /// <summary>
        /// 由于拆分后创建的新单，合并单
        /// </summary>
        TradeCraeatedAsCombined,

        /// <summary>
        /// 修改意见 本条订单有操作人手动添加
        /// </summary>
        ChangedMessage
    }

    /// <summary>
    /// 店铺类型
    /// </summary>
    public enum ShopType
    {
        /// <summary>
        ///所有店铺
        /// </summary>
        AllShop = 0,

        /// <summary>
        ///淘宝商城店铺
        /// </summary>
        TaobaoBShop = 1,

        /// <summary>
        /// 淘宝普通店
        /// </summary>
        TaobaoCShop = 2,

        /// <summary>
        /// ShopEx
        /// </summary>
        ShopEx = 3,

        /// <summary>
        /// EcShop
        /// </summary>
        EcShop = 4,

        /// <summary>
        /// 拍拍
        /// </summary>
        Paipai = 5,

        /// <summary>
        /// 易趣
        /// </summary>
        Ebay = 6,

        /// <summary>
        /// 有啊
        /// </summary>
        Youa = 7,

        /// <summary>
        /// 其他
        /// </summary>
        Other = 8,
    }

    /// <summary>
    /// 税率计算公式
    /// </summary>
    public enum Formula
    {
        /// <summary>
        /// 计算公式为税额=含税金额/（1+税率）*税率
        /// </summary>
        First=1,

        /// <summary>
        /// 计算公式为税额=含税金额/税率
        /// </summary>
        Second=2
    }

    /// <summary>
    /// 商品状态,如正常,超储等
    /// </summary>
    public enum ItemProductStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        ProductNormal = 1,

        /// <summary>
        /// 缺货
        /// </summary>
        ProductLack,

        /// <summary>
        /// 超储
        /// </summary>
        ProductOverStock,

        /// <summary>
        /// 预警
        /// </summary>
        ProductWarn,
    }

    /// <summary>
    /// 进出库单据类型
    /// </summary>
    public enum InOutType
    {
        /// <summary>
        /// 生产入库
        /// </summary>
        ProduceIn=1,

        /// <summary>
        /// 采购进货
        /// </summary>
        PurchaseIn = 2,

        /// <summary>
        /// 调拨入库
        /// </summary>
        AllocateIn = 3,
        
        /// <summary>
        /// 销售退货入库
        /// </summary>
        SelledReturnIn = 4,

        /// <summary>
        /// 报溢入库
        /// </summary>   
        ProfitIn = 5,

        /// <summary>
        /// 期初入库
        /// </summary>
        InitInput=6,

        /// <summary>
        /// 其他入库
        /// </summary>
        OtherIn = 7,

        /// <summary>
        /// 调拨出库
        /// </summary>
        AllocateOut = 8,

        /// <summary>
        /// 销售发货
        /// </summary>
        SaleOut = 9,

        /// <summary>
        /// 采购退货
        /// </summary>
        PurchaseReturnOut=10,
        
        /// <summary>
        /// 报损出库
        /// </summary>
        LossOut=11,
        /// <summary>
        /// 其它出库
        /// </summary>
        OtherOut=12,        
    }

    /// <summary>
    /// 进出库对应订单的状态
    /// </summary>
    public enum InOutStatus
    {
        /// <summary>
        /// 部分发货
        /// </summary>
        SomeSend=1,
        /// <summary>
        /// 全部发货
        /// </summary>
        AllSend=2,
        /// <summary>
        /// 未发货
        /// </summary>
        AllNotSend=3,
        /// <summary>
        /// 部分到货
        /// </summary>
        SomeReach=4,
        /// <summary>
        /// 未到货
        /// </summary>
        AllNotReach=5,
        /// <summary>
        /// 全部到货
        /// </summary>
        AllReach=6,
        /// <summary>
        /// 部分退款
        /// </summary>
        SomeRefund=7,
        /// <summary>
        /// 全部退款
        /// </summary>
        AllRefund=8,
        /// <summary>
        /// 未退款
        /// </summary>
        AllNotRefund=9,
    }

    /// <summary>
    /// 支付方式
    /// </summary>
    public enum PayType
    {
        /// <summary>
        /// 现金支付
        /// </summary>
        CASH= 1,
        /// <summary>
        /// 支票支付
        /// </summary>
        CHEQUE=2,
        /// <summary>
        /// 银行转账
        /// </summary>
        BANK_TRANSFER=3,
        /// <summary>
        /// 支付宝支付
        /// </summary>
        ALIPAY=4,
        /// <summary>
        /// 信用卡支付
        /// </summary>
        CREDIT_CARD=5,
         /// <summary>
        /// 其他方式支付
        /// </summary>
        OTHER
    }

    /// <summary>
    /// 盘点结果
    /// </summary>
    public enum ProfitType
    {
        /// <summary>
        /// 正常
        /// </summary>
        NORMAL=0,

        /// <summary>
        /// 报溢
        /// </summary>
        PROFIT,

        /// <summary>
        /// 报损
        /// </summary>
        LOSS
    }


    /// <summary>
    /// 入库或出库的类型
    /// </summary>
    public enum DetailType
    {
        /// <summary>
        /// 采购入库
        /// </summary>
        PurchaseIn = 1,

        /// <summary>
        /// 销售退货入库
        /// </summary>
        SaleReturnIn = 2,

        /// <summary>
        /// 盘点入库
        /// </summary>
        CheckIn = 3,

        /// <summary>
        /// 生产入库
        /// </summary>
        ProduceIn = 4,//

        /// <summary>
        /// 调拨入库
        /// </summary>
        AllocateIn = 5,

        /// <summary>
        /// 报溢入库
        /// </summary>
        ProfitIn = 6,

        /// <summary>
        /// 期初入库
        /// </summary>
        InitInput=7,

        /// <summary>
        /// 其他入库
        /// </summary>
        OtherIn = 8,//

        /// <summary>
        /// 淘宝销售出库
        /// </summary>
        TaobaoSaleOut = 9,

        /// <summary>
        /// 采购退货出库
        /// </summary>
        SaleReturnOut = 10,

        /// <summary>
        /// 其他店销售出库
        /// </summary>
        OtherShopSaleOut = 11,

        /// <summary>
        /// 调拨出库
        /// </summary>
        AllocateOut = 12,
              
        /// <summary>
        /// 报损出库
        /// </summary>
        LossOut = 13,

        /// <summary>
        /// 其他出库
        /// </summary>
        /// 
        OtherOut = 14,       
    }

    public enum ConsignStatus
    {
        /// <summary>
        /// 无记录
        /// </summary>
        Consigning = 0,
        /// <summary>
        /// 已签收
        /// </summary>
        Consigned,
        /// <summary>
        /// 问题件
        /// </summary>
        Problem,
        /// <summary>
        /// 未签收
        /// </summary>
        NotConsign
    }

    public enum UserStatus
    {
        /// <summary>
        /// 正常员工
        /// </summary>
        Normal = 0,        
        /// <summary>
        /// 离职
        /// </summary>
        Fire,
        /// <summary>
        /// 未激活
        /// </summary>
        Inactive,
        /// <summary>
        /// 冻结
        /// </summary>
        Reeze
    }

    public static class TradeType
    {
        /// <summary>
        /// 所有交易
        /// </summary>
        public const string AllTrade = "AllTrade";
        /// <summary>
        /// 待确认
        /// </summary>
        public const string WaitSentGoods = "WaitSentGoods";
        /// <summary>
        /// 待打印
        /// </summary>
        public const string WaitPrint = "WaitPrint";
        /// <summary>
        /// 已打印
        /// </summary>
        public const string Printed = "Printed";
        /// <summary>
        /// 代发货
        /// </summary>
        public const string WaitSent = "WaitSent";
        /// <summary>
        /// 已发货
        /// </summary>
        public const string HasSented = "HasSented";
        /// <summary>
        /// 待评价
        /// </summary>
        public const string WaitComment = "WaitComment";
        /// <summary>
        /// 已完成
        /// </summary>
        public const string HasFinished = "HasFinished";
        /// <summary>
        /// 退货
        /// </summary>
        public const string Refund = "Refund";
        /// <summary>
        /// 历史订单
        /// </summary>
        public const string HistoryTrade = "HistoryTrade";
        /// <summary>
        /// 未付款
        /// </summary>
        public const string NotPayed = "NotPayed";
    }
}
