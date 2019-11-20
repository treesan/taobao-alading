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
using DevExpress.XtraPrinting;
using DevExpress.XtraTab;

namespace Alading.Forms.Trade.Controls
{
    [ToolboxItem(false)]
    public partial class TradeExportExcel : DevExpress.XtraEditors.XtraUserControl
    {
        DataSet dataset = new DataSet();
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="tradeList">需要导出数据的TradeList</param>
        public TradeExportExcel(DataSet dataSet)
        {
            InitializeComponent();
            this.dataset = dataSet;
            InitExportGridView(dataset);
            
        }

        private void InitExportGridView(DataSet dataSet)
        {
            LoadTrade(gcExportExcel, dataSet);
        }

        public void ExportExcel()
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "导出Excel";
                saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
                DialogResult dialogResult = saveFileDialog.ShowDialog(this);
                if (dialogResult == DialogResult.OK)
                {
                    XlsExportOptions options = new XlsExportOptions();
                    gcExportExcel.ExportToXls(saveFileDialog.FileName, options);
                    DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (System.Exception ex)
            {
            	
            }
            finally
            {
                dataset.Clear();
            }
            
        }

        /// <summary>
        /// 将一个TradeList中的数据绑定到GridControl中  方便导出数据
        /// </summary>
        /// <param name="tabParentGC">需要绑定的tabParentGC</param>
        /// <param name="currentTabTradeList">传入参数可以使用List<View_TradeStock>类型的 为保持一致
        /// 用 List<IGrouping<....>></param>
        private void LoadTrade(GridControl tabParentGC, DataSet dataSet)
        {
            #region  创建Order冗余表
            //////////////////////////////////////////////////////////////////////////
            ///创建一个DataSet数据库，将所需展示数据存入两张虚拟表tradeTable,
            ///orderTable.并利用CustomTid来将两张表联系起来，GridControl自动绑定上去
            //////////////////////////////////////////////////////////////////////////
            DataTable itemTable = dataSet.Tables.Add("OrderList");    
            itemTable.Columns.Add("RecordMessage").Caption="交易信息";
            itemTable.Columns.Add("CustomTid");
            itemTable.Columns.Add("type").Caption = "交易类别";
            itemTable.Columns.Add("receiver_name").Caption = "收货人姓名";
            itemTable.Columns.Add("buyer_email").Caption = "收货人邮箱";
            itemTable.Columns.Add("seller_nick").Caption = "卖家昵称";
            itemTable.Columns.Add("buyer_nick").Caption = "买家昵称";
            itemTable.Columns.Add("created").Caption = "创建时间";
            itemTable.Columns.Add("ShopName").Caption = "所属网店";
            itemTable.Columns.Add("ShopType").Caption = "所属网店类别";
            itemTable.Columns.Add("TradePayment").Caption = "交易应付金额";
            itemTable.Columns.Add("post_fee").Caption = "邮费";
            itemTable.Columns.Add("LockedUserName").Caption = "锁定备注";
            itemTable.Columns.Add("LockedTime").Caption = "锁定时间";
            itemTable.Columns.Add("HasInvoice").Caption = "是否开票";
            itemTable.Columns.Add("alipay_no").Caption = "买家支付宝账号";
            itemTable.Columns.Add("TradeIsLockProduct").Caption = "是否缺货";
            itemTable.Columns.Add("tid").Caption = "交易id";
            itemTable.Columns.Add("iid").Caption = "商品id";
            itemTable.Columns.Add("ItemName").Caption = "商品名";
            itemTable.Columns.Add("sku_properties_name").Caption = "商品属性";
            itemTable.Columns.Add("LeftQuantity").Caption = "库存剩余数";
            itemTable.Columns.Add("num").Caption = "购买数";
            itemTable.Columns.Add("price").Caption = "商品价格";
            itemTable.Columns.Add("adjust_fee").Caption = "调整价";
            itemTable.Columns.Add("payment").Caption = "应付金额";
            itemTable.Columns.Add("OrderType").Caption = "商品种类";
            itemTable.Columns.Add("TradeIsLackProduct").Caption = "交易货物状态";
            itemTable.Columns.Add("ProductIsLack").Caption = "订单货物状态";
            #endregion

            #region 初始化数据

            /* 计算当前的交易顺序号 为导出数据编号 */
            Int32 tradeCounter=1;
            foreach (DataRow tradeCur in dataSet.Tables[0].Rows)
            {
                #region  order表初始化
                /*用于计算缺货与否*/
                /*用于消除赠品的影响*/
                DataRow tradeRow = itemTable.NewRow();
                tradeRow["CustomTid"] = tradeCur["CustomTid"];
                tradeRow["RecordMessage"] = "交易" + (tradeCounter).ToString();
                tradeRow["type"] = tradeCur["type"];
                tradeRow["receiver_name"] = tradeCur["receiver_name"];
                tradeRow["created"] = tradeCur["created"];
                tradeRow["buyer_nick"] = tradeCur["buyer_nick"];
                tradeRow["seller_nick"] =  tradeCur["seller_nick"];
                tradeRow["buyer_email"] = tradeCur["buyer_email"];
                tradeRow["post_fee"] = tradeCur["post_fee"];
                tradeRow["TradePayment"] = tradeCur["tradePayment"];
                tradeRow["ShopName"] = tradeCur["ShopName"];
                tradeRow["ShopType"] = tradeCur["ShopType"];
                tradeRow["LockedUserName"] = tradeCur["LockedUserName"];
                tradeRow["LockedTime"] = tradeCur["LockedTime"];
                tradeRow["HasInvoice"] =  tradeCur["HasInvoice"] ;
                tradeRow["alipay_no"] = tradeCur["alipay_no"];
                tradeRow["TradeIsLackProduct"] = tradeCur["TradeIsLackProduct"];
                itemTable.Rows.Add(tradeRow);

                /* 计算当前的订单顺序号 为导出数据编号 */
                Int32 orderCounter=1;
                foreach (DataRow orderCur in tradeCur.GetChildRows(Alading.Taobao.Constants.TRADE_ORDER_RELATION))
                {
                    DataRow orderRow = itemTable.NewRow();
                    orderRow["RecordMessage"] = "交易" + (tradeCounter).ToString() + "-订单" + orderCounter.ToString();
                    orderRow["tid"] = orderCur["tid"];
                    orderRow["iid"] = orderCur["iid"];
                    orderRow["CustomTid"] = orderCur["CustomTid"];
                    orderRow["ItemName"] = orderCur["ItemName"];
                    orderRow["sku_properties_name"] = orderCur["sku_properties_name"];
                    orderRow["LeftQuantity"] = orderCur["LeftQuantity"];
                    orderRow["num"] = orderCur["num"] ;
                    orderRow["price"] = orderCur["price"];
                    orderRow["payment"] = orderCur["payment"] ;
                    orderRow["adjust_fee"] = orderCur["adjust_fee"];
                    orderRow["OrderType"] = orderCur["OrderType"];
                    orderRow["ProductIsLack"] = orderCur["ProductIsLack"];
                #endregion
                    itemTable.Rows.Add(orderRow);
                    orderCounter++;
                }

                tradeCounter++;
            }
            tabParentGC.DataSource = dataSet.Tables["OrderList"];
            tabParentGC.ForceInitialize();//强制初始化
            #endregion
        }
    }
}