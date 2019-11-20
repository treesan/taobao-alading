using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Forms.Stock.Control;
using Alading.Entity;
using Alading.Business;
using System.Collections;
using Alading.Core.Enum;
using System.Linq;
using Alading.Taobao;

namespace Alading.Forms.Stock.Allocation
{
    public partial class AllocationAdd : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 新增还是修改的标志,true为新增
        /// </summary>
        bool IsAddFlag = true;
        private DataTable dTable = new DataTable();
        ProductSelect ps = new ProductSelect();
        public AllocationAdd()
        {
            InitializeComponent();
            this.Text = "库存调拨-新增";
            AddColumns();
            dateInOutTime.DateTime = DateTime.Now;
            textOperatorName.Text = Alading.Utils.SystemHelper.Name;
        }
        public AllocationAdd(Alading.Entity.StockInOut stockInOut, DataTable dTable)
        {
            InitializeComponent();

            IsAddFlag = false;
            this.Text = "库存调拨-修改";
            AddColumns();//给table增加row
            textOperatorName.Text = stockInOut.OperatorName;
            dateInOutTime.DateTime = stockInOut.InOutTime;
            textInOutCode.Text = stockInOut.InOutCode;
            comboStockOut.Text = stockInOut.HouseNameOut;
            comboStockIn.Text = stockInOut.HouseNameIn;
            simpleBtnSaveAdd.Visible = false;
            foreach (DataRow dRow in dTable.Rows)
            {
                DataRow newRow = this.dTable.NewRow();
                newRow["IsSelected"] = false;
                newRow["Name"] = dRow["Name"];
                newRow["OuterID"] = dRow["OuterID"];
                newRow["Specification"] = dRow["Specification"];
                newRow["Model"] = dRow["Model"];
                newRow["SaleProps"] = dRow["SaleProps"];
                newRow["Num"] = StockHouseService.GetQuantity(dRow["SkuOuterID"].ToString(), stockInOut.HouseCodeOut, dRow["LayoutCodeOut"].ToString());//调出仓库库位库存量
                newRow["SkuOuterID"] = dRow["SkuOuterID"];
                newRow["SkuQuantity"] = dRow["SkuQuantity"];
                newRow["LayoutCodeIn"] = dRow["LayoutCodeIn"];
                newRow["LayoutCodeOut"] = dRow["LayoutCodeOut"];                            
                newRow["LayoutNameIn"] = dRow["LayoutNameIn"];          
                newRow["LayoutNameOut"] = dRow["LayoutNameOut"];
                this.dTable.Rows.Add(newRow);
            }

            int rowHandle = gVStockProduct.FocusedRowHandle;
            //gVStockProduct.FocusedRowChanged -= gVStockProduct_FocusedRowChanged;
            gridStockProduct.DataSource = this.dTable;
            if (rowHandle == 0 && gVStockProduct.FocusedRowHandle > -1)
            {
                FocusedRowChange();
            }

            //加载出库库位
            string tag = string.Empty;
            DataRow dataRow = gVStockProduct.GetFocusedDataRow();
            if (dataRow != null)
            {
                string skuOuterID = dataRow["SkuOuterID"].ToString();
                List<StockHouseProduct> stockLayoutList = StockHouseService.GetStockHouseProduct(i => i.SkuOuterID == skuOuterID && i.HouseCode == stockInOut.HouseCodeOut);
                foreach (StockHouseProduct vStockHouse in stockLayoutList)
                {
                    if (string.IsNullOrEmpty(vStockHouse.LayoutName))
                        continue;
                    repositoryItemComboBoxLayoutOut.Items.Add(vStockHouse.LayoutName);
                    tag += vStockHouse.LayoutCode + ",";
                }
                repositoryItemComboBoxLayoutOut.Tag = tag.Trim(',');
            }

            //加载入库库位
            string tagIn = string.Empty;
            List<StockLayout> LayoutList = StockLayoutService.GetStockLayout(i => i.StockHouseCode == stockInOut.HouseCodeIn);
            foreach (StockLayout layout in LayoutList)
            {
                if (string.IsNullOrEmpty(layout.LayoutName))
                    continue;
                repositoryItemComboBoxLayoutIn.Items.Add(layout.LayoutName);
                tagIn += layout.StockLayoutCode + ",";
            }
            repositoryItemComboBoxLayoutIn.Tag = tagIn.Trim(',');

        }


