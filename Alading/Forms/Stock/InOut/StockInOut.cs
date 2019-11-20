using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Entity;
using Alading.Business;
using Alading.Core.Enum;
using System.Linq;
using Alading.Utils;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Alading.Forms.Stock.InOut;
using DevExpress.Utils;
using Alading.Taobao;

namespace Alading.Forms.Stock
{
    public partial class StockInOut : DevExpress.XtraEditors.XtraForm
    {
        public StockInOut()
        {
            InitializeComponent();
            allVidpList = StockInOutService.GetAllView_InOutDetailProducts().GroupBy(c => c.InOutCode).ToList();           
            AddInOutTableColumns();
            AddProductTableColumns();
            gridCtrlStockInOut.DataSource = inOutTable;
            gridCtrlStockItem.DataSource = productTable;
            currentVidpList = allVidpList;
            GetIntOutTableDataSource(GetDisplayGroup());
            GetAllIndex();
            SetPageBtnEnable();
            AddTreeListNodes();
            foreach (TreeListNode node in tcInOutType.Nodes)
            {
                node.Expanded=true;
            }
            RowClick();
        }

        /// <summary>
        /// 添加树节点
        /// </summary>
        private void AddTreeListNodes()
        {
            TreeListNode allInputNode = tcInOutType.AppendNode(new object[] { "所有入库单" }, null, null);

            TreeListNode node1 = tcInOutType.AppendNode(new object[] { "期初入库" }, allInputNode, null);
            AddSonNode(node1);

            TreeListNode node2 = tcInOutType.AppendNode(new object[] { "生产入库" }, allInputNode, null);
            AddSonNode(node2);

            TreeListNode node3 = tcInOutType.AppendNode(new object[] { "采购入库" }, allInputNode, null);
            AddSonNode(node3);

            TreeListNode node4 = tcInOutType.AppendNode(new object[] { "调拨入库" }, allInputNode, null);
            AddSonNode(node4);

            TreeListNode node5 = tcInOutType.AppendNode(new object[] { "退货入库" }, allInputNode, null);
            AddSonNode(node5);

            TreeListNode node6 = tcInOutType.AppendNode(new object[] { "报溢入库" }, allInputNode, null);
            AddSonNode(node6);

            TreeListNode node7 = tcInOutType.AppendNode(new object[] { "其它入库" }, allInputNode, null);
            AddSonNode(node7);

            TreeListNode allOutPutNode = tcInOutType.AppendNode(new object[] { "所有出库单" }, null, null);

            TreeListNode onode1 = tcInOutType.AppendNode(new object[] { "调拨出库" }, allOutPutNode, null);
            AddSonNode(onode1);

            TreeListNode onode2 = tcInOutType.AppendNode(new object[] { "销售出库" }, allOutPutNode, null);
            AddSonNode(onode2);

            TreeListNode onode3 = tcInOutType.AppendNode(new object[] { "退货出库" }, allOutPutNode, null);
            AddSonNode(onode3);

            TreeListNode onode4 = tcInOutType.AppendNode(new object[] { "报损出库" }, allOutPutNode, null);
            AddSonNode(onode4);

            TreeListNode onode5 = tcInOutType.AppendNode(new object[] { "其它出库" }, allOutPutNode, null);
            AddSonNode(onode5);
        }

        /// <summary>
        /// 加载子节点
        /// </summary>
        /// <param name="fNode"></param>
        private void AddSonNode(TreeListNode fNode)
        {
            TreeListNode timeNode = tcInOutType.AppendNode(new object[] { "按日期查询" }, fNode, null);

            TreeListNode node1 = tcInOutType.AppendNode(new object[] { "一周内" }, timeNode, null);

            TreeListNode node2 = tcInOutType.AppendNode(new object[] { "一月内" }, timeNode, null);

            TreeListNode node3 = tcInOutType.AppendNode(new object[] { "三月内" }, timeNode, null);

            TreeListNode node4 = tcInOutType.AppendNode(new object[] { "半年内" }, timeNode, null);

            TreeListNode node5 = tcInOutType.AppendNode(new object[] { "一年内" }, timeNode, null);

        }

