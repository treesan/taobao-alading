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
using Alading.Utils;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using System.Linq;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using Alading.Taobao;

namespace Alading.Forms.Stock.Control
{
    public partial class ProductSelect : DevExpress.XtraEditors.XtraUserControl
    {
        #region 构造函数,LOAD事件，全局变量及DataTable加载类

        /// <summary>
        /// 当前页码
        /// </summary>
        int currentIndex = 1;

        /// <summary>
        /// 每页显示
        /// </summary>
        int dataPerPage = 30;

        /// <summary>
        /// 全部页码
        /// </summary>
        int allIndex = 0;

        DataTable table = new DataTable();

        public List<View_StockItemProduct> vsipList = new List<View_StockItemProduct>();

        private RepositoryItemPopupContainerEdit repositoryItemPopupContainerEditSelect = new RepositoryItemPopupContainerEdit();

        public List<View_StockItemProduct> GetSelectedItems()
        {
            return vsipList;
        }

        /// <summary>
        /// 只展示此仓库的Product
        /// </summary>
        public string stockHouseCode;

        /// <summary>
        /// 注意，这是用于与外界进行数据交互的DataTable，必须在外部初始化后传入
        /// </summary>
        public DataTable dTable;

        /// <summary>
        /// 用来记录是否已经在已选商品列表中，如果已经选中了，就不再加载
        /// </summary>
        public List<string> skuOutIdList = new List<string>();

        /// <summary>
        /// 无参构造函数，用于放在其它控件上
        /// </summary>
        public ProductSelect()
        {
            InitializeComponent();
            AddTableColumns();
            gridCtrlStockItem.DataSource = this.table;
            gridViewSelected.BestFitColumns();
            gridViewStockItem.BestFitColumns();
        }

        /// <summary>
        /// LOAD事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductSelect_Load(object sender, EventArgs e)
        {
            this.tlStockCats.BeginUnboundLoad();
            List<StockCat> stockCatList = StockCatService.GetStockCat(c => c.ParentCid == "0");
            foreach (StockCat stockCat in stockCatList)
            {
                TreeListNode node = tlStockCats.AppendNode(new object[] { stockCat.StockCatName }, null, new TreeListNodeTag(stockCat.StockCid));
                //设置是否有子节点，有则会显示一个+号
                node.HasChildren = stockCat.IsParent;
            }
            tlStockCats.EndUnboundLoad();

            //已选商品列表
            gridViewSelected.BestFitColumns();
        }

