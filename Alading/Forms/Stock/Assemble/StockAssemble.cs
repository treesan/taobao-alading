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
using System.Linq;
using Alading.Taobao;
using Alading.Utils;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.Utils;
using Alading.Core.Code128;
using DevExpress.XtraTreeList;
using Alading.Core.Enum;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using System.Collections;
using DevExpress.XtraTreeList.Columns;
using System.Configuration;
using Alading.Forms.Stock.SettingUp;

namespace Alading.Forms.Stock.Assemble
{
    public partial class StockAssemble : DevExpress.XtraEditors.XtraForm
    {
        #region 全局变量
        /// <summary>
        /// 异步获取图片
        /// </summary>
        /// <param name="outerID"></param>
        /// <returns></returns>
        public delegate Image ItemImageDelegate(string outerID,string assembleCode);
        string taxCode = string.Empty;
        /// <summary>
        /// 组合商品是否还有效
        /// </summary>
        bool IsUse = true;
        /// <summary>
        /// 是否是所有组合商品
        /// </summary>
        bool IsAllAssemble = false;
        /// <summary>
        /// 每页显示条数
        /// </summary>
        int pageSize = int.Parse(ConfigurationManager.AppSettings["StockItemPageSize"]);
        /// <summary>
        /// 总数据量
        /// </summary>
        int totalCount = 0;
        /// <summary>
        /// 总页码数
        /// </summary>
        int totalPages = 0;
        /// <summary>
        /// 当前页码
        /// </summary>
        int curreentPage = 1;
        /// <summary>
        /// 商品是否含有图片
        /// </summary>
        bool hasPicture = false;
        #endregion
        public StockAssemble()
        {
            InitializeComponent();
            //加载计量单位
            InitUnit();
            //加载税率
            InitTax();
        }

        private void gVItemCombine_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRowChange();
        }

        #region 公共方法