        /// <summary>
        /// 返回当前需要显示的数据列表
        /// </summary>
        /// <returns></returns>
        List<IGrouping<string, View_InOutDetailProduct>> GetDisplayGroup()
        {
            return currentVidpList.Skip((currentIndex - 1) * countPerPage).Take(countPerPage).ToList();
        }

        /// <summary>
        /// 所有的View_InOutDetailProduct列表
        /// </summary>
        List<IGrouping<string, View_InOutDetailProduct>> allVidpList = new List<IGrouping<string, View_InOutDetailProduct>>();

        /// <summary>
        /// 当前选择状态下的View_InOutDetailProduct列表
        /// </summary>
        List<IGrouping<string, View_InOutDetailProduct>> currentVidpList = new List<IGrouping<string, View_InOutDetailProduct>>();

        /// <summary>
        /// 当前出入库单列表的DataSource
        /// </summary>
        DataTable inOutTable = new DataTable();

        /// <summary>
        /// 当前商品表的DataSource
        /// </summary>
        DataTable productTable = new DataTable();

        /// <summary>
        /// 当前页码
        /// </summary>
        int currentIndex = 1;

        /// <summary>
        /// 每页显示
        /// </summary>
        int countPerPage = 30;

        /// <summary>
        /// 所有页码
        /// </summary>
        int allIndex = 1;

        /// <summary>
        /// 给intOutTable添加列
        /// </summary>
        void AddInOutTableColumns()
        {
            inOutTable.Columns.Add(gcAmountTax.FieldName,typeof(string));
            inOutTable.Columns.Add(gcDiscountFee.FieldName,typeof(string));
            inOutTable.Columns.Add(gcDueFee.FieldName,typeof(string));
            inOutTable.Columns.Add(gcFreightCode.FieldName,typeof(string));
            inOutTable.Columns.Add(gcFreightCompany.FieldName, typeof(string));
            inOutTable.Columns.Add(gcIncomeTime.FieldName,typeof(string));
            inOutTable.Columns.Add(gcInOutCode.FieldName, typeof(string));
            inOutTable.Columns.Add(gcInOutStatus.FieldName,typeof(string));
            inOutTable.Columns.Add(gcInOutTime.FieldName, typeof(string));
            inOutTable.Columns.Add(gcInOutType.FieldName, typeof(string));
            inOutTable.Columns.Add(gcOperatorName.FieldName, typeof(string));
            inOutTable.Columns.Add(gcPayTerm.FieldName, typeof(string));
            inOutTable.Columns.Add(gcPayType.FieldName, typeof(string));
        }

