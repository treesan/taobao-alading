using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Business;
using DevExpress.XtraTreeList.Nodes;
using System.Linq;
using Alading.Utils;
using Alading.Taobao;
using Alading.Entity;
using DevExpress.XtraTreeList;
using DevExpress.Utils;
using Alading.Core.Enum;

namespace Alading.Forms.Stock.SettingUp
{
    [ToolboxItem(false)]
    public partial class StockCat : DevExpress.XtraEditors.XtraUserControl
    {
        public StockCat()
        {
            InitializeComponent();
        }

        private void StockCat_Load(object sender, EventArgs e)
        {
            Init(null);
        }

        #region 公共方法

        bool flag = true;

        /// <summary>
        /// 初始化/刷新
        /// </summary>
        private void Init(TreeListNode fnode)
        {
            if (flag)
            {
                treeListStockCat.Nodes.Clear();
                //只加载最顶级的类目
                List<Alading.Entity.StockCat> stockCatList = StockCatService.GetStockCat(i => i.ParentCid == "0");
                treeListStockCat.BeginUnboundLoad();
                foreach (Alading.Entity.StockCat stockCat in stockCatList)
                {
                    TreeListNode node = treeListStockCat.AppendNode(new object[] { stockCat.StockCatName }, null, new TreeListNodeTag(stockCat.StockCid));
                    //设置是否有子节点，有则会显示一个+号
                    node.HasChildren = stockCat.IsParent;
                }
                treeListStockCat.EndUnboundLoad();
                //不可编辑
                treeListStockCat.OptionsBehavior.Editable = false;
                treeListStockCat.FocusedNodeChanged += treeListStockCat_FocusedNodeChanged;
                flag = false;
                if (treeListStockCat.FocusedNode != null)
                {
                    TreeListNodeTag tag = treeListStockCat.FocusedNode.Tag as TreeListNodeTag;
                    textCatCode.Text = tag.Cid;
                    textCatName.Text = treeListStockCat.FocusedNode.GetDisplayText(0);
                    gridCtrlStockProp.DataSource = StockPropService.GetStockProp(c => c.StockCid == tag.Cid);
                    Alading.Entity.StockProp stockProp = gridViewStockProp.GetFocusedRow() as Alading.Entity.StockProp;
                    if (stockProp != null)
                    {
                        gridCtrlStockPropValue.DataSource = StockPropValueService.GetStockPropValue(c => c.StockPid == stockProp.StockPid);
                    }
                    else
                    {
                        gridCtrlStockPropValue.DataSource = null;
                    }
                }
            }
            else if (fnode != null)
            {
                TreeListNodeTag tag = fnode.Tag as TreeListNodeTag;
                textCatCode.Text = tag.Cid;
                textCatName.Text = fnode.GetDisplayText(0);
                gridCtrlStockProp.DataSource = StockPropService.GetStockProp(c => c.StockCid == tag.Cid);
                Alading.Entity.StockProp stockProp = gridViewStockProp.GetFocusedRow() as Alading.Entity.StockProp;
                if (stockProp != null)
                {
                    gridCtrlStockPropValue.DataSource = StockPropValueService.GetStockPropValue(c => c.StockPid == stockProp.StockPid);
                }
                else
                {
                    gridCtrlStockPropValue.DataSource = null;
                }
            }
        }

        private void LoadCat(List<Alading.Entity.StockCat> stockCatList,int parentNodeID)
        {
            foreach (Alading.Entity.StockCat stockCat in stockCatList)
            {
                TreeListNode node = treeListStockCat.AppendNode(new object[] { stockCat.StockCatName }, parentNodeID, new TreeListNodeTag(stockCat.StockCid));
                //设置是否有子节点，有则会显示一个+号
                node.HasChildren = stockCat.IsParent;
            }
        }
      
        #endregion

