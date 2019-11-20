using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Business;
using Alading.Entity;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.Collections;
using Alading.Taobao.Entity.Extend;
using Alading.Taobao.API;
using System.Data;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Windows.Forms;
using Alading.Core.Enum;
using System.Web;
using Alading.Taobao;

namespace Alading.Utils
{
    /// <summary>
    /// UI界面公共方法
    /// </summary>
    public static class UIHelper
    {
        /// <summary>
        /// 获取单位
        /// </summary>
        /// <param name="ct"></param>
        public static void GetUnit(object ct)
        {
            List<View_GroupUnit> vguList = StockUnitService.GetAllView_GroupUnit();
            Hashtable table = new Hashtable();
            int index = 0;
            if (ct is MRUEdit)
            {
                MRUEdit mruEdit = (MRUEdit)ct;
                foreach (View_GroupUnit vgu in vguList)
                {
                    mruEdit.Properties.Items.Add(vgu.StockUnitName + "(" + vgu.StockUnitGroupName + ")");
                    table.Add(index++, vgu.StockUnitCode);
                }
                mruEdit.Tag = table;
            }
            else if (ct is ComboBoxEdit)
            {
                ComboBoxEdit comboxEdit = (ComboBoxEdit)ct;
                foreach (View_GroupUnit vgu in vguList)
                {
                    comboxEdit.Properties.Items.Add(vgu.StockUnitName + "(" + vgu.StockUnitGroupName + ")");
                    table.Add(index++, vgu.StockUnitCode);
                }
                comboxEdit.Tag = table;
            }
            else if (ct is RepositoryItemComboBox)
            {
                RepositoryItemComboBox ricb = (RepositoryItemComboBox)ct;
                foreach (View_GroupUnit vgu in vguList)
                {
                    ricb.Items.Add(vgu.StockUnitName + "(" + vgu.StockUnitGroupName + ")");
                    table.Add(index++, vgu.StockUnitCode);
                }
                ricb.Tag = table;
            }
        }

        /// <summary>
        /// 获取税率
        /// </summary>
        /// <param name="obj"></param>
        public static void GetTax(object ct)
        {
            List<Tax> taxList = StockUnitService.GetAllTax();
            Hashtable table = new Hashtable();
            int index = 0;
            if (ct is MRUEdit)
            {
                MRUEdit mruEdit = (MRUEdit)ct;
                foreach (Tax tax in taxList)
                {
                    mruEdit.Properties.Items.Add(tax.TaxName);
                    table.Add(index++, tax.TaxCode);
                }
                mruEdit.Tag = table;
            }
            else if (ct is ComboBoxEdit)
            {
                ComboBoxEdit comboxEdit = (ComboBoxEdit)ct;
                foreach (Tax tax in taxList)
                {
                    comboxEdit.Properties.Items.Add(tax.TaxName);
                    table.Add(index++, tax.TaxCode);
                }
                comboxEdit.Tag = table;
            }
            else if (ct is RepositoryItemComboBox)
            {
                RepositoryItemComboBox ricb = (RepositoryItemComboBox)ct;
                foreach (Tax tax in taxList)
                {
                    ricb.Items.Add(tax.TaxName);
                    table.Add(index++, tax.TaxCode);
                }
                ricb.Tag = table;
            }
        }

        /// <summary>
        /// 加载仓库
        /// </summary>
        /// <param name="repositoryItemComboStockHouse"></param>
        public static void LoadStockHouse(RepositoryItemComboBox repositoryItemComboStockHouse)
        {
            List<Alading.Entity.StockHouse> shList = StockHouseService.GetAllStockHouse();
            Hashtable table = new Hashtable();
            int i = 0;
            foreach (Alading.Entity.StockHouse sh in shList)
            {
                repositoryItemComboStockHouse.Items.Add(sh.HouseName);
                table.Add(i++, sh.StockHouseCode);
            }
            repositoryItemComboStockHouse.Tag = table;
        }

        /// <summary>
        /// 加载库位
        /// </summary>
        /// <param name="repositoryItemComboStockLayout"></param>
        /// <param name="combo"></param>
        /// <param name="table"></param>
        public static void LoadStockLayout(RepositoryItemComboBox repositoryItemComboStockLayout, ComboBoxEdit combo,Hashtable table)
        {
            repositoryItemComboStockLayout.Items.Clear();
            List<StockLayout> list = StockLayoutService.GetStockLayout(c => c.StockHouseCode == table[combo.SelectedIndex].ToString());
            Hashtable layoutTable = new Hashtable();
            int i = 0;
            foreach (StockLayout sl in list)
            {
                repositoryItemComboStockLayout.Items.Add(sl.LayoutName);
                layoutTable.Add(i++, sl.StockLayoutCode);
            }
            repositoryItemComboStockLayout.Tag = layoutTable;
        }

        /// <summary>
        ///  加载ItemCat到TreeList
        /// </summary>
        /// <param name="tlItemCat"></param>
        public static void LoadItemCat(DevExpress.XtraTreeList.TreeList tlItemCat)
        {
            tlItemCat.BeginUnboundLoad();
            List<ItemCat> itemCatList = ItemCatService.GetItemCat("0", "normal");

            foreach (ItemCat itemCat in itemCatList)
            {
                TreeListNode node = tlItemCat.AppendNode(new object[] { itemCat.name }, null, new TreeListNodeTag(itemCat.cid.ToString()));
                //设置是否有子节点，有则会显示一个+号
                node.HasChildren = itemCat.is_parent;
            }
            tlItemCat.EndUnboundLoad();
        }