        /// <summary>
        /// 加载item所有的属性
        /// </summary>
        /// <param name="item"></param>
        /// <param name="categoryRowKeyProps"></param>
        /// <param name="categoryRowSaleProps"></param>
        /// <param name="categoryRowNotKeyProps"></param>
        /// <param name="categoryRowStockProps"></param>
        public void LoadItemPropValue(View_ShopItem item, CategoryRow categoryRowKeyProps, CategoryRow categoryRowSaleProps, CategoryRow categoryRowNotKeyProps, CategoryRow categoryRowStockProps)
        {
            try
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
                //分隔所有属性，如3032757:21942439;2234738:44627。pid:vid

                if (!string.IsNullOrEmpty(item.props))
                {
                    propsTable = new Hashtable();
                    //先按照;分割，再按照:分割，同时StringSplitOptions.RemoveEmptyEntries表示去掉空格
                    List<string> propsList = item.props.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                    foreach (string prop in propsList)
                    {
                        string[] propArray = prop.Split(':');
                        if (!propsTable.ContainsKey(propArray[0]))
                        {
                            propsTable.Add(propArray[0], new List<string>());
                        }

                        List<string> vids = propsTable[propArray[0]] as List<string>;
                        vids.Add(propArray[1]);
                    }
                }


                Hashtable inputPvsTable = null;
                //关键属性的自定义的名称 如：input_pids：20000,1632501，对应的input_str：FJH,688

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


                Hashtable propertyAliasTable = null;
                //重新命名销售属性,如：1627207:28341:黑色;1627207:3232481:棕色
                if (!string.IsNullOrEmpty(item.property_alias))
                {
                    propertyAliasTable = new Hashtable();
                    List<string> propertyAliasList = item.property_alias.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                    foreach (string propertyAlias in propertyAliasList)
                    {
                        string[] propertyAliasArray = propertyAlias.Split(new Char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        if (propertyAliasArray.Count() == 3)
                        {
                            if (!propertyAliasTable.ContainsKey(propertyAliasArray[0]))
                            {
                                propertyAliasTable.Add(propertyAliasArray[0], new List<PropertyAlias>());
                            }

                            List<PropertyAlias> propertyAliasObjList = propertyAliasTable[propertyAliasArray[0]] as List<PropertyAlias>;
                            PropertyAlias propertyAliasObj = new PropertyAlias();
                            propertyAliasObj.Vid = propertyAliasArray[1];
                            propertyAliasObj.Value = propertyAliasArray[2];
                            propertyAliasObjList.Add(propertyAliasObj);
                        }
                    }
                }

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
                        tag.IsInputProp = ipv.is_input_prop;
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

                        //通过判断is_muti,is_must,is_input....来设置相应的控件，同时通过is_sale等设置CategoryRow
                        if (ipv.multi)
                        {
                            // 非销售属性
                            if (!ipv.is_sale_prop)
                            {
                                RepositoryItemCheckedComboBoxEdit ccmb = new RepositoryItemCheckedComboBoxEdit();
                                row.Properties.RowEdit = ccmb;

                                ////将Item中的自定义的属性名称，把原有的值替换掉，这样显示的就是替换后的值
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
                                }//

                                if (!ipv.is_sale_prop)
                                {
                                    ccmb.EditValueChanged += new EventHandler(ccmb_EditValueChanged); ;
                                }

                            }//if (!ipv.is_sale_prop)
                            else
                            {

                                #region
                                ////销售属性只展示，不能改
                                //if (propsTable != null)
                                //{
                                //    string value = string.Empty;
                                //    //如果属性中包含该pid
                                //    if (propsTable.ContainsKey(g.Key.ToString()))
                                //    {
                                //        List<string> vids = propsTable[g.Key.ToString()] as List<string>;
                                //        List<View_ItemPropValue> propValues = g.Where(v => vids.Contains(v.vid.ToString())).ToList();
                                //        if (propValues != null)
                                //        {
                                //            //SortedList<string, string> vidValueList = new SortedList<string, string>();
                                //            //foreach (View_ItemPropValue propValue in propValues)
                                //            //{
                                //            //    vidValueList.Add(propValue.vid, propValue.name);
                                //            //}

                                //            //分隔符号后面必须加个空格
                                //            //row.Properties.Value = string.Join(ccmb.SeparatorChar.ToString() + " ", vidList.ToArray());
                                //            //给row赋值    

                                //            foreach (View_ItemPropValue propValue in propValues)
                                //            {
                                //                //给自定义赋值
                                //                if (propertyAliasTable != null && propertyAliasTable.ContainsKey(g.Key.ToString()))
                                //                {
                                //                    List<PropertyAlias> propertyAliasObjList = propertyAliasTable[g.Key.ToString()] as List<PropertyAlias>;
                                //                    if (propertyAliasObjList != null)
                                //                    {
                                //                        PropertyAlias propertyAlias = propertyAliasObjList.Where(v => v.Vid == propValue.vid).FirstOrDefault();
                                //                        if (propertyAlias != null)
                                //                        {
                                //                            //给row赋值
                                //                            value += propertyAlias.Value + ",";
                                //                        }
                                //                        else
                                //                        {
                                //                            //给row赋值
                                //                            value += propValue.name + ",";
                                //                        }
                                //                    }
                                //                }
                                //                else
                                //                {
                                //                    //给row赋值
                                //                    value += propValue.name + ",";
                                //                }
                                //            }

                                //        }
                                //        //给row赋值
                                //        row.Properties.Value = value.Trim(',');
                                //    }

                                //}
                                #endregion

                                RepositoryItemCheckedComboBoxEdit ccmb = new RepositoryItemCheckedComboBoxEdit();

                                //销售属性 自定义
                                if (ipv.is_sale_prop && ipv.is_allow_alias)
                                {

                                    RepositoryItemPopupContainerEdit popEdit = new RepositoryItemPopupContainerEdit();
                                    PopupContainerControl popUp = new PopupContainerControl();
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
                                    tree.Columns[1].Caption = "自定义";
                                    tree.Columns[1].FieldName = "自定义";
                                    tree.Columns[1].VisibleIndex = 1;

                                    foreach (View_ItemPropValue value in g)
                                    {
                                        //这里设置的vid为value，所以赋值的时候也需要这个vid才能正常显示
                                        TreeListNode node = tree.AppendNode(new object[] { value.name }, -1, value.vid);
                                    }

                                    tree.Text = ipv.prop_name;
                                    tree.Tag = ipv.pid;
                                    //popEdit.Closed += new ClosedEventHandler(popEdit_Closed);
                                    popUp.Controls.Add(tree);
                                    popEdit.PopupControl = popUp;
                                    row.Properties.RowEdit = popEdit;

                                    //给node打钩
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
                                                //row.Properties.Value = string.Join(ccmb.SeparatorChar.ToString() + " ", vidList.ToArray());
                                                //给row赋值    
                                                string value = string.Empty;
                                                foreach (string vid in vidList)
                                                {
                                                    foreach (TreeListNode node in tree.Nodes)
                                                    {
                                                        if (node.Tag.ToString() == vid)
                                                        {
                                                            node.Checked = true;
                                                            //给自定义赋值
                                                            if (propertyAliasTable != null && propertyAliasTable.ContainsKey(g.Key.ToString()))
                                                            {
                                                                List<PropertyAlias> propertyAliasObjList = propertyAliasTable[g.Key.ToString()] as List<PropertyAlias>;
                                                                if (propertyAliasObjList != null)
                                                                {
                                                                    PropertyAlias propertyAlias = propertyAliasObjList.Where(v => v.Vid == vid).FirstOrDefault();
                                                                    if (propertyAlias != null)
                                                                    {
                                                                        node.SetValue(1, propertyAlias.Value);
                                                                        //给row赋值
                                                                        value += propertyAlias.Value + ",";
                                                                    }
                                                                    else
                                                                    {
                                                                        //给row赋值
                                                                        value += node.GetDisplayText(0) + ",";
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                //给row赋值
                                                                value += node.GetDisplayText(0) + ",";
                                                            }
                                                        }
                                                    }
                                                }
                                                //给row赋值
                                                row.Properties.Value = value.Trim(',');
                                            }
                                        }
                                    }// if (propsTable != null)                            

                                    #region
                                    //}
                                    //else
                                    //{                                
                                    //    row.Properties.RowEdit = ccmb;
                                    //    foreach (View_ItemPropValue value in g)
                                    //    {
                                    //        //这里设置的vid为value，所以赋值的时候也需要这个vid才能正常显示
                                    //        ccmb.Items.Add(value.vid, value.name);
                                    //    }

                                    //    //销售属性选中的时候引发的事件，销售属性一定是多选
                                    //    if (ipv.is_sale_prop)
                                    //    {
                                    //        //ccmb.EditValueChanged += new EventHandler(ccmb_EditValueChanged);
                                    //    }
                                    //    ////将Item中的自定义的属性名称，写在自定义列
                                    //    foreach (View_ItemPropValue value in g)
                                    //    {
                                    //        if (propertyAliasTable != null && propertyAliasTable.ContainsKey(g.Key.ToString()))
                                    //        {
                                    //            List<PropertyAlias> propertyAliasObjList = propertyAliasTable[g.Key.ToString()] as List<PropertyAlias>;
                                    //            if (propertyAliasObjList != null)
                                    //            {
                                    //                PropertyAlias propertyAlias = propertyAliasObjList.Where(v => v.Vid == value.vid.ToString()).FirstOrDefault();
                                    //                if (propertyAlias != null)
                                    //                {
                                    //                    ccmb.Items.Add(value.vid.ToString(), propertyAlias.Value);
                                    //                }
                                    //                else
                                    //                {
                                    //                    ccmb.Items.Add(value.vid.ToString(), value.name);
                                    //                }
                                    //            }
                                    //        }
                                    //        else
                                    //        {
                                    //            //这里设置的vid为value，所以赋值的时候也需要这个vid才能正常显示
                                    //            ccmb.Items.Add(value.vid.ToString(), value.name);
                                    //        }
                                    //    }

                                    //    if (propsTable != null)
                                    //    {
                                    //        //如果属性中包含该pid
                                    //        if (propsTable.ContainsKey(g.Key.ToString()))
                                    //        {
                                    //            List<string> vids = propsTable[g.Key.ToString()] as List<string>;
                                    //            List<View_ItemPropValue> propValues = g.Where(v => vids.Contains(v.vid.ToString())).ToList();
                                    //            if (propValues != null)
                                    //            {
                                    //                List<string> vidList = new List<string>();
                                    //                foreach (View_ItemPropValue propValue in propValues)
                                    //                {
                                    //                    vidList.Add(propValue.vid.ToString());
                                    //                }

                                    //                //分隔符号后面必须加个空格
                                    //                row.Properties.Value = string.Join(ccmb.SeparatorChar.ToString() + " ", vidList.ToArray());
                                    //                foreach (string vid in vidList)
                                    //                {
                                    //                    foreach (CheckedListBoxItem citem in ccmb.Items)
                                    //                    {
                                    //                        if (citem.Value.ToString() == vid)
                                    //                        {
                                    //                            citem.CheckState = CheckState.Checked;
                                    //                        }
                                    //                    }
                                    //                }
                                    //            }
                                    //        }
                                    //    }//

                                    //}
                                    #endregion

                                }//if (ipv.is_sale_prop && ipv.is_allow_alias)
                                else
                                {


                                    row.Properties.RowEdit = ccmb;
                                    foreach (View_ItemPropValue value in g)
                                    {
                                        //这里设置的vid为value，所以赋值的时候也需要这个vid才能正常显示
                                        ccmb.Items.Add(value.vid, value.name);
                                    }

                                    //销售属性选中的时候引发的事件，销售属性一定是多选
                                    if (ipv.is_sale_prop)
                                    {
                                        //ccmb.EditValueChanged += new EventHandler(ccmb_EditValueChanged);
                                    }
                                    ////将Item中的自定义的属性名称，写在自定义列
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
                                    }//




                                }
                            }

                        }//if (ipv.multi)
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

                            if (!ipv.is_sale_prop)
                            {
                                cmb.SelectedIndexChanged += new EventHandler(cmb_SelectedIndexChanged);
                            }

                            //说明有下级
                            if (ipv.is_parent || ipv.is_key_prop)
                            {
                                //控件的tag中存储hashtable，保存当前列表中的vid
                                cmb.SelectedIndexChanged += new EventHandler(cmbParent_SelectedIndexChanged);
                                tag.IsParent = true;

                                //加载值
                                rowList.Add(row);
                            }

                        }

                        if (ipv.is_key_prop)
                        {
                            categoryRowKeyProps.ChildRows.Add(row);
                        }
                        else if (ipv.is_sale_prop)
                        {
                            row.Properties.ReadOnly = true;
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
                }
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
                                if (inputPvsTable != null && propValue.name=="自定义")
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
                            }                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }

        //循环加载数据




        /// <summary>
        /// 多选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ccmb_EditValueChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 单选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            vGridAssembleProps.FocusedRow.Properties.Value = ((DevExpress.XtraEditors.ComboBoxEdit)(sender)).EditValue == null ? string.Empty : ((DevExpress.XtraEditors.ComboBoxEdit)(sender)).EditValue.ToString();
        }

        void cmbParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit cbe = sender as ComboBoxEdit;
            RepositoryItemComboBox cmb = cbe.Properties as RepositoryItemComboBox;

            //由于SelectedIndexChanged阻止了设置值，为了防止值不显示
            vGridAssembleProps.FocusedRow.Properties.Value = cbe.Text;

            //如果值不等于自定义，则加载数据
            EditorRow row = new EditorRow();
            EditorRowTag tag = vGridAssembleProps.FocusedRow.Tag as EditorRowTag;
            string cid = tag.Cid;
            string pid = tag.Pid;
            //row.Properties.Caption = tag.ChildTemplate;
            if (tag.IsMust)
            {
                row.Properties.ImageIndex = 0;
            }

            //删除不需要的row                              
            vGridAssembleProps.FocusedRow.ChildRows.Clear();
            BaseRow delRow = vGridAssembleProps.FocusedRow.ParentRow.ChildRows.GetRowByFieldName(vGridAssembleProps.FocusedRow.Properties.Caption + "自定义");
            if (delRow != null)
            {
                vGridAssembleProps.FocusedRow.ParentRow.ChildRows.Remove(delRow);
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
                    vGridAssembleProps.FocusedRow.ChildRows.Add(row);
                }
            }
            else if (cbe.Text == "自定义")
            {
                //如果是自定义，则在当前焦点行的父行里添加一子行(目前暂时作为其子项添加)
                string caption = vGridAssembleProps.FocusedRow.Properties.Caption + "自定义";
                row.Properties.Caption = caption;
                row.Properties.FieldName = caption;
                vGridAssembleProps.FocusedRow.ChildRows.Clear();
                vGridAssembleProps.FocusedRow.ChildRows.Add(row);/**/
            }            
        }

        /// <summary>
        /// 加载item所有的属性
        /// </summary>
        /// <param name="item"></param>
        /// <param name="categoryRowKeyProps"></param>
        /// <param name="categoryRowSaleProps"></param>
        /// <param name="categoryRowNotKeyProps"></param>
        /// <param name="categoryRowStockProps"></param>
        public void LoadItemPropValueItem(View_ShopItem item, CategoryRow categoryRowKeyProps, CategoryRow categoryRowSaleProps, CategoryRow categoryRowNotKeyProps, CategoryRow categoryRowStockProps)
        {
            try
            {
                if (item == null)//如果不存在,则不予以操作，防止脏数据造成异常
                {
                    return;
                }

                //清除当前的子行
                categoryRowItemSaleProps.ChildRows.Clear();
                categoryRowItemKeyProps.ChildRows.Clear();
                categoryRowItemNotKeyProps.ChildRows.Clear();
                categoryRowItemStockProps.ChildRows.Clear();

                Hashtable propsTable = null;
                //分隔所有属性，如3032757:21942439;2234738:44627。pid:vid

                if (!string.IsNullOrEmpty(item.props))
                {
                    propsTable = new Hashtable();
                    //先按照;分割，再按照:分割，同时StringSplitOptions.RemoveEmptyEntries表示去掉空格
                    List<string> propsList = item.props.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                    foreach (string prop in propsList)
                    {
                        string[] propArray = prop.Split(':');
                        if (propArray.Length == 2)
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


                Hashtable inputPvsTable = null;
                //关键属性的自定义的名称 如：input_pids：20000,1632501，对应的input_str：FJH,688

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


                Hashtable propertyAliasTable = null;
                //重新命名销售属性,如：1627207:28341:黑色;1627207:3232481:棕色
                if (!string.IsNullOrEmpty(item.property_alias))
                {
                    propertyAliasTable = new Hashtable();
                    List<string> propertyAliasList = item.property_alias.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                    foreach (string propertyAlias in propertyAliasList)
                    {
                        string[] propertyAliasArray = propertyAlias.Split(new Char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        if (propertyAliasArray.Count() == 3)
                        {
                            if (!propertyAliasTable.ContainsKey(propertyAliasArray[0]))
                            {
                                propertyAliasTable.Add(propertyAliasArray[0], new List<PropertyAlias>());
                            }

                            List<PropertyAlias> propertyAliasObjList = propertyAliasTable[propertyAliasArray[0]] as List<PropertyAlias>;
                            PropertyAlias propertyAliasObj = new PropertyAlias();
                            propertyAliasObj.Vid = propertyAliasArray[1];
                            propertyAliasObj.Value = propertyAliasArray[2];
                            propertyAliasObjList.Add(propertyAliasObj);
                        }
                    }
                }

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
                        tag.IsInputProp = ipv.is_input_prop;
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

                        //通过判断is_muti,is_must,is_input....来设置相应的控件，同时通过is_sale等设置CategoryRow
                        if (ipv.multi)
                        {
                            RepositoryItemCheckedComboBoxEdit ccmb = new RepositoryItemCheckedComboBoxEdit();
                            row.Properties.RowEdit = ccmb;

                            //将Item中的自定义的属性名称，把原有的值替换掉，这样显示的就是替换后的值
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
                            if (!ipv.is_sale_prop)
                            {
                                ccmb.EditValueChanged += new EventHandler(ccmb_EditValueChanged);
                            }

                        }// if (ipv.multi)
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

                            //值改变的时候触发的事件
                            if (!ipv.is_sale_prop)
                            {
                                cmb.SelectedIndexChanged += new EventHandler(cmb_SelectedIndexChanged);
                            }

                            //说明有下级
                            if (ipv.is_parent)
                            {
                                //控件的tag中存储hashtable，保存当前列表中的vid
                                cmb.SelectedIndexChanged += new EventHandler(cmbParent_SelectedIndexChanged);
                                tag.IsParent = true;

                                //加载值
                                rowList.Add(row);
                            }

                        }

                        if (ipv.is_key_prop)
                        {
                            categoryRowKeyProps.ChildRows.Add(row);
                        }
                        else if (ipv.is_sale_prop)
                        {
                            row.Properties.ReadOnly = true;
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

                }
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
                            }
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
        /// 焦点行变化
        /// </summary>
        private void FocusedRowChange()
        {           
            if (barBtnPropsModify.Enabled == true)
            {
                if (XtraMessageBox.Show("您修改了商品属性,是否保存?", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    SaveModify();
                }
            }
            barBtnSaveModify.Enabled = false;
            barBtnPropsModify.Enabled = false;
            simpleModify.Enabled = false;
            AssembleItem itemAssemble = gVAssembleItem.GetFocusedRow() as AssembleItem;
            if (itemAssemble == null)
            {
                gStockProduct.DataSource = null;
                gridItem.DataSource = null;
                gVStockProduct.BestFitColumns();
                gViewItem.BestFitColumns();
                //清空组件值
                LoadAssembleText(null);
                //清除当前的子行
                categoryRowSaleProps.ChildRows.Clear();
                categoryRowKeyProps.ChildRows.Clear();
                categoryRowNotKeyProps.ChildRows.Clear();
                categoryRowStockProps.ChildRows.Clear();

                //清除当前的子行
                categoryRowItemSaleProps.ChildRows.Clear();
                categoryRowItemKeyProps.ChildRows.Clear();
                categoryRowItemNotKeyProps.ChildRows.Clear();
                categoryRowItemStockProps.ChildRows.Clear();
            }
            else
            {
                WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                try
                {
                    //组合商品未停用,屏蔽启用按钮
                    if (itemAssemble.IsUsage == true)
                    {
                        barBtnUse.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        barBtnDisuse.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        //组合商品已停用,屏蔽停用按钮
                        barBtnUse.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        barBtnDisuse.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    //加载组合商品基本信息
                    LoadAssembleText(itemAssemble);

                    simpleModify.Enabled = false;

                    ////加载组合商品详情
                    //int assRowHandel = gVStockProduct.FocusedRowHandle;
                    //List<View_AssembleProduct> assList = new List<View_AssembleProduct>(ItemService.GetViewAssembleProduct(itemAssemble.AssembleCode));

                    //gStockProduct.DataSource = assList;
                    //if (assRowHandel == 0 && gVStockProduct.FocusedRowHandle > -1)
                    //{
                    //    FocusedRowLoadItemProps();
                    //}

                    ////加载关联宝贝
                    //int itemRowHandle = gViewItem.FocusedRowHandle;
                    //List<Alading.Entity.Item> itemList = ItemService.GetItem(i => i.outer_id == itemAssemble.OuterID && i.IsAsociate==true);
                    //gridItem.DataSource = itemList;                    
                    //if (itemRowHandle == 0 && gViewItem.FocusedRowHandle > -1)
                    //{
                    //    FocusedRowLoadItemProps();
                    //}

                    //加载组合商品属性
                    //if (xtraTabControl1.SelectedTabPage == xtraTabPageAssemble)
                    if (panelContainer1.ActiveChild == dockPanelAssembleProps)
                    {
                        View_ShopItem item = new View_ShopItem();
                        item.cid = itemAssemble.Cid;
                        item.props = itemAssemble.Props;
                        item.property_alias = itemAssemble.SkuProps;
                        if (!string.IsNullOrEmpty(itemAssemble.InputPids))
                        {
                            item.input_pids = itemAssemble.InputPids;
                        }
                        if (!string.IsNullOrEmpty(itemAssemble.InputStr))
                        {
                            item.input_str = itemAssemble.InputStr;
                        }
                        LoadItemPropValue(item, categoryRowKeyProps, categoryRowSaleProps, categoryRowNotKeyProps, categoryRowStockProps);
                    }
                    waitFrm.Close();
                }
                catch (Exception ex)
                {
                    waitFrm.Close();
                    XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
                }
            }
        }


        /// <summary>
        /// 加载商品属性
        /// </summary>
        private void FocusedRowLoadItemProps()
        {
            try
            {
                if (panelContainer1.ActiveChild == dockPanelProps)
                {
                    //清除当前的子行
                    categoryRowItemSaleProps.ChildRows.Clear();
                    categoryRowItemKeyProps.ChildRows.Clear();
                    categoryRowItemNotKeyProps.ChildRows.Clear();
                    categoryRowItemStockProps.ChildRows.Clear();
                    //组合商品详情的商品属性
                    if (xtraTabControl1.SelectedTabPage == xtraTabPageAssemble)
                    {
                        View_AssembleProduct assProduct = gVStockProduct.GetFocusedRow() as View_AssembleProduct;
                        if (assProduct != null)
                        {
                            View_ShopItem item = new View_ShopItem();
                            item.cid = assProduct.Cid;
                            item.props = assProduct.Props;
                            item.input_str = assProduct.InputStr;
                            item.input_pids = assProduct.InputPids;
                            item.property_alias = assProduct.Property_Alias;
                            LoadItemPropValueItem(item, categoryRowItemKeyProps, categoryRowItemSaleProps, categoryRowItemNotKeyProps, categoryRowItemStockProps);
                        }                        
                    }
                    else if (xtraTabControl1.SelectedTabPage == xtraTabPageItem)
                    {//已关联的宝贝的商品属性
                        Alading.Entity.Item item = gViewItem.GetFocusedRow() as Alading.Entity.Item;
                        if (item != null)
                        {
                            View_ShopItem Vitem = new View_ShopItem();
                            Vitem.cid = item.cid;
                            Vitem.props = item.props;
                            Vitem.input_str = item.input_str;
                            Vitem.input_pids = item.input_pids;
                            Vitem.property_alias = item.property_alias;
                            LoadItemPropValueItem(Vitem, categoryRowItemKeyProps, categoryRowItemSaleProps, categoryRowItemNotKeyProps, categoryRowItemStockProps);
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
        /// 加载组合商品基本信息
        /// </summary>
        private void LoadAssembleText(AssembleItem itemAssemble)
        {
            try
            {
                if (itemAssemble != null)
                {
                    textModel.Text = itemAssemble.Model;
                    textName.Text = itemAssemble.Name;
                    textOuterID.Text = itemAssemble.OuterID;
                    textPrice.Text = itemAssemble.Price.ToString();
                    textSimpleName.Text = itemAssemble.SimpleName;
                    textSpecification.Text = itemAssemble.Specification;

                    pceItemCat.Text = itemAssemble.CatName;
                    pceItemCat.Tag = itemAssemble.Cid;

                    popupUnit.Text = itemAssemble.UnitName;
                    popupUnit.Tag = itemAssemble.UnitCode;

                    comboTax.Text = itemAssemble.TaxName;
                    taxCode = itemAssemble.TaxCode;//全局变量

                    ////加载图片
                    ////pictureEditPic.Image=
                    //List<Picture> picList = PictureService.GetPicture(i => i.OuterID == itemAssemble.OuterID);
                    //if (picList != null && picList.Count > 0)
                    //{
                    //    System.IO.MemoryStream stream = new System.IO.MemoryStream(picList.First().PictureContent);
                    //    pictureEditPic.Image = Image.FromStream(stream);
                    //    hasPicture = true;
                    //}
                    //else
                    //{
                    //    pictureEditPic.Image = null;
                    //    hasPicture = false;
                    //}
                    if (!string.IsNullOrEmpty(itemAssemble.OuterID))
                    {
                        //异步加载图片
                        ItemImageDelegate imgDelegate = new ItemImageDelegate(LoadImage);
                        IAsyncResult asyncResult = imgDelegate.BeginInvoke(itemAssemble.OuterID, itemAssemble.AssembleCode, new AsyncCallback(GetItemImageCallback), imgDelegate);
                    }
                    else
                    {
                        pictureEditPic.Image = null;
                        hasPicture = false;
                    }
                    memoDesc.Text = itemAssemble.AssembleDesc;
                }
                else
                {
                    hasPicture = false;
                    pictureEditPic.Image = null;
                    textModel.Text = string.Empty;
                    textName.Text = string.Empty;
                    textOuterID.Text = string.Empty;
                    textPrice.Text = string.Empty;
                    textSimpleName.Text = string.Empty;
                    textSpecification.Text = string.Empty;

                    pceItemCat.Text = string.Empty;
                    pceItemCat.Tag = null;

                    popupUnit.Text = string.Empty;
                    popupUnit.Tag = null;

                    comboTax.Text = string.Empty;
                    memoDesc.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }

        #region 异步加载图片
        private void GetItemImageCallback(IAsyncResult ar)
        {
            Image picImage = ((ItemImageDelegate)ar.AsyncState).EndInvoke(ar);
            BeginInvoke(new Action(() =>
            {
                pictureEditPic.Image = picImage;

            }));
        }
        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="outerID"></param>
        /// <returns></returns>
        private Image LoadImage(string outerID,string assembleCode)
        {
            BeginInvoke(new Action(() =>
            {
                //加载组合商品详情
                int assRowHandel = gVStockProduct.FocusedRowHandle;
                List<View_AssembleProduct> assList = new List<View_AssembleProduct>(ItemService.GetViewAssembleProduct(assembleCode));

                gStockProduct.DataSource = assList;
                if (assRowHandel == 0 && gVStockProduct.FocusedRowHandle > -1)
                {
                    FocusedRowLoadItemProps();
                }

                //加载关联宝贝
                int itemRowHandle = gViewItem.FocusedRowHandle;
                List<Alading.Entity.Item> itemList = ItemService.GetItem(i => i.outer_id == outerID && i.IsAsociate == true);
                gridItem.DataSource = itemList;
                if (itemRowHandle == 0 && gViewItem.FocusedRowHandle > -1)
                {
                    FocusedRowLoadItemProps();
                }

                gVStockProduct.BestFitColumns();
                gViewItem.BestFitColumns();

            }));

            List<Picture> picList = PictureService.GetPicture(i => i.OuterID == outerID);
            if (picList != null && picList.Count > 0)
            {
                System.IO.MemoryStream stream = new System.IO.MemoryStream(picList.First().PictureContent);
                hasPicture = true;
                return Image.FromStream(stream);
            }
            else
            {
                hasPicture = false;
                return null;
            }
        }
        #endregion

        /// <summary>
        /// 初始化页码
        /// </summary>
        private void InitPage()
        {
            repositoryItemComboBox1.SelectedIndexChanged -= repositoryItemComboBox1_SelectedIndexChanged;
            repositoryItemComboBox1.Items.Clear();
            if (totalCount % pageSize == 0)
            {
                totalPages = totalCount / pageSize;
            }
            else
            {
                totalPages = totalCount / pageSize + 1;
            }
            if (totalPages == 0)
            {
                barEditPage.EditValue = "第1页";
                return;
            }
            for (int i = 1; i <= totalPages; i++)
            {
                repositoryItemComboBox1.Items.Add(string.Format("第{0}页", i));
            }
            if (repositoryItemComboBox1.Items.Count > 0)
            {
                barEditPage.EditValue = repositoryItemComboBox1.Items[0];
            }
            repositoryItemComboBox1.SelectedIndexChanged += repositoryItemComboBox1_SelectedIndexChanged;
        }

        /// <summary>
        /// 根据页码把一些按钮不可用
        /// </summary>
        private void pageBtnEnable()
        {
            //若为首页，则上一页和首页不可用
            if (curreentPage == 1)
            {
                barBtnFirst.Enabled = false;
                barBtnFront.Enabled = false;
            }
            else
            {
                barBtnFirst.Enabled = true;
                barBtnFront.Enabled = true;
            }
            //若为尾页，则下一页和尾页不可用
            if (curreentPage == totalPages || totalPages == 0)
            {
                barBtnNext.Enabled = false;
                barBtnLast.Enabled = false;
            }
            else
            {
                barBtnNext.Enabled = true;
                barBtnLast.Enabled = true;
            }
        }


        /// <summary>
        /// 初始化、刷新
        /// </summary>
        private void Init()
        {
            try
            {                
                int rowHandle = gVAssembleItem.FocusedRowHandle;
                List<AssembleItem> itemAssembleList = new List<AssembleItem>();
                if (string.IsNullOrEmpty(textKeyWord.Text))
                {
                    //所有组合商品
                    if (IsAllAssemble == true)
                    {
                        itemAssembleList = ItemService.GetAllAssembleItem(curreentPage, pageSize, out totalCount);
                    }
                    else
                    {
                        itemAssembleList = ItemService.GetAssembleItem(i => i.IsUsage == IsUse, curreentPage, pageSize, out totalCount);
                    }
                }
                else
                {
                    itemAssembleList = ItemService.SearchAssembleItem(textKeyWord.Text, curreentPage, pageSize, out totalCount);
                }
                //根据页码把一些按钮不可用
                pageBtnEnable();

                //修改按钮初始不可用
                barBtnSaveModify.Enabled = false;
                barBtnPropsModify.Enabled = false;
                simpleModify.Enabled = false;

                //gVItemCombine.FocusedRowChanged -= gVItemCombine_FocusedRowChanged;
                gAssembleItem.DataSource = itemAssembleList;                
                if (rowHandle == 0 && gVAssembleItem.FocusedRowHandle > -1)
                {
                    FocusedRowChange();//加载第一行
                }
                //gVItemCombine.FocusedRowChanged += gVItemCombine_FocusedRowChanged;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }
        #endregion


        #region 组合商品列表
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AssembleAdd assembleAdd = new AssembleAdd();
            assembleAdd.ShowDialog();
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {                
                Init();//刷新界面
                InitPage();
                waitFrm.Close();
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnModify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AssembleItem assembleItem = gVAssembleItem.GetFocusedRow() as AssembleItem;
            if (assembleItem != null)
            {
                AssembleAdd assembleAdd = new AssembleAdd(assembleItem);
                assembleAdd.ShowDialog();
                Init();//刷新界面
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            AssembleItem itemAssemble = gVAssembleItem.GetFocusedRow() as AssembleItem;
            if (itemAssemble != null)
            {
                if (XtraMessageBox.Show(string.Format("是否删除组合商品\n{0}?", itemAssemble.Name), "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    try
                    {
                        if (ReturnType.Success == ItemService.RemoveAssembleItem(itemAssemble.AssembleCode))
                        {
                            XtraMessageBox.Show("删除成功", Constants.SYSTEM_PROMPT);
                            Init();//刷新界面
                            InitPage();
                        }
                        else
                        {
                            XtraMessageBox.Show("删除失败", Constants.SYSTEM_PROMPT);
                        }
                        waitFrm.Close();
                    }
                    catch (Exception ex)
                    {
                        waitFrm.Close();
                        XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
                    }
                }
            }
        }
        /// <summary>
        /// 停用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnDisuse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AssembleItem itemAssemble = gVAssembleItem.GetFocusedRow() as AssembleItem;
            if (itemAssemble != null)
            {
                if (XtraMessageBox.Show(string.Format("是否停用组合商品\n{0}?", itemAssemble.Name), "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    itemAssemble.IsUsage = false;
                    itemAssemble.Modified = DateTime.Now;
                    if (ItemService.UpdateAssembleItem(itemAssemble) == ReturnType.Success)
                    {
                        XtraMessageBox.Show("操作成功", Constants.SYSTEM_PROMPT);
                        WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                        Init();//刷新界面
                        InitPage();
                        waitFrm.Close();
                    }
                    else
                    {
                        XtraMessageBox.Show("操作失败", Constants.SYSTEM_PROMPT);
                    }
                }
            }
        }
        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnUse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AssembleItem itemAssemble = gVAssembleItem.GetFocusedRow() as AssembleItem;
            if (itemAssemble != null)
            {
                if (XtraMessageBox.Show(string.Format("是否启用组合商品\n{0}?", itemAssemble.Name), "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    itemAssemble.IsUsage = true;
                    itemAssemble.Modified = DateTime.Now;
                    if (ItemService.UpdateAssembleItem(itemAssemble) == ReturnType.Success)
                    {
                        XtraMessageBox.Show("操作成功", Constants.SYSTEM_PROMPT);
                        WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                        Init();//刷新界面
                        InitPage();
                        waitFrm.Close();
                    }
                    else
                    {
                        XtraMessageBox.Show("操作失败", Constants.SYSTEM_PROMPT);
                    }
                }
            }
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            Init();//刷新
            InitPage();
            waitFrm.Close();
        }
        #endregion

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StockAssemble_Load(object sender, EventArgs e)
        {
            Init();//初始化
            gVAssembleItem.BestFitColumns();            
            InitPage();
        }

        #region NavBar
        /// <summary>
        /// 未停用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarIsUse_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            textKeyWord.Text = string.Empty;
            //组合商品未停用,屏蔽启用按钮
            barBtnUse.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barBtnDisuse.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

            IsAllAssemble = false;
            IsUse = true;
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            Init();
            InitPage();
            waitFrm.Close();
        }
        /// <summary>
        /// 停用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarDisUse_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            textKeyWord.Text = string.Empty;
            //组合商品已停用,屏蔽停用按钮
            barBtnUse.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barBtnDisuse.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            IsAllAssemble = false;
            IsUse = false;
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            Init();
            InitPage();
            waitFrm.Close();
        }
        /// <summary>
        /// 所有
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarAll_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            textKeyWord.Text = string.Empty;
            //组合商品已停用,屏蔽停用按钮
            barBtnUse.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barBtnDisuse.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            IsAllAssemble = true;
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            Init();
            InitPage();
            waitFrm.Close();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        private void Search()
        {
            int rowHandle = gVAssembleItem.FocusedRowHandle;
            if (textKeyWord.Text.Trim() == string.Empty)
            {
                XtraMessageBox.Show("请输入关键词", Constants.SYSTEM_PROMPT);
                return;
            }
            List<AssembleItem> itemAssembleList = new List<AssembleItem>();
            curreentPage = 1;
            //所有组合商品
            //if (IsAllAssemble == true)
            //{
            itemAssembleList = ItemService.SearchAssembleItem(textKeyWord.Text, curreentPage, pageSize, out totalCount);
            //}
            //else
            //{
            //itemAssembleList = ItemService.SearchAssembleItem(false, IsUse, textKeyWord.Text, curreentPage, pageSize, out totalCount);
            //}
            //初始化页码
            InitPage();
            //根据页码把一些按钮不可用
            pageBtnEnable();

            //修改按钮初始不可用
            barBtnSaveModify.Enabled = false;
            barBtnPropsModify.Enabled = false;
            simpleModify.Enabled = false;
            gAssembleItem.DataSource = itemAssembleList;

            if (rowHandle == 0 && gVAssembleItem.FocusedRowHandle > -1)
            {
                FocusedRowChange();//加载第一行              
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
        #endregion
        #region 页码操作


        private void repositoryItemComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int page = ((DevExpress.XtraEditors.ComboBoxEdit)(sender)).SelectedIndex;
            if (page >= 0)
            {
                curreentPage = page + 1;

                Init();
                //根据页码把一些按钮不可用
                pageBtnEnable();
            }
        }

        private void barBtnFirst_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            curreentPage = 1;
            Init();
            //根据页码把一些按钮不可用
            pageBtnEnable();
        }

        private void barBtnFront_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            curreentPage--;
            Init();
            //根据页码把一些按钮不可用
            pageBtnEnable();

        }

        private void barBtnNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            curreentPage++;
            Init();
            //根据页码把一些按钮不可用
            pageBtnEnable();
        }

        private void barBtnLast_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            curreentPage = totalPages;
            Init();
            //根据页码把一些按钮不可用
            pageBtnEnable();
        }
        #endregion

        /// <summary>
        /// 组合商品详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gVStockProduct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRowLoadItemProps();
        }
        /// <summary>
        /// 已关联的宝贝
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gViewItem_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRowLoadItemProps();
        }

        private void panelContainer1_ActiveChildChanged(object sender, DevExpress.XtraBars.Docking.DockPanelEventArgs e)
        {
            if (panelContainer1.ActiveChild == dockPanelAssembleProps)
            {
                //加载组合属性
                FocusedRowChange();
            }
            else if (panelContainer1.ActiveChild == dockPanelProps)
            {
                //加载商品属性
                FocusedRowLoadItemProps();
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPageItem || xtraTabControl1.SelectedTabPage == xtraTabPageAssemble)
            {
                //加载商品属性
                FocusedRowLoadItemProps();
            }
        }

        private void comboTax_SelectedIndexChanged(object sender, EventArgs e)
        {
            simpleModify.Enabled = true;
            if (comboTax.Tag != null)
            {
                string[] codeList = comboTax.Tag.ToString().Split(',');
                if (comboTax.SelectedIndex != -1 && codeList.Length > comboTax.SelectedIndex)
                {
                    taxCode = codeList[comboTax.SelectedIndex];
                }
            }
        }

        #region 保存修改
        private void gVStockProduct_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            barBtnSaveModify.Enabled = true;
            if (e.Column.FieldName == "IsSelected")
            {
                gVStockProduct.SetFocusedRowCellValue("IsSelected", e.Value);
            }
            else if (e.Column.FieldName == "Count")
            {
                gVStockProduct.SetFocusedRowCellValue("Count", e.Value);
            }
        }

        private void vGridAssembleProps_CellValueChanging(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            barBtnPropsModify.Enabled = true;
            e.Row.Properties.Value = e.Value;
        }

        private void textName_EditValueChanged(object sender, EventArgs e)
        {
            simpleModify.Enabled = true;
            textSimpleName.Text = UIHelper.GetChineseSpell(textName.Text);
        }

        private void textOuterID_EditValueChanged(object sender, EventArgs e)
        {
            simpleModify.Enabled = true;
            //动态产生条形码
            Image myimg = Code128Rendering.MakeBarcodeImage(textOuterID.Text.Trim(), 1, true);
            picBarCodeImage.Image = myimg;
        }

        private void textSimpleName_EditValueChanged(object sender, EventArgs e)
        {
            simpleModify.Enabled = true;
        }

        private void textSpecification_EditValueChanged(object sender, EventArgs e)
        {
            simpleModify.Enabled = true;
        }

        private void textModel_EditValueChanged(object sender, EventArgs e)
        {
            simpleModify.Enabled = true;
        }

        private void textPrice_EditValueChanged(object sender, EventArgs e)
        {
            simpleModify.Enabled = true;
        }

        private void popupUnit_EditValueChanged(object sender, EventArgs e)
        {
            simpleModify.Enabled = true;
        }

        private void memoDesc_EditValueChanged(object sender, EventArgs e)
        {
            simpleModify.Enabled = true;
        }
        #endregion

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

        #region 保存修改
        private void simpleModify_Click(object sender, EventArgs e)
        {
            try
            {
                if (gVAssembleItem.RowCount == 0)
                    return;
                if (textName.Text.Trim() == string.Empty || textOuterID.Text.Trim() == string.Empty || pceItemCat.Text.Trim() == string.Empty
                    || textPrice.Text.Trim() == string.Empty || popupUnit.Text.Trim() == string.Empty || comboTax.Text == string.Empty)
                {
                    XtraMessageBox.Show("带*号的为必填项", Constants.SYSTEM_PROMPT);
                    return;
                }
                AssembleItem assembleItem = gVAssembleItem.GetFocusedRow() as AssembleItem;
                if (assembleItem == null)
                    return;

                if (XtraMessageBox.Show("是否保存修改?", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    #region AssembleItem
                    assembleItem.AssembleDesc = memoDesc.Text;
                    //assembleItem.CatName = pceItemCat.Text;
                    //assembleItem.Cid = pceItemCat.Tag == null ? string.Empty : pceItemCat.Tag.ToString();
                    assembleItem.Modified = DateTime.Now;
                    assembleItem.Model = textModel.Text;
                    assembleItem.OuterID = textOuterID.Text;
                    assembleItem.PicUrl = string.Empty;///////////                    

                    assembleItem.Price = textPrice.Text == string.Empty ? 0.0 : double.Parse(textPrice.Text);
                    //assembleItem.Quantity = textQuantity.Text == string.Empty ? 0.0 : double.Parse(textQuantity.Text);
                    assembleItem.SimpleName = textSimpleName.Text;                
                    assembleItem.Specification = textSpecification.Text;
                    assembleItem.TaxName = comboTax.Text;
                    assembleItem.TaxCode = taxCode;
                    int length = popupUnit.Text.Length;
                    int index = popupUnit.Text.LastIndexOf('(');
                    if (index != -1)
                    {
                        assembleItem.UnitName = popupUnit.Text.Substring(0, length - index + 1);
                    }
                    else
                    {
                        assembleItem.UnitName = popupUnit.Text;
                    }
                    assembleItem.UnitCode = popupUnit.Tag == null ? string.Empty : popupUnit.Tag.ToString();

                    #endregion

                    ReturnType type=ItemService.UpdateAssembleItemBase(assembleItem);
                    if (type == ReturnType.Success)
                    {
                        //   #region 保存图片
                        //if (pictureEditPic.Image != null)
                        //{
                        //    Picture pic = new Picture();
                        //    pic.OuterID = assembleItem.OuterID;
                        //    pic.PictureCode = Guid.NewGuid().ToString();
                        //    pic.PictureRemark = string.Empty;
                        //    pic.PictureTitle = string.Empty;
                        //    pic.PictureContent = SystemHelper.GetImageBytes(pictureEditPic.Image);
                        //    PictureService.AddPicture(pic);
                        //}
                        //   #endregion

                        gVAssembleItem.SetFocusedRowCellValue("Price", assembleItem.Price);
                        XtraMessageBox.Show("保存修改成功", Constants.SYSTEM_PROMPT);
                        simpleModify.Enabled = false;
                    }
                    else if (type == ReturnType.PropertyExisted)
                    {
                        XtraMessageBox.Show("商家编码重复,请重输", Constants.SYSTEM_PROMPT);
                        textOuterID.Focus();
                    }
                    else
                    {
                        XtraMessageBox.Show("保存修改失败", Constants.SYSTEM_PROMPT);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }

        /// <summary>
        /// 保存销售信息修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnSaveModify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                AssembleItem assembleItem = gVAssembleItem.GetFocusedRow() as AssembleItem;
                if (assembleItem == null)
                    return;
                List<View_AssembleProduct> assList = gStockProduct.DataSource as List<View_AssembleProduct>;
                if (assList != null && assList.Count > 0)
                {
                    if (XtraMessageBox.Show("是否保存修改?", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        List<AssembleDetail> detailList = new List<AssembleDetail>();
                        foreach (View_AssembleProduct view in assList)
                        {
                            AssembleDetail detail = new AssembleDetail();
                            detail.AssembleCode = assembleItem.AssembleCode;
                            detail.SkuOuterID = view.SkuOuterID;
                            detail.Count = view.Count;
                            detailList.Add(detail);
                        }
                        if (ItemService.UpdateAssembleItemDetails(assembleItem.AssembleCode, detailList) == ReturnType.Success)
                        {
                            XtraMessageBox.Show("保存修改成功", Constants.SYSTEM_PROMPT);
                            barBtnSaveModify.Enabled = false;
                        }
                        else
                        {
                            XtraMessageBox.Show("保存修改失败", Constants.SYSTEM_PROMPT);
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show("请选择商品", Constants.SYSTEM_PROMPT);
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
        /// 保存商品属性
        /// </summary>
        private void SaveModify()
        {
            try
            {
                //#region 验证销售属性
                ////遍历销售属性 
                //bool tag = false;
                //if (categoryRowSaleProps.ChildRows.Count == 2)
                //{
                //    BaseRow fRow = categoryRowSaleProps.ChildRows[0];
                //    RepositoryItemCheckedComboBoxEdit fccmb = fRow.Properties.RowEdit as RepositoryItemCheckedComboBoxEdit;
                //    foreach (CheckedListBoxItem item in fccmb.Items)
                //    {
                //        //如果当前项目被选中
                //        if (item.CheckState == System.Windows.Forms.CheckState.Checked)
                //        {
                //            tag = true;
                //        }
                //    }
                //    bool fTag = tag;
                //    BaseRow sRow = categoryRowSaleProps.ChildRows[1];
                //    RepositoryItemCheckedComboBoxEdit sccmb = sRow.Properties.RowEdit as RepositoryItemCheckedComboBoxEdit;
                //    foreach (CheckedListBoxItem item in sccmb.Items)
                //    {
                //        //如果当前项目被选中
                //        if (item.CheckState == System.Windows.Forms.CheckState.Checked)
                //        {
                //            tag = !fTag;
                //        }
                //    }
                //    if (tag == true)
                //    {
                //        XtraMessageBox.Show("您只选了销售属性中的一项,请全选或全不选", Constants.SYSTEM_PROMPT);
                //        return;
                //    }
                //}
                //#endregion

                //验证必须输入的属性是否输入完整
                if (IsAllNeededPropsInput(vGridAssembleProps.Rows) == false)
                {
                    XtraMessageBox.Show("打钩的为必填属性");
                    return;
                }

                //更新商品属性
                if (XtraMessageBox.Show("是否保存修改?", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    AssembleItem assembleItem = gVAssembleItem.GetFocusedRow() as AssembleItem;
                    if (assembleItem == null)
                        return;

                    string Props = string.Empty;
                    /*属性串赋值*/
                    Props += UIHelper.GetCategoryRowData(categoryRowKeyProps);
                    Props += UIHelper.GetCategoryRowData(categoryRowNotKeyProps);
                    Props += GetCategoryRowData(categoryRowSaleProps);
                    /*去掉最后一个分号,注意判断是否为空*/
                    if (!string.IsNullOrEmpty(Props) && Props.Contains(";"))
                    {
                        Props = Props.Substring(0, Props.Length - 1);
                    }
                    else if (Props == null)
                    {
                        Props = string.Empty;
                    }
                    assembleItem.Props = Props;
                    assembleItem.InputPids = string.Empty;
                    assembleItem.InputStr = string.Empty;
                    
                    Dictionary<string, string> inputDic = UIHelper.GetCategoryInputRowData(categoryRowKeyProps, categoryRowNotKeyProps);
                    if (inputDic.Count > 0 && inputDic.Keys.Contains("pid") && inputDic.Keys.Contains("str"))
                    {
                        assembleItem.InputPids = inputDic["pid"];
                        assembleItem.InputStr = inputDic["str"];
                    }                   

                    if (ItemService.UpdateAssembleItemProps(assembleItem) == ReturnType.Success)
                    {
                        XtraMessageBox.Show("保存修改成功", Constants.SYSTEM_PROMPT);
                        barBtnPropsModify.Enabled = false;
                    }
                    else
                    {
                        XtraMessageBox.Show("保存修改失败", Constants.SYSTEM_PROMPT);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }

        /// <summary>
        /// 将销售属性控件中的属性以pid:vid串的形式返回
        /// </summary>
        /// <param name="fRow"></param>
        /// <returns></returns>
        public string GetCategoryRowData(BaseRow fRow)
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

        /// <summary>
        /// 保存属性修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnPropsModify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveModify();
        }
        #endregion

        /// <summary>
        /// 选择商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gVAssembleItem.RowCount == 0)
                    return;

                barBtnSaveModify.Enabled = true;

                DataTable table = new DataTable();
                ProductSelected ps = new ProductSelected(table, null);
                ps.ShowDialog();

                List<string> skuOuterIDList = new List<string>();
                List<View_AssembleProduct> assList = gStockProduct.DataSource as List<View_AssembleProduct>;
                var q = from i in assList
                        select i.SkuOuterID;
                skuOuterIDList = q.ToList();

                //商品新增           
                foreach (DataRow row in table.Rows)
                {
                    //防止出现同一个商品
                    if (row["SkuOuterID"] == null || skuOuterIDList.Contains(row["SkuOuterID"].ToString()))
                        continue;

                    View_AssembleProduct view = new View_AssembleProduct();
                    view.CatName = row["CatName"].ToString();
                    view.Cid = row["Cid"].ToString();
                    view.Name = row["Name"].ToString();
                    view.SkuOuterID = row["SkuOuterID"].ToString();
                    view.Specification = row["Specification"].ToString();
                    view.Model = row["Model"].ToString();
                    view.StockCatName = row["StockCatName"].ToString();
                    view.CatName = row["CatName"].ToString();
                    view.Count = 1;
                    view.SkuPrice = double.Parse(row["SkuPrice"].ToString());
                    view.StockUnitName = row["StockUnitName"].ToString();
                    view.SaleProps = row["SaleProps"].ToString();
                    view.IsSelected = false;

                    //用于展示所选商品的属性
                    view.Props = row["Props"].ToString();
                    view.InputPids = row["InputPids"].ToString();
                    view.InputStr = row["InputStr"].ToString();
                    view.Property_Alias = row["Property_Alias"].ToString();
                    assList.Add(view);
                }
                int rowHandle = gVStockProduct.FocusedRowHandle;
                gStockProduct.DataSource = assList;
                gVStockProduct.RefreshData();
                if (rowHandle == 0 && gVStockProduct.FocusedRowHandle > -1)
                {
                    FocusedRowLoadItemProps();
                }
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
        private void barBtnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gVStockProduct.RowCount > 0)
                {
                    barBtnSaveModify.Enabled = true;
                    List<View_AssembleProduct> assList = gStockProduct.DataSource as List<View_AssembleProduct>;
                    List<View_AssembleProduct> deleteList = assList.Where(i => i.IsSelected == true).ToList();
                    if (deleteList != null && deleteList.Count > 0)
                    {
                        if (XtraMessageBox.Show("是否删除所有选中项?", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            foreach (View_AssembleProduct view in deleteList)
                            {
                                assList.Remove(view);
                            }
                            gStockProduct.DataSource = assList;
                            gVStockProduct.RefreshData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }

        private void popupUnit_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Index == 1)
                {
                    NewUnitForm unit = new NewUnitForm();
                    unit.ShowDialog();
                    //刷新
                    InitUnit();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }

        private void comboTax_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Index == 1)
                {
                    TaxRateForm rate = new TaxRateForm();
                    rate.ShowDialog();
                    //刷新 
                    InitTax();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }

        /// <summary>
        /// 取消关联
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gViewItem.RowCount > 0)
            {
                Alading.Entity.Item item = gViewItem.GetFocusedRow() as Alading.Entity.Item;
                if (item != null)
                {
                    List<string> iidList=new List<string>();
                    iidList.Add(item.iid);
                    if (ItemService.UpdateItemsAssociate(iidList, false) == ReturnType.Success)
                    {
                        XtraMessageBox.Show("操作成功", Constants.SYSTEM_PROMPT);
                    }
                    else
                    {
                        XtraMessageBox.Show("操作失败", Constants.SYSTEM_PROMPT);
                    }
                }
            }
        }
        #region 处理图片

        private void HandlePic(string outerID)
        {
            if (pictureEditPic.Image == null)
                return;
            //有图片 更新
            if (hasPicture == true)
            {
                if (XtraMessageBox.Show("是否把本图片更新到数据库?", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    //更新图片
                    Picture pic = new Picture();
                    pic.OuterID = outerID;
                    pic.PictureCode = Guid.NewGuid().ToString();
                    pic.PictureRemark = string.Empty;
                    pic.PictureTitle = string.Empty;
                    pic.PictureContent = SystemHelper.GetImageBytes(pictureEditPic.Image);
                    if (PictureService.UpdatePicture(outerID, pic) == ReturnType.Success)
                    {
                        XtraMessageBox.Show("图片更新成功", Constants.SYSTEM_PROMPT);
                    }
                    else
                    {
                        XtraMessageBox.Show("图片更新失败", Constants.SYSTEM_PROMPT);
                    }
                }
            }
            else
            {
                if (XtraMessageBox.Show("是否把本图片添加到数据库?", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    //添加图片
                    Picture pic = new Picture();
                    pic.OuterID = outerID;
                    pic.PictureCode = Guid.NewGuid().ToString();
                    pic.PictureRemark = string.Empty;
                    pic.PictureTitle = string.Empty;
                    pic.PictureContent = SystemHelper.GetImageBytes(pictureEditPic.Image);
                    if (PictureService.AddPicture(pic) == ReturnType.Success)
                    {
                        XtraMessageBox.Show("图片添加成功", Constants.SYSTEM_PROMPT);
                        hasPicture = true;
                    }
                    else
                    {
                        XtraMessageBox.Show("图片添加失败", Constants.SYSTEM_PROMPT);
                    }
                }
            }
        }
        private void buttonPic_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Index == 0)
                {
                    pictureEditPic.LoadImage();
                    return;
                }
                if (e.Button.Index == 1)
                {
                    pictureEditPic.Image = null;
                    return;
                }

                AssembleItem itemAssemble = gVAssembleItem.GetFocusedRow() as AssembleItem;
                if (itemAssemble == null)
                {
                    return;
                }
                //处理图片
                HandlePic(itemAssemble.OuterID);

                //switch (e.Button.Index)
                //{
                //    //case 0:
                //    //    pictureEditPic.LoadImage();
                //    //    break;
                //    //case 1:
                //    //    if (XtraMessageBox.Show("是否从数据库删除本图片?",Constants.SYSTEM_PROMPT,MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
                //    //    {
                //    //        //删除图片
                //    //        //加载图片                            
                //    //        List<Picture> picList = PictureService.GetPicture(i => i.OuterID == itemAssemble.OuterID);
                //    //        if (picList != null && picList.Count > 0)
                //    //        {
                //    //            System.IO.MemoryStream stream = new System.IO.MemoryStream(picList.First().PictureContent);
                //    //            pictureEditPic.Image = Image.FromStream(stream);
                //    //        }
                //    //        else
                //    //        {
                //    //            pictureEditPic.Image = null;
                //    //        }
                //    //    }                        
                //    //    break;
                //    case 2:
                        
                //        break;
                //    case 3:
                        
                //        break;
                //}
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void textKeyWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search();
            }
        }
    }
}