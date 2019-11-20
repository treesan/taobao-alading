using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Entity;
using Alading.Business;
using Alading.Utils;
using Alading.Core.Enum;
using System.Linq;
using Alading.Taobao;

namespace Alading.Forms.Stock.Allocation
{
    public partial class StockAllocation : DevExpress.XtraEditors.XtraForm
    {       
        
        public StockAllocation()
        {
            InitializeComponent();
        }

        #region 调拨单列表
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AllocationAdd allocationAdd = new AllocationAdd();
            allocationAdd.ShowDialog();
            Init();// 刷新
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnModify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           //Alading.Entity.StockInOut stockInOut = gvAllocation.GetFocusedRow() as Alading.Entity.StockInOut;
           //if (stockInOut != null)
           //{
           //    DataTable dTable=gridStockProduct.DataSource as DataTable;
           //    AllocationAdd allocationAdd = new AllocationAdd(stockInOut, dTable);
           //    allocationAdd.ShowDialog();
           //    Init();// 刷新
           //} 
            
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Alading.Entity.StockInOut stockInOut = gvAllocation.GetFocusedRow() as Alading.Entity.StockInOut;
            if (stockInOut != null)
            {
                if (XtraMessageBox.Show(string.Format("是否删除编号为\n{0}\n的调拨单?", stockInOut.InOutCode),"系统提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (StockInOutService.RemoveStockInOutDetail(stockInOut.InOutCode) == ReturnType.Success)
                    {
                        XtraMessageBox.Show("删除成功");
                        Init();//刷新
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
            Init();
        }

        private void gvAllocation_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {          
            FocusedRowChange();// 展示调拨详情
        }
        #endregion


        #region 公共方法
        /// <summary>
        /// 初始化、刷新
        /// </summary>
        private void Init()
        {
            int rowHandle = gvAllocation.FocusedRowHandle;
            List<Alading.Entity.StockInOut> stockInOutList = StockInOutService.GetStockInOut(i => i.InOutType == (int)InOutType.AllocateIn);

            gridAllocation.DataSource = stockInOutList;
            if (rowHandle == 0 && gvAllocation.FocusedRowHandle > -1)
            {
                FocusedRowChange();
            }
            //gvAllocation.FocusedRowChanged += gvAllocation_FocusedRowChanged;
            //}
        }
        /// <summary>
        /// 焦点行改变触发展示调拨详情
        /// </summary>
        private void FocusedRowChange()
        {
            Alading.Entity.StockInOut stockInOut = gvAllocation.GetFocusedRow() as Alading.Entity.StockInOut;
            if (stockInOut == null)
            {
                gridStockProduct.DataSource = null;
            }
            else
            {
                List<View_StockDetailInOut> detailList = StockInOutService.GetStockDetailInOut(i => i.InOutCode == stockInOut.InOutCode);
                DataTable dTable = new DataTable();
                dTable.Columns.Add("Name");
                dTable.Columns.Add("OuterID");
                dTable.Columns.Add("Specification");
                dTable.Columns.Add("Model");
                dTable.Columns.Add("SaleProps");

                dTable.Columns.Add("SkuOuterID");
                dTable.Columns.Add("SkuQuantity");
                //dTable.Columns.Add("StockHouseCodeOut");
                //dTable.Columns.Add("StockHouseCodeIn");
                dTable.Columns.Add("LayoutCodeIn");

                dTable.Columns.Add("LayoutCodeOut");
                //dTable.Columns.Add("HouseNameIn");
                dTable.Columns.Add("LayoutNameIn");
                //dTable.Columns.Add("HouseNameOut");
                dTable.Columns.Add("LayoutNameOut");

                //List<string> inOutCodeList = new List<string>();
                //var q = from i in detailList
                //        select i.InOutCode;
                //inOutCodeList = q.Distinct().ToList();
                //if (inOutCodeList == null || inOutCodeList.Count == 0)
                //    return;

                //foreach (string inOutCode in inOutCodeList)
                //{

                List<View_StockDetailInOut> stockOutList = detailList.Where(i => i.InOutCode == stockInOut.InOutCode && i.DetailType == (int)DetailType.AllocateOut).ToList();
                List<View_StockDetailInOut> stockInList = detailList.Where(i => i.InOutCode == stockInOut.InOutCode && i.DetailType == (int)DetailType.AllocateIn).ToList();

                if (stockOutList != null && stockInList != null && stockOutList.Count() > 0 && stockOutList.Count() == stockInList.Count())
                    for (int i = 0; i < stockOutList.Count(); i++)
                    {
                        View_StockDetailInOut stockOut = stockOutList[i];
                        View_StockDetailInOut stockIn = stockInList[i];

                        DataRow dRow = dTable.NewRow();
                        if (stockIn != null)
                        {
                            dRow["Name"] = stockIn.Name;
                            dRow["OuterID"] = stockIn.OuterID;
                            dRow["Specification"] = stockIn.Specification;
                            dRow["Model"] = stockIn.Model;
                            dRow["SaleProps"] = stockIn.SkuProps_Str;
                            //dRow["HouseNameIn"] = stockIn.HouseName;
                            dRow["LayoutNameIn"] = stockIn.LayoutName;
                            //dRow["StockHouseCodeIn"] = stockIn.StockHouseCode;
                            dRow["LayoutCodeIn"] = stockIn.StockLayOutCode;
                        }
                        if (stockOut != null)
                        {
                            dRow["SkuOuterID"] = stockOut.ProductSkuOuterId;
                            dRow["SkuQuantity"] = stockOut.Quantity;
                            //dRow["StockHouseCodeOut"] = stockOut.StockHouseCode;
                            dRow["LayoutCodeOut"] = stockOut.StockLayOutCode;
                            //dRow["HouseNameOut"] = stockOut.HouseName;
                            dRow["LayoutNameOut"] = stockOut.LayoutName;
                        }
                        dTable.Rows.Add(dRow);
                        //}
                    }

                gridStockProduct.DataSource = dTable;
                gVStockProduct.BestFitColumns();
            }
        }        

        #endregion

        private void StockAllocation_Load(object sender, EventArgs e)
        {
            Init();// 初始化
        }

        private void SearchAllocation(DateTime start)
        {
            int rowHandle = gvAllocation.FocusedRowHandle;
            List<Alading.Entity.StockInOut> stockInOutList = StockInOutService.GetStockInOut(i => i.InOutType == (int)InOutType.AllocateIn);

            gridAllocation.DataSource = stockInOutList.Where(i => i.InOutTime >= start).ToList();
            
            if (rowHandle == 0 && gvAllocation.FocusedRowHandle > -1)
            {
                FocusedRowChange();
            }
        }

        #region 按时间
        /// <summary>
        /// 近一周
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarWeek_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SearchAllocation(DateTime.Now.AddDays(-7));
        }
        /// <summary>
        /// 近一月
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarMonth_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SearchAllocation(DateTime.Now.AddMonths(-1));
        }
        /// <summary>
        /// 近三月
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarThreeMonth_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SearchAllocation(DateTime.Now.AddMonths(-3));
        }
        /// <summary>
        /// 近半年
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarHalfYear_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SearchAllocation(DateTime.Now.AddMonths(-6));
        }
        /// <summary>
        /// 近一年
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarYear_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SearchAllocation(DateTime.Now.AddYears(-1));
        }

        #endregion

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleBtnSearch_Click(object sender, EventArgs e)
        {
            int rowHandle = gvAllocation.FocusedRowHandle;
            if (textKeyWord.Text == string.Empty)
            {
                XtraMessageBox.Show("请输入关键词",Constants.SYSTEM_PROMPT);
                return;
            }

            List<Alading.Entity.StockInOut> stockInOutList = StockInOutService.GetStockInOut(i => i.InOutType == (int)InOutType.AllocateIn);
            gridAllocation.DataSource = stockInOutList.Where(i => i.InOutCode.Contains(textKeyWord.Text) || i.InOutTime.ToString().Contains(textKeyWord.Text) || i.OperatorName.Contains(textKeyWord.Text)).ToList();
            if (rowHandle == 0 && gvAllocation.FocusedRowHandle > -1)
            {
                FocusedRowChange();
            }

        }
    }   
}