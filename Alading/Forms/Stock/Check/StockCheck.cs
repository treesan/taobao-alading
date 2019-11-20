using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Forms.Stock.Control;
using Alading.Entity;
using Alading.Business;

namespace Alading.Forms.Stock.Check
{
    public partial class StockCheck : DevExpress.XtraEditors.XtraForm
    {
        private InventoryCheck InCheck;
        private CheckSearch checkSearch;
        public StockCheck()
        {
            InitializeComponent();
        }
        public StockCheck(bool IsCheck)
        {
            InitializeComponent();
            LoadForm(IsCheck);
        }

        public void LoadForm(bool IsCheck)
        {
            if (IsCheck == true)
            {
                //库存盘点
                if (InCheck == null)
                {
                    InCheck = new InventoryCheck();
                    InCheck.Dock = DockStyle.Fill;
                }
                panelStockCheck.Controls.Clear();
                panelStockCheck.Controls.Add(InCheck);
            }
            else
            {
                //盘点查询
                if (checkSearch == null)
                {
                    checkSearch = new CheckSearch();
                    checkSearch.Dock = DockStyle.Fill;
                }
                panelStockCheck.Controls.Clear();
                panelStockCheck.Controls.Add(checkSearch);
            }
        }

        #region Nav
        /// <summary>
        /// 库存盘点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarStockCheck_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //库存盘点
            if (InCheck == null)
            {
                InCheck = new InventoryCheck();
                InCheck.Dock = DockStyle.Fill;
            }
            panelStockCheck.Controls.Clear();
            panelStockCheck.Controls.Add(InCheck);
        }
        /// <summary>
        /// 盘点查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarCheckSearch_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //盘点查询
            if (checkSearch == null)
            {
                checkSearch = new CheckSearch();
                checkSearch.Dock = DockStyle.Fill;
            }
            panelStockCheck.Controls.Clear();
            panelStockCheck.Controls.Add(checkSearch);
        }
        #endregion

    }
}