        /// <summary>
        /// 带参构造函数，用于直接使用
        /// </summary>
        /// <param name="barBtnSelect"></param>
        /// <param name="dTable"></param>
        public ProductSelect(bool barBtnSelect, DataTable dTable, RepositoryItemPopupContainerEdit repositoryItemPopupContainerEditSelect)
        {
            InitializeComponent();
            this.dTable = dTable;
            if (barBtnSelect == true)
            {
                this.barBtnSelect.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            AddTableColumns();
            this.repositoryItemPopupContainerEditSelect = repositoryItemPopupContainerEditSelect;
            gridCtrlStockItem.DataSource = this.table;
            AddColumns();
            gridViewSelected.BestFitColumns();
            gridViewStockItem.BestFitColumns();
        }

        /// <summary>
        /// 内置table加列
        /// </summary>
        private void AddTableColumns()
        {
            this.table.Columns.Add("Select", typeof(bool));
            this.table.Columns.Add("OuterID", typeof(string));
            this.table.Columns.Add("CatName", typeof(string));
            this.table.Columns.Add("Cid", typeof(string));
            this.table.Columns.Add("Name", typeof(string));
            this.table.Columns.Add("Model", typeof(string));
            this.table.Columns.Add("StockCatName", typeof(string));
            this.table.Columns.Add("StockCid", typeof(string));
            this.table.Columns.Add("SaleProps", typeof(string));
            this.table.Columns.Add("Num", typeof(string));
            this.table.Columns.Add("Specification", typeof(string));
            this.table.Columns.Add("SkuOuterID", typeof(string));
            this.table.Columns.Add("SkuPrice", typeof(double));//零售价            
            this.table.Columns.Add("HouseName", typeof(string));//仓库名称
            this.table.Columns.Add("LayoutName", typeof(string));//库位名称
            this.table.Columns.Add("HouseCode", typeof(string));//仓库编号
            this.table.Columns.Add("LayoutCode", typeof(string));//库位编号
            this.table.Columns.Add("BarCode", typeof(string));//条形码
            this.table.Columns.Add(gcUnitName.FieldName, typeof(string));//计量单位
            this.table.Columns.Add(gcTaxCode.FieldName, typeof(string));//税率

            //用于展示所选商品的属性
            this.table.Columns.Add("Props", typeof(string));
            this.table.Columns.Add("InputPids", typeof(string));
            this.table.Columns.Add("InputStr", typeof(string));
            this.table.Columns.Add("Property_Alias", typeof(string));
        }

        /// <summary>
        /// 传进来的dTable加列
        /// </summary>
        public void AddColumns()
        {
            this.dTable.Columns.Add("Select", typeof(bool));
            this.dTable.Columns.Add("OuterID", typeof(string));
            this.dTable.Columns.Add("CatName", typeof(string));
            this.dTable.Columns.Add("Cid", typeof(string));
            this.dTable.Columns.Add("Name", typeof(string));
            this.dTable.Columns.Add("Model", typeof(string));
            this.dTable.Columns.Add("StockCatName", typeof(string));
            this.dTable.Columns.Add("StockCid", typeof(string));
            this.dTable.Columns.Add("SaleProps", typeof(string));
            this.dTable.Columns.Add("Num", typeof(string));
            this.dTable.Columns.Add("Specification", typeof(string));
            this.dTable.Columns.Add("SkuOuterID", typeof(string));
            this.dTable.Columns.Add("BarCodeSelect", typeof(string));//条形码
            this.dTable.Columns.Add("SkuPrice", typeof(double));//零售价
            this.dTable.Columns.Add("HouseName", typeof(string));//仓库名称
            this.dTable.Columns.Add("LayoutName", typeof(string));//库位名称
            this.dTable.Columns.Add("HouseCode", typeof(string));//仓库编号
            this.dTable.Columns.Add("LayoutCode", typeof(string));//库位编号
            this.dTable.Columns.Add("StockUnitName", typeof(string));//计量单位
            this.dTable.Columns.Add(gridColumnTaxCode.FieldName, typeof(string));//税率

            //用于展示所选商品的属性
            this.dTable.Columns.Add("Props", typeof(string));
            this.dTable.Columns.Add("InputPids", typeof(string));
            this.dTable.Columns.Add("InputStr", typeof(string));
            this.dTable.Columns.Add("Property_Alias", typeof(string));

            gridCtrlSelected.DataSource = dTable;
        }

        #endregion

        #region TreeList的事件,搜索按钮及根据tag.Cid或者name获取View_StockItemProduct的方法

        /// <summary>
        /// 在展开前加载子类目信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlStockCats_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
        {
            TreeListNode focusedNode = e.Node;
            tlStockCats.FocusedNode = focusedNode;
            //XtraMessageBox.Show(tlItemCat.IsUnboundMode.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            TreeListNodeTag tag = focusedNode.Tag as TreeListNodeTag;

            #region 获得当前节点的子节点
            if (!tag.HasExpanded)
            {
                tlStockCats.BeginUnboundLoad();
                List<StockCat> stockCatList = StockCatService.GetStockCat(i => i.ParentCid == tag.Cid.ToString());

                foreach (StockCat stockCat in stockCatList)
                {
                    TreeListNode node = tlStockCats.AppendNode(new object[] { stockCat.StockCatName }, focusedNode, new TreeListNodeTag(stockCat.StockCid));
                    node.HasChildren = stockCat.IsParent;
                }
                tlStockCats.EndUnboundLoad();
                tag.HasExpanded = true;
            }
            #endregion
        }

        /// <summary>
        /// 双击加载该类目下的商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlStockCats_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        /// <summary>
        /// 获取满足需求的商品列表
        /// </summary>
        /// <param name="value"></param>
        /// <param name="si"></param>
        void GetStockItem(string value, StockItemElement si)
        {
            WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
            waitForm.Show();
            try
            {
                table.Rows.Clear();
                List<string> skuOuterIDList = new List<string>();
                List<StockHouseProduct> houseProList = new List<StockHouseProduct>();//StockHouseService.GetSHProBySkuOuterID(skuOuterIDList);
                if (stockHouseCode == null)
                {
                    List<View_StockItemProduct> stockItemList;
                    if (si == StockItemElement.Name && !string.IsNullOrEmpty(value))
                    {
                        Func<View_StockItemProduct, bool> func =new Func<View_StockItemProduct,bool>(c => c.Name.Contains(value) || c.SkuOuterID.Contains(value));
                        stockItemList = View_StockItemProductService.GetView_StockItemProduct(func,currentIndex,dataPerPage,out allIndex);
                    }
                    else
                    {
                        stockItemList = View_StockItemProductService.GetView_StockItemProduct(value, currentIndex, dataPerPage, ref allIndex);
                    }
                    if (stockItemList == null)
                    {
                        waitForm.Close();
                        XtraMessageBox.Show("没有找到对应的商品！", Constants.SYSTEM_PROMPT);
                        return;
                    }
                    foreach (View_StockItemProduct stockItem in stockItemList)
                    {
                        DataRow row = table.NewRow();
                        StockHouseProduct houseProduct = houseProList.Find(c => c.SkuOuterID == stockItem.SkuOuterID);

                        /*读取选中的节点显示名称*/
                        row["Select"] = false;
                        row["CatName"] = stockItem.CatName;//淘宝类目
                        row["Cid"] = stockItem.Cid;//淘宝类目
                        row["StockCatName"] = stockItem.StockCatName;//库存类目名称
                        row["Name"] = stockItem.Name;//商品名称
                        row["Num"] = stockItem.SkuQuantity;//商品库存总数量
                        row["SaleProps"] = stockItem.SkuProps_Str;//销售属性
                        row["SkuOuterID"] = stockItem.SkuOuterID;//
                        row["Model"] = stockItem.Model;
                        row["Specification"] = stockItem.Specification;
                        row["StockUnitName"] = stockItem.StockUnitName;
                        row["OuterID"] = stockItem.OuterID;//
                        row[gcTaxCode.FieldName] = stockItem.Tax;
                        //用于展示所选商品的属性
                        row["StockCid"] = stockItem.StockCid;//
                        row["Props"] = stockItem.Props;//
                        row["InputPids"] = stockItem.InputPids;//
                        row["InputStr"] = stockItem.InputStr;//
                        row["Property_Alias"] = stockItem.Property_Alias;//

                        #region 入库需要信息
                        row["SkuPrice"] = stockItem.SkuPrice;//销售价
                        if (houseProduct != null)
                        {
                            row["HouseName"] = houseProduct.HouseName;//仓库名称
                            row["LayoutName"] = houseProduct.LayoutName;//库位名称
                            row["HouseCode"] = houseProduct.HouseCode;//仓库编号
                            row["LayoutCode"] = houseProduct.LayoutCode;//库位编号
                        }
                        #endregion

                        table.Rows.Add(row);
                        skuOuterIDList.Add(stockItem.SkuOuterID);
                    }
                }
                else
                {
                    if (stockHouseCode.Trim() != string.Empty)
                    {
                        List<View_StockProductHouse> stockItemList = View_StockItemProductService.GetView_StockProductHouse(value, stockHouseCode, currentIndex, dataPerPage, ref allIndex);
                        if (stockItemList == null)
                        {
                            return;
                        }
                        if (stockHouseCode != string.Empty)
                        {
                            foreach (View_StockProductHouse stockItem in stockItemList)
                            {
                                DataRow row = table.NewRow();
                                StockHouseProduct houseProduct = houseProList.Find(c => c.SkuOuterID == stockItem.SkuOuterID);

                                /*读取选中的节点显示名称*/
                                row["Select"] = false;
                                row["CatName"] = stockItem.CatName;//淘宝类目
                                row["StockCatName"] = stockItem.StockCatName;//库存类目名称
                                row["Cid"] = stockItem.Cid;//淘宝类目
                                row["Name"] = stockItem.Name;//商品名称
                                row["Num"] = stockItem.SkuQuantity;//商品库存总数量
                                row["SaleProps"] = stockItem.SkuProps_Str;//销售属性
                                row["SkuOuterID"] = stockItem.SkuOuterID;//
                                row["Model"] = stockItem.Model;
                                row["Specification"] = stockItem.Specification;
                                row["OuterID"] = stockItem.OuterID;//
                                row["StockUnitName"] = stockItem.StockUnitName;//计量单位
                                row[gcTaxCode.FieldName] = stockItem.Tax;
                                //用于展示所选商品的属性  
                                row["StockCid"] = stockItem.StockCid;//
                                row["Props"] = stockItem.Props;//
                                row["InputPids"] = stockItem.InputPids;//
                                row["InputStr"] = stockItem.InputStr;//
                                row["Property_Alias"] = stockItem.Property_Alias;//

                                #region 入库需要信息
                                row["SkuPrice"] = stockItem.SkuPrice;//销售价
                                if (houseProduct != null)
                                {
                                    row["HouseName"] = houseProduct.HouseName;//仓库名称
                                    row["LayoutName"] = houseProduct.LayoutName;//库位名称
                                    row["HouseCode"] = houseProduct.HouseCode;//仓库编号
                                    row["LayoutCode"] = houseProduct.LayoutCode;//库位编号
                                }
                                #endregion
                                table.Rows.Add(row);
                                skuOuterIDList.Add(stockItem.SkuOuterID);
                            }
                        }
                    }
                }

                gridCtrlStockItem.DataSource = table;
                gridViewSelected.BestFitColumns();
                gridViewStockItem.BestFitColumns();

                #region 判断4个分页按钮哪个的enable应为false*/
                if (allIndex <= 1)
                {
                    /*若页码总数为1，则所有分页按钮都为false*/
                    bbtnFirstIndex.Enabled = false;
                    bbtnNextIndex.Enabled = false;
                    bbtnPriviousIndex.Enabled = false;
                    bbtnLastIndex.Enabled = false;
                }
                else if (currentIndex == 1)
                {
                    /*若页码总数不为1但当前页码为1*/
                    bbtnFirstIndex.Enabled = false;
                    bbtnNextIndex.Enabled = true;
                    bbtnPriviousIndex.Enabled = false;
                    bbtnLastIndex.Enabled = true;
                }
                else if (currentIndex < allIndex)
                {
                    /*若页码总数不为1且当前页码不等于1且小于页码总数*/
                    bbtnFirstIndex.Enabled = true;
                    bbtnNextIndex.Enabled = true;
                    bbtnPriviousIndex.Enabled = true;
                    bbtnLastIndex.Enabled = true;
                }
                else if (currentIndex == allIndex)
                {
                    bbtnFirstIndex.Enabled = true;
                    bbtnNextIndex.Enabled = false;
                    bbtnPriviousIndex.Enabled = true;
                    bbtnLastIndex.Enabled = false;
                }
                #endregion

                /*显示当前页码,末尾加个空格避免触发事件*/
                bbtnComboxChangeTo.EditValue = "第" + currentIndex.ToString() + "页" + " ";
                waitForm.Close();
            }
            catch (Exception ex)
            {
                waitForm.Close();
                XtraMessageBox.Show(ex.ToString(), Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barEditMruSearch.EditValue != null)
            {
                currentIndex = 1;
                string value = barEditMruSearch.EditValue.ToString();
                GetStockItem(value, StockItemElement.Name);
                repositoryItemComboChangeTo.Items.Clear();
                for (int i = 1; i <= allIndex; i++)
                {
                    string text = "第" + i.ToString() + "页";
                    repositoryItemComboChangeTo.Items.Add(text);
                }
            }
        }

        #endregion

        #region gridCtrl事件及展示属性相关方法

        /// <summary>
        /// 单击展示商品属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewStockItem_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            ShowProps(gridViewStockItem);
        }

        /// <summary>
        /// 展示商品属性的方法
        /// </summary>
        /// <param name="gv"></param>
        void ShowProps(GridView gv)
        {
            /*如果显示的是属性panel才去加载属性*/
            if (panelContainer1.ActiveChild.Name == dockPanel1.Name)
            {
                DataRow row = gv.GetFocusedDataRow();
                if (row != null && row["OuterID"] != null && row["SkuOuterID"] != null)
                {
                    List<View_StockItemProduct> tempVsipList = View_StockItemProductService.GetView_StockItemProduct(v => v.OuterID == row["OuterID"].ToString() && v.SkuOuterID == row["SkuOuterID"].ToString());
                    if (tempVsipList.Count > 0)
                    {
                        View_StockItemProduct vsip = tempVsipList.First();
                        View_ShopItem item = new View_ShopItem();
                        item.props = vsip.Props;
                        item.input_pids = vsip.InputPids;
                        item.input_str = vsip.InputStr;
                        item.property_alias = vsip.PropsAlias;
                        item.cid = vsip.Cid;
                        UIHelper.LoadItemPropValue(item, categoryRowKeyProps, categoryRowSaleProps, categoryNotKeyProps, categoryRowInputProps);
                    }
                }
            }
        }

        /// <summary>
        /// 单击展示商品属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewSelected_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            ShowProps(gridViewSelected);
        }


