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

namespace Alading.Forms.Print
{
    public partial class PrintManage : DevExpress.XtraEditors.XtraForm
    {
        //记录当前交点所在位置的Trade
        private Alading.Entity.Trade _focusedTrade =null;

        public PrintManage()
        {
            InitializeComponent();
        }

        private void PrintManage_Load(object sender, EventArgs e)
        {
            List<Alading.Entity.Trade> trades = Alading.Business.TradeService.GetTrade(p => p.LocalStatus == Alading.Core.Enum.LocalTradeStatus.SummitNotPrint && p.status == Alading.Core.Enum.TradeEnum.WAIT_SELLER_SEND_GOODS);
            gridCtrlWait.DataSource = trades;
            gvWaitPrint.BestFitColumns();
        }

        #region  2010-04-04之前代码

        private void LoadPrintPreview(Alading.Entity.Trade trade)
        {
            if (trade != null && !string.IsNullOrEmpty(trade.TemplateCode))
            {
                LogisticCompanyTemplate template = LogisticCompanyService.GetLogisticCompanyTemplate(t => t.TemplateCode == trade.TemplateCode).FirstOrDefault();

                if (template != null)
                {
                    byte[] decommpressBytes = CompressHelper.Decompress(template.TemplateData);
                    MemoryStream memStream = new MemoryStream(decommpressBytes);
                    XtraReport xtraReport = new XtraReport();
                    xtraReport.PrintingSystem = expressPreview.PrintingSystem;
                    xtraReport.LoadLayout(memStream);
                    xtraReport.CreateDocument(true);
                    memStream.Close();

                    SetValue(xtraReport, "收件人", trade.receiver_name);
                    SetValue(xtraReport, "收件人电话", trade.receiver_phone);
                    SetValue(xtraReport, "收件人手机", trade.receiver_mobile);
                    SetValue(xtraReport, "收件人地址", trade.receiver_address);
                    SetValue(xtraReport, "收件人邮编", trade.receiver_zip);
                    SetValue(xtraReport, "寄件人", trade.seller_name);
                    SetValue(xtraReport, "寄件人手机", trade.seller_mobile);
                    SetValue(xtraReport, "寄件人电话", trade.seller_phone);


                }
            }
            //PrintingSystem printingSystem = new PrintingSystem();

            ////非常关键，设置打印页面大小
            ////自定义的纸张,注意单位是:百分之一英寸 0.01英寸
            //PaperSize customPaperSize = new PaperSize("Custom Paper Size", 500, 550);
            //Margins customMargins = new Margins(0, 0, 0, 0);
            //PageSettings customPageSettings = new PageSettings();
            //customPageSettings.PaperSize = customPaperSize;
            //customPageSettings.Margins = customMargins;
            //printingSystem.PageSettings.Assign(customPageSettings);

            //this.printCtrlExpress.PrintingSystem = printingSystem;

            //XtraReport xtraReport = new XtraReport();
            //xtraReport.LoadLayout(@"d:\XtraReport.repx");
            //xtraReport.PrintingSystem = printingSystem;
            //xtraReport.CreateDocument(true);

            ////预览配货单
            //PrintingSystem printingProductSystem = new PrintingSystem();
            //this.printCtrlProduct.PrintingSystem = printingProductSystem;

            //PreviewReport productReport = new PreviewReport();
            //Parameter parameter = productReport.Parameters["TradeCode"];
            //parameter.Value = "20100345789531";
            //productReport.PrintingSystem = printingProductSystem;
            //productReport.CreateDocument(true);
            //printingProductSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.Parameters, new object[] { true });
        }

        private void SetValue(XtraReport xtraReport, string ctrlName, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                value = string.Empty;
            }

            XRControl ctrl = xtraReport.FindControl(ctrlName, true);
            if (ctrl != null)
            {
                ctrl.Text = value;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 刷新待打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barPrtOne_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitSelectMainTab();
        }

        /// <summary>
        /// 导出待打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barPrtAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExportExcel(gridCtrlWait);
        }

