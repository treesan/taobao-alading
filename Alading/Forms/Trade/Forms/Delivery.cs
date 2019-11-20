
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using Alading.Taobao.Entity.Extend;
using Alading.Taobao.API;
using Alading.Business;
using Alading.Core.Enum;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Alading.Taobao;
using Alading.Utils;
using Alading.Entity;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Linq;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;

namespace Alading.Forms.Trade.Forms
{
    public partial class Delivery : DevExpress.XtraEditors.XtraForm
    {

        private Alading.Entity.Trade _focusedTrade = null;//记录当前焦点所在交易

        public Delivery()
        {
            InitializeComponent();
            InitSelectMainTab(); //初始化界面
        }


        #region  2010-04-12流程处理代码 By  DSS

        #region 界面处理函数 套路辅助
        //根据选中选项卡绑定数据
        private void InitSelectMainTab()
        {
            List<Alading.Entity.Trade> trades = null;
            GridControl currentGC = GetCurrentGC();
            GridView currentGV = GetCurrentGV();
            switch (tabsMain.SelectedTabPageIndex)
            {
                //调用存储过程获取数据
                case 0:
                    trades = TradeService.GetTradesByStatus(LocalTradeStatus.WaitAssort,TradeEnum.WAIT_SELLER_SEND_GOODS);
                    break;
                case 1:
                    trades = TradeService.GetTradesByStatus(LocalTradeStatus.AssortedNotSent, TradeEnum.WAIT_SELLER_SEND_GOODS);
                    break;
                case 2:
                    trades = TradeService.GetTradesByStatus(LocalTradeStatus.SentNotRate,TradeEnum.WAIT_SELLER_SEND_GOODS);
                    break;
            }
            currentGC.DataSource = trades;
            currentGV.BestFitColumns();
            currentGC.ForceInitialize();
        }

        //绑定数据到下方界面 根据选中选中选项卡取得数据
        private void SetTradeDetailMessage(Alading.Entity.Trade trade)
        {
            if (trade == null)
            {
                gcOrdersDetail.DataSource = null;
                detailGCFlowMessage.DataSource = null;
                return;
            }

            switch (tabsDetail.SelectedTabPageIndex)
            {
                case 0:
                    gcOrdersDetail.DataSource = TradeOrderService.GetView_TradeDetailItemsDataSet(_focusedTrade.CustomTid).Tables[0];
                    break;
                case 1:
                    detailGCFlowMessage.DataSource = TradeInfoService.GetTradeInfo(p => p.CustomTid == trade.CustomTid);
                    break;
            }
        }

        //获得当前界面的gridControl
        private GridControl GetCurrentGC()
        {
            GridControl currentGC = null;
            switch (tabsMain.SelectedTabPageIndex)
            {
                case 0:
                    currentGC = gcWaitAssort; //待配货
                    break;
                case 1:
                    currentGC = gcAssorted;//已配货
                    break;
                case 2:
                    currentGC = gcSended;//已发货
                    break;
            }
            return currentGC;
        }

        //获得当前界面gridview
        private GridView GetCurrentGV()
        {
            GridView currentGV = null;
            switch (tabsMain.SelectedTabPageIndex)
            {
                case 0:
                    currentGV = gvWaitAssort;
                    break;
                case 1:
                    currentGV = gvAssorted;
                    break;
                case 2:
                    currentGV = gvSended;
                    break;
            }
            return currentGV;
        }

        #endregion

        #region 事件处理函数


        //选中交易的焦点改变时事件
        private void gvPublic_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GridView currntGV = sender as GridView;
            _focusedTrade = currntGV.GetRow(e.FocusedRowHandle) as Alading.Entity.Trade;
            SetTradeDetailMessage(_focusedTrade);
            //默认绑定第一条商品的商品属性
            DataRow defaultRow = gvSaleInfo.GetDataRow(0);
            if (defaultRow != null)
            {
                ShowItemPropValue(defaultRow["iid"].ToString(), defaultRow["SkuProps_Str"].ToString());
            }
        }