        #endregion

        /// <summary>
        /// 双击商品列表将该条记录加到已选商品列表中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void gridCtrlStockItem_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    GridHitInfo hitInfo=gridViewStockItem.CalcHitInfo(new Point(e.X,e.Y));
        //    if(hitInfo.Column!=null && hitInfo.InRow)
        //    {
        //        DataRow fRow = gridViewStockItem.GetDataRow(hitInfo.RowHandle);
        //        if(fRow!=null)
        //        {
        //            if (gridCtrlSelected.DataSource != null)
        //            {
        //                dTable = gridCtrlSelected.DataSource as DataTable;
        //            }
        //            DataRow row = dTable.NewRow();
        //            row["SaleProps"] = fRow["SaleProps"];
        //            row["Name"] = fRow["Name"];
        //            row["SkuOuterID"] = fRow["SkuOuterID"];
        //            row["Model"] = fRow["Model"];
        //            row["Specification"] = fRow["Specification"];
        //            row["Num"] = fRow["Num"];
        //            row["StockCat"] = fRow["StockCat"];
        //            row["ItemCat"] = fRow["ItemCat"];
        //            row["OutId"] = fRow["OutId"];
        //            dTable.Rows.Add(row);
        //            gridCtrlSelected.DataSource = dTable;
        //        }
        //    }