        /// <summary>
        /// 根据item属性返回其关键属性值,
        /// propValueList是当前类目cid下的所有属性及属性值
        /// </summary>
        /// <param name="bRow"></param>
        /// <returns></returns>
        public static string GetKeyPropRowValue(View_ShopItem item,List<View_ItemPropValue> propValueList)
        {            
            string returnProps = string.Empty;
            //分隔所有属性，如3032757:21942439;2234738:44627。pid:vid

            if (!string.IsNullOrEmpty(item.input_pids) && !string.IsNullOrEmpty(item.input_str))
            {
                string[] inputPidsArray = item.input_pids.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] inputStrArray = item.input_str.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                //保证两个值的数组长度一致，不出现索引错误
                if (inputPidsArray.Count() == inputStrArray.Count())
                {
                    for (int i = 0; i < inputPidsArray.Count(); i++)
                    {
                        View_ItemPropValue vipv = propValueList.FirstOrDefault(c => c.pid == inputPidsArray[i] && c.is_key_prop == true);
                        if (vipv != null)
                        {
                            returnProps += vipv.prop_name + ":" + inputStrArray[i] + ";";
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(item.props))
            {
                //先按照;分割，再按照:分割，同时StringSplitOptions.RemoveEmptyEntries表示去掉空格
                List<string> propsList = item.props.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                foreach (string prop in propsList)
                {
                    string[] propArray = prop.Split(':');
                    if (propArray.Length==2)
                    {
                        View_ItemPropValue vipv = propValueList.FirstOrDefault(c => c.pid == propArray[0]&&c.vid== propArray[1] &&c.is_key_prop);
                        if (vipv != null && !returnProps.Contains(vipv.prop_name) )
                        {
                            returnProps += vipv.prop_name + ":" + vipv.name + ";";
                        }
                    }
                   
                }
            }

            return returnProps;
        }

        /// <summary>
        /// 将控件中的属性以pid:vid串的形式返回
        /// </summary>
        /// <param name="fRow"></param>
        /// <returns></returns>
        public static string GetCategoryRowData(BaseRow fRow)
        {
            string props = string.Empty;
            foreach (EditorRow row in fRow.ChildRows)
            {
                EditorRowTag tag = row.Tag as EditorRowTag;
                if (row.Properties.RowEdit is RepositoryItemCheckedComboBoxEdit)
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
                else if (row.Properties.RowEdit is RepositoryItemComboBox)
                {
                    RepositoryItemComboBox cmb = row.Properties.RowEdit as RepositoryItemComboBox;
                    Hashtable table = cmb.Tag as Hashtable;
                    if (table != null)
                    {
                        int index = cmb.Items.IndexOf(row.Properties.Value);
                        /*table第一项可能是空*/
                        if (index != -1 && table[index]!=null && !string.IsNullOrEmpty(table[index].ToString()))
                        {
                            props += tag.Pid.ToString() + ":" + table[index] + ";";
                        }
                    }
                    /*若有下一级，则*/
                    if (row.HasChildren)
                    {
                        props += GetCategoryRowData(row);
                    }
                }
            }
            return props;
        }

        /// <summary>
        /// 将inputpid及intputStr用一个Hashtable返回，key为“pid”的对应input_pid,key为“str”的对应intput_str
        /// 其中返回值已经去掉最后一个逗号
        /// </summary>
        /// <param name="fRow"></param>
        /// <returns></returns>
        public static Dictionary<string,string> GetCategoryInputRowData(CategoryRow keyPropRow, CategoryRow notKeyPropRow)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string pid = string.Empty;
            string str = string.Empty;
            foreach (EditorRow row in keyPropRow.ChildRows)
            {
                //EditorRowTag tag=row.Tag as EditorRowTag;
                //if (row.Properties.Value != null && row.Properties.Value.ToString() == "自定义" && row.HasChildren)
                //{
                //    pid += tag.Pid + ",";
                //    string rowChildRowsPropertiesValue = row.ChildRows[0].Properties.Value==null?string.Empty:row.ChildRows[0].Properties.Value.ToString().Trim();
                //    str +=rowChildRowsPropertiesValue+",";
                //}
                BaseRow eRow=row;
                while (eRow.ChildRows.Count > 0)
                {
                    EditorRowTag eTag = eRow.Tag as EditorRowTag;
                    if (eRow.Properties.Value != null && eRow.Properties.Value.ToString() == "自定义" && eRow.HasChildren)
                    {
                        pid += eTag.Pid + ",";
                        string rowChildRowsPropertiesValue = eRow.ChildRows[0].Properties.Value == null ? string.Empty : eRow.ChildRows[0].Properties.Value.ToString().Trim();
                        str += rowChildRowsPropertiesValue + ",";
                    }
                    eRow = eRow.ChildRows[0];
                }
            }
            foreach (EditorRow row in notKeyPropRow.ChildRows)
            {
                //EditorRowTag tag = row.Tag as EditorRowTag;
                //if (tag.IsInputProp)
                //{
                //    if (row.Properties.Value != null && !string.IsNullOrEmpty(row.Properties.Value.ToString().Trim()))
                //    {
                //        pid += tag.Pid + ",";
                //        str += row.Properties.Value.ToString().Trim() + ",";
                //    }
                //}
                BaseRow eRow = row;
                while (eRow.ChildRows.Count > 0)
                {
                    EditorRowTag eTag = eRow.Tag as EditorRowTag;
                    if (eRow.Properties.Value != null && eRow.Properties.Value.ToString() == "自定义" && eRow.HasChildren)
                    {
                        pid += eTag.Pid + ",";
                        string rowChildRowsPropertiesValue = eRow.ChildRows[0].Properties.Value == null ? string.Empty : eRow.ChildRows[0].Properties.Value.ToString().Trim();
                        str += rowChildRowsPropertiesValue + ",";
                    }
                    eRow = eRow.ChildRows[0];
                }
            }
            pid=pid.TrimEnd(',');
            str=str.TrimEnd(',');
            if (!string.IsNullOrEmpty(pid))
            {
                dic.Add("pid", pid);
                dic.Add("str", str);
            }
            return dic;
        }

        /// <summary>
        /// 加载StockCat到TreeList
        /// </summary>
        /// <param name="tlStockCat"></param>
        public static void LoadStockCat(DevExpress.XtraTreeList.TreeList tlStockCat)
        {
            tlStockCat.BeginUnboundLoad();
            List<StockCat> stockCatList = StockCatService.GetStockCat(i => i.ParentCid == "0");

            foreach (StockCat stockCat in stockCatList)
            {
                TreeListNode node = tlStockCat.AppendNode(new object[] { stockCat.StockCatName }, null, new TreeListNodeTag(stockCat.StockCid.ToString()));
                //设置是否有子节点，有则会显示一个+号
                node.HasChildren = stockCat.IsParent;
            }
            tlStockCat.EndUnboundLoad();
        }
        
        /// <summary>
        /// 扩展方法，判定是否是偶数
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsOdd(this int n)
        { 
            return Convert.ToBoolean(n % 2);
        }

        /// <summary>
        /// 显示item所有的属性，不做修改
        /// </summary>
        /// <param name="item"></param>
        /// <param name="categoryRowKeyProps"></param>
        /// <param name="categoryRowSaleProps"></param>
        /// <param name="categoryRowNotKeyProps"></param>
        /// <param name="categoryRowStockProps"></param>
        public static void ShowItemPropValue(ShopItem item, CategoryRow categoryRowKeyProps, CategoryRow categoryRowSaleProps, CategoryRow categoryRowNotKeyProps, CategoryRow categoryRowStockProps)
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

            //sku的信息
            Hashtable skusTable = null;
            if (!string.IsNullOrEmpty(item.SkuProps))
            {
                skusTable = new Hashtable();
                //先按照;分割，再按照:分割，同时StringSplitOptions.RemoveEmptyEntries表示去掉空格
                List<string> skuList = item.SkuProps.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                foreach (string prop in skuList)
                {
                    string[] propArray = prop.Split(':');
                    if (propArray.Length == 2)
                    {
                        if (!skusTable.ContainsKey(propArray[0]))
                        {
                            skusTable.Add(propArray[0], propArray[1]);
                        }
                    }
                }
            }

            Hashtable skusStrTable = null;
            if (!string.IsNullOrEmpty(item.SkuProps_Str))
            {
                skusStrTable = new Hashtable();
                //先按照;分割，再按照:分割，同时StringSplitOptions.RemoveEmptyEntries表示去掉空格
                List<string> skuList = item.SkuProps_Str.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                foreach (string prop in skuList)
                {
                    string[] propArray = prop.Split(':');
                    if (propArray.Length == 2)
                    {
                        if (!skusStrTable.ContainsKey(propArray[0]))
                        {
                            skusStrTable.Add(propArray[0], propArray[1]);
                        }
                    }
                }
            }

            //用存储过程获取所有cid对应的属性值对
            List<View_ItemPropValue> propValueList = ItemPropValueService.GetView_ItemPropValueList(item.cid, "-1", "-1");

            //按照pid分组绑定
            List<IGrouping<string, View_ItemPropValue>> propValueGroup = propValueList.GroupBy(i => i.pid).ToList();

            //遍历每一组
            foreach (IGrouping<string, View_ItemPropValue> g in propValueGroup)
            {
                View_ItemPropValue ipv = g.First();
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
                            if (!ipv.is_sale_prop)
                            {
                                row.Properties.RowEdit = ccmb;
                            }

                            //将Item中的自定义的属性名称，把原有的值替换掉，这样显示的就是替换后的值
                            foreach (View_ItemPropValue value in g)
                            {
                                if (!ipv.is_sale_prop)
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

                                        //如果是销售属性，则不全部勾中，只要当前的商品的属性 1627207:3232484;21921:33263
                                        //如只有颜色和尺码
                                        if (ipv.is_sale_prop)
                                        {
                                            if (skusStrTable != null)
                                            {
                                                if (skusStrTable.Contains(row.Properties.Caption))
                                                {
                                                    row.Properties.Value = skusStrTable[row.Properties.Caption];
                                                }
                                            }
                                            else if (skusTable != null)
                                            {

                                            }
                                        }
                                        else
                                        {
                                            foreach (View_ItemPropValue propValue in propValues)
                                            {
                                                vidList.Add(propValue.vid.ToString());
                                            }
                                        }

                                        if (!ipv.is_sale_prop)
                                        {
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
                            }     
                    }
                    else
                    {
                        RepositoryItemComboBox cmb = new RepositoryItemComboBox();
                        row.Properties.RowEdit = cmb;
                        cmb.Name = ipv.pid.ToString();
                        //给控件绑定tag
                        Hashtable table = new Hashtable();
                        int index = 0;
                        /*如果该属性不是必须的且不是可输入的属性，则添加一行空行。*/
                        if (!ipv.must && !ipv.is_input_prop)
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
                    }

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

        }

        /// <summary>
        /// 加载item所有的属性
        /// </summary>
        /// <param name="item"></param>
        /// <param name="categoryRowKeyProps"></param>
        /// <param name="categoryRowSaleProps"></param>
        /// <param name="categoryRowNotKeyProps"></param>
        /// <param name="categoryRowStockProps"></param>
        public static void LoadItemPropValue(View_ShopItem item, CategoryRow categoryRowKeyProps, CategoryRow categoryRowSaleProps, CategoryRow categoryRowNotKeyProps, CategoryRow categoryRowStockProps)
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
                View_ItemPropValue ipv = g.FirstOrDefault(c=>c.is_parent);
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
                    }
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
                    }

