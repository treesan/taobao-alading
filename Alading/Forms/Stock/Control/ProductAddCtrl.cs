using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraEditors;
using Alading.Utils;
using DevExpress.XtraTreeList.Nodes;
using Alading.Entity;
using Alading.Business;
using DevExpress.XtraTreeList;
using DevExpress.XtraVerticalGrid.Rows;
using System.Linq;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using System.Collections;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraRichEdit.Layout;
using Alading.Core.Enum;
using Alading.Taobao.API;
using Alading.Taobao.Entity.Extend;
using System.Text.RegularExpressions;
using Alading.Core.Code128;
using Alading.Forms.Stock.SettingUp;
using Alading.Taobao;
using DevExpress.Utils;
using System.Net;

namespace Alading.Forms.Stock.Control
{
    public delegate Image ItemImageDelegate(string picUrl);

    [ToolboxItem(true)]
    public partial class ProductAddCtrl : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 全部库存属性
        /// </summary>
        List<StockProp> allStockPropList = null;

        /// <summary>
        /// 全部库存属性值
        /// </summary>
        List<StockPropValue> allStockPropValueList;

        public bool StockHouseFlag = false;

        public bool StockLayoutFlag = true;

        /// <summary>
        /// 销售信息自定义表
        /// </summary>
        private DataTable saleInfoTable = new DataTable();

        /// <summary>
        /// 记录当前的productID
        /// </summary>
        string productID = "0";

        private DataTable saveCtTable = new DataTable();

        /// <summary>
        /// 存储当前选中的cid对应的View_ItemPropValue集合
        /// </summary>
        List<View_ItemPropValue> tmpPropValueList = null;

        /// <summary>
        /// 用于存储那些可自定义的属性的FieldName
        /// </summary>
        private List<string> defPropsList = new List<string>();

        /// <summary>
        /// 标记哪些销售属性选中过，哪些没有选择过。如果有3个销售属性，则必须选中3个。
        /// 则checkedFlag为3，如果checkedFlag>0&&checkedFlag<3则说明没有完全选中，不允许保存。
        /// </summary>
        int checkedSalePropCount = 0;

        public ProductAddCtrl()
        {
            InitializeComponent();
            saveCtTable.Columns.Add("SkuOuterID", typeof(string));
            saveCtTable.Columns.Add("SkuPrice", typeof(double));
            saveCtTable.Columns.Add("MinSkuPrice", typeof(double));
            saveCtTable.Columns.Add("MaxSkuPrice", typeof(double));
            saveCtTable.Columns.Add("CommissionPrice", typeof(double));
            saveCtTable.Columns.Add("WholeSalePrice", typeof(double));
            saveCtTable.Columns.Add("LastStockPrice", typeof(double));
            saveCtTable.Columns.Add("UsingWarnNum", typeof(bool));
            saveCtTable.Columns.Add("LowestNum", typeof(int));
            saveCtTable.Columns.Add("HighestNum", typeof(int));
            saveCtTable.Columns.Add("StockHouse", typeof(string));
            saveCtTable.Columns.Add("StockHouseCode", typeof(string));
            saveCtTable.Columns.Add("StockLayout", typeof(string));
            saveCtTable.Columns.Add("StockLayoutCode", typeof(string));
            saveCtTable.Columns.Add(gridColumnWeight.FieldName, typeof(float));
            saveCtTable.Columns.Add("Num", typeof(int));
            //UIHelper.GetUnit(mruUnit);
            UIHelper.GetTax(mruTaxRate);
            InitUnit();
            gvSaleInfo.BestFitColumns();
        }

        /// <summary>
        /// 加载计量单位
        /// </summary>
        private void InitUnit()
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

        private void ProductAdd_Load(object sender, EventArgs e)
        {
            allStockPropList = StockPropService.GetAllStockProp();
            allStockPropValueList = StockPropValueService.GetAllStockPropValue();
            //加载淘宝类目
            UIHelper.LoadItemCat(tlItemCat);
            UIHelper.LoadStockCat(tlStockCat);
            /*LOAD事件将仓库中*/
            UIHelper.LoadStockHouse(repositoryItemComboStockHouse);
            //加载库存类目            
        }

        /// <summary>
        /// 注意，这里修改了Columns的名字
        /// </summary>
        private void InitSaleInfo()
        {
            /*移除隐藏列*/
            List<GridColumn> columnList = new List<GridColumn>();
            foreach (GridColumn column in gvSaleInfo.Columns)
            {
                if (column.Visible == false && column.Name != gridColumnStockLayoutCode.Name && column.Name!=gridColumnStockHouseCode.Name)
                {
                    columnList.Add(column);
                }
            }
            foreach (GridColumn column in columnList)
            {
                gvSaleInfo.Columns.Remove(column);
            }
            //移除多余的列            
            int removeCount = gvSaleInfo.Columns.Count - 16;//有几行减几行
            for (int i = 0; i < removeCount; i++)
            {
                gvSaleInfo.Columns.RemoveAt(0);
            }

            saleInfoTable.Rows.Clear();
            saleInfoTable.Columns.Clear();

            saleInfoTable.Columns.Add("SkuOuterID", typeof(string));
            saleInfoTable.Columns.Add("SkuPrice", typeof(double));
            saleInfoTable.Columns.Add("MinSkuPrice", typeof(double));
            saleInfoTable.Columns.Add("MaxSkuPrice", typeof(double));
            saleInfoTable.Columns.Add("CommissionPrice", typeof(double));
            saleInfoTable.Columns.Add("WholeSalePrice", typeof(double));
            saleInfoTable.Columns.Add("LastStockPrice", typeof(double));
            saleInfoTable.Columns.Add("UsingWarnNum", typeof(bool));
            saleInfoTable.Columns.Add("LowestNum", typeof(int));
            saleInfoTable.Columns.Add("HighestNum", typeof(int));
            saleInfoTable.Columns.Add("Num", typeof(int));
            saleInfoTable.Columns.Add("StockHouse", typeof(string));
            saleInfoTable.Columns.Add("StockHouseCode", typeof(string));
            saleInfoTable.Columns.Add("StockLayout", typeof(string));
            saleInfoTable.Columns.Add("StockLayoutCode", typeof(string));
            saleInfoTable.Columns.Add(gridColumnWeight.FieldName, typeof(float));

            DataRow row = saleInfoTable.NewRow();
            row["SkuOuterID"] = string.Empty;
            row["SkuPrice"] = 0.0;
            row["MinSkuPrice"] = 0.0;
            row["MaxSkuPrice"] = 0.0;
            row["CommissionPrice"] = 0.0;
            row["WholeSalePrice"] = 0.0;
            row["LastStockPrice"] = 0.0;
            row["UsingWarnNum"] = false;
            row["LowestNum"] = 0;
            row["HighestNum"] = 0;
            row["Num"] = 0;
            row["StockHouse"] = string.Empty;
            row["StockLayout"] = string.Empty;
            row[gridColumnWeight.FieldName] = 0.0;
            saleInfoTable.Rows.Add(row);

            gridCtrlSaleInfo.DataSource = saleInfoTable.DefaultView;
            gvSaleInfo.BestFitColumns();
        }

        private void tlItemCat_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
        {
            TreeListNode focusedNode = e.Node;
            tlItemCat.FocusedNode = focusedNode;
            //XtraMessageBox.Show(tlItemCat.IsUnboundMode.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            TreeListNodeTag tag = focusedNode.Tag as TreeListNodeTag;

            #region 获得当前节点的子节点
            if (!tag.HasExpanded)
            {
                tlItemCat.BeginUnboundLoad();
                List<ItemCat> itemCatList = ItemCatService.GetItemCat(i => i.parent_cid.ToString() == tag.Cid && i.status == "normal");

                foreach (ItemCat itemCat in itemCatList)
                {
                    TreeListNode node = tlItemCat.AppendNode(new object[] { itemCat.name }, focusedNode, new TreeListNodeTag(itemCat.cid.ToString()));
                    node.HasChildren =(bool) itemCat.is_parent;
                }
                tlItemCat.EndUnboundLoad();
                tag.HasExpanded = true;
            }
            #endregion
        }        

        private void tlItemCat_MouseDown(object sender, MouseEventArgs e)
        {
            TreeListHitInfo hitInfo = tlItemCat.CalcHitInfo(new Point(e.X, e.Y));
            
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
                    LoadItemPropValue(tag.Cid.ToString());
                    StockHouseFlag = false;
                    StockLayoutFlag = false;
                }

