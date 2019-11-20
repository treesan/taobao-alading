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

namespace Alading.Forms.Trade.Controls
{
    public partial class TradePay : DevExpress.XtraEditors.XtraUserControl
    {

        #region  将数据绑定到界面方法LoadTrade

        /// 将数据库中的数据展示到GridView中 
        private void LoadTrade(DataSet dataSet)
        {
            try
            {
            _currentDataSet = dataSet;
            if (dataSet != null)
            {

                 #region  界面需求处理DataSet
                //为查询结果对应命名
                dataSet.Tables[0].TableName = "TradeSource";        //交易表
                dataSet.Tables[1].TableName = "OrderSource";        //订单表
                dataSet.Tables[2].TableName = "LackFlagSource";   //缺货标量表
                dataSet.Tables[3].TableName = "NotBuildSource";   //未关联库存表

                #region 界面处理交易缺货状态

                //通过存储过程返回的第三张表 包含了一笔交易的缺货标量 根据此表量来计算缺货值
                DataTable lackHelpTable = dataSet.Tables["LackFlagSource"];
                DataTable notBuildHelpTable = dataSet.Tables["NotBuildSource"];
                foreach (DataRow tradeRow in dataSet.Tables["TradeSource"].Rows)
                {
                    tradeRow["IsSelected"] = false; //设定选中值为False 防止异常
                    #region  构造正常，缺货，部分缺货状态
                    DataRow helpLackRow = lackHelpTable.Select("CustomTid ='" + tradeRow["CustomTid"].ToString() + "'").FirstOrDefault();
                    if (helpLackRow != null)
                    {
                        if (int.Parse(helpLackRow["TradeMinFlag"].ToString()) >= 0)
                        {
                            tradeRow["TradeIsLackProduct"] = LackProductOrNot.Normal;
                        }
                        else if (int.Parse(helpLackRow["TradeMaxFlag"].ToString()) < 0)
                        {
                            tradeRow["TradeIsLackProduct"] = LackProductOrNot.Lack;
                        }
                        else
                        {
                            tradeRow["TradeIsLackProduct"] = LackProductOrNot.PartLack;
                        }
                    }
                 #endregion

                    #region  构造未关联库存列表
                    DataRow notBuildRow = notBuildHelpTable.Select("CustomTid ='" + tradeRow["CustomTid"].ToString() + "'").FirstOrDefault();
                    if (notBuildRow != null)
                    {
                        tradeRow["TradeIsLackProduct"] = LackProductOrNot.NotBuildStock;
                    }
              
                    #endregion
               }  

                #endregion
                
                
                #region   初始化Order数据的列名
                dataSet.Tables["OrderSource"].Columns["iid"].Caption = "商品id";
                dataSet.Tables["OrderSource"].Columns["OuterID"].Caption = "商家编码";
                dataSet.Tables["OrderSource"].Columns["oid"].Caption = "订单号";
                dataSet.Tables["OrderSource"].Columns["ItemName"].Caption = "商品名";
                dataSet.Tables["OrderSource"].Columns["sku_properties_name"].Caption = "商品属性";
                dataSet.Tables["OrderSource"].Columns["LeftQuantity"].Caption = "库存剩余量";
                dataSet.Tables["OrderSource"].Columns["num"].Caption = "购买数";
                dataSet.Tables["OrderSource"].Columns["price"].Caption = "商品价格";
                dataSet.Tables["OrderSource"].Columns["payment"].Caption = "实付金额"; 
                dataSet.Tables["OrderSource"].Columns["ItemType"].Caption = "宝贝类别";
                dataSet.Tables["OrderSource"].Columns["OrderType"].Caption = "商品性质";
                dataSet.Tables["OrderSource"].Columns["orderDetail"].Caption = "修改订单";
                dataSet.Tables["OrderSource"].Columns["ProductIsLack"].Caption = "缺货状态";
                dataSet.Tables["OrderSource"].Columns["OrderTimeStamp"].Caption = "订单时间戳";
                dataSet.Tables["OrderSource"].Columns["adjust_fee"].Caption = "调整价格";
                dataSet.Tables["OrderSource"].Columns["StockUnitName"].Caption = "计量单位";
               #endregion
                //建立Trade和Order的外键关系
                dataSet.Relations.Add(Constants.TRADE_ORDER_RELATION, dataSet.Tables["TradeSource"].Columns["CustomTid"],
                      dataSet.Tables["OrderSource"].Columns["CustomTid"]);
              
                

                #endregion

                // 数据绑定相关连操作
                GridControl gcParentGC = GetCurrentGC();
                GridView gvChildGV = GetCurrentOrderGV();
                GridView gvFatherGV = GetCurrentTradeGV();
                gcParentGC.DataSource = dataSet.Tables["TradeSource"];
                gcParentGC.ForceInitialize();//强制初始化
                gvFatherGV.BestFitColumns();
                gvChildGV.BestFitColumns();
                
                gcParentGC.LevelTree.Nodes.Add(Alading.Taobao.Constants.TRADE_ORDER_RELATION, gvChildGV);//建立联级绑定

                //获取当前页总行数 并在下方显示
                gvFatherGV.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }

        #endregion

        #region 选项卡,Gird事件响应函数

        #region  选项卡选中Tabs改变事件响应 PageChanged
        private void tradeTabMain_SelectedPageChanged(object sender, TabPageChangedEventArgs e)    
        {
            #region  设置修改界面的可见性
            GridControl currentGC= GetCurrentGC();
            if (currentGC == gcWaitPrint || currentGC==gcWaitConfirm)
            {
                txtBuyerName.Properties.ReadOnly = false;
                txtMobile.Properties.ReadOnly = false;
                txtZip.Properties.ReadOnly = false;
                txtPhone.Properties.ReadOnly = false;
                cmbShippingCompany.Properties.ReadOnly = false;
                cmbShippingTemplate.Properties.ReadOnly = false;
                txtReceiverAddress.Properties.ReadOnly = false;
                cmbReceiverState.Properties.ReadOnly = false;
                cmbReceiverCity.Properties.ReadOnly = false;
                cmbReceiverDistrict.Properties.ReadOnly = false;
                memoBuyerMemo.Properties.ReadOnly = false;
                memoBuyerMessage.Properties.ReadOnly = false;
                btnStoreReceiverMsg.Visible = true;
            }
            else
            {
                txtBuyerName.Properties.ReadOnly = true;
                txtMobile.Properties.ReadOnly = true;
                txtZip.Properties.ReadOnly = true;
                txtPhone.Properties.ReadOnly = true;
                cmbShippingCompany.Properties.ReadOnly = true;
                cmbShippingTemplate.Properties.ReadOnly = true;
                txtReceiverAddress.Properties.ReadOnly = true;
                cmbReceiverState.Properties.ReadOnly = true;
                cmbReceiverCity.Properties.ReadOnly = true;
                cmbReceiverDistrict.Properties.ReadOnly = true;
                memoBuyerMemo.Properties.ReadOnly = true;
                memoBuyerMessage.Properties.ReadOnly = true;
                btnStoreReceiverMsg.Visible = false;
            }
#endregion
           
            InitSelectTab();
            /*如果是打印页  显示相应的信息*/
            if (tradeTabMain.SelectedTabPage == tabWaitPrint)
            {
                if (GetCurrentTradeGV().GetFocusedDataSourceRowIndex() == 0)
                {
                    LoadTradeDetail();
                }
            }
        }

        #endregion

        #region  主GridView展开时事件响应 MasterRowExpanded
        private void gvParentGV_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            GridView senderIn = sender as GridView;
            GridView aView = (DevExpress.XtraGrid.Views.Grid.GridView)senderIn.GetDetailView(e.RowHandle, 0);

            //默认绑定第一条数据
            //DataRow defaultRow =
            //    GetCurrentTradeGV().GetDataRow(e.RowHandle).GetChildRows(Constants.TRADE_ORDER_RELATION).FirstOrDefault();
            //ShowItemPropValue(defaultRow["iid"].ToString(), defaultRow["sku_properties_name"].ToString());

            #region 界面相关处理
            if (aView != null)
            {
                aView.OptionsBehavior.Editable = false;
                //隐藏子GridView不显示的列
                if (aView.Columns["CustomTid"] != null)
                {
                    aView.Columns["CustomTid"].Visible = false;
                    aView.Columns["tid"].Visible = false;
                    aView.Columns["iid"].Visible = false;
                    aView.Columns["OrderTimeStamp"].Visible = false;
                    aView.Columns["TradeOrderCode"].Visible = false;
                    //如果不是待确认和待打印 则隐藏修改订单操作项

                    if(!(GetCurrentGC()==gcWaitConfirm))
                    {
                        aView.Columns[0].Visible = false;
                    }
                }
            #endregion
            }
        }

        #endregion

        #region  主GridView行值改变时响应  CellValueChanged
        private void gvParentGV_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            (sender as GridView).UpdateCurrentRow();
        }