        #region 类目新增，修改，删除,刷新
        /// <summary>
        /// 类目新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAddCat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
             
        }
        /// <summary>
        /// 类目修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barModifyCat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode node = treeListStockCat.FocusedNode;
            if (node != null)
            {
                TreeListNodeTag tag = node.Tag as TreeListNodeTag; 
                if (tag.Cid == "10000")
                {
                    XtraMessageBox.Show("该类目为系统内置默认类目，不能修改！", Constants.SYSTEM_PROMPT);
                    return;
                }
                string cid = tag.Cid;
                string catName = node.GetDisplayText(0);
                string fatherName = string.Empty;
                string fatherCid = "0";
                TreeListNode fatherNode = node.ParentNode;
                if (fatherNode != null)
                {
                    fatherName = fatherNode.GetDisplayText(0);
                    TreeListNodeTag fatherTag = fatherNode.Tag as TreeListNodeTag;
                    fatherCid = fatherTag.Cid;
                }
                ModifyStockCat mstockCat = new ModifyStockCat(catName, cid, fatherName, fatherCid);
                mstockCat.ShowDialog();
                if (mstockCat.DialogResult == DialogResult.OK)
                {
                    flag = true;
                    Init(null);//刷新 
                }
            }
            else
            {
                XtraMessageBox.Show("请先选中一个类目进行修改！", Constants.SYSTEM_PROMPT);
                return;
            }
        }

        /// <summary>
        /// 根据stockCid获取所有的子类目Cid
        /// </summary>
        /// <param name="stockCid"></param>
        private void GetChildrenCidList(List<string> cidList, string stockCid)
        {
            List<Alading.Entity.StockCat> catList = StockCatService.GetStockCat(i => i.ParentCid == stockCid);
            foreach (Alading.Entity.StockCat cat in catList)
            {
                if (cat.IsParent == true)
                {
                    GetChildrenCidList(cidList, cat.StockCid);
                }
                cidList.Add(cat.StockCid);
            }
        }

        /// <summary>
        /// 类目删除,如果类目CID为10000的为默认类目，不让删除。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barDeleteCat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode node = treeListStockCat.FocusedNode;
            if (node != null)
            {
                TreeListNodeTag tag = node.Tag as TreeListNodeTag;
                string cid = tag.Cid;
                if (cid == "10000")
                {
                    XtraMessageBox.Show("该类目为系统内置默认类目，不能删除！", Constants.SYSTEM_PROMPT);
                    return;
                }
                DialogResult result = XtraMessageBox.Show("确定要删除该类目及该类目下的所有属性信息吗？", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
                waitForm.Show();
                ReturnType returnType=StockCatService.DeleteStockCat(cid);
                if (returnType== ReturnType.Success)
                {
                    waitForm.Close();
                    XtraMessageBox.Show("删除类目成功！", Constants.SYSTEM_PROMPT);
                    flag = true;
                    Init(null);
                    return;
                }
                else if (returnType == ReturnType.Conflicted)
                {
                    waitForm.Close();
                    XtraMessageBox.Show("该类目为父类目，不能直接删除！请将该类目下的子类目全部删除再删除该类目！", Constants.SYSTEM_PROMPT);
                    return;
                }
                else
                {
                    waitForm.Close();
                    XtraMessageBox.Show("删除类目失败！", Constants.SYSTEM_PROMPT);
                    return;
                }
            }
            else
            {
                XtraMessageBox.Show("请先选中要删除的类目！", Constants.SYSTEM_PROMPT);
                return;
            }
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            flag = true;
            Init(null);
        }

        #endregion

        private void treeListStockCat_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
                    
        }

        private void treeListStockCat_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
        {
            //防止重复加载
            e.Node.Nodes.Clear();
            TreeListNodeTag tag = (TreeListNodeTag)e.Node.Tag;
            List<Alading.Entity.StockCat> stockCatList = StockCatService.GetStockCat(i => i.ParentCid == tag.Cid);
            LoadCat(stockCatList, e.Node.Id);
        }

        /// <summary>
        /// 添加库存自定义属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnAddNewProp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeListStockCat.FocusedNode != null && !treeListStockCat.FocusedNode.HasChildren)
            {
                string cid = textCatCode.Text;
                string catName = textCatName.Text;
                string fParentPid = "0";
                string fParentName = "";
                string fParentVid = "0";
                string fParentValueName = "";
                StockPropAdd spa = new StockPropAdd(cid, catName, fParentName, fParentPid, fParentValueName, fParentVid,false);
                spa.ShowDialog();
                WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
                waitForm.Show();
                gridCtrlStockProp.DataSource = StockPropService.GetStockProp(c => c.StockCid == cid);
                waitForm.Close();
            }
        }

        /// <summary>
        /// 修改库存自定义属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnModifyProp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            StockProp sp = gridViewStockProp.GetFocusedRow() as StockProp;
            if (sp != null)
            {
                string cid = textCatCode.Text;
                string catName = textCatName.Text;
                string fParentPid = sp.ParentPid;
                string fParentName=string.Empty;
                if (fParentPid != "0")
                {
                    fParentName = StockPropService.GetStockProp(sp.ParentPid).Name;
                }               
                string fParentVid = sp.ParentVid;
                string fParentValueName=string.Empty;
                if (fParentVid != "0")
                {
                    fParentValueName = StockPropValueService.GetStockPropValue(sp.ParentVid).Name;
                }
                StockPropAdd spa = new StockPropAdd(cid, catName, fParentName, fParentPid, fParentValueName, fParentVid,sp.StockPid,sp.Name);
                spa.ShowDialog();
                WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
                waitForm.Show();
                gridCtrlStockProp.DataSource = StockPropService.GetStockProp(c => c.StockCid == cid);
                waitForm.Close();
            }
        }

        /// <summary>
        /// 增加属性值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnAddStockPropValue_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            StockProp sp = gridViewStockProp.GetFocusedRow() as StockProp;
            if (sp != null)
            {
                string cid = textCatCode.Text;
                string catName = textCatName.Text;
                StockPropValueAdd spva = new StockPropValueAdd(cid, catName, sp.Name, sp.StockPid);
                spva.ShowDialog();
                WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
                waitForm.Show();
                gridCtrlStockPropValue.DataSource = StockPropValueService.GetStockPropValue(c => c.StockPid == sp.StockPid);
                waitForm.Close();
            }
        }

        /// <summary>
        /// 增加子属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnAddSonStockProp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            StockProp sp = gridViewStockProp.GetFocusedRow() as StockProp;
            StockPropValue spv = gridViewStockPropValue.GetFocusedRow() as StockPropValue;
            if (sp != null && spv!=null)
            {
                string cid = sp.StockCid;
                string catName = textCatName.Text;
                string fParentPid = sp.StockPid;
                string fParentName =sp.Name;
                string fParentVid = spv.StockVid;
                string fParentValueName = spv.Name;
                StockPropAdd spa = new StockPropAdd(cid, catName, fParentName, fParentPid, fParentValueName, fParentVid,true);
                spa.ShowDialog();
                WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
                waitForm.Show();
                gridCtrlStockProp.DataSource = StockPropService.GetStockProp(c => c.StockCid == cid);
                gridCtrlStockPropValue.DataSource = StockPropValueService.GetStockPropValue(c => c.StockPid == sp.StockPid);
                waitForm.Close();
            }
        }

        /// <summary>
        /// 修改子属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnModifyStockPropValue_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            StockProp sp = gridViewStockProp.GetFocusedRow() as StockProp;
            StockPropValue spv = gridViewStockPropValue.GetFocusedRow() as StockPropValue;
            if (sp != null && spv!=null)
            {
                string cid = textCatCode.Text;
                string catName = textCatName.Text;
                StockPropValueAdd spva = new StockPropValueAdd(cid, catName, sp.Name, sp.StockPid, spv.Name,spv.StockVid);
                spva.ShowDialog();
                WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
                waitForm.Show();
                gridCtrlStockPropValue.DataSource = StockPropValueService.GetStockPropValue(c => c.StockPid == sp.StockPid);
                waitForm.Close();
            }
        }

        private void treeListStockCat_MouseDown(object sender, MouseEventArgs e)
        {
            TreeListHitInfo hitInfo = treeListStockCat.CalcHitInfo(new Point(e.X, e.Y));
            
            /*如果单击到单元格内*/
            if (hitInfo.HitInfoType == HitInfoType.Cell)
            {
                WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
                waitForm.Show();
                try
                {
                    TreeListNodeTag tag = hitInfo.Node.Tag as TreeListNodeTag;
                    textCatName.Text = hitInfo.Node.GetDisplayText(0);
                    textCatCode.Text = tag.Cid;
                    if (!hitInfo.Node.HasChildren)
                    {
                        Init(hitInfo.Node);
                    }
                    waitForm.Close();
                }
                catch (Exception ex)
                {
                    waitForm.Close();
                }
            }
        }

        /// <summary>
        /// 点击属性显示其属性值列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewStockProp_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            StockProp sp = gridViewStockProp.GetFocusedRow() as StockProp;
            if (sp != null)
            {
                gridCtrlStockPropValue.DataSource = StockPropValueService.GetStockPropValue(c => c.StockPid == sp.StockPid);
            }
        }

        /// <summary>
        /// 新增同级类目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode node = treeListStockCat.FocusedNode;
            if (node != null)
            {
                TreeListNode fatherNode = node.ParentNode;
                string fatherName = string.Empty;
                string fatherCode = "0";
                if (fatherNode != null)
                {
                    fatherName = fatherNode.GetDisplayText(0);
                    TreeListNodeTag tag = fatherNode.Tag as TreeListNodeTag;
                    fatherCode = tag.Cid;
                }
                StockCatAdd stockCatAdd = new StockCatAdd(fatherName, "0");
                stockCatAdd.ShowDialog();
                if (stockCatAdd.DialogResult == DialogResult.OK)
                {
                    flag = true;
                    Init(null);//刷新 
                }
            }
            else
            {
                StockCatAdd stockCatAdd = new StockCatAdd(string.Empty, "0");
                stockCatAdd.ShowDialog();
                if (stockCatAdd.DialogResult == DialogResult.OK)
                {
                    flag = true;
                    Init(null);//刷新 
                }
            }
        }

        /// <summary>
        /// 新增子类目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewStockProp.RowCount > 0)
            {
                XtraMessageBox.Show("该类目下有属性信息，不能添加子类目！", Constants.SYSTEM_PROMPT);
                return;
            }
            TreeListNode node = treeListStockCat.FocusedNode;
            if (node != null)
            {
                TreeListNodeTag tag = node.Tag as TreeListNodeTag; 
                string fatherCode = tag.Cid;
                if (tag.Cid == "10000")
                {
                    XtraMessageBox.Show("该类目为系统内置默认类目，不能有子类目！", Constants.SYSTEM_PROMPT);
                    return;
                }
                string fatherName = node.GetDisplayText(0);                             
                StockCatAdd stockCatAdd = new StockCatAdd(fatherName, fatherCode);
                stockCatAdd.ShowDialog();
                if (stockCatAdd.DialogResult == DialogResult.OK)
                {
                    flag = true;
                    Init(null);//刷新 
                }
            }
            else
            {
                XtraMessageBox.Show("请先选中一个类目进行子节点的添加！", Constants.SYSTEM_PROMPT);
                return;
            }
        }

        /// <summary>
        /// 删除类目属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnDelProp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Alading.Entity.StockProp stockProp = gridViewStockProp.GetFocusedRow() as Alading.Entity.StockProp;
            if (stockProp != null)
            {
                DialogResult result = XtraMessageBox.Show("确定要删除该属性及该属性下的所有属性值及子属性吗？", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);                
                if (result == DialogResult.OK)
                {
                    WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
                    waitForm.Show();
                    try
                    {
                        if (StockPropService.DeleteStockPropAndValue(stockProp) == ReturnType.Success)
                        {
                            waitForm.Close();
                            XtraMessageBox.Show("删除类目属性成功！", Constants.SYSTEM_PROMPT);
                            waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
                            waitForm.Show();
                            //flag = true;
                            Init(treeListStockCat.FocusedNode);
                            waitForm.Close();                            
                        }
                        else
                        {
                            waitForm.Close();
                            XtraMessageBox.Show("删除类目属性失败！", Constants.SYSTEM_PROMPT);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        waitForm.Close();
                        XtraMessageBox.Show("删除类目属性失败！", Constants.SYSTEM_PROMPT);
                        return;
                    }
                }               
            }
        }

        /// <summary>
        /// 删除属性值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnDeletePropValue_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Alading.Entity.StockPropValue propValue = gridViewStockPropValue.GetFocusedRow() as Alading.Entity.StockPropValue;
            if (propValue != null)
            {
                DialogResult result = XtraMessageBox.Show("确定要删除该属性值及该属性值下的所有属性吗？", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
                    waitForm.Show();
                    try
                    {
                        if (StockPropValueService.RemoveStockPropValue(propValue) == ReturnType.Success)
                        {
                            waitForm.Close();
                            XtraMessageBox.Show("删除属性值成功！", Constants.SYSTEM_PROMPT);
                            waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
                            waitForm.Show();
                            Init(treeListStockCat.FocusedNode);
                            waitForm.Close();
                        }
                        else
                        {
                            waitForm.Close();
                            XtraMessageBox.Show("删除属性值失败！", Constants.SYSTEM_PROMPT);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        waitForm.Close();
                        XtraMessageBox.Show("删除属性值失败！", Constants.SYSTEM_PROMPT);
                        return;
                    }
                } 
            }
        }
    }
}
