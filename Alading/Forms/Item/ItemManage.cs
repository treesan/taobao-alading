using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using Alading.Taobao.Entity.Extend;
using Alading.Taobao.API;
using Alading.Business;
using Alading.Core.Enum;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Alading.Taobao; 
using Alading.Utils;
using Alading.Entity;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Collections;
using System.Linq;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using Alading.Taobao.API.Common;
using DevExpress.XtraGrid;
using Alading.Core.Code128;
using DevExpress.XtraGrid.Views.Grid;

namespace Alading.Forms.Item
{
    public delegate Image ItemImageDelegate(string shopSid, string picUrl);

    public partial class ItemManage : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 所有卖家自定义类目
        /// </summary>
        List<Alading.Entity.SellerCat> listSellerCats = new List<Alading.Entity.SellerCat>();
        /// <summary>
        /// 当前宝贝所属店铺Nick
        /// </summary>
        string sellerNick = string.Empty;
        /// <summary>
        /// 宝贝销售属性列表
        /// </summary>
        DataTable saleInfoTable = new DataTable();
        /// <summary>
        /// 销售属性表gridviewsku新增加列明
        /// </summary>
        List<string> listNewColumns = new List<string>();

        public ItemManage()
        {
            InitializeComponent();
        }

        public ItemManage(WaitDialogForm waitForm)
        {
            InitializeComponent();
            waitForm.Hide();
        }

        private void ItemManage_Load(object sender, EventArgs e)
        {
           
            //加载商品自定义类目列表
            LoadSelllerCatList();
            //加载计量单位
            //LoadStockUnit();
        }

        # region 对treeList操作

        # region  popup淘宝类目
        private void popupContainerEditTBCat_Popup(object sender, EventArgs e)
        {
            //加载商品淘宝类目
            UIHelper.LoadItemCat(treeListItemCat);
        }

        /// <summary>
        /// 获得当前节点的子节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListItemCat_BeforeExpand(object sender, BeforeExpandEventArgs e)
        {
            TreeListNode focusedNode = e.Node;
            treeListItemCat.FocusedNode = focusedNode;
            TreeListNodeTag tag = focusedNode.Tag as TreeListNodeTag;

            #region 获得当前节点的子节点
            if (!tag.HasExpanded)
            {
                treeListItemCat.BeginUnboundLoad();
                List<Alading.Entity.ItemCat> itemCatList = ItemCatService.GetItemCat(tag.Cid ,"normal");

                foreach (Alading.Entity.ItemCat itemCat in itemCatList)
                {
                    TreeListNode node = treeListItemCat.AppendNode(new object[] { itemCat.name }, focusedNode, new TreeListNodeTag(itemCat.cid.ToString()));
                    node.HasChildren = (bool)itemCat.is_parent;
                }
                treeListItemCat.EndUnboundLoad();
                tag.HasExpanded = true;
            }
            #endregion
        }