        //}

        #region 删除，选择，及CellValueChanging事件

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int i = 0;
            while (i < gridViewSelected.RowCount)
            {
                DataRow row = dTable.Rows[i];
                if (Convert.ToBoolean(row["Select"]))
                {
                    skuOutIdList.Remove(row["SkuOuterID"].ToString());
                    vsipList.Remove(vsipList.FirstOrDefault(c => c.SkuOuterID == row["SkuOuterID"].ToString()));
                    dTable.Rows.Remove(row);
                }
                else
                {
                    i++;
                }
            }
            gridCtrlSelected.DataSource = dTable;
            gridViewSelected.BestFitColumns();
            gridViewStockItem.BestFitColumns();
        }

        /// <summary>
        /// 将选中的商品加入已选商品列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnSelectStockItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridCtrlSelected.DataSource != null)
            {
                dTable = gridCtrlSelected.DataSource as DataTable;
            }
            for (int i = 0; i < gridViewStockItem.RowCount; i++)
            {
                DataRow fRow = gridViewStockItem.GetDataRow(i);
                if (Convert.ToBoolean(fRow["Select"]) && !skuOutIdList.Contains(fRow["SkuOuterID"].ToString()))
                {
                    DataRow row = dTable.NewRow();
                    row["Select"] = false;
                    row["SaleProps"] = fRow["SaleProps"];
                    row["Name"] = fRow["Name"];
                    row["SkuOuterID"] = fRow["SkuOuterID"];
                    row["Model"] = fRow["Model"];
                    row["Specification"] = fRow["Specification"];
                    row["Num"] = fRow["Num"];
                    row["StockCatName"] = fRow["StockCatName"];
                    row["CatName"] = fRow["CatName"];
                    row["Cid"] = fRow["Cid"];//淘宝类目
                    row["OuterID"] = fRow["OuterID"];
                    row[gridColumnTaxCode.FieldName] = fRow[gcTaxCode.FieldName];
                    //用于展示所选商品的属性  
                    row["StockCid"] = fRow["StockCid"];
                    row["Props"] = fRow["Props"];//
                    row["InputPids"] = fRow["InputPids"];//
                    row["InputStr"] = fRow["InputStr"];//
                    row["Property_Alias"] = fRow["Property_Alias"];//

                    row["StockUnitName"] = fRow["StockUnitName"];//计量单位
                    #region 入库需要信息
                    row["SkuPrice"] = fRow["SkuPrice"];//销售价
                    row["HouseName"] = fRow["HouseName"];//仓库名称
                    row["LayoutName"] = fRow["LayoutName"];//库位名称
                    row["HouseCode"] = fRow["HouseCode"];//仓库编号
                    row["LayoutCode"] = fRow["LayoutCode"];//库位编号
                    #endregion

                    dTable.Rows.Add(row);
                    skuOutIdList.Add(row["SkuOuterID"].ToString());
                    //View_StockItemProduct temp = View_StockItemProductService.GetView_StockItemProductBySkuOuterId(row["SkuOuterID"].ToString());
                    //if (temp != null)
                    //{
                    //    vsipList.Add(temp);
                    //}
                }
            }
            gridCtrlSelected.DataSource = dTable;
            int rowCount = gridViewStockItem.RowCount;
            gridViewStockItem.BeginUpdate();
            for (int i = 0; i < rowCount; i++)
            {
                DataRow row = gridViewStockItem.GetDataRow(i);
                row[gcSelect.FieldName] = false;
            }
            //选中商品是否勾选
            if (repositoryItemCheckAllSelect.Tag == null || repositoryItemCheckAllSelect.Tag.ToString() == false.ToString())
            {
                for (int i = 0; i < gridViewSelected.RowCount; i++)
                {
                    DataRow row = gridViewSelected.GetDataRow(i);
                    row[Select.FieldName] = false;
                }
            }
            else
            {
                for (int i = 0; i < gridViewSelected.RowCount; i++)
                {
                    DataRow row = gridViewSelected.GetDataRow(i);
                    row[Select.FieldName] = true;
                }
            }

