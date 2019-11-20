using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;
//using Aspose.Cells;
using System.Data.OleDb;
using Alading.Entity;
using Alading.Business;
using System.Linq;

namespace Alading.Forms.PurchaseManager
{
    public partial class PurchaseOrderList : DevExpress.XtraEditors.XtraForm
    {

        public DataTable dt = new DataTable();
        public List<PurchaseOrder> purOrderList = PurchaseOrderService.GetAllPurchaseOrder();
        public List<string> purOrderCodeList = new List<string>();
 
        public PurchaseOrderList()
        {
            InitializeComponent();
            Init();
            MainTabPage.PageVisible = true;
            InvalidTabPage.PageVisible = false;
            NotCheckTabPage.PageVisible = false;
            PerformingTabPage.PageVisible = false;
            PerformedTabPage.PageVisible = false;
        }

        private void PurchaseOrder_Load(object sender, EventArgs e)
        {

        } 

        public void Init()
        {           
            dt.Columns.Add("IsDelete", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("OrderCode", typeof(string));
            dt.Columns.Add("Provider", typeof(string));
            dt.Columns.Add("TotalSum", typeof(double));
            dt.Columns.Add("BusinessDate", typeof(string));
            dt.Columns.Add("DeliveryDate", typeof(string));
            dt.Columns.Add("OrderStatus", typeof(string));
            dt.Columns.Add("CheckStatus", typeof(string));
            dt.Columns.Add("OrderDeadline",typeof(int));
            GetPurchaseOrder(purOrderCodeList);     
        }

        /// <summary>
        /// 刷新采购单表格
        /// </summary>
        public void RefreshGridControl()
        {
            dt.Rows.Clear();
            MainTabPage.PageVisible = true;
            InvalidTabPage.PageVisible = false;
            NotCheckTabPage.PageVisible = false;
            PerformingTabPage.PageVisible = false;
            PerformedTabPage.PageVisible = false;
            List<PurchaseOrder> newPurchaseOrderList = new List<PurchaseOrder>();
            newPurchaseOrderList = PurchaseOrderService.GetAllPurchaseOrder();
            foreach (PurchaseOrder purOrder in newPurchaseOrderList)
            {
                DataRow row = dt.NewRow();
                row["IsDelete"] = false;
                row["OrderCode"] = purOrder.PurchaseOrderCode;
                row["Provider"] = purOrder.SupplierName;
                row["BusinessDate"] = purOrder.OrderTime;
                row["DeliveryDate"] = purOrder.OrderTime;
                row["OrderStatus"] = StatusToString(purOrder.OrderStatus);
                if (purOrder.OrderStatus == 1)
                {
                    row["CheckStatus"] = "未审批";
                }
                else
                {
                    row["CheckStatus"] = "已审批";
                }
                dt.Rows.Add(row);
            }

            gridControl1.DataSource = dt;
        }

        public DataTable GetPurchaseOrderList(List<PurchaseOrder> purOderList)
        {
            foreach (PurchaseOrder purOrder in purOderList)
            {
                DataRow row = dt.NewRow();
                row["IsDelete"] = false;
                row["OrderCode"] = purOrder.PurchaseOrderCode;
                row["Provider"] = purOrder.SupplierName;
                row["BusinessDate"] = purOrder.OrderTime;
                row["DeliveryDate"] = purOrder.OrderTime;
                row["OrderStatus"] = StatusToString(purOrder.OrderStatus);
                if (purOrder.OrderStatus == 1)
                {
                    row["CheckStatus"] = "未审批";
                }
                else
                {
                    row["CheckStatus"] = "已审批";
                }
                DateTime timeNow = DateTime.Parse(DateTime.Now.ToShortDateString());
                DateTime purDeliveryDate = DateTime.Parse(purOrder.OrderTime.ToString("yyyy-MM-dd"));
                TimeSpan st= timeNow- purDeliveryDate;
                int days = st.Days;
                row["OrderDeadLine"] = days;
                dt.Rows.Add(row);
            }
            return dt;
        }

        /// <summary>
        /// 将各个不同的status转化为字符串类型进行提示[OrderStatus]
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string StatusToString(int status)
        {
            if ((OrderStatus)status == OrderStatus.NotPerformed)  // 1代表“未执行”
            {
                return "未执行";
            }
            if((OrderStatus)status == OrderStatus.Performing)
            {
                return "执行中";
            }
            if((OrderStatus)status == OrderStatus.Performed)
            {
                return "已执行";
            }
            if ((OrderStatus)status == OrderStatus.Invalid)
            {
                return "已作废";
            }
            return "";
        }

       /// <summary>
        /// 为了从新建的采购订单[NewPurchaseOrder]中获取数据
       /// </summary>
       /// <param name="purchaseorderCodeList">被选中的商品code列表</param>
        public void GetPurchaseOrder(List<string> purchaseorderCodeList)
        {
            gridControl1.DataSource = GetPurchaseOrderList(purOrderList);
        }

           

        /// <summary>
        ///  删除采购订单被选中的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var orderList = from order in purOrderList
                            select order.PurchaseOrderCode;
            foreach (var list in orderList)
            {
                purOrderCodeList.Add(list);
            }
            for (int delRow = 0; delRow < gridView1.RowCount; delRow++)
            {
                DataRow row = dt.Rows[delRow];
                string purOrderCode = row["OrderCode"].ToString();
                if (Convert.ToBoolean(row["IsDelete"]) == true)
                {
                    purOrderCodeList.Remove(purOrderCode);
                    PurchaseOrderService.RemovePurchaseOrder(purOrderCode);
                    PurchaseProductService.RemovePurchaseProduct(purOrderCode);
                    // PurchaseProductService(purOrderCode);
                    dt.Rows.Remove(row);
                }
            }           
            gridControl1.DataSource = dt;
        }