        /// <summary>
        /// 修改商品淘宝类目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListItemCat_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                TreeListHitInfo hitInfor = treeListItemCat.CalcHitInfo(new Point(e.X, e.Y));
                TreeListNode focusedNode = hitInfor.Node;
                if (focusedNode == null || focusedNode.Tag == null)
                {
                    return;
                }
                if (focusedNode.HasChildren)
                {
                    return;
                }
                TreeListNodeTag tag = focusedNode.Tag as TreeListNodeTag;
                string taobaoCat = string.Empty;
                do
                {
                    taobaoCat = focusedNode.GetDisplayText(0) + "/" + taobaoCat;
                    popupContainerEditTBCat.Text = taobaoCat.TrimEnd('/');
                    focusedNode = focusedNode.ParentNode;
                } while (focusedNode != null);
                popupContainerEditTBCat.Tag = tag.Cid;
                popupContainerEditTBCat.ClosePopup();
                //加载该淘宝类目下商品的属性
                LoadItemPropValue(tag.Cid);
            }
        }
        # endregion//popup淘宝类目

        #region popup卖家自定义类目
        /// <summary>
        /// 加载卖家自定义类目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupContainerEditSellerCat_Popup(object sender, EventArgs e)
        {
            if (treeListSellerCat.Nodes.FirstNode != null && treeListSellerCat.Nodes.FirstNode.Tag != null)
            {
                string tag = treeListSellerCat.Nodes.FirstNode.Tag.ToString();
                string nick = tag.Substring(tag.IndexOf(',') + 1);
                if (nick == sellerNick)
                {
                    return;
                }
                else
                {
                    treeListSellerCat.ClearNodes();
                }
            }
            List<Alading.Entity.SellerCat> listSellerCat = listSellerCats.FindAll(a => a.SellerNick == sellerNick);
            if (listSellerCat != null)
            {
                foreach (Alading.Entity.SellerCat sellerCat in listSellerCat)
                {
                    if (sellerCat.parent_cid == "0")
                    {
                        TreeListNode childNode = treeListSellerCat.AppendNode(new object[] { sellerCat.name }, null);
                        childNode.Tag = sellerCat.cid + "," + sellerNick;
                        AppendNodes(sellerNick, sellerCat.cid, childNode, treeListSellerCat, listSellerCat);
                    }
                }
                treeListSellerCat.ExpandAll();
                #region 在卖家自定义类目列表中对属于宝贝的自定义类目的点选中
                if (!string.IsNullOrEmpty(popupContainerEditSellerCat.Text))
                {
                    string[] sellercatsArray = popupContainerEditSellerCat.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string sellercat in sellercatsArray)
                    {
                        foreach (TreeListNode node in treeListSellerCat.Nodes)
                        {
                            if (!node.HasChildren)
                            {
                                if (node.GetDisplayText(0) == sellercat)
                                {
                                    node.Checked = true;
                                    break;
                                }
                            }
                            else
                            {
                                foreach (TreeListNode childNode in node.Nodes)
                                {
                                    if (childNode.GetDisplayText(0) == sellercat)
                                    {
                                        childNode.Checked = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
            }
        }

        private void popupContainerEditSellerCat_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            string sellercat = string.Empty;
            string sellercid = string.Empty;
            foreach (TreeListNode node in treeListSellerCat.Nodes)
            {
                if (!node.HasChildren && node.Checked)
                {
                    sellercat += node.GetDisplayText(0) + ",";
                    if (node.Tag != null)
                    {
                        string tag = node.Tag.ToString();
                        sellercid += tag.Substring(0, tag.IndexOf(',')) + ",";
                    }
                }
                else if (node.HasChildren)
                {
                    foreach (TreeListNode chidnode in node.Nodes)
                    {
                        if (chidnode.Checked)
                        {
                            sellercat += chidnode.GetDisplayText(0) + ",";
                            if (node.Tag != null)
                            {
                                string tag = node.Tag.ToString();
                                sellercid += tag.Substring(0, tag.IndexOf(',')) + ",";
                            }
                        }
                    }
                }
            }
            popupContainerEditSellerCat.Text = sellercat.Trim(',');
            popupContainerEditSellerCat.Tag = sellercid.Trim(',');
        }

        private void treeListSellerCat_AfterCheckNode(object sender, NodeEventArgs e)
        {
            if (e.Node.HasChildren)
            {
                e.Node.ExpandAll();
                foreach (TreeListNode node in e.Node.Nodes)
                {
                    node.Checked = e.Node.Checked;
                }
            }
        }
        #endregion//popup卖家自定义类目

        # region 卖家自定义类目列表
        /// <summary>
        /// 加载商品类目
        /// </summary>
        private void LoadSelllerCatList()
        {
            try
            {
                treeListShop.Nodes.Clear();
                List<Alading.Entity.Shop> listShop = ShopService.GetAllShop();
                if (listShop != null)
                {
                    //加载店铺
                    foreach (Alading.Entity.Shop shop in listShop)
                    {
                        checkedComboBoxEditShop.Properties.Items.Add(shop.nick);
                    }
                }
                treeListShop.AppendNode(new object[] { "本地库存中的普通商品" }, null, "local");
                treeListShop.AppendNode(new object[] { "本地库存中的组合商品" }, null, "local");
                listSellerCats = SellerCatService.GetAllSellerCat();
                if (listShop != null && listSellerCats != null)
                {
                    foreach (Alading.Entity.Shop shop in listShop)
                    {
                        TreeListNode shopNode = treeListShop.AppendNode(new object[] { shop.nick }, -1);
                        treeListShop.AppendNode(new object[] { "异常的宝贝" }, shopNode, shop.nick);
                        treeListShop.AppendNode(new object[] { "回收站" }, shopNode, shop.nick);
                        treeListShop.AppendNode(new object[] { "橱窗中的宝贝" }, shopNode, shop.nick);
                        TreeListNode saleNode = treeListShop.AppendNode(new object[] { "出售中的宝贝" }, shopNode, shop.nick);
                        saleNode.HasChildren = true;
                        //不属于卖家自定义类目宝贝的新建类目
                        treeListShop.AppendNode(new object[] { "其他" }, saleNode, shop.nick);
                        AddNodes(shop.nick, saleNode, treeListShop);
                        treeListShop.AppendNode(new object[] { "仓库中的宝贝" }, shopNode, shop.nick);
                        //TreeListNode campaignNode = treeListShop.AppendNode(new object[] { "参与活动的宝贝" }, shopNode, shop.nick);
                        //treeListShop.AppendNode(new object[] { "折扣活动" }, campaignNode, shop.nick);
                        //treeListShop.AppendNode(new object[] { "送积分活动" }, campaignNode, shop.nick);
                        //treeListShop.AppendNode(new object[] { "全场活动" }, campaignNode, shop.nick);
                        //treeListShop.AppendNode(new object[] { "免邮费活动" }, campaignNode, shop.nick);
                        //treeListShop.AppendNode(new object[] { "满就送活动" }, campaignNode, shop.nick);
                        //treeListShop.AppendNode(new object[] { "满就钱购活动" }, campaignNode, shop.nick);
                        //treeListShop.AppendNode(new object[] { "抵积分活动" }, campaignNode, shop.nick);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 向treeList中加载出售中的商品的类目
        /// </summary>
        /// <param name="shopNick">店铺昵称</param>
        /// <param name="rootNode">父节点</param>
        /// <param name="treeListShop">被加载的控件treeListShop</param>
        private void AddNodes(string shopNick, TreeListNode rootNode, TreeList treeListShop)
        {
            List<Alading.Entity.SellerCat> listSellerCat = listSellerCats.FindAll(delegate(Alading.Entity.SellerCat s) { return s.SellerNick == shopNick; });
            if (listSellerCat != null)
            {
                foreach (Alading.Entity.SellerCat sellerCat in listSellerCat)
                {
                    if (sellerCat.parent_cid == "0")
                    {
                        TreeListNode childNode = treeListShop.AppendNode(new object[] { sellerCat.name }, rootNode);
                        childNode.Tag = sellerCat.cid + "," + shopNick;
                        AppendNodes(shopNick, sellerCat.cid, childNode, treeListShop, listSellerCat);
                    }
                }
            }
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="sellerCatCid">父节点Cid</param>
        /// <param name="node1">父节点</param>
        /// <param name="treeListShop">被加载的控件treeListShop</param>
        /// <param name="listSellerCat">所有类目</param>
        private void AppendNodes(string shopNick, string sellerCatCid, TreeListNode rootNode, TreeList treeListShop, List<Alading.Entity.SellerCat> listSellerCat)
        {
            foreach (Alading.Entity.SellerCat sellerCat in listSellerCat)
            {
                if (sellerCat.parent_cid == sellerCatCid)
                {
                    TreeListNode childNode = treeListShop.AppendNode(new object[] { sellerCat.name }, rootNode);
                    childNode.Tag = sellerCat.cid + "," + shopNick;
                    AppendNodes(shopNick, sellerCat.cid, childNode, treeListShop, listSellerCat);
                }
            }
        }

        /// <summary>
        /// 类目改变
        /// </summary>
        private void treeListShop_MouseDown(object sender, MouseEventArgs e)
        {
            gcStock.DataSource = null;
            gcStockProduct.DataSource = null;
            gridControlItem.DataSource = null;
            gridControlSku.DataSource = null;
            TreeListHitInfo hitInfo = treeListShop.CalcHitInfo(new Point(e.X, e.Y));
            //如果单击到单元格内
            if (hitInfo.HitInfoType == HitInfoType.Cell)
            {
                gpcItem.Text = "出售中宝贝信息";
                int rowHandle = gridViewItem.FocusedRowHandle;
                TreeListNode ClickedNode = hitInfo.Node;
                string sellercid = string.Empty;
                string nick = string.Empty;
                if (ClickedNode == null || ClickedNode.Tag == null)
                {
                    return;
                }
                WaitDialogForm waitForm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                try
                {
                    switch (ClickedNode.GetDisplayText(0))
                    {
                        case "出售中的宝贝":
                            btnModify.Enabled = true;
                            btnUpdateDown.Enabled = true;
                            btnDeleteItem.Enabled = true;
                            cbItemType.Enabled = true;
                            gridControlItem.MainView = gridViewItem;
                            nick = ClickedNode.Tag.ToString();
                            sellercid = null;
                            GetShopItemByStatus(nick, "onsale", false);
                            LoadFirstRow(rowHandle);
                            break;
                        case "仓库中的宝贝":
                            gpcItem.Text = "仓库中宝贝信息";
                            btnModify.Enabled = true;
                            btnUpdateDown.Enabled = true;
                            btnDeleteItem.Enabled = true;
                            cbItemType.Enabled = true;
                            gridControlItem.MainView = gridViewItem;
                            nick = ClickedNode.Tag.ToString();
                            sellercid = null;
                            GetShopItemByStatus(nick, "instock", false);
                            LoadFirstRow(rowHandle);
                            break;
                        case "其他":
                            btnModify.Enabled = true;
                            btnUpdateDown.Enabled = true;
                            btnDeleteItem.Enabled = true;
                            cbItemType.Enabled = true;
                            gridControlItem.MainView = gridViewItem;
                            nick = ClickedNode.Tag.ToString();
                            sellercid = string.Empty;
                            ShowItemInGridView(sellercid, nick, "onsale");
                            LoadFirstRow(rowHandle);
                            break;
                        case "本地库存中的普通商品":
                            gpcItem.Text = "仓库中普通商品信息";
                            gcStockProduct.DataSource = null;
                            btnModify.Enabled = false;
                            btnUpdateDown.Enabled = false;
                            btnDeleteItem.Enabled = false;
                            cbItemType.Enabled = true;
                            gridControlItem.MainView = gridViewStockItem;
                            GetLocalStockItem();
                            if (gridViewStockItem.FocusedRowHandle == 0)
                            {
                                //加载商品信息
                                LoadStockitemProductInfor();
                            }
                            break;
                        case "本地库存中的组合商品":
                            gpcItem.Text = "仓库中组合商品信息";
                            cbItemType.Enabled = false;
                            btnModify.Enabled = false;
                            btnUpdateDown.Enabled = false;
                            btnDeleteItem.Enabled = false;
                            comboBoxEditFreight.Enabled = false;
                            gridControlItem.MainView = gridViewAssemble;
                            GetAssembleItems();
                            if (gridViewAssemble.FocusedRowHandle == 0)
                            {
                                LoadAssembleItem();
                            }
                            LoadBaseUnit();
                            break;
                        case "回收站":
                            btnModify.Enabled = false;
                            btnUpdateDown.Enabled = false;
                            btnDeleteItem.Enabled = false;
                            cbItemType.Enabled = true;
                            gridControlItem.MainView = gridViewItem;
                            nick = ClickedNode.Tag.ToString();
                            LoadHistoryItem(nick, true);
                            LoadFirstRow(rowHandle);
                            break;
                        case "橱窗中的宝贝":
                            btnModify.Enabled = true;
                            btnUpdateDown.Enabled = true;
                            btnDeleteItem.Enabled = true;
                            cbItemType.Enabled = true;
                            gridControlItem.MainView = gridViewItem;
                            nick = ClickedNode.Tag.ToString();
                            LoadShowCaseItems(nick, false, true);
                            LoadFirstRow(rowHandle);
                            break;
                        case "异常的宝贝":
                            btnModify.Enabled = true;
                            btnUpdateDown.Enabled = true;
                            btnDeleteItem.Enabled = true;
                            cbItemType.Enabled = true;
                            gridControlItem.MainView = gridViewItem;
                            break;
                        default:
                            btnModify.Enabled = true;
                            btnUpdateDown.Enabled = true;
                            btnDeleteItem.Enabled = true;
                            cbItemType.Enabled = true;
                            gridControlItem.MainView = gridViewItem;
                            string[] arr = ClickedNode.Tag.ToString().Split(',');
                            if (arr.Length == 2)
                            {
                                sellercid = arr[0];
                                nick = arr[1];
                                if (!ClickedNode.HasChildren)
                                {
                                    ShowItemInGridView(sellercid, nick, "onsale");
                                }
                                else
                                {
                                    //当前卖家自定义类目有子类目，获取子类目
                                    List<string> listChildCid = new List<string>();
                                    GetAllChildNode(listChildCid, nick, sellercid);
                                    ShowItem(listChildCid, nick, "onsale");
                                }
                                LoadFirstRow(rowHandle);
                            }
                            break;
                    }
                    waitForm.Close();
                }
                catch (Exception ex)
                {
                    waitForm.Close();
                    XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion//卖家自定义类目列表

        #region popup加载地域
        private void cmbLocation_Popup(object sender, EventArgs e)
        {
            treeListArea.BeginUnboundLoad();
            List<Alading.Entity.Area> listArea = AreaService.GetArea(p => p.parent_id == "1");
            foreach (Alading.Entity.Area area in listArea)
            {
                TreeListNode provinceNode = treeListArea.AppendNode(new object[] { area.name }, null, new TreeListNodeTag(area.id));
                provinceNode.HasChildren = true;
            }
            treeListArea.EndUnboundLoad();
        }

        private void treeListArea_BeforeExpand(object sender, BeforeExpandEventArgs e)
        {
            TreeListNode focusedNode = e.Node;
            treeListArea.FocusedNode = focusedNode;
            TreeListNodeTag tag = focusedNode.Tag as TreeListNodeTag;

            #region 获得当前节点的子节点
            if (!tag.HasExpanded)
            {
                treeListArea.BeginUnboundLoad();
                List<Alading.Entity.Area> listArea = AreaService.GetArea(p => p.parent_id == tag.Cid);

                foreach (Alading.Entity.Area area in listArea)
                {
                    TreeListNode node = treeListArea.AppendNode(new object[] { area.name }, focusedNode, new TreeListNodeTag(area.id));
                    if ((int)area.type != 4 && (int)area.type != 3)
                    {
                        node.HasChildren = true;
                    }
                }
                treeListArea.EndUnboundLoad();
                tag.HasExpanded = true;
            }
            #endregion
        }

        /// <summary>
        /// 修改买家地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListArea_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                TreeListHitInfo hitInfor = treeListArea.CalcHitInfo(new Point(e.X, e.Y));
                TreeListNode focusedNode = hitInfor.Node;
                string area = string.Empty;
                if (focusedNode != null && !focusedNode.HasChildren)
                {
                    do
                    {
                        area = focusedNode.GetDisplayText(0) + "/" + area;
                        popupContainerEditLocation.Text = area.TrimEnd('/');
                        focusedNode = focusedNode.ParentNode;
                    } while (focusedNode != null);
                    popupContainerEditLocation.ClosePopup();
                }
            }
        }
        # endregion//popup加载地域

        # endregion//对treeList操作

        # region 宝贝下载、新建、更新、删除
        /// <summary>
        /// 下载店铺宝贝
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDowmAllItems_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //string session = SystemHelper.GetSessionKey("aliforfree");
            Item.ItemsDown downForm = new Item.ItemsDown();
            downForm.ShowDialog();
        }
        /// <summary>
        /// 长传新建宝贝
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //检查必填信息是否填写完整
                bool isNullEmpty = string.IsNullOrEmpty(cmbItemTitle.Text) || string.IsNullOrEmpty(popupContainerEditTBCat.Text) || string.IsNullOrEmpty(textEditOuterId.Text);
                bool isnullEmpty = string.IsNullOrEmpty(cmbStaffStatus.Text) || string.IsNullOrEmpty(calcEditNum.Text) || string.IsNullOrEmpty(comboBoxEditTradeType.Text) || string.IsNullOrEmpty(popupContainerEditLocation.Text);
                if (isnullEmpty && isNullEmpty)
                {
                    XtraMessageBox.Show(Constants.ERROR_SHORT, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //在库存中查找是否有与该宝贝关联的商品
                List<Alading.Entity.StockItem> stockitemList = StockItemService.GetStockItem(p => p.OuterID == textEditOuterId.Text.Trim());
                if (stockitemList == null)
                {
                    return;
                }
                if (stockitemList.Count == 0)
                {
                    XtraMessageBox.Show(Constants.ERROR_REPORT, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                
                List<Taobao.Entity.Sku> skulist = new List<Taobao.Entity.Sku>();
                string propAlias = string.Empty;
                ItemReq itemreq = new ItemReq();
                BuildUpdateParameter(itemreq);
                BuildSkuAndAlias(skulist, ref propAlias);
                itemreq.PropAlias = propAlias;
                string skuprops = string.Empty;
                string skuprices = string.Empty;
                string skuquatites = string.Empty;
                string skuouterIds = string.Empty;
                foreach (Taobao.Entity.Sku sku in skulist)
                {
                    skuprops += String.Format("{0},", sku.SkuProps);
                    skuprices += String.Format("{0},", sku.Price);
                    skuquatites += String.Format("{0},", sku.Quantity);
                    skuouterIds += String.Format("{0},", sku.OuterId);
                }
                itemreq.SkuProperties = string.IsNullOrEmpty(skuprops) ? string.Empty : skuprops.TrimEnd(',');
                itemreq.SkuQuantities = string.IsNullOrEmpty(skuquatites) ? string.Empty : skuquatites.TrimEnd(',');
                itemreq.Skuprices = string.IsNullOrEmpty(skuprices) ? string.Empty : skuprices.TrimEnd(',');
                itemreq.SkuOuterIds = string.IsNullOrEmpty(skuouterIds) ? string.Empty : skuouterIds.TrimEnd(',');
                ItemAdd addFrm = new ItemAdd(itemreq);
                addFrm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 上传修改宝贝
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //获得当前选中的行的DataRow数据
                View_ShopItem viewItem = gridViewItem.GetFocusedRow() as View_ShopItem;
                if (viewItem == null)
                {
                    XtraMessageBox.Show("没有选中任何宝贝！");
                    return;
                }
                List<Taobao.Entity.Sku> skulist = new List<Taobao.Entity.Sku>();
                string propAlias = string.Empty;

                ItemReq itemreq = new ItemReq();
                itemreq.Iid = viewItem.iid;
                // BuildUpdateParameter(itemreq);
                BuildSkuAndAlias(skulist, ref propAlias);
                itemreq.PropAlias = propAlias;
                string session = SystemHelper.GetSessionKey(viewItem.nick);

                ItemRsp updateRsp = null;
                foreach (Taobao.Entity.Sku sku in skulist)
                {
                    updateRsp = TopService.ItemSkuUpate(session, viewItem.iid, sku.SkuProps, sku.Quantity, sku.Price, sku.OuterId, null);
                }

                // ItemRsp updateRsp = TopService.ItemUpdate(session, itemreq);

                if (updateRsp != null && updateRsp.Item != null)
                {
                    //string skuprops = string.Empty;
                    //string skuprices = string.Empty;
                    //string skuquatites = string.Empty;
                    //string skuouterIds = string.Empty;
                    //foreach (Taobao.Entity.Sku sku in skulist)
                    //{
                    //    skuprops += String.Format("{0},", sku.SkuProps);
                    //    skuprices += String.Format("{0},", sku.Price);
                    //    skuquatites += String.Format("{0},", sku.Quantity);
                    //    skuouterIds += String.Format("{0},", sku.OuterId);
                    //}
                    ////itemreq.SkuProperties = skuprops.TrimEnd(',');
                    ////itemreq.SkuQuantities = skuquatites.TrimEnd(',');
                    ////itemreq.Skuprices = skuprices.TrimEnd(',');
                    ////itemreq.SkuOuterIds = skuouterIds;
                    //updateRsp = TopService.ItemSkuUpate(session, viewItem.iid, skuprops, skuquatites, skuprices, skuouterIds, null);


                    ItemRsp getRsp = TopService.ItemGet(session, viewItem.nick, updateRsp.Item.Iid, null);
                    Alading.Entity.Item item = new Alading.Entity.Item();
                    if (getRsp.Item == null)
                    {
                        getRsp.Item = itemreq as Taobao.Entity.Item;
                        item.skus = skulist == null ? string.Empty : JsonConvert.SerializeObject(skulist);
                    }
                    UIHelper.ItemCopyData(item, getRsp.Item);
                    ItemService.AddItem(item);//不存在添加，存在更新
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 更新选中宝贝
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateDown_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //获得当前选中的行的DataRow数据
            View_ShopItem viewItem = gridViewItem.GetFocusedRow() as View_ShopItem;
            if (viewItem == null)
            {
                XtraMessageBox.Show("没有选中任何宝贝！");
                return;
            }
            string session = SystemHelper.GetSessionKey(viewItem.nick);
            ItemRsp rsp = TopService.ItemGet(session, viewItem.nick, viewItem.iid, null);
            if (rsp != null && rsp.Item != null)
            {
                Alading.Entity.Item item = new Alading.Entity.Item();
                UIHelper.ItemCopyData(item, rsp.Item);
                ItemService.AddItem(item);
                XtraMessageBox.Show("更新成功！");
            }
        }
        /// <summary>
        /// 宝贝删除
        /// </summary>
        private void btnDeleteItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            View_ShopItem item = gridViewItem.GetFocusedRow() as View_ShopItem;
            try
            {
                if (item == null)
                {
                    XtraMessageBox.Show("您未选中任何宝贝！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DialogResult = XtraMessageBox.Show("您确定要删除当前焦点行的商品吗？", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.Cancel == DialogResult)
                {
                    return;
                }

                if (item != null && !string.IsNullOrEmpty(item.iid))
                {
                    string session = SystemHelper.GetSessionKey(item.nick);
                    ItemRsp itemRsp = TopService.ItemDelete(session, item.iid);

                    //更新本地宝贝字段IsHistory为false
                    ItemService.UpdateItem(item.iid);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == Constants.ITEM_DELETED)
                {
                    //更新本地宝贝字段IsHistory为true
                    ItemService.UpdateItem(item.iid);
                }
                else
                {
                    XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// 同步库存
        /// </summary>
        private void btnUpdateStock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        #endregion

        #region gridcontrolItem操作
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport2Excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            DialogResult dialogResult = saveFileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                //gridControl1.ExportToXls(saveFileDialog.FileName, options);
                gridControlItem.MainView.ExportToExcelOld(saveFileDialog.FileName);
                XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControlItem.MainView.RefreshData();
        }
        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        #endregion

        # region 加载宝贝信息
        private void gridViewItem_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                LoadItem(e.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 加载Item事件
        /// </summary>
        /// <param name="focusedRowHandle"></param>
        private void LoadItem(int focusedRowHandle)
        {
            if (focusedRowHandle < 0)
            {
                return;
            }

            //获得当前选中的行的DataRow数据
            View_ShopItem viewItem = gridViewItem.GetRow(focusedRowHandle) as View_ShopItem;

            if (viewItem != null)
            {
                //全局变量Nick赋值
                sellerNick = viewItem.nick;
                //第一次加载好了，如果是左边的列表发生变化，则重新加载ItemPropValue，否则只换cell值
                LoadItemPropValue(viewItem.props, viewItem.input_pids, viewItem.input_str, viewItem.cid, categoryRowKeyProps, categoryRowSaleProps, categoryRowNotKeyProps, categoryRowStockProps);
                //加载计量单位
                //InitStockUnit();
                //加载商品信息
                LoadItemInformation(viewItem);
            }
        }

        /// <summary>
        /// 加载商品信息
        /// </summary>
        /// <param name="item"></param>
        private void LoadItemInformation(View_ShopItem item)
        {
            try
            {
                gcStockProduct.DataSource = null;
                //清除控件中信息
                ClearInfor();
                string sellercat = string.Empty;
                //加载淘宝类目
                Alading.Entity.ItemCat itemCat = ItemCatService.GetItemCat(item.cid);
                popupContainerEditTBCat.Text = itemCat == null ? string.Empty : itemCat.name;
                popupContainerEditTBCat.Tag = item.cid;
                //加载卖家自定义类目
                if (!string.IsNullOrEmpty(item.seller_cids))
                {
                    string[] SellerCatArray = item.seller_cids.Split(',');
                    if (SellerCatArray != null && SellerCatArray.Length != 0)
                    {
                        List<Alading.Entity.SellerCat> listSellerCat = SellerCatService.GetSellerCatByCid(SellerCatArray);
                        if (listSellerCat != null)
                        {
                            foreach (Alading.Entity.SellerCat sellerCat in listSellerCat)
                            {
                                if (sellerCat != null)
                                { 
                                    sellercat += sellerCat.name + ",";
                                }
                            }
                        }
                        popupContainerEditSellerCat.Text = sellercat.Trim(',');
                        //item.seller_cids形如 ,73403941,73404283,73403993,
                        popupContainerEditSellerCat.Tag = item.seller_cids.TrimStart(',').TrimEnd(',');
                    }
                }
                //加载宝贝sku
                gridControlSku.DataSource = null;
                gridViewSku.Columns.Clear();
                saleInfoTable.Rows.Clear();
                saleInfoTable.Columns.Clear();
                listNewColumns.Clear();
                //获取cid下的所有属性值
                List<View_ItemPropValue> viewPropValueList = ItemPropValueService.GetView_ItemPropValueList(item.cid, "-1", "-1");
                //获取该类目下所有必填的销售属性
                List<View_ItemPropValue> salePropMustList = viewPropValueList.Where(v => v.is_sale_prop).ToList();
                //宝贝没有销售属性
                if (salePropMustList.Count == 0)
                {
                    GridColumn priceColumn = gcPrice;
                    gridViewSku.Columns.Insert(0, priceColumn);
                    GridColumn quantityColumn = gcNum;
                    gridViewSku.Columns.Insert(0, quantityColumn);
                    GridColumn outidColumn = gcOuterId;
                    gridViewSku.Columns.Insert(1, outidColumn);
                    DataTable skuTable = new DataTable();
                    skuTable.Columns.Add("price");
                    skuTable.Columns.Add("outer_id");
                    skuTable.Columns.Add("quantity");
                    DataRow row = skuTable.NewRow();
                    row["price"] = item.price;
                    row["outer_id"] = item.outer_id;
                    row["quantity"] = item.num;
                    skuTable.Rows.Add(row);
                    gridControlSku.DataSource = skuTable;
                    gridViewSku.BestFitColumns();
                }
                else
                {
                    LoadItemSku(item.skus, item.property_alias, gridControlSku, gridViewSku);
                }
                cmbItemTitle.Text = item.title;
                textEditOuterId.Text = item.outer_id;
                checkEditShowCase.Checked = item.has_showcase;
                checkEditInvoice.Checked = item.has_invoice;
                checkEditWarranty.Checked = item.has_warranty;
                calcEditNum.Text = item.num.ToString();
                checkEditVIP.Checked = item.has_discount;
                checkEditAutoPost.Checked = item.auto_repost;
                comboBoxEditValidThru.Text = item.valid_thru.ToString();
                popupContainerEditLocation.Text = item.location_state + "/" + item.location_city;
                calcEditPost.Text = item.post_fee;
                calcEditExpress.Text = item.express_fee;
                calcEditEMS.Text = item.ems_fee;
                calcEditPrice.Text = item.price;
                editorHtml.BodyHtml = System.Web.HttpUtility.HtmlDecode(item.desc);
                cbStockUnit.Text = item.StockUnitName;
                cbStockUnit.Tag = item.UnitCode;
                //加载与宝贝关联的库存商品信息
                LoadStockItem(item);
                //积分返点比例值针对B店
                if (item.shop_type == "B")
                {
                    spinEditAutoPoint.Enabled = true;
                    spinEditAutoPoint.Text = item.auction_point.ToString();
                }
                else if (item.shop_type == "C")
                {
                    spinEditAutoPoint.Enabled = false;
                    spinEditAutoPoint.Text = string.Empty;
                }
                switch (item.ItemType)
                {
                    case "普通商品":
                        cbItemType.Text = "普通";
                        //加载基本计量单位
                        LoadBaseUnit();
                        break;
                    case "组合商品":
                        //加载基本计量单位
                        LoadBaseUnit();
                        cbItemType.Text = "组合";
                        break;
                    case "包装商品":
                        cbItemType.Text = "包装";
                        LoadUnitForPacage();
                        break;
                }
                switch (item.stuff_status)
                {
                    case "new": cmbStaffStatus.Text = "全新";
                        break;
                    case "unused": cmbStaffStatus.Text = "闲置";
                        break;
                    default: cmbStaffStatus.Text = "二手";
                        break;
                }
                switch (item.approve_status)
                {
                    case "onsale": comboBoxEditUpdateStytle.Text = "立即";
                        break;
                    case "instock": comboBoxEditUpdateStytle.Text = "进线上仓库";
                        break;
                    default: comboBoxEditUpdateStytle.Text = "定时";
                        break;
                }
                switch (item.freight_payer)
                {
                    case "buyer": comboBoxEditFreight.Text = "买家";
                        break;
                    default: comboBoxEditFreight.Text = "卖家";
                        break;
                }
                switch (item.type)
                {
                    case "fixed": comboBoxEditTradeType.Text = "一口价";
                        break;
                    default: comboBoxEditTradeType.Text = "拍卖";
                        break;
                }

                //同步加载
                //Image picImage = SystemHelper.GetItemImage(item.sid, item.pic_url);
                //if (picImage != null)
                //{
                //    picItemImage.Image = picImage;
                //}

                //异步加载图片
                ItemImageDelegate imgDelegate = new ItemImageDelegate(GetItemImage);
                IAsyncResult asyncResult = imgDelegate.BeginInvoke(item.sid, item.pic_url, new AsyncCallback(GetItemImageCallback), imgDelegate);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 加载宝贝库存信息
        /// </summary>
        /// <param name="outerid">宝贝商家编码</param>
        private void LoadStockItem(View_ShopItem viewItem)
        {
            if (viewItem.IsAsociate && !string.IsNullOrEmpty(viewItem.outer_id))
            {
                List<Alading.Entity.View_StockItemProduct> stockitemList = View_StockItemProductService.GetView_StockItemProductItem(viewItem.outer_id);
                if (stockitemList != null)
                {
                    gcStockProduct.DataSource = stockitemList;
                    gvStockProduct.BestFitColumns();
                }
            }
        }

        private void LoadFirstRow(int rowHandle)
        {
            if (rowHandle == 0)
            {
                //重新加载第一行数据
                LoadItem(gridViewItem.FocusedRowHandle);
            }
        }

        private void repositoryItemCheckEditSelect_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SellerCatForm sellerCatForm = new SellerCatForm();
            sellerCatForm.ShowDialog();
        }

        /// <summary>
        /// 类目下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemDownLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                foreach (Alading.Entity.Shop shop in SystemHelper.ShopList)
                {
                    UIHelper.DownSellerCatsList(shop.nick);
                }
            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void cmbTradeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEditTradeType.SelectedIndex == 0)
            {
                this.labelTradeType.Text = "一口价：";
                this.checkEditIncreament.Enabled = false;
                //设置运费承担方式可编辑
                this.comboBoxEditFreight.Enabled = true;
            }
            else if (comboBoxEditTradeType.SelectedIndex == 1)
            {
                this.labelTradeType.Text = "起始价：";
                this.checkEditIncreament.Enabled = true;
                //设置运费承担方式由卖家
                this.comboBoxEditFreight.SelectedIndex = 1;
                this.comboBoxEditFreight.Enabled = false;
            }
        }

        private void checkEditIncreament_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditIncreament.Checked)
            {
                calcEditIncreament.Enabled = true;
            }
            else
            {
                calcEditIncreament.Enabled = false;
            }
        }

        private void comboBoxEditFreight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEditFreight.SelectedIndex == 0)
            {
                calcEditPost.Enabled = true;
                calcEditExpress.Enabled = true;
                calcEditEMS.Enabled = true;
                comboBoxEditPostModel.Enabled = false;
                btnSetPostMode.Enabled = false;
            }
            else if (comboBoxEditFreight.SelectedIndex == 1)
            {
                calcEditPost.Enabled = false;
                calcEditExpress.Enabled = false;
                calcEditEMS.Enabled = false;
                comboBoxEditPostModel.Enabled = false;
                btnSetPostMode.Enabled = false;
            }
            else if (comboBoxEditFreight.SelectedIndex == 2)
            {
                calcEditPost.Enabled = false;
                calcEditExpress.Enabled = false;
                calcEditEMS.Enabled = false;
                comboBoxEditPostModel.Enabled = true;
                btnSetPostMode.Enabled = true;
                //加载运费模板
                comboBoxEditPostModel.Properties.Items.Clear();
                List<Alading.Entity.Postage> listPostage = PostageService.GetAllPostage();
                if (listPostage != null)
                {
                    foreach (Alading.Entity.Postage postage in listPostage)
                    {
                        comboBoxEditPostModel.Properties.Items.Add(postage.name);
                    }
                }
            }
        }

        private void comboBoxEditUpdateStytle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEditUpdateStytle.SelectedIndex != 1)
            {
                dateEditHour.Enabled = false;
                timeEditMin.Enabled = false;
                dateEditHour.EditValue = DateTime.Now.ToShortDateString();
                timeEditMin.EditValue = DateTime.Now.ToShortTimeString();
            }
            else
            {
                dateEditHour.Enabled = true;
                timeEditMin.Enabled = true;
            }
        }

        /// <summary>
        /// 设置运费模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetPostMode_Click(object sender, EventArgs e)
        {
            //获得当前选中的行的DataRow数据
            View_ShopItem viewItem = gridViewItem.GetFocusedRow() as View_ShopItem;
            if (viewItem == null)
            {
                XtraMessageBox.Show("请选择添加运费模板的宝贝！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(viewItem.nick))
            {
                XtraMessageBox.Show("选中的宝贝未指定卖家店铺，不能添加运费模板！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PostageModelForm postagemodelForm = new PostageModelForm(viewItem.nick);
            postagemodelForm.ShowDialog();
            comboBoxEditPostModel.Text = postagemodelForm.Tag == null ? string.Empty : postagemodelForm.Tag.ToString();
            postagemodelForm.Dispose();
        }

        /// <summary>
        /// 动态产生条形码
        /// </summary>
        private void textEditOuterId_EditValueChanged(object sender, EventArgs e)
        {
            Image myimg = Code128Rendering.MakeBarcodeImage(textEditOuterId.Text.Trim(), 1, true);
            picBarCodeImage.Image = myimg;
        }

        /// <summary>
        /// 加载邮费模板同时加载postage_id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxEditPostModel_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBoxEditPostModel.Text))
            {
                Alading.Entity.Postage postage = PostageService.GetPostageByName(comboBoxEditPostModel.Text);
                comboBoxEditPostModel.Tag = postage.postage_id;
            }
        }
        #endregion

        #region 图片
        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                switch (buttonEdit1.Properties.Buttons.IndexOf(e.Button))
                {
                    case 0:
                        picItemImage.LoadImage();
                        break;
                    case 1:
                        if (gridControlItem.DataSource == null || gridControlItem.MainView.DataRowCount == 0)
                        {
                            return;
                        }
                        DialogResult result = XtraMessageBox.Show("您确定要删除图片吗？", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (result == DialogResult.Cancel)
                        {
                            return;
                        }
                        picItemImage.Image = null;
                        break;
                    case 2:
                        if (gridControlItem.DataSource == null || gridControlItem.MainView.DataRowCount==0)
                        {
                            return;
                        }
                        View_ShopItem viewItem = gridViewItem.GetFocusedRow() as View_ShopItem;
                        if (viewItem == null)
                        {
                            XtraMessageBox.Show("当前商品为库存商品，不能更新到淘宝！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            if (viewItem.IsHistory)
                            {
                                XtraMessageBox.Show("当前宝贝已在淘宝上删除，不能上传图片！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }

                        if (picItemImage.Image == null)
                        {
                            XtraMessageBox.Show("没有图像文件！");
                            return;
                        }
                        DialogResult dialogResult = XtraMessageBox.Show("您确定要更新图片到淘宝吗？", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (dialogResult == DialogResult.Cancel)
                        {
                            return;
                        }
                        if (UpItemImage(viewItem.nick, viewItem.iid))
                        {
                            XtraMessageBox.Show("上传图片成功！");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool UpItemImage(string nick, string iid)
        {
            try
            {
                string session = SystemHelper.GetSessionKey(nick);
                Image image = picItemImage.Image;
                Byte[] picBytes = SystemHelper.GetImageBytes(image);
                ItemRsp rsp = TopService.ItemImgUpload(session, iid, null, null, picBytes, true);
                if (rsp != null && rsp.ItemImg != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 异步加载图片
        private void GetItemImageCallback(IAsyncResult ar)
        {
            Image picImage = ((ItemImageDelegate)ar.AsyncState).EndInvoke(ar);
            BeginInvoke(new Action(() =>
            {
                if (picImage != null)
                {
                    picItemImage.Image = picImage;
                }
            }));
        }

        /// <summary>
        /// 返回Item的图片，如果不存在则从网上下载
        /// </summary>
        /// <param name="shopSid"></param>
        /// <param name="picUrl"></param>
        /// <returns></returns>
        public Image GetItemImage(string shopSid, string picUrl)
        {
            if (string.IsNullOrEmpty(shopSid) || string.IsNullOrEmpty(picUrl))
            {
                return null;
            }

            StringBuilder dirBuilder = new StringBuilder();
            dirBuilder.Append(Application.StartupPath);
            dirBuilder.Append("\\ItemPics\\");
            dirBuilder.Append(shopSid);
            dirBuilder.Append("\\");
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

        #endregion
        #endregion

        #region 销售属性展示

        /// <summary>
        /// 宝贝销售属性发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ccmb_EditValueChanged(object sender, EventArgs e)
        {
            saleInfoTable.Rows.Clear();
            gridControlSku.DataSource = null;

            List<Props> listProps = new List<Props>();
            foreach (EditorRow row in categoryRowSaleProps.ChildRows)
            {
                EditorRowTag tag = row.Tag as EditorRowTag;
                string fieldName = "FieldName" + tag.Pid.ToString();

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
            View_ShopItem viewitem = gridViewItem.GetFocusedRow() as View_ShopItem;
            if (viewitem != null)
            {
                //当前宝贝的所有sku
                Skus itemskus = TopUtils.DeserializeObject<Skus>(viewitem.skus);
                //向saleInfoTable中添加销售属性组合
                AddPropsToTable(listProps, itemskus, viewitem.property_alias);
                gridControlSku.DataSource = saleInfoTable.DefaultView;
                gridViewSku.BestFitColumns();
            }
            #endregion
        }

        /// <summary>
        /// 加载宝贝sku
        /// </summary>
        /// <param name="sku">宝贝sku</param>
        private void LoadItemSku(string skus, string propertyAlias, GridControl gridControlSku, GridView gridViewSku)
        {
            //当前宝贝的所有sku
            Skus itemskus = TopUtils.DeserializeObject<Skus>(skus);
            #region 向gridview 与 saleInfoTable中添加必备列
            saleInfoTable.Columns.Add("price", typeof(double));
            saleInfoTable.Columns.Add("quantity", typeof(int));
            saleInfoTable.Columns.Add("outer_id");
            saleInfoTable.Columns.Add("sku_id");

            GridColumn priceColumn = gcPrice;
            gridViewSku.Columns.Insert(0, priceColumn);
            GridColumn outidColumn = gcOuterId;
            gridViewSku.Columns.Insert(1, outidColumn);
            GridColumn quantityColumn = gcNum;
            gridViewSku.Columns.Insert(0, quantityColumn);
            #endregion

            List<Props> listProps = new List<Props>();
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
                    defcolumn.VisibleIndex = 0;
                    if (gridViewSku.Columns.ColumnByFieldName("def" + fieldName) == null)
                    {
                        gridViewSku.Columns.Insert(0, defcolumn);
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
                column.VisibleIndex = 0;
                column.OptionsColumn.AllowEdit = false;

                //向gridview中添加列显示属性vid
                GridColumn vcolumn = new GridColumn();
                vcolumn.FieldName = "vid" + fieldName;
                vcolumn.Name = "vid" + row.Properties.Caption;
                vcolumn.Visible = false;

                if (gridViewSku.Columns.ColumnByFieldName(fieldName) == null)
                {
                    gridViewSku.Columns.Insert(0, column);
                    gridViewSku.Columns.Add(vcolumn);
                }
                //向saleInfoTable添加一列属性值
                if (saleInfoTable.Columns.IndexOf(fieldName) == -1)
                {
                    saleInfoTable.Columns.Add(fieldName, typeof(string));
                    saleInfoTable.Columns.Add("vid" + fieldName, typeof(string));
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
            AddPropsToTable(listProps, itemskus, propertyAlias);
            gridControlSku.DataSource = saleInfoTable.DefaultView;
            gridViewSku.BestFitColumns();
        }

        /// <summary>
        /// 向saleInfoTable中添加销售属性组合 ，在gridControlSku显示
        /// </summary>
        private void AddPropsToTable(List<Props> listProps, Skus itemskus, string propertyAlias)
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
                                    newRow["price"] = sku.Price == null ? 0 : double.Parse(sku.Price);
                                    newRow["quantity"] = sku.Quantity;
                                    newRow["sku_id"] = sku.SkuId ?? string.Empty;
                                    newRow["outer_id"] = sku.OuterId ?? string.Empty;
                                }
                                saleInfoTable.Rows.Add(newRow);
                            }
                        }
                    }
                }
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
                        //若有重名，需添加
                        newRow["def" + fieldName] = UIHelper.GetNewName(prop.vid, propertyAlias);
                        //在sku中获取属性对应价格、数量
                        if (itemskus != null)
                        {
                            Alading.Taobao.Entity.Sku sku = UIHelper.GetSelectedSku(prop.vid, itemskus);
                            newRow["price"] = sku.Price == null ? 0.0 : double.Parse(sku.Price);
                            newRow["quantity"] = sku.Quantity;
                            newRow["outer_id"] = sku.OuterId;
                            newRow["sku_id"] = sku.SkuId;
                        }
                        saleInfoTable.Rows.Add(newRow);
                    }
                }
                #endregion
            }
        }

        private void gridViewSku_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
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
            }
        }
        #endregion

        #region 组合商品
        /// <summary>
        /// 获取所有组合商品
        /// </summary>
        private void GetAssembleItems()
        {
            try
            {
                List<AssembleItem> asmbList = ItemService.GetAllAssembleItem();
                if (asmbList != null)
                {
                    gridControlItem.DataSource = asmbList;
                    gridViewAssemble.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 组合商品焦点行变化
        /// </summary>
        private void gridViewAssemble_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                ClearInfor();
                LoadAssembleItem();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        /// <summary>
        /// 加载焦点行组合商品信息与组合商品内各商品
        /// </summary>
        private void LoadAssembleItem()
        {
            AssembleItem foucuedAssembleProduct = gridViewAssemble.GetFocusedRow() as AssembleItem;
            if (foucuedAssembleProduct != null)
            {
                cmbItemTitle.Text = foucuedAssembleProduct.Name;
                cbStockUnit.Text = foucuedAssembleProduct.UnitName;
                textEditOuterId.Text = foucuedAssembleProduct.OuterID;
                calcEditPrice.Text = foucuedAssembleProduct.Price.ToString();
                popupContainerEditTBCat.Text = foucuedAssembleProduct.CatName;
                cbItemType.Text = "组合";
                editorHtml.BodyHtml = System.Web.HttpUtility.HtmlDecode(foucuedAssembleProduct.AssembleDesc);
                //加载属性
                LoadItemPropValue(foucuedAssembleProduct.Props, foucuedAssembleProduct.InputPids, foucuedAssembleProduct.InputStr, foucuedAssembleProduct.Cid, categoryRowKeyProps, categoryRowSaleProps, categoryRowNotKeyProps, categoryRowStockProps);
                if (!string.IsNullOrEmpty(foucuedAssembleProduct.OuterID))
                {
                    //异步加载图片
                    ItemImageDelegate imgDelegate = new ItemImageDelegate(LoadImage);
                    IAsyncResult asyncResult = imgDelegate.BeginInvoke(foucuedAssembleProduct.OuterID, null, new AsyncCallback(GetItemImageCallback), imgDelegate);
                }
                else
                {
                    picItemImage.Image = null;                   
                }

                //加载组合商品中各商品
                List<View_AssembleProduct> assembleProductList = ItemService.GetViewAssembleProduct(foucuedAssembleProduct.AssembleCode);
                gcStock.DataSource = assembleProductList;
                gvStock.BestFitColumns();
            }
        }

        private Image LoadImage(string outerID, string assembleCode)
        {           
            List<Picture> picList = PictureService.GetPicture(i => i.OuterID == outerID);
            if (picList != null && picList.Count > 0)
            {
                System.IO.MemoryStream stream = new System.IO.MemoryStream(picList.First().PictureContent);                
                return Image.FromStream(stream);
            }
            else
            {                
                return null;
            }
        }

        #endregion

        #region 本地库存商品
        /// <summary>
        /// 获取所有本地库存商品
        /// </summary>
        private void GetLocalStockItem()
        {
            try
            {
                List<View_StockItemProduct> stockItemProductList = StockItemService.GetLocalStockItem();
                gridControlItem.DataSource = stockItemProductList;
                gridViewStockItem.BestFitColumns();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 库存商品列表焦点行改变
        /// </summary>
        private void gridViewStockItem_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                ClearInfor();
                LoadStockitemProductInfor();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 加载焦点行的库存商品信息
        /// </summary>
        private void LoadStockitemProductInfor()
        {
            try
            {
                Alading.Entity.View_StockItemProduct stockitem = gridViewStockItem.GetFocusedRow() as Alading.Entity.View_StockItemProduct;
                if (stockitem != null)
                {
                    cmbItemTitle.Text = stockitem.Name;
                    textEditOuterId.Text = stockitem.SkuOuterID;
                    popupContainerEditTBCat.Text = stockitem.CatName;
                    calcEditNum.Text = stockitem.SkuQuantity.ToString();
                    cbStockUnit.Text = stockitem.StockUnitName;
                    calcEditPrice.Text = stockitem.MarketPrice.ToString();

                    editorHtml.BodyHtml = System.Web.HttpUtility.HtmlDecode(stockitem.StockItemDesc);
                    //加载库存商品
                    LoadItemPropValue(stockitem.Props, stockitem.InputPids, stockitem.InputStr, stockitem.Cid, categoryRowKeyProps, categoryRowSaleProps, categoryRowNotKeyProps, categoryRowStockProps);

                    //异步加载图片,添加到文件夹local中
                    ItemImageDelegate imgDelegate = new ItemImageDelegate(GetItemImage);
                    IAsyncResult asyncResult = imgDelegate.BeginInvoke("local", stockitem.PicUrl, new AsyncCallback(GetItemImageCallback), imgDelegate);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 模糊搜索
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(checkedComboBoxEditShop.Text))
            {
                XtraMessageBox.Show("请选择店铺！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(texteditSearchText.Text))
            {
                XtraMessageBox.Show("请输入关键词！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            List<Alading.Entity.View_ShopItem> viewShopItemList = ItemService.Get_ViewShopItem(checkedComboBoxEditShop.Text.Trim(), texteditSearchText.Text.Trim());
            gridControlItem.MainView = gridViewItem;
            gridControlItem.DataSource = viewShopItemList;
            gridViewItem.BestFitColumns();
        }
        #endregion

        #region 公共方法

        //加载基本计量单位
        private void LoadBaseUnit()
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
                    string name = string.Empty;
                    if (view.IsBaseUnit)
                    {
                        name = string.Format("{0}", view.StockUnitName);
                        treeUnit.AppendNode(new object[] { name }, pNode, new TreeListNodeTag(view.StockUnitCode));
                    }
                }
            }
            treeUnit.EndUnboundLoad();
            treeUnit.ExpandAll();
        }

        //加载包装商品的计量单位
        private void LoadUnitForPacage()
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
                    if (!view.IsBaseUnit)
                    {
                        string name = string.Format("{0}(={1}{2})", view.StockUnitName, view.Conversion, view.BaseUnit);
                        treeUnit.AppendNode(new object[] { name }, pNode, new TreeListNodeTag(view.StockUnitCode));
                    }
                }
            }
            treeUnit.EndUnboundLoad();
            treeUnit.ExpandAll();
        }

        /// <summary>
        /// 焦点行改变，刷新控件中信息
        /// </summary>
        private void ClearInfor()
        {
            cmbItemTitle.Text = string.Empty;
            textEditOuterId.Text = string.Empty;
            popupContainerEditTBCat.Text = string.Empty;
            popupContainerEditSellerCat.Text = string.Empty;
            popupContainerEditLocation.Text = string.Empty;
            checkEditShowCase.Checked = false;
            checkEditInvoice.Checked = false;
            checkEditWarranty.Checked = false;
            calcEditNum.Text = string.Empty;
            checkEditVIP.Checked = false;
            checkEditAutoPost.Checked = false;
            comboBoxEditValidThru.Text = string.Empty;
            popupContainerEditLocation.Text = string.Empty;
            calcEditPost.Text = string.Empty;
            calcEditExpress.Text = string.Empty;
            calcEditEMS.Text = string.Empty;
            calcEditPrice.Text = string.Empty;
            editorHtml.BodyHtml = null;
            cbStockUnit.Text = string.Empty;
            spinEditAutoPoint.Text = string.Empty;
            cbItemType.Text = string.Empty;
            cmbStaffStatus.Text = string.Empty;
            comboBoxEditUpdateStytle.Text = string.Empty;
            comboBoxEditFreight.Text = string.Empty;
            comboBoxEditTradeType.Text = string.Empty;
            picItemImage.Image = null;
            treeListSellerCat.Nodes.Clear();
            //sellerNick = string.Empty;
        }

        /// <summary>
        /// 加载item所有的属性
        /// </summary>
        /// <param name="item"></param>
        /// <param name="categoryRowKeyProps"></param>
        /// <param name="categoryRowSaleProps"></param>
        /// <param name="categoryRowNotKeyProps"></param>
        /// <param name="categoryRowStockProps"></param>
        private void LoadItemPropValue(string props, string input_pids, string input_str, string cid, CategoryRow categoryRowKeyProps, CategoryRow categoryRowSaleProps, CategoryRow categoryRowNotKeyProps, CategoryRow categoryRowStockProps)
        {
            //if (item == null)//如果不存在,则不予以操作，防止脏数据造成异常
            //{
            //    return;
            //}

            //清除当前的子行
            categoryRowSaleProps.ChildRows.Clear();
            categoryRowKeyProps.ChildRows.Clear();
            categoryRowNotKeyProps.ChildRows.Clear();
            categoryRowStockProps.ChildRows.Clear();

            Hashtable propsTable = null;
            //分隔所有属性，如3032757:21942439;2234738:44627。pid:vid

            if (!string.IsNullOrEmpty(props))
            {
                propsTable = new Hashtable();
                //先按照;分割，再按照:分割，同时StringSplitOptions.RemoveEmptyEntries表示去掉空格
                List<string> propsList = props.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

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
            if (!string.IsNullOrEmpty(input_pids) && !string.IsNullOrEmpty(input_str))
            {
                string[] inputPidsArray = input_pids.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] inputStrArray = input_str.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

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
            List<View_ItemPropValue> propValueList = ItemPropValueService.GetView_ItemPropValueList(cid, "-1", "-1");

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

        void cmbParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit cbe = sender as ComboBoxEdit;
            RepositoryItemComboBox cmb = cbe.Properties as RepositoryItemComboBox;

            //由于SelectedIndexChanged阻止了设置值，为了防止值不显示
            vGridControl1.FocusedRow.Properties.Value = cbe.Text;

            //如果值不等于自定义，则加载数据
            EditorRow row = new EditorRow();
            EditorRowTag tag = vGridControl1.FocusedRow.Tag as EditorRowTag;
            string cid = tag.Cid;
            string pid = tag.Pid;
            //row.Properties.Caption = tag.ChildTemplate;
            if (tag.IsMust)
            {
                row.Properties.ImageIndex = 0;
            }

            //删除不需要的row                              
            vGridControl1.FocusedRow.ChildRows.Clear();
            BaseRow delRow = vGridControl1.FocusedRow.ParentRow.ChildRows.GetRowByFieldName(vGridControl1.FocusedRow.Properties.Caption + "自定义");
            if (delRow != null)
            {
                vGridControl1.FocusedRow.ParentRow.ChildRows.Remove(delRow);
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
                    vGridControl1.FocusedRow.ChildRows.Add(row);
                }
            }
            else if (cbe.Text == "自定义")
            {
                //如果是自定义，则在当前焦点行的父行里添加一子行(目前暂时作为其子项添加)
                string caption = vGridControl1.FocusedRow.Properties.Caption + "自定义";
                row.Properties.Caption = caption;
                row.Properties.FieldName = caption;
                vGridControl1.FocusedRow.ChildRows.Clear();
                vGridControl1.FocusedRow.ChildRows.Add(row);/**/
            }
            vGridControl1.FocusNext();
        }

        /// <summary>
        /// 单选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            vGridControl1.FocusedRow.Properties.Value = ((DevExpress.XtraEditors.ComboBoxEdit)(sender)).EditValue == null ? string.Empty : ((DevExpress.XtraEditors.ComboBoxEdit)(sender)).EditValue.ToString();
        }

        /// <summary>
        /// 加载淘宝类目cid下宝贝属性
        /// </summary>
        /// <param name="cid">淘宝cid</param>
        public void LoadItemPropValue(string cid)
        {
            categoryRowSaleProps.ChildRows.Clear();
            categoryRowKeyProps.ChildRows.Clear();
            categoryRowNotKeyProps.ChildRows.Clear();
            categoryRowStockProps.ChildRows.Clear();

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
                    if (ipv.is_parent)
                    {
                        //控件的tag中存储hashtable，保存当前列表中的vid
                        //cmb.SelectedIndexChanged += new EventHandler(cmb_SelectedIndexChanged);
                        tag.IsParent = true;
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


        /// <summary>
        /// 显示宝贝
        /// </summary>
        /// <param name="sellercid">当前卖家自定义类目</param>
        /// <param name="nick">卖家昵称</param>
        /// <param name="status">宝贝状态</param>
        private void ShowItemInGridView(string sellercid, string nick, string status)
        {
            try
            {
                List<Alading.Entity.View_ShopItem> listItem = View_ShopItemService.GetView_ShopItem(sellercid, nick, status);
                if (listItem != null)
                {
                    gridControlItem.DataSource = listItem;
                    gridViewItem.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 通过存储过程获取当前卖家库存商品
        /// </summary>
        private void GetShopItemByStatus(string nick, string status,bool isHistory)
        {
            try
            {
                List<View_ShopItem> shopItemList = View_ShopItemService.GetView_ShopItemInStock(nick, status, isHistory);
                gridControlItem.DataSource = shopItemList;
                gridViewItem.BestFitColumns();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 通过存储过程获取当前卖家橱窗中宝贝
        /// </summary>
        private void LoadShowCaseItems(string nick, bool history, bool showCase)
        {
            try
            {
                List<View_ShopItem> viewShopItemList = View_ShopItemService.GetView_ShopItemShowCaseList(nick, history, showCase);
                if (viewShopItemList != null)
                {
                    gridControlItem.DataSource = viewShopItemList;
                    gridViewItem.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 下载回收商品
        /// </summary>
        private void LoadHistoryItem(string nick, bool history)
        {
            try
            {
                List<View_ShopItem> viewShopItemList = View_ShopItemService.GetView_ShopItemHistoryList(nick, history);
                if (viewShopItemList != null)
                {
                    gridControlItem.DataSource = viewShopItemList;
                    gridViewItem.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  显示宝贝
        /// </summary>
        /// <param name="listChildCid">当前卖家自定义类目下的子类目</param>
        /// <param name="nick">卖家昵称</param>
        /// <param name="status">宝贝状态</param>
        private void ShowItem(List<string> listChildCid, string nick, string status)
        {
            try
            {
                List<Alading.Entity.View_ShopItem> listItem = View_ShopItemService.GetView_ShopItem(listChildCid, nick, status);
                if (listItem != null)
                {
                    gridControlItem.DataSource = listItem;
                    gridViewItem.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取当前卖家自定义类目下的子类目
        /// </summary>
        /// <param name="nick">卖家昵称</param>
        /// <param name="sellerCid">当前卖家自定义类目</param>
        private void GetAllChildNode(List<string> listChildCid, string nick, string sellerCid)
        {
            List<Alading.Entity.SellerCat> listSellerCat = listSellerCats.FindAll(delegate(Alading.Entity.SellerCat sellerCat) { return sellerCat.SellerNick == nick; });
            SearchChildNode(listChildCid, listSellerCat, sellerCid);
        }

        /// <summary>
        ///  递归寻找子类目
        /// </summary>
        /// <param name="listChildCid">当前卖家自定义类目下的所有子类目</param>
        /// <param name="listSellerCat">所有卖家自定义类目</param>
        /// <param name="sellerCid">当前卖家自定义类目</param>
        private void SearchChildNode(List<string> listChildCid, List<Alading.Entity.SellerCat> listSellerCat, string sellerCid)
        {
            foreach (Alading.Entity.SellerCat sellerCat in listSellerCat)
            {
                if (sellerCat.parent_cid == sellerCid)
                {
                    listChildCid.Add(sellerCat.cid);
                    SearchChildNode(listChildCid, listSellerCat, sellerCat.cid);
                }
            }
        }

        private void BuildUpdateParameter(ItemReq itemReq)
        {
            try
            {
                itemReq.Title = cmbItemTitle.Text;
                itemReq.Num = string.IsNullOrEmpty(calcEditNum.Text) ? 0 : int.Parse(calcEditNum.Text);
                itemReq.SellerCids = popupContainerEditSellerCat.Tag == null ? string.Empty : popupContainerEditSellerCat.Tag.ToString();
                itemReq.Cid = popupContainerEditTBCat.Tag == null ? "0" : popupContainerEditTBCat.Tag.ToString();
                itemReq.Location = GetLocation();
                string keyProps = UIHelper.GetCategoryRowData(categoryRowKeyProps);
                string saleProps = UIHelper.GetCategoryRowData(categoryRowSaleProps);
                string notkeyProps = UIHelper.GetCategoryRowData(categoryRowNotKeyProps);
                itemReq.Props = keyProps + saleProps + notkeyProps.TrimEnd(';');
                //获取属性自定义属性值
                Dictionary<string, string> inputDic = UIHelper.GetCategoryInputRowData(categoryRowKeyProps, categoryRowNotKeyProps);
                if (inputDic.Count > 0 && inputDic.Keys.Contains("pid") && inputDic.Keys.Contains("str"))
                {
                    itemReq.InputPids = inputDic["pid"];
                    itemReq.InputStrs = inputDic["str"];
                }     
                itemReq.Desc = editorHtml.BodyHtml;
                itemReq.EmsFee = calcEditEMS.Value.ToString();
                itemReq.PostFee = calcEditPost.Value.ToString();
                itemReq.ExpressFee = calcEditExpress.Value.ToString();
                itemReq.PostageId = comboBoxEditPostModel.Tag == null ? string.Empty : comboBoxEditPostModel.Tag.ToString();
                itemReq.HasShowcase = checkEditShowCase.Checked;
                itemReq.HasInvoice = checkEditInvoice.Checked;
                itemReq.HasWarranty = checkEditWarranty.Checked;
                itemReq.HasDiscount = checkEditVIP.Checked;
                itemReq.AutoRepost = checkEditAutoPost.Checked;
                itemReq.Increment = calcEditIncreament.Value.ToString();
                itemReq.OuterId = textEditOuterId.Text;
                itemReq.Price = calcEditPrice.Value.ToString();
                itemReq.ValidThru = comboBoxEditValidThru.EditValue == null ? 14 : int.Parse(comboBoxEditValidThru.EditValue.ToString().TrimEnd('天'));

                switch (cmbStaffStatus.Text)
                {
                    case "二手":
                        itemReq.StuffStatus = "second";
                        break;
                    case "闲置":
                        itemReq.StuffStatus = "unused";
                        break;
                    case "全新":
                        itemReq.StuffStatus = "new";
                        break;
                }
                switch (comboBoxEditTradeType.Text)
                {
                    case "一口价":
                        itemReq.Type = "fixed";
                        break;
                    case "拍卖":
                        itemReq.Type = "auction";
                        break;
                }
                if (comboBoxEditUpdateStytle.Text == "进线上仓库")
                {
                    itemReq.ApproveStatus = "instock";
                }
                else
                {
                    itemReq.ApproveStatus = "onsale";
                }
                if (comboBoxEditFreight.SelectedIndex == 1)
                {
                    itemReq.FreightPayer = "seller";
                }
                else
                {
                    itemReq.FreightPayer = "buyer";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获得省市的值赋值给itemreq
        /// </summary>
        private Alading.Taobao.Entity.Location GetLocation()
        {
            Alading.Taobao.Entity.Location location = new Alading.Taobao.Entity.Location();
            if (this.popupContainerEditLocation.Text == string.Empty)
            {
                return null;
            }
            switch (this.popupContainerEditLocation.Text.Split('/')[0])
            {
                case "黑龙江省":
                    location.State = "黑龙江";
                    break;
                case "内蒙古自治区":
                    location.State = "内蒙古";
                    break;
                default:
                    location.State = this.popupContainerEditLocation.Text.Split('/')[0].Substring(0, 2);
                    break;
            }
            if (this.popupContainerEditLocation.Text.Split('/')[1].EndsWith("市"))
            {
                location.City = this.popupContainerEditLocation.Text.Split('/')[1].Substring(0, this.popupContainerEditLocation.Text.Split('/')[1].IndexOf("市"));
            }
            else if (this.popupContainerEditLocation.Text.Split('/')[1].EndsWith("地区"))
            {
                location.City = this.popupContainerEditLocation.Text.Split('/')[1].Substring(0, this.popupContainerEditLocation.Text.Split('/')[1].IndexOf("地区"));
            }
            else if (this.popupContainerEditLocation.Text.Split('/')[1].EndsWith("自治州"))
            {
                switch (this.popupContainerEditLocation.Text.Split('/')[1])
                {
                    case "西双版纳傣族自治州":
                        location.City = "西双版纳";
                        break;
                    case "延边朝鲜族自治州":
                        location.City = "延吉";
                        break;
                    default:
                        location.City = this.popupContainerEditLocation.Text.Split('/')[1].Substring(0, 2);
                        break;
                }

            }
            else if (this.popupContainerEditLocation.Text.Split('/')[1].EndsWith("岛"))
            {
                location.City = this.popupContainerEditLocation.Text.Split('/')[1].Substring(0, 2);
            }
            else location.City = this.popupContainerEditLocation.Text.Split('/')[1];
            return location;
        }

        /// <summary>
        /// 计算编辑后的宝贝sku与Property_Alias
        /// </summary>
        private void BuildSkuAndAlias(List<Alading.Taobao.Entity.Sku> listSku, ref string propertity_Alias)
        {
             try
            {
                #region 将saleInfoTable中的动态添加新列分类：属性类型列、重命名属性值列、属性值列
                //属性类型如颜色列表
                List<string> listFieldNameVid = new List<string>();
                //属性值如红色列表
                List<string> listVidFieldNameVid = new List<string>();
                //重命名的属性值的新名称列表
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

                #region 计算编辑后的宝贝sku与Property_Alias
                List<string> listAlias = new List<string>();
                foreach (DataRow skuRow in saleInfoTable.Rows)
                {
                    //价格或数量中有一项为空时，忽略此行
                    if (!string.IsNullOrEmpty(skuRow["quantity"].ToString()) && !string.IsNullOrEmpty(skuRow["price"].ToString()))
                    {
                        Alading.Taobao.Entity.Sku sku = new Alading.Taobao.Entity.Sku();
                        sku.SkuId = skuRow["sku_id"].ToString();
                        sku.Quantity = (int)skuRow["quantity"];
                        sku.Price = skuRow["price"].ToString();
                        sku.OuterId = skuRow["outer_id"].ToString();
                        //计算sku的props
                        string skuProps = string.Empty;
                        for (int i = 0; i < listFieldNameVid.Count; i++)
                        {
                            //属性所属类型pid,如红色所属的颜色pid
                            string pid = listFieldNameVid[i].Replace("FieldName", string.Empty);
                            //属性vid
                            string vid = skuRow[listVidFieldNameVid[i]].ToString();
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
                            if (hasDefFieldName && !string.IsNullOrEmpty(skuRow[listdefFieldNameVid[j]].ToString()))
                            {
                                string newName = skuRow[listdefFieldNameVid[j]].ToString();
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
                            skuProps = skuProps.TrimEnd(';');
                            sku.SkuProps = skuProps;
                        }
                        listSku.Add(sku);
                    }
                }

                //计算出propertity_Alias字符串
                if (listAlias.Count != 0)
                {
                    foreach (string alias in listAlias)
                    {
                        propertity_Alias += alias + ";";
                    }
                    propertity_Alias = propertity_Alias.TrimEnd(';');
                }
                #endregion
            }
             catch (Exception ex)
             {
                 throw ex;
             }
        }
        #endregion

        #region 宝贝类型改变
        //修改宝贝类型
        private void cbItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //当前页面商品不是宝贝，返回
                if (gridControlItem.MainView != gridViewItem)
                {
                    return;
                }
                /*当前焦点行宝贝类型改变，引起cbItemType的SelectedIndex改变时返回
                cbItemType未获得焦点，即未直接通过cbItemType改变宝贝类型时返回*/
                if (gridViewItem.FocusedRowModified || !cbItemType.Focused)
                {
                    return;
                }
                string itemType = cbItemType.Text;
                //获得当前选中的行的DataRow数据
                View_ShopItem viewItem = gridViewItem.GetFocusedRow() as View_ShopItem;
                if (viewItem == null)
                {
                    return;
                }

                switch (cbItemType.Text)
                {
                    case "组合":
                        //将当前宝贝类型改为组合商品
                        AssembleForm assembleForm = new AssembleForm(viewItem.iid);
                        assembleForm.ShowDialog();

                        //修改当前页面宝贝的outer_id与商品类型
                        if (assembleForm.Tag != null)
                        {
                            string outer_id = assembleForm.Tag.ToString();
                            int focucedHandle = gridViewItem.FocusedRowHandle;
                            gridViewItem.SetRowCellValue(focucedHandle, gcOuter_id, outer_id);
                            gridViewItem.RefreshRowCell(focucedHandle, gcOuter_id);
                            textEditOuterId.Text = outer_id;
                            gridViewItem.SetRowCellValue(focucedHandle, gcItemType, "组合商品");
                            gridViewItem.RefreshRowCell(focucedHandle, gcItemType);
                        }
                        break;
                    case "包装":
                        //加载不含基本计量单位的计量单位组
                        treeUnit.Nodes.Clear();
                        LoadUnitForPacage();
                        cbStockUnit.Focus();
                        break;
                    case "普通":
                        //加载基本计量单位
                        LoadBaseUnit();
                        break;
                    default :
                        break;

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void treeUnit_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                TreeListHitInfo hitInfor = treeUnit.CalcHitInfo(new Point(e.X, e.Y));
                TreeListNode hitNode = hitInfor.Node;
                if (hitNode != null && hitNode.Tag != null && !hitNode.HasChildren)
                {
                    TreeListNodeTag tag = hitNode.Tag as TreeListNodeTag;
                    cbStockUnit.Tag = tag.Cid;
                    cbStockUnit.Text = hitNode.GetDisplayText(0);
                    cbStockUnit.ClosePopup();
                }
            }
        }

        //当宝贝计量单位改为“包装”型后，修改当前宝贝在本地数据库类型与gridViewItem中显示类型
        private void cbStockUnit_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //获得当前选中的行的DataRow数据
                View_ShopItem viewItem = gridViewItem.GetFocusedRow() as View_ShopItem;
                if (viewItem == null)
                {
                    return;
                }
                if (cbItemType.Text == "包装")
                {
                    if (!string.IsNullOrEmpty(cbStockUnit.Text))
                    {
                        //更新gridViewItem中当前行宝贝计量单位
                        int focucedHandle = gridViewItem.FocusedRowHandle;
                        gridViewItem.SetRowCellValue(focucedHandle, gCStockUnit, cbStockUnit.Text);
                        gridViewItem.RefreshRowCell(focucedHandle, gCStockUnit);
                        gridViewItem.SetRowCellValue(focucedHandle, gcItemType, "包装商品");
                        gridViewItem.RefreshRowCell(focucedHandle, gcItemType);

                        //根据索引找到新计量单位的stockUnitCode，改变本地宝贝计量单位
                        string stockUnitCode = cbStockUnit.Tag.ToString();
                        ItemService.UpdateItemStockUnit(viewItem.iid, stockUnitCode, "包装商品");
                    }
                }
                else if (cbItemType.Text == "普通")
                {
                    if (!string.IsNullOrEmpty(cbStockUnit.Text))
                    {
                        //更新gridViewItem中当前行宝贝计量单位
                        int focucedHandle = gridViewItem.FocusedRowHandle;
                        gridViewItem.SetRowCellValue(focucedHandle, gCStockUnit, cbStockUnit.Text);
                        gridViewItem.RefreshRowCell(focucedHandle, gCStockUnit);

                        //根据索引找到新计量单位的stockUnitCode，改变本地宝贝计量单位
                        string stockUnitCode = cbStockUnit.Tag.ToString();
                        ItemService.UpdateItemStockUnit(viewItem.iid, stockUnitCode, "普通商品");
                    }
                }
                else if (cbItemType.Text == "组合")
                {
                    if (!string.IsNullOrEmpty(cbStockUnit.Text))
                    {
                        //更新gridViewItem中当前行宝贝计量单位
                        int focucedHandle = gridViewItem.FocusedRowHandle;
                        gridViewItem.SetRowCellValue(focucedHandle, gCStockUnit, cbStockUnit.Text);
                        gridViewItem.RefreshRowCell(focucedHandle, gCStockUnit);

                        //根据索引找到新计量单位的stockUnitCode，改变本地宝贝计量单位
                        string stockUnitCode = cbStockUnit.Tag.ToString();
                        ItemService.UpdateItemStockUnit(viewItem.iid, stockUnitCode, "组合商品");
                    }
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //取消关联
        private void btnCancelAssociate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
             try
             {
                //获得当前选中的行的DataRow数据
                View_ShopItem viewItem = gridViewItem.GetFocusedRow() as View_ShopItem;
                if (viewItem == null)
                {
                    return;
                }
                //在本地修改当前宝贝为未关联
                List<string> iidList = new List<string>();
                iidList.Add(viewItem.iid);
                ItemService.UpdateItemsAssociate(iidList, false);
                gcStockProduct.DataSource = null;
             }
             catch (Exception ex)
             {
                 XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
        }
        #endregion

        class Props
        {
            /// <summary>
            /// 属性所属分类vid，如颜色vid相当于pid
            /// </summary>
            public string pid
            {
                set;
                get;
            }
            /// <summary>
            /// 属性vid
            /// </summary>
            public string vid
            {
                set;
                get;
            }
            /// <summary>
            /// 属性值
            /// </summary>
            public string value
            {
                set;
                get;
            }
        }
    }
}