        #endregion

        #region 主GridView行点击事件响应RowCellClick
        public void gvParentGV_RowCellClick(object sender, RowCellClickEventArgs e)
        {
           // 隐藏组合商品
            detailAessemble.PageVisible = false;
            GridView senderIn = sender as GridView;
            DataRow rowMatch = senderIn.GetDataRow(e.RowHandle);
            /*发票信息*/
            if (rowMatch != null && rowMatch != _currentTradeRow)//避免多次响应同一重复事件
            {
                // 记录当前 焦点位置的所在Trade行
                _currentTradeRow = rowMatch;
                SetTradeDetailMessages(rowMatch);
            }

            #region  如果所选行拆分过 则拆分按钮不可用
            if (Convert.ToBoolean(rowMatch["IsSplited"].ToString()))
            {
                btnSpilt.Enabled = false;
            }
            else
            {
                btnSpilt.Enabled = true;
            }
            #endregion

            #region  未锁定  解锁按钮不能用  若为锁定  锁定按钮不能用
            if (rowMatch["LockedUserName"].ToString() == UNLOCKED.VALUE)
            {
                unlock.Enabled = false;
                Lock.Enabled = true;
            }
            else
            {
                Lock.Enabled = false;
                unlock.Enabled = true;
            }
            #endregion
        }
        #endregion

