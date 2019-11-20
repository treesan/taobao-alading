using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Business;
using DevExpress.XtraTreeList.Nodes;
using Alading.Utils;
using Alading.Taobao.API;
using Alading.Taobao.Entity.Extend;
using DevExpress.Utils;
using Alading.Taobao;


namespace Alading.Forms.Item
{
    public partial class SellerCatForm : DevExpress.XtraEditors.XtraForm
    {
        public SellerCatForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 当前卖家自定义类目
        /// </summary>
        List<Alading.Entity.SellerCat> listSellerCat = new List<Alading.Entity.SellerCat>();

        #region 加载店铺
        private void SellerCatForm_Load(object sender, EventArgs e)
        {
            try
            {
                List<Alading.Entity.Shop> listShop = ShopService.GetAllShop();
                if (listShop != null)
                {
                    //加载店铺
                    foreach (Alading.Entity.Shop shop in listShop)
                    {
                        comboBoxEditShop.Properties.Items.Add(shop.nick);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region labelControlSellerCatName操作
        private void barButtonItemNewFather_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBoxEditShop.Text))
            {
                textEditSellerCatName.Text = string.Empty;
                labelControlSellerCatName.Text = "添加新类目";
            }
        }

        private void barButtonItemNewChid_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBoxEditShop.Text))
            {
                TreeListNode focusedNode = treeListSellerCats.FocusedNode;
                if (focusedNode != null)
                {
                    if (focusedNode.ParentNode != null)
                    {
                        XtraMessageBox.Show("子类目下不能再添加子类目！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        labelControlSellerCatName.Text = "添加子类目";
                    }
                }
            }
        }
        #endregion