        private void AddColumns()
        {
            dTable.Columns.Add("IsSelected", typeof(bool));
            //dTable.Columns.Add("CatName");
            dTable.Columns.Add("Name");
            dTable.Columns.Add("SkuOuterID");
            //dTable.Columns.Add("StockCatName");
            dTable.Columns.Add("OuterID");
            dTable.Columns.Add("Specification");
            dTable.Columns.Add("Model");
            dTable.Columns.Add("SkuQuantity");
            dTable.Columns.Add("SaleProps");

            dTable.Columns.Add("Num");//调出仓库库位库存量

            //dTable.Columns.Add("HouseNameOut");
            dTable.Columns.Add("LayoutNameOut");
            //dTable.Columns.Add("HouseNameIn");
            dTable.Columns.Add("LayoutNameIn");
            //dTable.Columns.Add("StockHouseCodeIn");
            //dTable.Columns.Add("StockHouseCodeOut");
            dTable.Columns.Add("LayoutCodeIn");
            dTable.Columns.Add("LayoutCodeOut");
        }

        /// <summary>
        /// 选择商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (comboStockOut.Text == string.Empty)
                {
                    XtraMessageBox.Show("请选择调出仓库", Constants.SYSTEM_PROMPT);
                    return;
                }
                if (comboStockIn.Text == string.Empty)
                {
                    XtraMessageBox.Show("请选择调入仓库", Constants.SYSTEM_PROMPT);
                    return;
                }
                if (comboStockIn.Text == comboStockOut.Text)
                {
                    XtraMessageBox.Show("调出仓库和调入仓库不能相同", Constants.SYSTEM_PROMPT);
                    return;
                }
                string stockHouseCode = string.Empty;
                if (comboStockOut.Tag != null)
                {
                    string[] codeList = comboStockOut.Tag.ToString().Split(',');
                    if (codeList.Length > comboStockOut.SelectedIndex)
                    {
                        stockHouseCode = codeList[comboStockOut.SelectedIndex];
                    }
                }

