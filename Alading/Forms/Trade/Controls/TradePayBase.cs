using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid; 
using Alading.Forms.Trade;
using Alading.Taobao.API;
using Alading.Taobao.Entity.Extend;
using Alading.Business;
using System.Reflection;
using Alading.Core.Enum;
using Alading.Entity;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using DevExpress.XtraGauges.Win.Base;
using System.Globalization;
using DevExpress.XtraTab;
using Alading.Taobao;
using DevExpress.Utils;

namespace Alading.Forms.Trade.Controls
{
    public partial class TradePay : DevExpress.XtraEditors.XtraUserControl
    {

        #region  本界面全局公有变量 辅助功能实现
        /* 本界面全局变量 保存当前所在Tab的交易列表*/
        private DataSet _currentDataSet;
        /*本界面全局变量 保存当前焦点所在的Trade行 为刷新数据准备*/
        private DataRow _currentTradeRow = null;
        /*本界面全局变量 保存当前焦点所在的Order行 为刷新数据准备*/
        private DataRow _currentOrderRow = null;
        #endregion

        #region 绑定当前选中选项卡数据

        /// 按照每个Tab快的功能不同而设定其GridControl的值
        private void InitSelectTab()
        {
            WaitDialogForm wdf = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            DataSet resultSet = null;
            detailTicketMessage.PageVisible = false;
            //根据状态的不同  从而设置不同的参数来绑定
            switch (tradeTabMain.SelectedTabPageIndex)
            {
                case 0://所有交易
                    resultSet = TradeOrderService.GetView_TradeStockDataSet("%", "%", DateTime.Now.AddYears(-1), DateTime.Now);
                    detailCombineTradeMessage.PageVisible = false;/*隐藏合并单信息*/
                    detailAessemble.PageVisible = false;/*隐藏商品信息*/
                    break;
                case 1://未付款
                    resultSet = TradeOrderService.GetView_TradeStockDataSet(LocalTradeStatus.HasNotSummit, TradeEnum.WAIT_BUYER_PAY, DateTime.Now.AddYears(-1), DateTime.Now.AddDays(1));
                    detailAessemble.PageVisible = false;/*隐藏商品信息*/
                    break;
                case 2://待确认
                    resultSet = TradeOrderService.GetView_TradeStockDataSet(LocalTradeStatus.HasNotSummit, TradeEnum.WAIT_SELLER_SEND_GOODS, DateTime.Now.AddYears(-1), DateTime.Now.AddDays(1));
                    detailCombineTradeMessage.PageVisible = false;/*隐藏合并单信息*/
                    detailAessemble.PageVisible = false;/*隐藏商品信息*/
                    break;
                case 3://待打印
                    resultSet = TradeOrderService.GetView_TradeStockDataSet(LocalTradeStatus.SummitNotPrint, TradeEnum.WAIT_SELLER_SEND_GOODS, DateTime.Now.AddYears(-1), DateTime.Now.AddDays(1));
                    detailCombineTradeMessage.PageVisible = true;/*显示合并单信息*/
                    detailAessemble.PageVisible = false;/*隐藏商品信息*/
                    break;
                case 4://已打印
                    resultSet = TradeOrderService.GetView_TradeStockDataSet(LocalTradeStatus.Printed, TradeEnum.WAIT_SELLER_SEND_GOODS, DateTime.Now.AddYears(-1), DateTime.Now.AddDays(1));
                    break;
                case 5://已打印待配货
                    resultSet = TradeOrderService.GetView_TradeStockDataSet(LocalTradeStatus.WaitAssort, TradeEnum.WAIT_SELLER_SEND_GOODS, DateTime.Now.AddYears(-1), DateTime.Now.AddDays(1));
                    break;
                case 6://待发送
                    resultSet = TradeOrderService.GetView_TradeStockDataSet(LocalTradeStatus.AssortedNotSent, TradeEnum.WAIT_SELLER_SEND_GOODS, DateTime.Now.AddYears(-1), DateTime.Now.AddDays(1));
                    break;
                case 7://已发送
                    resultSet = TradeOrderService.GetView_TradeStockDataSet(LocalTradeStatus.SentNotRate, TradeEnum.WAIT_BUYER_CONFIRM_GOODS, DateTime.Now.AddYears(-1), DateTime.Now.AddDays(1));
                    break;
                case 8://待评价
                    resultSet = TradeOrderService.GetView_TradeStockDataSet(LocalTradeStatus.SentNotRate, TradeEnum.WAIT_BUYER_CONFIRM_GOODS, DateTime.Now.AddYears(-1), DateTime.Now.AddDays(1));
                    break;
                case 9://已完成
                    resultSet = TradeOrderService.GetView_TradeStockDataSet(LocalTradeStatus.TradeFinish, TradeEnum.TRADE_FINISHED, DateTime.Now.AddYears(-1), DateTime.Now.AddDays(1));
                    break;
                case 10://退货
                    resultSet = TradeOrderService.GetView_TradeStockDataSet(LocalTradeStatus.REFUND_STATUS, TradeEnum.ALL_CLOSED, DateTime.Now.AddYears(-1), DateTime.Now.AddDays(1));
                    break;
                case 11://历史
                    resultSet = TradeOrderService.GetView_TradeStockDataSet("%", "%", DateTime.Now.AddYears(-1), DateTime.Now.AddDays(-15));
                    break;
                default:
                    break;
            }
            LoadTrade(resultSet);
            //界面出示化时默认绑定第一条数据
            _currentTradeRow = GetCurrentTradeGV().GetDataRow(0);
            if (_currentTradeRow != null)
            {
                SetTradeDetailMessages(_currentTradeRow);
            }
            wdf.Close();
        }

