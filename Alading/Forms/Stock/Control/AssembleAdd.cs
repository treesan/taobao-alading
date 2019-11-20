using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Utils;
using Alading.Entity;
using Alading.Business;
using System.Linq;
using Alading.Taobao;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraEditors.Repository;
using System.Collections;
using DevExpress.XtraEditors.Controls;
using Alading.Core.Code128;
using Alading.Forms.Stock.SettingUp;
using DevExpress.XtraTreeList.Columns;

namespace Alading.Forms.Stock.Control
{
    [ToolboxItem(true)]
    public partial class AssembleAdd : DevExpress.XtraEditors.XtraUserControl
    {
        private DataTable dTable = new DataTable();
        public bool IsModify = false;

        string pidVid = string.Empty;

        /// <summary>
        /// key为销售属性名称,value为(key为pid:vid,value为红色套餐)
        /// </summary>
        SortedList<string, SortedList<string, string>> salePropsList = new SortedList<string, SortedList<string, string>>();
        /// <summary>
        /// key为pid:vid,value为价格
        /// </summary>
        SortedList<string, string> priceList = new SortedList<string, string>();

        public AssembleAdd()
        {
            InitializeComponent();
        }

        public void LoadAseemble(AssembleItem assembleItem)
        {
            IsModify = true;
            memoDesc.Text = assembleItem.AssembleDesc;
            pceItemCat.Text = assembleItem.CatName;
            pceItemCat.Tag = assembleItem.Cid;
            assembleItem.Model = textModel.Text = assembleItem.Model;
            assembleItem.Name = textName.Text = assembleItem.Name;
            textOuterID.Text = assembleItem.OuterID;
            textPrice.Text = assembleItem.Price.ToString();
            textSimpleName.Text = assembleItem.SimpleName;
            textSpecification.Text = assembleItem.Specification;
            comboTax.Text = assembleItem.TaxName;
            popupUnit.Text = assembleItem.UnitName;
            popupUnit.Tag = assembleItem.UnitCode;

            barEditItemProps.EditValue = assembleItem.SkuProps_Str;

        }


        #region 选择
        /// <summary>
        /// 给dataTable加载列
        /// </summary>
        private void AddColumns(DataTable table)
        {
            table.Columns.Add("Key");
            table.Columns.Add("Name");
            table.Columns.Add("StockCatName");
            table.Columns.Add("CatName");
            table.Columns.Add("Cid");
            table.Columns.Add("SkuOuterID");
            table.Columns.Add("Specification");
            table.Columns.Add("Model");
            table.Columns.Add("Count");//数量
            table.Columns.Add("SaleProps");
            table.Columns.Add("SkuPrice");
            table.Columns.Add("StockUnitName");//计量单位       
            table.Columns.Add("IsSelected", typeof(bool));

            //用于展示所选商品的属性
            table.Columns.Add("Props", typeof(string));
            table.Columns.Add("InputPids", typeof(string));
            table.Columns.Add("InputStr", typeof(string));
            table.Columns.Add("Property_Alias", typeof(string));
        }

        /// <summary>
        /// 获取被选中的行
        /// </summary>
        /// <returns></returns>
        //private List<int> GetRowChecked()
        //{
        //    List<int> rowChecked = new List<int>();
        //    for (int i = 0; i < gVStockProduct.RowCount; i++)
        //    {
        //        if (gVStockProduct.GetRowCellValue(i, "IsSelected") != null)
        //        {
        //            bool IsCheck = (bool)gVStockProduct.GetRowCellValue(i, "IsSelected");
        //            if (IsCheck == true)
        //                rowChecked.Add(i);
        //        }
        //    }
        //    return rowChecked;
        //}

        /// <summary>
        /// 选择商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DataTable table = new DataTable();
                ProductSelected ps = new ProductSelected(table, null);
                ps.ShowDialog();