                    if (ipv.is_parent)
                    {
                        rowList.Add(row);
                        /*拷代码要在这里注册事件*/
                    }
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

        /// <summary>
        /// 重新命名销售属性
        /// </summary>
        /// <param name="pidVid"></param>
        /// <returns></returns>
        public static string GetSalePidVidValue(string cid,string property_alias)
        {            
            string valueStr = string.Empty;
            
            //property_alias = "1627207:28341:黑色;1627207:3232481:棕色";
            //重新命名销售属性,如：1627207:28341:黑色;1627207:3232481:棕色
            if (!string.IsNullOrEmpty(property_alias))
            {               
                if (!string.IsNullOrEmpty(property_alias))
                {
                    List<string> propertyAliasList = property_alias.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (string propertyAlias in propertyAliasList)
                    {
                        string[] propertyAliasArray = propertyAlias.Split(new Char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        if (propertyAliasArray.Count() == 3)
                        {
                            int newCid=int.Parse(cid);
                            int pid = int.Parse(propertyAliasArray[0]);
                            //ItemProp item = ItemPropService.GetItemProp(i => i.cid == newCid).FirstOrDefault(i=>i.pid == pid);
                            ItemProp item = ItemPropService.GetItemProp(cid, pid.ToString());
                            if (item != null)
                                valueStr += string.Format("{0}:{1};", item.name, propertyAliasArray[2]);
                        }
                    }
                }
            }
            return valueStr.Trim(';');
        }

        /// <summary>
        /// 返回pidvid串的值,如:20000:21422 => 品牌:佳能
        /// </summary>
        /// <param name="pidVid"></param>
        /// <returns></returns>
        public static string GetKeyPidVidValue(string cid, string pidvids)
        {
            string valueStr = string.Empty;
            //cid = "1403";
            //pidvids = "20000:21422;20955:130863";
            //重新命名销售属性,如：20000:21422 => 品牌:佳能，20955:130863=>显示屏尺寸:2.6
            if (!string.IsNullOrEmpty(pidvids))
            {
                if (!string.IsNullOrEmpty(pidvids))
                {
                    List<string> propertyAliasList = pidvids.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (string propertyAlias in propertyAliasList)
                    {
                        string[] propertyAliasArray = propertyAlias.Split(new Char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        if (propertyAliasArray.Count() == 2)
                        {
                            ItemPropValue itemPropValue = ItemPropValueService.GetItemPropValue(cid, propertyAliasArray[0], propertyAliasArray[1]);
                            if (itemPropValue != null)
                                valueStr += string.Format("{0}:{1};", itemPropValue.prop_name, itemPropValue.name);
                        }
                    }
                }
            }
            return valueStr.Trim(';');
        }

        /// <summary>
        /// 将淘宝获取的Item值赋值到数据库Item
        /// </summary>
        public static void ItemCopyData(Entity.Item item, Taobao.Entity.Item topItem)
        {
            if (topItem==null)
            {
                throw new NoNullAllowedException();
            }
            item.approve_status = topItem.ApproveStatus ?? string.Empty;
            item.auction_point = topItem.AuctionPoint;
            item.cid = topItem.Cid;
            item.delist_time = topItem.DelistTime == null ? string.Empty : topItem.DelistTime;
            item.desc = topItem.Desc == null ? string.Empty : HttpUtility.HtmlEncode(topItem.Desc);
            item.detail_url = topItem.DetailUrl == null ? string.Empty : topItem.DetailUrl;
            item.ems_fee = topItem.EmsFee == null ? string.Empty : topItem.EmsFee;
            item.express_fee = topItem.ExpressFee == null ? string.Empty : topItem.ExpressFee;
            item.freight_payer = topItem.FreightPayer == null ? string.Empty : topItem.FreightPayer;
            item.has_discount = topItem.HasDiscount;
            item.has_invoice = topItem.HasInvoice;
            item.has_showcase = topItem.HasShowcase;
            item.has_warranty = topItem.HasWarranty;
            item.iid = topItem.Iid ?? string.Empty;
            item.increment = topItem.Increment ?? string.Empty;
            item.input_pids = topItem.InputPids == null ? string.Empty : topItem.InputPids;
            item.input_str = topItem.InputStrs == null ? string.Empty : topItem.InputStrs;
            item.is_3D = topItem.Is3D;
            item.is_ex = topItem.IsExternal;
            item.is_taobao = topItem.IsTaobao;
            item.is_virtual = topItem.IsVirtural;
            item.item_imgs = topItem.ItemImgList == null ? string.Empty : JsonConvert.SerializeObject(topItem.ItemImgList);
            item.list_time = topItem.ListTime == null ? string.Empty : topItem.ListTime;
            item.location_address = topItem.Location.Address == null ? string.Empty : topItem.Location.Address;
            item.location_city = topItem.Location.City == null ? string.Empty : topItem.Location.City;
            item.location_country = topItem.Location.Country == null ? string.Empty : topItem.Location.Country;
            item.location_district = topItem.Location.District == null ? string.Empty : topItem.Location.District;
            item.location_state = topItem.Location.State == null ? string.Empty : topItem.Location.State;
            item.location_zip = topItem.Location.Zip == null ? string.Empty : topItem.Location.Zip;
            item.modified = topItem.Modified == null ? string.Empty : topItem.Modified;
            item.nick = topItem.Nick == null ? string.Empty : topItem.Nick;
            item.num = topItem.Num;
            item.num_iid = topItem.NumIid == null ? string.Empty : topItem.NumIid;
            item.one_station = topItem.OneStation;
            item.outer_id = topItem.OuterId == null ? string.Empty : topItem.OuterId;
            item.pic_url = topItem.PicUrl == null ? string.Empty : topItem.PicUrl;
            item.post_fee = topItem.PostFee == null ? string.Empty : topItem.PostFee;
            item.postage_id = topItem.PostageId == null ? string.Empty : topItem.PostageId;
            item.price = topItem.Price == null ? string.Empty : topItem.Price;
            item.product_id = topItem.ProductId == null ? string.Empty : topItem.ProductId;
            item.prop_imgs = topItem.PropImgList == null ? string.Empty : JsonConvert.SerializeObject(topItem.PropImgList);
            item.property_alias = topItem.PropAlias == null ? string.Empty : topItem.PropAlias;
            item.props = topItem.Props == null ? string.Empty : topItem.Props;
            item.score = topItem.Score;
            item.seller_cids = topItem.SellerCids == null ? string.Empty : topItem.SellerCids;
            item.skus = topItem.SkuList == null ? string.Empty : JsonConvert.SerializeObject(topItem.SkuList);
            item.stuff_status = topItem.StuffStatus == null ? string.Empty : topItem.StuffStatus;
            item.title = topItem.Title == null ? string.Empty : topItem.Title;
            item.type = topItem.Type == null ? string.Empty : topItem.Type;
            item.valid_thru = topItem.ValidThru;
            item.volume = topItem.Volume;
            item.videos = topItem.VideoList == null ? string.Empty : JsonConvert.SerializeObject(topItem.VideoList);
            item.StockProps = string.Empty;
            item.KeyProps = string.Empty;
            item.NotKeyProps = string.Empty;
            item.SaleProps = string.Empty;
            item.IsAsociate = false;
            item.IsUpdate = false;
            item.IsHistory = false;
            item.IsInAutoPlan = false;
            item.IsSelected = false;
            item.ItemType = Alading.Core.Enum.ItemType.CommonProduct;
            item.UnitCode = Constants.DEFAULT_UNIT_CODE;
        }

        /// <summary>
        /// 将淘宝获取的Shop 和 User值赋值到数据库Shop
        /// </summary>
        /// <param name="shop"></param>
        /// <param name="TbShop"></param>
        /// <param name="TbUser"></param>
        public static void ShopCopyData(Entity.Shop shop, Taobao.Entity.Shop TbShop, Taobao.Entity.User TbUser)
        {
            shop.sid = TbShop.Sid;//店铺编号
            shop.cid = TbShop.Cid;//店铺类目编号
            shop.nick = TbShop.SellerNick;//店主淘宝会员号
            shop.title = TbShop.Title;//店铺名称

            shop.desc = TbShop.Description == null ? string.Empty : HttpUtility.HtmlEncode(TbShop.Description);//店铺描述
            shop.bulletin = TbShop.Bulletin == null ? string.Empty : HttpUtility.HtmlEncode(TbShop.Bulletin);//店铺公告

            shop.pic_path = TbShop.PicPath;//店标地址
            if (!string.IsNullOrEmpty(TbShop.Created))
            {
                shop.created = DateTime.Parse(TbShop.Created);//创建时间
            }
            else
            {
                shop.created = DateTime.MinValue;
            }
            if (!string.IsNullOrEmpty(TbShop.Modified))
            {
                shop.modified = DateTime.Parse(TbShop.Modified);//修改时间
            }
            else
            {
                shop.modified = DateTime.MinValue;
            }
            shop.sex = TbUser.Sex;

            /*卖家信用*/
            shop.seller_level = TbUser.SellerCredit.Level;
            shop.seller_score = TbUser.SellerCredit.Score;
            shop.seller_total_num = TbUser.SellerCredit.TotalNum;
            shop.seller_good_num = TbUser.SellerCredit.GoodNum;

            /*用户当前居住地公开信息*/
            shop.seller_zip = TbUser.Location.Zip;
            shop.seller_state = TbUser.Location.State;
            shop.seller_address = TbUser.Location.Address;
            shop.seller_city = TbUser.Location.City;
            shop.seller_state = TbUser.Location.State;
            shop.seller_country = TbUser.Location.Country;
            shop.seller_district = TbUser.Location.District;

            shop.user_created = DateTime.Parse(TbUser.Created);
            shop.last_visit = Convert.ToDateTime(TbUser.LastVisit);
            shop.birthday = Convert.ToDateTime(TbUser.Birthday);
            shop.type = TbUser.Type;//用户类型。可选值:B(B商家),C(C商家) 
            if (shop.type == "B")
            {
                shop.ShopType = (int)ShopType.TaobaoBShop;
            }
            else if (shop.type == "C")
            {
                shop.ShopType = (int)ShopType.TaobaoCShop;
            }
            else
            {
                shop.ShopType = 0;
            }
            shop.has_more_pic = TbUser.HasMorePic;
            shop.item_img_num = TbUser.ItemImgNum;
            shop.item_img_size = TbUser.ItemImgSize;
            shop.prop_img_num = TbUser.PropImgNum;
            shop.prop_img_size = TbUser.PropImgSize;
            shop.auto_repost = TbUser.AutoRepost;
            shop.promoted_type = TbUser.PromotedType;
            shop.status = TbUser.Status;//状态值:normal(正常),inactive(未激活),delete(删除),reeze(冻结),supervise(监管)
            shop.alipay_bind = TbUser.AlipayAccount;
            shop.consumer_protection = TbUser.ConsumerProtection;
            shop.alipay_account = TbUser.AlipayAccount;
            shop.alipay_no = TbUser.AlipayNo;
        }

        /// <summary>
        /// 将枚举类的数字值转为其对应的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumData(string key, int? value)
        {
            string returnValue = string.Empty;
            if (key == "InOutStatus")
            {
                if (value == 1)
                {
                    returnValue = "部分发货";
                }
                else if (value == 2)
                {
                    returnValue = "全部发货";
                }
                else if (value == 3)
                {
                    returnValue = "未发货";
                }
                else if (value == 4)
                {
                    returnValue = "部分到货";
                }
                else if (value == 5)
                {
                    returnValue = "未到货";
                }
                else if (value == 6)
                {
                    returnValue = "全部到货";
                }
                else if (value == 7)
                {
                    returnValue = "部分退款";
                }
                else if (value == 8)
                {
                    returnValue = "全部退款";
                }
                else if (value == 9)
                {
                    returnValue = "未退款";
                }
            }
            else if (key == "InOutType")
            {
                if (value == 1)
                {
                    returnValue = "生产入库";
                }
                else if (value == 2)
                {
                    returnValue = "采购入库";
                }
                else if (value == 3)
                {
                    returnValue = "调拨入库";
                }
                else if (value == 4)
                {
                    returnValue = "销售退货入库";
                }
                else if (value == 5)
                {
                    returnValue = "报溢入库";
                }
                else if (value == 6)
                {
                    returnValue = "期初入库";
                }
                else if (value == 7)
                {
                    returnValue = "其他入库";
                }
                else if (value == 8)
                {
                    returnValue = "调拨出库";
                }
                else if (value == 9)
                {
                    returnValue = "销售出库";
                }
                else if (value == 10)
                {
                    returnValue = "采购退货出库";
                }
                else if (value == 11)
                {
                    returnValue = "报损出库";
                }
                else if (value == 12)
                {
                    returnValue = "其它出库";
                }
            }
            else if (key == "PayType")
            {
                if (value == 1)
                {
                    returnValue = "现金支付";
                }
                else if (value == 2)
                {
                    returnValue = "支票支付";
                }
                else if (value == 3)
                {
                    returnValue = "银行转账";
                }
                else if (value == 4)
                {
                    returnValue = "支付宝支付";
                }
                else if (value == 5)
                {
                    returnValue = "信用卡支付";
                }
                else if (value == 6)
                {
                    returnValue = "其他方式支付";
                }
            }
            else if (key == "DetailType")
            {
                if (value == 1)
                {
                    returnValue = "采购入库";
                }
                else if (value == 2)
                {
                    returnValue = "销售退货入库";
                }
                else if (value == 3)
                {
                    returnValue = "盘点入库";
                }
                else if (value == 4)
                {
                    returnValue = "生产入库";
                }
                else if (value == 5)
                {
                    returnValue = "调拨入库";
                }
                else if (value == 6)
                {
                    returnValue = "报溢入库";
                }
                else if (value == 7)
                {
                    returnValue = "期初入库";
                }
                else if (value == 8)
                {
                    returnValue = "其它入库";
                }
                else if (value == 9)
                {
                    returnValue = "淘宝销售出库";
                }
                else if (value == 10)
                {
                    returnValue = "采购退货出库";
                }
                else if (value == 11)
                {
                    returnValue = "其他店销售出库";
                }
                else if (value == 12)
                {
                    returnValue = "调拨出库";
                }
                else if (value == 13)
                {
                    returnValue = "报损出库";
                }
                else if (value == 14)
                {
                    returnValue = "其他出库";
                }
            }
            return returnValue;
        }

        //比较   小盆同学专用
        public static bool ProSkuComPare(string orderSku,string taobaoOrderSku)
        {
            if (SkuProChange(orderSku).Equals(SkuProChange(taobaoOrderSku)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 销售属性串重构
        /// </summary>
        /// <param name="skuPros"></param>
        /// <returns></returns>
        public static  string SkuProChange(string skuPros)
        {
            List<string> str_list = new List<string>();
            string[] str1 = null;
            StringBuilder strBuilder = new StringBuilder();
            str1 = skuPros.Split(';');
            foreach (string str in str1)
            {
                str_list.Add(str);
            }
            str_list.Sort();
            foreach (string str in str_list)
            {
                strBuilder.Append(str);
                strBuilder.Append(";");
            }
            //去掉最后一个“；”
            return strBuilder.ToString().Substring(0, strBuilder.ToString().Length - 1);
        }

        #region 下载属性方法
        /// <summary>
        /// 下载类目cid下的所有属性及属性值,
        /// 如果不使用BackgroundWorker，可将其置为null
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="worker"></param>
        public static void DownPropsAndValues(string cid, BackgroundWorker worker)
        {
            try
            {
                //获取cid下的所有属性值
                if (!ItemPropValueService.IsExistedCid(cid))
                {
                    DownItemPropvalue(cid);
                }

                //获取cid下的所有属性存入数据库
                if (!ItemPropService.IsPropExistedCid(cid))
                {
                    DownItemProp(cid);
                }

                //获取所有关键属性的属性值，并更新属性值的is_parent字段
                UpdatePropValueIsParent(cid, worker);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 先下载类目cid下的所有属性值，不能返回is_parent字段
        /// </summary>
        public static void DownItemPropvalue(string cid)
        {
            ItemCatRsp myrsp = new ItemCatRsp();
            try
            {
                myrsp = TopService.ItemPropValuesGet(cid, null, "2005-01-01 00:00:00");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (myrsp.PropValues != null && myrsp.PropValues.PropValue != null)
            {
                DataTable table = new DataTable();
                table.Columns.Add("cid", typeof(string));
                table.Columns.Add("is_parent", typeof(bool));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("name_alias", typeof(string));
                table.Columns.Add("pid", typeof(string));
                table.Columns.Add("prop_name", typeof(string));
                table.Columns.Add("vid", typeof(string));
                table.Columns.Add("status", typeof(string));
                table.Columns.Add("sort_order", typeof(Int32));
                Alading.Taobao.Entity.PropValue[] pvalueArr = myrsp.PropValues.PropValue;
                for (int i = 0; i < pvalueArr.Length; i++)
                {
                    DataRow row = table.NewRow();
                    row["cid"] = pvalueArr[i].Cid;
                    row["is_parent"] = pvalueArr[i].IsParent;//API不能返回此值
                    row["name"] = pvalueArr[i].Name ?? string.Empty;
                    row["name_alias"] = pvalueArr[i].NameAlias ?? string.Empty;
                    row["pid"] = pvalueArr[i].Pid;
                    row["prop_name"] = pvalueArr[i].PropName ?? string.Empty;
                    row["vid"] = pvalueArr[i].Vid;
                    row["status"] = pvalueArr[i].Status ?? string.Empty;
                    row["sort_order"] = pvalueArr[i].SortOrder;
                    table.Rows.Add(row);
                }
                ItemPropValueService.AddItemPropValueSqlBulkCopy(table);
            }
        }

        /// <summary>
        /// 下载类目cid下的所有属性，不能返回prop_values
        /// </summary>
        public static void DownItemProp(string cid)
        {
            ItemCatRsp rsp = new ItemCatRsp();
            try
            {
                rsp = TopService.ItemPropsGet(cid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (rsp.ItemProps != null && rsp.ItemProps.ItemProp != null)
            {
                DataTable table = new DataTable();
                table.Columns.Add("cid", typeof(string));
                table.Columns.Add("child_template", typeof(string));
                table.Columns.Add("is_allow_alias", typeof(bool));
                table.Columns.Add("is_color_prop", typeof(bool));
                table.Columns.Add("is_enum_prop", typeof(bool));
                table.Columns.Add("is_input_prop", typeof(bool));
                table.Columns.Add("is_item_prop", typeof(bool));
                table.Columns.Add("is_key_prop", typeof(bool));
                table.Columns.Add("is_sale_prop", typeof(bool));
                table.Columns.Add("multi", typeof(bool));
                table.Columns.Add("must", typeof(bool));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("parent_pid", typeof(string));
                table.Columns.Add("parent_vid", typeof(string));
                table.Columns.Add("pid", typeof(string));
                table.Columns.Add("prop_name", typeof(string));
                table.Columns.Add("prop_values", typeof(string));
                table.Columns.Add("status", typeof(string));
                table.Columns.Add("sort_order", typeof(Int32));

                Taobao.Entity.ItemProp[] propArr = rsp.ItemProps.ItemProp;
                for (int i = 0; i < propArr.Length; i++)
                {
                    #region 数据赋值
                    DataRow row = table.NewRow();
                    row["cid"] = cid;
                    row["child_template"] = propArr[i].ChildPropTemplate ?? string.Empty;
                    row["is_allow_alias"] = propArr[i].IsAllowAlias;
                    row["is_color_prop"] = propArr[i].IsColorProp;
                    row["is_enum_prop"] = propArr[i].IsEnumProp;
                    row["is_input_prop"] = propArr[i].IsInputProp;
                    row["is_item_prop"] = propArr[i].IsItemProp;
                    row["is_key_prop"] = propArr[i].IsKeyProp;
                    row["is_sale_prop"] = propArr[i].IsSaleProp;
                    row["multi"] = propArr[i].IsMulti;
                    row["must"] = propArr[i].IsMust;
                    row["name"] = propArr[i].Name ?? string.Empty;
                    row["parent_pid"] = propArr[i].ParentPid;
                    row["parent_vid"] = propArr[i].ParentVid;
                    row["pid"] = propArr[i].Pid;
                    row["prop_values"] = JsonConvert.SerializeObject(propArr[i].PropValues) ?? string.Empty;
                    row["sort_order"] = propArr[i].SortOrder;
                    row["status"] = propArr[i].Status ?? string.Empty;
                    #endregion
                    table.Rows.Add(row);
                }//for
                ItemPropService.AddItemPropSqlBulkCopy(table);
            }
        }

        /// <summary>
        /// 更新类目cid下所有属性值的is_parent字段，条件是必须保证cid下的所有属性及属性值已完全下载,
        /// 如果不使用BackgroundWorker，可将其置为null
        /// </summary>
        public static void UpdatePropValueIsParent(string cid, BackgroundWorker worker)
        {
            try
            {
                /*读取存在子属性的关键属性的pid*/
                List<string> keyPropsPidList = ItemPropService.GetKeyPropPid(cid);
                if (keyPropsPidList.Count == 0)
                {
                    return;
                }
                DataTable table = new DataTable();

                #region 构造DataTable，为此类目下属性值添加自定义
                table.Columns.Add("cid", typeof(string));
                table.Columns.Add("is_parent", typeof(bool));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("name_alias", typeof(string));
                table.Columns.Add("pid", typeof(string));
                table.Columns.Add("prop_name", typeof(string));
                table.Columns.Add("vid", typeof(string));
                table.Columns.Add("status", typeof(string));
                table.Columns.Add("sort_order", typeof(Int32));
                #endregion

                List<Taobao.Entity.PropValue> pValueListNeedUpdate = new List<Taobao.Entity.PropValue>();

                #region 循环获取keyPropsPidList的属性值
                float n = keyPropsPidList.Count;
                int temp = 0;//作用是避免进度值propgress没有改变时得重复报告
                for (int i = 0; i < n; i++)
                {
                    string pid = keyPropsPidList[i];
                    ItemCatRsp rsp = TopService.ItemPropsGet(cid, pid, null);
                    if (rsp.ItemProps == null || rsp.ItemProps.ItemProp == null)
                    {
                        /*说明下载失败*/
                        continue;
                    }

                    #region 给每个关键属性的值加一条“自定义”记录
                    if (!ItemPropValueService.IsExistedPropValueName(cid, pid, "自定义"))
                    {
                        DataRow row = table.NewRow();
                        row["cid"] = cid;
                        row["is_parent"] = false;
                        row["name"] = row["name_alias"] = "自定义";
                        row["pid"] = pid;
                        row["vid"] = 0;
                        row["status"] = "normal";
                        row["sort_order"] = 1000;
                        if (rsp.ItemProps.ItemProp != null && rsp.ItemProps.ItemProp.Length > 0)
                        {
                            row["prop_name"] = rsp.ItemProps.ItemProp[0].Name ?? string.Empty;
                        }
                        else
                        {
                            row["prop_name"] = string.Empty;
                        }
                        table.Rows.Add(row);
                    }
                    #endregion

                    foreach (Taobao.Entity.ItemProp prop in rsp.ItemProps.ItemProp)
                    {
                        Taobao.Entity.PropValues propvalues = prop.PropValues;
                        if (propvalues != null && propvalues.PropValue != null)
                        {
                            List<Alading.Taobao.Entity.PropValue> propValueWhereToList = propvalues.PropValue.Where(p => p.IsParent).ToList();
                            foreach (Taobao.Entity.PropValue pv in propValueWhereToList)
                            {
                                pv.Cid = cid;
                                pv.Pid = pid;
                            }
                            pValueListNeedUpdate.AddRange(propValueWhereToList);
                        }
                    }//foreach

                    //进度报告
                    if (worker != null)
                    {     
                        int propgress = (int)((float)(i + 1) / n * 100);
                        if (propgress > temp)
                        {
                            worker.ReportProgress(propgress, null);
                        }
                        temp = propgress;
                    }
                }//for 
                #endregion

                //添加自定义属性
                ItemPropValueService.AddItemPropValueSqlBulkCopy(table);

                //更新is_parent字段
                if (pValueListNeedUpdate.Count > 0)
                {
                    ItemPropValueService.UpdateItemPropValueDataParameters(pValueListNeedUpdate);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 下载店铺自定义类目和B家授权类目
        /// <summary>
        /// 下载Nick店铺的授权列表
        /// </summary>
        public static void DownSellerAuthorize(string nick)
        {
            try
            {
                ItemCatRsp myrsp = TopService.ItemCatsAuthorizeGet(string.Empty);
                if (myrsp.SellerAuthorize == null)
                {
                    return;
                }
                if (myrsp.SellerAuthorize.Brands != null && myrsp.SellerAuthorize.Brands.Brand != null)
                {
                    foreach (Alading.Taobao.Entity.Brand brand in myrsp.SellerAuthorize.Brands.Brand)
                    {
                        if (BrandService.IsBrandExisted(nick, brand.ValueId.ToString()) == ReturnType.PropertyExisted)
                        {
                            continue;
                        }
                        Alading.Entity.Brand NewBrand = new Alading.Entity.Brand();
                        NewBrand.BrandCode = System.Guid.NewGuid().ToString();
                        NewBrand.vid = brand.ValueId;
                        NewBrand.name = brand.ValueName ?? string.Empty;
                        NewBrand.pid = brand.PropId;
                        NewBrand.prop_name = brand.PropName ?? string.Empty;
                        NewBrand.SellerNick = nick;
                        BrandService.AddBrand(NewBrand);
                    }
                }
                if (myrsp.SellerAuthorize.ItemCats != null && myrsp.SellerAuthorize.ItemCats.ItemCat != null)
                {
                    foreach (Alading.Taobao.Entity.ItemCat itemcat in myrsp.SellerAuthorize.ItemCats.ItemCat)
                    {
                        if (ItemSellerAuthorizeService.IsAuthorizeExisted(nick, itemcat.Cid) == ReturnType.PropertyExisted)
                        {
                            continue;
                        }
                        Alading.Entity.ItemSellerAuthorize sellerauthorize = new Alading.Entity.ItemSellerAuthorize();
                        sellerauthorize.ItemSellerAuthorizeCode = System.Guid.NewGuid().ToString();
                        sellerauthorize.Cid = itemcat.Cid;
                        sellerauthorize.Name = itemcat.Name ?? string.Empty;
                        sellerauthorize.SellerNick = nick;
                        ItemSellerAuthorizeService.AddItemSellerAuthorize(sellerauthorize);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 下载卖家自定义类目
        /// </summary>
        /// <param name="nick"></param>
        public static void DownSellerCatsList(string nick)
        {
            try
            {

                ShopRsp shoprsp = TopService.SellerCatsListGet(nick);
                if (shoprsp != null && shoprsp.SellerCats != null && shoprsp.SellerCats.SellerCat != null)
                {
                    foreach (Alading.Taobao.Entity.SellerCat sellercat in shoprsp.SellerCats.SellerCat)
                    {
                        Alading.Entity.SellerCat dbSellercat = new Alading.Entity.SellerCat();
                        dbSellercat.SellerNick = nick;
                        dbSellercat.cid = sellercat.Cid;
                        dbSellercat.name = sellercat.Name ?? string.Empty;
                        dbSellercat.parent_cid = sellercat.ParentCid;
                        dbSellercat.pic_url = sellercat.PicUrl ?? string.Empty;
                        dbSellercat.sort_order = sellercat.SortOrder;
                        dbSellercat.SellerCatCode = System.Guid.NewGuid().ToString();
                        SellerCatService.AddSellerCat(dbSellercat);//存在更新，不存在更新
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取汉字首字母
        /// <summary>
        /// 获取汉字的拼音首字母，其余字符不会改变
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string GetChineseSpell(string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += getSpell(strText.Substring(i, 1));
            }
            return myStr;
        }
        public static string getSpell(string cnChar)
        {
            byte[] arrCN = Encoding.Default.GetBytes(cnChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                        //return Encoding.Default.GetString(new byte[] { (byte)(97 + i) });
                    }
                }
                return string.Empty;
            }
            else return cnChar;
        }
        #endregion

        /// <summary>
        /// 销售属性为一个时，获取当前sku
        /// </summary>
        /// <param name="vid">属性的vid</param>
        /// <param name="itemskus">商品所有sku</param>
        /// <returns>当前属性对应的sku</returns>
        public static Alading.Taobao.Entity.Sku GetSelectedSku(string vid, Skus itemskus)
        {
            Alading.Taobao.Entity.Sku ssku = new Alading.Taobao.Entity.Sku();
            foreach (Alading.Taobao.Entity.Sku sku in itemskus.Sku)
            {
                string[] propsArray = sku.SkuProps.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (propsArray.Length == 2 && propsArray[1] == vid)
                {
                    ssku = sku;
                }
            }
            return ssku;
        }

        /// <summary>
        /// 销售属性为二个时寻找当前属性组合对应的sku,(sku形式： 1627207:28341;21921:29542)
        /// </summary>
        /// <param name="vid">属性的vid</param>
        /// <param name="vvid">属性的vid</param>
        /// <param name="itemskus">商品所有sku</param>
        /// <returns>当前属性组合对应的sku</returns>
        public static Alading.Taobao.Entity.Sku GetSelectedSku(string vid, string vvid, Skus itemskus)
        {
            Alading.Taobao.Entity.Sku ssku = new Alading.Taobao.Entity.Sku();
            foreach (Alading.Taobao.Entity.Sku sku in itemskus.Sku)
            {
                string[] propsArray = sku.SkuProps.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (propsArray.Length == 2)
                {
                    string pvid = string.Empty;
                    string[] pvidArray = propsArray[0].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    if (pvidArray.Count() == 2)
                    {
                        pvid = pvidArray[1];
                    }

                    string ppvid = string.Empty;
                    string[] ppvidArray = propsArray[1].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    if (ppvidArray.Count() == 2)
                    {
                        ppvid = ppvidArray[1];
                    }
                    if (vid == pvid && vvid == ppvid || vid == ppvid && vvid == pvid)
                    {
                        ssku = sku;
                    }
                }
            }
            return ssku;
        }

        /// <summary>
        ///  如：1627207:28341:黑色;1627207:3232481:棕色。 获取属性的重命名
        /// </summary>
        /// <param name="vid">属性vid</param>
        /// <param name="propertyAlias">重命名的属性串</param>
        /// <returns>返回属性的重命名</returns>
        public static string GetNewName(string vid, string propertyAlias)
        {
            string newName = string.Empty;
            if (!string.IsNullOrEmpty(propertyAlias))
            {
                string[] aliasArray = propertyAlias.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string alias in aliasArray)
                {
                    string[] propsArray = alias.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    if (propsArray.Length == 3 && propsArray[1] == vid)
                    {
                        newName = propsArray[2];
                    }
                }
            }
            return newName;
        }

    }

    #region 扩展类
    public class ShopItem
    {
        public string props
        {
            get;
            set;
        }

        public string input_pids
        {
            get;
            set;
        }

        public string property_alias
        {
            get;
            set;
        }

        public string cid
        {
            get;
            set;
        }

        public string input_str
        {
            get;
            set;
        }

        public string SkuProps
        {
            get;
            set;
        }

        public string SkuProps_Str
        {
            get;
            set;
        }
        
    }
    /// <summary>
    /// 自定义属性
    /// </summary>
    public class PropertyAlias
    {
        public string Vid
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }
    }

    /// <summary>
    /// TreeListNodeTag对象
    /// </summary>
    public class TreeListNodeTag
    {
        public TreeListNodeTag(string cid)
        {
            this.Cid = cid;
            this.HasExpanded = false;
        }
        public string Cid
        {
            get;
            set;
        }

        public bool HasExpanded
        {
            get;
            set;
        }
    }

    public class EditorRowTag
    {
        public EditorRowTag()
        {
            IsInputProp = false;
        }

        public string Cid
        {
            get;
            set;
        }

        public string Pid
        {
            get;
            set;
        }

        public string Vid
        {
            get;
            set;
        }

        public bool IsInputProp
        {
            get;
            set;
        }

        public bool IsParent
        {
            get;
            set;
        }

        public string ChildTemplate
        {
            get;
            set;
        }

        public bool Is_Allow_Alias
        {
            get;
            set;
        }

        public bool IsMust
        {
            get;
            set;
        }
    }

    #region 订单管理辅助函数 交易状态更新 库存数量更新 原语操作
    public class TradeHelper
    {
        
        /// <summary>
        /// 更新交易本地状态，在涉及本地状态改变时，必须用本方法来更新 
        /// </summary>
        /// <param name="trade">待更新状态的交易</param>
        /// <param name="newLocalStatus">待更新的本地状态值</param>
        private static void SetTradeLocalStatas(string customTid, string newLocalStatus)
        {
            Alading.Entity.Trade trade = TradeService.GetTrade(customTid);
            trade.LocalStatus = newLocalStatus;
            TradeService.UpdateTrade(trade);
        }

        /// <summary>
        ///增加库存占用量
        /// </summary>
        /// <param name="skuOutId">库存商品的skuOutId</param>
        /// <param name="occupiedQuantity">新占用库存商品量</param>
        private static void AddStockOccupied(string skuOutId, int occupiedQuantity)
        {
            StockProduct stockProduct = StockProductService.GetStockProduct(skuOutId);
            stockProduct.OccupiedQuantity += occupiedQuantity;
            StockProductService.UpdateStockProduct(stockProduct);
        }

        /// <summary>
        /// 释放库存占用量
        /// </summary>
        /// <param name="skuOutId">库存商品的skuOutId</param>
        /// <param name="occupiedQuantity">新释放库存商品量</param>
        private static void ReleaseStockOccupied(string skuOutId, int occupiedQuantity)
        {
            StockProduct stockProduct = StockProductService.GetStockProduct(skuOutId);
            stockProduct.OccupiedQuantity += occupiedQuantity;
            StockProductService.UpdateStockProduct(stockProduct);
        }

        /// <summary>
        /// 获取某一商品的剩余库存量
        /// </summary>
        /// <param name="skuOutId">库存商品的skuOutId</param>
        /// <returns></returns>
        private static int GetStockRemainQuantity(string skuOutId)
        {
            StockProduct stockProduct = StockProductService.GetStockProduct(skuOutId);
            return stockProduct.SkuQuantity - stockProduct.OccupiedQuantity;
        }
        #endregion

    }

    #endregion
}
