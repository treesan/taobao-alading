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
using Alading.Forms.Trade.Forms;
using Alading.Utils;
using Alading.Taobao;
using System.Net;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using DevExpress.Utils;


namespace Alading.Forms.Trade.Forms
{
    public partial class GiftGiven : XtraForm
    {
        private string _customTid = string.Empty;//当前添加赠品所在交易

        public GiftGiven()
        {
            InitializeComponent();
            //本构造函数在新建交易时使用 因而隐藏功能
        }

        //自定义构造函数
        public GiftGiven(string customTid)
        {
            InitializeComponent();
            _customTid = customTid;
            InitGiftList();
            InitTradeList();
        }

        #region 初始化界面函数
        //初始化赠品列表
        private void InitGiftList()
        {
            gcGiftList.DataSource = View_StockItemProductService.GetView_StockItemProductByType((int)Alading.Core.Enum.StockItemType.GiftGoods);
           gcGiftList.ForceInitialize();
        }

        //刷新交易赠品列表
        private void InitTradeList()
        {
            gcGiftOrders.DataSource=TradeOrderService.GetTradeOrder(p => p.CustomTid == _customTid&&p.OrderType==Alading.Core.Enum.emumOrderType.GiftGoods);
            gcGiftOrders.ForceInitialize();
        }

#endregion

        #region ToolBar系列
        //刷新赠品列表
        private void barReflush_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitGiftList();
        }

        //删除选中交易
        private void barDeleteGifts_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int totalRowCount = gvGiftOrders.RowCount;//循环获取需要提交的Trade
            TradeOrder orderItem = null;
            List<string> removeOrderList = new List<string>();//批量删除数据

            WaitDialogForm waitFrm = new WaitDialogForm(Alading.Taobao.Constants.OPERATE_DB_DATA);

            for (int runner = 0; runner < totalRowCount; runner++)
            {
                orderItem = gvGiftOrders.GetRow(runner) as Alading.Entity.TradeOrder;

                if (Convert.ToBoolean(orderItem.IsSelected))
                {
                    removeOrderList.Add(orderItem.TradeOrderCode);
                }
            }
            waitFrm.Close();
            TradeOrderService.RemoveTradeOrder(removeOrderList);
            InitTradeList();
            XtraMessageBox.Show("删除赠品成功！");
        }

        /// 向交易添加赠品
        private void barGiftAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int totalRowCount = gvGiftList.RowCount;//循环获取需要提交的Trade
            List<TradeOrder> orderList = new List<TradeOrder>();//批量提交数据
            View_StockItemProduct giftItem = null;

            WaitDialogForm waitFrm = new WaitDialogForm(Alading.Taobao.Constants.OPERATE_DB_DATA);

            for (int runner = 0; runner < totalRowCount; runner++)
            {
                giftItem = gvGiftList.GetRow(runner) as Alading.Entity.View_StockItemProduct;

                if (Convert.ToBoolean(giftItem.IsSelected))
                {
                   int existFlag= (gvGiftOrders.DataSource as List<TradeOrder>).Count(p=>p.outer_sku_id==giftItem.SkuOuterID);
                    if(existFlag!=0)
                    {
                        XtraMessageBox.Show("交易中已经含有赠品："+giftItem.Name);
                    }
                    else
                    {
                        TradeOrder createOrder = new TradeOrder();
                        CreateOrderRow(createOrder, giftItem);
                        orderList.Add(createOrder);
                    }
                }
            }
            TradeOrderService.AddTradeOrder(orderList);
            waitFrm.Close();
            InitTradeList();
            XtraMessageBox.Show("添加赠品成功！");
        }

        //返回
        private void barReturn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        //确认订单修改
        private void barConfirmNumChange_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int totalRowCount = gvGiftOrders.RowCount;//循环获取需要提交的Trade
            TradeOrder orderItem = null;

            WaitDialogForm waitFrm = new WaitDialogForm(Alading.Taobao.Constants.OPERATE_DB_DATA);

            for (int runner = 0; runner < totalRowCount; runner++)
            {
                orderItem = gvGiftOrders.GetRow(runner) as Alading.Entity.TradeOrder;
                TradeOrderService.UpdateTradeOrder(orderItem);
            }
            waitFrm.Close();
            InitTradeList();
            XtraMessageBox.Show("保存修改成功！");
        }
        #endregion

        #region 界面GridView事件响应
        private void gvGiftList_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView senderIn = sender as GridView;
            View_StockItemProduct item = senderIn.GetRow(e.RowHandle) as Alading.Entity.View_StockItemProduct;
            if (e.Column == giftSelected)
            {
                item.IsSelected = bool.Parse(e.Value.ToString());
                gvGiftList.UpdateCurrentRow();
            }
            
        }

        private void gvGiftOrders_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView senderIn = sender as GridView;
            TradeOrder item = senderIn.GetRow(e.RowHandle) as Alading.Entity.TradeOrder;
            if (e.Column == orderSelected)
            {
                item.IsSelected= bool.Parse(e.Value.ToString());
                gvGiftOrders.UpdateCurrentRow();
            }
        }


        private void gvGiftOrders_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView senderIn = sender as GridView;
            TradeOrder item = senderIn.GetRow(e.RowHandle) as Alading.Entity.TradeOrder;
            if (e.Column == OrderNum)
            {
                item.num =int.Parse(e.Value.ToString());
                gvGiftOrders.UpdateCurrentRow();
            }
        }
#endregion

        #region  构造一条交易数据
        private void CreateOrderRow(TradeOrder createOrder,View_StockItemProduct item)
        {
            createOrder.CustomTid = _customTid;
            createOrder.iid = string.Empty;
            createOrder.sku_id = string.Empty;
            createOrder.TradeOrderCode = Guid.NewGuid().ToString();
            createOrder.oid = createOrder.TradeOrderCode;
            createOrder.outer_sku_id = item.SkuOuterID;
            createOrder.outer_iid = item.OuterID;
            createOrder.sku_properties_name = item.SkuProps_Str;
            createOrder.price = item.SkuPrice;
            createOrder.total_fee = 0.0;
            createOrder.discount_fee = 0.0;//淘宝系统优惠价  为 0. 0
            createOrder.adjust_fee = 0.0;
            createOrder.payment = 0.0;
            createOrder.item_meal_name = string.Empty;
            createOrder.num = 1;
            createOrder.title ="赠送礼品";
            createOrder.pic_path = string.Empty;
            createOrder.seller_nick = string.Empty;
            createOrder.buyer_nick = string.Empty;
            createOrder.created = DateTime.Now;
            createOrder.refund_status = Alading.Core.Enum.RefundStatus.NO_REFUND;
            createOrder.status = TradeEnum.WAIT_SELLER_SEND_GOODS;
            createOrder.snapshot_url = string.Empty;
            createOrder.snapshot = string.Empty;
            createOrder.timeout_action_time = DateTime.MinValue;
            createOrder.OrderType = Alading.Core.Enum.emumOrderType.GiftGoods;//系统配置订单类型
            createOrder.type = string.Empty;
            createOrder.seller_type = string.Empty;
            createOrder.HouseCode = string.Empty;
            createOrder.LayoutCode = string.Empty;
            createOrder.IsSelected = false;
        }
        #endregion

     


  
    }
}