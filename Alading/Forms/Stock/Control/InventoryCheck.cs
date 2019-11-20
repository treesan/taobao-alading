using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Entity;
using Alading.Business;
using Alading.Core.Enum;
using Alading.Taobao;
using System.Collections;
using System.Linq;
using Alading.Utils;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.Utils;

namespace Alading.Forms.Stock.Control
{
    [ToolboxItem(false)]
    public partial class InventoryCheck : DevExpress.XtraEditors.XtraUserControl
    {
        private DataTable dTable=new DataTable();

        List<string> skuOuterIDList = new List<string>();

        public InventoryCheck()
        {
            InitializeComponent();
            gvProductCheck.BestFitColumns();
            gridViewDetail.BestFitColumns();
        }

        private void InventoryCheck_Load(object sender, EventArgs e)
        {
            comboBoxStockHouse.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            dTable.Columns.Add("SkuOuterID");//条形码
            dTable.Columns.Add("Name");
            dTable.Columns.Add("Model");        
            dTable.Columns.Add("SaleProps");
            dTable.Columns.Add("Specification");
            dTable.Columns.Add("SkuQuantity");
            dTable.Columns.Add("Quantity");//实际数量,由用户填写

            dTable.Columns.Add("ProfitType");//报溢报损,0正常1报溢2报损
            dTable.Columns.Add("CheckQuantity");//报溢报损数量
            dTable.Columns.Add(gcPrice.FieldName);
            dTable.Columns.Add("IsSelected", typeof(bool));
            dTable.Columns.Add("LayoutCode");
            dTable.Columns.Add("LayoutName");
            dTable.Columns.Add("StockUnitName");
            dTable.Columns.Add("StockUnitCode");        
            dTable.Columns.Add("OuterID");
            
            //确定日期
            textEditDate.Text = DateTime.Now.ToShortDateString().ToString();
            ComponentInit();
            gridViewHistoryDetail.BestFitColumns();
            gridViewDetail.BestFitColumns();
            gvProductCheck.BestFitColumns();
        }

        #region 公共方法

        /// <summary>
        /// 加载仓库信息
        /// </summary>
        private void ComponentInit()
        {
            //加载仓库
            List<StockHouse> stockHouseList = StockHouseService.GetAllStockHouse();
            Hashtable table = new Hashtable();
            int index = 0;
            foreach (StockHouse stockHouse in stockHouseList)
            {
                comboBoxStockHouse.Properties.Items.Add(stockHouse.HouseName);
                table.Add(index++, stockHouse.StockHouseCode);
            }
            comboBoxStockHouse.Tag = table;
        }

        /// <summary>
        /// 获取对应的盘点单及其
        /// </summary>
        /// <returns></returns>
        //private SortedList<StockCheck, List<StockCheckDetail>> GetStockCheckAndDetails()
        //{
        //    SortedList<StockCheck, List<StockCheckDetail>> stockCheckAndDetails = new SortedList<StockCheck, List<StockCheckDetail>>();
        //    StockCheck stockCheck = new StockCheck();
        //    //时间
        //    stockCheck.Created = DateTime.Parse(textEditDate.Text);
        //    if (textOperator.Tag != null)
        //    {
        //        stockCheck.OperatorCode = textOperator.Tag.ToString();/////////////// ///////////////         
        //    }
        //    else
        //    {
        //        stockCheck.OperatorCode = string.Empty;
        //    }
        //    //stockCheck.StockCheckCode = textCheckCode.Text;
        //    if (comboBoxStockHouse.Tag != null)
        //    {
        //        string[] codeList = comboBoxStockHouse.Tag.ToString().Split(',');
        //        if (codeList.Length > comboBoxStockHouse.SelectedIndex)
        //        {
        //            stockCheck.StockHouseCode = codeList[comboBoxStockHouse.SelectedIndex];
        //        }
        //    }

        //    List<StockCheckDetail> checkDetails = new List<StockCheckDetail>();
        //    for (int i = 0; i < gvProductCheck.RowCount; i++)
        //    {
        //        StockCheckDetail detail = new StockCheckDetail();          
        //        DataRow dRow = gvProductCheck.GetDataRow(i);