        /// <summary>
        /// 给当前要显示的列表添加行
        /// </summary>
        void GetIntOutTableDataSource(List<IGrouping<string, View_InOutDetailProduct>> vidpList)
        {
            WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
            waitForm.Show();
            try
            {
                inOutTable.Rows.Clear();
                foreach (IGrouping<string, View_InOutDetailProduct> g in vidpList)
                {
                    View_InOutDetailProduct vidp = g.First();
                    DataRow row = inOutTable.NewRow();
                    row[gcAmountTax.FieldName] = vidp.AmountTax;
                    row[gcDiscountFee.FieldName] = vidp.DiscountFee;
                    row[gcDueFee.FieldName] = vidp.DueFee;
                    row[gcFreightCode.FieldName] = vidp.FreightCode;
                    row[gcFreightCompany.FieldName] = vidp.FreightCompany;
                    row[gcIncomeTime.FieldName] = vidp.IncomeTime;
                    row[gcInOutCode.FieldName] = vidp.InOutCode;
                    row[gcInOutStatus.FieldName] = UIHelper.GetEnumData("InOutStatus", vidp.InOutStatus);
                    row[gcInOutTime.FieldName] = vidp.InOutTime;
                    row[gcInOutType.FieldName] = UIHelper.GetEnumData("InOutType", vidp.InOutType);
                    row[gcOperatorName.FieldName] = vidp.OperatorName;
                    row[gcPayTerm.FieldName] = vidp.PayTerm;
                    row[gcPayType.FieldName] = UIHelper.GetEnumData("PayType", vidp.PayType);
                    inOutTable.Rows.Add(row);
                }
                gridViewStockInOut.BestFitColumns();
                gridViewStockItem.BestFitColumns();
                waitForm.Close();
            }
            catch (Exception ex)
            {
                waitForm.Close();
                XtraMessageBox.Show(ex.ToString(), Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }       

        /// <summary>
        /// 给productTable添加列
        /// </summary>
        void AddProductTableColumns()
        {
            productTable.Columns.Add(pgcCatName.FieldName, typeof(string));
            productTable.Columns.Add(pgcHouseName.FieldName, typeof(string));
            productTable.Columns.Add(pgcLayoutName.FieldName, typeof(string));
            productTable.Columns.Add(pgcName.FieldName, typeof(string));
            productTable.Columns.Add(pgcProductSkuOuterID.FieldName, typeof(string));
            productTable.Columns.Add(pgcQuantity.FieldName, typeof(string));
            productTable.Columns.Add(pgcSkuProps_Str.FieldName, typeof(string));
            productTable.Columns.Add(pgcStockCatName.FieldName, typeof(string));
        }

        /// <summary>
        /// 给当前要显示的明细列表添加行
        /// </summary>
        /// <param name="vidpList"></param>
        void GetProductTableDataSource(IGrouping<string, View_InOutDetailProduct> vidpList)
        {
            productTable.Rows.Clear();
            foreach (View_InOutDetailProduct vidp in vidpList)
            {
                DataRow row = productTable.NewRow();
                row[pgcCatName.FieldName] = vidp.CatName;
                row[pgcHouseName.FieldName] = vidp.HouseName;
                row[pgcLayoutName.FieldName] = vidp.LayoutName;
                row[pgcQuantity.FieldName] = vidp.Quantity;
                row[pgcName.FieldName] = vidp.Name;
                row[pgcProductSkuOuterID.FieldName] = vidp.ProductSkuOuterId;
                row[pgcSkuProps_Str.FieldName] = vidp.SkuProps_Str;
                row[pgcStockCatName.FieldName] = vidp.StockCatName;
                productTable.Rows.Add(row);
            }
            List<HistoryStockDetail> historyList = StockDetailService.GetHistoryDetail(vidpList.Key);
            if (historyList != null)
            {
                foreach (HistoryStockDetail historyDetail in historyList)
                {
                    //DataRow row = productTable.NewRow();
                    //row[pgcCatName.FieldName] = historyDetail.CatName;
                    //row[pgcHouseName.FieldName] = historyDetail.HouseName;
                    //row[pgcLayoutName.FieldName] = historyDetail.LayoutName;
                    //row[pgcQuantity.FieldName] = historyDetail.Quantity;
                    //row[pgcName.FieldName] = historyDetail.Name;
                    //row[pgcProductSkuOuterID.FieldName] = historyDetail.ProductSkuOuterId;
                    //row[pgcSkuProps_Str.FieldName] = historyDetail.SkuProps_Str;
                    //row[pgcStockCatName.FieldName] = historyDetail.StockCatName;
                    //productTable.Rows.Add(row);
                }
            }
            gridViewStockInOut.BestFitColumns();
            gridViewStockItem.BestFitColumns();
        }

        /// <summary>
        /// 获取当前列表的最大页码
        /// </summary>
        void GetAllIndex()
        {
            if ((float)(currentVidpList.Count / (float)countPerPage) > (int)(currentVidpList.Count / countPerPage))
            {
                allIndex = (int)(currentVidpList.Count / countPerPage) + 1;
            }
            else
            {
                allIndex = currentVidpList.Count / countPerPage;
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewStockInOut.RowCount > 0)
            {
                SaveFileDialog savefd = new SaveFileDialog();
                savefd.OverwritePrompt = true; // 文件存在是是否提示覆盖
                savefd.RestoreDirectory = true;
                savefd.FileName = "新建Excel文本"; // 默认文件名
                savefd.DefaultExt = "xlsx"; // 扩展名
                savefd.Filter = "XLS files (*.xlsx)|*.xlsx";
                //savefd.InitialDirectory = "c:\\";  // 初始路径
                if (DialogResult.OK == savefd.ShowDialog())
                {
                    string filepath = savefd.FileName;
                    gridViewStockInOut.ExportToXlsx(filepath);
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewStockInOut.RowCount > 0)
            {
                DialogResult result = XtraMessageBox.Show("您确定删除该记录？删除后将无法恢复！", "确定删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
                waitForm.Show();
                try
                {                                       
                    if (result == DialogResult.OK)
                    {
                        productTable.Rows.Clear();
                        DataRow row = gridViewStockInOut.GetFocusedDataRow();
                        StockInOutService.RemoveInOutAndDetails(row[gcInOutCode.FieldName].ToString());
                        allVidpList.Remove(allVidpList.FirstOrDefault(c => c.Key == row[gcInOutCode.FieldName].ToString()));
                        currentVidpList.Remove(currentVidpList.FirstOrDefault(c => c.Key == row[gcInOutCode.FieldName].ToString()));
                        GetIntOutTableDataSource(GetDisplayGroup());
                        SetPageBtnEnable();
                        RowClick();
                    }
                    waitForm.Close();
                }
                catch (Exception ex)
                {
                    waitForm.Close();
                    XtraMessageBox.Show(ex.ToString(), Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// 根据当前的总页数给repositoryItemComboBox1添加页码选项
        /// </summary>
        void GetComboIndex()
        {
            for (int i = 1; i <= allIndex; i++)
            {
                repositoryItemComboBox1.Items.Add("第" + i + "页");
            }
            barEditItem1.EditValue = "第" + currentIndex + "页"+" ";
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnFirstIndex_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            currentIndex = 1;
            GetIntOutTableDataSource(GetDisplayGroup());
            SetPageBtnEnable();
            barEditItem1.EditValue = "第" + currentIndex + "页"+" ";
            RowClick();
        }

        /// <summary>
        /// 根据当前显示页码及总页码设置页码按钮的Enable
        /// </summary>
        void SetPageBtnEnable()
        {
            if (currentIndex == 1 && currentIndex < allIndex)
            {
                bbtnFrontIndex.Enabled = false;
                bbtnFirstIndex.Enabled = false;
                bbtnNextIndex.Enabled = true;
                bbtnLastIndex.Enabled = true;
            }
            else if (currentIndex > 1 && currentIndex < allIndex)
            {
                bbtnFrontIndex.Enabled = true;
                bbtnFirstIndex.Enabled = true;
                bbtnNextIndex.Enabled = true;
                bbtnLastIndex.Enabled = true;
            }
            else if (currentIndex == allIndex && allIndex>1)
            {
                bbtnFrontIndex.Enabled = true;
                bbtnFirstIndex.Enabled = true;
                bbtnNextIndex.Enabled = false;
                bbtnLastIndex.Enabled = false;
            }
            else if (currentIndex == allIndex && allIndex == 1)
            {
                bbtnFrontIndex.Enabled = false;
                bbtnFirstIndex.Enabled = false;
                bbtnNextIndex.Enabled = false;
                bbtnLastIndex.Enabled = false;
            }
        }

        /// <summary>
        /// 前一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnFrontIndex_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            currentIndex--;
            GetIntOutTableDataSource(GetDisplayGroup());
            SetPageBtnEnable();
            barEditItem1.EditValue = "第" + currentIndex + "页" + " ";
            RowClick();
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnNextIndex_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            currentIndex++;
            GetIntOutTableDataSource(GetDisplayGroup());
            SetPageBtnEnable();
            barEditItem1.EditValue = "第" + currentIndex + "页" + " ";
            RowClick();
        }

        /// <summary>
        /// 尾页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnLastIndex_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            currentIndex = allIndex;
            GetIntOutTableDataSource(GetDisplayGroup());
            SetPageBtnEnable();
            barEditItem1.EditValue = "第" + currentIndex + "页" + " ";
            RowClick();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewStockItem_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

        }

        /// <summary>
        /// 点击显示入库商品详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewStockInOut_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            DataRow row = gridViewStockInOut.GetDataRow(e.RowHandle);
            string stockIntOutCode = row[gcInOutCode.FieldName].ToString();
            GetProductTableDataSource(currentVidpList.FirstOrDefault(c => c.Key == stockIntOutCode));
        }

        /// <summary>
        /// 用于没触发事件但需要执行触发事件时的方法
        /// </summary>
        private void RowClick()
        {
            productTable.Rows.Clear();
            DataRow row = gridViewStockInOut.GetFocusedDataRow();
            if (row != null)
            {                
                string stockIntOutCode = row[gcInOutCode.FieldName].ToString();
                GetProductTableDataSource(currentVidpList.FirstOrDefault(c => c.Key == stockIntOutCode));
            }
            gridViewStockInOut.BestFitColumns();
            gridViewStockItem.BestFitColumns();
        }

        /// <summary>
        /// 根据不同的筛选类型返回不同的list
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        void GetCurrentVidpList(TreeListNode node)
        {
            string nodeText=node.GetDisplayText(0);
            if (nodeText == "所有入库单")
            {
                currentVidpList = allVidpList.Where(c => c.First().InOutType > 0 && c.First().InOutType<=(int)InOutType.OtherIn).ToList();
            }
            else if (nodeText == "所有出库单")
            {
                currentVidpList = allVidpList.Where(c => c.First().InOutType > (int)InOutType.OtherIn && c.First().InOutType <= (int)InOutType.OtherOut).ToList();
            }
            else if (nodeText == "期初入库")
            {
                currentVidpList = allVidpList.Where(c => c.First().InOutType==(int)InOutType.InitInput).ToList();
            }
            else if (nodeText == "生产入库")
            {
                currentVidpList = allVidpList.Where(c => c.First().InOutType == (int)InOutType.ProduceIn).ToList();
            }
            else if (nodeText == "采购入库")
            {
                currentVidpList = allVidpList.Where(c => c.First().InOutType == (int)InOutType.ProduceIn).ToList();
            }
            else if (nodeText == "调拨入库")
            {
                currentVidpList = allVidpList.Where(c => c.First().InOutType == (int)InOutType.AllocateIn).ToList();
            }
            else if (nodeText == "退货入库")
            {
                currentVidpList = allVidpList.Where(c => c.First().InOutType == (int)InOutType.SelledReturnIn).ToList();
            }
            else if (nodeText == "其它入库")
            {
                currentVidpList = allVidpList.Where(c => c.First().InOutType == (int)InOutType.OtherIn).ToList();
            }
            else if (nodeText == "报溢入库")
            {
                currentVidpList = allVidpList.Where(c => c.First().InOutType == (int)InOutType.ProfitIn).ToList();
            }
            else if (nodeText == "调拨出库")
            {
                currentVidpList = allVidpList.Where(c => c.First().InOutType == (int)InOutType.AllocateOut).ToList();
            }
            else if (nodeText == "销售出库")
            {
                currentVidpList = allVidpList.Where(c => c.First().InOutType == (int)InOutType.SaleOut).ToList();
            }
            else if (nodeText == "退货出库")
            {
                currentVidpList = allVidpList.Where(c => c.First().InOutStatus == (int)InOutType.PurchaseReturnOut).ToList();
            }
            else if (nodeText == "报损出库")
            {
                currentVidpList = allVidpList.Where(c => c.First().InOutType == (int)InOutType.LossOut).ToList();
            }
            else if (nodeText == "其它出库")
            {
                currentVidpList = allVidpList.Where(c => c.First().InOutType == (int)InOutType.OtherOut).ToList();
            }
            else if (nodeText == "按日期查询")
            {

            }
            else
            {
                GetCurrentVidpList(node.ParentNode.ParentNode);
                if (nodeText == "一周内")
                {
                    currentVidpList = currentVidpList.Where(c => c.First().InOutTime.Date> DateTime.Now.AddDays(-7)).ToList();
                }
                else if (nodeText == "一月内")
                {
                    currentVidpList = currentVidpList.Where(c => c.First().InOutTime.Date> DateTime.Now.AddDays(-30)).ToList();
                }
                else if (nodeText == "三月内")
                {
                    currentVidpList = currentVidpList.Where(c => c.First().InOutTime.Date> DateTime.Now.AddDays(-90)).ToList();
                }
                else if (nodeText == "半年内")
                {
                    currentVidpList = currentVidpList.Where(c => c.First().InOutTime.Date > DateTime.Now.AddDays(-180)).ToList();
                }
                else if (nodeText == "一年内")
                {
                    currentVidpList = currentVidpList.Where(c => c.First().InOutTime.Date > DateTime.Now.AddDays(-365)).ToList();
                }
            }
        }

        /// <summary>
        /// 点击按类别显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tcInOutType_MouseDown(object sender, MouseEventArgs e)
        {
            TreeListHitInfo hitInfo = tcInOutType.CalcHitInfo(new Point(e.X, e.Y));

            /*如果单击到单元格内*/
            if (hitInfo.HitInfoType == HitInfoType.Cell)
            {
                currentIndex = 1;
                GetCurrentVidpList(hitInfo.Node);
                GetIntOutTableDataSource(GetDisplayGroup());
                GetAllIndex();
                GetComboIndex();
                SetPageBtnEnable();
                RowClick();
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            currentIndex = 1;
            
            string value=textEditSearch.Text;
            //currentVidpList = allVidpList.Where(c => c.First().sear).ToList();
            //if (comboSearchType.SelectedIndex == 0)
            //{
            //    currentVidpList = currentVidpList.Where(c => c.First().OperatorName.Contains(value)).ToList();
            //}
            //else if (comboSearchType.SelectedIndex == 1)
            //{
            //    currentVidpList = currentVidpList.Where(c => c.First().FreightCompany.Contains(value)).ToList();
            //}
            //GetAllIndex();
            //GetIntOutTableDataSource(GetDisplayGroup());
            //SetPageBtnEnable();
        }

        /// <summary>
        /// 生产入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnProduceIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Alading.Forms.Stock.InOut.ProduceInForm produceIn = new ProduceInForm();
            produceIn.ShowDialog();
        }

        /// <summary>
        /// 采购进货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnPurchaseIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PurchaseIn purchaseIn = new PurchaseIn();
            purchaseIn.ShowDialog();
        }

        /// <summary>
        /// 销售退货入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnSelledReturnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SelledReturnIn sri = new SelledReturnIn();
            sri.ShowDialog();
        }

        /// <summary>
        /// 其他入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnOtherIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OtherIn oi = new OtherIn();
            oi.ShowDialog();
        }

        /// <summary>
        /// 采购退货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnPurchaseReturnOut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PurchaseReturnOut pro = new PurchaseReturnOut();
            pro.ShowDialog();
        }

        /// <summary>
        /// 其它出库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnOtherOut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OtherOut oo = new OtherOut();
            oo.ShowDialog();
        }

        /// <summary>
        /// 销售出库单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnSaleOut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProductSoldOut pso = new ProductSoldOut();
            pso.ShowDialog();
        }

        /// <summary>
        /// 转到第N页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        { 
            ComboBoxEdit combo=(ComboBoxEdit)sender;
            if (combo.SelectedIndex >= 0)
            {
                currentIndex = combo.SelectedIndex+1;
                GetIntOutTableDataSource(GetDisplayGroup());
                SetPageBtnEnable();
                //barEditItem1.EditValue = "第" + currentIndex + "页" + " ";
                RowClick();
            }
        }
    }
}