using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using Alading.Business;
using Alading.Entity;
using Alading.Core.Enum;
using System.Linq;
using Alading.Utils;
using DevExpress.Utils;
using Alading.Taobao;
using System.Linq.Expressions;
using DevExpress.XtraTreeList;
using Alading.Core.Code128;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using System.Collections;
using System.Configuration;
using Alading.Forms.Stock.SettingUp;

namespace Alading.Forms.Stock.Product
{
    public partial class StockProduct : DevExpress.XtraEditors.XtraForm
    {
        #region 全局变量

        /// <summary>
        /// 异步获取图片
        /// </summary>
        /// <param name="outerID"></param>
        /// <returns></returns>
        public delegate Image ItemImageDelegate(string outerID);

        /// <summary>
        /// 商家编码被修改的标志,用于到数据库查询是否有重复
        /// </summary>
        bool skuOuterIDChangeTag = false;
        string TaxCode = string.Empty;
        /// <summary>
        /// 表明是根据StockItem查询
        /// </summary>
        bool itemFlag = true;
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
        /// StockItem查询条件
        /// </summary>
        Func<View_StockItemUnit, bool> func = null;

        /// <summary>
        /// View_StockItemProduct查询条件，1表示正常,其余见Enum
        /// </summary>
        int funcType = 1;
        ///// <summary>
        ///// 焦点行行号
        ///// </summary>
        ////int focusedRowHandle = 0;
        /// <summary>
        /// 商品是否有图片
        /// </summary>
        bool hasPicture = false;
        #endregion
        public StockProduct()
        {
            InitializeComponent();
            //加载计量单位
            InitUnit();
            //加载税率
            InitTax();
            //加载库存类目
            InitStockCat();
        }

        /// <summary>
        /// 加载计量单位
        /// </summary>
        private void InitUnit()
        {
            try
            {
                treeUnit.Nodes.Clear();
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
            try
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
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }

        /// <summary>
        /// 加载库存类目
        /// </summary>
        private void InitStockCat()
        {
            try
            {
                tlStockCat.Nodes.Clear();
                //只加载最顶级的类目
                List<Alading.Entity.StockCat> stockCatList = StockCatService.GetStockCat(i => i.ParentCid == "0");
                tlStockCat.BeginUnboundLoad();
                foreach (Alading.Entity.StockCat stockCat in stockCatList)
                {
                    TreeListNode node = tlStockCat.AppendNode(new object[] { stockCat.StockCatName }, null, new TreeListNodeTag(stockCat.StockCid));

                    ////设置是否有子节点，有则会显示一个+号
                    //node.HasChildren = stockCat.IsParent;

                    List<Alading.Entity.StockCat> childCatList = StockCatService.GetStockCat(i => i.ParentCid == stockCat.StockCid);
                    LoadCat(childCatList, node.Id);
                }

                tlStockCat.EndUnboundLoad();
                //不可编辑
                tlStockCat.OptionsBehavior.Editable = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }

        /// <summary>
        /// 添加库存类目子节点
        /// </summary>
        /// <param name="stockCatList"></param>
        /// <param name="parentNodeID"></param>
        private void LoadCat(List<Alading.Entity.StockCat> stockCatList, int parentNodeID)
        {
            try
            {
                foreach (Alading.Entity.StockCat stockCat in stockCatList)
                {
                    TreeListNode node = tlStockCat.AppendNode(new object[] { stockCat.StockCatName }, parentNodeID, new TreeListNodeTag(stockCat.StockCid));
                    //设置是否有子节点，有则会显示一个+号
                    if (stockCat.IsParent)
                    {
                        LoadCat(stockCatList.Where(i => i.ParentCid == stockCat.StockCid).ToList(), node.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }


        #region 按商品类别
        /// <summary>
        /// 产成品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem10_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            func = new Func<View_StockItemUnit, bool>(i => i.StockItemType == (int)StockItemType.FinishGoods);
            GetStockItems();

        }
        /// <summary>
        /// 原材料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem11_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            func = new Func<View_StockItemUnit, bool>(i => i.StockItemType == (int)StockItemType.RawMaterial);
            GetStockItems();

        }
        /// <summary>
        /// 服务类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem12_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            func = new Func<View_StockItemUnit, bool>(i => i.StockItemType == (int)StockItemType.ServiceGoods);
            GetStockItems();

        }
        /// <summary>
        /// 在产品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem13_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            func = new Func<View_StockItemUnit, bool>(i => i.StockItemType == (int)StockItemType.InProducts);
            GetStockItems();

        }
        /// <summary>
        /// 赠品、样品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem14_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            func = new Func<View_StockItemUnit, bool>(i => i.StockItemType == (int)StockItemType.GiftGoods);
            GetStockItems();
        }
        /// <summary>
        /// 代销品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem15_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            func = new Func<View_StockItemUnit, bool>(i => i.IsConsignment == true);
            GetStockItems();
        }
        #endregion

