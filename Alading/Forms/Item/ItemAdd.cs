using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Taobao.Entity.Extend;
using Alading.Utils;
using Alading.Taobao.API;
using Alading.Business;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;

namespace Alading.Forms.Item
{
    public partial class ItemAdd : DevExpress.XtraEditors.XtraForm
    {
        private ItemReq itemreq;

        public ItemAdd()
        {
            InitializeComponent();
        }

        public ItemAdd(ItemReq req)
        {
            InitializeComponent();
            this.itemreq = req;
        }


        private void ItemAdd_Load(object sender, EventArgs e)
        {
            foreach (Alading.Entity.Shop shop in SystemHelper.ShopList)
            {
                this.ccbNick.Properties.Items.Add(shop.nick);
            }
        }

        private void ccbNick_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ccbNick.Text))
            {
                treeListSellerCat.Nodes.Clear();
                List<Alading.Entity.SellerCat> sellercatList = SellerCatService.GetSellerCat(p => p.SellerNick == ccbNick.Text);
                //TreeListNode selleNode = treeListSellerCat.AppendNode(new object[] { ccbNick.Text }, null);
                AddNodes(null, treeListSellerCat, sellercatList);
                treeListSellerCat.ExpandAll();
            }
        }

        private void AddNodes(TreeListNode rootNode, TreeList treeList, List<Alading.Entity.SellerCat> sellercatList)
        {
            if (sellercatList != null)
            {
                foreach (Alading.Entity.SellerCat sellerCat in sellercatList)
                {
                    if (sellerCat.parent_cid == "0")
                    {
                        TreeListNode childNode = treeList.AppendNode(new object[] { sellerCat.name }, rootNode);
                        childNode.Tag = sellerCat.cid;
                        AppendNodes(sellerCat.cid, childNode, treeList, sellercatList);
                    }
                }
            }
        }

        private void AppendNodes(string sellerCatCid, TreeListNode rootNode, TreeList treeList, List<Alading.Entity.SellerCat> sellercatList)
        {
            foreach (Alading.Entity.SellerCat sellerCat in sellercatList)
            {
                if (sellerCat.parent_cid == sellerCatCid)
                {
                    TreeListNode childNode = treeList.AppendNode(new object[] { sellerCat.name }, rootNode);
                    childNode.Tag = sellerCat.cid;
                    AppendNodes(sellerCat.cid, childNode, treeList, sellercatList);
                }
            }
        }

        private void btnStart_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ccbNick.Text))
                {
                    XtraMessageBox.Show("请选择店铺！");
                    return;
                }

                #region 买家自定义类目
                //读取买家自定义类目列表中被选中的子节点
                string sellercids = string.Empty;
                foreach (TreeListNode node in treeListSellerCat.Nodes)
                {
                    if (node.HasChildren)
                    {
                        foreach (TreeListNode childNode in node.Nodes)
                        {
                            if (childNode.CheckState == CheckState.Checked)
                            {
                                sellercids += string.Format("{0},", childNode.Tag.ToString());
                            }
                        }
                    }
                    else
                    {
                        if (node.CheckState == CheckState.Checked)
                        {
                            sellercids += string.Format("{0},", node.Tag.ToString());
                        }
                    }
                }
                itemreq.SellerCids = sellercids.TrimEnd(',');
                #endregion

                /*在这里判断stockItem中是否存在此商品，不存在要生成一个*/
                string session = SystemHelper.GetSessionKey(ccbNick.Text);
                ItemRsp addRsp = TopService.ItemAdd(session, itemreq);
                //上传成功执行
                if (addRsp != null && addRsp.Item != null)
                {
                    ItemRsp rsp = TopService.ItemGet(session, ccbNick.Text, addRsp.Item.Iid, null);
                    Alading.Entity.Item item = new Alading.Entity.Item();
                    if (rsp.Item == null)
                    {
                        rsp.Item = itemreq as Taobao.Entity.Item;
                    }
                    UIHelper.ItemCopyData(item, rsp.Item);
                    ItemService.AddItem(item);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
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
    }
}