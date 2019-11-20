using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using Alading.Business;
using Alading.Entity;
using DevExpress.XtraTreeList;
using DevExpress.Utils;
using Alading.Taobao;
using Alading.Utils;
using DevExpress.XtraGrid;
using System.IO;
using Alading.Taobao.Entity.Extend;
using Alading.Taobao.API.Common;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using System.Linq;
using Alading.Forms.Stock.Control;
using Newtonsoft.Json;
using Alading.Core.Enum;
using System.Collections;
using System.Text.RegularExpressions;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.Objects;

namespace Alading.Controls.Init
{
    public partial class InitStock : DevExpress.XtraEditors.XtraUserControl
    {
        public InitStock()
        {
            InitializeComponent();
        }
      
        /// <summary>
        /// 当前宝贝sku列表
        /// </summary>
        DataTable saleInfoTable = new DataTable();

        List<string> listNewColumns = new List<string>();
 
        private void InitStock_Load(object sender, EventArgs e)
        {
            InitStockHouse();
            UIHelper.GetUnit(rICBUnit);
            TreeListNode rootNode = treeListCat.AppendNode(new object[] { "所有类目" }, null);
            AddNodes(rootNode, treeListCat);
            rootNode.ExpandAll();
            //LoadShop();
        }

        //未关联商品焦点行改变
        /// <summary>
        /// 展示未关联商品属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewNotAssociate_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadItem(e.FocusedRowHandle, gridViewNotAssociate);
            ViewShopItemInherit vsii = gridViewNotAssociate.GetRow(e.FocusedRowHandle) as ViewShopItemInherit;
            if (vsii != null)
            {
                GetLayout(vsii.StockHouseCode);
            }
        }