        //        if (dRow["CheckQuantity"] != null && dRow["CheckQuantity"].ToString() != string.Empty)
        //        {
        //            detail.CheckQuantity = int.Parse(dRow["CheckQuantity"].ToString());
        //        }
        //        if (dRow["ProfitType"] != null && dRow["ProfitType"].ToString() != string.Empty)
        //        {
        //            detail.ProfitType = int.Parse(dRow["ProfitType"].ToString());
        //        }
        //        if (dRow["Quantity"] != null && dRow["Quantity"].ToString() != string.Empty)
        //        {
        //            detail.Quantity = int.Parse(dRow["Quantity"].ToString());
        //        }
        //        if (dRow["SkuOuterID"] != null && dRow["SkuOuterID"].ToString() != string.Empty)
        //        {
        //            detail.SkuOuterID = dRow["SkuOuterID"].ToString();
        //        }
        //        if (dRow["SkuQuantity"] != null && dRow["SkuQuantity"].ToString() != string.Empty)
        //        {
        //            detail.SkuQuantity = int.Parse(dRow["SkuQuantity"].ToString());
        //        }

        //        detail.LayoutCode = "";

        //        if (gvProductCheck.GetRowCellValue(i, "LayoutCode") != null && gvProductCheck.GetRowCellValue(i, "LayoutCode").ToString() != string.Empty)
        //        {
        //            if (rICBLayout.Tag != null)
        //            {
        //                detail.LayoutCode = gvProductCheck.GetRowCellValue(i, "LayoutCode").ToString();
        //            }
        //        }

        //        detail.StockCheckCode = textCheckCode.Text;
        //        checkDetails.Add(detail);
        //    }

        //    stockCheckAndDetails.Add(stockCheck, checkDetails);
        //    return stockCheckAndDetails;
        //}

        #endregion