            gridViewStockItem.EndUpdate();
            gridViewSelected.BestFitColumns();
            gridViewStockItem.BestFitColumns();
        }

        /// <summary>
        /// 选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewStockItem_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = gridViewStockItem.GetDataRow(e.RowHandle);
            if (e.Column == gcSelect)
            {
                gridViewStockItem.BeginDataUpdate();
                row["Select"] = e.Value;
                gridViewStockItem.EndDataUpdate();
            }
        }

        /// <summary>
        /// 选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewSelected_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = gridViewSelected.GetDataRow(e.RowHandle);
            if (e.Column == Select)
            {
                gridViewSelected.BeginDataUpdate();
                row["Select"] = e.Value;
                gridViewSelected.EndDataUpdate();
            }
        }

        #endregion

        private void gridCtrlStockItem_Click(object sender, EventArgs e)
        {

        }

        private void barBtnSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (repositoryItemPopupContainerEditSelect != null && repositoryItemPopupContainerEditSelect.PopupControl != null && repositoryItemPopupContainerEditSelect.PopupControl.OwnerEdit != null)
            {
                repositoryItemPopupContainerEditSelect.PopupControl.OwnerEdit.ClosePopup();
            }
        }

        private void panelContainer1_ActiveChildChanged(object sender, DevExpress.XtraBars.Docking.DockPanelEventArgs e)
        {

        }

        private void bbtnDAllClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowCount = gridViewSelected.RowCount;
            gridViewSelected.BeginUpdate();
            for (int i = 0; i < rowCount; i++)
            {
                DataRow row = gridViewSelected.GetDataRow(i);
                row[Select.FieldName] = false;
            }
            gridViewSelected.EndUpdate();
        }

        private void bbtnAllSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowCount = gridViewStockItem.RowCount;
            gridViewStockItem.BeginUpdate();
            for (int i = 0; i < rowCount; i++)
            {
                DataRow row = gridViewStockItem.GetDataRow(i);
                row[gcSelect.FieldName] = true;
            }
            gridViewStockItem.EndUpdate();
        }

        private void bbtnAllClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowCount = gridViewStockItem.RowCount;
            gridViewStockItem.BeginUpdate();
            for (int i = 0; i < rowCount; i++)
            {
                DataRow row = gridViewStockItem.GetDataRow(i);
                row[gcSelect.FieldName] = false;
            }
            gridViewStockItem.EndUpdate();
        }

        private void bbtnDAllSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowCount = gridViewSelected.RowCount;
            gridViewSelected.BeginUpdate();
            for (int i = 0; i < rowCount; i++)
            {
                DataRow row = gridViewSelected.GetDataRow(i);
                row[Select.FieldName] = true;
            }
            gridViewSelected.EndUpdate();
        }

        /// <summary>
        /// 单击显示类目下的商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlStockCats_MouseClick(object sender, MouseEventArgs e)
        {
            gridCtrlStockItem.DataSource = null;
            bbtnComboxChangeTo.EditValue = string.Empty;
            TreeListHitInfo hitInfo = tlStockCats.CalcHitInfo(new Point(e.X, e.Y));
            /*如果单击到单元格内*/
            if (hitInfo.HitInfoType == HitInfoType.Cell)
            {
                if (!hitInfo.Node.HasChildren)
                {
                    currentIndex = 1;
                    TreeListNodeTag tag = hitInfo.Node.Tag as TreeListNodeTag;
                    GetStockItem(tag.Cid, StockItemElement.StockCatCid);
                    repositoryItemComboChangeTo.Items.Clear();
                    for (int i = 1; i <= allIndex; i++)
                    {
                        string text = "第" + i.ToString() + "页";
                        repositoryItemComboChangeTo.Items.Add(text);
                    }
                }
            }
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnFirstIndex_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            currentIndex = 1;
            TreeListNodeTag tag = tlStockCats.FocusedNode.Tag as TreeListNodeTag;
            GetStockItem(tag.Cid, StockItemElement.StockCatCid);
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnPriviousIndex_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            currentIndex--;
            TreeListNodeTag tag = tlStockCats.FocusedNode.Tag as TreeListNodeTag;
            GetStockItem(tag.Cid, StockItemElement.StockCatCid);
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnNextIndex_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            currentIndex++;
            TreeListNodeTag tag = tlStockCats.FocusedNode.Tag as TreeListNodeTag;
            GetStockItem(tag.Cid, StockItemElement.StockCatCid);
        }

        /// <summary>
        /// 尾页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnLastIndex_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            currentIndex = allIndex;
            TreeListNodeTag tag = tlStockCats.FocusedNode.Tag as TreeListNodeTag;
            GetStockItem(tag.Cid, StockItemElement.StockCatCid);
        }

        /// <summary>
        /// 转到
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemComboChangeTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit combo = (ComboBoxEdit)sender;
            if (combo.SelectedIndex >= 0)
            {
                currentIndex = combo.SelectedIndex + 1;
                //combo.Text = combo.Properties.Items[combo.SelectedIndex].ToString();            
                TreeListNodeTag tag = tlStockCats.FocusedNode.Tag as TreeListNodeTag;
                GetStockItem(tag.Cid, StockItemElement.StockCatCid);
            }
        }

        /// <summary>
        /// 全选和全不选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (((DevExpress.XtraEditors.CheckEdit)(sender)).Checked == true)
            {
                //变为选中
                for (int index = 0; index < gridViewStockItem.RowCount; index++)
                {
                    DataRow dataRow = gridViewStockItem.GetDataRow(index);
                    gridViewStockItem.SetRowCellValue(index, gcSelect, true);
                }
            }
            else
            {
                //变为全不选中
                for (int index = 0; index < gridViewStockItem.RowCount; index++)
                {
                    DataRow dataRow = gridViewStockItem.GetDataRow(index);
                    gridViewStockItem.SetRowCellValue(index, gcSelect, false);
                }
            }
        }

        private void repositoryItemCheckAllSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (((DevExpress.XtraEditors.CheckEdit)(sender)).Checked == true)
            {
                //变为选中
                for (int index = 0; index < gridViewSelected.RowCount; index++)
                {
                    DataRow dataRow = gridViewSelected.GetDataRow(index);
                    gridViewSelected.SetRowCellValue(index, Select, true);
                    repositoryItemCheckAllSelect.Tag = true;
                }
            }
            else
            {
                //变为全不选中
                for (int index = 0; index < gridViewSelected.RowCount; index++)
                {
                    DataRow dataRow = gridViewSelected.GetDataRow(index);
                    gridViewSelected.SetRowCellValue(index, Select, false);
                    repositoryItemCheckAllSelect.Tag = false;
                }
            }
        }

    }

    enum StockItemElement
    {
        Name = 0,
        StockCatCid = 1
    }
}
