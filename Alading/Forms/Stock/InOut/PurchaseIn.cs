using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.XtraVerticalGrid.Rows;
using Alading.Business;
using Alading.Entity;
using Alading.Utils;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using System.Collections;

namespace Alading.Forms.Stock.InOut
{
    public partial class PurchaseIn : DevExpress.XtraEditors.XtraForm
    {
        Alading.Entity.StockInOut stockInOut;
        Alading.Forms.Stock.Control.PurchaseIn purchasezIn=new Alading.Forms.Stock.Control.PurchaseIn();
        List<StockDetail> sdList;

        public PurchaseIn()
        {
            InitializeComponent();
            sdList = new List<StockDetail>();
            stockInOut = new Alading.Entity.StockInOut();
            purchasezIn.Parent = panelCtrlPurchaseIn;
            purchasezIn.Dock = DockStyle.Fill;
            this.WindowState = FormWindowState.Maximized;
            this.MaximizeBox = true;
        }

        public PurchaseIn(WaitDialogForm waitForm)
        {
            InitializeComponent();
            waitForm.Hide();
        }

        private void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            List<StockHouseProduct> shpList=new List<StockHouseProduct>();
            List<View_StockItemProduct> vsipList = new List<View_StockItemProduct>();
            PayCharge payCharge = new PayCharge();
            try
            {
                if (purchaseIn1.GetData(stockInOut,payCharge, sdList, shpList, vsipList))
                {
                    StockInOutService.AddInOutAndDetails(stockInOut, payCharge, sdList, shpList, vsipList);
                    stockInOut = new Alading.Entity.StockInOut();
                    sdList = new List<StockDetail>();
                    purchaseIn1.AllClear();
                }
                else
                {
                    stockInOut = new Alading.Entity.StockInOut();
                    sdList = new List<StockDetail>();
                    return;
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<StockHouseProduct> shpList = new List<StockHouseProduct>();
            List<View_StockItemProduct> vsipList = new List<View_StockItemProduct>();
            PayCharge payCharge = new PayCharge();
            try
            {
                if (purchaseIn1.GetData(stockInOut,payCharge, sdList, shpList, vsipList))
                {
                    StockInOutService.AddInOutAndDetails(stockInOut,payCharge, sdList, shpList, vsipList);
                    this.Close();
                }
                else
                {
                    stockInOut = new Alading.Entity.StockInOut();
                    sdList = new List<StockDetail>();
                    return;
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}