        #region  主GridView 行值改变时事件响应函数 CellValueChanging
        
        private void gvParentGV_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            /*获得DataRow,并且更新数据*/
            GridView senderIn = sender as GridView;
            DataRow row = senderIn.GetDataRow(e.RowHandle);
            if (e.Column.AbsoluteIndex == 1 )
            {
                senderIn.BeginDataUpdate();
                row["IsSelected"] = e.Value;
                senderIn.EndDataUpdate();

                #region  未锁定  解锁按钮不能用  若为锁定  锁定按钮不能用
                if (row["LockedUserName"].ToString() == UNLOCKED.VALUE)
                {
                    unlock.Enabled = false;
                    Lock.Enabled = true;
                }
                else
                {
                    Lock.Enabled = false;
                    unlock.Enabled = true;
                }
                #endregion
            }
            /*是否开发票处理代码*/
            if (e.Column == HasInvoice)
            {
                Alading.Entity.Trade trade = TradeService.GetTrade(row["CustomTid"].ToString());
                if (row != null)
                {
                    if (Convert.ToBoolean(row["HasInvoice"].ToString()) == false)
                    {
                        trade.HasInvoice = true;
                        if (TradeService.UpdateTrade(trade) == ReturnType.Success)
                        {
                            detailTicketMessage.PageVisible = true;
                            tabsTradeDetail.SelectedPageChanged -= new TabPageChangedEventHandler(tabCtrlTradeDetail_SelectedPageChanged);
                            tabsTradeDetail.SelectedTabPageIndex = 3;
                            tabsTradeDetail.SelectedPageChanged += new TabPageChangedEventHandler(tabCtrlTradeDetail_SelectedPageChanged);
                        }
                        else
                            /*数据库没有更新不能该界面*/
                            row["HasInvoice"] = "否";
                    }
                    else
                    {
                        trade.HasInvoice = false;
                        if (TradeService.UpdateTrade(trade) == ReturnType.Success)
                        {
                            detailTicketMessage.PageVisible = false;
                        }
                        else
                            row["HasInvoice"] = "是";
                    }
                }
            }
        }
        #endregion