        #region 选择，删除商品
        /// <summary>
        /// 选择商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
            waitForm.Show();
            try
            {
                string stockHouseCode = string.Empty;
                if (comboBoxStockHouse.Text == string.Empty)
                {
                    waitForm.Hide();
                    XtraMessageBox.Show("请选择要盘点的仓库!", Constants.SYSTEM_PROMPT);                   
                    return;
                }
                if (comboBoxStockHouse.Tag != null)
                {
                    Hashtable tagTable = comboBoxStockHouse.Tag as Hashtable;
                    stockHouseCode = tagTable[comboBoxStockHouse.SelectedIndex].ToString();
                }
                DataTable table = new DataTable();
                ProductSelected ps = new ProductSelected(table, stockHouseCode);
                waitForm.Hide();
                ps.ShowDialog();
                waitForm.Show();
                foreach (DataRow row in table.Rows)
                {
                    if (row["SkuOuterID"] != null)
                    {
                        //如果包含则表明已经有该product
                        if (skuOuterIDList.Contains(row["SkuOuterID"].ToString()))
                        {
                            continue;
                        }

                    }
                    DataRow dRow = dTable.NewRow();
                    dRow["Name"] = row["Name"];
                    dRow["SkuOuterID"] = row["SkuOuterID"];//条形码
                    dRow["Specification"] = row["Specification"];
                    dRow["Model"] = row["Model"];
                    dRow[gcPrice.FieldName] = row["SkuPrice"];
                    //dRow["LayoutCode"];
                    //dRow["LayoutName"];
                    //dRow["StockUnitName"];
                    //dRow["StockUnitCode"];

                    dRow["SkuQuantity"] = row["Num"];
                    dRow["SaleProps"] = row["SaleProps"];
                    dRow["IsSelected"] = false;
                    dTable.Rows.Add(dRow);

                    skuOuterIDList.Add(row["SkuOuterID"].ToString());
                }
                gridProductCheck.DataSource = dTable;
                gvProductCheck.BestFitColumns();
                gridViewDetail.BestFitColumns();
                waitForm.Close();
            }
            catch (Exception ex)
            {
                waitForm.Close();
                XtraMessageBox.Show(ex.ToString(), Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
            waitForm.Show();
            try
            {
                int rowCount = gvProductCheck.RowCount;
                if (rowCount > 0)
                {
                    waitForm.Hide();
                    if (XtraMessageBox.Show("是否删除所有选中项", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        waitForm.Show();
                        gridCtrlDetail.DataSource = null;
                        for (int i = 0; i < rowCount; )
                        {
                            DataRow dRow = gvProductCheck.GetDataRow(i);
                            if (Convert.ToBoolean(dRow[gcIsSelected.FieldName].ToString()))
                            {
                                if (dRow["SkuOuterID"] != null && skuOuterIDList.Contains(dRow["SkuOuterID"].ToString()))
                                {
                                    skuOuterIDList.Remove(dRow["SkuOuterID"].ToString());
                                }
                                dTable.Rows.Remove(dRow);
                                rowCount = gvProductCheck.RowCount;
                            }
                            else
                            {
                                i++;
                            }
                        }
                    }
                }
                gvProductCheck.BestFitColumns();
                gridViewDetail.BestFitColumns();
                GetDetails();
                waitForm.Close();
            }
            catch (Exception ex)
            {
                waitForm.Close();
                XtraMessageBox.Show(ex.ToString(), Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
       
        #endregion       

        /// <summary>
        /// 选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvProductCheck_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == gcIsSelected)
            {
                DataRow row = gvProductCheck.GetFocusedDataRow();
                gvProductCheck.BeginUpdate();
                row[gcIsSelected.FieldName] = e.Value;
                gvProductCheck.EndUpdate();
            }
        }

        /// <summary>
        /// 填写实际数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvProductCheck_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == gcQuantity)
            {
                CalculateProfitOrLoss();
            }
        }

        /// <summary>
        /// 根据填写的实际数量做相应的处理
        /// </summary>
        void CalculateProfitOrLoss()
        {
            if (gvProductCheck.GetFocusedRowCellValue("Quantity") == null || gvProductCheck.GetFocusedRowCellValue("Quantity").ToString() == string.Empty)
            {
                gvProductCheck.SetFocusedRowCellValue("ProfitType", string.Empty);
                gvProductCheck.SetFocusedRowCellValue(gcCheckQuantity.FieldName, string.Empty);
                return;
            }
            if (gvProductCheck.GetFocusedRowCellValue("SkuQuantity") == null || gvProductCheck.GetFocusedRowCellValue("SkuQuantity").ToString() == string.Empty)
            {
                return;
            }
            //实际数量
            int quantity = int.Parse(gvProductCheck.GetFocusedRowCellValue("Quantity").ToString());
            //账面数量,即数据库数量
            int skuQuantity = int.Parse(gvProductCheck.GetFocusedRowCellValue("SkuQuantity").ToString());
            int checkQuantity = quantity - skuQuantity;
            if (checkQuantity > 0)
            {
                gvProductCheck.SetFocusedRowCellValue("ProfitType", (int)ProfitType.PROFIT);
                gvProductCheck.SetFocusedRowCellValue("CheckQuantity", checkQuantity);
            }
            else if (checkQuantity < 0)
            {
                gvProductCheck.SetFocusedRowCellValue("ProfitType", (int)ProfitType.LOSS);
                gvProductCheck.SetFocusedRowCellValue("CheckQuantity", -checkQuantity);
            }
            else
            {
                gvProductCheck.SetFocusedRowCellValue("ProfitType", (int)ProfitType.NORMAL);
                gvProductCheck.SetFocusedRowCellValue("CheckQuantity", 0);
            }
            gvProductCheck.BestFitColumns();
            gridViewDetail.BestFitColumns();
        }

        #region 归档
        /// <summary>
        /// 归档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void simpleBtnSave_Click(object sender, EventArgs e)
        //{
        //    if (comboBoxStockHouse.Text == string.Empty)
        //    {
        //        XtraMessageBox.Show("请选择仓库");
        //        return;
        //    }
        //    for (int i = 0; i < gvProductCheck.RowCount; i++)
        //    {
        //        if (gvProductCheck.GetRowCellValue(i,"Quantity") == null || gvProductCheck.GetRowCellValue(i,"Quantity").ToString() == string.Empty)
        //        {
        //            XtraMessageBox.Show("请输入实际数量",Constants.SYSTEM_PROMPT);
        //            gvProductCheck.FocusedRowHandle = i;
        //            return;
        //        }
        //    }
        //    SortedList<StockCheck, List<StockCheckDetail>> stockCheckAndDetails = GetStockCheckAndDetails();
        //    if (stockCheckAndDetails == null || stockCheckAndDetails.Count==0)
        //        return;
        //    if (StockProductService.AddStockCheckAndDetails(stockCheckAndDetails.Keys[0], stockCheckAndDetails.Values[0]) == ReturnType.Success)
        //    {
        //        XtraMessageBox.Show("归档成功", Constants.SYSTEM_PROMPT);
        //        dTable.Rows.Clear();
        //    }
        //}
   
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleBtnCancel_Click(object sender, EventArgs e)
        {
            dTable.Rows.Clear();
        }
        #endregion

        /// <summary>
        /// 仓库变化加载库位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxStockHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            dTable.Rows.Clear();
            skuOuterIDList.Clear();
            if (string.Empty == comboBoxStockHouse.Text)
            {
                return;
            }
            if (comboBoxStockHouse.Tag == null)
            {
                return;
            }
            Hashtable table = comboBoxStockHouse.Tag as Hashtable;
            string houseCode = table[comboBoxStockHouse.SelectedIndex].ToString();
            rICBLayout.Items.Clear();
            List<StockLayout> layOutList = StockLayoutService.GetStockLayout(i => i.StockHouseCode == houseCode);
            if (layOutList != null && layOutList.Count > 0)
            {
                Hashtable  tag = new Hashtable();
                int index = 0;
                foreach (StockLayout stockLayout in layOutList)
                {
                    rICBLayout.Items.Add(stockLayout.LayoutName);
                    tag.Add(index++,stockLayout.StockLayoutCode);
                }
                rICBLayout.Tag = tag;
            }
            gvProductCheck.BestFitColumns();
            gridViewDetail.BestFitColumns();
        }

        /// <summary>
        /// 焦点行变化，显示新焦点的出入库详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvProductCheck_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //WaitDialogForm waitForm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            //waitForm.Show();
            try
            {
                GetDetails();
                //waitForm.Close();
            }
            catch (Exception ex)
            {
                //waitForm.Close();
                XtraMessageBox.Show(ex.ToString(), Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 根据当前行获取当前焦点商品的出入库详细信息
        /// </summary>
        void GetDetails()
        {
            DataRow row = gvProductCheck.GetFocusedDataRow();
            if (row != null)
            {
                gridCtrlDetail.DataSource = null;
                string skuOuterId = row[gcSkuOuterID.FieldName].ToString();
                string layoutCode = row[gcLayoutCode.FieldName] != null ? row[gcLayoutCode.FieldName].ToString() : string.Empty;
                string houseCode = (comboBoxStockHouse.Tag as Hashtable)[comboBoxStockHouse.SelectedIndex].ToString();
                List<StockDetail> stockDetailList;
                List<HistoryStockDetail> historyDetailList;
                if (string.IsNullOrEmpty(layoutCode))
                {
                    stockDetailList = StockDetailService.GetStockDetail(c => c.ProductSkuOuterId == skuOuterId && c.StockHouseCode == houseCode);
                    historyDetailList = StockDetailService.GetHistoryDetail(c => c.ProductSkuOuterId == skuOuterId && c.StockHouseCode == houseCode);
                }
                else
                {
                    stockDetailList = StockDetailService.GetStockDetail(c => c.ProductSkuOuterId == skuOuterId && c.StockHouseCode == houseCode && c.StockLayOutCode == layoutCode);
                    historyDetailList = StockDetailService.GetHistoryDetail(c => c.ProductSkuOuterId == skuOuterId && c.StockHouseCode == houseCode && c.StockLayOutCode == layoutCode);
                }
                List<SonStockDetail> sonStockDetailList = new List<SonStockDetail>();
                foreach (StockDetail sd in stockDetailList)
                {
                    SonStockDetail ssd = new SonStockDetail(sd);
                    ssd.Type = UIHelper.GetEnumData("DetailType", ssd.DetailType);
                    sonStockDetailList.Add(ssd);
                }
                gridCtrlDetail.DataSource = sonStockDetailList;

                List<SonStockDetail> sonStockDetailList2 = new List<SonStockDetail>();
                foreach (HistoryStockDetail sd in historyDetailList)
                {
                    SonStockDetail ssd = new SonStockDetail(sd);
                    ssd.Type = UIHelper.GetEnumData("DetailType", ssd.DetailType);
                    sonStockDetailList2.Add(ssd);
                }
                gridCtrlHistoryDetail.DataSource = sonStockDetailList2;
            }

            gridViewDetail.BestFitColumns();
            gridViewHistoryDetail.BestFitColumns();
        }

        /// <summary>
        /// 库位变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rICBLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow row = gvProductCheck.GetFocusedDataRow();
            if (row != null)
            {
                gvProductCheck.BeginUpdate();
                string houseCode = (comboBoxStockHouse.Tag as Hashtable)[comboBoxStockHouse.SelectedIndex].ToString();
                ComboBoxEdit cbe = (ComboBoxEdit)sender;
                row[gcLayoutName.FieldName] = cbe.Properties.Items[cbe.SelectedIndex].ToString();

                Hashtable table = rICBLayout.Tag as Hashtable;
                string layoutCode = table[cbe.SelectedIndex].ToString();
                row[gcLayoutCode.FieldName] = layoutCode;
                List<StockHouseProduct> list = StockHouseService.GetStockHouseProduct(c => c.HouseCode == houseCode && c.LayoutCode == layoutCode && c.SkuOuterID == row[gcSkuOuterID.FieldName].ToString());
                if (list.Count > 0)
                {
                    StockHouseProduct houseProduct = list.First();
                    row[gcSkuQuantity.FieldName] = houseProduct.Num;
                }
                else
                {
                    row[gcSkuQuantity.FieldName] = 0;
                }
                gvProductCheck.EndUpdate();
                CalculateProfitOrLoss();
                GetDetails();
            }
            gvProductCheck.BestFitColumns();
            gridViewDetail.BestFitColumns();
        }

        /// <summary>
        /// 归档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
            waitForm.Show();
            try
            {
                if (string.IsNullOrEmpty(comboBoxStockHouse.Text))
                {
                    waitForm.Close();
                    XtraMessageBox.Show("请选择仓库进行盘点！");
                    return;
                }

                if (gvProductCheck.RowCount == 0)
                {
                    waitForm.Close();
                    XtraMessageBox.Show("请选择一件商品进行盘点！");
                    return;
                }
                if (string.IsNullOrEmpty(pceOperator.Text))
                {
                    waitForm.Close();
                    XtraMessageBox.Show("请选择操作人！");
                    return;
                }
                /*盘点必须选择具体库位，否则返回*/
                if (!IsChooseLayout())
                {
                    waitForm.Close();
                    XtraMessageBox.Show("请选择具体库位！");
                    return;
                }

                /*盘点必须输入实际数量，否则返回*/
                if (!IsInputQuantity())
                {
                    waitForm.Close();
                    XtraMessageBox.Show("请输入实际数量！");
                    return;
                }

                /*开始进行盘点*/

                #region 出入库单
                ///*实例化两个StockInOut，一个用于记录报损出库，一个用于记录报溢入库*/
                //Alading.Entity.StockInOut profitInout = new Alading.Entity.StockInOut();
                //Alading.Entity.StockInOut lossInout = new Alading.Entity.StockInOut();

                //#region 初始化赋值
                //profitInout.AmountTax = 0;
                //profitInout.DiscountFee = 0;
                //profitInout.DueFee = 0;
                //profitInout.FreightCode = string.Empty;
                //profitInout.FreightCompany = string.Empty;
                //profitInout.HouseCodeIn = string.Empty;
                //profitInout.HouseCodeOut = string.Empty;
                //profitInout.HouseNameIn = string.Empty;
                //profitInout.HouseNameOut = string.Empty;
                //profitInout.IncomeTime = DateTime.Now;
                ///*GUID*/
                //profitInout.InOutCode = System.Guid.NewGuid().ToString();
                //profitInout.InOutStatus = (int)InOutStatus.AllReach;
                //profitInout.InOutTime = DateTime.Now;
                //profitInout.InOutType = (int)InOutType.ProfitIn;
                //profitInout.IsSettled = true;
                ///*操作人，暂时为空*/
                //profitInout.OperatorCode = string.Empty;
                //profitInout.OperatorName = string.Empty;

                //profitInout.PayTerm = DateTime.Now;
                //profitInout.PayThisTime = 0;
                //profitInout.PayType = (int)PayType.OTHER;
                //profitInout.SearchText = string.Empty;
                //profitInout.TradeOrderCode = string.Empty;
                //profitInout.TransportCode = string.Empty;

                //lossInout.AmountTax = 0;
                //lossInout.DiscountFee = 0;
                //lossInout.DueFee = 0;
                //lossInout.FreightCode = string.Empty;
                //lossInout.FreightCompany = string.Empty;
                //lossInout.HouseCodeIn = string.Empty;
                //lossInout.HouseCodeOut = string.Empty;
                //lossInout.HouseNameIn = string.Empty;
                //lossInout.HouseNameOut = string.Empty;
                //lossInout.IncomeTime = DateTime.Now;
                ///*GUID*/
                //lossInout.InOutCode = System.Guid.NewGuid().ToString();
                //lossInout.InOutStatus = (int)InOutStatus.AllReach;
                //lossInout.InOutTime = DateTime.Now;
                //lossInout.InOutType = (int)InOutType.ProfitIn;
                //lossInout.IsSettled = true;
                ///*操作人，暂时为空*/
                //lossInout.OperatorCode = string.Empty;
                //lossInout.OperatorName = string.Empty;

                //lossInout.PayTerm = DateTime.Now;
                //lossInout.PayThisTime = 0;
                //lossInout.PayType = (int)PayType.OTHER;
                //lossInout.SearchText = string.Empty;
                //lossInout.TradeOrderCode = string.Empty;
                //lossInout.TransportCode = string.Empty;
                //#endregion
                #endregion

                Hashtable table = comboBoxStockHouse.Tag as Hashtable;
                string houseCode = table[comboBoxStockHouse.SelectedIndex].ToString();
                int rowCount = gvProductCheck.RowCount;
                /*盘点详情列表*/
                //List<StockCheckDetail> stockCheckDetailList = new List<StockCheckDetail>();

                #region 盘点单

                StockCheck stockCheck = new StockCheck();
                stockCheck.Created = DateTime.Now;
                /*操作人*/
                stockCheck.OperatorCode = pceOperator.Tag!=null ?pceOperator.Tag.ToString():string.Empty;
                /*盘点单号*/
                //string year = DateTime.Now.Year.ToString();
                //string month = DateTime.Now.Month.ToString();
                //string day = DateTime.Now.Day.ToString();
                //string hour = DateTime.Now.Hour.ToString();
                //string minute = DateTime.Now.Minute.ToString();
                //string second = DateTime.Now.Second.ToString();
                //stockCheck.StockCheckCode = "PDD-"+year+month+day+"-"+hour+minute+second;
                stockCheck.StockCheckCode = !string.IsNullOrEmpty(textEditCheckCode.Text) ? textEditCheckCode.Text.Trim() : System.Guid.NewGuid().ToString();
                stockCheck.StockHouseCode = houseCode;
                #endregion

                if (StockDetailService.AddStockCheck(stockCheck) == ReturnType.Success)
                {
                    for (int i = 0; i < rowCount; i++)
                    {
                        DataRow row = gvProductCheck.GetDataRow(i);
                        string profitType = row[gcProfitType.FieldName].ToString();
                        string skuOuterId = row[gcSkuOuterID.FieldName].ToString();
                        int factQuantity = int.Parse(row[gcQuantity.FieldName].ToString());
                        int skuQuantity = int.Parse(row[gcSkuQuantity.FieldName].ToString());
                        string layoutCode = row[gcLayoutCode.FieldName].ToString();
                        string layoutName = row[gcLayoutName.FieldName].ToString();

                        #region 盘点入库Detail
                        StockDetail stockDetail = new StockDetail();
                        stockDetail.DetailRemark = string.Empty;
                        stockDetail.DetailType = (int)DetailType.CheckIn;
                        stockDetail.DurabilityDate = DateTime.Now;
                        stockDetail.HouseName = comboBoxStockHouse.Text;
                        stockDetail.InOutCode = string.Empty;
                        stockDetail.LayoutName = layoutName;
                        /*价格问题？？？？？？*/
                        stockDetail.Price = row[gcPrice.FieldName] != null && row[gcPrice.FieldName].ToString() != string.Empty ? float.Parse(row[gcPrice.FieldName].ToString()) : 0;
                        stockDetail.ProductSkuOuterId = skuOuterId;
                        stockDetail.Quantity = factQuantity;
                        /*搜索字段*/
                        stockDetail.SearchText = string.Empty;
                        stockDetail.StockDetailCode = System.Guid.NewGuid().ToString();
                        stockDetail.StockHouseCode = houseCode;
                        stockDetail.StockLayOutCode = layoutCode;
                        stockDetail.Tax = string.Empty;
                        stockDetail.TotalFee = stockDetail.Price * stockDetail.Quantity;
                        #endregion

                        #region 盘点详情

                        StockCheckDetail stockCheckDetail = new StockCheckDetail();
                        /*报溢/报损数量*/
                        stockCheckDetail.CheckQuantity = factQuantity - skuQuantity;
                        if (stockCheckDetail.CheckQuantity < 0)
                        {
                            stockCheckDetail.CheckQuantity = -stockCheckDetail.CheckQuantity;
                        }

                        stockCheckDetail.LayoutCode = layoutCode;
                        /*实际数量*/
                        stockCheckDetail.Quantity = factQuantity;
                        /*搜索字段*/
                        stockCheckDetail.SearchText = string.Empty;
                        stockCheckDetail.SkuOuterID = skuOuterId;
                        /*账面数量*/
                        stockCheckDetail.SkuQuantity = skuQuantity;
                        stockCheckDetail.StockCheckCode = stockCheck.StockCheckCode;
                        #endregion

                        /*将所有该仓库该库位的商品出入库详情取出，放入历史表*/
                        List<StockDetail> hisStockDetailList = StockDetailService.GetStockDetail(c => c.ProductSkuOuterId == skuOuterId && c.StockHouseCode == houseCode && c.StockLayOutCode == layoutCode);

                        if (profitType == "1")
                        {
                            /*报溢处理*/
                            StockDetail profitDetail = new StockDetail();
                            profitDetail.DetailRemark = "报溢入库";
                            profitDetail.DetailType = (int)DetailType.ProfitIn;
                            profitDetail.DurabilityDate = stockDetail.DurabilityDate;
                            profitDetail.HouseName = stockDetail.HouseName;
                            profitDetail.InOutCode = string.Empty;
                            profitDetail.LayoutName = stockDetail.LayoutName;
                            profitDetail.Price = stockDetail.Price;
                            profitDetail.ProductSkuOuterId = stockDetail.ProductSkuOuterId;
                            /*报溢入库数量=实际数量-账面数量*/
                            profitDetail.Quantity = factQuantity - skuQuantity;
                            /*搜索字段*/
                            profitDetail.SearchText = string.Empty;
                            profitDetail.StockDetailCode = System.Guid.NewGuid().ToString();
                            profitDetail.StockHouseCode = houseCode;
                            profitDetail.StockLayOutCode = layoutCode;
                            profitDetail.Tax = string.Empty;
                            /*总花费*/
                            profitDetail.TotalFee = profitDetail.Quantity * profitDetail.Price;
                            hisStockDetailList.Add(profitDetail);

                            /*盘点详情的类型为报溢*/
                            stockCheckDetail.ProfitType = (int)ProfitType.PROFIT;
                        }
                        else if (profitType == "2")
                        {
                            /*报损处理*/
                            StockDetail LossDetail = new StockDetail();
                            LossDetail.DetailRemark = "报损入库";
                            LossDetail.DetailType = (int)DetailType.LossOut;
                            LossDetail.DurabilityDate = stockDetail.DurabilityDate;
                            LossDetail.HouseName = stockDetail.HouseName;
                            LossDetail.InOutCode = string.Empty;
                            LossDetail.LayoutName = stockDetail.LayoutName;
                            LossDetail.Price = stockDetail.Price;
                            LossDetail.ProductSkuOuterId = stockDetail.ProductSkuOuterId;
                            /*报损出库数量=实际数量-账面数量*/
                            LossDetail.Quantity = skuQuantity - factQuantity;
                            /*搜索字段*/
                            LossDetail.SearchText = string.Empty;
                            LossDetail.StockDetailCode = System.Guid.NewGuid().ToString();
                            LossDetail.StockHouseCode = houseCode;
                            LossDetail.StockLayOutCode = layoutCode;
                            LossDetail.Tax = string.Empty;
                            /*总花费*/
                            LossDetail.TotalFee = LossDetail.Quantity * LossDetail.Price;
                            hisStockDetailList.Add(LossDetail);

                            /*盘点详情的类型为报损*/
                            stockCheckDetail.ProfitType = (int)ProfitType.LOSS;
                        }
                        else
                        {
                            /*正常处理*/
                            stockCheckDetail.ProfitType = (int)ProfitType.NORMAL;
                        }
                        /*盘点详情列表*/
                        //stockCheckDetailList.Add();
                        /*盘点单需要的参数：1、类型（报溢/报损）2、报溢或报损数量 3、历史出入库详情 4、盘点入库详情 5、盘点单列表*/
                        StockDetailService.Check(factQuantity - skuQuantity, hisStockDetailList, stockDetail, stockCheckDetail);
                    }
                }
                else
                {
                    /**/
                }
                gvProductCheck.BestFitColumns();
                gridViewDetail.BestFitColumns();
                waitForm.Close();
            }
            catch (Exception ex)
            {
                waitForm.Close();
                XtraMessageBox.Show(ex.ToString(), Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 验证是否选择了具体库位
        /// </summary>
        /// <returns></returns>
        bool IsChooseLayout()
        {
            int rowCount = gvProductCheck.RowCount;
            for (int i = 0; i < rowCount; i++)
            {
                DataRow row = gvProductCheck.GetDataRow(i);
                if (row != null)
                {
                    if (row[gcLayoutName.FieldName] == null || row[gcLayoutName.FieldName].ToString() == string.Empty)
                    {                      
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 验证是否输入了实际数量
        /// </summary>
        /// <returns></returns>
        bool IsInputQuantity()
        {
            int rowCount = gvProductCheck.RowCount;
            for (int i = 0; i < rowCount; i++)
            {
                DataRow row = gvProductCheck.GetDataRow(i);
                if (row != null)
                {
                    if (row[gcQuantity.FieldName] == null || row[gcQuantity.FieldName].ToString() == string.Empty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        InOutHelper inoutHelper = new InOutHelper();

        /// <summary>
        /// 加载业务员结点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pceOperator_Popup(object sender, EventArgs e)
        {
            try
            {
                inoutHelper.GetOperator(tlOperator);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 选择操作人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlOperator_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                TreeListHitInfo hitInfo = tlOperator.CalcHitInfo(new Point(e.X, e.Y));
                //如果单击到单元格内
                if (hitInfo.HitInfoType == HitInfoType.Cell)
                {
                    TreeListNode clickedNode = hitInfo.Node;
                    if (clickedNode != null && !clickedNode.HasChildren)
                    {
                        pceOperator.Text = clickedNode.GetDisplayText(0);
                        pceOperator.Tag = clickedNode.Tag;
                        pceOperator.ClosePopup();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            int rowCount = gvProductCheck.RowCount;
            gvProductCheck.BeginUpdate();
            for(int i=0;i<rowCount;i++)
            {
                DataRow row = gvProductCheck.GetDataRow(i);
                row[gcIsSelected.FieldName] = ((CheckEdit)sender).Checked;
            }
            gvProductCheck.EndUpdate();
        }
    }

    class SonStockDetail : StockDetail
    {
        public SonStockDetail(StockDetail sd)
        {
            this.DetailRemark = sd.DetailRemark;
            this.DetailType = sd.DetailType;
            this.DurabilityDate = sd.DurabilityDate;
            this.HouseName = sd.HouseName;
            this.InOutCode = sd.InOutCode;
            this.LayoutName = sd.LayoutName;
            this.Price = sd.Price;
            this.ProductSkuOuterId = sd.ProductSkuOuterId;
            this.Quantity = sd.Quantity;
            this.SearchText = sd.SearchText;
            this.StockDetailCode = sd.StockDetailCode;
            this.StockHouseCode = sd.StockHouseCode;
            this.StockLayOutCode = sd.StockLayOutCode;
            this.Tax = sd.Tax;
            this.TotalFee = sd.TotalFee;
            this.Type = string.Empty;
        }

        public SonStockDetail(HistoryStockDetail sd)
        {
            this.DetailRemark = sd.DetailRemark;
            this.DetailType = sd.DetailType;
            this.DurabilityDate = sd.DurabilityDate;
            this.HouseName = sd.HouseName;
            this.InOutCode = sd.InOutCode;
            this.LayoutName = sd.LayoutName;
            this.Price = sd.Price;
            this.ProductSkuOuterId = sd.ProductSkuOuterId;
            this.Quantity = sd.Quantity;
            this.SearchText = sd.SearchText;
            this.StockDetailCode = sd.HistoryStockDetailCode;
            this.StockHouseCode = sd.StockHouseCode;
            this.StockLayOutCode = sd.StockLayOutCode;
            this.Tax = sd.Tax;
            this.TotalFee = sd.TotalFee;
            this.Type = string.Empty;
        }

        public string Type { get; set; }
    }
}
