using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Business;
using Alading.Taobao;

namespace Alading.Controls.Init
{
    public partial class ItemRepeatForm : DevExpress.XtraEditors.XtraForm
    {
        public ItemRepeatForm()
        {
            InitializeComponent();
        }

        public ItemRepeatForm(List<ViewShopItemInherit> ItemRepeatList, List<ViewShopItemInherit> DbRepeatList)
        {
            InitializeComponent();
            this.gridControlItemRepeat.DataSource = ItemRepeatList;
            this.gridControlDbRepeat.DataSource = DbRepeatList;
        }

        #region 切换视图
        private void barCheckItemRepeatList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.gridControlItemRepeat.MainView = gridViewItemRepeat;
            this.barCheckItemRepeatList.Checked = true;
            this.barCheckItemRepeatCard.Checked = false;
        }

        private void barCheckItemRepeatCard_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.gridControlItemRepeat.MainView = layoutViewItemRepeat;
            this.barCheckItemRepeatList.Checked = false;
            this.barCheckItemRepeatCard.Checked = true;
        }

        private void barCheckDbList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.gridControlDbRepeat.MainView = gridViewDbRepeat;
            this.barCheckDbList.Checked = true;
            this.barCheckDbCard.Checked = false;
        }

        private void barCheckDbCard_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.gridControlDbRepeat.MainView = layoutViewDbRepeat;
            this.barCheckDbList.Checked = false;
            this.barCheckDbCard.Checked = true;
        } 
        #endregion


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

        private void gridViewDbRepeat_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ViewShopItemInherit vsii = gridViewDbRepeat.GetRow(e.FocusedRowHandle) as ViewShopItemInherit;
            if (vsii != null && vsii.outer_id != null)
            {
                LoadStockItem(vsii.outer_id);
            }
        }

        private void layoutViewDbRepeat_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ViewShopItemInherit vsii = layoutViewDbRepeat.GetRow(e.FocusedRowHandle) as ViewShopItemInherit;
            if (vsii != null && vsii.outer_id != null)
            {
                LoadStockItem(vsii.outer_id);
            }
        }

        private void btnContinue_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult result = XtraMessageBox.Show("如果仍有相同的商家编码将视为同一件商品处理不再入库，确定继续吗？", Constants.SYSTEM_PROMPT, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                this.Close();
                this.DialogResult = DialogResult.Yes;
            }
        }

        private void btnBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.No;    
        }

    }
}