using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Alading.Core.Enum;
using Alading.Entity;
using Alading.Business;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using Alading.Utils;
using Alading.Properties;
using System.IO;

namespace Alading.Forms.Trade.Forms
{
    public partial class TradeSplit : DevExpress.XtraEditors.XtraForm
    {
        #region 全局 数据
        private string _customTid = string.Empty;//当前界面所展示的交易号
        /*分后的父交易*/
        private Alading.Entity.Trade ParentTrade = new Alading.Entity.Trade();
        /*分后的子交易*/
        private Alading.Entity.Trade ChildTrade = new Alading.Entity.Trade();
        /*父交易订单列表，用于展示*/
        private List<ViewTradeStock> _parentOrderList = new List<ViewTradeStock>();
        private List<ViewTradeStock> _childOrderList = new List<ViewTradeStock>();//缺货子交易列表

        /*用于更新数据库*/
        List<View_TradeStock> ParentTradeList = new List<View_TradeStock>();
        List<View_TradeStock> ChildTradeList = new List<View_TradeStock>();
        
        #endregion

        #region  构造函数
        /// <summary>
        /// 自定义构造函数
        /// </summary>
        /// <param name="customTid"></param>
        public TradeSplit(string customTid)
        {
            InitializeComponent();
            _customTid = customTid;
            /*从数据库获取数据CustomTid,非礼品并且是没有拆分过的 */
            ParentTradeList = View_TradeStockService.GetView_TradeStock(p => p.CustomTid == _customTid
                                                                                                                                && p.OrderType != emumOrderType.GiftGoods
                                                                                                                                && p.IsSplited == false);
            /*数据转换*/
            _parentOrderList = ViewTradeStockCopyData(ParentTradeList);
            LoadSpiltTrade(gcParentTrade, _parentOrderList);
        }
        #endregion

        #region  界面加载
        /// <summary>
        /// 将订单信息展示到GridControl界面
        /// </summary>
        /// <param name="gc">展示界面</param>
        /// <param name="orderList">展示订单列表</param>
        private void LoadSpiltTrade(GridControl gc, List<ViewTradeStock> orderList)
        {
            if (orderList != null)
            {
                DataSet dataSet = new DataSet();
                MemoryStream stream = new MemoryStream(Resources.OrderSpiltSchema);
                dataSet.ReadXmlSchema(stream);
                stream.Close();

                foreach (ViewTradeStock order in orderList)
                {
                    DataRow orderRow = dataSet.Tables["OrderList"].NewRow();
                    InitOrderRow(orderRow, order);//将一条订单信息赋值到一行上面去
                    dataSet.Tables["OrderList"].Rows.Add(orderRow);
                }
                gc.DataSource = dataSet.Tables["OrderList"];
                gc.ForceInitialize();
            }
        }

        /// <summary>
        /// 将一条订单相关信息展示到一行上面去
        /// </summary>
        /// <param name="orderRow">展示行</param>
        /// <param name="orderCur">展示订单</param>
        private void InitOrderRow(DataRow orderRow, ViewTradeStock orderCur)
        {
            orderRow["tid"] = orderCur.tid;
            orderRow["oid"] = orderCur.oid;
            orderRow["TradeOrderCode"] = orderCur.TradeOrderCode;
            orderRow["IsSelected"] = false;
            orderRow["CustomTid"] = orderCur.CustomTid;
            orderRow["ItemName"] = orderCur.title;
            orderRow["sku_properties_name"] = orderCur.sku_properties_name;
            orderRow["LeftQuantity"] = orderCur.SkuQuantity - orderCur.OccupiedQuantity;
            orderRow["num"] = orderCur.num;
            orderRow["price"] = orderCur.price;
            orderRow["payment"] = orderCur.payment;
            orderRow["OrderType"] = orderCur.OrderType;
            orderRow["numSplit"] = string.Empty;
            orderRow["orderTotalFee"] = orderCur.total_fee;
            orderRow["iid"] = orderCur.iid;
            //查询货物库存量
            if (orderCur.SkuQuantity == null)
            {
                orderRow["TradeIsLockProduct"] = LackProductOrNot.NotBuildStock;
            }
            if (orderCur.SkuQuantity - orderCur.OccupiedQuantity - orderCur.num >= 0)
            {
                orderRow["TradeIsLockProduct"] = LackProductOrNot.Normal;
            }
            else
            {
                orderRow["TradeIsLockProduct"] = LackProductOrNot.Lack;
            }
        }
        #endregion

        #region 按钮事件
        /// <summary>
        /// 拆分按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Split_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