        /// <summary>
        /// Excel中数据导入到采购订单 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //TODO: (WXC) 导出Excel文件
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            DialogResult dialogResult = saveFileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                //gridControl1.ExportToXls(saveFileDialog.FileName, options);
                gridControl1.ExportToExcelOld(saveFileDialog.FileName);
                DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        ///  采购订单中数据导出到Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string fileName = string.Empty;
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Office   Documents(*.xls)|*.xls";
                if (DialogResult.OK == dialog.ShowDialog())
                {
                    fileName = dialog.FileName;
                }
                if (fileName != "")
                {
                    string connectionString;
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;'";
                    OleDbConnection con = new OleDbConnection(connectionString);//连接到指定的Excel文件 
                    con.Open();
                    DataTable schemaTable = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                    //StringBuilder builder = new StringBuilder();
                    //for(int i = 0; i<schemaTable.Columns.Count;i++) //{
                    //    builder.Append(schemaTable.Columns[i].Caption);
                    //    builder.Append("; ");
                    //}MessageBox.Show(builder.ToString());

                    string strSQL = string.Format("SELECT * FROM [{0}]", schemaTable.Rows[0]["TABLE_NAME"]);
                    OleDbCommand command = new OleDbCommand(strSQL, con);
                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    gridControl1.DataSource = table;
                    con.Close();
                    adapter.Dispose();

                    //Workbook workbook = new Workbook();
                    //workbook.Open(fileName);
                    ////Worksheet sheet = workbook.Worksheets[0];
                    ////Cells cells = sheet.Cells;
                    //////获取excel中的数据保存到一个datatable中
                    ////DataTable dt_Import = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, false);
                    ////// dt_Import.
                    ////gridControl1.DataSource = dt_Import;
                    //Cells cells = workbook.Worksheets[0].Cells;
                    //int rowCount = cells.Rows.Count;
                    //int colCount = cells.Columns.Count;
                    //dt.Clear();
                    //// Cells.ExportDataTable(DataTable,firstRow, firstColumn, rowNumber, exportColumnNames) 
                    ////to export worksheet with the same datatypes as per your data table. 
                    ////workbook.Worksheets[0].Cells.ExportDataTable(dt, 1, 0, rowCount - 1, colCount - 1, false);
                    ////DataTable exportDTable = workbook.Worksheets[0].Cells.ExportDataTable( 0, 0, rowCount, colCount, true);
                    ////gridControl1.DataSource = exportDTable;
                    //dt = workbook.Worksheets[0].Cells.ExportDataTable(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, false);
                    //gridControl1.DataSource = dt;
                    //Worksheets wsts = workbook.Worksheets;
                    //for (int i = 0; i < wsts.Count; i++)
                    //{
                    //    if (wsts[i].Name.ToLower() == "sheetname")
                    //    {
                    //        Worksheet wst = wsts[i];
                    //        int maxR = wst.Cells.MaxRow;
                    //        int maxC = wst.Cells.MaxColumn;
                    //        if (maxR > 0 && maxC > 0)
                    //        {
                    //           dt = wst.Cells.ExportDataTableAsString(0, 0, maxR + 1, maxC + 1, true);
                    //           gridControl1.DataSource = dt;
                    //        }
                    //    }
                    //}  

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///  新建采购订单 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem7_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NewPurchaseOrder nPurOrder = new NewPurchaseOrder();
            nPurOrder.ShowDialog();
            RefreshGridControl();
        }