        #region 按商品状态
        /// <summary>
        /// 正常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //viewFunc = new Func<View_StockItemProduct, bool>(i => i.SkuQuantity >= i.LowestNum && i.SkuQuantity <= i.HighestNum);
            funcType = (int)ItemProductStatus.ProductNormal;
            GetStockProducts();

        }
        /// <summary>
        /// 缺货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem6_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //viewFunc = new Func<View_StockItemProduct, bool>(i => i.SkuQuantity <= i.OccupiedQuantity);
            funcType = (int)ItemProductStatus.ProductLack;
            GetStockProducts();
        }
        /// <summary>
        /// 超储
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem7_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //viewFunc = new Func<View_StockItemProduct, bool>(i => i.SkuQuantity > i.HighestNum);
            funcType = (int)ItemProductStatus.ProductOverStock;
            GetStockProducts();

        }
        /// <summary>
        /// 预警
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem8_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //viewFunc = new Func<View_StockItemProduct, bool>(i => i.SkuQuantity < i.WarningCount);
            funcType = (int)ItemProductStatus.ProductWarn;
            GetStockProducts();

        }
        #endregion

        #region 按商品日期
        /// <summary>
        /// 近一周
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            func = new Func<View_StockItemUnit, bool>(i => i.Created > DateTime.Now.AddDays(-7) && i.Created < DateTime.Now);
            GetStockItems();

        }
        /// <summary>
        /// 近一月
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            func = new Func<View_StockItemUnit, bool>(i => i.Created > DateTime.Now.AddMonths(-1) && i.Created < DateTime.Now);
            GetStockItems();

        }
        /// <summary>
        /// 近三月
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            func = new Func<View_StockItemUnit, bool>(i => i.Created > DateTime.Now.AddMonths(-1) && i.Created < DateTime.Now);
            GetStockItems();

        }
        /// <summary>
        /// 近半年
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            func = new Func<View_StockItemUnit, bool>(i => i.Created > DateTime.Now.AddMonths(-6) && i.Created < DateTime.Now);
            GetStockItems();

        }
        /// <summary>
        /// 近一年
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem9_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            func = new Func<View_StockItemUnit, bool>(i => i.Created > DateTime.Now.AddYears(-1) && i.Created < DateTime.Now);
            GetStockItems();

        }
        #endregion

        #region 按商品类目

        private void treeListCat_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node != null && e.Node.Tag != null)
            {
                List<string> cidList = e.Node.Tag.ToString().Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                //库存根节点Tag为stock,淘宝根节点Tag为taobao
                if (e.Node.RootNode.Tag.ToString().Contains("stock"))
                {
                    func = new Func<View_StockItemUnit, bool>(i => cidList.Contains(i.StockCid));
                    GetStockItems();
                }
                else
                {
                    func = new Func<View_StockItemUnit, bool>(i => cidList.Contains(i.Cid));
                    GetStockItems();
                }
            }
        }

        #endregion

        #region 加载类目

        /// <summary>
        /// 初始化加载商品类目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StockProduct_Load(object sender, EventArgs e)
        {
            //初始时保存修改按钮不可用
            SetBtnModify(false);
            LoadCat();//加载类目
            repositoryItemMemoExEdit1.ReadOnly = true;
            repositoryItemMemoExEdit2.ReadOnly = true;
            repositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;           
        }

        /// <summary>
        /// 加载类目
        /// </summary>
        private void LoadCat()
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                gvStockItem.BestFitColumns();
                treeListCat.Nodes[0].Tag = "stock";
                treeListCat.Nodes[1].Tag = "taobao";
                AddStockNodes(treeListCat.Nodes[0], treeListCat);//加载库存类目
                AddNodes(treeListCat.Nodes[1], treeListCat);//加载淘宝类目  
                //加载库存商品
                if (treeListCat.Nodes[0].Tag != null)
                {
                    List<string> cidList = treeListCat.Nodes[0].Tag.ToString().Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    func = new Func<View_StockItemUnit, bool>(i => cidList.Contains(i.StockCid));
                    GetStockItems();
                }
                treeListCat.Nodes[0].ExpandAll();
                treeListCat.Nodes[1].ExpandAll();
                waitFrm.Close();
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 向treeList中加载出售中的商品的类目
        /// </summary>       
        /// <param name="node1">父节点</param>
        /// <param name="treeListCat">被加载的控件treeListCat</param>
        private void AddNodes(TreeListNode parentNode, DevExpress.XtraTreeList.TreeList treeListCat)
        {
            List<ItemCat> itemCatList = StockItemService.GetAllStockItemCid();
            if (itemCatList != null && itemCatList.Count > 0)
            {
                foreach (Alading.Entity.ItemCat itemCat in itemCatList.FindAll(i => i.parent_cid == "0"))
                {
                    TreeListNode childNode = treeListCat.AppendNode(new object[] { itemCat.name }, parentNode);
                    childNode.Tag = itemCat.cid;
                    AppendNodes(itemCat.cid.ToString(), childNode, treeListCat, itemCatList);
                    parentNode.Tag += "," + childNode.Tag.ToString();
                }
            }
        }

        /// <summary>
        /// 添加淘宝类目子节点
        /// </summary>
        /// <param name="sellerCatCid">父节点Cid</param>
        /// <param name="node1">父节点</param>       
        /// <param name="listSellerCat">所有类目</param>
        private void AppendNodes(string parentCid, TreeListNode parentNode, DevExpress.XtraTreeList.TreeList treeListCat, List<Alading.Entity.ItemCat> itemCatList)
        {
            foreach (Alading.Entity.ItemCat stockCat in itemCatList.FindAll(i => i.parent_cid == parentCid))
            {
                TreeListNode childNode = treeListCat.AppendNode(new object[] { stockCat.name }, parentNode);
                childNode.Tag = stockCat.cid;
                AppendNodes(stockCat.cid.ToString(), childNode, treeListCat, itemCatList);
                parentNode.Tag += "," + childNode.Tag.ToString();
            }
        }

        /// <summary>
        /// 向treeList中加载商品的库存类目
        /// </summary>
        /// <param name="shopNick">店铺昵称</param>
        /// <param name="node1">父节点</param>
        /// <param name="treeListCat">被加载的控件treeListCat</param>
        private void AddStockNodes(TreeListNode parentNode, DevExpress.XtraTreeList.TreeList treeListCat)
        {
            List<Alading.Entity.StockCat> listStockCat = StockCatService.GetAllStockCat();
            if (listStockCat != null && listStockCat.Count > 0)
            {
                foreach (Alading.Entity.StockCat stockCat in listStockCat.FindAll(i => i.ParentCid == "0"))
                {
                    TreeListNode childNode = treeListCat.AppendNode(new object[] { stockCat.StockCatName }, parentNode);
                    childNode.Tag = stockCat.StockCid;
                    if (stockCat.IsParent)
                    {
                        AppendStockNodes(childNode.Tag, stockCat.StockCid, childNode, treeListCat, listStockCat);
                    }
                    parentNode.Tag += "," + childNode.Tag.ToString();
                }
            }

        }

        /// <summary>
        /// 添加库存类目子节点
        /// </summary>
        /// <param name="sellerCatCid">父节点Cid</param>
        /// <param name="node1">父节点</param>
        /// <param name="treeListShop">被加载的控件treeListShop</param>
        /// <param name="listSellerCat">所有类目</param>
        private void AppendStockNodes(object tag, string parentCid, TreeListNode parentNode, DevExpress.XtraTreeList.TreeList treeListCat, List<Alading.Entity.StockCat> listStockCat)
        {
            foreach (Alading.Entity.StockCat stockCat in listStockCat.FindAll(i => i.ParentCid == parentCid))
            {
                TreeListNode childNode = treeListCat.AppendNode(new object[] { stockCat.StockCatName }, parentNode);
                childNode.Tag = stockCat.StockCid;
                if (stockCat.IsParent)
                {
                    AppendStockNodes(childNode.Tag, stockCat.StockCid, childNode, treeListCat, listStockCat);
                }
                parentNode.Tag += "," + childNode.Tag.ToString();
            }
        }
        #endregion

        #region 页码操作

        /// <summary>
        /// 根据页码把一些按钮不可用
        /// </summary>
        private void pageBtnEnable()
        {
            //若为首页，则上一页和首页不可用
            if (curreentPage == 1)
            {
                bbtnFirstPage.Enabled = false;
                bbtnFrontPage.Enabled = false;
            }
            else
            {
                bbtnFirstPage.Enabled = true;
                bbtnFrontPage.Enabled = true;
            }
            //若为尾页，则下一页和尾页不可用
            if (curreentPage == totalPages || totalPages == 0)
            {
                bbtnNextPage.Enabled = false;
                bbtnLastPage.Enabled = false;
            }
            else
            {
                bbtnNextPage.Enabled = true;
                bbtnLastPage.Enabled = true;
            }
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            curreentPage = 1;
            ShowItemProductInGrid();
            //根据页码把一些按钮不可用
            pageBtnEnable();
        }
        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            curreentPage--;
            ShowItemProductInGrid();
            //根据页码把一些按钮不可用
            pageBtnEnable();
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            curreentPage++;
            ShowItemProductInGrid();
            //根据页码把一些按钮不可用
            pageBtnEnable();
        }
        /// <summary>
        /// 尾页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            curreentPage = totalPages;
            ShowItemProductInGrid();
            //根据页码把一些按钮不可用
            pageBtnEnable();
        }

        /// <summary>
        /// 转到
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int page = ((DevExpress.XtraEditors.ComboBoxEdit)(sender)).SelectedIndex;
            if (page >= 0)
            {
                curreentPage = page + 1;
                ShowItemProductInGrid();
                //根据页码把一些按钮不可用
                pageBtnEnable();
            }
        }

        /// <summary>
        /// 展示StockItem、StockProduct
        /// </summary>
        private void ShowItemProductInGrid()
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                if (curreentPage - 1 < repositoryItemComboBox1.Items.Count)
                {
                    barEditPage.EditValue = repositoryItemComboBox1.Items[curreentPage - 1];
                }
                if (itemFlag == true)
                {
                    List<View_StockItemUnit> stockItemList = new List<View_StockItemUnit>(StockItemService.GetStockItems(func, curreentPage, pageSize, out totalCount));
                    LoadItemList(stockItemList);
                }
                else
                {
                    List<View_StockItemUnit> stockItemList = new List<View_StockItemUnit>(View_StockItemProductService.GetView_StockItemProducts(funcType, curreentPage, pageSize, out totalCount));
                    LoadItemList(stockItemList);
                }
                //根据页码把一些按钮不可用
                pageBtnEnable();
                waitFrm.Close();
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }

        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 保存按钮是否可用
        /// </summary>
        /// <param name="enabled"></param>
        private void SetBtnModify(bool enabled)
        {
            simpleBtnModify.Enabled = enabled;
            barBtnSaleModify.Enabled = enabled;
            barBtnPropsModify.Enabled = enabled;
        }

        /// <summary>
        /// 加载基本信息
        /// </summary>
        private void LoadTextEditStockItem(Alading.Entity.View_StockItemUnit stockItem)
        {
            try
            {
                textName.Text = stockItem.Name;
                textEditSimpleName.Text = stockItem.SimpleName;
                textEditOutID.Text = stockItem.OuterID;
                textEditModel.Text = stockItem.Model;
                textEditSpecification.Text = stockItem.Specification;
                pceItemCat.Text = stockItem.CatName;
                pceStockCat.Text = stockItem.StockCatName;
                //枚举类从0开始,要注意Item顺序必须和枚举类相同 ---从1开始
                comboItemType.SelectedIndex = stockItem.StockItemType - 1;
                popupUnit.Text = stockItem.StockUnitName;
                comboTax.Text = stockItem.TaxName;

                pceItemCat.Tag = stockItem.Cid;
                pceStockCat.Tag = stockItem.StockCid;
                popupUnit.Tag = stockItem.UnitCode;
                TaxCode = stockItem.Tax;

                mruStockWebSite.Text = stockItem.StockCheckUrl;
                memoExDesc.Text = stockItem.StockItemRemark;
                //代销1是0否
                if (stockItem.IsConsignment == true)
                {
                    radioGroupIsConsignment.SelectedIndex = 1;
                }
                else
                {
                    radioGroupIsConsignment.SelectedIndex = 0;
                }

                //加载图片
                if (!string.IsNullOrEmpty(stockItem.OuterID))
                {
                    ////同步加载图片
                    //pictureEditPic.Image = LoadImage(stockItem.OuterID);

                    //异步加载图片
                    ItemImageDelegate imgDelegate = new ItemImageDelegate(LoadImage);
                    IAsyncResult asyncResult = imgDelegate.BeginInvoke(stockItem.OuterID, new AsyncCallback(GetItemImageCallback), imgDelegate);
                }
                else
                {
                    pictureEditPic.Image = null;
                    hasPicture = false;
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
        private Image LoadImage(string outerID)
        {
            BeginInvoke(new Action(() =>
            {
                //List<Alading.Entity.StockProduct> stockProductList = StockProductService.GetStockProduct(i => i.OuterID == stockItem.OuterID);                    
                List<Alading.Entity.StockProduct> stockProductList = StockProductService.GetStockProductByOuterId(outerID);

                gridStockProduct.DataSource = stockProductList;
                gVStockProduct.BestFitColumns();
                //加载关联宝贝
                //int itemRowHandle = gViewItem.FocusedRowHandle;
                List<Alading.Entity.Item> itemList = ItemService.GetItem(i => i.outer_id == outerID && i.IsAsociate == true);
                gridItem.DataSource = itemList;
                gvStockItem.BestFitColumns();

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
        /// 焦点行改变加载商品详情和属性
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
            Alading.Entity.View_StockItemUnit stockItem = gvStockItem.GetFocusedRow() as View_StockItemUnit;
            if (stockItem != null)
            {
                WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                try
                {                    
                    // 加载基本信息
                    LoadTextEditStockItem(stockItem);

                    //保存修改按钮不可用
                    SetBtnModify(false);

                    //重置 编码是否被修改的标志
                    skuOuterIDChangeTag = false;

                    ////List<Alading.Entity.StockProduct> stockProductList = StockProductService.GetStockProduct(i => i.OuterID == stockItem.OuterID);                    
                    //List<Alading.Entity.StockProduct> stockProductList = StockProductService.GetStockProductByOuterId(stockItem.OuterID);

                    //gridStockProduct.DataSource = stockProductList;
                    //gVStockProduct.BestFitColumns();

                    ////加载关联宝贝
                    ////int itemRowHandle = gViewItem.FocusedRowHandle;
                    //List<Alading.Entity.Item> itemList = ItemService.GetItem(i => i.outer_id == stockItem.OuterID && i.IsAsociate==true);
                    //gridItem.DataSource = itemList;
                    //gViewItem.BestFitColumns();



                    //if (itemRowHandle == 0 && gViewItem.FocusedRowHandle > -1)
                    //{
                    //    FocusedRowLoadItemProps();
                    //}


                    //dockPanelProps才加载商品属性
                    if (panelContainerItem.ActiveChild != dockPanelProps)
                    {
                        waitFrm.Close();
                        return;
                    }
                    View_ShopItem item = new View_ShopItem();
                    item.cid = stockItem.Cid;
                    item.props = stockItem.Props;
                    item.input_pids = stockItem.InputPids;
                    item.input_str = stockItem.InputStr;
                    item.property_alias = stockItem.Property_Alias;
                    LoadItemPropValue(item, categoryRowKeyProps, categoryRowSaleProps, categoryRowNotKeyProps, categoryRowStockProps);
                    waitFrm.Close();
                }
                catch (Exception ex)
                {
                    waitFrm.Close();
                    XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
                }
            }
            else
            {
                LoadTextEditStockItem(new View_StockItemUnit());
                comboItemType.SelectedIndex = -1;
                comboTax.Text = string.Empty;
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

        void ccmb_EditValueChanged(object sender, EventArgs e)
        {

        }

        void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            vGridCtrl.FocusedRow.Properties.Value = ((DevExpress.XtraEditors.ComboBoxEdit)(sender)).EditValue == null ? string.Empty : ((DevExpress.XtraEditors.ComboBoxEdit)(sender)).EditValue.ToString();
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
                //获得下级的所有值
                List<View_ItemPropValue> propValueList = ItemPropValueService.GetView_ItemPropValueList(cid, pid, vid).ToList();

                View_ItemPropValue propValue = propValueList.FirstOrDefault();
                if (propValue != null)
                {
                    row.Properties.Caption = propValue.prop_name;
                }

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
                        childCmb.TextEditStyle = TextEditStyles.DisableTextEditor;
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


        /// <summary>
        /// 根据不同条件获取StockItem数据
        /// </summary>
        private void GetStockItems()
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                curreentPage = 1;
                itemFlag = true;
                List<View_StockItemUnit> stockItemList = new List<View_StockItemUnit>(StockItemService.GetStockItems(func, curreentPage, pageSize, out totalCount));
                LoadItemList(stockItemList);
                InitPage();
                //根据页码把一些按钮不可用
                pageBtnEnable();
                waitFrm.Close();
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }

        /// <summary>
        /// 根据不同条件获取StockProduct数据
        /// </summary>
        private void GetStockProducts()
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                curreentPage = 1;
                itemFlag = false;
                List<View_StockItemUnit> stockItemList = new List<View_StockItemUnit>(View_StockItemProductService.GetView_StockItemProducts(funcType, curreentPage, pageSize, out totalCount));
                LoadItemList(stockItemList);
                InitPage();
                //根据页码把一些按钮不可用
                pageBtnEnable();
                waitFrm.Close();
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }

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
        /// StockItem查询 加载商品列表
        /// </summary>
        /// <param name="stockItemProductList"></param>
        private void LoadItemList(List<View_StockItemUnit> stockItemList)
        {
            try
            {
                int rowHandle = gvStockItem.FocusedRowHandle;
                gStockItem.DataSource = stockItemList;
                if (rowHandle == 0 && gvStockItem.FocusedRowHandle > -1)
                {
                    //加载商品商品和属性
                    FocusedRowChange();
                }
                gvStockItem.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }

        #region 导出
        /// <summary>
        /// 导出为exl
        /// </summary>
        private void ExportAll()
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "导出Excel";
                saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
                DialogResult dialogResult = saveFileDialog.ShowDialog(this);
                if (dialogResult == DialogResult.OK)
                {
                    DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                    //gridControl1.ExportToXls(saveFileDialog.FileName, options);
                    gridStockProduct.ExportToExcelOld(saveFileDialog.FileName);
                    XtraMessageBox.Show("导出成功！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }
        #endregion


        #endregion

        #region 商品列表操作
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProductAdd productAdd = new ProductAdd();
            productAdd.ShowDialog();
            ShowItemProductInGrid();//刷新
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvStockItem.RowCount > 0)
            {
                ExportAll();
            }
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowItemProductInGrid();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                View_StockItemUnit stockItem = gvStockItem.GetFocusedRow() as View_StockItemUnit;
                if (stockItem != null)
                {
                    if (XtraMessageBox.Show(string.Format("是否删除商品\n{0}", stockItem.Name), Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        //Item里面有的就不能删
                        if (StockItemService.RemoveStockItem(stockItem.StockItemCode) == ReturnType.Success)
                        {
                            XtraMessageBox.Show("删除成功", Constants.SYSTEM_PROMPT);
                            gvStockItem.DeleteRow(gvStockItem.FocusedRowHandle);
                            FocusedRowChange();
                        }
                        else
                        {
                            XtraMessageBox.Show("删除失败", Constants.SYSTEM_PROMPT);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }

        private void gvStockItem_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRowChange();
        }

        #endregion

        private void panelContainerItem_ActiveChildChanged(object sender, DevExpress.XtraBars.Docking.DockPanelEventArgs e)
        {
            FocusedRowChange();
        }

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        private void Search()
        {
            if (textKeyWord.Text == string.Empty)
            {
                XtraMessageBox.Show("请输入关键词", Constants.SYSTEM_PROMPT);
                return;
            }
            func = new Func<View_StockItemUnit, bool>(i => i.OuterID.ToUpper().Contains(textKeyWord.Text.ToUpper()) || i.Name.ToUpper().Contains(textKeyWord.Text.ToUpper())
                || i.SimpleName.ToUpper().Contains(textKeyWord.Text.ToUpper()) || i.Model.ToUpper().Contains(textKeyWord.Text.ToUpper()) || i.Specification.ToUpper().Contains(textKeyWord.Text.ToUpper()));
            GetStockItems();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void navBarControl1_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            //dockPanelProps才加载商品属性
            if (panelContainerItem.ActiveChild != dockPanelProps)
            {
                return;
            }
            Alading.Entity.StockItem stockItem = gvStockItem.GetFocusedRow() as StockItem;
            if (stockItem != null)
            {
                WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                View_ShopItem item = new View_ShopItem();
                item.cid = stockItem.Cid;
                item.props = stockItem.Props;
                item.input_pids = stockItem.InputPids;
                item.input_str = stockItem.InputStr;
                item.property_alias = stockItem.Property_Alias;
                LoadItemPropValue(item, categoryRowKeyProps, categoryRowSaleProps, categoryRowNotKeyProps, categoryRowStockProps);
                waitFrm.Close();
            }
        }

        #endregion

        #region 保存商品属性

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
                if (IsAllNeededPropsInput(vGridCtrl.Rows) == false)
                {
                    XtraMessageBox.Show("打钩的为必填属性", Constants.SYSTEM_PROMPT);
                    return;
                }

                //更新商品属性
                if (XtraMessageBox.Show("是否保存修改?", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Alading.Entity.View_StockItemUnit stockItem = gvStockItem.GetFocusedRow() as Alading.Entity.View_StockItemUnit;
                    if (stockItem != null)
                    {
                        string Props = string.Empty;
                        /*属性串赋值*/
                        Props += UIHelper.GetCategoryRowData(categoryRowKeyProps);
                        Props += UIHelper.GetCategoryRowData(categoryRowNotKeyProps);
                        Props += UIHelper.GetCategoryRowData(categoryRowSaleProps);
                        /*去掉最后一个分号,注意判断是否为空*/
                        if (!string.IsNullOrEmpty(Props) && Props.Contains(";"))
                        {
                            Props = Props.Substring(0, Props.Length - 1);
                        }
                        else if (Props == null)
                        {
                            Props = string.Empty;
                        }
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

                        if (StockItemService.UpdateStockItemProps(stockItem.StockItemCode, Props, stockItem.InputPids, stockItem.InputStr) == ReturnType.Success)
                        {
                            XtraMessageBox.Show("保存修改成功", Constants.SYSTEM_PROMPT);
                            //不行修改,这个View不能修改~~~主键已改
                            stockItem.Props = Props;
                            barBtnPropsModify.Enabled = false;
                        }
                        else
                        {
                            XtraMessageBox.Show("保存修改失败", Constants.SYSTEM_PROMPT);
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
        /// 保存属性信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnPropsModify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveModify();
        }
        /// <summary>
        /// 保存基本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleBtnModify_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvStockItem.RowCount == 0)
                    return;
                if (textName.Text.Trim() == string.Empty || textEditOutID.Text.Trim() == string.Empty || pceItemCat.Text.Trim() == string.Empty
                   || pceStockCat.Text == string.Empty || popupUnit.Text.Trim() == string.Empty || comboItemType.Text == string.Empty || comboTax.Text == string.Empty)
                {
                    XtraMessageBox.Show("带*号的为必填项", Constants.SYSTEM_PROMPT);
                    return;
                }

                #region StockItem
                //View_StockItemUnit viewStockItem = gvStockItem.GetFocusedRow() as View_StockItemUnit;
                //if (viewStockItem == null)
                //    return;
                View_StockItemUnit stockItem = gvStockItem.GetFocusedRow() as View_StockItemUnit;
                if (stockItem == null)
                    return;
                if (XtraMessageBox.Show("是否保存修改?", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    //StockItem stockItem = GetData(viewStockItem);
                    stockItem.StockItemRemark = memoExDesc.Text;
                    stockItem.CatName = pceItemCat.Text;
                    stockItem.Cid = pceItemCat.Tag == null ? string.Empty : pceItemCat.Tag.ToString();
                    stockItem.Modified = DateTime.Now;
                    stockItem.Model = textEditModel.Text;
                    stockItem.Name = textName.Text;
                    stockItem.OuterID = textEditOutID.Text;
                    /*产品类别，要注意下拉items的顺序与枚举类一致 ,从1开始*/
                    stockItem.StockItemType = comboItemType.SelectedIndex +1;
                    stockItem.PicUrl = string.Empty;///////////
                    stockItem.SimpleName = textEditSimpleName.Text;
                    stockItem.Specification = textEditSpecification.Text;
                    stockItem.TaxName = comboTax.Text;
                    stockItem.StockCheckUrl = mruStockWebSite.Text;
                    stockItem.StockCatName = pceStockCat.Text;
                    stockItem.StockCid = pceStockCat.Tag != null ? pceStockCat.Tag.ToString() : string.Empty; ;
                    stockItem.UnitCode = popupUnit.Tag != null ? popupUnit.Tag.ToString() : string.Empty;
                    //if (comboTax.Tag != null)
                    //{
                    //    string[] codeList = comboTax.Tag.ToString().Split(',');
                    //    if (codeList.Length > comboTax.SelectedIndex)
                    //    {
                    //        stockItem.Tax = codeList[comboTax.SelectedIndex];
                    //    }
                    //}
                    stockItem.Tax = TaxCode;

                    //代销1是0否
                    if (radioGroupIsConsignment.SelectedIndex == 1)
                    {
                        stockItem.IsConsignment = true;
                    }
                    else
                    {
                        stockItem.IsConsignment = false;
                    }

                    //#region 保存图片
                    //if (pictureEditPic.Image != null)
                    //{
                    //    Picture pic = new Picture();
                    //    pic.OuterID = stockItem.OuterID;
                    //    pic.PictureCode = Guid.NewGuid().ToString();
                    //    pic.PictureRemark = string.Empty;
                    //    pic.PictureTitle = string.Empty;
                    //    pic.PictureContent = SystemHelper.GetImageBytes(pictureEditPic.Image);
                    //    PictureService.AddPicture(pic);
                    //}
                    //#endregion


                    //int length = popupUnit.Text.Length;
                    //if (popupUnit.Text.LastIndexOf('(') != -1)
                    //{
                    //    stockItem.StockUnitName = popupUnit.Text.Substring(0, length - popupUnit.Text.LastIndexOf('(') + 1);
                    //}
                    //else
                    //{
                    //    stockItem.StockUnitName = popupUnit.Text;
                    //}

                    ReturnType type=StockItemService.UpdateStockItem(stockItem.StockItemCode, stockItem);
                    if (type == ReturnType.Success)
                    {
                        gvStockItem.SetFocusedRowCellValue("Model", textEditModel.Text);
                        gvStockItem.SetFocusedRowCellValue("Specification", textEditSpecification.Text);
                        gvStockItem.SetFocusedRowCellValue("StockUnitName", popupUnit.Text);
                        XtraMessageBox.Show("保存修改成功", Constants.SYSTEM_PROMPT);
                        simpleBtnModify.Enabled = false;
                    }
                    else if (type == ReturnType.PropertyExisted)
                    {
                        XtraMessageBox.Show("商家编码重复,请重输", Constants.SYSTEM_PROMPT);
                        textEditOutID.Focus();
                    }
                    else
                    {
                        XtraMessageBox.Show("保存修改失败", Constants.SYSTEM_PROMPT);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT);
            }
        }

        //private StockItem GetData(View_StockItemUnit stockitem)
        //{
        //    StockItem ob = new StockItem();
        //    ob.StockItemCode = stockitem.StockItemCode;
        //    ob.OuterID = stockitem.OuterID;
        //    ob.UnitCode = stockitem.UnitCode;
        //    ob.Specification = stockitem.Specification;
        //    ob.Model = stockitem.Model;
        //    ob.TaxName = stockitem.TaxName;
        //    ob.Tax = stockitem.Tax;
        //    ob.ProductID = stockitem.ProductID;
        //    ob.Name = stockitem.Name;
        //    ob.SimpleName = stockitem.SimpleName;
        //    ob.Cid = stockitem.Cid;
        //    ob.CatName = stockitem.CatName;
        //    ob.StockCid = stockitem.StockCid;
        //    ob.StockCatName = stockitem.StockCatName;
        //    ob.StockProps = stockitem.StockProps;
        //    ob.KeyProps = stockitem.KeyProps;
        //    ob.NotKeyProps = stockitem.NotKeyProps;
        //    ob.SaleProps = stockitem.SaleProps;
        //    ob.HasSaleProps = stockitem.HasSaleProps;
        //    ob.StockProps = stockitem.StockProps;
        //    ob.InputPids = stockitem.InputPids;
        //    ob.InputStr = stockitem.InputStr;
        //    ob.PicUrl = stockitem.PicUrl;
        //    ob.StockItemImgs = stockitem.StockItemImgs;
        //    ob.TotalQuantity = stockitem.TotalQuantity;
        //    ob.IsConsignment = stockitem.IsConsignment;
        //    ob.StockCheckUrl = stockitem.StockCheckUrl;
        //    ob.Created = stockitem.Created;
        //    ob.Modified = stockitem.Modified;
        //    ob.StockItemRemark = stockitem.StockItemRemark;
        //    ob.StockItemType = stockitem.StockItemType;
        //    ob.Props = stockitem.Props;
        //    ob.Property_Alias = stockitem.Property_Alias;
        //    return ob;
        //}

        /// <summary>
        /// 保存销售信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnSaleModify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (skuOuterIDChangeTag == true)
                {   
                    XtraMessageBox.Show("商家编码有重复,请重输", Constants.SYSTEM_PROMPT);
                    return;
                }
                List<Alading.Entity.StockProduct> stockProductList = new List<Alading.Entity.StockProduct>(gVStockProduct.DataSource as List<Alading.Entity.StockProduct>);
                if (stockProductList != null && stockProductList.Count > 0)
                {
                    if (XtraMessageBox.Show("是否保存修改?", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        View_StockItemUnit stockItem = gvStockItem.GetFocusedRow() as View_StockItemUnit;
                        if (stockItem != null)
                        {
                            if (StockItemService.UpdateStockItem(stockItem.OuterID, stockProductList) == ReturnType.Success)
                            {
                                XtraMessageBox.Show("保存修改成功", Constants.SYSTEM_PROMPT);
                                barBtnSaleModify.Enabled = false;
                            }
                            else
                            {
                                XtraMessageBox.Show("保存修改失败", Constants.SYSTEM_PROMPT);
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

        #endregion

        #region 保存修改按钮可用

        private void gVStockProduct_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //保存修改按钮可用
            barBtnSaleModify.Enabled = true;
        }

        private void vGridControlProperty_CellValueChanging(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            //保存修改按钮可用
            barBtnPropsModify.Enabled = true;
            e.Row.Properties.Value = e.Value;
            //if (e.Row.ParentRow == categoryRowSaleProps)
            //{
            //    if (categoryRowSaleProps.ChildRows.Count == 2)
            //    {
            //        if ((categoryRowSaleProps.ChildRows[0].Properties.Value == null || categoryRowSaleProps.ChildRows[0].Properties.Value.ToString() == string.Empty) &&
            //            (categoryRowSaleProps.ChildRows[1].Properties.Value == null || categoryRowSaleProps.ChildRows[1].Properties.Value.ToString() == string.Empty))
            //        {
            //        }
            //        else
            //        {
            //            XtraMessageBox.Show("销售属性不能只输入一个", Constants.SYSTEM_PROMPT);
            //        }
            //    }
            //}          

        }

        private void textEditStockItemName_EditValueChanged(object sender, EventArgs e)
        {
            simpleBtnModify.Enabled = true;
            textEditSimpleName.Text = UIHelper.GetChineseSpell(textName.Text);
        }

        private void textEditSimpleName_EditValueChanged(object sender, EventArgs e)
        {
            simpleBtnModify.Enabled = true;
        }

        private void textEditOutID_EditValueChanged(object sender, EventArgs e)
        {
            simpleBtnModify.Enabled = true;
            //动态产生条形码
            Image myimg = Code128Rendering.MakeBarcodeImage(textEditOutID.Text.Trim(), 1, true);
            picBarCodeImage.Image = myimg;
        }

        private void textEditModel_EditValueChanged(object sender, EventArgs e)
        {
            simpleBtnModify.Enabled = true;
        }

        private void textEditSpecification_EditValueChanged(object sender, EventArgs e)
        {
            simpleBtnModify.Enabled = true;
        }

        private void pceItemCat_EditValueChanged(object sender, EventArgs e)
        {
            simpleBtnModify.Enabled = true;
        }

        private void comboItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            simpleBtnModify.Enabled = true;
        }

        private void mruUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            simpleBtnModify.Enabled = true;
        }

        private void pceStockCat_EditValueChanged(object sender, EventArgs e)
        {
            simpleBtnModify.Enabled = true;
        }

        private void mruTaxRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            simpleBtnModify.Enabled = true;
        }

        //private void mruStockWebSite_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    simpleBtnModify.Enabled = true;
        //}

        private void radioGroupIsConsignment_SelectedIndexChanged(object sender, EventArgs e)
        {
            simpleBtnModify.Enabled = true;
        }

        #endregion

        private void vGridControlProperty_Leave(object sender, EventArgs e)
        {

        }

        private void comboTax_SelectedIndexChanged(object sender, EventArgs e)
        {
            simpleBtnModify.Enabled = true;
            if (comboTax.Tag != null)
            {
                string[] codeList = comboTax.Tag.ToString().Split(',');
                if (comboTax.SelectedIndex != -1 && codeList.Length > comboTax.SelectedIndex)
                {
                    TaxCode = codeList[comboTax.SelectedIndex];
                }
            }
        }

        private void popupUnit_EditValueChanged(object sender, EventArgs e)
        {
            simpleBtnModify.Enabled = true;
        }

        private void tlStockCat_Click(object sender, EventArgs e)
        {
            TreeListHitInfo hitInfo = tlStockCat.CalcHitInfo(((System.Windows.Forms.MouseEventArgs)(e)).Location);

            /*如果单击到单元格内*/
            if (hitInfo.HitInfoType == HitInfoType.Cell)
            {
                if (hitInfo.Node != null && !hitInfo.Node.HasChildren)
                {
                    TreeListNodeTag tag = hitInfo.Node.Tag as TreeListNodeTag;
                    pceStockCat.Text = hitInfo.Node.GetDisplayText(0);
                    /*绑定CID以便下面读取*/
                    pceStockCat.Tag = tag.Cid;
                    pceStockCat.ClosePopup();
                }
            }
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

        private void gVStockProduct_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SkuOuterID")
            {
                Alading.Entity.StockProduct productSrc = gVStockProduct.GetRow(e.RowHandle) as Alading.Entity.StockProduct;
                Alading.Entity.StockProduct product = StockProductService.GetStockProduct(productSrc.SkuOuterID);
                if (product != null && (product.OuterID != productSrc.OuterID || product.SkuProps != productSrc.SkuProps))
                {
                    XtraMessageBox.Show("商家编码重复,请重输", Constants.SYSTEM_PROMPT);
                    gVStockProduct.FocusedRowHandle = e.RowHandle;
                    skuOuterIDChangeTag = true;
                }
                else
                {
                    skuOuterIDChangeTag = false;
                }
            }
        }
    

        private void gVStockProduct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //if (skuOuterIDChangeTag == true)
            //{
            //    //Alading.Entity.StockProduct productSrc = gVStockProduct.GetRow(e.PrevFocusedRowHandle) as Alading.Entity.StockProduct;
            //    //Alading.Entity.StockProduct product = StockProductService.GetStockProduct(productSrc.SkuOuterID);
            //    //if (product != null && (product.OuterID != productSrc.OuterID || product.SkuProps != productSrc.SkuProps))
            //    //{
            //    XtraMessageBox.Show("商家编码重复,请重输", Constants.SYSTEM_PROMPT);
            //    //gVStockProduct.FocusedRowHandle = e.PrevFocusedRowHandle;
            //    //}
            //    //skuOuterIDChangeTag = false;
            //}
        }

        private void memoExDesc_EditValueChanged(object sender, EventArgs e)
        {
            simpleBtnModify.Enabled = true;
        }

        private void mruStockWebSite_EditValueChanged(object sender, EventArgs e)
        {
            simpleBtnModify.Enabled = true;
        }

        private void popupUnit_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                try
                {
                    NewUnitForm newUnit = new NewUnitForm();
                    newUnit.ShowDialog();
                    //刷新
                    InitUnit();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //private void pceStockCat_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        //{
        //    if (e.Button.Index == 1)
        //    {
        //        try
        //        {
        //            //ModifyStockCat stock = new ModifyStockCat();
        //            //stock.ShowDialog();
        //            ////刷新
        //            //InitStockCat();
        //        }
        //        catch (Exception ex)
        //        {
        //            XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //}

        private void comboTax_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                try
                {
                    TaxRateForm trf = new TaxRateForm();
                    trf.ShowDialog();
                    //刷新
                    InitTax();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                Alading.Entity.View_StockItemUnit stockItem = gvStockItem.GetFocusedRow() as View_StockItemUnit;
                if (stockItem == null)
                {
                    return;
                }
                //处理图片
                HandlePic(stockItem.OuterID);
                
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

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
                    List<string> iidList = new List<string>();
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

        private void textKeyWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search();
            }
        }
    }
}