        //主选项卡切换事件
        private void tabsMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            InitSelectMainTab();  //调用界面辅助函数绑定数据
            //默认绑定第一行数据
            _focusedTrade = GetCurrentGC().DefaultView.GetRow(0) as Alading.Entity.Trade;
            SetTradeDetailMessage(_focusedTrade);
        }

        //下方选项卡切换tab重新绑定数据
        private void tabsDetail_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            SetTradeDetailMessage(_focusedTrade);
        }

        //gvWaitAssort行值改变事件响应
        private void gvWaitAssort_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView senderIn = sender as GridView;
            Alading.Entity.Trade trade = senderIn.GetRow(e.RowHandle) as Alading.Entity.Trade;
            if (e.Column == WaitAssortIsSelected)
            {
                senderIn.BeginDataUpdate();
                trade.IsSelected = bool.Parse(e.Value.ToString());
                senderIn.EndDataUpdate();
            }
        }

        //gvAssorted行值改变事件响应
        private void gvAssorted_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView senderIn = sender as GridView;
            Alading.Entity.Trade trade = senderIn.GetRow(e.RowHandle) as Alading.Entity.Trade;
            if (e.Column == AssortedIsSelected)
            {
                senderIn.BeginDataUpdate();
                trade.IsSelected = bool.Parse(e.Value.ToString());
                senderIn.EndDataUpdate();
            }
        }

        //gvSended行值改变事件响应
        private void gvSended_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView senderIn = sender as GridView;
            Alading.Entity.Trade trade = senderIn.GetRow(e.RowHandle) as Alading.Entity.Trade;
            if (e.Column == SendedIsSelected)
            {
                senderIn.BeginDataUpdate();
                trade.IsSelected = bool.Parse(e.Value.ToString());
                senderIn.EndDataUpdate();
            }
        }

        //下方界面行值改变事件响应
        private void gvSaleInfo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow currentRow = gvSaleInfo.GetFocusedDataRow();
            ShowItemPropValue(currentRow["iid"].ToString(), currentRow["SkuProps_Str"].ToString());
            //加载出库仓库
            repositoryItemComboStockHouse.Items.Clear();
            List<StockHouseProduct> stockHouseList = StockHouseService.GetStockHouseProduct(i => i.SkuOuterID == currentRow["SkuOuterID"].ToString());
            string houseCode = string.Empty;
            foreach (StockHouseProduct stockHouse in stockHouseList)
            {
                repositoryItemComboStockHouse.Items.Add(stockHouse.HouseName);
                houseCode += stockHouse.HouseCode + ",";
            }            
            repositoryItemComboStockHouse.Tag = houseCode.Trim(',');
        }
        #endregion

        # region  Toolbar系列

        //取消打印 待打印-->待确认
        private void barCanelPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridView currentView = GetCurrentGV();//取得当前展示交易的GridView
            int totalRowCount = currentView.RowCount;//循环获取需要提交的Trade
            Alading.Entity.Trade trade = null;

            WaitDialogForm waitFrm = new WaitDialogForm(Alading.Taobao.Constants.OPERATE_DB_DATA);

            for (int runner = 0; runner < totalRowCount; runner++)
            {
                trade = currentView.GetRow(runner) as Alading.Entity.Trade;

                if (Convert.ToBoolean(trade.IsSelected))
                {
                    if (trade.IsCombined == true)
                    {
                        TradeService.RebackCommonTrade(trade.CustomTid, LocalTradeStatus.SummitNotPrint,
                            LocalTradeStatus.HasNotSummit);
                    }
                    else
                    {
                        int returnValue = TradeService.RebackCommonTrade(trade.CustomTid, LocalTradeStatus.SummitNotPrint, LocalTradeStatus.HasNotSummit);
                        switch (returnValue)
                        {
                            
                        }
                    }
                }
            }

            waitFrm.Close();
            InitSelectMainTab();
        }
      
        #endregion

        #region 右面的按钮事件响应
     
        
        #endregion

        #region  绑定商品属性
        private void ShowItemPropValue(string iid, string sku_props_name)
        {
            Alading.Entity.Item  vsItem = ItemService.GetItem(iid);
            if (vsItem == null) //如果为空 则不绑定
            {
                return;
            }
            ShopItem sItem = new ShopItem();
            sItem.cid = vsItem.cid;
            sItem.input_pids = vsItem.input_pids;
            sItem.input_str = vsItem.input_str;
            sItem.property_alias = vsItem.property_alias;
            sItem.props = vsItem.props;
            sItem.SkuProps = string.Empty;
            sItem.SkuProps_Str = sku_props_name;

            UIHelper.ShowItemPropValue(sItem, categoryRowKeyProps, categoryRowSaleProps, categoryRowNotKeyProps, categoryRowStockProps);
        }
        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleQuery_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region 处理意见
        private void barBtnHandle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_focusedTrade != null)
            {
                Alading.Forms.Trade.Forms.TradeOperateDaily tradeOperateDaily =
                   new Alading.Forms.Trade.Forms.TradeOperateDaily(_focusedTrade.CustomTid);
                tradeOperateDaily.ShowDialog();
            }
        }

        private void barBtnHandleAllo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_focusedTrade != null)
            {
                Alading.Forms.Trade.Forms.TradeOperateDaily tradeOperateDaily =
                   new Alading.Forms.Trade.Forms.TradeOperateDaily(_focusedTrade.CustomTid);
                tradeOperateDaily.ShowDialog();
            }
        }
        #endregion

        #region 全选
        private void repositoryItemCheckWait_CheckedChanged(object sender, EventArgs e)
        {
            List<Alading.Entity.Trade> tradeList = gcWaitAssort.DataSource as List<Alading.Entity.Trade>;
            if (tradeList != null && tradeList.Count > 0)
            {
                tradeList.ForEach(i => i.IsSelected = ((DevExpress.XtraEditors.CheckEdit)(sender)).Checked);
                gvWaitAssort.RefreshData();
            }
        }

        private void repositoryItemCheckAssort_CheckedChanged(object sender, EventArgs e)
        {
            List<Alading.Entity.Trade> tradeList = gcAssorted.DataSource as List<Alading.Entity.Trade>;
            if (tradeList != null && tradeList.Count > 0)
            {
                tradeList.ForEach(i => i.IsSelected = ((DevExpress.XtraEditors.CheckEdit)(sender)).Checked);
                gvAssorted.RefreshData();
            }
        }
        #endregion

        #region 刷新
        private void barBtnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitSelectMainTab();
        }

        private void barBtnRefreshAllo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitSelectMainTab();
        } 
        
        private void barBtnRefreshSend_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitSelectMainTab();
        }
        #endregion

        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnAllocate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<Alading.Entity.Trade> tradeList = gcWaitAssort.DataSource as List<Alading.Entity.Trade>;
            if (tradeList != null && tradeList.Count > 0)
            {
                List<Alading.Entity.Trade> selectTradeList = tradeList.Where(i => i.IsSelected == true).ToList();
                if (selectTradeList != null && selectTradeList.Count > 0)
                {
                    //标记为已配货并出库



                }
                else
                {
                    XtraMessageBox.Show(Constants.NOT_SELECT_ITEM,Constants.SYSTEM_PROMPT);
                }
            }
        }

        /// <summary>
        /// 更新淘宝状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnOut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<Alading.Entity.Trade> tradeList = gcAssorted.DataSource as List<Alading.Entity.Trade>;
            if (tradeList != null && tradeList.Count > 0)
            {
                List<Alading.Entity.Trade> selectTradeList = tradeList.Where(i => i.IsSelected == true).ToList();
                if (selectTradeList != null && selectTradeList.Count > 0)
                {
                    //标记为已配货并出库



                }
                else
                {
                    XtraMessageBox.Show(Constants.NOT_SELECT_ITEM, Constants.SYSTEM_PROMPT);
                }
            }
        }

        private void repositoryItemComboStockHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            //加载出库库位

        }

       
    }
}