                List<string> skuOuterIDList = new List<string>();
                DataView dView = new DataView(dTable);
                dView.RowFilter = "Key='" + pidVid + "'";//
                DataTable dataTable = dView.ToTable();

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row["SkuOuterID"] != null && !skuOuterIDList.Contains(row["SkuOuterID"].ToString()))
                        {
                            skuOuterIDList.Add(row["SkuOuterID"].ToString());
                        }
                    }
                }

                //商品新增           
                foreach (DataRow row in table.Rows)
                {
                    //防止出现同一个商品
                    if (row["SkuOuterID"] == null || skuOuterIDList.Contains(row["SkuOuterID"].ToString()))
                        continue;

                    DataRow dRow = dTable.NewRow();

                    dRow["Key"] = pidVid;//用于筛选DataRow

                    dRow["CatName"] = row["CatName"];
                    dRow["Cid"] = row["Cid"];
                    dRow["Name"] = row["Name"];
                    dRow["SkuOuterID"] = row["SkuOuterID"];
                    dRow["Specification"] = row["Specification"];
                    dRow["Model"] = row["Model"];
                    dRow["StockCatName"] = row["StockCatName"];
                    dRow["CatName"] = row["CatName"];
                    dRow["Count"] = 1;
                    dRow["SkuPrice"] = row["SkuPrice"];
                    dRow["StockUnitName"] = row["StockUnitName"];
                    dRow["SaleProps"] = row["SaleProps"];
                    dRow["IsSelected"] = false;

                    //用于展示所选商品的属性
                    dRow["Props"] = row["Props"];
                    dRow["InputPids"] = row["InputPids"];
                    dRow["InputStr"] = row["InputStr"];
                    dRow["Property_Alias"] = row["Property_Alias"];
                    dTable.Rows.Add(dRow);
                }
                SetDataSource();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
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
                //List<int> rowChecked = GetRowChecked();
                List<DataRow> rowList = new List<DataRow>();
                foreach (DataRow row in dTable.Rows)
                {
                    if (row["IsSelected"].ToString() == true.ToString())
                    {
                        rowList.Add(row);
                    }
                }

                if (rowList != null && rowList.Count > 0)
                {
                    if (XtraMessageBox.Show("是否删除所有选中项", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        //List<DataRow> rowList = new List<DataRow>();
                        //foreach (DataRow row in rowList)
                        //{
                        //    rowList.Add(gVStockProduct.GetDataRow(row));
                        //}
                        foreach (DataRow dataRow in rowList)
                        {
                            dTable.Rows.Remove(dataRow);
                        }
                    }
                }

                gVStockProduct.SetFocusedRowCellValue("IsSelected", false);
                SetDataSource();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }

        }
        #endregion

        private void AssembleAdd_Load(object sender, EventArgs e)
        {
            try
            {
                AddColumns(this.dTable);
                //加载淘宝类目
                UIHelper.LoadItemCat(tlItemCat);
                //加载计量单位
                InitUnit();
                //加载税率
                InitTax();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }

        #region 公共方法
        /// <summary>
        /// 加载计量单位
        /// </summary>
        private void InitUnit()
        {
            try
            {
                IEnumerable<View_GroupUnit> vGroupUnit = StockUnitService.GetAllView_GroupUnit();
                var q = from i in vGroupUnit
                        select i.StockUnitGroupCode;
                List<string> groupCodeList = q.Distinct().ToList();
                treeUnit.BeginUnboundLoad();
                foreach (string groupCode in groupCodeList)
                {
                    IEnumerable<View_GroupUnit> viewList = vGroupUnit.Where(i => i.StockUnitGroupCode == groupCode);
                    TreeListNode pNode = treeUnit.AppendNode(new object[] { viewList.FirstOrDefault().StockUnitGroupName }, null, new TreeListNodeTag(viewList.FirstOrDefault().StockUnitGroupCode));
                    foreach (View_GroupUnit view in viewList)
                    {
                        string name;
                        if (!view.IsBaseUnit)
                        {
                            name = string.Format("{0}(={1}{2})", view.StockUnitName, view.Conversion, view.BaseUnit);
                        }
                        else
                        {
                            name = string.Format("{0}", view.StockUnitName);
                        }
                        TreeListNode cNode = treeUnit.AppendNode(new object[] { name }, pNode, new TreeListNodeTag(view.StockUnitCode));
                    }
                }
                treeUnit.EndUnboundLoad();
                treeUnit.ExpandAll();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }

        /// <summary>
        /// 加载税率
        /// </summary>
        private void InitTax()
        {
            comboTax.Properties.Items.Clear();
            List<Tax> taxList = StockUnitService.GetAllTax();
            if (taxList == null)
                return;
            string code = string.Empty;
            foreach (Tax tax in taxList)
            {
                comboTax.Properties.Items.Add(tax.TaxName);
                code += tax.TaxCode + ",";
            }
            comboTax.Tag = code.Trim(',');
        }

        /// <summary>
        /// 加载属性
        /// </summary>
        /// <param name="cid"></param>
        public void LoadItemPropValue(bool IsItemProps, string cid)
        {
            try
            {
                //加载组合商品属性
                if (IsItemProps == false)
                {
                    categoryRowSaleProps.ChildRows.Clear();
                    categoryRowKeyProps.ChildRows.Clear();
                    categoryRowNotKeyProps.ChildRows.Clear();
                    categoryRowStockProps.ChildRows.Clear();
                }
                else
                { //加载商品属性
                    categoryRowItemSaleProps.ChildRows.Clear();
                    categoryRowItemKeyProps.ChildRows.Clear();
                    categoryRowItemNotKeyProps.ChildRows.Clear();
                    categoryRowItemStockProps.ChildRows.Clear();
                }

                List<View_ItemPropValue> tmpPropValueList = ItemPropValueService.GetView_ItemPropValueList(cid, "-1", "-1");
                List<IGrouping<string, View_ItemPropValue>> propValueGroup = tmpPropValueList.Where(p => p.parent_pid == "0" && p.parent_vid == "0").GroupBy(i => i.pid).ToList();
                foreach (IGrouping<string, View_ItemPropValue> g in propValueGroup)
                {
                    View_ItemPropValue ipv = g.FirstOrDefault(c => c.is_parent == true);
                    if (ipv == null)
                    {
                        ipv = g.First();
                    }
                    EditorRow row = new EditorRow();
                    row.Properties.Caption = ipv.prop_name;
                    if (ipv.must)
                    {
                        row.Properties.ImageIndex = 0;
                    }

                    //tag中存储EditorRowTag pid cid vid
                    EditorRowTag tag = new EditorRowTag();
                    tag.IsInputProp = ipv.is_input_prop;
                    tag.Pid = ipv.pid;
                    tag.Cid = ipv.cid;
                    tag.Vid = ipv.vid;
                    tag.ChildTemplate = ipv.child_template;
                    tag.Is_Allow_Alias = ipv.is_allow_alias;
                    tag.IsMust = ipv.must;

                    //通过判断is_muti,is_must,is_input....来设置相应的控件，同时通过is_sale等设置CategoryRow
                    if (ipv.multi)
                    {
                        RepositoryItemCheckedComboBoxEdit ccmb = new RepositoryItemCheckedComboBoxEdit();
                        //row.Properties.RowEdit = ccmb;
                        foreach (View_ItemPropValue value in g)
                        {
                            //这里设置的vid为value，所以赋值的时候也需要这个vid才能正常显示
                            ccmb.Items.Add(value.vid, value.name);
                        }

                        //销售属性选中的时候引发的事件，销售属性一定是多选
                        if (ipv.is_sale_prop)
                        {
                            ccmb.EditValueChanged += new EventHandler(ccmb_EditValueChanged);
                        }

                        if (IsItemProps == false && ipv.is_sale_prop && ipv.is_allow_alias)
                        {
                            RepositoryItemPopupContainerEdit popEdit = new RepositoryItemPopupContainerEdit();
                            PopupContainerControl popUp = new PopupContainerControl();
                            popUp.Size = new Size(250, 300);
                            TreeList tree = new TreeList();
                            //((System.ComponentModel.ISupportInitialize)(tree)).BeginInit();
                            tree.Dock = DockStyle.Fill;
                            tree.OptionsView.ShowCheckBoxes = true;
                            TreeListColumn column = new TreeListColumn();
                            TreeListColumn columnAlias = new TreeListColumn();
                            tree.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] { column, columnAlias });
                            tree.Columns[0].Caption = ipv.prop_name;
                            tree.Columns[0].FieldName = ipv.prop_name;
                            tree.Columns[0].OptionsColumn.AllowEdit = false;
                            tree.Columns[0].VisibleIndex = 0;
                            //tree.Columns[0].OptionsColumn.AllowMoveToCustomizationForm = false;
                            //tree.Columns[0].OptionsColumn.ShowInCustomizationForm = false;
                            tree.Columns[1].Caption = "自定义";
                            tree.Columns[1].FieldName = "自定义";
                            tree.Columns[1].VisibleIndex = 1;

                            //tree.Columns[1].OptionsColumn.AllowMoveToCustomizationForm = false;
                            //tree.Columns[1].OptionsColumn.ShowInCustomizationForm = false;


                            foreach (View_ItemPropValue value in g)
                            {
                                //这里设置的vid为value，所以赋值的时候也需要这个vid才能正常显示
                                TreeListNode node = tree.AppendNode(new object[] { value.name }, -1, value.vid);
                                //ccmb.Items.Add(value.vid, value.name);
                            }                            

                            tree.Text = ipv.prop_name;
                            tree.Tag = ipv.pid;
                            popEdit.Closed += new ClosedEventHandler(popEdit_Closed);
                            popUp.Controls.Add(tree);
                            popEdit.PopupControl = popUp;
                            row.Properties.RowEdit = popEdit;
                        }
                        else
                        {
                            row.Properties.RowEdit = ccmb;
                        }
                    }
                    else
                    {
                        RepositoryItemComboBox cmb = new RepositoryItemComboBox();
                        row.Properties.RowEdit = cmb;
                        cmb.Name = ipv.pid.ToString();

                        /*如果该属性不是必须的,则其第0项是空值*/
                        Hashtable table = new Hashtable();
                        int index = 0;
                        if (!ipv.must)
                        {
                            table.Add(index, string.Empty);
                            cmb.Items.Add(string.Empty);
                            index++;
                        }
                        foreach (View_ItemPropValue value in g)
                        {
                            table.Add(index, value.vid);
                            cmb.Items.Add(value.name);
                            index++;
                        }

                        cmb.Tag = table;

                        if (cmb.Items.Count > 0)
                        {
                            cmb.TextEditStyle = TextEditStyles.DisableTextEditor;
                        }                        

                        //说明有下级
                        if (ipv.is_parent || ipv.is_key_prop)
                        {
                            //控件的tag中存储hashtable，保存当前列表中的vid
                            cmb.SelectedIndexChanged += new EventHandler(cmbParent_SelectedIndexChanged);
                            tag.IsParent = true;                          
                        }
                    }
                    /*tag赋值*/
                    row.Tag = tag;

                    if (IsItemProps == false)
                    {
                        if (ipv.is_key_prop)
                        {
                            categoryRowKeyProps.ChildRows.Add(row);
                        }
                        else if (ipv.is_sale_prop)
                        {
                            categoryRowSaleProps.ChildRows.Add(row);
                        }
                        else
                        {
                            categoryRowNotKeyProps.ChildRows.Add(row);
                        }
                    }
                    else
                    {
                        if (ipv.is_key_prop)
                        {
                            categoryRowItemKeyProps.ChildRows.Add(row);
                        }
                        else if (ipv.is_sale_prop)
                        {
                            categoryRowItemSaleProps.ChildRows.Add(row);
                        }
                        else
                        {
                            categoryRowItemNotKeyProps.ChildRows.Add(row);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }


        void cmbParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit cbe = sender as ComboBoxEdit;
            RepositoryItemComboBox cmb = cbe.Properties as RepositoryItemComboBox;

            //由于SelectedIndexChanged阻止了设置值，为了防止值不显示
            vGridCtrl.FocusedRow.Properties.Value = cbe.Text;

            //如果值不等于自定义，则加载数据
            EditorRow row = new EditorRow();
            EditorRowTag tag = vGridCtrl.FocusedRow.Tag as EditorRowTag;
            string cid = tag.Cid;
            string pid = tag.Pid;
            //row.Properties.Caption = tag.ChildTemplate;
            if (tag.IsMust)
            {
                row.Properties.ImageIndex = 0;
            }

            //删除不需要的row                              
            vGridCtrl.FocusedRow.ChildRows.Clear();
            BaseRow delRow = vGridCtrl.FocusedRow.ParentRow.ChildRows.GetRowByFieldName(vGridCtrl.FocusedRow.Properties.Caption + "自定义");
            if (delRow != null)
            {
                vGridCtrl.FocusedRow.ParentRow.ChildRows.Remove(delRow);
            }

            if (cbe.Text != "自定义")
            {
                Hashtable table = cmb.Tag as Hashtable;
                string vid = table[cbe.SelectedIndex].ToString();
                //View_ItemPropValue prop = ItemPropValueService.GetView_ItemPropValueList(cid, pid, vid).FirstOrDefault();

                //获得下级的所有值
                List<View_ItemPropValue> propValueList = ItemPropValueService.GetView_ItemPropValueList(cid, pid, vid).ToList();

                if (propValueList.Count > 0)
                {
                    View_ItemPropValue ipv = propValueList.First();
                    row.Properties.Caption = ipv.prop_name;
                    EditorRowTag fTag = new EditorRowTag();
                    fTag.Pid = ipv.pid;
                    fTag.Cid = cid;
                    fTag.Vid = ipv.vid;
                    fTag.ChildTemplate = ipv.child_template;
                    fTag.IsMust = ipv.must;
                    row.Tag = fTag;
                    if (ipv.multi)
                    {
                        RepositoryItemCheckedComboBoxEdit childCmb = new RepositoryItemCheckedComboBoxEdit();
                        foreach (View_ItemPropValue value in propValueList)
                        {
                            childCmb.Items.Add(value.vid, value.name);
                        }
                        if (childCmb.Items.Count > 0)
                        {
                            childCmb.TextEditStyle = TextEditStyles.DisableTextEditor;
                        }
                        row.Properties.RowEdit = childCmb;

                        //销售属性选中的时候引发的事件，销售属性一定是多选
                        if (ipv.is_sale_prop)
                        {
                            childCmb.EditValueChanged += new EventHandler(ccmb_EditValueChanged);
                        }
                    }
                    else
                    {
                        RepositoryItemComboBox childCmb = new RepositoryItemComboBox();
                        childCmb.Name = ipv.pid.ToString();
                        Hashtable childTable = new Hashtable();
                        int index = 0;
                        foreach (View_ItemPropValue value in propValueList)
                        {
                            childTable.Add(index, value.vid);
                            childCmb.Items.Add(value.name);
                            index++;
                        }

                        if (childCmb.Items.Count > 0)
                        {
                            childCmb.TextEditStyle = TextEditStyles.DisableTextEditor;
                        }
                        childCmb.Tag = childTable;


                        //若items里面包含“自定义”选项，则继续监听其SelectedIndexChanged事件
                        if (childCmb.Items.Contains("自定义") || ipv.is_key_prop)
                        {
                            childCmb.SelectedIndexChanged += new EventHandler(cmbParent_SelectedIndexChanged);
                        }
                        row.Properties.RowEdit = childCmb;
                    }

                    //添加row  
                    vGridCtrl.FocusedRow.ChildRows.Add(row);
                }
            }
            else if (cbe.Text == "自定义")
            {
                //如果是自定义，则在当前焦点行的父行里添加一子行(目前暂时作为其子项添加)
                string caption = vGridCtrl.FocusedRow.Properties.Caption + "自定义";
                row.Properties.Caption = caption;
                row.Properties.FieldName = caption;
                vGridCtrl.FocusedRow.ChildRows.Clear();
                vGridCtrl.FocusedRow.ChildRows.Add(row);/**/
            }
        }

        void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit cbe = sender as ComboBoxEdit;
            RepositoryItemComboBox cmb = cbe.Properties as RepositoryItemComboBox;

            //由于SelectedIndexChanged阻止了设置值，为了防止值不显示
            vGridCtrl.FocusedRow.Properties.Value = cbe.Text;

            //如果值不等于自定义，则加载数据
            EditorRow row = new EditorRow();
            EditorRowTag tag = vGridCtrl.FocusedRow.Tag as EditorRowTag;
            string cid = tag.Cid;
            string pid = tag.Pid;
            row.Properties.Caption = tag.ChildTemplate;
            if (tag.IsMust)
            {
                row.Properties.ImageIndex = 0;
            }

            //删除不需要的row                              
            vGridCtrl.FocusedRow.ChildRows.Clear();
            BaseRow delRow = vGridCtrl.FocusedRow.ParentRow.ChildRows.GetRowByFieldName(vGridCtrl.FocusedRow.Properties.Caption + "自定义");
            if (delRow != null)
            {
                vGridCtrl.FocusedRow.ParentRow.ChildRows.Remove(delRow);
            }

            if (cbe.Text != "自定义")
            {
                Hashtable table = cmb.Tag as Hashtable;
                string vid = table[cbe.SelectedIndex].ToString();                
                //获得下级的所有值
                List<View_ItemPropValue> propValueList = ItemPropValueService.GetView_ItemPropValueList(cid,pid, vid).ToList();

                if (propValueList.Count > 0)
                {
                    View_ItemPropValue ipv = propValueList.First();
                    EditorRowTag fTag = new EditorRowTag();
                    fTag.Pid = ipv.pid;
                    fTag.Cid = cid;
                    fTag.Vid = ipv.vid;
                    fTag.ChildTemplate = ipv.child_template;
                    fTag.IsMust = ipv.must;
                    row.Tag = fTag;
                    if (ipv.multi)
                    {
                        RepositoryItemCheckedComboBoxEdit childCmb = new RepositoryItemCheckedComboBoxEdit();
                        foreach (View_ItemPropValue value in propValueList)
                        {
                            childCmb.Items.Add(value.vid, value.name);
                        }

                        if (cmb.Items.Count > 0)
                        {
                            cmb.TextEditStyle = TextEditStyles.DisableTextEditor;
                        }
                        row.Properties.RowEdit = childCmb;

                        ////销售属性选中的时候引发的事件，销售属性一定是多选
                        //if (ipv.is_sale_prop)
                        //{
                        //    childCmb.EditValueChanged += new EventHandler(ccmb_EditValueChanged);
                        //}
                    }
                    else
                    {
                        RepositoryItemComboBox childCmb = new RepositoryItemComboBox();
                        childCmb.Name = ipv.pid.ToString();
                        Hashtable childTable = new Hashtable();
                        int index = 0;
                        foreach (View_ItemPropValue value in propValueList)
                        {
                            childTable.Add(index, value.vid);
                            childCmb.Items.Add(value.name);
                            index++;
                        }

                        childCmb.Tag = childTable;


                        //若items里面包含“自定义”选项，则继续监听其SelectedIndexChanged事件
                        if (childCmb.Items.Contains("自定义"))
                        {
                            childCmb.SelectedIndexChanged += new EventHandler(cmb_SelectedIndexChanged);
                        }
                        row.Properties.RowEdit = childCmb;
                    }

                    //添加row  
                    vGridCtrl.FocusedRow.ChildRows.Add(row);
                }
            }
            else if (cbe.Text == "自定义")
            {
                //如果是自定义，则在当前焦点行的父行里添加一子行(目前暂时作为其子项添加)
                string caption = vGridCtrl.FocusedRow.Properties.Caption + "自定义";
                row.Properties.Caption = caption;
                row.Properties.FieldName = caption;
                vGridCtrl.FocusedRow.ChildRows.Clear();
                vGridCtrl.FocusedRow.ChildRows.Add(row);/**/
            }            
        }

        void tree_AfterCheckNode(object sender, NodeEventArgs e)
        {
            //if (vGridCtrl.FocusedRow.Properties.Value == null || vGridCtrl.FocusedRow.Properties.Value.ToString() == string.Empty)
            //{
            //    if (e.Node.GetDisplayText(1) == string.Empty)
            //    {
            //        vGridCtrl.FocusedRow.Properties.Value = e.Node.GetDisplayText(0);
            //    }
            //    else
            //    {
            //        vGridCtrl.FocusedRow.Properties.Value = e.Node.GetDisplayText(1);
            //    }
            //}
            //else
            //{
            //    if (e.Node.GetDisplayText(1) == string.Empty)
            //    {
            //        vGridCtrl.FocusedRow.Properties.Value = "," + e.Node.GetDisplayText(0);
            //    }
            //    else
            //    {
            //        vGridCtrl.FocusedRow.Properties.Value = "," + e.Node.GetDisplayText(1);
            //    }                
            //}
        }

        void popEdit_Closed(object sender, ClosedEventArgs e)
        {
            try
            {
                //pid:vid
                //SortedList<string, SortedList<string, string>> fRowList = new SortedList<string, SortedList<string, string>>();
                //SortedList<string, string> sRowList = new SortedList<string, string>();

                TreeList tree = ((DevExpress.XtraTreeList.TreeList)(((DevExpress.XtraEditors.PopupContainerEdit)(sender)).Properties.PopupControl.Controls[0]));

                //用于给外面的Row赋值            
                string rowValue = string.Empty;

                //先清空
                salePropsList.Remove(vGridCtrl.FocusedRow.Properties.Caption);

                //自定义销售属性            
                foreach (TreeListNode node in tree.Nodes)
                {
                    if (node.Checked == true)
                    {
                        string value = tree.Text + ":" + node.GetDisplayText(0);
                        string key = tree.Tag + ":" + node.Tag;
                        if (node.GetDisplayText(1) == string.Empty)
                        {
                            rowValue += node.GetDisplayText(0) + ","; //用于给外面的Row赋值 

                            SortedList<string, string> keyValue = new SortedList<string, string>();
                            keyValue.Add(key, value);
                            //if (tag == true)
                            //{
                            //    RowListAdd(fRowList,key, value);
                            //}
                            //else
                            //{
                            //    RowListAdd(sRowList,key, value);
                            //}
                            SaleRowListAdd(salePropsList, vGridCtrl.FocusedRow.Properties.Caption, keyValue);
                        }
                        else
                        {
                            key += ":" + node.GetDisplayText(1);
                            value = tree.Text + ":" + node.GetDisplayText(1);
                            rowValue += node.GetDisplayText(1) + ","; //用于给外面的Row赋值 
                            SortedList<string, string> keyValue = new SortedList<string, string>();
                            keyValue.Add(key, value);
                            //if (tag == true)
                            //{
                            //    RowListAdd(fRowList, key, value);
                            //}
                            //else
                            //{
                            //    RowListAdd(sRowList, key, value);
                            //}
                            SaleRowListAdd(salePropsList, vGridCtrl.FocusedRow.Properties.Caption, keyValue);
                        }

                    }
                }
                vGridCtrl.FocusedRow.Properties.Value = rowValue.Trim(','); //用于给外面的Row赋值
                vGridCtrl.FocusNext();

                repositoryItemComboProps.Items.Clear();
                repositoryItemComboProps.Tag = null;

                if (salePropsList.Count != 2 && categoryRowSaleProps.ChildRows.Count == 2)
                    return;

                if (salePropsList.Count == 1 && categoryRowSaleProps.ChildRows.Count == 1)
                {
                    string code = string.Empty;
                    for (int k = 0; k < salePropsList.Values[0].Count; k++)
                    {
                        repositoryItemComboProps.Items.Add(salePropsList.Values[0].Values[k]);
                        code += salePropsList.Values[0].Keys[k] + ",";
                    }
                    pidVid = salePropsList.Values[0].Keys[0];
                    repositoryItemComboProps.Tag = code.Trim(',');
                }
                else if (salePropsList.Count == 2 && categoryRowSaleProps.ChildRows.Count == 2)
                {
                    string code = string.Empty;
                    for (int k = 0; k < salePropsList.Values[0].Count; k++)
                    {
                        for (int m = 0; m < salePropsList.Values[1].Count; m++)
                        {
                            repositoryItemComboProps.Items.Add(salePropsList.Values[0].Values[k] + ";" + salePropsList.Values[1].Values[m]);
                            code += salePropsList.Values[0].Keys[k] + ";" + salePropsList.Values[1].Keys[m] + ",";
                        }
                    }
                    repositoryItemComboProps.Tag = code.Trim(',');
                    pidVid = salePropsList.Values[0].Keys[0] + ";" + salePropsList.Values[1].Keys[0];

                }

                if (textPrice.Text != string.Empty)
                {
                    //for (int k = 0; k < repositoryItemComboProps.Items.Count; k++)
                    //{
                    //    string key = repositoryItemComboProps.Items[k].ToString();
                    //    RowListAdd(priceList, key, textPrice.Text)
                    //}
                    if (repositoryItemComboProps.Tag != null)
                    {
                        string[] codeList = repositoryItemComboProps.Tag.ToString().Split(',');
                        for (int i = 0; i < codeList.Length; i++)
                        {
                            string key = codeList[i];
                            if (!priceList.Keys.Contains(key))
                            {
                                priceList.Add(key, textPrice.Text);
                            }
                            else
                            {
                                priceList[key] = textPrice.Text;
                            }
                        }
                    }
                }

                if (repositoryItemComboProps.Items.Count > 0)
                {
                    barEditItemProps.EditValue = repositoryItemComboProps.Items[0];
                    barPrice.EditValue = textPrice.Text;

                    foreach (DataRow row in dTable.Rows)
                    {
                        if (row["Key"].ToString() == string.Empty)
                        {
                            row["Key"] = pidVid;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }

        private void RowListAdd(SortedList<string, string> rowList, string key, string value)
        {
            if (rowList.ContainsKey(key))
            {
                rowList[key] = value;
            }
            else
            {
                rowList.Add(key, value);
            }
        }

        private void SaleRowListAdd(SortedList<string, SortedList<string, string>> rowList, string key, SortedList<string, string> value)
        {
            if (rowList.ContainsKey(key))
            {
                if (rowList[key].Keys.Contains(value.Keys[0]))
                {
                    rowList[key][key] = value.Values[0];
                }
                else
                {
                    rowList[key].Add(value.Keys[0], value.Values[0]);
                }
            }
            else
            {
                rowList.Add(key, value);
            }
        }

        /// <summary>
        /// 销售属性变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ccmb_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //pid:vid
                //SortedList<string, string> fRowList = new SortedList<string, string>();
                //SortedList<string, string> sRowList = new SortedList<string, string>();
                //遍历销售属性
                //List<string> strList = new List<string>();
                //int i = 0;
                //foreach (BaseRow row in categoryRowSaleProps.ChildRows)
                //{
                //    RepositoryItemCheckedComboBoxEdit ccmb = row.Properties.RowEdit as RepositoryItemCheckedComboBoxEdit;
                //    if (ccmb == null)
                //    {
                //        continue;
                //    }
                //    EditorRowTag tag = row.Tag as EditorRowTag;
                //    i++;
                //    foreach (CheckedListBoxItem item in ccmb.Items)
                //    {
                //        //如果当前项目被选中
                //        if (item.CheckState == System.Windows.Forms.CheckState.Checked)
                //        {
                //            string key = row.Properties.Caption + ":" + item.Description;
                //            string value = tag.Pid + ":" + item.Value;                       
                //            sRowList.Add(key, value);

                //        }
                //    }               
                //}

                //先清空
                salePropsList.Remove(vGridCtrl.FocusedRow.Properties.Caption);

                CheckedComboBoxEdit ccmb = (DevExpress.XtraEditors.CheckedComboBoxEdit)(sender);
                EditorRowTag tag = vGridCtrl.FocusedRow.Tag as EditorRowTag;
                foreach (CheckedListBoxItem item in ccmb.Properties.Items)
                {
                    //如果当前项目被选中
                    if (item.CheckState == System.Windows.Forms.CheckState.Checked)
                    {
                        string value = vGridCtrl.FocusedRow.Properties.Caption + ":" + item.Description;
                        string key = tag.Pid + ":" + item.Value;
                        SortedList<string, string> keyValue = new SortedList<string, string>();
                        keyValue.Add(key, value);
                        SaleRowListAdd(salePropsList, vGridCtrl.FocusedRow.Properties.Caption, keyValue);
                    }
                }


                repositoryItemComboProps.Items.Clear();
                repositoryItemComboProps.Tag = null;

                if (salePropsList.Count != 2 && categoryRowSaleProps.ChildRows.Count == 2)
                    return;

                if (salePropsList.Count == 1 && categoryRowSaleProps.ChildRows.Count == 1)
                {
                    string code = string.Empty;
                    for (int k = 0; k < salePropsList.Values[0].Count; k++)
                    {
                        repositoryItemComboProps.Items.Add(salePropsList.Values[0].Values[k]);
                        code += salePropsList.Values[0].Keys[k] + ",";
                    }
                    pidVid = salePropsList.Values[0].Keys[0];
                    repositoryItemComboProps.Tag = code.Trim(',');
                }
                else if (salePropsList.Count == 2 && categoryRowSaleProps.ChildRows.Count == 2)
                {
                    string code = string.Empty;
                    for (int k = 0; k < salePropsList.Values[0].Count; k++)
                    {
                        for (int m = 0; m < salePropsList.Values[1].Count; m++)
                        {
                            repositoryItemComboProps.Items.Add(salePropsList.Values[0].Values[k] + ";" + salePropsList.Values[1].Values[m]);
                            code += salePropsList.Values[0].Keys[k] + ";" + salePropsList.Values[1].Keys[m] + ",";
                        }
                    }
                    repositoryItemComboProps.Tag = code.Trim(',');
                    pidVid = salePropsList.Values[0].Keys[0] + ";" + salePropsList.Values[1].Keys[0];
                    //foreach (DataRow row in dTable.Rows)
                    //{
                    //    if (row["Key"].ToString() == string.Empty)
                    //    {
                    //        row["Key"] = repositoryItemComboProps.Items[0];
                    //    }
                    //}                
                }



                if (textPrice.Text != string.Empty)
                {
                    //for (int k = 0; k < repositoryItemComboProps.Items.Count; k++)
                    //{
                    //    string key = repositoryItemComboProps.Items[k].ToString();
                    //    RowListAdd(priceList, key, textPrice.Text);
                    //}
                    if (repositoryItemComboProps.Tag != null)
                    {
                        string[] codeList = repositoryItemComboProps.Tag.ToString().Split(',');
                        for (int i = 0; i < codeList.Length; i++)
                        {
                            string key = codeList[i];
                            if (!priceList.Keys.Contains(key))
                            {
                                priceList.Add(key, textPrice.Text);
                            }
                            else
                            {
                                priceList[key] = textPrice.Text;
                            }
                        }
                    }
                }

                if (repositoryItemComboProps.Items.Count > 0)
                {
                    barEditItemProps.EditValue = repositoryItemComboProps.Items[0];
                    barPrice.EditValue = textPrice.Text;
                    foreach (DataRow row in dTable.Rows)
                    {
                        if (row["Key"].ToString() == string.Empty)
                        {
                            row["Key"] = pidVid;// repositoryItemComboProps.Items[0];
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
        /// 验证是否商品的必须属性全输入了
        /// </summary>
        /// <param name="vRows"></param>
        /// <returns></returns>
        private bool IsAllNeededPropsInput(VGridRows vRows)
        {
            foreach (BaseRow row in vRows)
            {
                if (row.Properties.ImageIndex == 0)
                {
                    if (row.Properties.Value == null)
                    {
                        return false;
                    }
                    else
                    {
                        if (row.Properties.Value.ToString().Trim() == string.Empty)
                        {
                            return false;
                        }
                    }
                }
                if (row.HasChildren)
                {
                    if (IsAllNeededPropsInput(row.ChildRows) == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 验证销售属性
        /// </summary>
        /// <param name="vRows"></param>
        /// <returns></returns>
        private bool IsSalePropsInput()
        {
            //遍历销售属性 
            bool checkTag = false;
            bool fTag = false;
            if (categoryRowSaleProps.ChildRows.Count == 2)
            {
                int i = 0;
                foreach (BaseRow row in categoryRowSaleProps.ChildRows)
                {
                    EditorRowTag tag = row.Tag as EditorRowTag;
                    if (tag.Is_Allow_Alias)
                    {
                        if (row.Properties.Value == null || row.Properties.Value.ToString() == string.Empty)
                        {
                            continue;
                        }
                        else
                        {
                            if (i == 0)
                            {
                                checkTag = true;
                            }
                            else
                            {
                                checkTag = !fTag;
                            }
                        }
                    }
                    else
                    {
                        RepositoryItemCheckedComboBoxEdit fccmb = row.Properties.RowEdit as RepositoryItemCheckedComboBoxEdit;
                        if (fccmb != null)
                        {
                            foreach (CheckedListBoxItem item in fccmb.Items)
                            {
                                //如果当前项目被选中
                                if (item.CheckState == System.Windows.Forms.CheckState.Checked)
                                {
                                    if (i == 0)
                                    {
                                        checkTag = true;
                                    }
                                    else
                                    {
                                        checkTag = !fTag;
                                    }
                                }
                            }
                        }
                    }
                    i++;
                    fTag = checkTag;
                }

                //BaseRow fRow = categoryRowSaleProps.ChildRows[0];
                //RepositoryItemCheckedComboBoxEdit fccmb = fRow.Properties.RowEdit as RepositoryItemCheckedComboBoxEdit;
                //if (fccmb != null)
                //{
                //    foreach (CheckedListBoxItem item in fccmb.Items)
                //    {
                //        //如果当前项目被选中
                //        if (item.CheckState == System.Windows.Forms.CheckState.Checked)
                //        {
                //            tag = true;
                //        }
                //    }
                //}

                //bool fTag = tag;
                //BaseRow sRow = categoryRowSaleProps.ChildRows[1];
                //RepositoryItemCheckedComboBoxEdit sccmb = sRow.Properties.RowEdit as RepositoryItemCheckedComboBoxEdit;
                //if (sccmb != null)
                //{
                //    foreach (CheckedListBoxItem item in sccmb.Items)
                //    {
                //        //如果当前项目被选中
                //        if (item.CheckState == System.Windows.Forms.CheckState.Checked)
                //        {
                //            tag = !fTag;
                //        }
                //    }
                //}
                if (checkTag == true)
                {
                    XtraMessageBox.Show("您只选了销售属性中的一项,请全选或全不选", Constants.SYSTEM_PROMPT);
                    return false;
                }
            }
            return true;
        }

        public SortedList<List<AssembleItem>, List<AssembleDetail>> GetAssembleItemList()
        {
            try
            {
                if (textName.Text.Trim() == string.Empty || textOuterID.Text.Trim() == string.Empty || pceItemCat.Text.Trim() == string.Empty
                    || textPrice.Text.Trim() == string.Empty || popupUnit.Text.Trim() == string.Empty || comboTax.Text == string.Empty)
                {
                    XtraMessageBox.Show("带*号的为必填项", Constants.SYSTEM_PROMPT);
                    return null;
                }

                //验证销售属性  
                if (IsSalePropsInput() == false)
                {
                    return null;
                }


                //验证必须输入的属性是否输入完整
                if (IsAllNeededPropsInput(vGridCtrl.Rows) == false)
                {
                    XtraMessageBox.Show("打钩的为必填属性");
                    return null;
                }

                if (gVStockProduct.RowCount == 0)
                {
                    XtraMessageBox.Show("请选择商品", Constants.SYSTEM_PROMPT);
                    return null;
                }
                for (int i = 0; i < gVStockProduct.RowCount; i++)
                {
                    DataRow dRow = gVStockProduct.GetDataRow(i);
                    if (dRow != null)
                    {
                        if (dRow["Count"] == null || dRow["Count"].ToString() == string.Empty)
                        {
                            XtraMessageBox.Show("请填写数量", Constants.SYSTEM_PROMPT);
                            return null;
                        }
                    }
                }

                #region AssembleItem
                AssembleItem assembleItem = new AssembleItem();
                assembleItem.AssembleDesc = memoDesc.Text;
                assembleItem.CatName = pceItemCat.Text;
                assembleItem.Cid = pceItemCat.Tag == null ? string.Empty : pceItemCat.Tag.ToString();
                assembleItem.Created = DateTime.Now;
                assembleItem.IsUsage = true;
                assembleItem.Model = textModel.Text;
                assembleItem.OuterID = textOuterID.Text;
                assembleItem.PicUrl = string.Empty;///////////

                #region 保存图片
                if (pictureEditPic.Image != null)
                {
                    Picture pic = new Picture();
                    pic.OuterID = assembleItem.OuterID;
                    pic.PictureCode = Guid.NewGuid().ToString();
                    pic.PictureRemark = string.Empty;
                    pic.PictureTitle = string.Empty;
                    pic.PictureContent = SystemHelper.GetImageBytes(pictureEditPic.Image);
                    PictureService.AddPicture(pic);
                }
                #endregion

                //assembleItem.Quantity = textQuantity.Text == string.Empty ? 0.0 : double.Parse(textQuantity.Text);
                assembleItem.SimpleName = textSimpleName.Text;
                //if (barEditItemProps.EditValue != null && barEditItemProps.EditValue.ToString() != string.Empty)
                //{
                //    assembleItem.SkuProps_Str = barEditItemProps.EditValue.ToString();
                //    if (repositoryItemComboProps.Tag != null)
                //    {
                //        string[] codeList = repositoryItemComboProps.Tag.ToString().Split(',');
                //        int index = repositoryItemComboProps.Items.IndexOf(assembleItem.SkuProps_Str);
                //        if (codeList.Length > index)
                //        {
                //            assembleItem.SkuProps = codeList[index];
                //        }
                //    }
                //}
                assembleItem.Specification = textSpecification.Text;
                assembleItem.TaxName = comboTax.Text;
                assembleItem.TaxCode = string.Empty;
                if (comboTax.Tag != null)
                {
                    string[] codeList = comboTax.Tag.ToString().Split(',');
                    if (codeList.Length > comboTax.SelectedIndex)
                    {
                        assembleItem.TaxCode = codeList[comboTax.SelectedIndex];
                    }
                }
                int length = popupUnit.Text.Length;
                if (popupUnit.Text.LastIndexOf('(') != -1)
                {
                    assembleItem.UnitName = popupUnit.Text.Substring(0, length - popupUnit.Text.LastIndexOf('(') + 1);
                }
                else
                {
                    assembleItem.UnitName = popupUnit.Text;
                }
                assembleItem.UnitCode = popupUnit.Tag == null ? string.Empty : popupUnit.Tag.ToString();

                /*属性串赋值*/
                assembleItem.Props += UIHelper.GetCategoryRowData(categoryRowKeyProps);
                assembleItem.Props += UIHelper.GetCategoryRowData(categoryRowNotKeyProps);
                assembleItem.Props += GetCategoryRowData(categoryRowSaleProps);
                /*去掉最后一个分号,注意判断是否为空*/
                if (!string.IsNullOrEmpty(assembleItem.Props) && assembleItem.Props.Contains(";"))
                {
                    assembleItem.Props = assembleItem.Props.Substring(0, assembleItem.Props.Length - 1);
                }
                else if (assembleItem.Props == null)
                {
                    assembleItem.Props = string.Empty;
                }

                Dictionary<string, string> inputDic = UIHelper.GetCategoryInputRowData(categoryRowKeyProps, categoryRowNotKeyProps);
                if (inputDic.Count > 0 && inputDic.Keys.Contains("pid") && inputDic.Keys.Contains("str"))
                {
                    assembleItem.InputPids = inputDic["pid"];
                    assembleItem.InputStr = inputDic["str"];
                }
                else
                {
                    assembleItem.InputPids = string.Empty;
                    assembleItem.InputStr = string.Empty;
                }

                #endregion

                SortedList<List<AssembleItem>, List<AssembleDetail>> assembleItemList = new SortedList<List<AssembleItem>, List<AssembleDetail>>();
                List<AssembleDetail> detailList = new List<AssembleDetail>();
                List<AssembleItem> itemList = new List<AssembleItem>();

                List<string> keyList = new List<string>();
                foreach (DataRow row in this.dTable.Rows)
                {
                    if (!keyList.Contains(row["Key"].ToString()))
                    {
                        keyList.Add(row["Key"].ToString());
                    }
                }
                DataView dv = new DataView(dTable);
                dv.Sort = "Key";
                for (int i = 0; i < keyList.Count; i++)
                {
                    assembleItem.AssembleCode = Guid.NewGuid().ToString();//和AssembleDetail相关联

                    if (repositoryItemComboProps.Tag != null && repositoryItemComboProps.Tag.ToString() != string.Empty)
                    {
                        string[] codeList = repositoryItemComboProps.Tag.ToString().Split(',');
                        for (int k = 0; k < codeList.Length; k++)
                        {
                            if (codeList[k] == keyList[i])
                            {
                                assembleItem.Name = string.Format("{0} 【{1}】", textName.Text, repositoryItemComboProps.Items[k]);//////
                                assembleItem.SkuProps = codeList[k];
                                assembleItem.SkuProps_Str = StringSort(repositoryItemComboProps.Items[k].ToString());
                            }
                        }
                    }
                    else
                    {
                        assembleItem.Name = textName.Text;
                        assembleItem.SkuProps = string.Empty;
                        assembleItem.SkuProps_Str = string.Empty;
                    }

                    if (priceList.Keys.Contains(keyList[i]))
                    {
                        assembleItem.Price = double.Parse(priceList[keyList[i]]);
                    }
                    else
                    {
                        assembleItem.Price = 0.0;
                    }

                    foreach (DataRowView dRow in dv.FindRows(keyList[i]))
                    {
                        if (dRow["SkuOuterID"] == null)
                            continue;
                        AssembleDetail detail = new AssembleDetail();
                        detail.AssembleCode = assembleItem.AssembleCode;
                        detail.SkuOuterID = dRow["SkuOuterID"].ToString();
                        detail.Count = int.Parse(dRow["Count"].ToString());
                        detailList.Add(detail);
                    }
                    //不能用同一个Assemble ，要出引用的问题
                    itemList.Add(GetData(assembleItem));
                }
                assembleItemList.Add(itemList, detailList);
                return assembleItemList;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
                return null;
            }
        }

        private AssembleItem GetData(AssembleItem src)
        {
            AssembleItem assembleItem = new AssembleItem();
            //这个code很重要,和Detail相关联
            assembleItem.AssembleCode = src.AssembleCode;
            assembleItem.Name = src.Name;
            assembleItem.Price = src.Price;
            assembleItem.AssembleDesc = src.AssembleDesc;
            assembleItem.CatName = src.CatName;
            assembleItem.Cid = src.Cid;
            assembleItem.Created = src.Created;
            assembleItem.IsUsage = src.IsUsage;
            assembleItem.Model = src.Model;
            assembleItem.OuterID = src.OuterID;
            assembleItem.PicUrl = src.PicUrl;
            assembleItem.SimpleName = src.SimpleName;
            assembleItem.SkuProps_Str = src.SkuProps_Str;
            assembleItem.SkuProps = src.SkuProps;
            assembleItem.Specification = src.Specification;
            assembleItem.TaxName = src.TaxName;
            assembleItem.TaxCode = src.TaxCode;
            assembleItem.UnitName = src.UnitName;
            assembleItem.UnitCode = src.UnitCode;
            assembleItem.Props = src.Props;
            return assembleItem;
        }

        private string StringSort(string str)
        {
            List<string> strList = str.Split(';').ToList();
            if (strList.Count == 2)
            {
                strList.Sort();
                return strList[0] + ";" + strList[1];
            }
            else
            {
                return str;
            }
        }


        /// <summary>
        /// 将销售属性控件中的属性以pid:vid串的形式返回
        /// </summary>
        /// <param name="fRow"></param>
        /// <returns></returns>
        public string GetCategoryRowData(BaseRow fRow)
        {
            try
            {
                string props = string.Empty;
                foreach (EditorRow row in fRow.ChildRows)
                {
                    EditorRowTag tag = row.Tag as EditorRowTag;
                    if (tag.Is_Allow_Alias)
                    {
                        RepositoryItemPopupContainerEdit popEdit = (RepositoryItemPopupContainerEdit)row.Properties.RowEdit;
                        if (popEdit == null)
                            continue;
                        PopupContainerControl popUp = popEdit.PopupControl;
                        if (popUp == null)
                            continue;
                        TreeList tree = (TreeList)popUp.Controls[0];
                        if (tree == null)
                            continue;
                        //自定义销售属性            
                        foreach (TreeListNode node in tree.Nodes)
                        {
                            if (node.Checked == true)
                            {
                                props += tree.Tag + ":" + node.Tag + ";";
                            }
                        }
                    }
                    else
                    {
                        /*若是多选，则有多个pid:vid串的pid相同*/
                        RepositoryItemCheckedComboBoxEdit ccmb = row.Properties.RowEdit as RepositoryItemCheckedComboBoxEdit;
                        foreach (CheckedListBoxItem item in ccmb.Items)
                        {
                            if (item.CheckState == CheckState.Checked)
                            {
                                props += tag.Pid.ToString() + ":" + item.Value + ";";
                            }
                        }
                    }
                }
                return props;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
                return string.Empty;
            }
        }


        /// <summary>
        /// 初始化组合属性
        /// </summary>
        public bool InitProps(bool IsAdd)
        {
            try
            {
                List<string> keyList = new List<string>();
                foreach (DataRow row in dTable.Rows)
                {
                    if (!keyList.Contains(row["Key"].ToString()))
                    {
                        keyList.Add(row["Key"].ToString());
                    }
                }

                if (repositoryItemComboProps.Items.Count > keyList.Count)
                {
                    if (XtraMessageBox.Show("还有其他组合属性尚未制定组合商品,是否继续?", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        //删除已制定的组合商品属性
                        string code = repositoryItemComboProps.Tag.ToString();
                        List<string> codeList = code.Split(',').ToList();
                        //用于存已制定组合的index
                        List<int> indexList = new List<int>();
                        foreach (string key in keyList)
                        {
                            priceList.Remove(key);
                            codeList.Remove(key);
                            int index = codeList.IndexOf(key);
                            if (index != -1)
                            {
                                indexList.Add(index);
                            }
                            if (code.Contains(string.Format(",{0}", key)))
                            {
                                code = code.Replace(string.Format(",{0}", key), "");
                            }
                            else
                            {
                                code = code.Replace(string.Format("{0},", key), "");
                            }
                        }
                        //先删除大的,这样才不影响序列
                        indexList.Sort();
                        for (int i = indexList.Count - 1; i >= 0; i--)
                        {
                            if (repositoryItemComboProps.Items.Count > indexList[i])
                            {
                                repositoryItemComboProps.Items.RemoveAt(indexList[i]);
                            }
                        }

                        repositoryItemComboProps.Tag = code;
                        barEditItemProps.EditValue = repositoryItemComboProps.Items[0];
                        if (codeList.Count > 0)
                        {
                            pidVid = codeList[0];
                        }
                        return true;
                    }
                }

                //保存并新增
                if (IsAdd == true)
                {
                    //int index = repositoryItemComboProps.Items.IndexOf(barEditItemProps.EditValue);
                    //repositoryItemComboProps.Items.Remove(barEditItemProps.EditValue);
                    //if (repositoryItemComboProps.Tag != null)
                    //{
                    //    string code = repositoryItemComboProps.Tag.ToString();
                    //    code = code.Replace(string.Format(",{0}", barEditItemProps.EditValue), "");
                    //    repositoryItemComboProps.Tag = code;
                    //}
                    //barEditItemProps.EditValue = repositoryItemComboProps.Items[0];                

                    textModel.Text = string.Empty;
                    textName.Text = string.Empty;
                    textOuterID.Text = string.Empty;
                    textPrice.Text = string.Empty;
                    textSimpleName.Text = string.Empty;
                    textSpecification.Text = string.Empty;
                    comboTax.Text = string.Empty;
                    comboTax.Tag = null;
                    pceItemCat.Text = string.Empty;
                    pceItemCat.Tag = null;
                    popupUnit.Text = string.Empty;
                    popupUnit.Tag = null;
                    memoDesc.Text = string.Empty;

                    barEditItemProps.EditValue = string.Empty;
                    barPrice.EditValue = string.Empty;
                    pidVid = string.Empty;
                    salePropsList = new SortedList<string, SortedList<string, string>>();
                    priceList = new SortedList<string, string>();

                    categoryRowKeyProps.ChildRows.Clear();
                    categoryRowNotKeyProps.ChildRows.Clear();
                    categoryRowSaleProps.ChildRows.Clear();
                    categoryRowStockProps.ChildRows.Clear();

                    categoryRowItemKeyProps.ChildRows.Clear();
                    categoryRowItemNotKeyProps.ChildRows.Clear();
                    categoryRowItemSaleProps.ChildRows.Clear();
                    categoryRowItemStockProps.ChildRows.Clear();

                    dTable.Rows.Clear();
                    gStockProduct.DataSource = null;
                    picBarCodeImage.Image = null;
                    pictureEditPic.Image = null;
                    repositoryItemComboProps.Items.Clear();
                    repositoryItemComboProps.Tag = null;

                    return true;
                }
                //else
                //{
                //    //保存
                //    if (repositoryItemComboProps.Items.Count > keyList.Count)
                //    {
                //        if (XtraMessageBox.Show("还有其他组合属性尚未制定组合商品,是否继续?", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                //        {
                //            //删除已制定的组合商品属性
                //            foreach (string key in keyList)
                //            {
                //                int index = repositoryItemComboProps.Items.IndexOf(barEditItemProps.EditValue);
                //                repositoryItemComboProps.Items.Remove(barEditItemProps.EditValue);
                //                if (repositoryItemComboProps.Tag != null)
                //                {
                //                    string code = repositoryItemComboProps.Tag.ToString();
                //                    code = code.Replace(string.Format(",{0}", barEditItemProps.EditValue), "");
                //                    repositoryItemComboProps.Tag = code;
                //                }
                //            }
                //            barEditItemProps.EditValue = repositoryItemComboProps.Items[0];
                //            return true;
                //        }
                //    }
                //}
                //textModel.Text = string.Empty;
                //textName.Text = string.Empty;
                //textOuterID.Text = string.Empty;
                //textPrice.Text = string.Empty;
                //textSimpleName.Text = string.Empty;
                //textSpecification.Text = string.Empty;
                //comboTax.Text = string.Empty;
                //comboTax.Tag = null;
                //pceItemCat.Text = string.Empty;
                //pceItemCat.Tag = null;
                //popupUnit.Text = string.Empty;
                //popupUnit.Tag = null;
                //memoDesc.Text = string.Empty;

                //categoryRowKeyProps.ChildRows.Clear();
                //categoryRowNotKeyProps.ChildRows.Clear();
                //categoryRowSaleProps.ChildRows.Clear();
                //categoryRowStockProps.ChildRows.Clear();

                //dTable = new DataTable();
                //picBarCodeImage.Image =null;
                //pictureEditPic.Image = null;
                return false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
                return true;
            }
        }
        #endregion


        private void treeUnit_DoubleClick(object sender, EventArgs e)
        {
            TreeListHitInfo hitInfo = treeUnit.CalcHitInfo(((System.Windows.Forms.MouseEventArgs)(e)).Location);

            /*如果单击到单元格内*/
            if (hitInfo.HitInfoType == HitInfoType.Cell)
            {
                if (hitInfo.Node != null && !hitInfo.Node.HasChildren)
                {
                    TreeListNodeTag tag = hitInfo.Node.Tag as TreeListNodeTag;
                    popupUnit.Text = hitInfo.Node.GetDisplayText(0);
                    /*绑定CID以便下面读取*/
                    popupUnit.Tag = tag.Cid;
                    popupUnit.ClosePopup();
                }
            }
        }

        private void tlItemCat_BeforeExpand(object sender, BeforeExpandEventArgs e)
        {
            TreeListNode focusedNode = e.Node;
            tlItemCat.FocusedNode = focusedNode;
            TreeListNodeTag tag = focusedNode.Tag as TreeListNodeTag;

            #region 获得当前节点的子节点
            if (!tag.HasExpanded)
            {
                tlItemCat.BeginUnboundLoad();

                //List<ItemCat> itemCatList = ItemCatService.GetItemCat(i => i.parent_cid.ToString() == tag.Cid && i.status == "normal");
                List<ItemCat> itemCatList = ItemCatService.GetItemCat(tag.Cid, "normal");
                if (itemCatList == null)
                    return;
                foreach (ItemCat itemCat in itemCatList)
                {
                    TreeListNode node = tlItemCat.AppendNode(new object[] { itemCat.name }, focusedNode, new TreeListNodeTag(itemCat.cid.ToString()));
                    node.HasChildren = (bool)itemCat.is_parent;
                }
                tlItemCat.EndUnboundLoad();
                tag.HasExpanded = true;
            }
            #endregion
        }

        private void textOuterID_EditValueChanged(object sender, EventArgs e)
        {
            //动态产生条形码
            Image myimg = Code128Rendering.MakeBarcodeImage(textOuterID.Text.Trim(), 1, true);
            picBarCodeImage.Image = myimg;
        }

        private void gVStockProduct_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "IsSelected")
            {
                gVStockProduct.SetFocusedRowCellValue("IsSelected", e.Value);
                DataRow row = gVStockProduct.GetFocusedDataRow();
                if (row["SkuOuterID"] != null)
                {
                    foreach (DataRow dRow in dTable.Rows)
                    {
                        if (dRow["SkuOuterID"] == row["SkuOuterID"])
                        {
                            dRow["IsSelected"] = e.Value;
                            break;
                        }
                    }
                }
            }
            else if (e.Column.FieldName == "Count")
            {
                gVStockProduct.SetFocusedRowCellValue("Count", e.Value);
                DataRow row = gVStockProduct.GetFocusedDataRow();
                if (row["SkuOuterID"] != null)
                {
                    foreach (DataRow dRow in dTable.Rows)
                    {
                        if (dRow["SkuOuterID"] == row["SkuOuterID"])
                        {
                            dRow["Count"] = e.Value;
                            break;
                        }
                    }
                }
            }
        }

        private void comboTax_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind.ToString() == "Plus")
            {
                TaxRateForm rate = new TaxRateForm();
                rate.ShowDialog();
                //刷新 
                InitTax();
            }
        }

        private void popupUnit_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind.ToString() == "Plus")
            {
                NewUnitForm unit = new NewUnitForm();
                unit.ShowDialog();
                //刷新
                InitUnit();
            }
        }

        private void textName_EditValueChanged(object sender, EventArgs e)
        {
            textSimpleName.Text = UIHelper.GetChineseSpell(textName.Text);
        }

        private void treeUnit_Click(object sender, EventArgs e)
        {
            TreeListHitInfo hitInfo = treeUnit.CalcHitInfo(((System.Windows.Forms.MouseEventArgs)(e)).Location);

            /*如果单击到单元格内*/
            if (hitInfo.HitInfoType == HitInfoType.Cell)
            {
                if (hitInfo.Node != null && !hitInfo.Node.HasChildren)
                {
                    TreeListNodeTag tag = hitInfo.Node.Tag as TreeListNodeTag;
                    popupUnit.Text = hitInfo.Node.GetDisplayText(0);
                    /*绑定CID以便下面读取*/
                    popupUnit.Tag = tag.Cid;
                    popupUnit.ClosePopup();
                }
            }
        }

        private void tlItemCat_Click(object sender, EventArgs e)
        {
            TreeListHitInfo hitInfo = tlItemCat.CalcHitInfo(((System.Windows.Forms.MouseEventArgs)(e)).Location);

            /*如果单击到单元格内*/
            if (hitInfo.HitInfoType == HitInfoType.Cell)
            {
                if (!hitInfo.Node.HasChildren)
                {
                    TreeListNodeTag tag = hitInfo.Node.Tag as TreeListNodeTag;

                    this.pceItemCat.Text = hitInfo.Node.GetDisplayText(0);

                    /*绑定CID以便下面读取*/
                    this.pceItemCat.Tag = tag.Cid;
                    this.pceItemCat.ClosePopup();

                    /*加载属性*/
                    LoadItemPropValue(false, tag.Cid);
                    //清空原有的销售属性
                    salePropsList = new SortedList<string, SortedList<string, string>>();
                    pidVid = string.Empty;
                }
            }
        }

        private void repositoryItemComboProps_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (repositoryItemComboProps.Tag != null)
                {
                    string[] codeList = repositoryItemComboProps.Tag.ToString().Split(',');
                    int index = ((DevExpress.XtraEditors.ComboBoxEdit)(sender)).SelectedIndex;
                    if (index != -1 && codeList.Length > index)
                    {
                        pidVid = codeList[index];
                    }
                }

                //价格
                //string nextValue = ((DevExpress.XtraEditors.ComboBoxEdit)(sender)).EditValue.ToString();
                if (priceList.Keys.Contains(pidVid))
                {
                    barPrice.EditValue = priceList[pidVid];
                }
                else
                {
                    barPrice.EditValue = string.Empty;
                }
                //if (gVStockProduct.RowCount > 0)
                //{

                SetDataSource();
                //DataView dv = new DataView(dTable);
                //dv.RowFilter = "Key='" + nextValue + "'";
                //if (dv.ToTable() != null && dv.ToTable().Rows.Count > 0)
                //{
                //    gStockProduct.DataSource = dv.ToTable();
                //}
                //else
                //{
                //    gStockProduct.DataSource = null;                  
                //    categoryRowItemSaleProps.ChildRows.Clear();
                //    categoryRowItemKeyProps.ChildRows.Clear();
                //    categoryRowItemNotKeyProps.ChildRows.Clear();
                //    categoryRowItemStockProps.ChildRows.Clear();
                //}
                //}


                //    if (g_AssembleList.Count > 0)
                //    {                    
                //        if (g_AssembleList.Keys.Contains(barEditItemProps.EditValue.ToString()))
                //        {                        
                //            g_AssembleList[barEditItemProps.EditValue.ToString()] = dv;//dTable.Select("Key='"+barEditItemProps.EditValue.ToString()+"'").c;
                //        }
                //        else
                //        {
                //            g_AssembleList.Add(barEditItemProps.EditValue.ToString(), dv);
                //        }
                //    }
                //    else
                //    {
                //        g_AssembleList.Add(barEditItemProps.EditValue.ToString(), dv);
                //    }
                //}
                //else
                //{
                //    if (g_AssembleList.Keys.Contains(barEditItemProps.EditValue.ToString()))
                //    {
                //        g_AssembleList.Remove(barEditItemProps.EditValue.ToString());
                //    }
                //}

                ////加载StockProduct
                //if (g_AssembleList.Keys.Contains(((DevExpress.XtraEditors.ComboBoxEdit)(sender)).EditValue.ToString()))
                //{
                //    gStockProduct.DataSource = g_AssembleList[((DevExpress.XtraEditors.ComboBoxEdit)(sender)).EditValue.ToString()];
                //    int rowHandle = gVStockProduct.FocusedRowHandle;
                //    //gStockProduct.DataSource = dTable;
                //    if (rowHandle == 0 && gVStockProduct.FocusedRowHandle > -1)
                //    {
                //        FocusedRowChange();
                //    }
                //}
                //else
                //{
                //    gStockProduct.DataSource = null;
                //    //this.dTable.Rows.Clear();
                //    categoryRowItemSaleProps.ChildRows.Clear();
                //    categoryRowItemKeyProps.ChildRows.Clear();
                //    categoryRowItemNotKeyProps.ChildRows.Clear();
                //    categoryRowItemStockProps.ChildRows.Clear();
                //}
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }

        }

        private void barBtnClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                List<DataRow> rowList = new List<DataRow>();
                foreach (DataRow row in dTable.Rows)
                {
                    if (row["Key"].ToString() == pidVid)
                    {
                        rowList.Add(row);
                    }
                }
                if (rowList != null && rowList.Count > 0)
                {
                    if (XtraMessageBox.Show("是否清空商品?", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        //DataView dView = new DataView(dTable);
                        //dView.RowFilter = "Key='" + pidVid + "'";
                        //DataTable dataTable = dView.ToTable();
                        //if (dataTable == null)
                        //    return;
                        foreach (DataRow row in rowList)
                        {
                            this.dTable.Rows.Remove(row);
                        }
                        //给StockProduct赋值
                        SetDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }

        private void gVStockProduct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRowChange();
        }

        /// <summary>
        /// 给StockProduct赋值                   
        /// </summary>
        private void SetDataSource()
        {
            try
            {
                int rowHandle = gVStockProduct.FocusedRowHandle;
                DataView dv = new DataView(dTable);
                dv.RowFilter = "Key='" + pidVid + "'";
                if (dv.ToTable() != null && dv.ToTable().Rows.Count > 0)
                {
                    gStockProduct.DataSource = dv.ToTable();
                }
                else
                {
                    gStockProduct.DataSource = null;
                    categoryRowItemSaleProps.ChildRows.Clear();
                    categoryRowItemKeyProps.ChildRows.Clear();
                    categoryRowItemNotKeyProps.ChildRows.Clear();
                    categoryRowItemStockProps.ChildRows.Clear();
                }
                if (rowHandle == 0 && gVStockProduct.FocusedRowHandle > -1)
                {
                    FocusedRowChange();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }

        /// <summary>
        /// 加载选择的商品属性
        /// </summary>
        private void FocusedRowChange()
        {
            try
            {
                if (panelContainer.ActiveChild == dockPanelProps)
                {
                    DataRow dRow = gVStockProduct.GetFocusedDataRow();
                    if (dRow != null)
                    {
                        View_ShopItem item = new View_ShopItem();
                        if (dRow["Cid"] != null)
                            item.cid = dRow["Cid"].ToString();
                        if (dRow["Props"] != null)
                            item.props = dRow["Props"].ToString();
                        if (dRow["InputPids"] != null)
                            item.input_pids = dRow["InputPids"].ToString();
                        if (dRow["InputStr"] != null)
                            item.input_str = dRow["InputStr"].ToString();
                        if (dRow["Property_Alias"] != null)
                            item.property_alias = dRow["Property_Alias"].ToString();

                        UIHelper.LoadItemPropValue(item, categoryRowItemKeyProps, categoryRowItemSaleProps, categoryRowItemNotKeyProps, categoryRowItemStockProps);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }

        private void vGridCtrl_Leave(object sender, EventArgs e)
        {
            //验证销售属性
            IsSalePropsInput();
        }

        private void textPrice_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (repositoryItemComboProps.Tag != null)
                {
                    string[] codeList = repositoryItemComboProps.Tag.ToString().Split(',');
                    for (int i = 0; i < codeList.Length; i++)
                    {
                        string key = codeList[i];
                        if (!priceList.Keys.Contains(key))
                        {
                            priceList.Add(key, textPrice.Text);
                        }
                        else
                        {
                            priceList[key] = textPrice.Text;
                        }
                    }
                }
                else
                {
                    priceList.Clear();
                    priceList.Add(pidVid, textPrice.Text);
                }
                if (priceList.Keys.Contains(pidVid))
                {
                    barPrice.EditValue = textPrice.Text;
                }

                //if (repositoryItemComboProps.Items.Count>0)
                //{
                //    for (int i = 0; i < repositoryItemComboProps.Items.Count; i++)
                //    {
                //        string key = repositoryItemComboProps.Items[i].ToString();
                //        if (!priceList.Keys.Contains(key))
                //        {
                //            priceList.Add(key, textPrice.Text);
                //        }
                //        else
                //        {
                //            priceList[key] = textPrice.Text;
                //        }
                //    }
                //}
                //if(barEditItemProps.EditValue!=null && priceList.Keys.Contains(barEditItemProps.EditValue.ToString()))
                //{
                //    barPrice.EditValue = textPrice.Text;
                //}
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }

        private void repositoryItemTextEditPrice_EditValueChanged(object sender, EventArgs e)
        {
            //if (barEditItemProps.EditValue != null)
            //{
            //    string key = barEditItemProps.EditValue.ToString();
            //    if (!priceList.Keys.Contains(key))
            //    {
            //        priceList.Add(key,((DevExpress.XtraEditors.TextEdit)(sender)).EditValue.ToString() );
            //    }
            //    else
            //    {
            //        priceList[key] = ((DevExpress.XtraEditors.TextEdit)(sender)).EditValue.ToString();
            //    }
            //}
            if (!priceList.Keys.Contains(pidVid))
            {
                priceList.Add(pidVid, ((DevExpress.XtraEditors.TextEdit)(sender)).EditValue.ToString());
            }
            else
            {
                priceList[pidVid] = ((DevExpress.XtraEditors.TextEdit)(sender)).EditValue.ToString();
            }

        }

        private void panelContainer_ActiveChildChanged(object sender, DevExpress.XtraBars.Docking.DockPanelEventArgs e)
        {
            if (panelContainer.ActiveChild == dockPanelProps)
            {
                FocusedRowChange();
            }
        }

        private void buttonPic_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                pictureEditPic.LoadImage();
                return;
            }
            else if (e.Button.Index == 1)
            {
                pictureEditPic.Image = null;
                return;
            }
        }

        //private void repositoryItemTextEditPrice_Leave(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (barPrice.EditValue != null && barPrice.EditValue.ToString() != string.Empty)
        //        {
        //            double price = double.Parse(((DevExpress.XtraEditors.TextEdit)(sender)).EditValue.ToString());
        //        }
        //    }
        //    catch
        //    {
        //        XtraMessageBox.Show("请正确输入价格");
        //    }
        //}
    }
}
