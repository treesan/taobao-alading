using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Entity;

namespace Alading.Forms.Trade.Forms
{
    public partial class TradeProductSelect : DevExpress.XtraEditors.XtraForm
    {
        //用设计器添加控件不行 只能手动添加控件
        Alading.Forms.Stock.Control.ProductSelect _productSelect = new Alading.Forms.Stock.Control.ProductSelect(false,new DataTable(),null);

        List<View_StockItemProduct> _selectedItems = null;//保存当前已选择products

        /// <summary>
        /// 只读索引器,取得当前选中的Product列表
        /// </summary>
        public List<View_StockItemProduct> SelectedItems
        {
            get { return _selectedItems; }
        }

        //自定义构造函数   手动添加控件
        public TradeProductSelect()
        {
            InitializeComponent();
            panelProductSelect.Controls.Add(_productSelect);
            _productSelect.Dock = DockStyle.Fill;
            this.DialogResult = DialogResult.Cancel;//默认框值为Canel，如果框值为Canel，说明已经未选择商品;
        }


        private void btnStore_Click(object sender, EventArgs e)
        {
            _selectedItems = _productSelect.GetSelectedItems();//由ProductSelect控件提供的接口,获得当前已经选择的商品
            if (_selectedItems != null)
            {
                this.DialogResult = DialogResult.OK;//如果框值为OK，说明已经选择了商品;
                this.Close();
            }
            else
            {
                XtraMessageBox.Show("你当前并未选择商品,请确认你已经选择商品！");
                return;
            }
        }

        private void btnCanel_Click(object sender, EventArgs e)
        {
            DialogResult result = XtraMessageBox.Show("确认退出选择商品？", "确认", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                this.DialogResult = DialogResult.Cancel;//如果框值为Canel，说明已经未选择商品;
                this.Close();
            }
            else
            {
                return;
            }
        }
    }
}