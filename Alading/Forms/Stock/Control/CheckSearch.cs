using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Entity;
using Alading.Business;
using Alading.Core.Enum;
using Alading.Taobao;
using System.Linq;

namespace Alading.Forms.Stock.Control
{
    [ToolboxItem(false)]
    public partial class CheckSearch : DevExpress.XtraEditors.XtraUserControl
    {
        
        public CheckSearch()
        {
            InitializeComponent();
            gvStockCheck.BestFitColumns();
            gridViewProductCheck.BestFitColumns();
        }

        #region 公共方法
        /// <summary>
        /// 初始化/刷新
        /// </summary>
        private void Init()
        {
            int rowHandle = gvStockCheck.FocusedRowHandle;
            List<View_StockCheck> stockCheckList = StockProductService.GetAllViewStockCheck();
            gridStockCheck.DataSource = stockCheckList;
            if (rowHandle == 0 && gvStockCheck.FocusedRowHandle>-1)
            {
                FocusedRowChange();
            }
        }
        /// <summary>
        /// 焦点行改变触发
        /// </summary>
        private void FocusedRowChange()
        {
            View_StockCheck stockCheck = gvStockCheck.GetFocusedRow() as View_StockCheck;
            if (stockCheck != null)
            {     
                List<View_CheckDetail> checkDetailList = StockProductService.GetViewCheckDetail(stockCheck.StockCheckCode);
                gridCtrlProductCheck.DataSource = checkDetailList;
            }
            gvStockCheck.BestFitColumns();
            gridViewProductCheck.BestFitColumns();
        }

        #endregion

        private void gvStockCheck_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {          
            FocusedRowChange();
        }

        private void CheckSearch_Load(object sender, EventArgs e)
        {
            Init();// 初始化
        }
       
        //private void comboBoxStockHouse_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    List<View_CheckDetail> checkDetailList = new List<View_CheckDetail>();
        //    switch (comboBoxStockHouse.SelectedIndex)
        //    {
                  
        //        case 0:
        //            //正常
        //            //checkDetailList=StockProductService.GetViewStockCheck(i=>i.profit
        //            break;
        //        case 1:
        //            //报溢
        //            break;
        //        case 2:
        //            //报损
        //            break;
        //    }
        //    gridStockCheck.DataSource = checkDetailList;
        //    if (rowHandle == 0 && gvStockCheck.FocusedRowHandle > -1)
        //    {
        //        FocusedRowChange();
        //        rowHandle = 0;
        //    }
        //}

        private void barBtnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Init();// 刷新
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int rowHandle = gvStockCheck.FocusedRowHandle;
            if (textKeyWord.Text == string.Empty)
            {
                XtraMessageBox.Show("请输入关键词", Constants.SYSTEM_PROMPT);
                return;
            }
            List<View_StockCheck> stockCheckList = StockProductService.GetAllViewStockCheck();
            gridStockCheck.DataSource = stockCheckList.Where(i=>i.StockCheckCode.Contains(textKeyWord.Text) || i.HouseName.Contains(textKeyWord.Text)
                || i.nick.Contains(textKeyWord.Text) || i.Created.ToString().Contains(textKeyWord.Text)).ToList();
            if (rowHandle == 0 && gvStockCheck.FocusedRowHandle > -1)
            {
                FocusedRowChange();
            }
        }
      
    }
}
