using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Business;
using Alading.Core.Enum;
using Alading.Taobao;
using DevExpress.Utils;
using System.Linq;
using DevExpress.XtraTreeList.Nodes;

namespace Alading.Forms.Stock.SettingUp
{
    [ToolboxItem(false)]
    public partial class StockUnit : DevExpress.XtraEditors.XtraUserControl
    {
        public StockUnit()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载所有计量单位组及计量单位组所属计量单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StockUnit_Load(object sender, EventArgs e)
        {
            AppendNodes();
            treeListUnitGroup.OptionsBehavior.Editable = false;
            gridViewUnitList.BestFitColumns();
        }


        # region 计量单位组编辑
        /// <summary>
        /// 新增计量单位组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemGroupNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GroupUnitForm newGroupUnitForm = new GroupUnitForm();
            newGroupUnitForm.ShowDialog();
            if (newGroupUnitForm.DialogResult == DialogResult.OK)
            {
                treeListUnitGroup.Nodes.Clear();
                AppendNodes();
            }
        }

        /// <summary>
        /// 计量单位组信息修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemGroupEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeListUnitGroup.Nodes.Count > 0)
            {
                string groupUnitCode = textEditCode.Text;
                List<Alading.Entity.StockUnitGroup> stockUnitGroupList = StockUnitGroupService.GetStockUnitGroup(c => c.StockUnitGroupCode == groupUnitCode);
                if (stockUnitGroupList.Count > 0)
                {
                    ModifyGroupUnitForm modifyGroupUnitForm = new ModifyGroupUnitForm(stockUnitGroupList.First());
                    modifyGroupUnitForm.ShowDialog();
                    if (modifyGroupUnitForm.DialogResult == DialogResult.OK)
                    {
                        TreeListNode node = treeListUnitGroup.FocusedNode;
                        node.SetValue(treeListColumn1, stockUnitGroupList.First().StockUnitGroupName);
                        textEditGroupName.Text = stockUnitGroupList.First().StockUnitGroupName;
                    }
                }
            }
        }

        /// <summary>
        /// 计量单位组删除，暂时不要
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemGroupDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeListUnitGroup.Nodes.Count > 0)
            {
                if (XtraMessageBox.Show(string.Format("是否删除计量单位组\n{0}", textEditGroupName.Text), Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    string stockGroupUnitCode = textEditCode.Text;
                    List<Alading.Entity.StockUnitGroup> stockUnitGroupList = StockUnitGroupService.GetStockUnitGroup(c => c.StockUnitGroupCode == stockGroupUnitCode);
                    if (stockUnitGroupList!=null && stockUnitGroupList.Count > 0)
                    {
                        List<Alading.Entity.StockUnit> listStockUnit = StockUnitService.GetStockUnit(stockGroupUnitCode);
                        if (listStockUnit != null)
                        {
                            if (listStockUnit.Count > 1)
                            {
                                XtraMessageBox.Show("该计量单位组包含除基本单位外的其他计量单位，不能删除！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                if (StockUnitGroupService.RemoveStockUnitGroup(stockGroupUnitCode) == ReturnType.Success &&
                                    StockUnitService.RemoveStockUnit(listStockUnit[0].StockUnitCode) == ReturnType.Success)
                                {
                                    XtraMessageBox.Show("删除成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemGroupRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            treeListUnitGroup.Nodes.Clear();
            AppendNodes();
        }
        #endregion

        # region 计量单位编辑
        /// <summary>
        /// 新增计量单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemUnitNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string stockUnitGroupCode = textEditCode.Text;
            List<Alading.Entity.StockUnitGroup> stockUnitGroupList = StockUnitGroupService.GetStockUnitGroup(c => c.StockUnitGroupCode == stockUnitGroupCode);
            if (stockUnitGroupList != null && stockUnitGroupList.Count > 0)
            {
                NewUnitForm newUnitForm = new NewUnitForm(stockUnitGroupList.First().StockUnitGroupCode, textEditBaseUnit.Text);
                newUnitForm.ShowDialog();
                if (newUnitForm.DialogResult == DialogResult.OK)
                {
                    UnitRefresh();
                }
            }
        }

        /// <summary>
        /// 计量单位信息修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemUnitEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow stockUnitRow = gridViewUnitList.GetDataRow(gridViewUnitList.FocusedRowHandle);
            if (stockUnitRow != null)
            {
                if (stockUnitRow["是否基本单位"].ToString() == "否")
                {
                    ModifyUnitForm modifyUnitForm = new ModifyUnitForm(stockUnitRow["stockUnitCode"].ToString(), textEditGroupName.Text,textEditBaseUnit.Text);
                    modifyUnitForm.ShowDialog();
                    //刷新
                    if (!string.IsNullOrEmpty(textEditCode.Text))
                    {
                        string stockCode = textEditCode.Text;
                        List<Alading.Entity.StockUnitGroup> stockUnitGroupList = StockUnitGroupService.GetStockUnitGroup(c=>c.StockUnitGroupCode==stockCode);
                        if (stockUnitGroupList != null && stockUnitGroupList.Count > 0)
                        {
                            ShowStockUnitInGridView(stockUnitGroupList.First());
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show("该计量单位为基本单位，不能被修改！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);                   
                }
            }
        }

        /// <summary>
        /// 计量单位删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemUnitDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
            waitForm.Show();
            try
            {
                DataRow stockUnitRow = gridViewUnitList.GetDataRow(gridViewUnitList.FocusedRowHandle);
                if (stockUnitRow != null && stockUnitRow["是否基本单位"].ToString() == "否")
                {
                    if (stockUnitRow["计量单位名称"] != null && stockUnitRow["计量单位名称"].ToString() != string.Empty)
                    {
                        waitForm.Close();
                        if (XtraMessageBox.Show(string.Format("是否删除计量单位\n{0}", stockUnitRow["计量单位名称"].ToString()), Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
                            waitForm.Show();
                            if (StockUnitService.RemoveStockUnit(stockUnitRow["stockUnitCode"].ToString()) == ReturnType.Success)
                            {
                                waitForm.Close();
                                XtraMessageBox.Show("计量单位删除成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                gridViewUnitList.DeleteRow(gridViewUnitList.FocusedRowHandle);
                                return;
                            }
                            else
                            {
                                waitForm.Close();
                                XtraMessageBox.Show("计量单位删除失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    waitForm.Close();
                    XtraMessageBox.Show("该计量单位为基本单位，不能被删除！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                waitForm.Close();
                XtraMessageBox.Show(ex.ToString(), Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemUnitRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UnitRefresh();
        }

        void UnitRefresh()
        {
            WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
            waitForm.Show();
            try
            {
                if (!string.IsNullOrEmpty(textEditGroupName.Text))
                {
                    string stockGroupUnitCode = textEditCode.Text;
                    List<Alading.Entity.StockUnitGroup> stockUnitGroupList = StockUnitGroupService.GetStockUnitGroup(c => c.StockUnitGroupCode == stockGroupUnitCode);
                    if (stockUnitGroupList.Count > 0)
                    {
                        ShowStockUnitInGridView(stockUnitGroupList.First());
                    }
                }
                waitForm.Close();
                gridViewUnitList.BestFitColumns();
            }
            catch (Exception ex)
            {
                waitForm.Close();
                XtraMessageBox.Show(ex.ToString(), Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        # endregion 

        /// <summary>
        /// treelist中焦点行改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListUnitGroup_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (treeListUnitGroup.FocusedNode != null)
            {
                string stockGroupUnitCode = treeListUnitGroup.FocusedNode.Tag.ToString();
                List<Alading.Entity.StockUnitGroup> stockUnitGroupList = StockUnitGroupService.GetStockUnitGroup(c => c.StockUnitGroupCode == stockGroupUnitCode);
                if (stockUnitGroupList.Count > 0)
                {
                    Alading.Entity.StockUnitGroup stockUnitGroup = stockUnitGroupList.First();
                    textEditGroupName.Text = stockUnitGroup.StockUnitGroupName;
                    textEditBaseUnit.Text = stockUnitGroup.BaseUnit;
                    textEditCode.Text = stockUnitGroup.StockUnitGroupCode;
                    ShowStockUnitInGridView(stockUnitGroup);
                }
            }
        }

        # region 公共函数
        /// <summary>
        /// 追加节点
        /// </summary>
        private void AppendNodes()
        {
            List<Alading.Entity.StockUnitGroup> listStockUnitGroup = StockUnitGroupService.GetAllStockUnitGroup();
            if (listStockUnitGroup.Count != 0)
            {
                foreach (Alading.Entity.StockUnitGroup stockUnitGroup in listStockUnitGroup)
                {
                    treeListUnitGroup.AppendNode(new object[] { stockUnitGroup.StockUnitGroupName }, null, stockUnitGroup.StockUnitGroupCode);
                }
            }
        }
        
        /// <summary>
        /// 展示计量单位组中的所有计量单位
        /// </summary>
        /// <param name="stockGroupUnitName">计量单位组</param>
        private void ShowStockUnitInGridView(Alading.Entity.StockUnitGroup stockUnitGroup)
        {
            List<Alading.Entity.StockUnit> listStockUnit = StockUnitService.GetStockUnit(stockUnitGroup.StockUnitGroupCode);
            DataTable stockUnitTable = new DataTable();
            stockUnitTable.Columns.Add("计量单位名称");
            stockUnitTable.Columns.Add("是否基本单位");
            stockUnitTable.Columns.Add("单位来源");
            stockUnitTable.Columns.Add("换算关系");
            stockUnitTable.Columns.Add("stockUnitCode");
            stockUnitTable.Rows.Clear();

            if (listStockUnit != null)
            {
                foreach (Alading.Entity.StockUnit stockUnit in listStockUnit)
                {
                    DataRow stockUnitRow = stockUnitTable.NewRow();
                    stockUnitRow["计量单位名称"] = stockUnit.StockUnitName;
                    if (stockUnit.IsBaseUnit)
                    {
                        stockUnitRow["是否基本单位"] = "是";
                    }
                    else
                    {
                        stockUnitRow["是否基本单位"] = "否";
                    }
                    stockUnitRow["单位来源"] = stockUnit.StockUnitSource;
                    if (!stockUnit.IsBaseUnit)
                    {
                        stockUnitRow["换算关系"] = string.Format("1 {0}= {1} {2}", stockUnit.StockUnitName, stockUnit.Conversion, stockUnitGroup.BaseUnit);// ((float)stockUnit.Conversion).ToString() + " " + stockUnitGroup.BaseUnit + " = " + "1 " + stockUnit.StockUnitName;
                    }
                    stockUnitRow["stockUnitCode"] = stockUnit.StockUnitCode;
                    stockUnitTable.Rows.Add(stockUnitRow);
                }
            }

            gridControlUnit.DataSource = stockUnitTable;
        }
        #endregion
    }
}