        #endregion

        #region  清空当前所有选项卡

        /// 清空当前选项卡已选项  默认选中选项卡 避免多次触发PageChanged事件
        /// <param name="defaultPage">默认选中选项卡 避免多次触发PageChanged事件</param>
        private void TabsClear(XtraTabPage defaultPage)
        {
            defaultPage.PageVisible = true;
            tradeTabMain.SelectedTabPage = defaultPage;
            foreach (XtraTabPage page in tradeTabMain.TabPages)
            {
                if (page == defaultPage) continue;
                page.PageVisible = false;
            }
        }

        #endregion

        #region  获得当前选项卡的GridControl，TradeGirdView，OrderGridRow

        /// <summary>
        /// 获得当前选中Tab的GirdControl
        /// </summary>
        /// <returns></returns>
        private GridControl GetCurrentGC()
        {
            //通过当前选项卡值来确定当前GridControl
            switch (tradeTabMain.SelectedTabPageIndex)
            {
                case 0://所有交易
                    return gcAllTrades;
                case 1://未付款
                    return gcUnPaid;
                case 2://待确认
                    return gcWaitConfirm;
                case 3://待打印
                    return gcWaitPrint;
                case 4://已打印
                    return gcPrinted;
                case 5://已打印带配货
                    return gcWaitAssort;
                case 6://已配货待发货
                    return gcAssortedProduct;
                case 7://已发送
                    return gcHasSended;
                case 8://待评价
                    return gcWaitEvaluate;
                case 9://已完成
                    return gcCompleted;
                case 10://退货
                    return gcRefund;
                case 11://历史
                    return gcHistroy;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获得当前选中Tab的TradeGridView
        /// </summary>
        /// <returns></returns>
        private GridView GetCurrentTradeGV()
        {
            //通过当前选项卡值来确定当前GridControl
            switch (tradeTabMain.SelectedTabPageIndex)
            {
                case 0://所有交易
                    return gvTradeAllTrades;
                case 1://未付款
                    return gvTradeUnPaid;
                case 2://待确认
                    return gvTradeWaitConfirm;
                case 3://待打印
                    return gvTradeWaitPrint;
                case 4://已打印
                    return gvTradePrinted;
                case 5 ://待配货
                    return gvTradeWaitAssort;
                case 6://已配货待发送
                    return gvTradeAssorted;
                case 7://已发送
                    return gvTradeHasSended;
                case 8://待评价
                    return gvTradeWaitEvaluate;
                case 9://已完成
                    return gvTradeCompleted;
                case 10://退货
                    return gvTradeRefund;
                case 11://历史
                    return gvTradeHistroy;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获得当前选中Tab的TradeGridView
        /// </summary>
        /// <returns></returns>
        private GridView GetCurrentOrderGV()
        {
            //通过当前选项卡值来确定当前GridControl
            switch (tradeTabMain.SelectedTabPageIndex)
            {
                case 0://所有交易
                    return gvOrderAllTrades;
                case 1://未付款
                    return gvOrderUnPaid;
                case 2://待确认
                    return gvOrderWaitConfirm;
                case 3://待打印
                    return gvOrderWaitPrint;
                case 4://已打印
                    return gvOrderPrinted;
                case 5://已配货待发送
                    return gvOrderWaitAssort;
                case 6: //已配货待发货
                    return gvOrderAssorted;
                case 7://已发送
                    return gvOrderHasSended;
                case 8://待评价
                    return gvOrderWaitEvaluate;
                case 9://已完成
                    return gvOrderCompleted;
                case 10://退货
                    return gvOrderRefund;
                case 11://历史
                    return gvOrderHistroy;
                default:
                    return null;
            }
        }

        #endregion

        #region  更新淘宝数据库的交易收货信息

        private bool UpdateTaobaoTradeAddressMessage(Alading.Entity.Trade trade)
        {
            string sessionKey = Alading.Utils.SystemHelper.GetSessionKey(trade.seller_nick);
            Alading.Taobao.Entity.Trade taobaoTrade = new Alading.Taobao.Entity.Trade();
            //传入输入级数据参数
            taobaoTrade.Tid = trade.tid;
            taobaoTrade.ReceiverName = trade.receiver_name;
            taobaoTrade.ReceiverPhone = trade.receiver_phone;
            taobaoTrade.ReceiverMobile = trade.receiver_mobile;
            taobaoTrade.ReceiverState = trade.receiver_state;
            taobaoTrade.ReceiverCity = trade.receiver_city;
            taobaoTrade.ReceiverDistrict = trade.receiver_district;
            taobaoTrade.ReceiverAddress = trade.receiver_address;
            taobaoTrade.ReceiverZip = trade.receiver_zip;
            try
            {
                Alading.Taobao.API.TopService.TradeShippingAddressUpdate(sessionKey, taobaoTrade);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return true;
        }

        #endregion

        #region 根据参数判断交易/订单是否缺货
        /// <summary>
        /// 根据订单号来确定一笔交易是否缺货
        /// </summary>
        /// <param name="tradeOrderCode">本地唯一订单号</param>
        /// <returns></returns>
        private string GetOrderLackMessage(string tradeOrderCode)
        {
            View_TradeStock order = View_TradeStockService.GetView_TradeStock(p => p.TradeOrderCode == tradeOrderCode).FirstOrDefault();

            if (order != null)
            {
                if ((order.SkuQuantity - order.OccupiedQuantity - order.num) >= 0)
                {
                    return LackProductOrNot.Normal;
                }
                else
                {
                    return LackProductOrNot.Lack;
                }
            }
            else{

                return LackProductOrNot.NotExist;
            }
        }

        /// <summary>
        /// 根据交易单号来判断一笔交易是否缺货
        /// </summary>
        /// <param name="customTid">交易号</param>
        /// <returns></returns>
        private string GetTradeLackMessage(string customTid)
        {
            List<View_TradeStock> orders = View_TradeStockService.GetView_TradeStock(p => p.CustomTid == customTid && p.OrderType != emumOrderType.GiftGoods);
            try
            {
                int max = (int)orders.Max(p => p.SkuQuantity - p.OccupiedQuantity - p.num);
                int min = (int)orders.Min(p => p.SkuQuantity - p.OccupiedQuantity - p.num);
                if (min >= 0)
                {
                    return LackProductOrNot.Normal;
                }
                else if (max < 0)
                {
                    return LackProductOrNot.Lack;
                }
                else
                {
                    return LackProductOrNot.PartLack;
                }
            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show("未建立库存关联！");
            }
            return string.Empty;
        }
        #endregion

    }
}