        /// <summary>
        /// 导出已打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnExportPrinted_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExportExcel(gridCtrlPrinted);
        }

        /// <summary>
        /// 刷新已打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnRefreshPrinted_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitSelectMainTab();
        }

        public void ShowItemDetail()
        {

        }

        /// <summary>
        /// 导出方法
        /// </summary>
        public void ExportExcel(GridControl gridCtrl)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            DialogResult dialogResult = saveFileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                XlsExportOptions options = new XlsExportOptions();
                gridCtrl.ExportToXls(saveFileDialog.FileName, options);
                DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region  2010-04-04流程处理代码

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
                    trades = TradeService.GetTradesByStatus(LocalTradeStatus.SummitNotPrint ,TradeEnum.WAIT_SELLER_SEND_GOODS);
                    break;
                case 1:
                    trades = TradeService.GetTradesByStatus( LocalTradeStatus.Printed ,TradeEnum.WAIT_SELLER_SEND_GOODS);
                    break;
                case 2:
                    trades = TradeService.GetTradesByStatus(LocalTradeStatus.WaitAssort, TradeEnum.WAIT_SELLER_SEND_GOODS);
                    break;
                //case 3:
                //    trades = TradeService.GetTrade(p => p.LocalStatus == LocalTradeStatus.AllocateNotSent && p.status == TradeStatus.WAIT_SELLER_SEND_GOODS);
                //    break;
            }
            currentGC.DataSource = trades;
            currentGV.BestFitColumns();
            currentGC.ForceInitialize();
        }

        //绑定数据到下方界面 根据选中选中选项卡取得数据
        private void SetTradeDetailMessage(Alading.Entity.Trade trade)
        {
            if (trade == null)
                return;

            switch (tabsDetailMessage.SelectedTabPageIndex)
            {
                case 0:
                    LoadPrintPreview(trade);
                    break;
                case 1:
                    break;
                case 2:
                    //调用存储过程获得数据
                    gcOrdersDetail.DataSource = TradeOrderService.GetView_TradeDetailItemsDataSet(trade.CustomTid).Tables[0];
                    break;
                case 3:
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
                    currentGC = gridCtrlWait;
                    break;
                case 1:
                    currentGC = gridCtrlPrinted;
                    break;
                case 2:
                    currentGC = gridCtrlWaitSendGood;
                    break;
                case 3:
                    currentGC = gridCtrlWaitSendGood;
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
                    currentGV = gvWaitPrint;
                    break;
                case 1:
                    currentGV = gridViewPrinted;
                    break;
                case 2:
                    currentGV = gvWaitSendGood;
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
            //默认绑定第一条的商品属性数据
            DataRow currentRow = gvSaleInfo.GetFocusedDataRow();
            if (currentRow != null)
            {
                ShowItemPropValue(currentRow["iid"].ToString(), currentRow["SkuProps_Str"].ToString());
            }

        }

        //待打印界面行值改变时响应 主要是选中
        private void gvWaitPrint_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView senderIn = sender as GridView;
            Alading.Entity.Trade trade = senderIn.GetRow(e.RowHandle) as Alading.Entity.Trade;
            if (e.Column == WaitPrintIsSelected)
            {
                senderIn.BeginDataUpdate();
                trade.IsSelected = bool.Parse(e.Value.ToString());
                senderIn.EndDataUpdate();
            }
        }

        //已打印界面行值改变时的响应 主要是选中和单号填写
        private void gridViewPrinted_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView senderIn = sender as GridView;
            Alading.Entity.Trade trade = senderIn.GetRow(e.RowHandle) as Alading.Entity.Trade;
            if (e.Column == PrintedIsSelected)
            {
                senderIn.BeginDataUpdate();
                trade.IsSelected = bool.Parse(e.Value.ToString());
                senderIn.EndDataUpdate();
            }
        }

        //待打印界面填入物流单号
        private void gridViewPrinted_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView senderIn = sender as GridView;
            Alading.Entity.Trade trade = senderIn.GetRow(e.RowHandle) as Alading.Entity.Trade;
            if (e.Column == ShippingCode)
            {
                senderIn.BeginDataUpdate();
                trade.ShippingCode = e.Value.ToString();
                senderIn.EndDataUpdate();
            }
        }
        //待打印界面行值改变时的响应 主要是选中和单号填写
        private void gvWaitSendGood_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
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

        //主选项卡切换事件
        private void tabsMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            InitSelectMainTab();  //调用界面辅助函数绑定数据
            //默认绑定第一行数据
            _focusedTrade = GetCurrentGC().DefaultView.GetRow(0) as Alading.Entity.Trade;
            SetTradeDetailMessage(_focusedTrade);
        }

        //下方选项卡切换事件
        private void tabsDetailMessage_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            SetTradeDetailMessage(_focusedTrade);
        }
       
         //下方商品详情绑定 行值改变
        private void gvSaleInfo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow currentRow = gvSaleInfo.GetFocusedDataRow();
            if (currentRow != null)
            {
                ShowItemPropValue(currentRow["iid"].ToString(), currentRow["SkuProps_Str"].ToString());
            }
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
                            case SummitReturnType.Success:
                                SystemHelper.CreateFlowMessage(trade.CustomTid, "取消打印成功", "交易取消打印。待打印-->待确认", "订单打印");
                                break;
                            case SummitReturnType.StatusChanged:
                                SystemHelper.CreateFlowMessage(trade.CustomTid, "取消打印失败", "交易状态已经改变", "订单管理");
                                break;
                        }
                    }
                }
            }

            waitFrm.Close();
            InitSelectMainTab();
        }

        //打印订单 待打印-->已打印
        private void barPrintOrders_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    //TODO 打印出来该订单  使用学长提供的打印接口 并且完成时间戳比对
                    //if SUCCESS
                    if (TradeService.UpdateTradeLocalStatus(trade.CustomTid, LocalTradeStatus.Printed) == ReturnType.Success)
                    {
                        SystemHelper.CreateFlowMessage(trade.CustomTid, "打印成功", "订单提交成功。待打印-->已打印", "订单打印");
                    }
                }
            }
            waitFrm.Close();
            InitSelectMainTab();  //刷新界面
        }

        //手动填写订单 直接转换状态  待打印-->已打印
        private void barHasPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
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
                        if (TradeService.UpdateTradeLocalStatus(trade.CustomTid, LocalTradeStatus.Printed) == ReturnType.Success)
                        {
                            SystemHelper.CreateFlowMessage(trade.CustomTid, "提交成功", "订单提交成功。待打印-->已打印", "订单打印");
                        }
                    }
                }
                waitFrm.Close();
                InitSelectMainTab();  //刷新界面
            }
        }

        //重新打印 已打印-->待打印
        private void barRebackWaitPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult result = XtraMessageBox.Show("重新打印操作会将当前物流单号清空，并返回待打印状态，是否继续？", "确认", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            //确认修改 则开始更新
            GridView currentView = GetCurrentGV();//取得当前展示交易的GridView
            int totalRowCount = currentView.RowCount;//循环获取需要提交的Trade
            Alading.Entity.Trade trade = null;

            WaitDialogForm waitFrm = new WaitDialogForm(Alading.Taobao.Constants.OPERATE_DB_DATA);

            for (int runner = 0; runner < totalRowCount; runner++)
            {
                trade = currentView.GetRow(runner) as Alading.Entity.Trade;

                if (Convert.ToBoolean(trade.IsSelected))
                {
                    trade.LocalStatus = LocalTradeStatus.SummitNotPrint;
                    trade.ShippingCode = string.Empty;
                    if (TradeService.UpdateTradeLocalStatus(trade.CustomTid, trade.LocalStatus, trade.ShippingCode) == ReturnType.Success)
                    {
                        SystemHelper.CreateFlowMessage(trade.CustomTid, "重新打印", "重新打印。已打印-->待打印", "订单打印");
                    }
                }
            }
            waitFrm.Close();
            InitSelectMainTab();  //刷新界面
        }

        //将打印提交到待配货 更新物流单号并提交
        private void barBtnCommit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridView currentView = GetCurrentGV();//取得当前展示交易的GridView
            int totalRowCount = currentView.RowCount;//循环获取需要提交的Trade
            Alading.Entity.Trade trade = null;
            StringBuilder operateMessage = new StringBuilder();
            WaitDialogForm waitFrm = new WaitDialogForm(Alading.Taobao.Constants.OPERATE_DB_DATA);

            for (int runner = 0; runner < totalRowCount; runner++)
            {
                trade = currentView.GetRow(runner) as Alading.Entity.Trade;

                if (Convert.ToBoolean(trade.IsSelected))
                {
                    if(trade.ShippingCode==string.Empty)
                    {
                        operateMessage.Append("交易" + trade.CustomTid + ":未填入物流单号,未予提交！\n");
                        continue;
                    }
                    switch (TradeService.SummitShippingCode(trade.CustomTid, LocalTradeStatus.Printed, LocalTradeStatus.WaitAssort, trade.ShippingCode))
                    {
                        case 1:
                            SystemHelper.CreateFlowMessage(trade.CustomTid, "填入物流单号", "提交成功。已打印-->待配货。物流单号：" + trade.ShippingCode, "订单打印");
                            break;
                        case 2:
                            SystemHelper.CreateFlowMessage(trade.CustomTid, "填入物流单号", "提交失败。物流单号" + trade.ShippingCode + "已经被占用", "订单打印");
                            break;
                        case 3:
                            SystemHelper.CreateFlowMessage(trade.CustomTid, "填入物流单号", "提交失败。订单当前已不在待打印状态", "订单打印");
                            break;
                    }
                }
               
            }
            waitFrm.Close();
            if (operateMessage.Length != 0)
            {
                XtraMessageBox.Show(operateMessage.ToString());
            }
                InitSelectMainTab();  //刷新界面
        } 

        /// 将待配货返回到 待打印
        private void barBtnBackPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    switch (TradeService.SummitShippingCode(trade.CustomTid, LocalTradeStatus.WaitAssort, LocalTradeStatus.Printed, string.Empty))
                    {
                        case 1:
                            SystemHelper.CreateFlowMessage(trade.CustomTid, "待配货返回", "返回成功。待配货-->已打印", "订单打印");
                            break;
                        case 2:
                            break;
                        case 3:
                            SystemHelper.CreateFlowMessage(trade.CustomTid, "待配货返回", "提交失败。订单当前已不在待打印状态", "订单打印");
                            break;
                    }
                }
            }
            waitFrm.Close();
            InitSelectMainTab();  //刷新界面
        }

        #endregion

        #region 右面的按钮事件响应
        //待打印
        private void navBarWaitPrint_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabsMain.SelectedTabPage = tabWaitPrint;
        }

        //已打印
        private void navBarPrinted_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabsMain.SelectedTabPage = tabHasPrint;
        }

        //待配货
        private void navBarWaitAssort_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabsMain.SelectedTabPage = tabWaitAssort;
        }
        #endregion

        #region  绑定商品详情函数
        private void ShowItemPropValue(string iid, string sku_props_name)
        {
            Alading.Entity.Item vsItem = ItemService.GetItem(iid);
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

        #endregion
    }
}
