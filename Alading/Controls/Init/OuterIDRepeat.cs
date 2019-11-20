using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Taobao;
using Alading.Business;

namespace Alading.Controls.Init
{
    public partial class OuterIDRepeat : DevExpress.XtraEditors.XtraForm
    {
        //public List<ViewShopItemInherit> repeatVSIIList;

        public OuterIDRepeat(List<ViewShopItemInherit> repeatVSIIList,List<ViewShopItemInherit> dRepeatList)
        {
            InitializeComponent();
            //this.repeatVSIIList = repeatVSIIList;
            gridControlNotAssociate.DataSource = repeatVSIIList;
            gridControlRepeat.DataSource = dRepeatList;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            DialogResult result= XtraMessageBox.Show("如果仍有相同的商家编码将视为同一件商品处理不再入库，确定继续吗？",Constants.SYSTEM_PROMPT,MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                this.Close();
                this.DialogResult = DialogResult.Yes;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.No;            
        }

        private void gridViewNotAssociate_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == gridColumnOuterId)
            {
                if (e.Value == null || e.Value.ToString().Trim() == string.Empty)
                {
                    return;
                }
                gridViewNotAssociate.BeginUpdate();
                ViewShopItemInherit vsii = gridViewNotAssociate.GetFocusedRow() as ViewShopItemInherit;
                vsii.outer_id = e.Value.ToString();
                gridViewNotAssociate.EndUpdate();
            }
        }

        #region 切换视图
        private void barCheckItemList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.gridControlRepeat.MainView = gridViewRepeat;
            this.barCheckItemList.Checked = true;
            this.barCheckItemCard.Checked = false;
        }

        private void barCheckItemCard_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.gridControlRepeat.MainView = layoutViewRepeat;
            this.barCheckItemList.Checked = false;
            this.barCheckItemCard.Checked = true;
        } 
        #endregion

        private void gridViewRepeat_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ViewShopItemInherit vsii = gridViewRepeat.GetRow(e.FocusedRowHandle) as ViewShopItemInherit;
            if (vsii != null && vsii.outer_id!=null)
            {
                LoadStockItem(vsii.outer_id);
            }
        }

        private void layoutViewRepeat_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ViewShopItemInherit vsii = layoutViewRepeat.GetRow(e.FocusedRowHandle) as ViewShopItemInherit;
            if (vsii != null && vsii.outer_id != null)
            {
                LoadStockItem(vsii.outer_id);
            }
        }

        /// <summary>
        /// 加载宝贝库存信息
        /// </summary>
        /// <param name="outerid">宝贝商家编码</param>
        private void LoadStockItem(string outerid)
        {
            List<Alading.Entity.View_StockItemProduct> stockitemList = View_StockItemProductService.GetView_StockItemProductItem(outerid);
            if (stockitemList != null)
            {
                gridControlStock.DataSource = stockitemList;
                gridViewStock.BestFitColumns();
            }
        }

    }
}