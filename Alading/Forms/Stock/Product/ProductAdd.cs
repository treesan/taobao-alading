using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using Alading.Entity;
using Alading.Business;
using Alading.Taobao;

namespace Alading.Forms.Stock.Product
{
    public partial class ProductAdd : DevExpress.XtraEditors.XtraForm
    {
        public ProductAdd()
        {
            InitializeComponent();            
        }

        public ProductAdd(WaitDialogForm waitForm)
        {
            InitializeComponent();
            waitForm.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
            waitForm.Show();
            try
            {
                List<StockHouseProduct> shpList = new List<StockHouseProduct>();
                StockItem stockItem = new StockItem();
                List<Alading.Entity.StockProduct> stockProductList = new List<Alading.Entity.StockProduct>();
                List<StockDetail> sdList = new List<StockDetail>();
                if (productAddCtrl1.GetData(waitForm, stockItem, stockProductList, sdList, shpList))
                {
                    StockItemService.AddStockItemProducts(stockItem, stockProductList, sdList, shpList);
                    waitForm.Close();
                    XtraMessageBox.Show("保存成功！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    waitForm.Close();
                    XtraMessageBox.Show("保存失败！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                waitForm.Close();
                XtraMessageBox.Show(ex.ToString(), Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
            waitForm.Show();
            try
            {
                List<StockHouseProduct> shpList = new List<StockHouseProduct>();
                StockItem stockItem = new StockItem();
                List<Alading.Entity.StockProduct> stockProductList = new List<Alading.Entity.StockProduct>();
                List<StockDetail> sdList = new List<StockDetail>();
                if (productAddCtrl1.GetData(waitForm, stockItem, stockProductList, sdList, shpList))
                {
                    StockItemService.AddStockItemProducts(stockItem, stockProductList, sdList, shpList);
                    productAddCtrl1.AllClear();
                    waitForm.Close();
                    XtraMessageBox.Show("保存成功！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    productAddCtrl1.StockHouseFlag = false;
                    productAddCtrl1.StockLayoutFlag = false;
                }
                else
                {
                    XtraMessageBox.Show("保存失败！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    waitForm.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                waitForm.Close();
                XtraMessageBox.Show(ex.ToString(), Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
            
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}