                FunSplit(gcParentTrade, gvParentTrade);
            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "出错");
            }
        }

        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Combine_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            try
            {
                combine();
            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "出错");
            }
        }

        /// <summary>
        /// 自动拆分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitAuto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            try
            {
                AutoSplit();
            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "出错");
            }
        }

        /// <summary>
        /// save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            try
            {
                save();
            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "出错");
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }
        #endregion

        #region 功能函数
        /// <summary>
        /// 拆分函数
        /// </summary>
        /// <param name="gc"></param>
        /// <param name="view"></param>
        private void FunSplit(GridControl gc, GridView view)
        {
            #region 订单拆分修改
            List<int> rowList = new List<int>();

            //接收从数据库视图表中传出来的值，此值不能修改
            Alading.Entity.View_TradeStock ParentOrder = new Alading.Entity.View_TradeStock();

            Alading.Entity.View_TradeStock ChildOrder = new Alading.Entity.View_TradeStock();

            //所勾选要被拆分的交易 ，用于修改数据库
            List<ViewTradeStock> tradeParentOrderList = new List<ViewTradeStock>();

            DataRow row = null;

            int NullRecord = 0;

            for (int rowHandle = 0; rowHandle < view.RowCount; rowHandle++)
            {
                //将从数据库视图表中传出来的值转换
                ViewTradeStock ParentOrderExtent = new ViewTradeStock();
                ViewTradeStock ChildOrderExtent = new ViewTradeStock();

                row = view.GetDataRow(rowHandle);

                if (row == null)
                {
                    return;
                }
                if (Convert.ToBoolean(row["IsSelected"].ToString()))
                {
                    ParentOrder = View_TradeStockService.GetView_TradeStock(q => q.oid == row["oid"].ToString()).FirstOrDefault();
                    if (ParentOrder == null)
                    {
                        return;
                    }
                    /*数据转换*/
                    viewCopy(ParentOrderExtent, ParentOrder);
                    /*购买量的修改*/
                    if (row["numSplit"].ToString() == null || row["numSplit"].ToString() == string.Empty)
                    {
                        return;
                    }

                    ParentOrderExtent.num = int.Parse(row["num"].ToString()) - int.Parse(row["numSplit"].ToString());
                    /*total_fee的修改*/
                    ParentOrderExtent.total_fee = float.Parse(row["price"].ToString()) * ParentOrderExtent.num;
                    /*修改payment*/
                    ParentOrderExtent.payment = (ParentOrderExtent.total_fee + ParentOrderExtent.adjust_fee);
                    tradeParentOrderList.Add(ParentOrderExtent);

                    ChildOrder = ParentOrder;
                    if (ChildOrder == null)
                    {
                        return;
                    }
                    /*数据转换*/
                    viewCopy(ChildOrderExtent, ChildOrder);
                    /*购买量修改*/
                    ChildOrderExtent.num = int.Parse(row["numSplit"].ToString());
                    ChildOrderExtent.TradeOrderCode = Guid.NewGuid().ToString();
                    /*total_fee的修改*/
                    ChildOrderExtent.total_fee = float.Parse(row["price"].ToString()) * ChildOrderExtent.num;
                    /*修改payment*/
                    ChildOrderExtent.payment = (ChildOrderExtent.total_fee);
                    /*修改customTid  子交易后面加1*/
                    ChildOrderExtent.CustomTid = "child" + ChildOrderExtent.CustomTid;

                    #region 如果新拆分后订单在子订单列表中存在，则累加相应的量
                    int count = 0;
                    for (count = 0; count < _childOrderList.Count; count++)
                    {
                        if (ChildOrderExtent.oid == _childOrderList[count].oid)
                        {
                            ChildOrderExtent.num += _childOrderList[count].num;

                            ChildOrderExtent.total_fee = (ChildOrderExtent.price) * ChildOrderExtent.num;
                            ChildOrderExtent.payment = (ChildOrderExtent.total_fee);

                            /*修改相关量*/
                            _childOrderList[count].num = ChildOrderExtent.num;
                            _childOrderList[count].total_fee = ChildOrderExtent.total_fee;
                            _childOrderList[count].payment = ChildOrderExtent.payment;

                            break;
                        }//if
                    }//for
                    /*子订单列表中没有新添加的订单*/
                    if (count >= _childOrderList.Count)
                    {
                        _childOrderList.Add(ChildOrderExtent);
                    }
                }/*if (Convert.ToBoolean(row["IsSelected"].ToString()))*/
                else
                    /*记载没被勾选的行数*/
                    NullRecord++;
            }/*for (int rowHandle = 0; rowHandle < view.RowCount; rowHandle++)*/
            if (NullRecord >= view.RowCount)
            {
                XtraMessageBox.Show(Alading.Properties.Resources.NotSelctCanNOtSplit, Alading.Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                    #endregion

            #endregion

            #region 修改显示的订单列表
            List<ViewTradeStock> removeTradeList = new List<ViewTradeStock>();
            for (int i = 0; i < tradeParentOrderList.Count; i++)
            {
                for (int j = 0; j < _parentOrderList.Count; j++)
                {
                    if (tradeParentOrderList[i].oid == _parentOrderList[j].oid)
                    {
                        if (tradeParentOrderList[i].num == 0)
                        {
                            removeTradeList.Add(_parentOrderList[j]);
                        }
                        _parentOrderList[j].num = tradeParentOrderList[i].num;
                        _parentOrderList[j].total_fee = tradeParentOrderList[i].total_fee;
                        _parentOrderList[j].payment = tradeParentOrderList[i].payment;
                    }
                }
            }
            /*删除_parentOrderList.num 为零的交易*/
            foreach (ViewTradeStock tradeStock in removeTradeList)
            {
                _parentOrderList.Remove(tradeStock);
            }
            #endregion

            #region 所对应交易的修改
            /*获取父交易*/
            ParentTrade = TradeService.GetTrade(_customTid);
            if (ParentTrade == null)
            {
                return;
            }
            /*修改父交易*/
            ParentTrade.IsSplited = true;
            ParentTrade.ParentCustomTid = "0";
            /*修改应付金额，总费*/
            ParentTrade.payment = 0;
            float total_fee = 0;
            foreach (ViewTradeStock order in _parentOrderList)
            {
                ParentTrade.payment +=(double) order.payment;
                total_fee += (float)order.total_fee;
            }
            ParentTrade.total_fee = total_fee;
            /*修改子交易*/
            ChildTrade = TradeService.GetTrade(_customTid);
            if (ChildTrade == null)
            {
                return;
            }
            /*CustomTid 随机产生一个*/
            ChildTrade.CustomTid = "child" + _customTid;
            ChildTrade.ParentCustomTid = _customTid;
            ChildTrade.IsSplited = true;
            /*邮费*/
            ChildTrade.post_fee = ChildTrade.post_fee;
            /*修改应付金额，总费*/
            ChildTrade.payment = 0;
            ChildTrade.total_fee = 0.0;
            float ChildTotalFee = 0;
            foreach (ViewTradeStock order in _childOrderList)
            {
                ChildTrade.payment += order.payment;
                ChildTotalFee += (float)(order.total_fee);
            }
            ChildTrade.total_fee = ChildTotalFee;
            /*子交易加上邮费*/
            ChildTrade.post_fee = ParentTradeList[0].post_fee;
            #endregion

            #region 数据展示
            //拆分不合理
            if (_parentOrderList.Count == 0)
            {
                XtraMessageBox.Show("拆分不合理!","出错", MessageBoxButtons.OK,MessageBoxIcon.Error);

                _parentOrderList.Clear();

                //数据转换
                _parentOrderList = ViewTradeStockCopyData(ParentTradeList);

                LoadSpiltTrade(gcParentTrade, _parentOrderList);

                _childOrderList.Clear();
            }
            LoadSpiltTrade(gcParentTrade, _parentOrderList);
            gcParentTrade.RefreshDataSource();

            LoadSpiltTrade(gcChildTrade, _childOrderList);
            gcChildTrade.RefreshDataSource();
            #endregion
        }

        /// <summary>
        /// 自动拆分
        /// </summary>
        private void AutoSplit()
        {
            #region 自动拆分
            /*子列表清空*/
            _childOrderList.Clear();
            /*父列表清空*/
            _parentOrderList.Clear();
            /*获取值*/
            List<View_TradeStock> ParentTradeList = View_TradeStockService.GetView_TradeStock(p => p.CustomTid == _customTid);
            /*数据转换*/
            _parentOrderList = ViewTradeStockCopyData(ParentTradeList);

            /*删除列表*/
            List<ViewTradeStock> removeTradeList = new List<ViewTradeStock>();

            for (int i = 0; i < _parentOrderList.Count; i++)
            {
                int LeftQuantity = 0;

                /*剩余量*/
                LeftQuantity = _parentOrderList[i].SkuQuantity.Value - _parentOrderList[i].OccupiedQuantity.Value;
                /*自动拆分缺货的订单*/
                if (LeftQuantity < _parentOrderList[i].num)
                {
                    View_TradeStock viewTrade = View_TradeStockService.GetView_TradeStock(q => q.TradeOrderCode == _parentOrderList[i].TradeOrderCode).FirstOrDefault();
                    ViewTradeStock viewChildTrade = new ViewTradeStock();
                    if (viewTrade == null)
                    {
                        return;
                    }
                    /*数据转换*/
                    viewCopy(viewChildTrade, viewTrade);
                    /*订单量改变*/
                    viewChildTrade.num = _parentOrderList[i].num - LeftQuantity;
                    /*订单量改变*/
                    viewChildTrade.total_fee = (viewChildTrade.price) * viewChildTrade.num;
                    /*修改customtid*/
                    viewChildTrade.CustomTid = "child" + viewChildTrade.CustomTid;
                    /*tradeordercode*/
                    viewChildTrade.TradeOrderCode = Guid.NewGuid().ToString(); 
                    /*向子列表中填值*/
                    _childOrderList.Add(viewChildTrade);

                    /*订单量改变*/
                    _parentOrderList[i].num = LeftQuantity;

                    if (_parentOrderList[i].num == 0)
                    {
                        removeTradeList.Add(_parentOrderList[i]);
                    }
                    /*订单量改变*/
                    _parentOrderList[i].total_fee = (_parentOrderList[i].price) * _parentOrderList[i].num;
                }//if
            }//for
            /*将全部缺货的订单分离出去*/
            foreach (ViewTradeStock tradeStock in removeTradeList)
            {
                _parentOrderList.Remove(tradeStock);
            }
            #endregion

            #region  子交易修改
            ChildTrade = TradeService.GetTrade(_customTid);
            if (ChildTrade == null)
            {
                return;
            }
            /*CustomTid 随机产生一个*/
            ChildTrade.CustomTid = "child" + _customTid;  
            ChildTrade.ParentCustomTid = _customTid;
            ChildTrade.IsSplited = true;
            /*邮费*/
            ChildTrade.post_fee = ChildTrade.post_fee;
            /*修改应付金额，总费*/
            ChildTrade.payment = 0;
            ChildTrade.total_fee = 0.0;
            float ChildTotalFee = 0;
            foreach (ViewTradeStock order in _childOrderList)
            {
                ChildTrade.payment += order.payment;
                ChildTotalFee += (float)(order.total_fee);
            }
            ChildTrade.total_fee = ChildTotalFee;
            ChildTrade.post_fee = ParentTradeList[0].post_fee;
            #endregion

            #region 父交易修改
            /*获取父交易*/
            ParentTrade = TradeService.GetTrade(_customTid);
            /*修改父交易*/
            ParentTrade.IsSplited = true;
            ParentTrade.ParentCustomTid = "0";
            /*修改应付金额，总费*/
            ParentTrade.payment = 0;
            float total_fee = 0;
            foreach (ViewTradeStock order in _parentOrderList)
            {
                ParentTrade.payment += order.payment;
                total_fee += (float)(order.total_fee);
            }
            ParentTrade.total_fee = total_fee;
            #endregion

            #region 数据展示
            LoadSpiltTrade(gcParentTrade, _parentOrderList);
            gcParentTrade.RefreshDataSource();

            LoadSpiltTrade(gcChildTrade, _childOrderList);
            gcChildTrade.RefreshDataSource();
            #endregion

        }

        /// <summary>
        /// 合并方法
        /// </summary>
        private void combine()
        {
            int count = 0;
            List<int> recordList = new List<int>();
            List<ViewTradeStock> viewList = new List<ViewTradeStock>();
            #region 记录被勾选的行
            for (; count < gvChildOrder.RowCount; count++)
            {
                DataRow row = gvChildOrder.GetDataRow(count);
                if (row == null)
                {
                    return;
                }
                if (Convert.ToBoolean(row["IsSelected"].ToString()))
                {
                    recordList.Add(count);
                }
            }
            #endregion
            for (int record = 0; record < recordList.Count; record++)
            {
                /*取出勾选行的数据*/
                DataRow dataRow = gvChildOrder.GetDataRow(record);
                if (dataRow == null)
                {
                    return;
                }
                int view = 0;
                for (view = 0; view < _parentOrderList.Count; view++)
                {
                    ViewTradeStock viewObj = new ViewTradeStock();
                    /*找到在_parentOrderList对应的行*/
                    if (_parentOrderList[view].oid == dataRow["oid"].ToString())
                    {
                        /*取出_childOrderList中的对应的数据*/
                        viewObj = _childOrderList.Where(q => q.oid == dataRow["oid"].ToString()).FirstOrDefault();
                        int flag = 0;
                        if (viewObj == null)
                        {
                            return;
                        }
                        /*取出数据在  _childOrderList下标*/
                        flag = _childOrderList.IndexOf(viewObj);
                        /*订单量更新*/
                        viewObj.num += _parentOrderList[view].num;
                        /*总消费更新*/
                        viewObj.total_fee = (viewObj.price) * viewObj.num;
                        _parentOrderList.Remove(_parentOrderList[view]);
                        viewList.Add(viewObj);
                        _childOrderList.Remove(_childOrderList[flag]);
                        break;
                    }
                }
                /*子列表中的订单在父列表中没有*/
                if (view >= _parentOrderList.Count)
                {
                    ViewTradeStock viewObj = new ViewTradeStock();
                    viewObj = _childOrderList.Where(q => q.oid == dataRow["oid"].ToString()).FirstOrDefault();
                    if (viewObj == null)
                    {
                        return;
                    }
                    viewList.Add(viewObj);
                    _childOrderList.Remove(viewObj);
                }
            }
            _parentOrderList.AddRange(viewList);
            LoadSpiltTrade(gcParentTrade, _parentOrderList);
            gcParentTrade.RefreshDataSource();
            LoadSpiltTrade(gcChildTrade, _childOrderList);
            gcChildTrade.RefreshDataSource();
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void save()
        {
            
            /*数据转换*/
            if (_childOrderList.Count == 0)
            {
                return;
            }

            if (XtraMessageBox.Show("确定要保存?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
            {
                return;
            }

            ChildTradeList = viewTradeStockConvert(_childOrderList);
            ParentTradeList = viewTradeStockConvert(_parentOrderList);
            if (ReturnType.Success == View_TradeStockService.Update_TradeOrder(ParentTradeList, ChildTradeList, ParentTrade, ChildTrade))
            {
                // 添加一条添加拆分流程信息到交易流程
                Alading.Utils.SystemHelper.CreateFlowMessage(_customTid, "拆分交易", "拆分交易id:" + ParentTradeList.FirstOrDefault().tid, "拆分交易");
                XtraMessageBox.Show(Alading.Properties.Resources.SaveSuccess, Alading.Properties.Resources.Imformation)  ;
            }
            this.Dispose();
        }

        /// <summary>
        /// 商品属性展示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvParentTrade_RowClick(object sender, RowClickEventArgs e)
        {
            GridView view = sender as GridView;
            DataRow row = view.GetDataRow(e.RowHandle);
            if (row != null)
            {
                ShowItemPropValue(row["iid"].ToString(), row["sku_properties_name"].ToString());
            }
        }

        /// <summary>
        /// 商品属性展示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvChildOrder_RowClick(object sender, RowClickEventArgs e)
        {
            GridView view = sender as GridView;
            DataRow row = view.GetDataRow(e.RowHandle);
            if (row != null)
            {
                ShowItemPropValue(row["iid"].ToString(), row["sku_properties_name"].ToString());
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

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvChildOrder.RowCount; i++)
            {
                DataRow row = gvChildOrder.GetDataRow(i);
                if (row != null)
                {
                    row["IsSelected"] = ((DevExpress.XtraEditors.CheckEdit)(sender)).Checked;
                }
            }
        }
        #endregion

        #region  继承view返回的list  数据处理
        public class ViewTradeStock : View_TradeStock
        {

        }
        private List<ViewTradeStock> ViewTradeStockCopyData(List<View_TradeStock> viewSrc)
        {
            if (viewSrc == null || viewSrc.Count == 0)
            {
                return null;
            }
            List<ViewTradeStock> viewDes = new List<ViewTradeStock>();
            foreach (View_TradeStock Tradestock in viewSrc)
            {
                ViewTradeStock TradestockObj = new ViewTradeStock();
                viewCopy(TradestockObj, Tradestock);
                viewDes.Add(TradestockObj);
            }
            return viewDes;
        }
        private List<View_TradeStock> viewTradeStockConvert(List<ViewTradeStock> viewSrc)
        {
            if (viewSrc == null || viewSrc.Count == 0)
            {
                return null;
            }
            List<View_TradeStock> viewDes = new List<View_TradeStock>();
            foreach (ViewTradeStock Tradestock in viewSrc)
            {
                View_TradeStock TradestockObj = new View_TradeStock();
                viewCopyData(TradestockObj, Tradestock);
                viewDes.Add(TradestockObj);
            }
            return viewDes;
        }

        private void viewCopy(ViewTradeStock viewDes, View_TradeStock viewSrc)
        {
            viewDes.TradeOrderID = viewSrc.TradeOrderID;
            viewDes.TradeOrderCode = viewSrc.TradeOrderCode;
            viewDes.iid = viewSrc.iid;
            viewDes.sku_id = viewSrc.sku_id;
            viewDes.oid = viewSrc.oid;
            viewDes.outer_sku_id = viewSrc.outer_sku_id;
            viewDes.outer_iid = viewSrc.outer_iid;
            viewDes.sku_properties_name = viewSrc.sku_properties_name;
            viewDes.price = viewSrc.price;
            viewDes.total_fee = viewSrc.total_fee;
            viewDes.discount_fee = viewSrc.discount_fee;
            viewDes.adjust_fee = viewSrc.adjust_fee;
            viewDes.payment = viewSrc.payment;
            viewDes.item_meal_name = viewSrc.item_meal_name;
            viewDes.num = viewSrc.num;
            viewDes.title = viewSrc.title;
            viewDes.pic_path = viewSrc.pic_path;
            viewDes.seller_nick = viewSrc.seller_nick;
            viewDes.buyer_nick = viewSrc.buyer_nick;
            viewDes.type = viewSrc.type;
            viewDes.created = viewSrc.created;
            viewDes.refund_status = viewSrc.refund_status;
            viewDes.seller_type = viewSrc.seller_type;
            viewDes.snapshot_url = viewSrc.snapshot_url;
            viewDes.snapshot = viewSrc.snapshot;
            viewDes.timeout_action_time = viewSrc.timeout_action_time;
            viewDes.OrderType = viewSrc.OrderType;
            viewDes.OrderTimeStamp = viewSrc.OrderTimeStamp;
            viewDes.HouseCode = viewSrc.HouseCode;
            viewDes.LayoutCode = viewSrc.LayoutCode;
            viewDes.ShopTypeName = viewSrc.ShopTypeName;
            viewDes.LogisticCompanyName = viewSrc.LogisticCompanyName;
            viewDes.TemplateName = viewSrc.TemplateName;
            viewDes.CustomTid = viewSrc.CustomTid;
            viewDes.LogisticCompanyCode = viewSrc.LogisticCompanyCode;
            viewDes.TemplateCode = viewSrc.TemplateCode;
            viewDes.tid = viewSrc.tid;
            viewDes.TradeCreated = viewSrc.TradeCreated;
            viewDes.buyer_message = viewSrc.buyer_message;
            viewDes.buyer_memo = viewSrc.buyer_memo;
            viewDes.alipay_no = viewSrc.alipay_no;
            viewDes.TradePayment = viewSrc.TradePayment;
            viewDes.status = viewSrc.status;
            viewDes.TradeAdjustFee = viewSrc.TradeAdjustFee;
            viewDes.TradeDiscountFee = viewSrc.TradeDiscountFee;
            viewDes.pay_time = viewSrc.pay_time;
            viewDes.end_time = viewSrc.end_time;
            viewDes.post_fee = viewSrc.post_fee;
            viewDes.receiver_name = viewSrc.receiver_name;
            viewDes.receiver_city = viewSrc.receiver_city;
            viewDes.receiver_district = viewSrc.receiver_district;
            viewDes.receiver_address = viewSrc.receiver_address;
            viewDes.receiver_zip = viewSrc.receiver_zip;
            viewDes.receiver_mobile = viewSrc.receiver_mobile;
            viewDes.receiver_phone = viewSrc.receiver_phone;
            viewDes.receiver_state = viewSrc.receiver_state;
            viewDes.TradeTimeStamp = viewSrc.TradeTimeStamp;
            viewDes.HasInvoice = viewSrc.HasInvoice;
            viewDes.TradeSourceType = viewSrc.TradeSourceType;
            viewDes.IsSplited = viewSrc.IsSplited;
            viewDes.IsCombined = viewSrc.IsCombined;
            viewDes.LockedUserName = viewSrc.LockedUserName;
            viewDes.LockedUserCode = viewSrc.LockedUserCode;
            viewDes.LockedTime = viewSrc.LockedTime;
            viewDes.ShippingCode = viewSrc.ShippingCode;
            viewDes.LastShippingType = viewSrc.LastShippingType;
            viewDes.LocalStatus = viewSrc.LocalStatus;
            viewDes.CombineCode = viewSrc.CombineCode;
            viewDes.ParentCustomTid = viewSrc.ParentCustomTid;
            viewDes.SkuProps_Str = viewSrc.SkuProps_Str;
            viewDes.SkuQuantity = viewSrc.SkuQuantity;
            viewDes.SkuPrice = viewSrc.SkuPrice;
            viewDes.OccupiedQuantity = viewSrc.OccupiedQuantity;
            viewDes.OuterID = viewSrc.OuterID;
            viewDes.ItemName = viewSrc.ItemName;
            viewDes.CatName = viewSrc.CatName;
            viewDes.Cid = viewSrc.Cid;
            viewDes.seller_name = viewSrc.seller_name;
            viewDes.seller_phone = viewSrc.seller_phone;
            viewDes.seller_mobile = viewSrc.seller_mobile;
            viewDes.seller_alipay_no = viewSrc.seller_alipay_no;
            viewDes.commission_fee = viewSrc.commission_fee;
            viewDes.buyer_email = viewSrc.buyer_email;
            viewDes.consign_time = viewSrc.consign_time;
            viewDes.buyer_alipay_no = viewSrc.buyer_alipay_no;
            viewDes.TradeTotalFee = viewSrc.TradeTotalFee;
            viewDes.buyer_obtain_point_fee = viewSrc.buyer_obtain_point_fee;
            viewDes.modified = viewSrc.modified;
            viewDes.point_fee = viewSrc.point_fee;
            viewDes.real_point_fee = viewSrc.real_point_fee;
            viewDes.seller_memo = viewSrc.seller_memo;
            viewDes.seller_rate = viewSrc.seller_rate;
            viewDes.buyer_rate = viewSrc.buyer_rate;
            viewDes.TradeType = viewSrc.TradeType;
            viewDes.shipping_type = viewSrc.shipping_type;
            viewDes.has_post_fee = viewSrc.has_post_fee;
            viewDes.seller_email = viewSrc.seller_email;
            viewDes.available_confirm_fee = viewSrc.available_confirm_fee;
            viewDes.received_payment = viewSrc.received_payment;
            viewDes.cod_fee = viewSrc.cod_fee;
            viewDes.cod_status = viewSrc.cod_status;
            viewDes.TradeTimeOutActionTime = viewSrc.TradeTimeOutActionTime;
            viewDes.is_3D = viewSrc.is_3D;
            viewDes.BuyerType = viewSrc.BuyerType;
            viewDes.SellerType = viewSrc.SellerType;
        }
        private void viewCopyData(View_TradeStock viewDes, ViewTradeStock viewSrc)
        {
            viewDes.TradeOrderID = viewSrc.TradeOrderID;
            viewDes.TradeOrderCode = viewSrc.TradeOrderCode;
            viewDes.iid = viewSrc.iid;
            viewDes.sku_id = viewSrc.sku_id;
            viewDes.oid = viewSrc.oid;
            viewDes.outer_sku_id = viewSrc.outer_sku_id;
            viewDes.outer_iid = viewSrc.outer_iid;
            viewDes.sku_properties_name = viewSrc.sku_properties_name;
            viewDes.price = viewSrc.price;
            viewDes.total_fee = viewSrc.total_fee;
            viewDes.discount_fee = viewSrc.discount_fee;
            viewDes.adjust_fee = viewSrc.adjust_fee;
            viewDes.payment = viewSrc.payment;
            viewDes.item_meal_name = viewSrc.item_meal_name;
            viewDes.num = viewSrc.num;
            viewDes.title = viewSrc.title;
            viewDes.pic_path = viewSrc.pic_path;
            viewDes.seller_nick = viewSrc.seller_nick;
            viewDes.buyer_nick = viewSrc.buyer_nick;
            viewDes.type = viewSrc.type;
            viewDes.created = viewSrc.created;
            viewDes.refund_status = viewSrc.refund_status;
            viewDes.seller_type = viewSrc.seller_type;
            viewDes.snapshot_url = viewSrc.snapshot_url;
            viewDes.snapshot = viewSrc.snapshot;
            viewDes.timeout_action_time = viewSrc.timeout_action_time;
            viewDes.OrderType = viewSrc.OrderType;
            viewDes.OrderTimeStamp = viewSrc.OrderTimeStamp;
            viewDes.HouseCode = viewSrc.HouseCode;
            viewDes.LayoutCode = viewSrc.LayoutCode;
            viewDes.ShopTypeName = viewSrc.ShopTypeName;
            viewDes.LogisticCompanyName = viewSrc.LogisticCompanyName;
            viewDes.TemplateName = viewSrc.TemplateName;
            viewDes.CustomTid = viewSrc.CustomTid;
            viewDes.LogisticCompanyCode = viewSrc.LogisticCompanyCode;
            viewDes.TemplateCode = viewSrc.TemplateCode;
            viewDes.tid = viewSrc.tid;
            viewDes.TradeCreated = viewSrc.TradeCreated;
            viewDes.buyer_message = viewSrc.buyer_message;
            viewDes.buyer_memo = viewSrc.buyer_memo;
            viewDes.alipay_no = viewSrc.alipay_no;
            viewDes.TradePayment = viewSrc.TradePayment;
            viewDes.status = viewSrc.status;
            viewDes.TradeAdjustFee = viewSrc.TradeAdjustFee;
            viewDes.TradeDiscountFee = viewSrc.TradeDiscountFee;
            viewDes.pay_time = viewSrc.pay_time;
            viewDes.end_time = viewSrc.end_time;
            viewDes.post_fee = viewSrc.post_fee;
            viewDes.receiver_name = viewSrc.receiver_name;
            viewDes.receiver_city = viewSrc.receiver_city;
            viewDes.receiver_district = viewSrc.receiver_district;
            viewDes.receiver_address = viewSrc.receiver_address;
            viewDes.receiver_zip = viewSrc.receiver_zip;
            viewDes.receiver_mobile = viewSrc.receiver_mobile;
            viewDes.receiver_phone = viewSrc.receiver_phone;
            viewDes.receiver_state = viewSrc.receiver_state;
            viewDes.TradeTimeStamp = viewSrc.TradeTimeStamp;
            viewDes.HasInvoice = viewSrc.HasInvoice;
            viewDes.TradeSourceType = viewSrc.TradeSourceType;
            viewDes.IsSplited = viewSrc.IsSplited;
            viewDes.IsCombined = viewSrc.IsCombined;
            viewDes.LockedUserName = viewSrc.LockedUserName;
            viewDes.LockedUserCode = viewSrc.LockedUserCode;
            viewDes.LockedTime = viewSrc.LockedTime;
            viewDes.ShippingCode = viewSrc.ShippingCode;
            viewDes.LastShippingType = viewSrc.LastShippingType;
            viewDes.LocalStatus = viewSrc.LocalStatus;
            viewDes.CombineCode = viewSrc.CombineCode;
            viewDes.ParentCustomTid = viewSrc.ParentCustomTid;
            viewDes.SkuProps_Str = viewSrc.SkuProps_Str;
            viewDes.SkuQuantity = viewSrc.SkuQuantity;
            viewDes.SkuPrice = viewSrc.SkuPrice;
            viewDes.OccupiedQuantity = viewSrc.OccupiedQuantity;
            viewDes.OuterID = viewSrc.OuterID;
            viewDes.ItemName = viewSrc.ItemName;
            viewDes.CatName = viewSrc.CatName;
            viewDes.Cid = viewSrc.Cid;
            viewDes.seller_name = viewSrc.seller_name;
            viewDes.seller_phone = viewSrc.seller_phone;
            viewDes.seller_mobile = viewSrc.seller_mobile;
            viewDes.seller_alipay_no = viewSrc.seller_alipay_no;
            viewDes.commission_fee = viewSrc.commission_fee;
            viewDes.buyer_email = viewSrc.buyer_email;
            viewDes.consign_time = viewSrc.consign_time;
            viewDes.buyer_alipay_no = viewSrc.buyer_alipay_no;
            viewDes.TradeTotalFee = viewSrc.TradeTotalFee;
            viewDes.buyer_obtain_point_fee = viewSrc.buyer_obtain_point_fee;
            viewDes.modified = viewSrc.modified;
            viewDes.point_fee = viewSrc.point_fee;
            viewDes.real_point_fee = viewSrc.real_point_fee;
            viewDes.seller_memo = viewSrc.seller_memo;
            viewDes.seller_rate = viewSrc.seller_rate;
            viewDes.buyer_rate = viewSrc.buyer_rate;
            viewDes.TradeType = viewSrc.TradeType;
            viewDes.shipping_type = viewSrc.shipping_type;
            viewDes.has_post_fee = viewSrc.has_post_fee;
            viewDes.seller_email = viewSrc.seller_email;
            viewDes.available_confirm_fee = viewSrc.available_confirm_fee;
            viewDes.received_payment = viewSrc.received_payment;
            viewDes.cod_fee = viewSrc.cod_fee;
            viewDes.cod_status = viewSrc.cod_status;
            viewDes.TradeTimeOutActionTime = viewSrc.TradeTimeOutActionTime;
            viewDes.is_3D = viewSrc.is_3D;
            viewDes.BuyerType = viewSrc.BuyerType;
            viewDes.SellerType = viewSrc.SellerType;
        }
        #endregion

        #region gridview 事件
        /// <summary>
        /// 置空处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvParentTrade_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            #region 置空处理
            for (int rowHandle = 0; rowHandle < (sender as GridView).RowCount; rowHandle++)
            {
                DataRow row = (sender as GridView).GetDataRow(rowHandle);
                if (row == null)
                {
                    return;
                }
                /*没有勾选，而拆分量空，将拆分量置空*/
                if (Convert.ToBoolean(row["IsSelected"].ToString()) == false && row["numSplit"].ToString() != string.Empty)
                {
                    row["numSplit"] = string.Empty;
                }
                /*勾选了，拆分量没有填写，勾选置空*/
                if (Convert.ToBoolean(row["IsSelected"].ToString()) && row["numSplit"].ToString() == string.Empty)
                {
                    row["IsSelected"] = false;
                }
            }
            #endregion
        }

        private void gvChildOrder_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = (sender as GridView).GetFocusedDataRow();
            if (e.Column == IsSelectedChild)
            {
                gvChildOrder.BeginDataUpdate();
                row["IsSelected"] = e.Value;
                gvChildOrder.EndDataUpdate();
            }
        }

        /// <summary>
        /// 勾选保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvParentTrade_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            #region  IsSelected 勾选保存
            GridView view = sender as GridView;
            DataRow row = view.GetDataRow(e.RowHandle);
            string numHistory = row["numSplit"].ToString();
            if (e.Column == IsSelected)
            {
                view.BeginDataUpdate();
                row["IsSelected"] = e.Value;
                view.EndDataUpdate();
            }
            #endregion

            #region
            if (e.Column == numSplit)
            {
                if (e.Value.ToString() == string.Empty)
                {
                    row["IsSelected"] = false;
                    return;
                }
                /*拆分量应小于库存订单量*/
                if (int.Parse(row["num"].ToString()) < int.Parse(e.Value.ToString()))
                {
                    XtraMessageBox.Show(Alading.Properties.Resources.SplitNumGreater, Alading.Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    row["IsSelected"] = false;
                    row["numSplit"] = string.Empty;
                    return;
                }
                row["numSplit"] = e.Value;
                /*修改拆分后自动勾选*/
                if (int.Parse(e.Value.ToString()) > 0)
                {
                    row["IsSelected"] = true;
                }
                if (int.Parse(e.Value.ToString()) <= 0)
                {
                    XtraMessageBox.Show(Alading.Properties.Resources.InvalidInput, Alading.Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                /*如果没有被勾选，修改量初始化为0*/
                if (Convert.ToBoolean(row["IsSelected"].ToString()) == false)
                {
                    row["numSplit"] = string.Empty;
                }
            }
            #endregion
        }

        #endregion

        #region  右键操作
        /// <summary>
        /// 右键拆分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitRight_Click(object sender, EventArgs e)
        {
            FunSplit(gcParentTrade, gvParentTrade);
        }

        /// <summary>
        /// 右键自动拆分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitAutoRight_Click(object sender, EventArgs e)
        {
            AutoSplit();
        }

        /// <summary>
        /// 右键合并
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void combineRight_Click(object sender, EventArgs e)
        {
            combine();
        }

        /// <summary>
        /// 右键全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectAllRight_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvChildOrder.RowCount; i++)
            {
                DataRow row = gvChildOrder.GetDataRow(i);
                if (row != null)
                {
                    row["IsSelected"] = true;
                }
            }
        }

        /// <summary>
        /// 右键保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveRight_Click(object sender, EventArgs e)
        {
            save();
        }
        #endregion
    }
}
