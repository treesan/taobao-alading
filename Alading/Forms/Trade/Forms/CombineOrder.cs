using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Entity;
using Alading.Business;
using Alading.Core.Enum;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using Alading.Utils;
using Alading.Properties;
using System.IO;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Alading.Forms.Trade.Forms
{
    public partial class CombineOrder : XtraForm
    {
        #region 全局变量
        List<string> customTidList = new List<string>();

        List<View_TradeStock> curTradeList = new List<View_TradeStock>();

        List<IGrouping<string, View_TradeStock>> TradeStockList = new List<IGrouping<string, View_TradeStock>>();

        List<Alading.Entity.TradeOrder> parentTradeOrderList = new List<Alading.Entity.TradeOrder>();

        Alading.Entity.Trade trade = new Alading.Entity.Trade();

        Alading.Entity.Trade FatherTrade = new Alading.Entity.Trade();

        List<Alading.Entity.Trade> tradeList = new List<Alading.Entity.Trade>();

        //用于标识是合并种类
        string CombineFlag = null;

        //订单副本
        List<TradeOrderStock> tradeOrderList = new List<TradeOrderStock>();
        #endregion

        private void CombineOrder_Load(object sender, EventArgs e)
        {
            //组合商品选卡隐藏
            combineProduct.PageEnabled = false;

            //撤消合并按钮不可用
            Rollback.Enabled = false;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="CustomTidList"></param>
        /// <param name="combine_flag"></param>
        public CombineOrder(List<string> CustomTidList, string combine_flag)
        {
            InitializeComponent();

            CombineFlag = combine_flag;
            customTidList = CustomTidList;
            /*初始化*/
            init(customTidList, CombineFlag);
        }

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="CustomTidList"></param>
        /// <param name="combine_flag"></param>
        private void init(List<string> CustomTidList, string combine_flag)
        {
            foreach (string CustomTid in customTidList)
            {
                curTradeList.AddRange(View_TradeStockService.GetView_TradeStock(q => q.CustomTid == CustomTid));
                TradeStockList = curTradeList.GroupBy(q => q.CustomTid).ToList();
            }

            /*交易还原成一个交易的时候  提交按钮隐藏*/
            if (CombineFlag == combineStyle.ConbineTradeFlg)
            {
                BtnCombineSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                saveBtn.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            /*不同交易合并成一个交易的时候，保存按钮隐藏*/
            if (CombineFlag == combineStyle.ConbinePrintFlg)
            {
                BtnCombineSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                saveBtn.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            /*数据展示*/
            LoadData(TradeStockList, gvTradePst, gcWaitPst);
        }

        /// <summary>
        /// 数据展示
        /// </summary>
        /// <param name="TradeStockList"></param>
        /// <param name="CurrentView"></param>
        /// <param name="CurrentGc"></param>
        private void LoadData(List<IGrouping<string, View_TradeStock>> TradeStockList, GridView CurrentView, GridControl CurrentGc)
        {

            DataSet dataSet = new DataSet();
            //从XML文件读出暂存数据库表格框架 包含TradeList表和OrderList表
            MemoryStream stream = new MemoryStream(Resources.TradeStockSchema);
            try
            {
                dataSet.ReadXmlSchema(stream);
            }
            finally
            {
                stream.Close();
            }
            #region 初始化数据
            foreach (IGrouping<string, View_TradeStock> tradeCur in TradeStockList)
            {
                /* trade表初始化*/
                DataRow tradeRow = dataSet.Tables["TradeList"].NewRow();
                InitTradeRow(tradeRow, tradeCur);
                dataSet.Tables["TradeList"].Rows.Add(tradeRow);

                int productAccount = tradeCur.Where(o => o.OrderType == emumOrderType.SellGoods).Count(); //用于消除赠品的影响
                foreach (View_TradeStock orderCurrent in tradeCur)
                {
                    /* order表初始化  */
                    DataRow orderRow = dataSet.Tables["OrderList"].NewRow();
                    InitOrderRow(orderRow, orderCurrent);
                    dataSet.Tables["OrderList"].Rows.Add(orderRow);
                }
                // 数据绑定相关连操作
                CurrentGc.DataSource = dataSet.Tables["TradeList"];
                CurrentGc.ForceInitialize();//强制初始化
                CurrentView.BestFitColumns();
                gcWaitPst.LevelTree.Nodes.Add(Alading.Taobao.Constants.TRADE_ORDER_RELATION, gvOrderPst);//建立联级绑定

            #endregion
            }
        }

        #region  Table赋值函数
        /// <summary>
        /// 将一条View_TradeStock的信息赋值到TradeRow
        /// </summary>
        /// <param name="tradeRow"></param>
        /// <param name="tradeCur"></param>
        private void InitTradeRow(DataRow tradeRow, IGrouping<string, View_TradeStock> tradeCurList)
        {
            View_TradeStock tradeCur = tradeCurList.FirstOrDefault();
            tradeRow["CustomTid"] = tradeCur.CustomTid;
            tradeRow["IsSelected"] = false;
            tradeRow["type"] = tradeCur.type;
            tradeRow["TradePayment"] = tradeCur.TradePayment;
            tradeRow["tid"] = tradeCur.tid;
            tradeRow["created"] = tradeCur.created;
            tradeRow["buyer_nick"] = tradeCur.buyer_nick;
            tradeRow["seller_nick"] = tradeCur.seller_nick;
            tradeRow["post_fee"] = tradeCur.post_fee;
            tradeRow["tradeTotalFee"] = tradeCur.TradeTotalFee;
            tradeRow["HasInvoice"] = tradeCur.HasInvoice == false ? "否" : "是";
            tradeRow["post_fee"] = tradeCur.post_fee;
            tradeRow["receiver_state"] = tradeCur.receiver_state;
            tradeRow["receiver_city"] = tradeCur.receiver_city;
            tradeRow["receiver_name"] = tradeCur.receiver_name;
            tradeRow["receiver_zip"] = tradeCur.receiver_zip;
            tradeRow["receiver_district"] = tradeCur.receiver_district;
            tradeRow["receiver_address"] = tradeCur.receiver_address;
        }

        /// <summary>
        /// 将一条View_TradeStock的信息赋值到TradeRow
        /// </summary>
        /// <param name="orderRow"></param>
        /// <param name="orderCur"></param>
        /// <param name="orderAccount"></param>
        private void InitOrderRow(DataRow orderRow, View_TradeStock orderCur)
        {
            orderRow["tid"] = orderCur.tid;
            orderRow["iid"] = orderCur.iid;
            orderRow["oid"] = orderCur.oid;
            orderRow["CustomTid"] = orderCur.CustomTid;
            orderRow["orderTotalFee"] = orderCur.total_fee;
            orderRow["ItemName"] = orderCur.ItemName;
            orderRow["sku_properties_name"] = orderCur.sku_properties_name;
            orderRow["num"] = orderCur.num;
            orderRow["price"] = orderCur.price;
            orderRow["payment"] = orderCur.payment;
            orderRow["OrderType"] = orderCur.OrderType;
            orderRow["adjust_fee"] = orderCur.adjust_fee;
            orderRow["ItemType"] = orderCur.ItemType;
        }

        /// <summary>
        /// 将一条订单相关信息展示到一行上面去
        /// </summary>
        /// <param name="orderRow">展示行</param>
        /// <param name="orderCur">展示订单</param>
        private void InitOrderRow(DataRow orderRow, TradeOrderStock tradeOrderStock)
        {
            TradeOrder orderCur = tradeOrderStock.order;
            orderRow["num"] = orderCur.num;
            orderRow["CustomTid"] = orderCur.CustomTid;
            orderRow["sku_properties_name"] = orderCur.sku_properties_name;
            orderRow["price"] = orderCur.price;
            orderRow["OrderType"] = orderCur.OrderType;
            orderRow["orderTotalFee"] = orderCur.total_fee;
            orderRow["TradeOrderCode"] = orderCur.TradeOrderCode;
            orderRow["adjust_fee"] = orderCur.adjust_fee;
            orderRow["payment"] = orderCur.payment;
            orderRow["ItemType"] = tradeOrderStock.itemType;
            orderRow["ItemName"] = tradeOrderStock.itemName;
            orderRow["iid"] = orderCur.iid;
            if (CombineFlag == combineStyle.ConbineTradeFlg)
            {
                orderRow["tid"] = trade.tid;
            }
        }
        #endregion

        /// <summary>
        /// MasterRowExpanded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvTradePst_MasterRowExpanded(object sender, DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs e)
        {
            GridView senderIn = sender as GridView;
            GridView aView = (DevExpress.XtraGrid.Views.Grid.GridView)senderIn.GetDetailView(e.RowHandle, 0);
            if (aView != null)
            {
                aView.OptionsBehavior.Editable = false;
                aView.BestFitColumns();
                //隐藏不显示列
                if (aView.Columns["CustomTid"] != null)
                {
                    aView.Columns["CustomTid"].Visible = false;
                    aView.Columns["LeftQuantity"].Visible = false;
                    aView.Columns["lackProductOrNot"].Visible = false;
                    aView.Columns["StockTimeStamp"].Visible = false;
                    aView.Columns["OuterID"].Visible = false;
                    aView.Columns["orderDetail"].Visible = false;
                    aView.Columns["TradeOrderCode"].Visible = false;
                    aView.Columns["oid"].Visible = false;
                    aView.Columns["iid"].Visible = false;
                }
            }
        }

        /// <summary>
        /// CellValueChanging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvTradePst_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == IsSelected)
            {
                DataRow row = gvTradePst.GetFocusedDataRow();
                gvTradePst.BeginDataUpdate();
                row["IsSelected"] = e.Value;
                gvTradePst.EndDataUpdate();
            }/*if*/
        }

        /// <summary>
        /// combine  buttton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void combine_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*合并还原成一个交易*/
            if (CombineFlag == combineStyle.ConbineTradeFlg)
            {
                ConbineTradeFlg();
            }

            /*合并成一个交易*/
            if (CombineFlag == combineStyle.ConbinePrintFlg)
            {
                ConbinePrintFlg();
                Rollback.Enabled = true;
            }
        }

        /// <summary>
        /// 合并还原成一个交易
        /// </summary>
        private void ConbineTradeFlg()
        {
            #region  交易合并 还原成一个交易
            if (DialogResult.Cancel == XtraMessageBox.Show(Alading.Properties.Resources.TidNotSame,
                                                                                                                                  Alading.Properties.Resources.Imformation, MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
            {
                return;
            }

            int OrdeFlg = 0;
            string customStr = customTidList[0];
            string customParent = null;
            string customChild = null;

            List<Alading.Entity.TradeOrder> childTradeOrderList = new List<Alading.Entity.TradeOrder>();
            Alading.Entity.TradeOrder order = new Alading.Entity.TradeOrder();

            /*找出父交易的customtid  与 子交易的customtid*/
            foreach (string str in customTidList)
            {
                if (str.Length > customStr.Length)
                {
                    customParent = customStr;
                    customChild = str;
                }/*if*/
                else
                {
                    customParent = str;
                    customChild = customStr;
                }/*else*/
            }/*foreach*/

            /*将父交易的订单列表找出*/
            parentTradeOrderList = TradeOrderService.GetTradeOrder(q => q.CustomTid == customParent);
            childTradeOrderList = TradeOrderService.GetTradeOrder(q => q.CustomTid == customChild);

            for (int i = 0; i < parentTradeOrderList.Count; i++)
            {
                /*找到相应的子订单*/
                order = childTradeOrderList.Where(q => q.oid == parentTradeOrderList[i].oid).FirstOrDefault();
                if (order != null)
                {
                    /*数量合并*/
                    parentTradeOrderList[i].num += order.num;
                    /*总费合并*/
                    parentTradeOrderList[i].total_fee = parentTradeOrderList[i].price * parentTradeOrderList[i].num;
                    /*应付金额合并*/
                    parentTradeOrderList[i].payment = parentTradeOrderList[i].total_fee
                                                                       + parentTradeOrderList[i].adjust_fee + parentTradeOrderList[i].discount_fee;
                }//if

                childTradeOrderList.Remove(order);
            }//fro

            /*子订单列表中剩余的订单 全部加到父订单列表中*/
            if (childTradeOrderList.Count > 0)
            {
                parentTradeOrderList.AddRange(childTradeOrderList);
            }

            /*修改customtid*/
            for (int i = 0; i < parentTradeOrderList.Count; i++)
            {
                parentTradeOrderList[i].CustomTid = customParent;
            }

            trade = TradeService.GetTrade(q => q.CustomTid == customParent).FirstOrDefault();
            if (trade == null)
            {
                return;
            }

            /*trade的总费*/
            trade.total_fee = parentTradeOrderList.Sum(q => q.total_fee);
            /*得到trade总的应付金额*/
            trade.payment = parentTradeOrderList.Sum(q => q.payment) + trade.post_fee;
            /*修改状态*/
            trade.IsSplited = false;

            /*显示数据*/
            LoadTradeOrder(gcCombineTrade, gvCombineOrder, null,parentTradeOrderList);
            LoadTextEdit(trade);
            XtraMessageBox.Show(Alading.Properties.Resources.ConbineSuccess, Alading.Properties.Resources.Imformation);
            #endregion
        }

        /// <summary>
        /// 交易合并成一个交易
        /// </summary>
        private void ConbinePrintFlg()
        {
            /*再次确认收货人姓名、地址，省市区是否一直等信息*/
            if (DialogResult.Cancel == XtraMessageBox.Show(Alading.Properties.Resources.CheckTidData,
                                                                                                                 Alading.Properties.Resources.Imformation, MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
            {
                combine.Enabled = true;
                return;
            }
            
            foreach (string custom in customTidList)
            {
                tradeList.AddRange(TradeService.GetTrade(q => q.CustomTid == custom));
                parentTradeOrderList.AddRange(TradeOrderService.GetTradeOrder(q => q.CustomTid == custom));
            }/*foreach*/

            //通过outer_id  GroupBy
            List<IGrouping<string, Alading.Entity.TradeOrder>> groupOrderList = new List<IGrouping<string, Alading.Entity.TradeOrder>>();
            groupOrderList = parentTradeOrderList.GroupBy(q => q.iid).ToList();

            //生成combine_code
            string combine_code = Guid.NewGuid().ToString();

            #region /*查找相同的订单 并把相同的订单合单*/
            foreach (IGrouping<string, Alading.Entity.TradeOrder> orderList in groupOrderList)
            {
                //在对sku_properties_name分类
                List<IGrouping<string, Alading.Entity.TradeOrder>> groupList = orderList.GroupBy(q => q.sku_properties_name).ToList();

                /*存在至少有两个相同的订单*/
                if (groupList.Count() < orderList.Count())
                {
                    foreach (IGrouping<string, Alading.Entity.TradeOrder> listOrder in groupList)
                    {
                        /*找到了相同的订单*/
                        if (listOrder.Count() > 1)
                        {
                            /*订单合并处理*/
                            TradeOrderStock tradeOrderStock = new TradeOrderStock();
                            int num = 0;

                            foreach (Alading.Entity.TradeOrder order in listOrder)
                            {
                                tradeOrderStock.order = order;

                                //购买量汇总
                                num += order.num;

                                //adjust_fee汇总
                                tradeOrderStock.order.adjust_fee += order.adjust_fee;

                                //discount_fee汇总
                                tradeOrderStock.order.discount_fee += order.discount_fee;
                            }/*if*/

                            AddDetail(listOrder.First().TradeOrderCode, tradeOrderStock);
                            //num 汇总
                            tradeOrderStock.order.num = num;

                            //total_fee汇总
                            tradeOrderStock.order.total_fee = tradeOrderStock.order.num * tradeOrderStock.order.price + tradeOrderStock.order.adjust_fee;
                           
                            //payment汇总
                            tradeOrderStock.order.payment = tradeOrderStock.order.discount_fee + tradeOrderStock.order.total_fee;

                            //customtid赋值
                            tradeOrderStock.order.CustomTid = combine_code;

                            //添加订单
                            tradeOrderList.Add(tradeOrderStock);

                        }/*if*/
                        else/*没有和其他订单相同属性的单个订单*/
                        {
                            TradeOrderStock tradeOrderStock = new TradeOrderStock();
                            tradeOrderStock.order = listOrder.First();
                            //添加宝贝名等信息
                            AddDetail(listOrder.First().TradeOrderCode, tradeOrderStock);
                            //CustomTid
                            tradeOrderStock.order.CustomTid = combine_code;
                            tradeOrderList.Add(tradeOrderStock);
                        }
                    }/*foreach*/
                }/*if*/
                else
                {
                    //单个订单
                    foreach (Alading.Entity.TradeOrder order in orderList)
                    {
                        TradeOrderStock tradeOrderStock = new TradeOrderStock();
                        tradeOrderStock.order = order;

                        //添加宝贝名等信息
                        AddDetail(order.TradeOrderCode, tradeOrderStock);
                        //CustomTid
                        tradeOrderStock.order.CustomTid = combine_code;

                        //total_fee汇总
                        tradeOrderStock.order.total_fee = tradeOrderStock.order.num * tradeOrderStock.order.price + tradeOrderStock.order.adjust_fee;

                        //payment汇总
                        tradeOrderStock.order.payment = tradeOrderStock.order.discount_fee + tradeOrderStock.order.total_fee;
                        tradeOrderList.Add(tradeOrderStock);
                    }
                }
            }/*foreach*/
            #endregion

            //邮费
            double realPostFee = 0.0;
            double payment = 0.0;
            double total_fee = 0.0;
            /*修改子交易信息*/
            for (int i = 0; i < tradeList.Count; i++)
            {
                if (realPostFee < tradeList[i].post_fee)
                {
                    realPostFee = tradeList[i].post_fee;
                }   

                //修改本地状态
                tradeList[i].LocalStatus = LocalTradeStatus.CombineTrade;
                //添加combinecode
                tradeList[i].CombineCode = combine_code;
            }/*foreach*/

            /*父交易*/
            FatherTrade = TradeService.GetTrade(q => q.CustomTid == customTidList[0]).FirstOrDefault();
            /*父订单（虚拟订单）*/
            /*修改父交易相关信息*/

            foreach (TradeOrderStock tradeOrderStock in tradeOrderList)
            {
                payment += tradeOrderStock.order.payment;
                total_fee += tradeOrderStock.order.total_fee;
            }
            FatherTrade.payment = payment  + realPostFee;
            FatherTrade.total_fee = total_fee;
            FatherTrade.post_fee = realPostFee;
            FatherTrade.CombineCode = combine_code;
            FatherTrade.tid = Guid.NewGuid().ToString();
            FatherTrade.IsCombined = true;
            FatherTrade.IsSelected = false;
            /*将combine_code赋给父交易的 CustomTid*/
            FatherTrade.CustomTid = combine_code;
            FatherTrade.LocalStatus = LocalTradeStatus.HasNotSummit;
            /*显示数据*/
            LoadTradeOrder(gcCombineTrade, gvCombineOrder, tradeOrderList,null);
            LoadTextEdit(FatherTrade);
            XtraMessageBox.Show(Alading.Properties.Resources.ConbineSuccess, Alading.Properties.Resources.Imformation);
            //合并不能用
            combine.Enabled = false;
        }

        /// <summary>
        /// 获取宝贝属性，商品名等
        /// </summary>
        /// <param name="TradeOrderCode"></param>
        /// <param name="tradeOrderStock"></param>
        private void AddDetail(string TradeOrderCode, TradeOrderStock tradeOrderStock)
        {
            View_TradeStock tradeStock = View_TradeStockService.GetView_TradeStock(q => q.TradeOrderCode == TradeOrderCode).FirstOrDefault();
            if (tradeStock != null)
            {
                //宝贝性质
                tradeOrderStock.itemType = tradeStock.ItemType;
                //商品类别
                tradeOrderStock.orderType = tradeStock.OrderType;
                //商品名
                tradeOrderStock.itemName = tradeStock.ItemName;
            }               
        }

        #region  数据加载
        /// <summary>
        /// 将订单信息展示到GridControl界面
        /// </summary>
        /// <param name="gc">展示界面</param>
        /// <param name="orderList">展示订单列表</param>
        private void LoadTradeOrder(GridControl gc, GridView view, List<TradeOrderStock>tradeOrderList,List<TradeOrder> orderList)
        {
            DataSet dataSet = new DataSet();
            MemoryStream stream = new MemoryStream(Resources.OrderSpiltSchema);
            try
            {
                dataSet.ReadXmlSchema(stream);
            }
            finally
            {
                stream.Close();
            }

            #region dataSet赋值
            if (orderList != null)
            {
                foreach (TradeOrder tradeOrder in orderList)
                {
                    TradeOrderStock tradeOrderStock = new TradeOrderStock();
                    tradeOrderStock.order = tradeOrder;
                    DataRow orderRow = dataSet.Tables["OrderList"].NewRow();

                    InitOrderRow(orderRow, tradeOrderStock);//将一条订单信息赋值到一行上面去
                    dataSet.Tables["OrderList"].Rows.Add(orderRow);
                }
            }
            if (tradeOrderList != null)
            {
                foreach (TradeOrderStock order in tradeOrderList)
                {
                    DataRow orderRow = dataSet.Tables["OrderList"].NewRow();
                    InitOrderRow(orderRow, order);//将一条订单信息赋值到一行上面去
                    dataSet.Tables["OrderList"].Rows.Add(orderRow);
                }
            }
            #endregion
            

            gc.DataSource = dataSet.Tables["OrderList"];
            view.BestFitColumns();
            gc.ForceInitialize();
        }

        /// <summary>
        /// textEdit 赋值
        /// </summary>
        private void LoadTextEdit(Alading.Entity.Trade trade)
        {
            txtBuyerName.Text = trade.receiver_name;
            textNick.Text = trade.buyer_nick;
            textPostFee.Text = trade.post_fee.ToString();
            textTotalFee.Text = trade.total_fee.ToString();
            textPayment.Text = trade.payment.ToString();
            txtPhone.Text = trade.receiver_phone??"未填写";
            txtMobile.Text = trade.receiver_mobile ?? "未填写";
            txtZip.Text = trade.receiver_zip;
            textReceiverState.Text = trade.receiver_state;
            textReceiverCity.Text = trade.receiver_city;
            textReceiverDistrict.Text = trade.receiver_district;
            txtReceiverAddress.Text = trade.receiver_address;
        }
        #endregion

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*用于删除数据库中原来子订单*/
            String ChildCustTomTid = "child" + trade.CustomTid;
            /*流程专用*/
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < curTradeList.Count; i++)
            {
                str.Append(curTradeList[i].tid);
                str.Append(";");
            }

            if (View_TradeStockService.Update_TradeOrder(ChildCustTomTid, trade, parentTradeOrderList) == ReturnType.Success)
            {
                // 添加一条合并流程信息到交易流程
                Alading.Utils.SystemHelper.CreateFlowMessage(trade.CustomTid, "合并交易", "合并交易id:" + str.ToString(), "合并交易");
                XtraMessageBox.Show(Alading.Properties.Resources.SaveSuccess, Alading.Properties.Resources.Imformation);
                this.Close();
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCombineSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tradeOrderList.Count <= 0)
            {
                return;
            }
            //更新数据库
            List<TradeOrder> orderList = new List<TradeOrder>();
            foreach (TradeOrderStock tradeOrder in tradeOrderList)
            {
                //TradeOrderCode
                tradeOrder.order.TradeOrderCode = Guid.NewGuid().ToString();

                //CustomTid
                tradeOrder.order.CustomTid = FatherTrade.CustomTid;
                orderList.Add(tradeOrder.order);
            }

            /*流程专用*/
            StringBuilder tidStr = new StringBuilder();
            for (int i = 0; i < curTradeList.Count; i++)
            {
                tidStr.Append(curTradeList[i].tid);
                tidStr.Append(";");
            }

            /*提交成功*/
            if (ReturnType.Success == View_TradeStockService.Update_TradeOrder(FatherTrade, orderList,tradeList))
            {
                // 添加一条合并流程信息到交易流程
                Alading.Utils.SystemHelper.CreateFlowMessage(FatherTrade.CustomTid, "合并交易", "合并交易id:" + tidStr.ToString(), "合并交易");
                XtraMessageBox.Show(Alading.Properties.Resources.SummitSuccess, Alading.Properties.Resources.Imformation);
                this.Close();
            }
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvTradePst.RowCount; i++)
            {
                DataRow row = gvTradePst.GetDataRow(i);
                if (row != null)
                {
                    row["IsSelected"] = ((DevExpress.XtraEditors.CheckEdit)(sender)).Checked;
                }
            }
        }

        /// <summary>
        /// 撤消合并
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rollback_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*合并还原成一个交易*/
            if (CombineFlag == combineStyle.ConbineTradeFlg)
            {
                parentTradeOrderList.Clear();

                Alading.Entity.Trade trade = new Alading.Entity.Trade();
                LoadTextEdit(trade);

                // 取消合并不可用
                Rollback.Enabled = false;

                //下载按钮可用
                combine.Enabled = true;
            }

            /*合并成一个交易*/
            if (CombineFlag == combineStyle.ConbinePrintFlg)
            {
                //父交易清空
                FatherTrade = null;

                //副本订单清空
                tradeOrderList.Clear();

                tradeList.Clear();

                Alading.Entity.Trade trade = new Alading.Entity.Trade();
                LoadTextEdit(trade);

                parentTradeOrderList.Clear();
                /*显示数据*/
                LoadTradeOrder(gcCombineTrade, gvCombineOrder, tradeOrderList, null);

                //下载按钮可用
                combine.Enabled = true;

                // 取消合并不可用
                Rollback.Enabled = false;
            }
        }

        /// <summary>
        /// 展示商品属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvChildGV_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            GridView view = (sender as GridView).GridControl.GetViewAt(e.Location) as GridView;
            if (view != null)
            {
                GridHitInfo hi = view.CalcHitInfo(e.Location);
                if (hi.Column != null && hi.InRowCell)
                {
                    //记录当前焦点所在的Row
                    DataRow currentMatch = view.GetDataRow(hi.RowHandle);
                    if (currentMatch != null)
                    {
                        //展示商品属性
                        ShowItemPropValue(currentMatch["iid"].ToString(), currentMatch["sku_properties_name"].ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 商品属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcWaitPst_MouseDown(object sender, MouseEventArgs e)
        {
            //隐藏组合商品选项卡
            combineProduct.PageVisible = false;

            GridView view = gcWaitPst.GetViewAt(e.Location) as GridView;
            if (view != null)
            {
                GridHitInfo hi = view.CalcHitInfo(e.Location);
                if (hi.Column != null && hi.InRowCell)
                {
                    //记录当前焦点所在的Row
                    DataRow currentMatch = view.GetDataRow(hi.RowHandle);
                    if (currentMatch.ItemArray.Count() != 24)
                    {
                        //展示商品属性
                        ShowItemPropValue(currentMatch["iid"].ToString(), currentMatch["sku_properties_name"].ToString());

                        if (currentMatch["ItemType"].ToString() == ItemType.ConbineProduct.ToString())
                        {
                            //显示组合商品选项卡
                            combineProduct.PageVisible = true;
                            xtraTabControl.SelectedTabPage = combineProduct;

                            //展示组合商品
                            GCAssembleDetail.DataSource = View_TradeAssembleStockService.GetViewTradeAssembleDataSet(currentMatch["TradeOrderCode"].ToString()).Tables[0];
                        }
                        else
                        {
                            combineProduct.PageVisible = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        ///  通过传iid来绑定商品属性到面板
        /// </summary>
        /// <param name="iid"></param>
        /// <param name="sku_props_name"></param>
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

        public class TradeOrderStock
        {
            //宝贝属性
            public string orderType { get; set; }
            //商品名
            public string itemType { get; set; }
            //商品名
            public string itemName { get; set; }
            //订单
            public TradeOrder order { get; set; }
        }

        private void gvCombineOrder_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            //隐藏组合商品选项卡
            combineProduct.PageVisible = false;

            GridView view = (sender as GridView);
            DataRow row = view.GetDataRow(e.RowHandle);

            if (row != null)
            {
                //订单属性显示在面板上
                ShowItemPropValue(row["iid"].ToString(), row["sku_properties_name"].ToString());
            }

            if (row["ItemType"].ToString() == ItemType.ConbineProduct.ToString())
            {
                //显示组合商品选项卡
                combineProduct.PageVisible = true;
                xtraTabControl.SelectedTabPage = combineProduct;

                //展示组合商品
                GCAssembleDetail.DataSource = View_TradeAssembleStockService.GetViewTradeAssembleDataSet(row["TradeOrderCode"].ToString()).Tables[0];
            }
        }

        private void gvTradePst_RowCellClick(object sender, RowCellClickEventArgs e)
        {

        }

        private void gcWaitPst_Click(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi =
                gvTradePst.CalcHitInfo((sender as Control).PointToClient(Control.MousePosition));

            GridView view = gcWaitPst.GetViewAt(Control.MousePosition) as GridView;
            if (hi.RowHandle >= 0)
            {
                DataRow row = gvOrderPst.GetDataRow(hi.RowHandle);
            }
        }
    }
}