        #region 加载类目
        private void comboBoxEditShop_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBoxEditShop.Text))
            {
                LoadSellerCats(comboBoxEditShop.Text);
            }
        }

        /// <summary>
        /// 加载当前卖家的自定义类目
        /// </summary>
        /// <param name="sellerNick">当前卖家昵称</param>
        private void LoadSellerCats(string sellerNick)
        {
            try
            {
                treeListSellerCats.Nodes.Clear();
                List<Alading.Entity.SellerCat> listSellerCat = SellerCatService.GetSellerCat(p => p.SellerNick == sellerNick);
                foreach (Alading.Entity.SellerCat sellerCat in listSellerCat)
                {
                    if (sellerCat.parent_cid == "0")
                    {
                        TreeListNode fatherNode = treeListSellerCats.AppendNode(new object[] { sellerCat.name }, null);
                        fatherNode.Tag = sellerCat.cid + "," + sellerCat.sort_order;
                        AppendChildNodes(listSellerCat, fatherNode);
                    }
                }
                treeListSellerCats.ExpandAll();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 加载当前类目的子类目
        /// </summary>
        /// <param name="listSellerCat">当前卖家所有类目</param>
        /// <param name="fatherNode">父类目</param>
        private void AppendChildNodes(List<Alading.Entity.SellerCat> listSellerCat, TreeListNode fatherNode)
        {
            foreach (Alading.Entity.SellerCat sellerCat in listSellerCat)
            {
                if (fatherNode.Tag != null)
                {
                    string[] tagArray = fatherNode.Tag.ToString().Split(',');
                    if (tagArray.Length == 2)
                    {
                        if (sellerCat.parent_cid == tagArray[0])
                        {
                            TreeListNode childNode = treeListSellerCats.AppendNode(new object[] { sellerCat.name }, fatherNode, sellerCat.cid);
                            childNode.Tag = sellerCat.cid + "," + sellerCat.sort_order;
                        }
                    }
                }
            }
        }

        /// <summary>
        ///  从淘宝网下载卖家自定义类目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonDownLoad_Click(object sender, EventArgs e)
        {
            WaitDialogForm waitForm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                if (!string.IsNullOrEmpty(comboBoxEditShop.Text))
                {
                    UIHelper.DownSellerCatsList(comboBoxEditShop.Text);
                }
                LoadSellerCats(comboBoxEditShop.Text);
                waitForm.Close();
                XtraMessageBox.Show("下载成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                waitForm.Close();
                XtraMessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        # endregion

        #region 类目编辑
        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            switch (labelControlSellerCatName.Text)
            {
                case "添加新类目":
                    AddFatherNode();
                    textEditSellerCatName.Text = string.Empty;
                    labelControlSellerCatName.Text = "修改类目";
                    break;
                case "添加子类目":
                    AddChildNode();
                    textEditSellerCatName.Text = string.Empty;
                    labelControlSellerCatName.Text = "修改类目";
                    break;
                case "修改类目":
                    SellerCatEdit();
                    textEditSellerCatName.Text = string.Empty;
                    break;
            }
        }

        /// <summary>
        /// 更新类目
        /// </summary>
        private void SellerCatEdit()
        {
            WaitDialogForm waitForm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                TreeListNode focusedNode = treeListSellerCats.FocusedNode;
                if (focusedNode != null && focusedNode.Tag != null)
                {
                    string[] tagArray = focusedNode.Tag.ToString().Split(',');
                    if (tagArray.Length == 2)
                    {
                        string session = SystemHelper.GetSessionKey(comboBoxEditShop.Text);
                        ShopRsp shopRsp = TopService.SellerCatsListUpdate(session, textEditSellerCatName.Text, string.Empty, tagArray[0], int.Parse(tagArray[1]));
                        //下载卖家自定义类目
                        UIHelper.DownSellerCatsList(comboBoxEditShop.Text);
                        //加载当前卖家的自定义类目
                        LoadSellerCats(comboBoxEditShop.Text);
                        waitForm.Close();
                        XtraMessageBox.Show("类目更新成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                waitForm.Close();
                XtraMessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 添加子类目
        /// </summary>
        private void AddChildNode()
        {
            WaitDialogForm waitForm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                TreeListNode focusedNode = treeListSellerCats.FocusedNode;
                if (focusedNode != null && focusedNode.Tag != null)
                {
                    string[] tagArray = focusedNode.Tag.ToString().Split(',');
                    if (tagArray.Length == 2)
                    {
                        List<Alading.Entity.SellerCat> listsellercat = listSellerCat.FindAll(delegate(Alading.Entity.SellerCat sellercat) { return sellercat.parent_cid == tagArray[1]; });
                        if (listsellercat != null)
                        {
                            int sortOrder = listsellercat.Count + 1;
                            string session = SystemHelper.GetSessionKey(comboBoxEditShop.Text);
                            ShopRsp shopRsp = TopService.SellerCatsListAdd(session, textEditSellerCatName.Text, string.Empty, int.Parse(tagArray[0]), sortOrder);
                            //下载卖家自定义类目
                            UIHelper.DownSellerCatsList(comboBoxEditShop.Text);
                            //加载当前卖家的自定义类目
                            LoadSellerCats(comboBoxEditShop.Text);
                            waitForm.Close();
                            XtraMessageBox.Show("新类目添加成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                waitForm.Close();
                XtraMessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 添加父类目
        /// </summary>
        private void AddFatherNode()
        {
            WaitDialogForm waitForm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                List<Alading.Entity.SellerCat> listsellercat = listSellerCat.FindAll(delegate(Alading.Entity.SellerCat sellercat) { return sellercat.parent_cid == "0"; });
                if (listsellercat != null)
                {
                    int sortOrder = listsellercat.Count + 1;
                    string session = SystemHelper.GetSessionKey(comboBoxEditShop.Text);
                    ShopRsp shopRsp = TopService.SellerCatsListAdd(session, textEditSellerCatName.Text, string.Empty, 0, sortOrder);
                    //下载卖家自定义类目
                    UIHelper.DownSellerCatsList(comboBoxEditShop.Text);
                    //加载当前卖家的自定义类目
                    LoadSellerCats(comboBoxEditShop.Text);
                    waitForm.Close();
                    XtraMessageBox.Show("新类目添加成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                 waitForm.Close();
                 XtraMessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void treeListSellerCats_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            TreeListNode focusedNode = treeListSellerCats.FocusedNode;
            if (focusedNode != null)
            {
                labelControlSellerCatName.Text = "修改类目";
                textEditSellerCatName.Text = focusedNode.GetDisplayText(0);
            }
        }
        #endregion
    }
}