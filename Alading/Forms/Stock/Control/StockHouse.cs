using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Entity;
using DevExpress.XtraTreeList.Nodes;
using Alading.Business;
using Alading.Taobao;
using Alading.Core.Enum;

namespace Alading.Forms.Stock.SettingUp
{
    [ToolboxItem(false)]
    public partial class StockHouse : DevExpress.XtraEditors.XtraUserControl
    {
        string stockHouseCode = string.Empty;
        public StockHouse()
        {
            InitializeComponent();
        }

        #region 仓库新增，修改，删除
        /// <summary>
        /// 仓库新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAddHouse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ModifyHouse modifyHouse = new ModifyHouse();
            modifyHouse.ShowDialog();
            Init();
        }
        /// <summary>
        /// 仓库修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barModifyHouse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeListStockHouse.FocusedNode == null)
                return;
            if (stockHouseCode == Constants.DEFAULT_STOCKHOUSE_CODE)
            {
                XtraMessageBox.Show("系统默认仓库,不能修改", Constants.SYSTEM_PROMPT);
                return;
            }
            List<Alading.Entity.StockHouse> stockHouseList = (List<Alading.Entity.StockHouse>)treeListStockHouse.DataSource;
            Alading.Entity.StockHouse stockHouse = stockHouseList[treeListStockHouse.FocusedNode.Id];
            if (stockHouse != null)
            {               
                ModifyHouse modifyHouse = new ModifyHouse(stockHouse);
                modifyHouse.ShowDialog();
                Init();
            }            
        }
        /// <summary>
        /// 仓库删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barDeleteHouse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeListStockHouse.FocusedNode == null)
                return;

            if (stockHouseCode == Constants.DEFAULT_STOCKHOUSE_CODE)
            {
                XtraMessageBox.Show("系统默认仓库,不能删除", Constants.SYSTEM_PROMPT);
                return;
            }
            
            /*仓库中如果有商品则不能删除*/
            if (XtraMessageBox.Show(string.Format("是否删除仓库\n{0}", textHouseName.Text), Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                List<StockHouseProduct> productList = StockHouseService.GetStockHouseProduct(i => i.HouseCode == stockHouseCode);
                if (productList == null || productList.Count == 0)
                {
                    if (StockHouseService.RemoveStockHouse(stockHouseCode) == ReturnType.Success)
                    {
                        XtraMessageBox.Show("仓库删除成功", Constants.SYSTEM_PROMPT);
                        Init();
                    }
                    else
                    {
                        XtraMessageBox.Show("仓库删除失败", Constants.SYSTEM_PROMPT);
                    }
                }
                else
                {
                    XtraMessageBox.Show("该仓库有商品,不能删除",Constants.SYSTEM_PROMPT);
                }
            }
        }
        #endregion

        #region 库位新增，修改，删除
        /// <summary>
        /// 库位新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAddLayout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ModifyLayout Layout = new ModifyLayout(stockHouseCode, textHouseName.Text);
            Layout.ShowDialog();
            //重新加载库位
            LoadLayout(stockHouseCode);
        }

        /// <summary>
        /// 库位修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barModifyLayout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            StockLayout stockLayout = gViewStockLayout.GetFocusedRow() as StockLayout;
            if (stockLayout != null)
            {
                if (stockLayout.StockLayoutCode == Constants.DEFAULT_STOCKLAYOUT_CODE || stockLayout.LayoutName == "默认库位")
                {
                    XtraMessageBox.Show("系统默认库位,不能修改", Constants.SYSTEM_PROMPT);
                    return;
                }
                ModifyLayout Layout = new ModifyLayout(stockLayout, textHouseName.Text);
                Layout.ShowDialog();
                //重新加载库位
                LoadLayout(stockHouseCode);
            }            
        }

        /// <summary>
        /// 库位删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barDeleteLayout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*库位中如果有商品则不能删除*/
            Alading.Entity.StockLayout layout = gViewStockLayout.GetFocusedRow() as Alading.Entity.StockLayout;
            if (layout != null)
            {
                if (layout.StockLayoutCode == Constants.DEFAULT_STOCKLAYOUT_CODE || layout.LayoutName == "默认库位")
                {
                    XtraMessageBox.Show("系统默认库位,不能删除", Constants.SYSTEM_PROMPT);
                    return;
                }
                if (XtraMessageBox.Show(string.Format("是否删除库位\n{0}", layout.LayoutName), Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    List<StockHouseProduct> productList = StockHouseService.GetStockHouseProduct(i => i.LayoutCode == layout.StockLayoutCode);
                    if (productList == null || productList.Count == 0)
                    {
                        if (StockLayoutService.RemoveStockLayout(layout.StockLayoutCode) == ReturnType.Success)
                        {
                            XtraMessageBox.Show("库位删除成功", Constants.SYSTEM_PROMPT);
                            //重新加载库位
                            LoadLayout(stockHouseCode);
                        }
                        else
                        {
                            XtraMessageBox.Show("库位删除失败", Constants.SYSTEM_PROMPT);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("该库位有商品,不能删除", Constants.SYSTEM_PROMPT);
                    }
                }
            }
        }
        #endregion

        private void StockHouse_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void treeListStockHouse_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            FocusedNode();
        }

        #region 公共方法
        /// <summary>
        /// 根据stockHouseCode加载库位
        /// </summary>
        /// <param name="houseCode"></param>
        private void LoadLayout(string stockHouseCode)
        {
            List<Alading.Entity.StockLayout> layOutList = StockLayoutService.GetStockLayout(i => i.StockHouseCode == stockHouseCode);
            gridStockLayout.DataSource = layOutList;
        }

        private void Init()
        {
            List<Alading.Entity.StockHouse> stockHouseList = StockHouseService.GetAllStockHouse();
            if (stockHouseList != null && stockHouseList.Count > 0)
            {   
                treeListStockHouse.DataSource = stockHouseList;
            }
            else
            {
                treeListStockHouse.DataSource = null;
                textHouseAddr.Text = string.Empty;
                textHouseName.Text = string.Empty;
                textEditHouseCode.Text = string.Empty;
                textContact.Text = string.Empty;
                memoHouseRemark.Text = string.Empty;
                stockHouseCode = string.Empty;
                gridStockLayout.DataSource = null;
            }          
        }

        private void FocusedNode()
        {
            if (treeListStockHouse.FocusedNode == null)
                return;
            List<Alading.Entity.StockHouse> stockHouseList = (List<Alading.Entity.StockHouse>)treeListStockHouse.DataSource;
            Alading.Entity.StockHouse stockHouse = stockHouseList[treeListStockHouse.FocusedNode.Id];// stockHouseList[e.Node.Id];
            if (stockHouse != null)
            {
                textHouseAddr.Text = stockHouse.HouseAddress;
                textHouseName.Text = stockHouse.HouseName;
                textEditHouseCode.Text = stockHouse.StockHouseCode;
                textContact.Text = stockHouse.HouseContact;
                memoHouseRemark.Text = stockHouse.HouseRemark;
                stockHouseCode = stockHouse.StockHouseCode;
                LoadLayout(stockHouse.StockHouseCode);
            }
        }



        #endregion

        private void barBtnRefreshHouse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Init();
        }

        private void barBtnRefreshLayout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FocusedNode();
        }

    }
}