                DataTable table = new DataTable();
                ProductSelected ps = new ProductSelected(table, stockHouseCode);
                ps.ShowDialog();
                foreach (DataRow row in table.Rows)
                {
                    DataRow dRow = dTable.NewRow();
                    dRow["Name"] = row["Name"];
                    dRow["SkuOuterID"] = row["SkuOuterID"];
                    dRow["OuterID"] = row["OuterID"];
                    dRow["Specification"] = row["Specification"];
                    dRow["Model"] = row["Model"];
                    dRow["SkuQuantity"] = 0;
                    dRow["SaleProps"] = row["SaleProps"];
                    dRow["IsSelected"] = false;

                    dTable.Rows.Add(dRow);

                }
                int rowHandle = gVStockProduct.FocusedRowHandle;
                //gVStockProduct.FocusedRowChanged -= gVStockProduct_FocusedRowChanged;
                gridStockProduct.DataSource = dTable;
                if (rowHandle == 0 && gVStockProduct.FocusedRowHandle > -1)
                {
                    FocusedRowChange();
                }
                //gVStockProduct.FocusedRowChanged += gVStockProduct_FocusedRowChanged;
                gVStockProduct.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message,Constants.SYSTEM_PROMPT);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                List<int> rowChecked = GetRowChecked();
                if (rowChecked != null && rowChecked.Count > 0)
                {
                    if (XtraMessageBox.Show("是否删除所有选中项", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        List<DataRow> rowList = new List<DataRow>();
                        foreach (int row in rowChecked)
                        {
                            rowList.Add(gVStockProduct.GetDataRow(row));
                        }
                        foreach (DataRow dataRow in rowList)
                        {
                            dTable.Rows.Remove(dataRow);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }


        /// <summary>
        /// 获取被选中的行
        /// </summary>
        /// <returns></returns>
        private List<int> GetRowChecked()
        {
            List<int> rowChecked = new List<int>();
            for (int i = 0; i < gVStockProduct.RowCount; i++)
            {
                if (gVStockProduct.GetRowCellValue(i, "IsSelected") != null)
                {
                    bool IsCheck = (bool)gVStockProduct.GetRowCellValue(i, "IsSelected");
                    if (IsCheck == true)
                        rowChecked.Add(i);
                }
            }
            return rowChecked;
        }

        /// <summary>
        /// 焦点行改变触发,加载该商品的所在仓库
        /// </summary>
        private void FocusedRowChange()
        {
            try
            {
                DataRow dRow = gVStockProduct.GetFocusedDataRow();
                string skuOuterID = string.Empty;
                if (dRow != null && dRow["SkuOuterID"] != null)
                {
                    skuOuterID = dRow["SkuOuterID"].ToString();
                }
                if (skuOuterID != string.Empty)
                {
                    //加载出库库位
                    string stockHouseCode = string.Empty;
                    if (comboStockOut.Tag != null)
                    {
                        string[] codeList = comboStockOut.Tag.ToString().Split(',');
                        if (codeList.Length > comboStockOut.SelectedIndex)
                        {
                            stockHouseCode = codeList[comboStockOut.SelectedIndex];
                        }
                    }
                    List<StockHouseProduct> stockHouseListOut = StockHouseService.GetStockHouseProduct(i => i.SkuOuterID == skuOuterID && i.HouseCode == stockHouseCode);
                    repositoryItemComboBoxLayoutOut.Items.Clear();
                    string tag = string.Empty;
                    foreach (StockHouseProduct stockHouse in stockHouseListOut)
                    {
                        repositoryItemComboBoxLayoutOut.Items.Add(stockHouse.LayoutName);
                        tag += stockHouse.LayoutCode + ",";
                    }
                    repositoryItemComboBoxLayoutOut.Tag = tag.Trim(',');
                }

                //加载入库库位
                //if (comboStockIn.Text != string.Empty)
                //{
                //    string stockHouseCode = string.Empty;
                //    if (comboStockOut.Tag != null)
                //    {
                //        string[] codeList = comboStockOut.Tag.ToString().Split(',');
                //        if (codeList.Length > comboStockOut.SelectedIndex)
                //        {
                //            stockHouseCode = codeList[comboStockOut.SelectedIndex];
                //        }
                //    }
                //    //HouseCode为空             
                //    if (string.IsNullOrEmpty(stockHouseCode))
                //    {
                //        repositoryItemComboBoxLayoutIn.Items.Clear();
                //    }
                //    else
                //    {
                //        //加载Layout
                //        repositoryItemComboBoxLayoutIn.Items.Clear();
                //        List<StockLayout> layOutList = StockLayoutService.GetStockLayout(i => i.StockHouseCode == stockHouseCode);
                //        if (layOutList != null && layOutList.Count > 0)
                //        {
                //            string tag = string.Empty;

                //            foreach (StockLayout stockLayout in layOutList)
                //            {
                //                repositoryItemComboBoxLayoutIn.Items.Add(stockLayout.LayoutName);
                //                tag += stockLayout.StockLayoutCode + ",";
                //            }
                //            repositoryItemComboBoxLayoutIn.Tag = tag.Trim(',');
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }


        /// <summary>
        /// 获取调拨单
        /// </summary>
        private SortedList<List<Alading.Entity.StockInOut>, List<StockDetail>> GetStockInOutDetail()
        {
            try
            {
                // || textOperatorName.Text == string.Empty
                if (dateInOutTime.Text == string.Empty
                    || comboStockOut.Text == string.Empty || comboStockIn.Text == string.Empty)
                {
                    XtraMessageBox.Show("带*号的为必填项", Constants.SYSTEM_PROMPT);
                    return null;
                }

                List<Alading.Entity.StockInOut> stockInOutList = new List<Alading.Entity.StockInOut>();

                Alading.Entity.StockInOut stockOut = new Alading.Entity.StockInOut();
                stockOut.AmountTax = 0;
                stockOut.DiscountFee = 0;
                stockOut.DueFee = 0;
                stockOut.FreightCode = string.Empty;
                stockOut.FreightCompany = string.Empty;
                if (textInOutCode.Text == string.Empty)
                {
                    stockOut.InOutCode = Guid.NewGuid().ToString();
                }
                else
                {
                    stockOut.InOutCode = textInOutCode.Text;//调拨单编号
                }
                stockOut.InOutTime = dateInOutTime.DateTime;
                stockOut.InOutType = (int)InOutType.AllocateOut;
                stockOut.InOutStatus = (int)InOutStatus.AllReach;
                stockOut.TradeOrderCode = string.Empty;
                if (Alading.Utils.SystemHelper.User != null)
                {
                    stockOut.OperatorCode = Alading.Utils.SystemHelper.User.UserCode;
                }
                else
                {
                    stockOut.OperatorCode = string.Empty;
                }
                stockOut.OperatorName = textOperatorName.Text;
                stockOut.PayType = (int)PayType.CASH;

                stockOut.IsSettled = true;
                stockOut.PayTerm = 0;
                stockOut.IncomeTime = DateTime.MinValue;
                stockOut.PayThisTime = 0;

                string stockHouseCodeOut = string.Empty;
                if (comboStockOut.Tag != null)
                {
                    string[] codeList = comboStockOut.Tag.ToString().Split(',');
                    if (codeList.Length > comboStockOut.SelectedIndex)
                    {
                        stockHouseCodeOut = codeList[comboStockOut.SelectedIndex];
                    }
                }

                string stockHouseCodeIn = string.Empty;
                if (comboStockIn.Tag != null)
                {
                    string[] codeList = comboStockIn.Tag.ToString().Split(',');
                    if (codeList.Length > comboStockIn.SelectedIndex)
                    {
                        stockHouseCodeIn = codeList[comboStockIn.SelectedIndex];
                    }
                }

                stockOut.HouseNameOut = comboStockOut.Text;
                stockOut.HouseCodeOut = stockHouseCodeOut;
                stockOut.HouseNameIn = comboStockIn.Text;
                stockOut.HouseCodeIn = stockHouseCodeIn;

                Alading.Entity.StockInOut stockIn = new Alading.Entity.StockInOut();
                stockIn.AmountTax = 0;
                stockIn.DiscountFee = 0;
                stockIn.DueFee = 0;
                stockIn.FreightCode = string.Empty;
                stockIn.FreightCompany = string.Empty;
                //注意需要一样才行
                stockIn.InOutCode = stockOut.InOutCode;
                stockIn.InOutStatus = stockOut.InOutStatus;
                stockIn.InOutTime = dateInOutTime.DateTime;
                stockIn.InOutType = (int)InOutType.AllocateIn;
                stockIn.TradeOrderCode = string.Empty;

                stockIn.OperatorCode = stockOut.OperatorCode;
                stockIn.OperatorName = textOperatorName.Text;
                stockIn.PayType = (int)PayType.CASH;

                stockIn.IsSettled = true;
                stockIn.PayTerm = 0;
                stockIn.IncomeTime = DateTime.MinValue;
                stockIn.PayThisTime = 0;

                stockIn.HouseNameOut = comboStockOut.Text;
                stockIn.HouseCodeOut = stockHouseCodeOut;
                stockIn.HouseNameIn = comboStockIn.Text;
                stockIn.HouseCodeIn = stockHouseCodeIn;

                stockInOutList.Add(stockIn);
                stockInOutList.Add(stockOut);


                List<StockDetail> stockDetailList = new List<StockDetail>();
                for (int i = 0; i < gVStockProduct.RowCount; i++)
                {
                    #region 判断库位

                    if (gVStockProduct.GetRowCellValue(i, "LayoutCodeOut") != null)
                    {
                        if (gVStockProduct.GetRowCellValue(i, "LayoutCodeOut").ToString() == string.Empty)
                        {
                            XtraMessageBox.Show("请选择调出库位");
                            return null;
                        }
                    }

                    if (gVStockProduct.GetRowCellValue(i, "LayoutCodeIn") != null)
                    {
                        if (gVStockProduct.GetRowCellValue(i, "LayoutCodeIn").ToString() == string.Empty)
                        {
                            XtraMessageBox.Show("请选择调入库位");
                            return null;
                        }
                    }

                    #endregion

                    #region 判断数量

                    int houseOutNum = int.Parse(gVStockProduct.GetRowCellValue(i, "Num").ToString());

                    if (gVStockProduct.GetRowCellValue(i, "SkuQuantity") != null)
                    {
                        string num = gVStockProduct.GetRowCellValue(i, "SkuQuantity").ToString();
                        if (num == string.Empty || int.Parse(num) == 0)
                        {
                            XtraMessageBox.Show("调拨数量应大于0");
                            return null;
                        }
                        else
                        {
                            if (int.Parse(num) > houseOutNum)
                            {
                                XtraMessageBox.Show("调拨数量应不大于调出仓库库存量");
                                return null;
                            }
                        }
                    }
                    #endregion

                    //调出
                    StockDetail stockDetailOut = new StockDetail();
                    if (gVStockProduct.GetRowCellValue(i, "SkuOuterID") != null)
                        stockDetailOut.ProductSkuOuterId = gVStockProduct.GetRowCellValue(i, "SkuOuterID").ToString();
                    stockDetailOut.DetailRemark = string.Empty;
                    stockDetailOut.DetailType = (int)Alading.Core.Enum.DetailType.AllocateOut;
                    stockDetailOut.DurabilityDate = DateTime.Now;
                    stockDetailOut.InOutCode = stockOut.InOutCode;
                    stockDetailOut.Price = 0;
                    stockDetailOut.Quantity = int.Parse(gVStockProduct.GetRowCellValue(i, "SkuQuantity").ToString());
                    stockDetailOut.StockDetailCode = Guid.NewGuid().ToString();

                    stockDetailOut.HouseName = comboStockOut.Text;
                    stockDetailOut.StockHouseCode = stockHouseCodeOut;

                    //if (gVStockProduct.GetRowCellValue(i, "LayoutCodeOut").ToString() != null)
                    //{
                    stockDetailOut.StockLayOutCode = gVStockProduct.GetRowCellValue(i, "LayoutCodeOut").ToString();
                    //}
                    stockDetailOut.LayoutName = gVStockProduct.GetRowCellValue(i, "LayoutNameOut").ToString();

                    //else
                    //{
                    //    stockDetailOut.StockLayOutCode = string.Empty;
                    //}
                    stockDetailOut.Tax = string.Empty;
                    stockDetailOut.TotalFee = 0;
                    //调入
                    StockDetail stockDetailIn = new StockDetail();
                    stockDetailIn.Tax = string.Empty;
                    stockDetailIn.TotalFee = 0;
                    stockDetailIn.DetailRemark = string.Empty;
                    stockDetailIn.DurabilityDate = DateTime.Now;
                    stockDetailIn.InOutCode = stockIn.InOutCode;
                    stockDetailIn.Price = 0;
                    stockDetailIn.Quantity = stockDetailOut.Quantity;
                    stockDetailIn.ProductSkuOuterId = stockDetailOut.ProductSkuOuterId;
                    stockDetailIn.DetailType = (int)DetailType.AllocateIn;
                    stockDetailIn.StockDetailCode = Guid.NewGuid().ToString();

                    stockDetailIn.HouseName = comboStockIn.Text;
                    stockDetailIn.StockHouseCode = stockHouseCodeIn;

                    //if (gVStockProduct.GetRowCellValue(i, "LayoutCodeIn").ToString() != null)
                    //{
                    stockDetailIn.StockLayOutCode = gVStockProduct.GetRowCellValue(i, "LayoutCodeIn").ToString();
                    //}
                    stockDetailIn.LayoutName = gVStockProduct.GetRowCellValue(i, "LayoutNameIn").ToString();

                    //else
                    //{
                    //    stockDetailIn.StockLayOutCode = string.Empty;
                    //}

                    stockDetailList.Add(stockDetailOut);
                    stockDetailList.Add(stockDetailIn);
                }

                SortedList<List<Alading.Entity.StockInOut>, List<StockDetail>> sortedStockDetail = new SortedList<List<Alading.Entity.StockInOut>, List<StockDetail>>();
                sortedStockDetail.Add(stockInOutList, stockDetailList);
                return sortedStockDetail;
            }
            catch (Exception ex)
            {                
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
                return null;
            }
        }

        /// <summary>
        /// 将组建值清空
        /// </summary>
        private void ComponentInit()
        {
            textInOutCode.Text = string.Empty;
            textOperatorName.Text = string.Empty;
            dateInOutTime.Text = string.Empty;
            comboStockIn.Text = string.Empty;
            comboStockOut.Text = string.Empty;
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SortedList<List<Alading.Entity.StockInOut>, List<StockDetail>> stockDetail = GetStockInOutDetail();
                if (stockDetail != null && stockDetail.Count > 0)
                {
                    //新增
                    if (IsAddFlag == true)
                    {
                        ReturnType type = StockInOutService.AddStockInOutDetail(stockDetail.Keys[0], stockDetail.Values[0]);
                        if (type == ReturnType.Success)
                        {
                            XtraMessageBox.Show("保存成功", Constants.SYSTEM_PROMPT);
                            this.Close();
                        }
                        else if (type == ReturnType.PropertyExisted)
                        {
                            XtraMessageBox.Show("调拨单编号重复,请重输", Constants.SYSTEM_PROMPT);
                        }
                        else
                        {
                            XtraMessageBox.Show("保存失败", Constants.SYSTEM_PROMPT);
                        }
                    }
                    //else
                    //{
                    //    //修改

                    //    XtraMessageBox.Show("还没写好怎么更新", Constants.SYSTEM_PROMPT);

                    //}
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }
        /// <summary>
        /// 保存并新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleBtnSaveAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SortedList<List<Alading.Entity.StockInOut>, List<StockDetail>> stockDetail = GetStockInOutDetail();
                if (stockDetail != null && stockDetail.Count > 0)
                {
                    StockInOutService.AddStockInOutDetail(stockDetail.Keys[0], stockDetail.Values[0]);
                }
                dTable.Rows.Clear();
                ComponentInit();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }
        

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleBtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AllocationAdd_Load(object sender, EventArgs e)
        {
            gVStockProduct.BestFitColumns();
            //出库及入库仓库
            comboStockOut.Properties.Items.Clear();
            comboStockIn.Properties.Items.Clear();
            List<StockHouse> stockHouseList = StockHouseService.GetAllStockHouse();
            string houseCode = string.Empty;
            foreach (StockHouse stockHouse in stockHouseList)
            {
                comboStockOut.Properties.Items.Add(stockHouse.HouseName);
                comboStockIn.Properties.Items.Add(stockHouse.HouseName);
                houseCode += stockHouse.StockHouseCode + ",";
            }
            comboStockOut.Tag = houseCode.Trim(',');
            comboStockIn.Tag = houseCode.Trim(',');
        }

        private void gVStockProduct_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.FieldName)
            {
                case "IsSelected":
                    gVStockProduct.SetFocusedRowCellValue("IsSelected", e.Value);
                    break;

                case "HouseNameOut":
                    gVStockProduct.SetFocusedRowCellValue("HouseNameOut", e.Value);
                    break;

                case "HouseNameIn":
                    gVStockProduct.SetFocusedRowCellValue("HouseNameIn", e.Value);
                    break;

                case "LayoutNameOut":
                    gVStockProduct.SetFocusedRowCellValue("LayoutNameOut", e.Value);
                    break;

                case "LayoutNameIn":
                    gVStockProduct.SetFocusedRowCellValue("LayoutNameIn", e.Value);
                    break;
            }
        }

        private void repositoryItemComboBoxHouseOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            //gVStockProduct.SetFocusedRowCellValue("LayoutCodeOut", string.Empty);
            //gVStockProduct.SetFocusedRowCellValue("LayoutNameOut", string.Empty);
            //gVStockProduct.SetFocusedRowCellValue("Num", string.Empty);
            //repositoryItemComboBoxLayoutOut.Items.Clear();

            //if (repositoryItemComboBoxHouseOut.Tag != null)
            //{
            //    int index = ((DevExpress.XtraEditors.ComboBoxEdit)(sender)).SelectedIndex;
            //    string[] codeList = repositoryItemComboBoxHouseOut.Tag.ToString().Split(',');
            //    //repositoryItemComboBoxLayoutOut.Items.Clear();
            //    if (index != -1 && codeList.Length > index)
            //    {
            //        string houseCode = codeList[index];
            //        gVStockProduct.SetFocusedRowCellValue("StockHouseCodeOut", houseCode);

            //        //出库库位
            //        string tag = string.Empty;
            //        DataRow dRow = gVStockProduct.GetFocusedDataRow();
            //        if (dRow != null)
            //        {
            //            string skuOuterID = dRow["SkuOuterID"].ToString();
            //            List<StockHouseProduct> stockLayoutList = StockHouseService.GetStockHouseProduct(i => i.SkuOuterID == skuOuterID && i.HouseCode == houseCode);
            //            foreach (StockHouseProduct vStockHouse in stockLayoutList)
            //            {
            //                if (string.IsNullOrEmpty(vStockHouse.LayoutName))
            //                    continue;
            //                repositoryItemComboBoxLayoutOut.Items.Add(vStockHouse.LayoutName);
            //                tag += vStockHouse.LayoutCode + ",";
            //            }
            //            repositoryItemComboBoxLayoutOut.Tag = tag.Trim(',');
            //        }
            //    }
            //    else
            //    {
            //        gVStockProduct.SetFocusedRowCellValue("StockHouseCodeOut", string.Empty);
            //        gVStockProduct.SetFocusedRowCellValue("LayoutCodeOut", string.Empty);
            //        gVStockProduct.SetFocusedRowCellValue("LayoutNameOut", string.Empty);
            //        gVStockProduct.SetFocusedRowCellValue("Num", string.Empty);
            //    }
            //}          
        }

        private void repositoryItemComboBoxHouseIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (repositoryItemComboBoxHouseIn.Tag != null)
            //{
            //    int index = ((DevExpress.XtraEditors.ComboBoxEdit)(sender)).SelectedIndex;
            //    string[] codeList = repositoryItemComboBoxHouseIn.Tag.ToString().Split(',');
            //    repositoryItemComboBoxLayoutIn.Items.Clear();
            //    if (index != -1 && codeList.Length > index)
            //    {
            //        string houseCode = codeList[index];
            //        gVStockProduct.SetFocusedRowCellValue("StockHouseCodeIn", houseCode);                    

            //        if (houseCode != string.Empty)
            //        {
            //            //加载Layout
            //            List<StockLayout> layOutList = StockLayoutService.GetStockLayout(i => i.StockHouseCode == houseCode);
            //            if (layOutList != null && layOutList.Count > 0)
            //            {
            //                string tag = string.Empty;

            //                foreach (StockLayout stockLayout in layOutList)
            //                {
            //                    repositoryItemComboBoxLayoutIn.Items.Add(stockLayout.LayoutName);
            //                    tag += stockLayout.StockLayoutCode + ",";
            //                }
            //                ////新加一个空白项
            //                //repositoryItemComboBoxLayoutIn.Items.Add(string.Empty);
            //                //tag += "";
            //                repositoryItemComboBoxLayoutIn.Tag = tag.Trim(',');
            //            }
            //        }
            //        else
            //        {
            //            gVStockProduct.SetFocusedRowCellValue("LayoutCodeIn", string.Empty);
            //            gVStockProduct.SetFocusedRowCellValue("LayoutNameIn", string.Empty);
            //        }
            //    }
            //}
            //else
            //{
            //    repositoryItemComboBoxLayoutIn.Items.Clear();
            //}
        }

        private void repositoryItemComboBoxLayoutIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = ((DevExpress.XtraEditors.ComboBoxEdit)(sender)).SelectedIndex;
            string[] codeList = repositoryItemComboBoxLayoutIn.Tag.ToString().Split(',');
            if (index != -1 && codeList.Length > index)
            {
                gVStockProduct.SetFocusedRowCellValue("LayoutCodeIn", codeList[index]);
            }
            else
            {
                gVStockProduct.SetFocusedRowCellValue("LayoutCodeIn", string.Empty);
            }
        }

        private void repositoryItemComboBoxLayoutOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = ((DevExpress.XtraEditors.ComboBoxEdit)(sender)).SelectedIndex;
            string[] codeList = repositoryItemComboBoxLayoutOut.Tag.ToString().Split(',');
            if (index != -1 && codeList.Length > index)
            {
                DataRow dRow = gVStockProduct.GetFocusedDataRow();
                gVStockProduct.SetFocusedRowCellValue("LayoutCodeOut", codeList[index]);
                if (dRow != null)
                {
                    string stockHouseCode = string.Empty;
                    if (comboStockOut.Tag != null)
                    {
                        string[] houseCodeList = comboStockOut.Tag.ToString().Split(',');
                        if (houseCodeList.Length > comboStockOut.SelectedIndex)
                        {
                            stockHouseCode = houseCodeList[comboStockOut.SelectedIndex];
                        }
                    }

                    int num = StockHouseService.GetQuantity(dRow["SkuOuterID"].ToString(), stockHouseCode, codeList[index]);//调出仓库库位库存量
                    gVStockProduct.SetFocusedRowCellValue("Num", num);
                }
            }
            else
            {
                gVStockProduct.SetFocusedRowCellValue("LayoutCodeOut", string.Empty);
            }
        }

        //private void repositoryItemPopupContainerEditSelect_QueryPopUp(object sender, CancelEventArgs e)
        //{
        //    popupContainerControlSelect.Controls.Clear();
        //    DataTable dataTable = new DataTable();
        //    ps = new ProductSelect(true, dataTable, repositoryItemPopupContainerEditSelect);
        //    popupContainerControlSelect.Controls.Add(ps);
        //    popupContainerControlSelect.Show();
        //}

        //private void repositoryItemPopupContainerEditSelect_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        //{
        //    if (ps.dTable.Rows.Count == 0)
        //        return;

        //    dTable.Rows.Clear();
        //    foreach (DataRow row in ps.dTable.Rows)
        //    {
        //        DataRow dRow = dTable.NewRow();
        //        dRow["Name"] = row["Name"];
        //        dRow["SkuOuterID"] = row["SkuOuterID"];
        //        dRow["OuterID"] = row["OuterID"];
        //        dRow["Specification"] = row["Specification"];
        //        dRow["Model"] = row["Model"];
        //        dRow["SkuQuantity"] = 0;
        //        dRow["SaleProps"] = row["SaleProps"];
        //        dRow["IsSelected"] = false;

        //        dTable.Rows.Add(dRow);

        //    }
        //    int rowHandle = gVStockProduct.FocusedRowHandle;
        //    //gVStockProduct.FocusedRowChanged -= gVStockProduct_FocusedRowChanged;
        //    gridStockProduct.DataSource = dTable;
        //    if (rowHandle == 0 && gVStockProduct.FocusedRowHandle > -1)
        //    {
        //        FocusedRowChange();
        //    }
        //    //gVStockProduct.FocusedRowChanged += gVStockProduct_FocusedRowChanged;   
        //}

        private void gVStockProduct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRowChange();
        }

        /// <summary>
        /// 仓库一变则库位信息全变
        /// </summary>
        private void SetCellLayoutEmpty(bool IsHouseOut)
        {
            if (IsHouseOut == true)
            {
                for (int i = 0; i < gVStockProduct.RowCount; i++)
                {
                    gVStockProduct.SetRowCellValue(i, "LayoutCodeOut", string.Empty);
                    gVStockProduct.SetRowCellValue(i, "LayoutNameOut", string.Empty);
                    gVStockProduct.SetRowCellValue(i, "Num", string.Empty);
                }
            }
            else
            {
                for (int i = 0; i < gVStockProduct.RowCount; i++)
                {
                    gVStockProduct.SetRowCellValue(i, "LayoutCodeIn", string.Empty);
                    gVStockProduct.SetRowCellValue(i, "LayoutNameIn", string.Empty);                   
                }
            }
        }

        /// <summary>
        /// 调出仓库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboStockOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gVStockProduct.RowCount > 0)
            {
                SetCellLayoutEmpty(true);
                repositoryItemComboBoxLayoutOut.Items.Clear();

                if (comboStockOut.Tag != null)
                {
                    int index = ((DevExpress.XtraEditors.ComboBoxEdit)(sender)).SelectedIndex;
                    string[] codeList = comboStockOut.Tag.ToString().Split(',');
                    //repositoryItemComboBoxLayoutOut.Items.Clear();
                    if (index != -1 && codeList.Length > index)
                    {
                        string houseCode = codeList[index];

                        //出库库位
                        string tag = string.Empty;
                        DataRow dRow = gVStockProduct.GetFocusedDataRow();
                        if (dRow != null)
                        {
                            string skuOuterID = dRow["SkuOuterID"].ToString();
                            List<StockHouseProduct> stockLayoutList = StockHouseService.GetStockHouseProduct(i => i.SkuOuterID == skuOuterID && i.HouseCode == houseCode);
                            foreach (StockHouseProduct vStockHouse in stockLayoutList)
                            {
                                if (string.IsNullOrEmpty(vStockHouse.LayoutName))
                                    continue;
                                repositoryItemComboBoxLayoutOut.Items.Add(vStockHouse.LayoutName);
                                tag += vStockHouse.LayoutCode + ",";
                            }
                            repositoryItemComboBoxLayoutOut.Tag = tag.Trim(',');
                        }
                    }
                    else
                    {
                        SetCellLayoutEmpty(true);
                        //gVStockProduct.SetFocusedRowCellValue("LayoutCodeOut", string.Empty);
                        //gVStockProduct.SetFocusedRowCellValue("LayoutNameOut", string.Empty);
                        //gVStockProduct.SetFocusedRowCellValue("Num", string.Empty);
                    }
                }
            }
        }
        /// <summary>
        /// 调入仓库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboStockIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboStockIn.Text == comboStockOut.Text)
            {
                XtraMessageBox.Show("调出仓库和调入仓库不能相同", Constants.SYSTEM_PROMPT);
                return;
            }

            SetCellLayoutEmpty(false);
            repositoryItemComboBoxLayoutIn.Items.Clear();

            if (comboStockIn.Tag != null)
            {
                int index = ((DevExpress.XtraEditors.ComboBoxEdit)(sender)).SelectedIndex;
                string[] codeList = comboStockIn.Tag.ToString().Split(',');
                if (index != -1 && codeList.Length > index)
                {
                    string houseCode = codeList[index];

                    //加载入库库位
                    string tag = string.Empty;

                    List<StockLayout> stockLayoutList = StockLayoutService.GetStockLayout(i => i.StockHouseCode == houseCode);
                    foreach (StockLayout layout in stockLayoutList)
                    {
                        if (string.IsNullOrEmpty(layout.LayoutName))
                            continue;
                        repositoryItemComboBoxLayoutIn.Items.Add(layout.LayoutName);
                        tag += layout.StockLayoutCode + ",";
                    }
                    repositoryItemComboBoxLayoutIn.Tag = tag.Trim(',');

                }
                else
                {
                    SetCellLayoutEmpty(false);
                    //gVStockProduct.SetFocusedRowCellValue("LayoutCodeIn", string.Empty);
                    //gVStockProduct.SetFocusedRowCellValue("LayoutNameIn", string.Empty);
                }
            }
        }       

        private void comboStockIn_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind.ToString() == "Plus")
            {
                Alading.Forms.Stock.SettingUp.ModifyHouse modifyHouse = new Alading.Forms.Stock.SettingUp.ModifyHouse();
                modifyHouse.ShowDialog();
                //刷新入库仓库
                comboStockIn.Properties.Items.Clear();
                List<StockHouse> stockHouseList = StockHouseService.GetAllStockHouse();
                string houseCode = string.Empty;
                foreach (StockHouse stockHouse in stockHouseList)
                {
                    comboStockIn.Properties.Items.Add(stockHouse.HouseName);
                    houseCode += stockHouse.StockHouseCode + ",";
                }
                comboStockIn.Tag = houseCode.Trim(',');
            }            
        }
    }
}