        //已关联商品焦点行改变
        /// <summary>
        /// 展示已关联商品属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewAssociate_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadItem(e.FocusedRowHandle, gridViewAssociate);
        }

        private void LoadItem(int focusedRowHandle, GridView gridView)
        {
            if (focusedRowHandle < 0)
            {
                return;
            }

            ViewShopItemInherit item = gridView.GetRow(focusedRowHandle) as ViewShopItemInherit;
            if (item != null)
            {
                List<ViewShopItemInherit> itemInheritList = new List<ViewShopItemInherit>();
                itemInheritList.Add(item);
                vGridCtrlBasicInfo.DataSource = itemInheritList;
                //第一次加载好了，如果是左边的列表发生变化，则重新加载ItemPropValue，否则只换cell值
                LoadItemPropValue(item, categoryRowKeyProps, categoryRowSaleProps, categoryRowNotKeyProps, categoryRowStockProps);

                //加载与之关联的库存商品
                if (!string.IsNullOrEmpty(item.outer_id))
                {
                    List<View_StockItemProduct> productlist = View_StockItemProductService.GetView_StockItemProductByOuterId(item.outer_id);
                    gridControlStock.DataSource = productlist;
                }
                else
                {
                    gridControlStock.DataSource = null;
                }
               
                //获取cid下的所有属性值
                List<View_ItemPropValue> viewPropValueList = ItemPropValueService.GetView_ItemPropValueList(item.cid, "-1", "-1");

                //获取该类目下所有必填的销售属性
                List<View_ItemPropValue> salePropMustList = viewPropValueList.Where(v => v.is_sale_prop).ToList();

                //宝贝没有销售属性时，构造一条sku,数量、价格、商家编码均为当前宝贝的值
                gridViewSku.Columns.Clear();
                gridControlSku.DataSource = null;
                saleInfoTable.Rows.Clear();
                saleInfoTable.Columns.Clear();
                listNewColumns.Clear();
                if (salePropMustList.Count == 0)
                {
                    Alading.Taobao.Entity.Sku sku = new Alading.Taobao.Entity.Sku();
                    sku.Price = item.price;
                    sku.Quantity = item.num;
                    sku.OuterId = string.Format("{0}-1", item.outer_id);
                    sku.SkuProps = string.Empty;
                    Alading.Taobao.Entity.Sku[] skuArray = new Alading.Taobao.Entity.Sku[] { sku };
                    Alading.Taobao.Entity.Extend.Skus skus = new Alading.Taobao.Entity.Extend.Skus();
                    skus.Sku = skuArray;
                    item.skus = JsonConvert.SerializeObject(skus);

                    //在gridViewSku中显示新构造的sku
                    GridColumn priceColumn = gcPrice;
                    gridViewSku.Columns.Insert(0, priceColumn);
                    GridColumn quantityColumn = gcSkuNum;
                    gridViewSku.Columns.Insert(0, quantityColumn);
                    GridColumn outidColumn = gcOuterid;
                    gridViewSku.Columns.Insert(1, outidColumn);
                    DataTable skuTable = new DataTable();
                    skuTable.Columns.Add("price");
                    skuTable.Columns.Add("outer_id");
                    skuTable.Columns.Add("quantity");
                    DataRow row= skuTable.NewRow();
                    row["price"] = item.price;
                    row["outer_id"] = item.outer_id;
                    row["quantity"] = item.num;
                    skuTable.Rows.Add(row);
                    gridControlSku.DataSource = skuTable;
                    gridViewSku.BestFitColumns();
                }
                else
                {
                    //加载宝贝sku
                    LoadItemSku(item.skus, item.property_alias, gridControlSku, gridView, gcPrice, gcSkuNum);
                    gridControlSku.DataSource = saleInfoTable.DefaultView;
                    gridViewSku.BestFitColumns();
                }

                //加载宝贝库存stockProduct信息
                if (!string.IsNullOrEmpty(item.outer_id))
                {
                    LoadStockItem(item.outer_id);
                }
            }
        }

        /// <summary>
        /// 加载宝贝库存信息
        /// </summary>
        /// <param name="outerid">宝贝商家编码</param>
        private void LoadStockItem(string outerid)
        {
            List<Alading.Entity.View_StockItemProduct> stockitemList = View_StockItemProductService.GetView_StockItemProductItem(outerid);
            if (stockitemList != null)
            {
                gridControlStock.DataSource = stockitemList;
                gridViewStock.BestFitColumns();
            }
        }

        #region 加载宝贝当前sku

        void ccmb_EditValueChanged(object sender, EventArgs e)
        {
            saleInfoTable.Rows.Clear();
            gridControlSku.DataSource = null;

            List<Props> listProps = new List<Props>();
            foreach (EditorRow row in categoryRowSaleProps.ChildRows)
            {
                EditorRowTag tag = row.Tag as EditorRowTag;
                string fieldName = "FieldName" + tag.Vid.ToString();

                #region 获取所有被选中的属性
                RepositoryItemCheckedComboBoxEdit ccmb = row.Properties.RowEdit as RepositoryItemCheckedComboBoxEdit;
                foreach (CheckedListBoxItem item in ccmb.Items)
                {
                    if (item.CheckState == System.Windows.Forms.CheckState.Checked)
                    {
                        Props prop = new Props();
                        prop.pid = tag.Pid;
                        prop.vid = item.Value.ToString();
                        prop.value = item.Description;
                        listProps.Add(prop);
                    }
                }
                #endregion
            }

            #region 向saleInfoTable中添加销售属性组合
            View_ShopItem viewitem = gridViewNotAssociate.GetFocusedRow() as View_ShopItem;
            if (viewitem != null)
            {
                //当前宝贝的所有sku
                Skus itemskus = TopUtils.DeserializeObject<Skus>(viewitem.skus);
                //向saleInfoTable中添加销售属性组合
                AddPropsToTable(listProps, itemskus, viewitem.property_alias, gridViewNotAssociate);
                gridControlSku.DataSource = saleInfoTable.DefaultView;
                gridViewSku.BestFitColumns();
            }
            #endregion
        }

        /// <summary>
        /// 加载宝贝当前sku
        /// </summary>
        /// <param name="item">当前宝贝</param>
        private void LoadItemSku(string skus, string property_alias, GridControl gridControlSku, GridView gridView, GridColumn gcPrice, GridColumn gcSkuNum)
        {
            //当前宝贝的所有sku
            Skus itemskus = JsonConvert.DeserializeObject<Skus>(skus);
        
            List<Props> listProps = new List<Props>();

            #region 向gridview 与 saleInfoTable中添加必备列
            saleInfoTable.Columns.Add("quantity", typeof(int));
            saleInfoTable.Columns.Add("price", typeof(double));
            saleInfoTable.Columns.Add("outer_id", typeof(string));
            saleInfoTable.Columns.Add("sku_id", typeof(string));

            GridColumn priceColumn = gcPrice;
            gridViewSku.Columns.Insert(0, priceColumn);

            GridColumn quantityColumn = gcSkuNum;
            gridViewSku.Columns.Insert(0, quantityColumn);

            GridColumn outidColumn = gcOuterid;
            gridViewSku.Columns.Insert(1, outidColumn);

            GridColumn skuidColumn = new GridColumn();
            skuidColumn.FieldName = "sku_id";
            skuidColumn.Visible = false;
            gridViewSku.Columns.Add(skuidColumn);

            # endregion

          
            foreach (EditorRow row in categoryRowSaleProps.ChildRows)
            {
                EditorRowTag tag = row.Tag as EditorRowTag;

                #region 向gridview 与 saleInfoTable 中动态添加列
                string fieldName = "FieldName" + tag.Pid.ToString();
                //允许自定义输入
                if (tag.Is_Allow_Alias)
                {
                    //添加一列到gridview控件中
                    GridColumn defcolumn = new GridColumn();
                    defcolumn.Caption = "自定义" + row.Properties.Caption;
                    defcolumn.FieldName = "def" + fieldName;
                    defcolumn.Name = "def" + row.Properties.Caption;
                    defcolumn.Visible = true;
                    defcolumn.VisibleIndex = 1;
                    if (gridViewSku.Columns.ColumnByFieldName("def" + fieldName) == null)
                    {
                        gridViewSku.Columns.Insert(1, defcolumn);
                    }
                    //添加一列到saleInfoTable
                    if (saleInfoTable.Columns.IndexOf("def" + fieldName) == -1)
                    {
                        saleInfoTable.Columns.Add("def" + fieldName, typeof(string));
                        listNewColumns.Add("def" + fieldName);
                    }
                }

                //向gridview中添加列显示属性值
                GridColumn column = new GridColumn();
                column.Caption = row.Properties.Caption;
                column.FieldName = fieldName;
                column.Name = row.Properties.Caption;
                column.Visible = true;
                column.VisibleIndex = 1;
                column.OptionsColumn.AllowEdit = false;

                //向gridview中添加列显示属性vid
                GridColumn vcolumn = new GridColumn();
                vcolumn.FieldName = "vid" + fieldName;
                vcolumn.Name = "vid" + row.Properties.Caption;
                vcolumn.Visible = false;

                if (gridViewSku.Columns.ColumnByFieldName(fieldName) == null)
                {
                    gridViewSku.Columns.Insert(1, column);
                    gridViewSku.Columns.Add(vcolumn);
                }
                //向saleInfoTable添加一列属性值与属性vid
                if (saleInfoTable.Columns.IndexOf(fieldName) == -1)
                {
                    saleInfoTable.Columns.Add(fieldName, typeof(string));
                    saleInfoTable.Columns.Add("vid" + fieldName, typeof(string));
                    //添加新增列
                    listNewColumns.Add(fieldName);
                    listNewColumns.Add("vid" + fieldName);
                }
                #endregion

                #region 获取所有被选中的属性
                RepositoryItemCheckedComboBoxEdit ccmb = row.Properties.RowEdit as RepositoryItemCheckedComboBoxEdit;
                foreach (CheckedListBoxItem item in ccmb.Items)
                {
                    if (item.CheckState == System.Windows.Forms.CheckState.Checked)
                    {
                        Props prop = new Props();
                        prop.pid = tag.Pid;
                        prop.vid = item.Value.ToString();
                        prop.value = item.Description;
                        listProps.Add(prop);
                    }
                }
                #endregion
            }

            //向saleInfoTable中添加销售属性组合
            AddPropsToTable(listProps, itemskus, property_alias, gridView);
        }

        /// <summary>
        /// 向saleInfoTable中添加销售属性组合 ，在gridControlSku显示
        /// </summary>
        private void AddPropsToTable(List<Props> listProps, Skus itemskus, string propertyAlias,GridView gridview)
        {
            List<IGrouping<string, Props>> propsGroups = listProps.GroupBy(i => i.pid).ToList();
            if (propsGroups.Count == 2)
            {
                #region 销售属性为2个
                foreach (Props prop in propsGroups[0])
                {
                    if (prop != null)
                    {
                        string fieldName = "FieldName" + prop.pid;
                        foreach (Props pprop in propsGroups[1])
                        {
                            if (pprop != null)
                            {
                                string ffieldName = "FieldName" + pprop.pid;
                                DataRow newRow = saleInfoTable.NewRow();
                                newRow[fieldName] = prop.value;
                                newRow["vid" + fieldName] = prop.vid;
                                newRow[ffieldName] = pprop.value;
                                newRow["vid" + ffieldName] = pprop.vid;
                                //获取重命名
                                newRow["def" + fieldName] = UIHelper.GetNewName(prop.vid, propertyAlias);
                                newRow["def" + ffieldName] = UIHelper.GetNewName(pprop.vid, propertyAlias);
                                //在sku中获取属性对应价格、数量
                                if (itemskus != null)
                                {
                                    Alading.Taobao.Entity.Sku sku = UIHelper.GetSelectedSku(prop.vid, pprop.vid, itemskus);
                                    newRow["price"] = sku.Price == null ? 0.0 : double.Parse(sku.Price);
                                    newRow["quantity"] = sku.Quantity;
                                    //newRow["outer_id"] = sku.OuterId;
                                    newRow["sku_id"] = sku.SkuId ?? string.Empty;
                                }
                                saleInfoTable.Rows.Add(newRow);
                            }
                        }
                    }
                }

                //添加skuouterid
                BuildSkuOutid();

                //重构当前宝贝sku和props
                ReBuildSkuAndProps(gridview);
                #endregion
            }
            else if (propsGroups.Count == 1)
            {
                #region 销售属性为1个
                foreach (Props prop in propsGroups.First())
                {
                    if (prop != null)
                    {
                        string fieldName = "FieldName" + prop.pid;
                        DataRow newRow = saleInfoTable.NewRow();
                        newRow[fieldName] = prop.value;
                        newRow["vid" + fieldName] = prop.vid;
                        //获取重命名
                        newRow["def" + fieldName] = UIHelper.GetNewName(prop.vid, propertyAlias);
                        //在sku中获取属性对应价格、数量
                        if (itemskus != null)
                        {
                            Alading.Taobao.Entity.Sku sku = UIHelper.GetSelectedSku(prop.vid, itemskus);
                            newRow["price"] = sku.Price == null ? 0.0 : double.Parse(sku.Price);
                            newRow["quantity"] = sku.Quantity;
                            //newRow["outer_id"] = sku.OuterId;
                            newRow["sku_id"] = sku.SkuId ?? string.Empty;
                        }
                        saleInfoTable.Rows.Add(newRow);
                    }
                }

                //添加skuouterid
                BuildSkuOutid();

                //重构当前宝贝sku和Props
                ReBuildSkuAndProps(gridview);
                #endregion
            }
        }

        /// <summary>
        /// 重构当前宝贝sku
        /// </summary>
        private void ReBuildSkuAndProps(GridView gridView)
        {
            #region 将saleInfoTable中的动态添加新列分类：属性类型列、重命名属性值列、属性值列
            //属性类型如颜色的列表
            List<string> listFieldNameVid = new List<string>();
            //属性值如红色的列表
            List<string> listVidFieldNameVid = new List<string>();
            //重命名的属性值的列表
            List<string> listdefFieldNameVid = new List<string>();

            foreach (string newColumn in listNewColumns)
            {
                if (newColumn.StartsWith("FieldName"))
                {
                    listFieldNameVid.Add(newColumn);
                }
                else if (newColumn.StartsWith("vid"))
                {
                    listVidFieldNameVid.Add(newColumn);
                }
                else
                {
                    listdefFieldNameVid.Add(newColumn);
                }
            }
            #endregion

            #region 重构sku与Property_Alias
            string propertity_Alias = string.Empty;
            Skus itemSkus = new Skus();
            List<string> listAlias = new List<string>();
            List<Alading.Taobao.Entity.Sku> listSku = new List<Alading.Taobao.Entity.Sku>();
            foreach (DataRow newRow in saleInfoTable.Rows)
            {
                //价格或数量中有一项为空时，忽略此行
                if (!string.IsNullOrEmpty(newRow["quantity"].ToString()) && !string.IsNullOrEmpty(newRow["price"].ToString()))
                {
                    Alading.Taobao.Entity.Sku sku = new Alading.Taobao.Entity.Sku();
                    sku.OuterId = newRow["outer_id"] == null ? string.Empty : newRow["outer_id"].ToString();
                    sku.Price = newRow["price"] == null ? string.Empty : newRow["price"].ToString();
                    sku.Quantity = newRow["quantity"] == null ? 0 : int.Parse(newRow["quantity"].ToString());

                    //计算sku的props
                    string skuProps = string.Empty;
                    for (int i = 0; i < listFieldNameVid.Count; i++)
                    {
                        //属性所属类型pid,如红色所属的颜色pid
                        string pid = listFieldNameVid[i].Replace("FieldName", string.Empty);
                        //属性vid
                        string vid = newRow[listVidFieldNameVid[i]].ToString();
                        if (!string.IsNullOrEmpty(pid) && !string.IsNullOrEmpty(vid))
                        {
                            skuProps += pid + ":" + vid + ";";
                        }

                        #region 计算Property_Alias
                        /*对于 FieldName1234,FieldName5678;defFieldName5678 即含有不可编辑的销售属性的情况时,
                      根据 listdefFieldNameVid 中的列名，判断当前属性值 FieldName**** 是否可自定义*/
                        bool hasDefFieldName = false;
                        int j = 0;
                        for (; j < listdefFieldNameVid.Count; j++)
                        {
                            if (listdefFieldNameVid[j].Contains(pid))
                            {
                                hasDefFieldName = true;
                                break;
                            }
                        }
                        //自定列不为空时，计算Property_Alias
                        if (hasDefFieldName && !string.IsNullOrEmpty(newRow[listdefFieldNameVid[j]].ToString()))
                        {
                            string newName = newRow[listdefFieldNameVid[j]].ToString();
                            string alias = pid + ":" + vid + ":" + newName;
                            if (!listAlias.Contains(alias))
                            {
                                listAlias.Add(alias);
                            }
                        }
                        #endregion
                    }
                    if (!string.IsNullOrEmpty(skuProps))
                    {
                        //去掉字符串最后的分号
                        sku.SkuProps = skuProps.TrimEnd(';');
                    }
                    listSku.Add(sku);
                }
            }
            itemSkus.Sku = listSku.ToArray();
            ViewShopItemInherit item = gridView.GetFocusedRow() as ViewShopItemInherit;
            if (item != null)
            {
                item.skus = JsonConvert.SerializeObject(itemSkus);
            }

            //计算出propertity_Alias字符串
            if (listAlias.Count != 0)
            {
                foreach (string alias in listAlias)
                {
                    propertity_Alias += alias + ";";
                }
                item.property_alias = propertity_Alias.TrimEnd(';');
            }
            #endregion

            #region 修改宝贝props
            string saleProps = UIHelper.GetCategoryRowData(categoryRowSaleProps);
            string keyProps = UIHelper.GetCategoryRowData(categoryRowKeyProps);
            string notKeyProps = UIHelper.GetCategoryRowData(categoryRowNotKeyProps);
            item.props = string.Format("{0}{1}{2}", saleProps, keyProps, notKeyProps);
            #endregion
        }

        /// <summary>
        /// 添加skuouterid
        /// </summary>
        private void BuildSkuOutid()
        {
            ViewShopItemInherit item = gridViewNotAssociate.GetFocusedRow() as ViewShopItemInherit;
            if (item==null)
            {
                return;
            }
            if (!string.IsNullOrEmpty(item.outer_id))
            {
                int index = 0;
                foreach (DataRow outerIdRow in saleInfoTable.Rows)
                {
                    index++;
                    outerIdRow["outer_id"] = string.Format(item.outer_id + "-" + "{0}", index);
                }
            }
        }

        #endregion

        #region 公用函数
        /// <summary>
        /// 展示Item
        /// </summary>
        /// <param name="cid">商品类目Cid</param>
        /// <param name="isAssociate">是否关联库存标志</param>
        private void ShowItemsInGridView(TreeListNode node, bool isAssociate, GridControl gridControl, GridView gridView)
        {
            if (node==null||node.Tag==null)
            {
                return;
            }

            List<string> listcid = GetAllChildNodeCid(node);
            listcid.Add(node.Tag.ToString());
            List<ViewShopItemInherit> listInheritItem = new List<ViewShopItemInherit>();
            //List<View_ShopItem> listItem = View_ShopItemService.GetView_ShopItem(listcid, isAssociate);
            string cids = string.Empty;
            foreach (string cid in listcid)
            {
                cids += "'" + cid + "'" + ",";
            }
            cids = cids.TrimEnd(',');
            List<View_ShopItem> listItem = View_ShopItemService.GetView_ShopItemToStock(cids,Convert.ToByte(isAssociate).ToString());
            if (listItem != null && listItem.Count() > 0)
            {
                string picPath = string.Empty;
                foreach (View_ShopItem shopItem in listItem)
                {
                    ViewShopItemInherit itemInherit = new ViewShopItemInherit();
                    shopItem.ItemPicture = GetImage(shopItem.sid, shopItem.pic_url);
                    CopyInherit(itemInherit, shopItem);
                    listInheritItem.Add(itemInherit);
                }
                gridControl.DataSource = listInheritItem;
                gridView.BestFitColumns();
            }
            else
            {
                gridControl.DataSource = null;
                gridView.BestFitColumns();
            }
        }

        /// <summary>
        /// 获取所有子节点cid
        /// </summary>
        /// <param name="parentCid">父节点类目Cid</param>
        private List<string> GetAllChildNodeCid(TreeListNode node)
        {
            List<string> listcid = new List<string>();
            if (node.HasChildren)
            {
                foreach (TreeListNode childnode in node.Nodes)
                {
                    if (childnode.Tag!=null)
                    {
                        listcid.Add(childnode.Tag.ToString());
                        listcid.AddRange(GetAllChildNodeCid(childnode));
                    }
                }
            }
            return listcid;
        }

        /// <summary>
        /// 根据houseCode获取库位下拉选项
        /// </summary>
        /// <param name="houseCode"></param>
        void GetLayout(string houseCode)
        {
            rICBStockLayoutName.Items.Clear();
            if (!string.IsNullOrEmpty(houseCode))
            {
                List<StockLayout> stockLayoutList = StockLayoutService.GetStockLayout(c => c.StockHouseCode == houseCode);
                Hashtable table = new Hashtable();
                int index = 0;
                foreach (StockLayout sl in stockLayoutList)
                {
                    rICBStockLayoutName.Items.Add(sl.LayoutName);
                    table.Add(index++, sl.StockLayoutCode);
                }
                rICBStockLayoutName.Tag = table;
            }
        }

        void InitStockHouse()
        {
            List<StockHouse> stockHouseList = StockHouseService.GetAllStockHouse();
            Hashtable table = new Hashtable();
            int index = 0;
            foreach (StockHouse sh in stockHouseList)
            {
                rICBStockHouseName.Items.Add(sh.HouseName);
                table.Add(index++, sh.StockHouseCode);
            }
            rICBStockHouseName.Tag = table;
        }

        /// <summary>
        /// 加载item所有的属性
        /// </summary>
        private void LoadItemPropValue(View_ShopItem item, CategoryRow categoryRowKeyProps, CategoryRow categoryRowSaleProps, CategoryRow categoryRowNotKeyProps, CategoryRow categoryRowStockProps)
        {
            if (item == null)//如果不存在,则不予以操作，防止脏数据造成异常
            {
                return;
            }

            //清除当前的子行
            categoryRowSaleProps.ChildRows.Clear();
            categoryRowKeyProps.ChildRows.Clear();
            categoryRowNotKeyProps.ChildRows.Clear();
            categoryRowStockProps.ChildRows.Clear();

            Hashtable propsTable = null;
            #region 分隔所有属性，如3032757:21942439;2234738:44627。pid:vid
            if (!string.IsNullOrEmpty(item.props))
            {
                propsTable = new Hashtable();
                //先按照;分割，再按照:分割，同时StringSplitOptions.RemoveEmptyEntries表示去掉空格
                List<string> propsList = item.props.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                foreach (string prop in propsList)
                {
                    string[] propArray = prop.Split(':');
                    if (propArray.Length==2)
                    {
                        if (!propsTable.ContainsKey(propArray[0]))
                        {
                            propsTable.Add(propArray[0], new List<string>());
                        }

                        List<string> vids = propsTable[propArray[0]] as List<string>;
                        vids.Add(propArray[1]);
                    }
                }
            }
            #endregion

            Hashtable inputPvsTable = null;
            #region 关键属性的自定义的名称 如：input_pids：20000,1632501，对应的input_str：FJH,688
            if (!string.IsNullOrEmpty(item.input_pids) && !string.IsNullOrEmpty(item.input_str))
            {
                string[] inputPidsArray = item.input_pids.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] inputStrArray = item.input_str.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                //保证两个值的数组长度一致，不出现索引错误
                if (inputPidsArray.Count() == inputStrArray.Count())
                {
                    inputPvsTable = new Hashtable();
                    for (int i = 0; i < inputPidsArray.Count(); i++)
                    {
                        if (!inputPvsTable.ContainsKey(inputPidsArray[i]))
                        {
                            inputPvsTable.Add(inputPidsArray[i], inputStrArray[i]);
                        }
                    }
                }
            }

            #endregion

            Hashtable propertyAliasTable = null;
            #region//重新命名销售属性,如：1627207:28341:黑色;1627207:3232481:棕色
            //if (!string.IsNullOrEmpty(item.property_alias))
            //{
            //    propertyAliasTable = new Hashtable();
            //    List<string> propertyAliasList = item.property_alias.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            //    foreach (string propertyAlias in propertyAliasList)
            //    {
            //        string[] propertyAliasArray = propertyAlias.Split(new Char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            //        if (propertyAliasArray.Count() == 3)
            //        {
            //            if (!propertyAliasTable.ContainsKey(propertyAliasArray[0]))
            //            {
            //                propertyAliasTable.Add(propertyAliasArray[0], new List<PropertyAlias>());
            //            }

            //            List<PropertyAlias> propertyAliasObjList = propertyAliasTable[propertyAliasArray[0]] as List<PropertyAlias>;
            //            PropertyAlias propertyAliasObj = new PropertyAlias();
            //            propertyAliasObj.Vid = propertyAliasArray[1];
            //            propertyAliasObj.Value = propertyAliasArray[2];
            //            propertyAliasObjList.Add(propertyAliasObj);
            //        }
            //    }
            //}
            #endregion

            //用存储过程获取所有cid对应的属性值对
            List<View_ItemPropValue> propValueList = ItemPropValueService.GetView_ItemPropValueList(item.cid, "-1", "-1");

            //按照pid分组绑定
            List<IGrouping<string, View_ItemPropValue>> propValueGroup = propValueList.GroupBy(i => i.pid).ToList();

            //遍历每一组
            List<EditorRow> rowList = new List<EditorRow>();
            List<IGrouping<string, View_ItemPropValue>> vipvList = new List<IGrouping<string, View_ItemPropValue>>();
            foreach (IGrouping<string, View_ItemPropValue> g in propValueGroup)
            {
                View_ItemPropValue ipv = g.FirstOrDefault(c => c.is_parent);
                if (ipv == null)
                {
                    ipv = g.First();
                }
                /*如果parent_vid==0且parent_pid==0则添加一行，否则该属性是子类，不添加*/
                if (ipv.parent_vid == "0" && ipv.parent_pid == "0")
                {
                    EditorRow row = new EditorRow();
                    row.Properties.Caption = ipv.prop_name;
                    EditorRowTag tag = new EditorRowTag();//可能会有问题
                    tag.Cid = ipv.cid;
                    tag.Is_Allow_Alias = ipv.is_allow_alias;
                    tag.IsMust = ipv.must;
                    tag.IsParent = ipv.is_parent;
                    tag.Pid = ipv.pid;
                    tag.Vid = ipv.vid;
                    row.Tag = tag;
                    if (ipv.must)
                    {
                        row.Properties.ImageIndex = 0;
                    }

                    /*通过判断is_muti,is_must,is_input....来设置相应的控件，同时通过is_sale等设置CategoryRow*/
                    #region 多选
                    if (ipv.multi)
                    {
                        RepositoryItemCheckedComboBoxEdit ccmb = new RepositoryItemCheckedComboBoxEdit();
                        row.Properties.RowEdit = ccmb;

                        #region 将Item中的自定义的属性名称，把原有的值替换掉，这样显示的就是替换后的值
                        foreach (View_ItemPropValue value in g)
                        {
                            if (propertyAliasTable != null && propertyAliasTable.ContainsKey(g.Key.ToString()))
                            {
                                List<PropertyAlias> propertyAliasObjList = propertyAliasTable[g.Key.ToString()] as List<PropertyAlias>;
                                if (propertyAliasObjList != null)
                                {
                                    PropertyAlias propertyAlias = propertyAliasObjList.Where(v => v.Vid == value.vid.ToString()).FirstOrDefault();
                                    if (propertyAlias != null)
                                    {
                                        ccmb.Items.Add(value.vid.ToString(), propertyAlias.Value);
                                    }
                                    else
                                    {
                                        ccmb.Items.Add(value.vid.ToString(), value.name);
                                    }
                                }
                            }
                            else
                            {
                                //这里设置的vid为value，所以赋值的时候也需要这个vid才能正常显示
                                ccmb.Items.Add(value.vid.ToString(), value.name);
                            }
                        }
                        #endregion

                        #region 设置基本属性
                        if (propsTable != null)
                        {
                            //如果属性中包含该pid
                            if (propsTable.ContainsKey(g.Key.ToString()))
                            {
                                List<string> vids = propsTable[g.Key.ToString()] as List<string>;
                                List<View_ItemPropValue> propValues = g.Where(v => vids.Contains(v.vid.ToString())).ToList();
                                if (propValues != null)
                                {
                                    List<string> vidList = new List<string>();
                                    foreach (View_ItemPropValue propValue in propValues)
                                    {
                                        vidList.Add(propValue.vid.ToString());
                                    }

                                    //分隔符号后面必须加个空格
                                    row.Properties.Value = string.Join(ccmb.SeparatorChar.ToString() + " ", vidList.ToArray());
                                    foreach (string vid in vidList)
                                    {
                                        foreach (CheckedListBoxItem citem in ccmb.Items)
                                        {
                                            if (citem.Value.ToString() == vid)
                                            {
                                                citem.CheckState = CheckState.Checked;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region 销售属性值改变注册事件
                        if (ipv.is_sale_prop)
                        {
                            ccmb.EditValueChanged += new EventHandler(ccmb_EditValueChanged);
                        }
                        #endregion
                    }
                    #endregion

                    #region 单选
                    else
                    {
                        RepositoryItemComboBox cmb = new RepositoryItemComboBox();
                        row.Properties.RowEdit = cmb;
                        cmb.Name = ipv.pid.ToString();
                        //给控件绑定tag
                        Hashtable table = new Hashtable();
                        int index = 0;
                        if (!ipv.must)
                        {
                            cmb.Items.Add(string.Empty);
                            table.Add(index++, string.Empty);
                        }
                        foreach (View_ItemPropValue value in g)
                        {
                            cmb.Items.Add(value.name);
                            table.Add(index++, value.vid);
                        }
                        cmb.Tag = table;
                        //如果pid是自定义的，说明有自定义的属性
                        if (inputPvsTable != null)
                        {
                            if (inputPvsTable.ContainsKey(g.Key.ToString()))
                            {
                                row.Properties.Value = "自定义";
                                //手动添加子row
                                EditorRow childRow = new EditorRow();
                                childRow.Properties.Caption = string.IsNullOrEmpty(ipv.child_template) ? "自定义" + row.Properties.Caption : ipv.child_template;
                                childRow.Properties.Value = inputPvsTable[g.Key.ToString()];
                                row.ChildRows.Add(childRow);
                            }
                        }

                        if (propsTable != null)
                        {
                            //如果属性中包含该pid
                            if (propsTable.ContainsKey(g.Key.ToString()))
                            {
                                List<string> vids = propsTable[g.Key.ToString()] as List<string>;

                                View_ItemPropValue propValue = g.Where(v => v.vid == (!string.IsNullOrEmpty(vids[0]) ? vids[0] : "-1")).FirstOrDefault();
                                if (propValue != null)
                                {
                                    row.Properties.Value = propValue.name;
                                }
                            }
                        }

                        //如果是下拉的则不能编辑cell value
                        if (cmb.Items.Count > 0)
                        {
                            cmb.TextEditStyle = TextEditStyles.DisableTextEditor;
                        }
                    }//else 
                    #endregion


                    if (ipv.is_key_prop)
                    {
                        categoryRowKeyProps.ChildRows.Add(row);
                        row.Properties.ReadOnly = true;
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
                    vipvList.Add(g);
                }
            }//foreach
            //over
            foreach (IGrouping<string, View_ItemPropValue> g in vipvList)
            {
                if (propsTable != null && propsTable.Contains(g.Key.ToString()))
                {
                    List<string> vids = propsTable[g.Key.ToString()] as List<string>;
                    if (vids == null || vids.Count == 0)
                    {
                        continue;
                    }

                    View_ItemPropValue propValue = g.Where(v => v.vid == (!string.IsNullOrEmpty(vids[0]) ? vids[0] : "-1")).FirstOrDefault();
                    if (propValue != null)
                    {
                        EditorRow row = rowList.Where(r => ((EditorRowTag)r.Tag).Pid == propValue.parent_pid).FirstOrDefault();
                        if (row != null)
                        {
                            EditorRow childRow = new EditorRow();
                            childRow.Properties.Caption = propValue.prop_name;
                            RepositoryItemComboBox cmb = new RepositoryItemComboBox();
                            cmb.TextEditStyle = TextEditStyles.DisableTextEditor;
                            EditorRowTag tag = new EditorRowTag();
                            tag.Pid = propValue.pid;
                            tag.Cid = propValue.cid;
                            tag.Vid = propValue.vid;
                            tag.IsMust = propValue.must;
                            tag.IsParent = propValue.is_parent;
                            tag.ChildTemplate = propValue.child_template;
                            childRow.Properties.RowEdit = cmb;
                            cmb.Name = propValue.pid.ToString();
                            Hashtable table = new Hashtable();
                            int index = 0;
                            if (!propValue.must)
                            {
                                cmb.Items.Add(string.Empty);
                                table.Add(index++, string.Empty);
                            }
                            foreach (View_ItemPropValue v in g)
                            {
                                cmb.Items.Add(v.name);
                                table.Add(index++, v.vid);
                            }
                            cmb.Tag = table;
                            childRow.Properties.Value = propValue.name;

                            //如果pid是自定义的，说明有自定义的属性
                            if (inputPvsTable != null && propValue.name == "自定义")
                            {
                                if (inputPvsTable.ContainsKey(g.Key.ToString()))
                                {
                                    //手动添加子row
                                    EditorRow cChildRow = new EditorRow();
                                    cChildRow.Properties.Caption = string.IsNullOrEmpty(propValue.child_template) ? "自定义" + childRow.Properties.Caption : propValue.child_template;
                                    cChildRow.Properties.Value = inputPvsTable[g.Key.ToString()];
                                    childRow.ChildRows.Add(cChildRow);
                                }
                            }

                            childRow.Tag = tag;
                            row.ChildRows.Add(childRow);
                            rowList.Add(childRow);
                            childRow.Properties.ReadOnly = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="picurl"></param>
        /// <returns></returns>
        private byte[] GetImage(string sid, string picurl)
        {
            StringBuilder dirBuilder = new StringBuilder();
            dirBuilder.Append(Application.StartupPath);
            dirBuilder.Append("\\ItemPics\\");
            dirBuilder.Append(sid);
            dirBuilder.Append("\\");
            dirBuilder.Append(Path.GetFileName(picurl));
            return SystemHelper.FileToBytes(dirBuilder.ToString());
        }

        /// <summary>
        /// 添加新增skuouterid到宝贝中
        /// </summary>
        private void AddOuterIdToSku()
        {
            ViewShopItemInherit item = gridViewNotAssociate.GetFocusedRow() as ViewShopItemInherit;
            if (string.IsNullOrEmpty(item.skus))
            {
                return;
            }
            //当前宝贝的所有sku
            Skus itemskus = JsonConvert.DeserializeObject<Skus>(item.skus);
            foreach (Alading.Taobao.Entity.Sku sku in itemskus.Sku)
            {
                foreach (DataRow skuRow in saleInfoTable.Rows)
                {
                    if (!string.IsNullOrEmpty(sku.SkuId) && sku.SkuId == skuRow["sku_id"].ToString())
                    {
                        sku.OuterId = skuRow["outer_id"].ToString();
                        break;
                    }
                }
            }
            //序列化改变后sku
            item.skus = JsonConvert.SerializeObject(itemskus);
        }

        /// <summary>
        /// 继承类赋值
        /// </summary>
        /// <param name="itemInherit"></param>
        /// <param name="item"></param>
        public void CopyInherit(ViewShopItemInherit itemInherit, View_ShopItem item)
        {
            /*默认值*/
            itemInherit.TotalQuantity = item.num;
            itemInherit.StockHouseName = Constants.DEFAULT_STOCKHOUSE_NAME;
            itemInherit.StockHouseCode = Constants.DEFAULT_STOCKHOUSE_CODE;
            itemInherit.StockLayoutName = Constants.DEFAULT_STOCKLAYOUT_NAME;
            itemInherit.StockLayoutCode = Constants.DEFAULT_STOCKLAYOUT_CODE;
            itemInherit.StockUnitName = Constants.DEFAULT_UNIT_NAME;
            itemInherit.UnitCode = Constants.DEFAULT_UNIT_CODE;
            itemInherit.StockCatName = Constants.DEFAULT_STOCKCAT_NAME;
            itemInherit.StockCid = Constants.DEFAULT_STOCKCAT_CID;

            itemInherit.OldOuterId = item.outer_id;//原商家编码
            itemInherit.outer_id = item.outer_id;//商家编码
            itemInherit.title = item.title;//商品名称
            itemInherit.pic_url = item.pic_url;//宝贝图片
            itemInherit.name = item.name;//淘宝类目
            itemInherit.ShopTitle = item.ShopTitle;//店铺名称
            itemInherit.delist_time = item.delist_time;//下架时间
            itemInherit.has_showcase = item.has_showcase;
            itemInherit.has_invoice = item.has_invoice;
            itemInherit.has_warranty = item.has_warranty;
            itemInherit.props = item.props;
            itemInherit.skus = item.skus;
            itemInherit.cid = item.cid;
            itemInherit.property_alias = item.property_alias;

            itemInherit.num = item.num;//库存数量
            itemInherit.TotalQuantity = (double)item.num;
            itemInherit.Recharge = string.Empty;//代充类型
            //读入图片
            itemInherit.ItemPicture = GetImage(item.sid, item.pic_url);

            itemInherit.post_fee = item.post_fee;
            itemInherit.express_fee = item.express_fee;
            itemInherit.ems_fee = item.ems_fee;
            itemInherit.increment = item.increment;
            itemInherit.has_discount = item.has_discount;
            itemInherit.auction_point = item.auction_point;
            //开始时间
            itemInherit.list_time = item.list_time;
            itemInherit.valid_thru = item.valid_thru;
            itemInherit.auto_repost = item.auto_repost;

            #region 选择值
            //新旧程度
            if (item.stuff_status == "new")
            {
                itemInherit.stuff_status = "全新";
            }
            else if (item.stuff_status == "unused")
            {
                itemInherit.stuff_status = "闲置";
            }
            else if (item.stuff_status == "second")
            {
                itemInherit.stuff_status = "二手";
            }
            else//未填
            {
                itemInherit.stuff_status = string.Empty;
            }

            //出价方式
            if (item.type == "fixed")
            {
                itemInherit.type = "一口价";
            }
            else if (item.type == "auction")
            {
                itemInherit.type = "拍卖";
            }
            else//未填
            {
                itemInherit.type = string.Empty;
            }

            //一口价或起拍价
            if (string.IsNullOrEmpty(item.price))
            {
                itemInherit.price = "0";
            }
            else
            {
                itemInherit.price = item.price;
            }

            //地址
            StringBuilder sbLocation = new StringBuilder();
            if (!string.IsNullOrEmpty(item.location_state))
            {
                sbLocation.Append(item.location_state);
            }
            if (!string.IsNullOrEmpty(item.location_city))
            {
                sbLocation.Append("/");
                sbLocation.Append(item.location_city);
            }
            if (!string.IsNullOrEmpty(item.location_district))
            {
                sbLocation.Append("/");
                sbLocation.Append(item.location_district);
            }
            itemInherit.Location = sbLocation.ToString();

            //运费
            if (item.freight_payer == "seller")
            {
                itemInherit.freight_payer = "卖家承担";
            }
            else if (item.freight_payer == "buyer")
            {
                itemInherit.freight_payer = "买家承担";
            }
            else//未填
            {
                itemInherit.freight_payer = string.Empty;
            } 
            #endregion

            #region 基本值
            itemInherit.iid = item.iid;
            itemInherit.detail_url = item.detail_url;//商品url
            itemInherit.num_iid = item.num_iid;//商品数字编码
            itemInherit.nick = item.nick;//卖家昵称
            itemInherit.cid = item.cid;//商品所属的叶子类目 id
            itemInherit.input_pids = item.input_pids;//用户自行输入的类目属性ID串
            itemInherit.input_str = item.input_str;//用户自行输入的子属性名和属性值
            itemInherit.desc = item.desc;//商品描述
            itemInherit.location_zip = item.location_zip;//邮政编码
            itemInherit.modified = item.modified;//商品修改时间
            itemInherit.approve_status = item.approve_status;//商品上传后的状态
            itemInherit.postage_id = item.postage_id;//宝贝所属的运费模板ID
            itemInherit.product_id = item.product_id;//宝贝所属产品的id(可能为空)
            itemInherit.item_imgs = item.item_imgs;//商品图片列表(包括主图)
            itemInherit.prop_imgs = item.prop_imgs;//商品属性图片列表
            itemInherit.is_virtual = item.is_virtual;//虚拟商品的状态字段
            itemInherit.is_taobao = item.is_taobao;//是否在淘宝显示
            itemInherit.videos = item.videos;//商品视频列表(目前只支持单个视频关联)
            itemInherit.is_3D = item.is_3D;//是否是3D淘宝的商品
            itemInherit.score = item.score;//商品所属卖家的信用等级数
            itemInherit.volume = item.volume;//商品30天交易量，只有调用商品搜索
            itemInherit.one_station = item.one_station;//是否淘1站商品
            itemInherit.StockProps = item.StockProps;//本地库存自定义属性
            itemInherit.KeyProps = item.KeyProps;//关键属性
            itemInherit.NotKeyProps = item.NotKeyProps;//非关键属性
            itemInherit.SaleProps = item.SaleProps;//销售属性
            itemInherit.IsAsociate = item.IsAsociate;//是否已关联
            //是否已更新到淘宝（针对初始化），标识本地的数据已经被更改，需要同步到淘宝
            itemInherit.IsUpdate = item.IsUpdate;
            itemInherit.IsHistory = item.IsHistory;//是否为历史商品，标记此商品是否已在淘宝上删除
            itemInherit.IsInAutoPlan = item.IsInAutoPlan;//是否加入自动上架计划
            itemInherit.ItemPicture = item.ItemPicture;//
            itemInherit.IsSelected = false;
            #endregion
        }
        #endregion

        #region 未关联库存视图切换
        private void barCheckItemList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControlNotAssociate.MainView = gridViewNotAssociate;
            barCheckItemCard.Checked = false;
            barCheckItemList.Checked = true;
        }
        private void barCheckItemCard_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControlNotAssociate.MainView = layoutViewNotAssociate;
            barCheckItemCard.Checked = true;
            barCheckItemList.Checked = false;
        }
        #endregion

        #region 加载店铺地址
        /// <summary>
        /// 点击按钮 加载淘宝地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemPopupContainerLocation_Popup(object sender, EventArgs e)
        {
            try
            {
                if (treeListLocation.AllNodesCount == 0)
                {
                    //获取省份
                    List<Area> areaList = AreaService.GetAreas("1");
                    TreeListNode Node = treeListLocation.AppendNode(new object[] { "全部省份" }, null, new AreaTag("1"));
                    foreach (Area stateArea in areaList)
                    {
                        TreeListNode stateNode = treeListLocation.AppendNode(new object[] { stateArea.name }, Node, new AreaTag(stateArea.id));
                        //设置是否有子节点，有则会显示一个+号
                        stateNode.HasChildren = true;
                    }
                    Node.HasChildren = true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 加载子节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListLocation_BeforeExpand(object sender, BeforeExpandEventArgs e)
        {
            TreeListNode focusedNode = e.Node;
            treeListLocation.FocusedNode = focusedNode;

            AreaTag areaTag = focusedNode.Tag as AreaTag;

            #region 获得当前节点的子节点
            if (!areaTag.HasExpanded)
            {
                treeListLocation.BeginUnboundLoad();
                List<Area> areaList = AreaService.GetAreas(areaTag.areaId); ;

                foreach (Area area in areaList)
                {
                    TreeListNode node = treeListLocation.AppendNode(new object[] { area.name }, focusedNode, new AreaTag(area.id));
                    if (area.type == 4)
                    {
                        //区县一级
                        node.HasChildren = false;
                    }
                    else
                    {
                        node.HasChildren = true;
                    }
                }
                treeListLocation.EndUnboundLoad();
                areaTag.HasExpanded = true;
            }
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListLocation_MouseClick(object sender, MouseEventArgs e)
        {
            TreeListHitInfo hitInfo = treeListLocation.CalcHitInfo(new Point(e.X, e.Y));
            /*如果单击到单元格内*/
            if (hitInfo.HitInfoType == HitInfoType.Cell)
            {
                if (!hitInfo.Node.HasChildren)
                {
                    AreaTag tag = hitInfo.Node.Tag as AreaTag;
                    TreeListNode node = hitInfo.Node;
                    StringBuilder sbLocation = new StringBuilder();
                    sbLocation.Append(node.GetDisplayText(0));
                    GetParentAreaName(node, sbLocation);
                    if (popupContainerCtrlLocation.OwnerEdit != null)
                    {
                        popupContainerCtrlLocation.OwnerEdit.Text = sbLocation.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// 获取父节点名称
        /// </summary>
        /// <param name="node"></param>
        /// <param name="sbLocation"></param>
        public void GetParentAreaName(TreeListNode node, StringBuilder sbLocation)
        {
            if (node.ParentNode != null && node.ParentNode.Id != 0)
            {
                sbLocation.Insert(0, "/");
                sbLocation.Insert(0, node.ParentNode.GetDisplayText(0));
                GetParentAreaName(node.ParentNode, sbLocation);
            }
            else
            {
                return;
            }
        }
        #endregion

        #region 加载库存类目

        /// <summary>
        /// 下拉时判断是否加载过库存类目，没加载过则加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemPopupEditGVStockCatName_Popup(object sender, EventArgs e)
        {
            if (treeListShopCat.AllNodesCount == 0)
            {
                UIHelper.LoadStockCat(treeListShopCat);
            }
        }

        private void repositoryItemPopupContainerEditStockCat_Popup(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 点击子节点加载子类目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListShopCat_BeforeExpand(object sender, BeforeExpandEventArgs e)
        {
            TreeListNode focusedNode = e.Node;
            treeListShopCat.FocusedNode = focusedNode;
            //XtraMessageBox.Show(tlItemCat.IsUnboundMode.ToString(), Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            TreeListNodeTag tag = focusedNode.Tag as TreeListNodeTag;

            #region 获得当前节点的子节点
            if (!tag.HasExpanded)
            {
                treeListShopCat.BeginUnboundLoad();
                List<StockCat> stockCatList = StockCatService.GetStockCat(i => i.ParentCid == tag.Cid.ToString());

                foreach (StockCat stockCat in stockCatList)
                {
                    TreeListNode node = treeListShopCat.AppendNode(new object[] { stockCat.StockCatName }, focusedNode, new TreeListNodeTag(stockCat.StockCid));
                    node.HasChildren = stockCat.IsParent;
                }
                treeListShopCat.EndUnboundLoad();
                tag.HasExpanded = true;
            }
            #endregion
        }

        /// <summary>
        /// 点击显示库存类目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListShopCat_MouseClick(object sender, MouseEventArgs e)
        {
            TreeListHitInfo hitInfo = treeListShopCat.CalcHitInfo(new Point(e.X, e.Y));
            /*如果单击到单元格内*/
            if (hitInfo.HitInfoType == HitInfoType.Cell)
            {
                if (!hitInfo.Node.HasChildren)
                {
                    TreeListNodeTag tag = hitInfo.Node.Tag as TreeListNodeTag;
                    //GetStockItem(tag.Cid, StockItemElement.StockCatCid);
                    TreeListNode node = hitInfo.Node;
                    string catName = node.GetDisplayText(0);
                    editorRowStockCat.Properties.RowEdit.NullText = catName;

                    gridViewNotAssociate.BeginUpdate();
                    ViewShopItemInherit fItem = gridViewNotAssociate.GetFocusedRow() as ViewShopItemInherit;
                    if (fItem != null)
                    {
                        fItem.StockCid = tag.Cid;
                        fItem.StockCatName = catName;
                        /*选中的行同步变化*/
                        int rowCount = gridViewNotAssociate.RowCount;
                        for (int i = 0; i < rowCount; i++)
                        {
                            ViewShopItemInherit item = gridViewNotAssociate.GetRow(i) as ViewShopItemInherit;
                            if (item.IsSelected == true)
                            {
                                /*选中的行的CODE赋值*/
                                item.StockCid = tag.Cid;
                                item.StockCatName = catName;
                                //gridViewNotAssociate.SetRowCellValue(i, gcStockCatName, catName);
                            }
                        }
                    }
                    (repositoryItemPopupEditGVStockCatName.PopupControl.OwnerEdit as PopupContainerEdit).ClosePopup();
                    gridViewNotAssociate.EndUpdate();
                }
            }
        }
        #endregion

        #region 按照店铺名称对treeListShop加载商品
        /// <summary>
        /// 加载店铺列表
        /// </summary>
        public void LoadShop()
        {
            treeListShop.Nodes.Clear();
            List<Shop> shopList = ShopService.GetAllShop();
            List<SellerCat> SellerCats = SellerCatService.GetAllSellerCat();

            foreach (Shop shop in shopList)
            {
                TreeListNode shopNode = treeListShop.AppendNode(new object[] { shop.nick }, -1);
                SellerCatTag sellerCatTag = new SellerCatTag();
                sellerCatTag.cid = "0";
                sellerCatTag.nick = shop.nick;
                shopNode.Tag = sellerCatTag;
                AddSellerCatNode(shop.nick, "0", shopNode, SellerCats);
            }
        }

        /// <summary>
        /// 加载店铺列表
        /// </summary>
        /// <param name="nick"></param>
        /// <param name="sellerCatCid"></param>
        /// <param name="Node"></param>
        /// <param name="SellerCats"></param>
        public void AddSellerCatNode(string nick, string sellerCatCid, TreeListNode Node, List<SellerCat> SellerCats)
        {
            List<SellerCat> listSellerCat = SellerCats.FindAll(delegate(Alading.Entity.SellerCat s) { return s.SellerNick == nick; });

            if (listSellerCat != null)
            {
                foreach (Alading.Entity.SellerCat sellerCat in listSellerCat)
                {
                    if (sellerCat.parent_cid == sellerCatCid)
                    {
                        TreeListNode childNode = treeListShop.AppendNode(new object[] { sellerCat.name }, Node);
                        SellerCatTag sellerCatTag = new SellerCatTag();
                        sellerCatTag.cid = sellerCat.cid;
                        sellerCatTag.nick = nick;
                        childNode.Tag = sellerCatTag;
                        AddSellerCatNode(nick, sellerCat.cid, childNode, SellerCats);
                    }
                }
            }
        }

        private void treeListShop_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            gridControlNotAssociate.DataSource = null;
            gridControlAssociate.DataSource = null;
            gridControlAll.DataSource = null;

            TreeListNode node = e.Node;
            try
            {
                switch (xtraTabControlItem.SelectedTabPageIndex)
                {
                    case 0:
                        //未关联
                        GetShopItem(node, false, gridViewNotAssociate, gridControlNotAssociate);
                        break;
                    case 1:
                        //关联
                        GetShopItem(node, true, gridViewAssociate, gridControlAssociate);
                        break;
                    case 2:
                        //全部
                        SellerCatTag tag = node.Tag as SellerCatTag;
                        List<View_ShopItem> listItem = View_ShopItemService.GetView_ShopItem(tag.nick, tag.cid);
                        if (listItem != null && listItem.Count>0)
                        {
                            string picPath = string.Empty;
                            foreach (View_ShopItem shopItem in listItem)
                            {
                                shopItem.ItemPicture = GetImage(shopItem.sid, shopItem.pic_url);
                            }

                            gridControlAll.DataSource = listItem;
                            gridViewAll.BestFitColumns();
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            waitFrm.Close();
        }

        public void GetShopItem(TreeListNode node, bool IsAsociate , GridView gridView, GridControl gridControl)
        {
            if (node==null)
            {
                return;
            }
            List<string> listSellerCid = new List<string>();
            SellerCatTag tag = node.Tag as SellerCatTag;
            if (tag != null)
            {
                listSellerCid.Add(tag.cid);
                GetChildSellerCatCid(node,listSellerCid);

                List<ViewShopItemInherit> listInheritItem = new List<ViewShopItemInherit>();
                List<View_ShopItem> listItem = View_ShopItemService.GetView_ShopItemBySellerCatCid(listSellerCid, tag.nick, IsAsociate);
                if (listItem != null && listItem.Count>0)
                {
                    string picPath = string.Empty;
                    foreach (View_ShopItem shopItem in listItem)
                    {
                        ViewShopItemInherit itemInherit = new ViewShopItemInherit();
                        shopItem.ItemPicture = GetImage(shopItem.sid, shopItem.pic_url);
                        CopyInherit(itemInherit, shopItem);
                        listInheritItem.Add(itemInherit);
                    }

                    gridControl.DataSource = listInheritItem;
                    gridView.BestFitColumns();
                }
            }
            //将操作列置为未选中状态
            for (int i = 0; i < gridView.RowCount; i++)
            {
                gridView.SetRowCellValue(i, "IsSelected", false);
            }
        }

        public void GetChildSellerCatCid(TreeListNode node, List<string> listSellerCid)
        {
            if (node.HasChildren)
            {
                TreeListNodes childNodes = node.Nodes;
                foreach (TreeListNode childNode in childNodes)
                {
                    SellerCatTag tag = childNode.Tag as SellerCatTag;
                    listSellerCid.Add(tag.cid);
                    GetChildSellerCatCid(childNode, listSellerCid);
                }
            }
        }
        #endregion

        #region 按照淘宝类目cid分类对treeList操作加载商品
        /// <summary>
        /// 向treeList中加载出售中的商品的淘宝类目
        /// </summary>
        /// <param name="node1">父节点</param>
        /// <param name="treeListCat">被加载的控件treeListCat</param>
        private void AddNodes(TreeListNode rootNode, TreeList treeListCat)
        {
            //获取所有Item的Cid
            List<string> listItemCid = ItemService.GetItemCids();
            //获取所有Item的淘宝类目及其父类目
            List<Alading.Entity.ItemCat> listItemCat = ItemCatService.GetAllItemCat(listItemCid);
            foreach (ItemCat itemCat in listItemCat)
            {
                if (itemCat.parent_cid == "0")
                {
                    TreeListNode childNode = treeListCat.AppendNode(new object[] { itemCat.name }, rootNode);
                    childNode.Tag = itemCat.cid;
                    AppendNodes(itemCat.cid, listItemCat, childNode, treeListCat);
                }
            }
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="cid">父节点Cid</param>
        /// <param name="listItemCat">所有类目</param>
        /// <param name="node1">父节点</param>
        /// <param name="treeListCat">被加载的控件treeListCat</param>
        private void AppendNodes(string cid, List<Alading.Entity.ItemCat> listItemCat, TreeListNode rootNode, TreeList treeListCat)
        {
            foreach (ItemCat itemCat in listItemCat)
            {
                if (itemCat.parent_cid == cid)
                {
                    TreeListNode childNode = treeListCat.AppendNode(new object[] { itemCat.name }, rootNode);
                    childNode.Tag = itemCat.cid;
                    AppendNodes(itemCat.cid, listItemCat, childNode, treeListCat);
                }
            }
        }

        /// <summary>
        /// 按淘宝类目分加载淘宝类目下的所有宝贝
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListCat_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            gridControlNotAssociate.DataSource = null;
            gridControlAssociate.DataSource = null;
            gridControlAll.DataSource = null;

            TreeListNode focusedNode = treeListCat.FocusedNode;
            if (focusedNode != null)
            {
                try 
                {
                    LoadItemsForXtraPage();

                    #region 加载首行
                    //关联
                    if (gridViewAssociate.FocusedRowHandle == 0)
                    {
                        LoadItem(gridViewAssociate.FocusedRowHandle, gridViewAssociate);
                        ViewShopItemInherit vsii = gridViewAssociate.GetRow(gridViewAssociate.FocusedRowHandle) as ViewShopItemInherit;
                        if (vsii != null)
                        {
                            GetLayout(vsii.StockHouseCode);
                        }
                    }
                    //未关联
                    if (gridViewNotAssociate.FocusedRowHandle == 0)
                    {
                        LoadItem(gridViewNotAssociate.FocusedRowHandle, gridViewNotAssociate);
                        ViewShopItemInherit vsii = gridViewNotAssociate.GetRow(gridViewNotAssociate.FocusedRowHandle) as ViewShopItemInherit;
                        if (vsii != null)
                        {
                            GetLayout(vsii.StockHouseCode);
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    waitFrm.Close();
                    XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            waitFrm.Close();
        }
        #endregion//按照淘宝类目cid分类

        //加载卡片商品信息
        private void layoutViewNotAssociate_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
            {
                return;
            }

            View_ShopItem item = layoutViewNotAssociate.GetRow(e.FocusedRowHandle) as View_ShopItem;
            if (item != null)
            {
                ViewShopItemInherit itemInherit = new ViewShopItemInherit();
                CopyInherit(itemInherit, item);
                List<ViewShopItemInherit> itemInheritList = new List<ViewShopItemInherit>();
                itemInheritList.Add(itemInherit);

                vGridCtrlBasicInfo.DataSource = itemInheritList;
                //第一次加载好了，如果是左边的列表发生变化，则重新加载ItemPropValue，否则只换cell值
                LoadItemPropValue(item, categoryRowKeyProps, categoryRowSaleProps, categoryRowNotKeyProps, categoryRowStockProps);
            }
        }

        //商家编码改变，skuOuterId随之改变
        private void repositoryItemTextEditOuterId_EditValueChanged(object sender, EventArgs e)
        {
            //商家编码一列值改变
            if (((DevExpress.XtraEditors.TextEdit)sender).EditorTypeName == "TextEdit")
            {
                gridControlSku.DataSource = null;
                int index = 0;
                foreach (DataRow outerIdRow in saleInfoTable.Rows)
                {
                    //若skuouterid为空
                    if (string.IsNullOrEmpty(outerIdRow["outer_id"].ToString()))
                    {
                        outerIdRow["outer_id"] = string.Format(((DevExpress.XtraEditors.TextEdit)sender).Text + "-" + "{0}", index);
                        index++;
                    }
                }
                gridControlSku.DataSource = saleInfoTable.DefaultView;
            }
        }

        //智能编码
        private void btnEncode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                List<ViewShopItemInherit> viewShopItemList = gridViewNotAssociate.DataSource as List<ViewShopItemInherit>;
                if (viewShopItemList == null || viewShopItemList.Count == 0)
                {
                    return;
                }
                List<ViewShopItemInherit> selectedItemList = viewShopItemList.Where(v => v.IsSelected == true).ToList();
                if (selectedItemList == null || selectedItemList.Count == 0)
                {
                    XtraMessageBox.Show(Constants.NOT_SELECT_ITEM);
                    return;
                }
                string cid = selectedItemList.FirstOrDefault().cid;
                List<View_ItemPropValue> viewPropValueList = ItemPropValueService.GetView_ItemPropValueList(cid,"-1","-1");
                int i = 0;
                foreach (ViewShopItemInherit item in selectedItemList)
                {
                    #region OuterId为空
                    if (string.IsNullOrEmpty(item.outer_id))
                    {
                        string keyPropValues = UIHelper.GetKeyPropRowValue(item, viewPropValueList);
                        List<string> strList = keyPropValues.Trim(';').Split(new char[] { ';', ':' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        List<string> keyPropList = new List<string>();
                        //获取关键属性的值
                        for (int j = 0; j < strList.Count; j++)
                        {
                            //这里不能做Remove，只能去增加
                            if (j % 2 != 0)
                            {
                                keyPropList.Add(strList[j]);
                            }
                        }
                        keyPropList = keyPropList.ConvertAll(a => UIHelper.GetChineseSpell(a).Trim().Replace(" ", string.Empty).ToUpper()).Distinct().ToList();//每个元素转化为大写拼音并去重

                        //最终值
                        string outerId = string.Empty;
                        //如果一个值包含另一个值，则保留长度较大的作为OuterId
                        if (keyPropList.Count == 2 && keyPropList[0].Contains(keyPropList[1]))
                        {
                            outerId = keyPropList[1];
                        }
                        else
                        {
                            foreach (string str in keyPropList)
                            {
                                outerId += str;
                            }
                        }
                        if (string.IsNullOrEmpty(outerId))
                        {
                            item.outer_id = string.Format("{0}-{1}", item.cid, i + 1);
                            i++;
                        }
                        else
                        {
                            item.outer_id = outerId;
                        }
                    }
                    #endregion

                    #region OuterId不为空
                    else//outerId不为空
                    {
                        //是否包含中文
                        if (Regex.IsMatch(item.outer_id, "[\u4e00-\u9fa5]"))
                        {
                            string chinese = string.Empty;
                            MatchCollection machs = Regex.Matches(item.outer_id, "[\u4e00-\u9fa5]");
                            IEnumerator iter = machs.GetEnumerator();
                            //获取中文
                            while (iter.MoveNext())
                            {
                                chinese += iter.Current;
                            }
                            //去掉中文剩余部分
                            string other = Regex.Replace(item.outer_id, "[\u4e00-\u9fa5]", string.Empty);
                            //去掉所有标点符号及空白
                            string pattern = "[，。？：；‘’！“”—……、]|(－{2})|(（）)|(【】)|({})|(《》)|\\s";
                            other = Regex.Replace(other, pattern, string.Empty);
                            //将汉字转换为大写首字母
                            chinese = UIHelper.GetChineseSpell(chinese);
                            if (other.Contains(chinese))
                            {
                                item.outer_id = other;
                            }
                            else if (chinese.Contains(other))
                            {
                                item.outer_id = chinese;
                            }
                            else
                            {
                                item.outer_id = chinese + other;
                            }
                        }
                        else
                        {
                            //去掉所有标点符号及空白
                            string pattern = "[，。？：；‘’！“”—……、]|(－{2})|(（）)|(【】)|({})|(《》)|\\s";
                            item.outer_id = Regex.Replace(item.outer_id, pattern, string.Empty);
                            item.outer_id = UIHelper.GetChineseSpell(item.outer_id);
                        }
                    }
                    #endregion

                    #region 生成sku_outer_id并保存至源数据中
                    if (string.IsNullOrEmpty(item.skus))
                    {
                        continue;
                    }
                    Skus itemskus = JsonConvert.DeserializeObject<Skus>(item.skus);
                    int sku_flag = 0;
                    foreach (Alading.Taobao.Entity.Sku sku in itemskus.Sku)
                    {
                        if (string.IsNullOrEmpty(sku.OuterId))
                        {
                            sku_flag++;
                            sku.OuterId = string.Format("{0}-{1}", item.outer_id, sku_flag);
                        }
                    }
                    item.skus = JsonConvert.SerializeObject(itemskus);
                    #endregion
                }
                gridViewNotAssociate.RefreshData();
                gridViewSku.RefreshData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        //入库
        private void btnInputStock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<ViewShopItemInherit> vsiiList = gridViewNotAssociate.DataSource as List<ViewShopItemInherit>;
            if (vsiiList == null || vsiiList.Count==0)
            {
                return;
            }
            /*判断是否选中了商品*/
            List<ViewShopItemInherit> vsiiListSelected  = vsiiList.FindAll(a =>a.IsSelected == true);
            List<ViewShopItemInherit> vsiiListNotSelected = vsiiList.FindAll(a => a.IsSelected == false);
            if (vsiiListSelected == null || vsiiListSelected.Count == 0)
            {
                XtraMessageBox.Show(Constants.NOT_SELECT_ITEM);
                return;
            }
            /*查找商家编码、库存类目、仓库与库位是否没有填写*/
            List<ViewShopItemInherit> list=  vsiiListSelected.Where(v => string.IsNullOrEmpty(v.outer_id) || string.IsNullOrEmpty(v.StockCatName) || string.IsNullOrEmpty(v.StockHouseName) ||
                string.IsNullOrEmpty(v.StockLayoutName) || string.IsNullOrEmpty(v.StockUnitName)).ToList();
            if (list != null && list.Count>0)
            {
                XtraMessageBox.Show("商家编码、库存类目、仓库、库位信息请填写完毕！");
                return;
            }
           
            #region 查找选中商品中outer_id相同的商品
            List<IGrouping<string, ViewShopItemInherit>> groupList = vsiiListSelected.GroupBy(c => c.outer_id).ToList();
            List<ViewShopItemInherit> repeatList = new List<ViewShopItemInherit>();
            foreach (IGrouping<string, ViewShopItemInherit> igroup in groupList)
            {
                if (igroup.Count() > 1)
                {
                    repeatList.AddRange(igroup.ToList());
                }
            } 
            #endregion

            #region 查找与数据库重复的商品
            List<string> outerIdList = vsiiListSelected.Select(v => v.outer_id).Distinct().ToList();
            List<string> dRepeatedOuterIdList = StockItemService.GetWhereInOuterIds(outerIdList);
            List<ViewShopItemInherit> dRepeatList = vsiiListSelected.FindAll(a => dRepeatedOuterIdList.Contains(a.outer_id)).ToList(); 
            #endregion

            if (repeatList.Count > 0 || dRepeatList.Count>0)
            {
                XtraMessageBox.Show("发现有重复的商家编码，请先处理再入库！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);

                ItemRepeatForm oidr = new ItemRepeatForm(repeatList, dRepeatList);
                oidr.ShowDialog();
                if (oidr.DialogResult != DialogResult.Yes)
                {
                    return;
                }
            }
        
            InputStock frm = new InputStock(vsiiListSelected);
            frm.ShowDialog();
            //显示没有入库成功的商品,需要加上原来没有勾中的
            vsiiListNotSelected.AddRange(frm.inPutFailedViewItemList);
            gridControlNotAssociate.DataSource = vsiiListNotSelected;
        }

        //  未关联全选
        private void repositoryItemCheckSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            List<ViewShopItemInherit> viewShopItemList = gridViewNotAssociate.DataSource as List<ViewShopItemInherit>;
            if (viewShopItemList == null)
            {
                return;
            }
            viewShopItemList.ForEach(a => a.IsSelected = ((DevExpress.XtraEditors.CheckEdit)(sender)).Checked);
            gridViewNotAssociate.RefreshData();
        }

        //已关联全选
        private void repositoryItemCheckAssociateSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            List<ViewShopItemInherit> viewShopItemList = gridViewAssociate.DataSource as List<ViewShopItemInherit>;
            if (viewShopItemList == null)
            {
                return;
            }
            viewShopItemList.ForEach(a => a.IsSelected = ((DevExpress.XtraEditors.CheckEdit)(sender)).Checked);
            gridViewAssociate.RefreshData();
        }

        // 宝贝商家编码变，其sku中商家编码同步变
        /// <summary>
        /// 宝贝商家编码变，其sku中商家编码同步变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewNotAssociate_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "outer_id")
            {
                if (string.IsNullOrEmpty(e.Value.ToString().Trim()))
                {
                    return;
                }

                gridControlSku.DataSource = null;
                int index = 0;
                foreach (DataRow outerIdRow in saleInfoTable.Rows)
                {
                    index++;
                    outerIdRow["outer_id"] = string.Format(e.Value + "-" + "{0}", index);
                }
                gridControlSku.DataSource = saleInfoTable.DefaultView;

                //保存修改
                AddOuterIdToSku();
            }
        }

        //当修改sku中商品数量，库存数量同步修改
        /// <summary>
        /// 当修改sku中商品数量，库存数量同步修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewSku_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            ViewShopItemInherit item = gridViewNotAssociate.GetFocusedRow() as ViewShopItemInherit;
            if (item==null)
            {
                return;
            }
            if (e.Column.FieldName == "quantity")
            {
                int totalQuantity = 0;
                foreach (DataRow outerIdRow in saleInfoTable.Rows)
                {
                    if (int.Parse(outerIdRow["quantity"].ToString()) < 0)
                    {
                        XtraMessageBox.Show("商品数量不能为负数！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    totalQuantity += int.Parse(outerIdRow["quantity"].ToString());
                }
                //修改库存中宝贝的总量
                item.TotalQuantity = totalQuantity;

                //修改gridViewNotAssociate中库存数量
                int rowhandle = gridViewNotAssociate.FocusedRowHandle;
                gridViewNotAssociate.SetRowCellValue(rowhandle, gridColumnStockNum, totalQuantity);
                gridViewNotAssociate.RefreshRowCell(rowhandle, gridColumnStockNum);

                //将对sku的修改保存到宝贝中
                ReBuildSkuAndProps(gridViewNotAssociate);
            }
            //添加自定义属性
            if (e.Column.FieldName.StartsWith("def"))
            {
                DataRowView focusedRow = gridViewSku.GetFocusedRow() as DataRowView;
                
                //自定义属性值
                string defProperty = focusedRow[e.Column.FieldName].ToString();
                //被自定义的属性列名
                string propertyColumn = e.Column.FieldName.Replace("def", string.Empty);
                //被自定义的属性值
                string focusedProperty = focusedRow[propertyColumn].ToString();
                //遍历gridViewSku,若当前行的一属性值与被自定义的属性值相同，对该属性的自定义列赋值
                for (int i = 0; i < gridViewSku.RowCount; i++)
                {
                    if (gridViewSku.GetDataRow(i)[propertyColumn] == focusedProperty)
                    {
                        gridViewSku.GetDataRow(i)[e.Column.FieldName] = defProperty;
                    }
                }
                gridViewSku.RefreshData();
                //将对sku的修改保存到宝贝中
                ReBuildSkuAndProps(gridViewNotAssociate);
            }
        }

        //仓库选项变化
        /// <summary>
        /// 仓库选项变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rICBStockHouseName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit combo = sender as ComboBoxEdit;
            /*开始更新*/
            gridViewNotAssociate.BeginUpdate();
            ViewShopItemInherit vsii = gridViewNotAssociate.GetFocusedRow() as ViewShopItemInherit;
            Hashtable table = rICBStockHouseName.Tag as Hashtable;
            vsii.StockHouseName = combo.Properties.Items[combo.SelectedIndex].ToString();
            vsii.StockHouseCode = table[combo.SelectedIndex].ToString();
            vsii.StockLayoutCode = string.Empty;
            vsii.StockLayoutName = string.Empty;
            /*根据选中的仓库编码加载其库位*/
            GetLayout(vsii.StockHouseCode);

            /*选中的行同步变化*/
            int rowCount = gridViewNotAssociate.RowCount;
            for (int i = 0; i < rowCount; i++)
            {
                ViewShopItemInherit item = gridViewNotAssociate.GetRow(i) as ViewShopItemInherit;
                if (item.IsSelected == true)
                {
                    /*选中的行的CODE赋值*/
                    item.StockHouseName = vsii.StockHouseName;
                    item.StockHouseCode = vsii.StockHouseCode;
                    item.StockLayoutCode = vsii.StockLayoutCode;
                    item.StockLayoutName = vsii.StockLayoutName;
                }
            }
            gridViewNotAssociate.EndUpdate();
        }

        // 库位选项变化
        /// <summary>
        /// 库位选项变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rICBStockLayoutName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit combo = sender as ComboBoxEdit;
            /*开始更新*/
            gridViewNotAssociate.BeginUpdate();
            ViewShopItemInherit vsii = gridViewNotAssociate.GetFocusedRow() as ViewShopItemInherit;
            Hashtable table = rICBStockLayoutName.Tag as Hashtable;
            vsii.StockLayoutName = combo.Properties.Items[combo.SelectedIndex].ToString();
            vsii.StockLayoutCode = table[combo.SelectedIndex].ToString();

            int rowCount = gridViewNotAssociate.RowCount;
            for (int i = 0; i < rowCount; i++)
            {
                ViewShopItemInherit item = gridViewNotAssociate.GetRow(i) as ViewShopItemInherit;
                if (item.IsSelected == true)
                {
                    /*选中的行的CODE赋值*/
                    item.StockLayoutName = vsii.StockLayoutName;
                    item.StockLayoutCode = vsii.StockLayoutCode;
                }
            }
            gridViewNotAssociate.EndUpdate();
        }

        //计量单位变化
        private void rICBUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit combo = sender as ComboBoxEdit;
            /*开始更新*/
            gridViewNotAssociate.BeginUpdate();
            ViewShopItemInherit vsii = gridViewNotAssociate.GetFocusedRow() as ViewShopItemInherit;
            Hashtable table = rICBUnit.Tag as Hashtable;
            vsii.StockUnitName = combo.Properties.Items[combo.SelectedIndex].ToString();
            vsii.UnitCode = table[combo.SelectedIndex].ToString();

            int rowCount = gridViewNotAssociate.RowCount;
            for (int i = 0; i < rowCount; i++)
            {
                ViewShopItemInherit item = gridViewNotAssociate.GetRow(i) as ViewShopItemInherit;
                if (item.IsSelected == true)
                {
                    /*选中的行的CODE赋值*/
                    item.StockUnitName = vsii.StockUnitName;
                    item.UnitCode = vsii.UnitCode;
                }
            }
            gridViewNotAssociate.EndUpdate();
        }

        //给gridColumnSelect赋值，避免选中延迟现象
        /// <summary>
        /// 给gridColumnSelect赋值，避免选中延迟现象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewNotAssociate_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == gridColumnSelect)
            {
                gridViewNotAssociate.SetFocusedRowCellValue(gridColumnSelect, e.Value);
            }
        }
         private void gridViewAssociate_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
             if (e.Column == gridColumnAssociateSelect)
            {
                gridViewAssociate.SetFocusedRowCellValue(gridColumnAssociateSelect, e.Value);
            }
        }

        //导出
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            DialogResult dialogResult = saveFileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                //gridControl1.ExportToXls(saveFileDialog.FileName, options);
                gridViewNotAssociate.ExportToXls(saveFileDialog.FileName);
                XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 取消关联 ,将宝贝的IsAssociate字段置false
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelAssociate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<ViewShopItemInherit> viewShopItemList = gridViewAssociate.DataSource as List<ViewShopItemInherit>;
            if (viewShopItemList == null || viewShopItemList.Count == 0)
            {
                return;
            }
            List<ViewShopItemInherit> selectedItemList = viewShopItemList.Where(v => v.IsSelected == true).ToList();
            if (selectedItemList == null || selectedItemList.Count == 0)
            {
                XtraMessageBox.Show(Constants.NOT_SELECT_ITEM);
                return;
            }
            
            List<string> iidList = selectedItemList.Select(i => i.iid).ToList();
            ItemService.UpdateItemsAssociate(iidList, false);
            //刷新取消关联操作后的页面
            LoadItemsForXtraPage();
        }

        //未关联与关联切换
        private void xtraTabControlItem_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadItemsForXtraPage();
        }

        /// <summary>
        /// 根据XtraTabControl的选择加载类目下商品
        /// </summary>
        private void LoadItemsForXtraPage()
        {
            this.barEditItemSelect.EditValue = false;
            this.barEditItemAssociateAll.EditValue = false;
            switch (xtraTabControlItem.SelectedTabPageIndex)
            {
                //未关联
                case 0:
                    if (navBarCtrlShop.ActiveGroup == navBarGroupShop)
                    {
                        TreeListNode node = treeListShop.FocusedNode;
                        GetShopItem(node, false, gridViewNotAssociate, gridControlNotAssociate);
                    }
                    else
                    {
                        TreeListNode focusedNode = treeListCat.FocusedNode;
                        ShowItemsInGridView(focusedNode, false, gridControlNotAssociate, gridViewNotAssociate);
                    }
                    break;
                //关联
                case 1:
                    if (navBarCtrlShop.ActiveGroup == navBarGroupShop)
                    {
                        TreeListNode node = treeListShop.FocusedNode;
                        GetShopItem(node, true, gridViewAssociate, gridControlAssociate);
                    }
                    else
                    {
                        TreeListNode focusedNode = treeListCat.FocusedNode;
                        ShowItemsInGridView(focusedNode, true, gridControlAssociate, gridViewAssociate);
                    }
                    break;
                //全部
                case 2:
                    if (navBarCtrlShop.ActiveGroup == navBarGroupShop)
                    {
                        TreeListNode node = treeListShop.FocusedNode;
                        SellerCatTag tag = node.Tag as SellerCatTag;
                        List<View_ShopItem> listItem = View_ShopItemService.GetView_ShopItem(tag.nick, tag.cid);
                        if (listItem != null)
                        {
                            string picPath = string.Empty;
                            foreach (View_ShopItem shopItem in listItem)
                            {
                                shopItem.ItemPicture = GetImage(shopItem.sid, shopItem.pic_url);
                            }

                            gridControlAll.DataSource = listItem;
                            gridViewAll.BestFitColumns();
                        }
                    }
                    else
                    {
                        TreeListNode focusedNode = treeListCat.FocusedNode;
                        //返回值不可能为Null
                        List<string> listcid = GetAllChildNodeCid(focusedNode);
                        if (focusedNode.Tag != null)
                        {
                            listcid.Add(focusedNode.Tag.ToString());
                        }
                        if (listcid.Count > 0)
                        {
                            List<View_ShopItem> listItem = View_ShopItemService.GetView_ShopItemList(listcid);
                            if (listItem != null)
                            {
                                gridControlAll.DataSource = listItem;
                                gridViewAll.BestFitColumns();
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public class SellerCatTag
        {
            public string nick { get; set; }

            public string cid { get; set; }
        }

        /// <summary>
        /// AreaTag对象
        /// </summary>
        public class AreaTag
        {
            public AreaTag(string areaId)
            {
                this.areaId = areaId;
                this.HasExpanded = false;
            }

            public string areaId { get; set; }

            public bool HasExpanded { get; set; }
        }

        public class Props
        {
            /// <summary>
            /// 属性所属分类的pid，如颜色的pid
            /// </summary>
            public string pid { get; set; }
           
            /// <summary>
            /// 属性的vid
            /// </summary>
            public string vid { get; set; }
           
            /// <summary>
            /// 属性值
            /// </summary>
            public string value { get; set; }
        
        }

        private void dockPanel1_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.gridViewNotAssociate.RefreshData();
        }

    
    }
}

/// <summary>
/// Item继承类
/// </summary>
public class ViewShopItemInherit : View_ShopItem
{
    /// <summary>
    /// 原外部编码，用来判断是否更新回淘宝
    /// </summary>
     public string OldOuterId{get;set;}

    /// <summary>
    /// 用于显示代充类型
    /// </summary>
    public string Recharge { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    public string Location { get; set; }

    /// <summary>
    /// 仓库名
    /// </summary>
    public string StockHouseName { get; set; }

    /// <summary>
    /// 库位名
    /// </summary>
    public string StockLayoutName { get; set; }


    /// <summary>
    /// 仓库编码
    /// </summary>
    public string StockHouseCode { get; set; }

    /// <summary>
    /// 库位编码
    /// </summary>
    public string StockLayoutCode { get; set; }

}