        /// <summary>
        ///  刷新采购订单中数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshGridControl();
        }       

        /// <summary>
        ///  选中删除商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(e.RowHandle);
            if (e.Column == gvDelete)
            {
                gridView1.BeginDataUpdate();
              //  gridView1.
                row["IsDelete"] = e.Value;
                gridView1.EndDataUpdate();
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //DataRow row = gridView1.GetDataRow(e.RowHandle);
            //if (e.Column == gvDelete)
            //{
            //    gridView1.BeginDataUpdate();
            //    row["IsDelete"] = e.Value;
            //    gridView1.EndDataUpdate();
            //}
        }

        /// <summary>
        ///  双击单元格，对每一个采购订单进行操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                DataRow row = gridView1.GetDataRow(e.RowHandle);
                string purOrderCode = row["OrderCode"].ToString();
                PurchaseOrderDetail purOrderDetail = new PurchaseOrderDetail(purOrderCode);
                purOrderDetail.ShowDialog();
            }
            else { 
            
            }
        }


        #region  按时间查询
        /// <summary>
        ///  所有的订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllOrderItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            bool result = MainTabPage.PageVisible;
            dt.Rows.Clear();
            RefreshGridControl();
        }

        /// <summary>
        ///  本周新增的订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThisWeekOrderItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DateTime dTime = System.DateTime.Now;
            //MessageBox.Show(dTime.DayOfWeek.ToString("d"));

            string weekNow = DateTime.Now.ToString();
            int intervalDays = Convert.ToInt32(dTime.DayOfWeek.ToString("d")); // 计算两个之间相隔的天数
            string weekStart = DateTime.Now.AddDays(-intervalDays).ToString("yyyy-MM-dd 00:00:00");
            //MessageBox.Show(weekStart.ToString());

            List<PurchaseOrder> weekOrderList = PurchaseOrderService.GetPurchaseOrder(Convert.ToDateTime(weekStart), Convert.ToDateTime(weekNow));
            
            dt.Rows.Clear();
            gridControl1.DataSource = GetPurchaseOrderList(weekOrderList);
            
        }

        /// <summary>
        ///  本月新增的订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThisMonthOrderItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DateTime dTime = System.DateTime.Now;
            string weekNow = DateTime.Now.ToString();
            //MessageBox.Show(weekNow.ToString());
            int intervalDays = Convert.ToInt32(dTime.Day.ToString("d")); // 计算两个之间相隔的天数
            string weekStart = DateTime.Now.AddDays(-intervalDays+1).ToString("yyyy-MM-dd 00:00:00");
            //MessageBox.Show(weekStart.ToString());

            List<PurchaseOrder> monthOrderList = PurchaseOrderService.GetPurchaseOrder(Convert.ToDateTime(weekStart), Convert.ToDateTime(weekNow));
            
            dt.Rows.Clear();             
            gridControl1.DataSource = GetPurchaseOrderList(monthOrderList);
        }

        /// <summary>
        ///  近15天内新增的订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HalfMonthOrderItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DateTime dTime = System.DateTime.Now;
            string weekNow = DateTime.Now.ToString();
            //MessageBox.Show(weekNow.ToString());
            int intervalDays = 15; // 计算两个之间相隔的天数
            string weekStart = DateTime.Now.AddDays(-intervalDays + 1).ToString("yyyy-MM-dd 00:00:00");
            //MessageBox.Show(weekStart.ToString());

            List<PurchaseOrder> monthOrderList = PurchaseOrderService.GetPurchaseOrder(Convert.ToDateTime(weekStart), Convert.ToDateTime(weekNow));

            dt.Rows.Clear();
            gridControl1.DataSource = GetPurchaseOrderList(monthOrderList);
        }

        /// <summary>
        ///  三个月内新增的订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreeMonthOrderItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DateTime dTime = System.DateTime.Now;
            string weekNow = DateTime.Now.ToString();
            //MessageBox.Show(weekNow.ToString());
            int intervalDays = 90; // 计算两个之间相隔的天数
            string weekStart = DateTime.Now.AddDays(-intervalDays + 1).ToString("yyyy-MM-dd 00:00:00");
            //MessageBox.Show(weekStart.ToString());

            List<PurchaseOrder> monthOrderList = PurchaseOrderService.GetPurchaseOrder(Convert.ToDateTime(weekStart), Convert.ToDateTime(weekNow));

            dt.Rows.Clear();
            gridControl1.DataSource = GetPurchaseOrderList(monthOrderList);
        }

        #endregion

        #region  按状态查询

        /// <summary>
        /// 审批状态查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckStatusItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            NotCheckTabPage.PageVisible = true;
            MainTabPage.PageVisible = false;
            PerformedTabPage.PageVisible = false;
            PerformingTabPage.PageVisible = false;
            InvalidTabPage.PageVisible = false;
            NotCheckTabPageInit();            
        }
        
        private void OrderStatusItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            
        }

        private void PerformingStatusItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {            
            PerformingTabPage.PageVisible = true;
            PerformedTabPage.PageVisible = false;
            MainTabPage.PageVisible = false;
            InvalidTabPage.PageVisible = false;
            NotCheckTabPage.PageVisible = false;
            PerformingTabPageInit();
        }

        private void PerformedStatusItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            PerformedTabPage.PageVisible = true;
            PerformingTabPage.PageVisible = false;
            MainTabPage.PageVisible = false;
            InvalidTabPage.PageVisible = false;
            NotCheckTabPage.PageVisible = false;
            PerformedTabPageInit();
        }

        #endregion


        #region  审批 订单状态的初始化
        
        /// <summary>
        ///  待审批的采购单据
        /// </summary>
        public void NotCheckTabPageInit()
        {
            int status = Convert.ToInt32(OrderStatus.NotPerformed);
            List<PurchaseOrder> purOrderList = PurchaseOrderService.GetStatusPurchaseOrder(status);            
            dt.Rows.Clear();            
            NotCheckControl.DataSource = GetPurchaseOrderList(purOrderList);
        }

        /// <summary>
        ///  未到货的采购订单
        /// </summary>
        public void PerformingTabPageInit()
        {
            int status = Convert.ToInt32(OrderStatus.Performing);
            List<PurchaseOrder> purOrderList = PurchaseOrderService.GetStatusPurchaseOrder(status);
            dt.Rows.Clear();
            PerformingControl.DataSource = GetPurchaseOrderList(purOrderList);
        }

        /// <summary>
        ///  到货的采购单据
        /// </summary>
        public void PerformedTabPageInit()
        {
            int status = Convert.ToInt32(OrderStatus.Performed);
            List<PurchaseOrder> purOrderList = PurchaseOrderService.GetStatusPurchaseOrder(status);
            dt.Rows.Clear();
            PerformedControl.DataSource = GetPurchaseOrderList(purOrderList); 
        }
        #endregion

        private void PerformingControl_Click(object sender, EventArgs e)
        {

        }       

                
    }

    #region   与采购订单相关的枚举

    /// <summary>
    ///  单据状态的枚举
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// 所有的单据
        /// </summary>
        All,
        /// <summary>
        /// 未执行
        /// </summary>
        NotPerformed,
        /// <summary>
        /// 执行中
        /// </summary>
        Performing,
        /// <summary>
        /// 已完成
        /// </summary>
        Performed,
        /// <summary>
        /// 已作废
        /// </summary>
        Invalid

    }

    /// <summary>
    ///  审批状态的枚举
    /// </summary>
    public enum CheckStatus
    {
        /// <summary>
        /// 所有
        /// </summary>
        All,
        /// <summary>
        /// 未审批
        /// </summary>
        NotChecked,
        /// <summary>
        ///  审批中
        /// </summary>
        Checking,
        /// <summary>
        /// 已驳回
        /// </summary>
        Rejected,
        /// <summary>
        /// 已审批
        /// </summary>
        Checked

    }

    /// <summary>
    /// 回执状态
    /// </summary>
    public enum ReceiptStatus
    {
        /// <summary>
        /// 所有
        /// </summary>
        All,
        /// <summary>
        /// 未回执
        /// </summary>
        NotReceipted,
        /// <summary>
        ///  已认可
        /// </summary>
        Endorsed,
        /// <summary>
        ///  不认可
        /// </summary>
        NotEndorsed
    }

    /// <summary>
    /// 发送状态
    /// </summary>
    public enum SendStatus
    {
        /// <summary>
        ///  所有
        /// </summary>
        All,
        /// <summary>
        ///  未发送
        /// </summary>
        NotSent,
        /// <summary>
        ///  已发送
        /// </summary>
        Sent
    }

    #endregion
    
}