        #region  子GridView行点击时事件响应 RowCellClick
        private void gvChildGV_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            GridView gv = (sender as GridView).GridControl.GetViewAt(e.Location) as GridView;
            if (gv != null)
            {
                GridHitInfo hi = gv.CalcHitInfo(e.Location);
                if (hi.Column != null && hi.InRowCell)
                {
                    //记录当前焦点所在的OrderRow
                    DataRow currentMatch = gv.GetDataRow(hi.RowHandle);

                    #region 点击种类 如果是组合商品 下面显示
                    if (currentMatch["ItemType"].ToString() == ItemType.ConbineProduct.ToString())
                    {

                        detailAessemble.PageVisible = true;
                        tabsTradeDetail.SelectedTabPage = detailAessemble;

                    }
                    else
                    {
                        detailAessemble.PageVisible = false;
                    }
                    #endregion

                    #region 如果不是连续点击 则绑定数据

                    //防止同一重复事件多次加载
                    if (_currentOrderRow != currentMatch)
                    {
                        //记录当前焦点所在的OrderRow
                        _currentOrderRow = currentMatch;

                        //记录当前订单的父Trade行
                        _currentTradeRow = currentMatch.GetParentRow(Alading.Taobao.Constants.TRADE_ORDER_RELATION);

                        //绑定TradeDetail信息
                        SetTradeDetailMessages(_currentTradeRow);

                        //绑定商品信息到面板
                        ShowItemPropValue(currentMatch["iid"].ToString(), currentMatch["sku_properties_name"].ToString());
                    }
                    #endregion

                    #region  如果选中列为’修改订单‘，则执行处理代码
                    if (hi.Column.ColumnHandle == 0)//
                    {
                        //如果是未建立库存 则不作处理
                        if (_currentOrderRow[1].ToString() == LackProductOrNot.NotBuildStock)
                        {
                            XtraMessageBox.Show("当前订单未关联库存，不能修改订单信息");
                            return;
                        }
                        //取得当前选中订单的数据库真实值
                        View_TradeStock focusedOrder =
                            View_TradeStockService.GetView_TradeStock(p => p.TradeOrderCode == _currentOrderRow["TradeOrderCode"].ToString()).FirstOrDefault();
                        if (focusedOrder == null)
                            return;
                        if(focusedOrder.LocalStatus!=LocalTradeStatus.HasNotSummit)
                        {
                            XtraMessageBox.Show("该订单的交易状态已经改变，不能换货！");
                            return;
                        }
                        #region 利用时间戳来防止并发
                        DialogResult result = DialogResult.Yes;
                        if (!SystemHelper.CompareTimeStamp(_currentOrderRow["OrderTimeStamp"] as byte[], focusedOrder.OrderTimeStamp))
                        {
                            //如果订单信息已经被改，询问是否继续修改
                            result = XtraMessageBox.Show("订单信息已被修改,继续修改(YES)/查看流程信息(NO)", "订单修改",
                                MessageBoxButtons.YesNo);
                            if (result == DialogResult.No)//如果选择为NO，则查看流程信息，并返回
                            {
                                tabsTradeDetail.SelectedTabPage = detailTabFlowMsg;
                                return;
                            }
                        }
                        if (result == DialogResult.Yes)//如果result=YES，则继续修改
                        {
                            //传入时间戳，只有在最后修改提交前验证才能保证不并发
                            ModifyOrder modifyOrder = new ModifyOrder(focusedOrder, _currentOrderRow["OrderTimeStamp"] as byte[]);//转入修改订单页面
                            modifyOrder.ShowDialog();

                            switch (modifyOrder.DialogResult)
                            {
                                case DialogResult.Ignore:
                                    return;
                                case DialogResult.OK:
                                    InitSelectTab();
                                    break;
                                case DialogResult.Cancel:
                                    tabsTradeDetail.SelectedTabPage = detailTabFlowMsg;
                                    break;
                            }
                        }
                        else//否则，直接返回
                        {
                            return;
                        }
                        #endregion
                    }
                    #endregion

                    
                }
            }
        }

        #endregion

        #region  下方界面的事件处理及事件绑定
        private void tabCtrlTradeDetail_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (_currentTradeRow != null)
            {
                SetTradeDetailMessages(_currentTradeRow);
            }
        }

        //绑定数据到买家数据
        private void BuyerInforDetail(string customTid)
        {
            string buyerNick = TradeService.GetTrade(customTid).buyer_nick;
            List<Alading.Entity.Consumer> buyerList = ConsumerService.GetConsumer(p => p.nick == buyerNick);
            if (buyerList.Count > 0)
            {
                BuyerNick.Text = buyerNick;
                Birthday.Text = (buyerList.First().birthday ?? DateTime.Parse(Alading.Taobao.Constants.DEFAULT_TIME)).ToString("yyyy-MM-dd");
                register.Text = (buyerList.First().created ?? DateTime.Parse(Alading.Taobao.Constants.DEFAULT_TIME)).ToString("yyyy-MM-dd");
                lastLogin.Text = (buyerList.First().last_visit ?? DateTime.Parse(Alading.Taobao.Constants.DEFAULT_TIME)).ToString("yyyy-MM-dd");
                zip.Text = buyerList.First().buyer_zip;
                if (buyerList.First().sex == "f")
                {
                    sex.SelectedIndex = 0;
                }
                else if (buyerList.First().sex == "m")
                {
                    sex.SelectedIndex = 1;
                }
                level.Text = "信用等级:" + buyerList.First().level.ToString();
                score.Text = "信用总分:" + buyerList.First().score.ToString();
                total.Text = buyerList.First().buyer_total_num.ToString();
                goodNum.Text = buyerList.First().buyer_good_num.ToString();
                ((ILinearGauge)rating.Gauges[0]).Scales[0].Value = (float)buyerList.First().level * 90 / 20;
            }
        }

        // 绑定Trade的货物信息数据到送货信息Tab
        private void ReceiverGoodsDetail(string customTid)
        {
            try
            {
                Alading.Entity.Trade tradeInRow = TradeService.GetTrade(customTid);
                txtBuyerName.Text = tradeInRow.receiver_name;
                txtMobile.Text = tradeInRow.receiver_mobile;
                txtPhone.Text = tradeInRow.receiver_phone;
                txtReceiverAddress.Text = tradeInRow.receiver_address;
                txtZip.Text = tradeInRow.receiver_zip;
                memoBuyerMemo.Text = tradeInRow.buyer_memo;
                memoBuyerMessage.Text = tradeInRow.buyer_message;
                /* 初始化买家省市区的信息并绑定数据*/
                cmbReceiverState.Properties.Items.Clear();
                cmbReceiverCity.Properties.Items.Clear();
                cmbReceiverDistrict.Properties.Items.Clear();
                cmbReceiverState.Text = tradeInRow.receiver_state.ToString();
                cmbReceiverCity.Text = tradeInRow.receiver_city;
                cmbReceiverDistrict.Text = tradeInRow.receiver_district;
                //绑定物流订单数据
                BoundCompanyMessage(tradeInRow.LastShippingType);
                BoundTemplateMessage(tradeInRow.LogisticCompanyCode);
                cmbShippingCompany.EditValue = tradeInRow.LogisticCompanyCode;
                cmbShippingTemplate.EditValue = tradeInRow.TemplateCode;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //根据选中Tab来设定界面值 动态加载
        private void SetTradeDetailMessages(DataRow currentRow)
        {
            try
            {
                //发票显示
                detailTicketMessage.PageVisible = Convert.ToBoolean(currentRow["HasInvoice"].ToString());
                string customTid = currentRow["CustomTid"].ToString();
                switch (tabsTradeDetail.SelectedTabPageIndex)
                {
                    /*收货信息填写*/
                    case 0:
                        ReceiverGoodsDetail(customTid);
                        break;
                    /*买家信息填写*/
                    case 1:
                        BuyerInforDetail(customTid);
                        break;
                    /*TODO 绑定Trade的流水信息到处理流程Tab  暂时TradeInfo里面没数据*/
                    case 2:
                        detailGCFlowMessage.DataSource = TradeInfoService.GetTradeInfo(p => p.CustomTid == customTid);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        gcAssembleDetail.DataSource= View_TradeAssembleStockService.GetViewTradeAssembleDataSet(_currentOrderRow["TradeOrderCode"].ToString()).Tables[0];
                        break;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        //合并单绑定商品面板数据
        private void gvAssembleDetail_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            GridView gv = (sender as GridView).GridControl.GetViewAt(e.Location) as GridView;
            if (gv != null)
            {
                GridHitInfo hi = gv.CalcHitInfo(e.Location);
                if (hi.Column != null && hi.InRowCell)
                {
                    //记录当前焦点所在的OrderRow
                    DataRow currentMatch = gv.GetDataRow(hi.RowHandle);
                    //绑定商品信息到面板
                    ShowItemPropValue(currentMatch["iid"].ToString(), currentMatch["SkuProps_Str"].ToString());
                }
            }
        }

        #region  绑定物流相关信息
        //绑定公司信息
        private void BoundCompanyMessage(string type)
        {
            List<LogisticCompany> companys = LogisticCompanyService.GetAllLogisticCompany();
            cmbShippingCompany.Properties.DataSource = companys;
        }

        //绑定公司模板信息
        private void BoundTemplateMessage(string companyCode)
        {
            List<LogisticCompanyTemplate> templates = LogisticCompanyTemplateService.GetLogisticCompanyTemplate(p => p.LogisticCompanyCode==companyCode);
            cmbShippingTemplate.Properties.DataSource = templates;
        }

        //值改变的时候重新绑定 模板源
       private void  cmbShippingCompany_EditValueChanged(object sender,EventArgs e)
       {
           List<LogisticCompanyTemplate> templates = LogisticCompanyTemplateService.GetLogisticCompanyTemplate(p => p.LogisticCompanyCode == cmbShippingCompany.EditValue.ToString());
           cmbShippingTemplate.Properties.DataSource = templates;
           try
           {
               cmbShippingTemplate.EditValue = templates.FirstOrDefault().TemplateCode;
           }
           catch (Exception ex)
           {
               cmbShippingTemplate.EditValue = string.Empty;
           }
       }

        #endregion 

        #region   对三个区域ComboBoxEdit的数据绑定 及 事件处理代码
        // 在Enter事件的时候动态绑定
        private void cmbReceiverState_Enter(object sender, EventArgs e)
        {
            if (cmbReceiverState.Properties.Items.Count < 2 && cmbReceiverState.SelectedItem != null)
            {
                BandingAreasMessage(2, cmbReceiverState.SelectedItem.ToString());
                BandingAreasMessage(3, cmbReceiverCity.SelectedItem.ToString());
                BandingAreasMessage(4, cmbReceiverDistrict.SelectedItem.ToString());
            }
        }
        // 在Enter事件的时候动态绑定
        private void cmbReceiverCity_Enter(object sender, EventArgs e)
        {
            if (cmbReceiverState.Properties.Items.Count < 2 && cmbReceiverCity.SelectedItem != null)
            {
                BandingAreasMessage(3, cmbReceiverCity.SelectedItem.ToString());
                BandingAreasMessage(4, cmbReceiverDistrict.SelectedItem.ToString());
            }
        }
        // 在Enter事件的时候动态绑定
        private void cmbReceiverDistrict_Enter(object sender, EventArgs e)
        {
            if (cmbReceiverState.Properties.Items.Count < 2 && cmbReceiverDistrict.SelectedItem != null)
            {
                BandingAreasMessage(4, cmbReceiverDistrict.SelectedItem.ToString());
            }
        }
        // 当选定项改变时触发
        private void cmbReceiverState_SelectedIndexChanged(object sender, EventArgs e)
        {
            BandingAreasMessage(3, null);
            BandingAreasMessage(4, null);

        }
        //当选定向改变时触发
        private void cmbReceiverCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            BandingAreasMessage(4, null);
        }

        /// 通过传入的参数来对ComboBoxEdit进行绑定 
        private void BandingAreasMessage(int areaType, string defaultSelect)
        {


            ComboBoxEdit cmbParent = null;
            ComboBoxEdit cmbCurrent = null;

            switch (areaType)
            {
                case 2:
                    cmbParent = null;
                    cmbCurrent = cmbReceiverState;
                    break;
                case 3:
                    cmbParent = cmbReceiverState;
                    cmbCurrent = cmbReceiverCity;
                    break;
                case 4:
                    cmbParent = cmbReceiverCity;
                    cmbCurrent = cmbReceiverDistrict;
                    break;
            }

            //获得上一级comboBox的选定项的id
            string parentId = string.Empty;
            int parentType = areaType - 1;
            if (cmbParent != null)
            {
                try
                {
                    parentId = AreaService.GetArea(a => (a.name == cmbParent.SelectedItem.ToString()) && (a.type == parentType)).FirstOrDefault().id;
                }
                catch (Exception e)
                {
                    return;
                }
            }
            else
            {
                parentId = "1";
            }
            List<Area> areaList = AreaService.GetArea(a => a.parent_id == parentId);
            cmbCurrent.Properties.Items.Clear();
            foreach (Area area in areaList)
            {
                cmbCurrent.Properties.Items.Add(area.name);
                /*设置选定项 */
                if (defaultSelect != null && defaultSelect == area.name)
                {
                    cmbCurrent.SelectedItem = area.name;
                }
            }
            if (defaultSelect == null)
            {
                if (areaList.FirstOrDefault() != null)
                    cmbCurrent.SelectedItem = areaList.FirstOrDefault().name;
                else
                    cmbCurrent.Text = "未指定";
            }
        }
        #endregion
        #endregion

        #endregion

        #region  右面DockPanel的响应函数

        /// 所有订单
        private void navBarAllTrade_LinkClicked(object sender, EventArgs e)
        {
            TabsClear(tabAllTrade);
            tabAllTrade.PageVisible = true;
        }

        /// 未付款订单
        private void navBarUnpaid_LinkClicked(object sender, EventArgs e)
        {
            TabsClear(tabUnPaid);
            tabWaitConfirm.PageVisible = true;
            tabWaitPrint.PageVisible = true;
            tabPrinted.PageVisible = true;
            tabWaitAssort.PageVisible = true;
            tabHasSended.PageVisible = true;
            tabWaitEvaluate.PageVisible = true;
            tabCompleted.PageVisible = true;
            tabUnPaid.PageVisible = true;
            tabRefund.PageVisible = true;
            tabHasAssorted.PageVisible = true;
        }

        /// 已付款订单
        private void navBarPaid_LinkClicked(object sender, EventArgs e)
        {
            TabsClear(tabWaitConfirm);
            tabWaitConfirm.PageVisible = true;
            tabWaitPrint.PageVisible = true;
            tabPrinted.PageVisible = true;
            tabWaitAssort.PageVisible = true;
            tabHasSended.PageVisible = true;
            tabWaitEvaluate.PageVisible = true;
            tabCompleted.PageVisible = true;
            tabUnPaid.PageVisible = true;
            tabRefund.PageVisible = true;
            tabHasAssorted.PageVisible = true;
        }

        /// 退款订单
        private void navBarRefund_LinkClicked(object sender, EventArgs e)
        {
            TabsClear(tabRefund);
            tabWaitConfirm.PageVisible = true;
            tabWaitPrint.PageVisible = true;
            tabPrinted.PageVisible = true;
            tabWaitAssort.PageVisible = true;
            tabHasSended.PageVisible = true;
            tabWaitEvaluate.PageVisible = true;
            tabCompleted.PageVisible = true;
            tabUnPaid.PageVisible = true;
            tabRefund.PageVisible = true;
            tabHasAssorted.PageVisible = true;
            
        }

        /// 历史订单
        private void navBarHistory_LinkClicked(object sender, EventArgs e)
        {
            TabsClear(tabHistroy);
            tabHistroy.PageVisible = true;
        }

        /// 通过传iid来绑定商品属性到面板
        private void ShowItemPropValue(string iid, string sku_props_name)
        {
            Alading.Entity.Item vsItem = ItemService.GetItem(iid);
            if(vsItem==null) //如果为空 则不绑定
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

    }
}