                //默认添加一条自定义saleInfoTable记录            
                InitSaleInfo();
            }            
        }

        public void AllClear()
        {
            foreach (System.Windows.Forms.Control ct in groupControl1.Controls)
            {
                if (ct is Label)
                {

                }
                else
                {
                    ct.Text = string.Empty;
                }
            }            
            categoryRowKeyProps.ChildRows.Clear();
            categoryRowNotKeyProps.ChildRows.Clear();
            categoryRowSaleProps.ChildRows.Clear();
            categoryRowStockProps.ChildRows.Clear();
            InitSaleInfo();
        }

        #region 加载属性
        public void LoadItemPropValue(string cid)
        {
            categoryRowSaleProps.ChildRows.Clear();
            categoryRowKeyProps.ChildRows.Clear();
            categoryRowNotKeyProps.ChildRows.Clear();
            categoryRowStockProps.ChildRows.Clear();
            defPropsList.Clear();

            tmpPropValueList = ItemPropValueService.GetView_ItemPropValueList(cid, "-1","-1");
            if (tmpPropValueList == null)
                return;
            List<IGrouping<string, View_ItemPropValue>> propValueGroup = tmpPropValueList.Where(p => p.parent_pid == "0" && p.parent_vid == "0").GroupBy(i => i.pid).ToList();
            foreach (IGrouping<string, View_ItemPropValue> g in propValueGroup)
            {
                View_ItemPropValue ipv = g.FirstOrDefault(c=>c.is_parent==true);
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
                    row.Properties.RowEdit = ccmb;
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
                }
                else
                {
                    RepositoryItemComboBox cmb = new RepositoryItemComboBox();
                    row.Properties.RowEdit = cmb;
                    cmb.Name = ipv.pid.ToString();

                    /*如果该属性不是必须的,则其第0项是空值*/
                    Hashtable table = new Hashtable();
                    int index = 0;
                    /*如果该属性不是必须的且不是可输入的属性，则添加一行空行。*/
                    if (!ipv.must &&!ipv.is_input_prop)
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
                        cmb.SelectedIndexChanged += new EventHandler(cmb_SelectedIndexChanged);
                        tag.IsParent=true;
                    }
                }
                /*tag赋值*/
                row.Tag = tag;

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
                View_ItemPropValue prop = tmpPropValueList.FirstOrDefault(c => c.pid == tag.Pid && c.vid == vid);
     
                //获得下级的所有值
                List<View_ItemPropValue> propValueList = tmpPropValueList.Where(i => i.parent_pid == pid && i.parent_vid == vid).ToList();

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

            /*加载product*/
            string propValue = string.Empty;
            if (GetProductValue(ref propValue, categoryRowKeyProps))
            {
                /*清空非关键属性及销售属性当前显示值*/
                RowValueClear(categoryRowNotKeyProps);
                RowValueClear(categoryRowSaleProps);

                ProductValueSorted(ref propValue);
                Alading.Entity.Product product = null;
                List<Alading.Entity.Product> procuctList = ProductService.GetProduct(p => p.props == propValue);
                //如果数据库中有，则将第一天数据取出，如果没有，到网上去取
                if (procuctList.Count > 0)
                {
                    product = procuctList.First();
                }
                else
                {
                    ProductRsp TPtoductRsp = TopService.ProductGet(tag.Cid, propValue);
                    Alading.Taobao.Entity.Product tProduct = TPtoductRsp.Product;
                    if (tProduct != null)
                    {
                        product = new Alading.Entity.Product();
                        product.binds = tProduct.BindProps != null ? tProduct.BindProps : string.Empty;
                        product.binds_str = tProduct.BindPropStrs != null ? tProduct.BindPropStrs : string.Empty;
                        product.cat_name = tProduct.CategoryName != null ? tProduct.CategoryName : string.Empty;
                        product.cid = tProduct.CategoryId != null ? tProduct.CategoryId : "-1";///////
                        product.created = tProduct.Created != null ? tProduct.Created : string.Empty;
                        product.desc = tProduct.Description != null ? tProduct.Description : string.Empty;
                        product.modified = tProduct.Modified != null ? tProduct.Modified : string.Empty;
                        product.name = tProduct.Name != null ? tProduct.Name : string.Empty;
                        product.outer_id = tProduct.OuterId != null ? tProduct.OuterId : string.Empty;
                        product.pic_url = tProduct.PrimaryImgUrl != null ? tProduct.PrimaryImgUrl : string.Empty;
                        product.price = tProduct.Price != null ? tProduct.Price : string.Empty;
                        product.product_id = tProduct.Id != null ? tProduct.Id : "-1";
                        product.product_imgs = string.Empty;////
                        product.product_prop_imgs = string.Empty;////
                        string props = tProduct.Props != null ? tProduct.Props : string.Empty;
                        /*将关键属性按照规则排序再存入数据库，保证下次取出时一定能对得上号*/
                        ProductValueSorted(ref props);
                        product.props = props;
                        if (!string.IsNullOrEmpty(product.pic_url))
                        {
                            ItemImageDelegate imgDelegate = new ItemImageDelegate(GetItemImage);
                            IAsyncResult asyncResult = imgDelegate.BeginInvoke(product.pic_url, new AsyncCallback(GetItemImageCallback), imgDelegate);
                        }
                        product.props_str = tProduct.PropStrs != null ? tProduct.PropStrs : string.Empty;
                        product.sale_props = tProduct.SaleProps != null ? tProduct.SaleProps : string.Empty;
                        product.sale_props_str = tProduct.SalePropStrs != null ? tProduct.SalePropStrs : string.Empty;
                        product.tsc = tProduct.StandardId != null ? tProduct.StandardId : string.Empty;
                        ProductService.AddProduct(product);


                        /*记录当前的productID*/
                        productID = product.product_id.ToString();

                        /*分隔非关键属性pidvid串及其值串，并遍历非关键属性行，找到对应的属性行并给该行赋值*/
                        List<string> propValueList = product.binds.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        List<string> propValue_strList = product.binds_str.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        for (int i = 0; i < propValueList.Count; i++)
                        {
                            string pv = propValueList[i];
                            string pv_str = propValue_strList[i];
                            string prop = pv.Split(':')[0];
                            string value = pv.Split(':')[1];
                            string value_str = pv_str.Split(':')[1];
                            SetNotKeyPropValue(prop, value, value_str);
                        }

                        propValueList.Clear();
                        propValue_strList.Clear();
                        propValueList = product.sale_props.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        for (int i = 0; i < propValueList.Count; i++)
                        {
                            string pv = propValueList[i];
                            string prop = pv.Split(':')[0];
                            string value = pv.Split(':')[1];
                            SetSalePropPropValue(prop, value, e);
                        }
                    }
                    else
                    {
                        productID = "0";
                    }
                }
            }
        }

        private void GetItemImageCallback(IAsyncResult ar)
        {
            Image picImage = ((ItemImageDelegate)ar.AsyncState).EndInvoke(ar);
            BeginInvoke(new Action(() =>
            {
                if (picImage != null)
                {
                    pictureEditPic.Image = picImage;
                }
            }));
        }

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="picUrl"></param>
        /// <returns></returns>
        public Image GetItemImage(string picUrl)
        {
            if (string.IsNullOrEmpty(picUrl))
            {
                return null;
            }

            StringBuilder dirBuilder = new StringBuilder();
            dirBuilder.Append(Application.StartupPath);
            dirBuilder.Append("\\ProductPics\\");
            string picDirectory = dirBuilder.ToString();
            //先检查是否存在该文件夹，没有则创建。再判断是否存在文件。
            if (!Directory.Exists(picDirectory))
            {
                Directory.CreateDirectory(picDirectory);
            }

            dirBuilder.Append(Path.GetFileName(picUrl));
            string picPath = dirBuilder.ToString();

            if (!File.Exists(picPath))
            {
                //下载图片
                HttpWebRequest request = null;
                HttpWebResponse response = null;
                int count = 0;
                long totalBytes = 0;
                int bufSize = 8192;
                try
                {
                    request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(picUrl);
                    response = (System.Net.HttpWebResponse)request.GetResponse();

                    //文件总长度
                    totalBytes = response.ContentLength;
                    System.IO.Stream stream = response.GetResponseStream();

                    //用于存储全部的数据
                    byte[] bytes = new byte[totalBytes];

                    //用于每次读取
                    byte[] buf = new byte[bufSize];
                    int totalCount = 0;

                    do
                    {
                        //填充数据，count表示读取了多少，并非一次全部读取成功
                        count = stream.Read(buf, 0, buf.Length);

                        //将本次读取的数据放入bytes数组中
                        if (count != 0)
                        {
                            Buffer.BlockCopy(buf, 0, bytes, totalCount, count);
                        }

                        //当前读取到的数据总长度
                        totalCount += count;
                    }
                    //是否还需要继续读取数据，如果count>0表示没有读取完成
                    while (count > 0);

                    // 把 byte[] 写入文件 
                    FileStream fs = new FileStream(picPath, FileMode.Create);

                    //BinaryWriter最后一次将bytes全部写入文件,减少i/o操作
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(bytes);
                    bw.Close();
                    fs.Close();
                    stream.Close();
                    //关闭流
                    response.Close();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }
                finally
                {
                    if (response != null)
                    {
                        response.Close();
                    }

                    //防止线程被阻塞
                    if (request != null)
                    {
                        request.Abort();
                    }
                }
            }

            return SystemHelper.FileToImage(picPath);
        }

        /// <summary>
        /// 清空目前各行显示的值
        /// </summary>
        /// <param name="categoryRow"></param>
        void RowValueClear(CategoryRow categoryRow)
        {
            foreach (EditorRow sRow in categoryRow.ChildRows)
            {
                sRow.ChildRows.Clear();
                sRow.Properties.Value = null;
            }
        }

        /// <summary>
        /// 依照product向销售属性赋值
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool SetSalePropPropValue(string prop, string value, EventArgs e)
        {
            foreach (EditorRow row in categoryRowSaleProps.ChildRows)
            {
                EditorRowTag tag = row.Tag as EditorRowTag;
                string pid = prop;
                string[] vidArray = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (pid == tag.Pid)
                {
                    RepositoryItemCheckedComboBoxEdit ccmb = row.Properties.RowEdit as RepositoryItemCheckedComboBoxEdit;
                    row.Properties.Value = string.Join(ccmb.SeparatorChar.ToString() + " ", vidArray);
                    foreach(string vid in vidArray)
                    {
                        foreach(CheckedListBoxItem clbItem in ccmb.Items)                     
                        {
                            string itemValue=clbItem.Value!=null ?clbItem.Value.ToString():string.Empty;
                            if (itemValue == vid)
                            {
                                clbItem.CheckState = CheckState.Checked;
                            }
                        }                       
                    }                                        
                    ccmb_EditValueChanged(row.Properties.RowEdit, e);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 依照product向非关键属性赋值
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value"></param>
        /// <param name="value_str"></param>
        /// <returns></returns>
        bool SetNotKeyPropValue(string prop,string value, string value_str)
        {
            foreach (EditorRow row in categoryRowNotKeyProps.ChildRows)
            {
                EditorRowTag tag = row.Tag as EditorRowTag;
                string pid=prop;
                if (tag.Pid == pid)
                {
                    if (row.Properties.RowEdit is RepositoryItemComboBox)
                    {
                        row.Properties.Value = value_str;
                        return true;
                    }
                    else if (row.Properties.RowEdit is RepositoryItemCheckedComboBoxEdit)
                    {
                        RepositoryItemCheckedComboBoxEdit ccmb = row.Properties.RowEdit as RepositoryItemCheckedComboBoxEdit;
                        foreach(CheckedListBoxItem item in ccmb.Items)
                        {
                            if (item.Description == value_str)
                            {
                                ccmb.BeginUpdate();
                                row.Properties.Value = string.Join(ccmb.SeparatorChar.ToString() + " ", new string[] { value });                                
                                ccmb.EndUpdate();
                                return true;
                            }
                        }
                    }                    
                }
                else
                {
                    continue;
                }
            }
            return false ;
        }

        /// <summary>
        /// 将属性串按照淘宝的规定排序
        /// </summary>
        /// <param name="propValue"></param>
        void ProductValueSorted(ref string propValue)
        {
            if (string.IsNullOrEmpty(propValue))
            {
                return;
            }
            else
            {
                Hashtable propsTable = new Hashtable();
                //先按照;分割，再按照:分割，同时StringSplitOptions.RemoveEmptyEntries表示去掉空格
                List<string> propsList = propValue.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                foreach (string prop in propsList)
                {
                    string[] propArray = prop.Split(':');
                    if (!propsTable.ContainsKey(propArray[0]))
                    {
                        propsTable.Add(propArray[0], propArray[1]);
                    }
                }
                propValue = string.Empty;
                foreach(object key in propsTable.Keys)
                {
                    propValue += key.ToString() + ":" + propsTable[key].ToString() + ";";
                }
            }
        }

        /// <summary>
        /// 获取关键属性的pidvid串
        /// </summary>
        /// <param name="propValue"></param>
        /// <returns></returns>
        bool GetProductValue(ref string propValue,BaseRow fRow)
        {
            if (fRow.ChildRows.Count == 0)
            {
                return false;
            }
            foreach (EditorRow row in fRow.ChildRows)
            {
                if (row.Properties.Value == null)//若属性值为空，说明没有将关键属性选全，无法获得product，返回false
                {
                    return false;
                }
                else if (row.Properties.Value.ToString().Contains("自定义"))//若是自定义的属性，肯定没product
                {
                    return false;
                }
                else
                {
                    RepositoryItemComboBox cmb = row.Properties.RowEdit as RepositoryItemComboBox;
                    if (cmb != null)
                    {
                        Hashtable table = cmb.Tag as Hashtable;
                        int vid = Convert.ToInt32(table[cmb.Items.IndexOf(row.Properties.Value.ToString())]);
                        propValue += (row.Tag as EditorRowTag).Pid + ":" + vid + ";";
                    }
                }
                if (row.HasChildren)
                {
                    if (GetProductValue(ref propValue,row))
                    {

                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        void ccmb_EditValueChanged(object sender, EventArgs e)
        {
            //销售属性变化，仓库与库位标签为false;
            StockHouseFlag = false;
            StockLayoutFlag = false;

            //初始化选中值，和选中状态
            checkedSalePropCount = 0;
            bool checkedFlag = false;

            //是否是当前列的第一个被选中的值
            bool isFirstValue = true;


            //将saleInfoTable临时存储到tempTable中，以恢复用户已经填好的值
            DataTable tempTable = saleInfoTable.Copy();
            saleInfoTable.Rows.Clear();

            //如果有多列的情况
            DataTable templateTable = null;


            //遍历销售属性
            foreach (BaseRow row in categoryRowSaleProps.ChildRows)
            {
                RepositoryItemCheckedComboBoxEdit ccmb = row.Properties.RowEdit as RepositoryItemCheckedComboBoxEdit;

                //FieldName+第一个vid构成0当前列的绑定字段名,防止用中文caption作fieldName出错
                EditorRowTag tag = row.Tag as EditorRowTag;
                string fieldName = "FieldName" + tag.Vid;
                foreach (CheckedListBoxItem item in ccmb.Items)
                {
                    //如果当前项目被选中
                    if (item.CheckState == System.Windows.Forms.CheckState.Checked)
                    {
                        //计数器累加
                        if (!checkedFlag)
                        {
                            checkedSalePropCount++;
                        }

                        //标记已经被选中
                        checkedFlag = true;

                        //如果被选中则添加column到table
                        if (saleInfoTable.Columns.IndexOf(fieldName) == -1)
                        {
                            //添加列到table
                            saleInfoTable.Columns.Add(fieldName, typeof(string));
                            saleInfoTable.Columns.Add("vid" + fieldName, typeof(string));

                            //添加列到grid控件
                            GridColumn column = new GridColumn();
                            column.Caption = row.Properties.Caption;
                            column.FieldName = fieldName;
                            column.Name = row.Properties.Caption;
                            column.Visible = true;
                            column.VisibleIndex = 0;
                            gvSaleInfo.Columns.Insert(0, column);

                            GridColumn vidColumn = new GridColumn();
                            vidColumn.FieldName = "vid" + fieldName;
                            vidColumn.Name = "vid" + row.Properties.Caption;
                            vidColumn.Visible = false;
                            gvSaleInfo.Columns.Add(vidColumn);

                            /*若允许自定义输入则添加一行*/
                            if (tag.Is_Allow_Alias)
                            {
                                saleInfoTable.Columns.Add("def" + fieldName, typeof(string));
                                GridColumn defColumn = new GridColumn();
                                defColumn.Caption = "自定义" + row.Properties.Caption;
                                defColumn.FieldName = "def" + fieldName;
                                /*若该属性可自定义输入，则将其加入到列表中*/
                                if (!defPropsList.Contains(defColumn.FieldName))
                                {
                                    defPropsList.Add(defColumn.FieldName);
                                }
                                defColumn.Name = "def" + row.Properties.Caption;
                                defColumn.Visible = true;
                                defColumn.VisibleIndex = 1;
                                gvSaleInfo.Columns.Insert(1, defColumn);
                            }
                        }

                        //判断当前是第几个销售属性，如果是第一个销售属性，选中3个，产生3行
                        //如果是第二个销售属性，选中一个则，3行全赋值，选中2个，则拷贝6行，依次类推，构造table
                        //如果是第一个销售属性，系统默认有一行，clone第一行则可以

                        //第一列自己加，以后的列直接复制前面的值
                        if (checkedSalePropCount == 1)
                        {
                            DataRow newRow = saleInfoTable.NewRow();
                            newRow["SkuOuterID"] = string.Empty;
                            newRow["SkuPrice"] = 0.0;
                            newRow["MinSkuPrice"] = 0.0;
                            newRow["MaxSkuPrice"] = 0.0;
                            newRow["CommissionPrice"] = 0.0;
                            newRow["WholeSalePrice"] = 0.0;
                            newRow["LastStockPrice"] = 0.0;
                            newRow["UsingWarnNum"] = false;
                            newRow["HighestNum"] = 0;
                            newRow["LowestNum"] = 0;
                            newRow[fieldName] = item.Description;
                            newRow["vid" + fieldName] = item.Value;
                            saleInfoTable.Rows.Add(newRow);
                        }
                        else
                        {
                            //第一个选中的值，不需要新加行
                            if (isFirstValue)
                            {
                                foreach (DataRow oldRow in saleInfoTable.Rows)
                                {
                                    oldRow.BeginEdit();
                                    oldRow[fieldName] = item.Description;
                                    oldRow["vid" + fieldName] = item.Value;
                                    oldRow.EndEdit();
                                }

                                templateTable = saleInfoTable.Copy();
                                isFirstValue = false;
                            }
                            else
                            {
                                //复制前面所有行
                                foreach (DataRow templateRow in templateTable.Rows)
                                {
                                    DataRow newRow = saleInfoTable.NewRow();
                                    newRow.ItemArray = templateRow.ItemArray;
                                    newRow[fieldName] = item.Description;
                                    newRow["vid" + fieldName] = item.Value;
                                    saleInfoTable.Rows.Add(newRow);
                                }
                            }
                        }
                    }
                }

                //如果有被选中的值,则检查saleInfoTable中是否存在该column，存在则删除该列
                if (!checkedFlag)
                {
                    //从table中移除
                    if (saleInfoTable.Columns.IndexOf(fieldName) != -1)
                    {
                        saleInfoTable.Columns.Remove(fieldName);
                    }

                    if (saleInfoTable.Columns.IndexOf("vid" + fieldName) != -1)
                    {
                        saleInfoTable.Columns.Remove("vid" + fieldName);
                    }

                    if (saleInfoTable.Columns.IndexOf("def" + fieldName) != -1)
                    {
                        saleInfoTable.Columns.Remove("def" + fieldName);
                    }
                    //从控件中移除
                    GridColumn column = gvSaleInfo.Columns.ColumnByFieldName(fieldName);
                    GridColumn defColumn = gvSaleInfo.Columns.ColumnByFieldName("def" + fieldName);
                    GridColumn vidColumn = gvSaleInfo.Columns.ColumnByFieldName("vid" + fieldName);
                    if (column != null)
                    {
                        gvSaleInfo.Columns.Remove(column);
                    }
                    if (vidColumn != null)
                    {
                        gvSaleInfo.Columns.Remove(vidColumn);
                    }
                    if (defColumn != null)
                    {
                        gvSaleInfo.Columns.Remove(defColumn);
                    }
                }

                //重置标志位
                checkedFlag = false;
                isFirstValue = true;
            }

            //1、检查saleInfoTable，至少保证有一条记录，用于不填写销售属性或者没有销售属性的商品
            //2、还原用户已经填好的信息
            gridCtrlSaleInfo.DataSource = saleInfoTable.DefaultView;
            AddSkuOuter_ID();
            gvSaleInfo.BestFitColumns();
        }
        #endregion

        /// <summary>
        /// 验证销售属性是否全输入了
        /// </summary>
        /// <returns></returns>
        private bool ChooseAllSaleProps()
        {
            foreach (EditorRow row in categoryRowSaleProps.ChildRows)
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
            return true;
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
        /// 验证所有必须输入的基本信息是否输入完了
        /// </summary>
        /// <returns></returns>
        private bool IsAllNecessariesInput()
        {
            if(string.IsNullOrEmpty(textEditStockItemName.Text))
            {
                textEditStockItemName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(textEditOutID.Text))
            {
                textEditOutID.Focus();
                return false;
            }
            if(string.IsNullOrEmpty(pceItemCat.Text))
            {
                pceItemCat.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(pceStockCat.Text))
            {
                pceStockCat.Focus();
                return false;
            }
            if(string.IsNullOrEmpty(mruTaxRate.Text))
            {
                mruTaxRate.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(popupUnit.Text))
            {
                popupUnit.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证是否都选择了仓库
        /// </summary>
        /// <returns></returns>
        bool IsStockHouseAllBeChoosed()
        {
            int count=gvSaleInfo.RowCount;
            for (int i = 0; i < count; i++)
            {
                DataRow row = gvSaleInfo.GetDataRow(i);
                if (row[gridColumnNum.FieldName] != null && !string.IsNullOrEmpty(row[gridColumnNum.FieldName].ToString().Trim()))
                {
                    if (row[gridColumnStockHouseCode.FieldName] == null || string.IsNullOrEmpty(row[gridColumnStockHouseCode.FieldName].ToString()))
                    {
                        gvSaleInfo.SelectRow(i);
                        return false;
                    }
                    if (row[gridColumnStockLayoutCode.FieldName] == null || string.IsNullOrEmpty(row[gridColumnStockLayoutCode.FieldName].ToString()))
                    {
                        gvSaleInfo.SelectRow(i);
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 验证products的skuOutId是否唯一
        /// </summary>
        /// <returns></returns>
        private bool IsSkuOutIdOnly()
        {
            List<string> skuOutIdList = new List<string>();
            for (int i = 0; i < gvSaleInfo.RowCount; i++)
            {
                DataRow row = gvSaleInfo.GetDataRow(i);
                if (row["SkuOuterID"] != null)
                {
                    string skuOuterID=row["SkuOuterID"].ToString().Trim();
                    /*若SkuOuterID不为空或空格且skuOutIdList不包含之，即没有重复的输入，则将其加入列表，否则返回false*/
                    if (!string.IsNullOrEmpty(skuOuterID) && !skuOutIdList.Contains(skuOuterID))
                    {
                        skuOutIdList.Add(skuOuterID);
                    }
                    else
                    {
                        gvSaleInfo.SelectRow(i);
                        return false;
                    }
                }
                else
                {
                    gvSaleInfo.SelectRow(i);
                    return false;
                }
            }
            if (StockProductService.SkuOutIdIsOnly(skuOutIdList) == ReturnType.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 读取控件的数据，作为向外界传送数据的方法
        /// </summary>
        /// <param name="stockItem"></param>
        /// <param name="stockProductList"></param>
        public bool GetData(WaitDialogForm waitForm,StockItem stockItem,List<StockProduct> stockProductList,List<StockDetail> sdList,List<StockHouseProduct> shpList)
        {
            //验证所有必须输入的基本信息是否输入完整
            if (!IsAllNecessariesInput())
            {
                waitForm.Hide();
                XtraMessageBox.Show("新建商品缺少必填信息，请补充完整再入库！（带*的为必填信息。）", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            //验证销售属性是否输入完整
            if (ChooseAllSaleProps() == false)
            {
                waitForm.Hide();
                XtraMessageBox.Show("新建商品有销售属性，请将销售属性输入完整再入库！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            //验证必须输入的属性是否输入完整
            if (IsAllNeededPropsInput(vGridCtrl.Rows) == false)
            {
                waitForm.Hide();
                XtraMessageBox.Show("新建商品有必填属性，请将必填属性输入完整再入库！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            /*验证OUT_ID是否重复*/
            string outId = textEditOutID.Text;
            if (StockItemService.GetStockItem(c => c.OuterID == outId).Count != 0)
            {
                waitForm.Hide();
                XtraMessageBox.Show("新建商品的商家编码与库存已有的商品编码相同，商家编码必须唯一，请重输！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
           
            /*验证输入的skuOutId是否重复*/
            if (!IsSkuOutIdOnly())
            {
                waitForm.Hide();
                XtraMessageBox.Show("销售信息中输入的商家编码与库存已有的商品编码相同，商家编码必须唯一，请重输！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            //验证仓库是否都选择了
            if (!IsStockHouseAllBeChoosed())
            {
                waitForm.Hide();
                XtraMessageBox.Show("新建商品填写了数量却没有选择仓库或者库位，请先选择仓库或者库位再入库！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            try
            {
                stockItem.KeyProps = string.Empty;
                stockItem.NotKeyProps = string.Empty;
                stockItem.SaleProps = string.Empty;

                /*属性串赋值*/
                stockItem.Props += UIHelper.GetCategoryRowData(categoryRowKeyProps);
                stockItem.Props += UIHelper.GetCategoryRowData(categoryRowNotKeyProps);
                stockItem.Props += UIHelper.GetCategoryRowData(categoryRowSaleProps);
                /*去掉最后一个分号,注意判断是否为空*/
                if (!string.IsNullOrEmpty(stockItem.Props) && stockItem.Props.Contains(";"))
                {
                    stockItem.Props = stockItem.Props.Substring(0, stockItem.Props.Length - 1);
                }
                else if (stockItem.Props == null)
                {
                    stockItem.Props = string.Empty;
                }

                stockItem.CatName = pceItemCat.Text;
                stockItem.Cid = pceItemCat.Tag.ToString();
                stockItem.Created = DateTime.Now;

                if (categoryRowSaleProps.ChildRows.Count > 0)
                {
                    stockItem.HasSaleProps = true;
                }
                else
                {
                    stockItem.HasSaleProps = false;
                }
                stockItem.StockItemCode = System.Guid.NewGuid().ToString();
                Dictionary<string, string> inputDic = UIHelper.GetCategoryInputRowData(categoryRowKeyProps, categoryRowNotKeyProps);
                if (inputDic.Count > 0 && inputDic.Keys.Contains("pid") && inputDic.Keys.Contains("str"))
                {
                    stockItem.InputPids = inputDic["pid"];
                    stockItem.InputStr = inputDic["str"];
                }
                else
                {
                    stockItem.InputPids = string.Empty;
                    stockItem.InputStr = string.Empty;
                }
                stockItem.IsConsignment = radioGroupIsConsignment.SelectedIndex == 0 ? false : true;
                stockItem.Modified = DateTime.Now;
                stockItem.Name = textEditStockItemName.Text;
                stockItem.OuterID = textEditOutID.Text;
                stockItem.PicUrl = string.Empty;//
                #region 保存图片
                if (pictureEditPic.Image != null)
                {
                    Picture pic = new Picture();
                    pic.OuterID = textEditOutID.Text;
                    pic.PictureCode = Guid.NewGuid().ToString();
                    pic.PictureRemark = string.Empty;
                    pic.PictureTitle = string.Empty;
                    pic.PictureContent = SystemHelper.GetImageBytes(pictureEditPic.Image);
                    PictureService.AddPicture(pic);
                }
                #endregion
                stockItem.ProductID = productID;
                stockItem.SimpleName = textEditSimpleName.Text;
                stockItem.StockCatName = pceStockCat.Text;
                stockItem.StockCheckUrl = mruStockWebSite.Text;
                stockItem.StockCid = pceStockCat.Tag != null ? pceStockCat.Tag.ToString() : string.Empty;
                stockItem.StockItemCode = System.Guid.NewGuid().ToString();
                stockItem.StockItemRemark = memo_ProductIntroduce.Text;
                stockItem.StockItemImgs = string.Empty;//
                stockItem.StockItemDesc = string.Empty;
                stockItem.StockProps = UIHelper.GetCategoryRowData(categoryRowStockProps);
                stockItem.TaxName = mruTaxRate.Text;
                stockItem.Tax = string.Empty;/////
                Hashtable taxtable = mruTaxRate.Tag as Hashtable;
                if (taxtable != null && taxtable.Count > 0)
                {
                    stockItem.Tax = taxtable[mruTaxRate.SelectedIndex].ToString();
                }
                stockItem.Model = textEditModel.Text;//型号
                stockItem.Specification = textEditSpecification.Text;//规格
                stockItem.UnitCode = popupUnit.Tag.ToString();//单位
                /*将product中的props_alias拼接起来存到item中*/
                string itemPropsAlias = string.Empty;

                int TotalQuantity = 0;
                /*一条gvSaleInfo表中的记录对应一条stockProduct记录*/
                for (int i = 0; i < gvSaleInfo.RowCount; i++)
                {
                    StockProduct sp = new StockProduct();
                    DataRow row = gvSaleInfo.GetDataRow(i);
                    sp.AvgStockPrice = row["LastStockPrice"] != null && !string.IsNullOrEmpty(row["LastStockPrice"].ToString()) ? double.Parse(row["LastStockPrice"].ToString()) : 0;//                
                    sp.CommissionPrice = row[gridColumnCommissionPrice.FieldName] != null && !string.IsNullOrEmpty(row[gridColumnCommissionPrice.FieldName].ToString()) ? double.Parse(row[gridColumnCommissionPrice.FieldName].ToString()) : 0;
                    sp.LastStockPrice = sp.AvgStockPrice;
                    sp.MaxSkuPrice = row["MaxSkuPrice"] != null && !string.IsNullOrEmpty(row["MaxSkuPrice"].ToString()) ? double.Parse(row["MaxSkuPrice"].ToString()) : 0;
                    sp.MinSkuPrice = row["MinSkuPrice"] != null && !string.IsNullOrEmpty(row["MinSkuPrice"].ToString()) ? double.Parse(row["MinSkuPrice"].ToString()) : 0;
                    sp.OccupiedQuantity = 0;
                    sp.OuterID = stockItem.OuterID;
                    sp.SkuQuantity = 0;
                    sp.ProductStatus = 0;//
                    sp.SkuOuterID = row["SkuOuterID"].ToString();//
                    sp.SkuPrice = double.Parse(row["SkuPrice"].ToString());
                    float weight = row[gridColumnWeight.FieldName] != null && row[gridColumnWeight.FieldName].ToString() != string.Empty ? float.Parse(row[gridColumnWeight.FieldName].ToString()) : 0;
                    sp.Weight = weight;
                    if (row["Num"] != null && !string.IsNullOrEmpty(row["Num"].ToString()))
                    {
                        /*若输入了数量要记录入库详情*/
                        StockDetail sd = new StockDetail();
                        sd.DetailRemark = string.Empty;
                        sd.DetailType = (int)Alading.Core.Enum.DetailType.OtherIn;//其它入库？
                        sd.DurabilityDate = DateTime.Now;//
                        sd.InOutCode = string.Empty;//
                        sd.Price = float.Parse(sp.SkuPrice.ToString());
                        sd.ProductSkuOuterId = sp.SkuOuterID;
                        sd.Quantity = int.Parse(row["Num"].ToString());

                        sp.SkuQuantity = sd.Quantity;/*数量大于0*/

                        sd.StockDetailCode = System.Guid.NewGuid().ToString();
                        sd.StockHouseCode = row[gridColumnStockHouseCode.FieldName].ToString();
                        sd.StockLayOutCode = row[gridColumnStockLayoutCode.FieldName].ToString();
                        sd.HouseName = row[gridColumnStockHouse.FieldName].ToString();
                        sd.LayoutName = row[gridColumnStockLayout.FieldName].ToString();
                        sd.Tax = stockItem.Tax;
                        sd.TotalFee = sd.Quantity * sd.Price;
                        /*总数量增加*/
                        TotalQuantity += sd.Quantity;
                        sd.SearchText = sd.InOutCode + "-" + sd.StockDetailCode + "-" + sd.ProductSkuOuterId;
                        sdList.Add(sd);

                        /*仓库商品表对应添加一条记录*/
                        StockHouseProduct shp = new StockHouseProduct();
                        shp.HouseCode = row[gridColumnStockHouseCode.FieldName].ToString();
                        shp.HouseProductCode = System.Guid.NewGuid().ToString();
                        shp.LayoutCode = row[gridColumnStockLayoutCode.FieldName].ToString();
                        shp.Num = sp.SkuQuantity;
                        shp.SkuOuterID = sp.SkuOuterID;
                        shp.HouseName = row[gridColumnStockHouse.FieldName].ToString();
                        shp.LayoutName = row[gridColumnStockLayout.FieldName].ToString();
                        shpList.Add(shp);
                    }

                    /*获取每一行的销售属性值*/
                    foreach (EditorRow pRow in categoryRowSaleProps.ChildRows)
                    {
                        EditorRowTag tag = pRow.Tag as EditorRowTag;
                        string fieldName = "FieldName" + tag.Vid;
                        /*如果该属性允许自定义输入，其属性串形式为pid:vid:自定义输入*/
                        if (tag.Is_Allow_Alias)
                        {
                            if (row["def" + fieldName] != null && !string.IsNullOrEmpty(row["def" + fieldName].ToString()))
                            {
                                string propsAlias = tag.Pid + ":" + row["vid" + fieldName] + ":" + row["def" + fieldName] + ";";
                                sp.PropsAlias += propsAlias;
                                if (!itemPropsAlias.Contains(propsAlias))
                                {
                                    itemPropsAlias += propsAlias;
                                }
                            }
                        }
                        sp.SkuProps += tag.Pid + ":" + row["vid" + fieldName] + ";";
                        sp.SkuProps_Str += pRow.Properties.Caption + ":" + row[fieldName].ToString() + ";";
                    }

                    if (!string.IsNullOrEmpty(sp.PropsAlias) && sp.PropsAlias.Contains(";"))
                    {
                        sp.PropsAlias = sp.PropsAlias.Substring(0, sp.PropsAlias.Length - 1);
                    }
                    else if (sp.PropsAlias == null)
                    {
                        sp.PropsAlias = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(sp.SkuProps) && sp.SkuProps.Contains(";"))
                    {
                        sp.SkuProps = sp.SkuProps.Substring(0, sp.SkuProps.Length - 1);
                    }
                    else if (sp.SkuProps == null)
                    {
                        sp.SkuProps = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(sp.SkuProps_Str) && sp.SkuProps.Contains(";"))
                    {
                        sp.SkuProps_Str = sp.SkuProps_Str.Substring(0, sp.SkuProps_Str.Length - 1);
                    }
                    else if (sp.SkuProps_Str == null)
                    {
                        sp.SkuProps_Str = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(sp.SkuProps_Str))
                    {
                        sp.SkuProps_Str = UIHelper.SkuProChange(sp.SkuProps_Str);
                    }
                    sp.StockProductRemark = string.Empty;//
                    sp.WarningCount = 0;//
                    if (Convert.ToBoolean(row["UsingWarnNum"]))
                    {
                        sp.IsUsingWarn = true;
                        int HighestNum = 0;
                        int LowestNum = 0;
                        if (row["HighestNum"] != null && !string.IsNullOrEmpty(row["HighestNum"].ToString()))
                        {
                            HighestNum = int.Parse(row["HighestNum"].ToString());
                        }
                        if (row["LowestNum"] != null && !string.IsNullOrEmpty(row["LowestNum"].ToString()))
                        {
                            LowestNum = int.Parse(row["LowestNum"].ToString());
                        }
                        sp.HighestNum = HighestNum;
                        sp.LowestNum = LowestNum;
                    }
                    else
                    {
                        sp.IsUsingWarn = false;
                        sp.HighestNum = 0;
                        sp.LowestNum = 0;
                    }
                    sp.WholeSalePrice = double.Parse(row["WholeSalePrice"].ToString());
                    stockProductList.Add(sp);
                }
                /*避免stockItem.Property_Alias为空*/
                if (itemPropsAlias.Contains(";"))
                {
                    stockItem.Property_Alias = itemPropsAlias.Substring(0, itemPropsAlias.Length - 1);
                }
                else
                {
                    stockItem.Property_Alias = string.Empty;
                }
                stockItem.TotalQuantity = TotalQuantity;//
                stockItem.StockItemType = comboItemType.SelectedIndex + 1;/*产品类别，要注意下拉items的顺序与枚举类一致*/
                /*搜索字段*/
                stockItem.SearchText = stockItem.Model + "-" + stockItem.SimpleName + "-" + stockItem.Specification + "-" + stockItem.Name + "-" + stockItem.OuterID;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*记住，测试时写上异常捕获能更方便地测试！！！*/

        /// <summary>
        ///添加图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addPictureButton_Click(object sender, EventArgs e)
        {
            //Picture picture = new Picture();
            //this.selectPictureDialog.ShowDialog();
            //string picturePath=this.selectPictureDialog.FileName;

            //// 将由图片转换的二进制组赋予picture表中类型为image的字段
            //picture.PictureContent=SetImageToByteArray(picturePath);

            ////picture表的唯一标识，编码方式待定
            //picture.PictureCode = "000";

            //PictureService.AddPicture(picture);
        }

        /// <summary>
        /// 将图片转为二进制数组
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private  byte[] SetImageToByteArray(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            int imageLength = (int)fs.Length;
            if(imageLength<200*1024)
            {
                byte[] image = new byte[imageLength];
                fs.Read(image, 0, imageLength);
                fs.Close();
                return image;
            }
            else
            {
                return null;
            }
            
        }

        private void tlStockCat_BeforeExpand(object sender, BeforeExpandEventArgs e)
        {
            TreeListNode focusedNode = e.Node;
            tlStockCat.FocusedNode = focusedNode;
            //XtraMessageBox.Show(tlItemCat.IsUnboundMode.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            TreeListNodeTag tag = focusedNode.Tag as TreeListNodeTag;

            #region 获得当前节点的子节点
            if (!tag.HasExpanded)
            {
                tlStockCat.BeginUnboundLoad();
                List<Alading.Entity.StockCat> stockCatList = StockCatService.GetStockCat(i => i.ParentCid == tag.Cid);

                foreach (Alading.Entity.StockCat stockCat in stockCatList)
                {
                    TreeListNode node = tlStockCat.AppendNode(new object[] { stockCat.StockCatName }, focusedNode, new TreeListNodeTag(stockCat.StockCid.ToString()));
                    node.HasChildren = stockCat.IsParent;
                }
                tlStockCat.EndUnboundLoad();
                tag.HasExpanded = true;
            }
            #endregion
        }

        /// <summary>
        /// 选择库存类目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlStockCat_MouseDown(object sender, MouseEventArgs e)
        {
            TreeListHitInfo hitInfo = tlStockCat.CalcHitInfo(new Point(e.X, e.Y));
            /*如果单击到单元格内*/
            if (hitInfo.HitInfoType == HitInfoType.Cell)
            {
                if (!hitInfo.Node.HasChildren)
                {
                    TreeListNodeTag tag = hitInfo.Node.Tag as TreeListNodeTag;
                    this.pceStockCat.Text = hitInfo.Node.GetDisplayText(0);
                    /*绑定CID以便下面读取*/
                    this.pceStockCat.Tag = tag.Cid;
                    LoadStockProps(tag.Cid, categoryRowStockProps);
                    this.pceStockCat.ClosePopup();
                }
            }
        }


        #region 加载库存自定义属性相关

        void LoadStockProps(string stockCid,BaseRow stockPropRow)
        {
            stockPropRow.ChildRows.Clear();
            List<StockProp> stockPropList = allStockPropList.Where(c => c.StockCid == stockCid &&c.ParentPid=="0" && c.ParentVid=="0").ToList();
            List<StockPropValue> stockPropValueList = allStockPropValueList.Where(c => c.StockCid == stockCid).ToList();
            foreach (StockProp stockProp in stockPropList)
            {
                EditorRowTag tag = new EditorRowTag();
                tag.Cid = stockCid;
                tag.Pid = stockProp.StockPid;
                EditorRow row = new EditorRow();
                row.Tag = tag;
                row.Properties.Caption = stockProp.Name;
                List<StockPropValue> list = stockPropValueList.Where(c => c.StockPid == stockProp.StockPid).ToList();
                RepositoryItemComboBox scmb = new RepositoryItemComboBox();
                scmb.SelectedIndexChanged += new EventHandler(scmb_SelectedIndexChanged);
                row.Properties.RowEdit = scmb;
                Hashtable table = new Hashtable();
                int index = 0;
                foreach (StockPropValue spv in list)
                {
                    scmb.Items.Add(spv.Name);
                    table.Add(index++,spv.StockVid);
                }
                scmb.Tag = table;
                stockPropRow.ChildRows.Add(row);
            }
        }

        void scmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit cbe = sender as ComboBoxEdit;
            RepositoryItemComboBox cmb = cbe.Properties as RepositoryItemComboBox;

            //由于SelectedIndexChanged阻止了设置值，为了防止值不显示
            vGridCtrl.FocusedRow.Properties.Value = cbe.Text;
            EditorRow row = vGridCtrl.FocusedRow as EditorRow;
            EditorRowTag tag = vGridCtrl.FocusedRow.Tag as EditorRowTag;
            Hashtable table = cmb.Tag as Hashtable;
            string value=table[cbe.SelectedIndex].ToString();
            StockPropValue spv = allStockPropValueList.FirstOrDefault(c => c.StockCid == tag.Cid && c.StockPid == tag.Pid && c.StockVid == value);
            if (spv != null)
            {
                if (spv.IsParent)
                {
                    List<StockProp> list = allStockPropList.Where(c => c.StockCid == tag.Cid && c.ParentPid == tag.Pid && c.ParentVid == value).ToList();
                    foreach (StockProp sp in list)
                    {
                        EditorRowTag sonTag = new EditorRowTag();
                        sonTag.Cid = sp.StockCid;
                        sonTag.Pid = sp.StockPid;
                        EditorRow sonRow = new EditorRow();
                        sonRow.Tag = sonTag;
                        sonRow.Properties.Caption = sp.Name;
                        List<StockPropValue> valueList = allStockPropValueList.Where(c => c.StockCid == sonTag.Cid && c.StockPid == sp.StockPid).ToList();
                        RepositoryItemComboBox sonCmb = new RepositoryItemComboBox();
                        //scmb.SelectedIndexChanged += new EventHandler(scmb_SelectedIndexChanged);
                        sonRow.Properties.RowEdit = sonCmb;
                        Hashtable sonTable = new Hashtable();
                        int index = 0;
                        foreach (StockPropValue sonSpv in valueList)
                        {
                            sonCmb.Items.Add(sonSpv.Name);
                            sonTable.Add(index++, sonSpv.StockVid);
                        }
                        sonCmb.Tag = sonTable;
                        row.ChildRows.Add(sonRow);
                    }                    
                }
            }
            gvSaleInfo.BestFitColumns();
        }

        #endregion

        private void repositoryItemComboStockHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit combo = sender as ComboBoxEdit;
            gvSaleInfo.BeginUpdate();
            DataRow row = gvSaleInfo.GetFocusedDataRow();
            Hashtable table = repositoryItemComboStockHouse.Tag as Hashtable;
            row[gridColumnStockHouse.FieldName] = combo.Properties.Items[combo.SelectedIndex];           
            row[gridColumnStockHouseCode.FieldName] = table[combo.SelectedIndex];
            row[gridColumnStockLayout.FieldName] = null;
            row[gridColumnStockLayoutCode.FieldName] = null;
            if (!StockHouseFlag)
            {
                int rowCount = gvSaleInfo.RowCount;
                for (int j = 0; j < rowCount; j++)
                {
                    DataRow sRow = gvSaleInfo.GetDataRow(j);
                    sRow[gridColumnStockHouse.FieldName] = row[gridColumnStockHouse.FieldName];
                    sRow[gridColumnStockHouseCode.FieldName] = row[gridColumnStockHouseCode.FieldName];
                }
            }
            StockHouseFlag = true;
            gvSaleInfo.EndUpdate();
            /*加载库位*/
            UIHelper.LoadStockLayout(repositoryItemComboStockLayout, combo, table);
            gvSaleInfo.BestFitColumns();
        }

        private void repositoryItemComboStockLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit combo = sender as ComboBoxEdit;
            gvSaleInfo.BeginUpdate();            
            Hashtable table = repositoryItemComboStockLayout.Tag as Hashtable;
            DataRow row = gvSaleInfo.GetFocusedDataRow();
            row[gridColumnStockLayout.FieldName] = combo.Properties.Items[combo.SelectedIndex];
            row[gridColumnStockLayoutCode.FieldName] = table[combo.SelectedIndex];
            if (!StockLayoutFlag)
            {
                int rowCount = gvSaleInfo.RowCount;
                for (int j = 0; j < rowCount; j++)
                {
                    DataRow sRow = gvSaleInfo.GetDataRow(j);
                    sRow[gridColumnStockLayout.FieldName] = row[gridColumnStockLayout.FieldName];
                    sRow[gridColumnStockLayoutCode.FieldName] = row[gridColumnStockLayoutCode.FieldName];
                }
            }
            StockLayoutFlag = true;
            gvSaleInfo.EndUpdate();
            gvSaleInfo.BestFitColumns();
        }

        /// <summary>
        /// 当决定使用预警量时要做一些更改，如不让修改等
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvSaleInfo_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = gvSaleInfo.GetDataRow(e.RowHandle);
            string rowField = e.Column.FieldName;
            if (e.Column == gridColumnUsingWarnNum)
            {
                gvSaleInfo.BeginDataUpdate();
                row["UsingWarnNum"] = e.Value;
                if (Convert.ToBoolean(e.Value))
                {
                    repositoryItemTextWarmNum.ReadOnly = false;
                }
                else
                {
                    /*若不选，则最高预警量与最低预警量数量归0，并不允许其输入*/
                    repositoryItemTextWarmNum.ReadOnly = true ;
                    row[gridColumnHighestNum.FieldName] = 0;
                    row[gridColumnLowestNum.FieldName] = 0;
                }
                gvSaleInfo.EndDataUpdate();
            }
            else if(e.Column==gridColumnLastStockPrice)
            {
                ChangePrice(e.Column,e.Value,e.RowHandle);
            }
            else if(e.Column==gridColumnMaxSkuPrice)
            {
                ChangePrice(e.Column, e.Value, e.RowHandle);
            }
            else if (e.Column == gridColumnMinSkuPrice)
            {
                ChangePrice(e.Column, e.Value, e.RowHandle);
            }
            else if (e.Column == gridColumnSkuPrice)
            {
                ChangePrice(e.Column, e.Value, e.RowHandle);
            }
            else if (e.Column == gridColumnWholeSalePrice)
            {
                ChangePrice(e.Column, e.Value, e.RowHandle);
            }
            else if (e.Column == gridColumnCommissionPrice)
            {
                ChangePrice(e.Column, e.Value, e.RowHandle);
            }
            else if (defPropsList.Contains(rowField))/*为何不能接收词组输入？？*/
            {
                string dValue = e.Value.ToString();
                string dRowField = rowField.Remove(0, 3);
                string rowValue = row[dRowField].ToString();
                gvSaleInfo.BeginDataUpdate();
                row[rowField] = dValue;
                for (int i = 0; i < gvSaleInfo.RowCount; i++)
                {
                    DataRow dRow = gvSaleInfo.GetDataRow(i);
                    if (dRow[dRowField].ToString() == rowValue)
                    {
                        dRow[rowField] = dValue;
                    }
                }
                gvSaleInfo.EndDataUpdate();
            }
            else if(e.Column==gridColumnWeight)
            {
                int rowCount=gvSaleInfo.RowCount;
                for (int i = 0; i < rowCount; i++)
                {
                    if (i == e.RowHandle)
                    {
                        continue;
                    }
                    DataRow dRow = gvSaleInfo.GetDataRow(i);
                    if (dRow[gridColumnWeight.FieldName] != null &&dRow[gridColumnWeight.FieldName].ToString().Trim()!=string.Empty && double.Parse(dRow[gridColumnWeight.FieldName].ToString()) != 0.0)
                    {
                        return;
                    }
                }
                for (int i = 0; i < rowCount; i++)
                {
                    DataRow dRow = gvSaleInfo.GetDataRow(i);
                    dRow[gridColumnWeight.FieldName] = e.Value;
                }
            }
            gvSaleInfo.BestFitColumns();
        }

        /// <summary>
        /// 价格输入。当所有列的价格均为空时，所有列的价格均接收该输入
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        void ChangePrice(GridColumn column, object value,int rowHandle)
        {
            int rowCount=gvSaleInfo.RowCount;
            for (int i = 0; i < rowCount; i++)
            {
                if (i == rowHandle)
                {
                    continue;
                }
                DataRow row = gvSaleInfo.GetDataRow(i);
                if (row[column.FieldName] != null &&row[gridColumnWeight.FieldName].ToString().Trim()!=string.Empty && double.Parse(row[column.FieldName].ToString())!=0.0)
                {
                    return;
                }
            }
            for (int i = 0; i < rowCount; i++)
            {
                DataRow row = gvSaleInfo.GetDataRow(i);
                row[column.FieldName] = value;
            }
        }

        private void gvSaleInfo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = gvSaleInfo.GetFocusedDataRow();
            if (row != null)
            {
                //若勾中使用预警量项则允许输入，否则不允许输入
                repositoryItemTextWarmNum.ReadOnly = !Convert.ToBoolean(row["UsingWarnNum"]);
                repositoryItemComboStockLayout.Items.Clear();
                if (row[gridColumnStockHouseCode.FieldName] != null)
                {
                    List<StockLayout> list = StockLayoutService.GetStockLayout(c => c.StockHouseCode == row[gridColumnStockHouseCode.FieldName].ToString());
                    Hashtable layoutTable = new Hashtable();
                    int i = 0;
                    foreach (StockLayout sl in list)
                    {
                        repositoryItemComboStockLayout.Items.Add(sl.LayoutName);
                        layoutTable.Add(i++, sl.StockHouseCode);
                    }
                    repositoryItemComboStockLayout.Tag = layoutTable;
                }
            }
            gvSaleInfo.BestFitColumns();
        }
        
        /// <summary>
        /// 验证正则表达式，经典方法
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public bool IsMatch(string str, string pattern)
        {
            if (Regex.IsMatch(str, pattern))
                return true;
            else
                return false;
        }

        //private void textEditOutID_EditValueChanging(object sender, ChangingEventArgs e)
        //{
        //    int count = gvSaleInfo.RowCount;
        //    for (int i = 0; i < count; i++)
        //    {
        //        DataRow row = gvSaleInfo.GetDataRow(i);
        //        //如果skuOuter_ID是以textEditOutID原内容加上“-”为前缀的话，则其前缀也要跟随textEditOutID的内容变化而变化
        //        if (row[gridColumnSkuOuterID.FieldName] != null && !string.IsNullOrEmpty(row[gridColumnSkuOuterID.FieldName].ToString().Trim()))
        //        {
        //            string value = row[gridColumnSkuOuterID.FieldName].ToString().ToUpper();
        //            string oldValue = e.OldValue.ToString().ToUpper();
        //            if (value.Length > oldValue.Length + 1 && value.Substring(0, oldValue.Length + 1) == oldValue + "-")
        //            {
        //                value = e.NewValue + value.Remove(0, e.OldValue.ToString().Length);
        //                row[gridColumnSkuOuterID.FieldName] = value.ToUpper();
        //            }
        //        }
        //    }
        //}

        //private void textEditOutID_Leave(object sender, EventArgs e)
        //{
        //    AddSkuOuter_ID();
        //}

        /// <summary>
        /// 根据textEditOutID的text更新skuOuter_ID为空的行，并避免重复
        /// </summary>
        void AddSkuOuter_ID()
        {
            #region 老代码，不要的
            //int count = gvSaleInfo.RowCount;
            ////用于存储需要更新的行
            //List<int> needToUpdateRowHandleList = new List<int>();
            ////用于存储当前所有长度大过textEditOutID.Text.Length的skuOuter_ID
            //List<string> skuOuter_IDList = new List<string>();
            //for (int i = 0; i < count; i++)
            //{
            //    DataRow row = gvSaleInfo.GetDataRow(i);
            //    //若该行的skuouter_id不为空且去掉空格不为string.empty则继续执行，否则将该序号加入需要更新的列号列表中
            //    if (row[gridColumnSkuOuterID.FieldName] != null && !string.IsNullOrEmpty(row[gridColumnSkuOuterID.FieldName].ToString().Trim()))
            //    {
            //        string skuOuter_ID = row[gridColumnSkuOuterID.FieldName].ToString();
            //        if (skuOuter_ID.Length > textEditOutID.Text.Length)
            //        {
            //            skuOuter_IDList.Add(skuOuter_ID);
            //        }
            //    }
            //    else
            //    {
            //        needToUpdateRowHandleList.Add(i);
            //    }
            //}

            //int index = 1;
            //gvSaleInfo.BeginUpdate();
            //foreach (int i in needToUpdateRowHandleList)
            //{
            //    bool b = true;
            //    DataRow row = gvSaleInfo.GetDataRow(i);
            //    while (b)
            //    {
            //        if (skuOuter_IDList.Contains(textEditOutID.Text + "-" + index.ToString()))
            //        {
            //            index++;
            //        }
            //        else
            //        {
            //            b = false;
            //        }
            //    }
            //    row[gridColumnSkuOuterID.FieldName] = textEditOutID.Text + "-" + index.ToString();
            //    skuOuter_IDList.Add(row[gridColumnSkuOuterID.FieldName].ToString());
            //}
            //gvSaleInfo.EndUpdate();  
            #endregion

            int count = gvSaleInfo.RowCount;
            int index=1;
            if (!string.IsNullOrEmpty(textEditOutID.Text))
            {
                string outerID = textEditOutID.Text.ToUpper();
                for (int i = 0; i < count; i++)
                {
                    DataRow row = gvSaleInfo.GetDataRow(i);
                    row[gridColumnSkuOuterID.FieldName] = outerID + "-" + index.ToString();
                    index++;
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    DataRow row = gvSaleInfo.GetDataRow(i);
                    row[gridColumnSkuOuterID.FieldName] = string.Empty;
                }
            }
            gvSaleInfo.BestFitColumns();
        }

        private void textEditOutID_EditValueChanged(object sender, EventArgs e)
        {
            //动态产生条形码
            Image myimg = Code128Rendering.MakeBarcodeImage(textEditOutID.Text.Trim(), 1, true);
            picBarCodeImage.Image = myimg;

            AddSkuOuter_ID();
        }

        /// <summary>
        /// 新增类目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void pceStockCat_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        //{
        //    //if (e.Button.Index == 1)
        //    //{
        //    //    WaitDialogForm waitForm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
        //    //    waitForm.Show();
        //    //    try
        //    //    {
        //    //        //Alading.Forms.Stock.SettingUp.ModifyStockCat msc = new Alading.Forms.Stock.SettingUp.ModifyStockCat();
        //    //        //waitForm.Close();
        //    //        //msc.ShowDialog();
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        waitForm.Close();
        //    //        XtraMessageBox.Show(ex.ToString(), Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    //    }
        //    //}
        //}

        /// <summary>
        /// 新增税率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mruSaleTaxRate_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                WaitDialogForm waitForm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                waitForm.Show();
                try
                {
                    TaxRateForm trf = new TaxRateForm();
                    waitForm.Close();
                    trf.ShowDialog();                   
                    UIHelper.GetTax(mruTaxRate);
                }
                catch (Exception ex)
                {
                    waitForm.Close();
                    XtraMessageBox.Show(ex.ToString(), Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeUnit_MouseDown(object sender, MouseEventArgs e)
        {
            TreeListHitInfo hitInfo = treeUnit.CalcHitInfo(new Point(e.X,e.Y));

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

        /// <summary>
        /// 新增单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupUnit_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                try
                {
                    NewUnitForm unit = new NewUnitForm();
                    unit.ShowDialog();                                   
                    //刷新
                    InitUnit();                   
                }
                catch (Exception ex)
                {                    
                    XtraMessageBox.Show(ex.ToString(), Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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

        private void vGridCtrl_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {

        }

        private void vGridCtrl_CellValueChanging(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            if (e.Row.ParentRow == categoryRowSaleProps)
            {
                RepositoryItemCheckedComboBoxEdit ccmb = e.Row.Properties.RowEdit as RepositoryItemCheckedComboBoxEdit;
                foreach (CheckedListBoxItem item in ccmb.Items)
                {
                    //如果当前项目被选中
                    string value=e.Value!=null ?e.Value.ToString():string.Empty;
                    if (value.Contains(item.Value.ToString()))
                    {
                        item.CheckState = CheckState.Checked;
                    }
                }
            }
        }
    }
}
