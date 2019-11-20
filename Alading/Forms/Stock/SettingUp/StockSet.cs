using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Alading.Forms.Stock.SettingUp
{
    public partial class StockSet : DevExpress.XtraEditors.XtraForm
    {
        #region 全局变量
        /// <summary>
        /// 商品类目
        /// </summary>
        StockCat stockCat;
        /// <summary>
        /// 计量单位
        /// </summary>
        StockUnit stockUnit;
        /// <summary>
        /// 仓库管理
        /// </summary>
        StockHouse stockHouse;
        #endregion

        public StockSet()
        {
            InitializeComponent();
            //Init();
        }
        public StockSet(int num)
        {
            InitializeComponent();
            /*数字不同加载的界面不一样*/            
            LoadForm(num);
        }

        //private void Init()
        //{
        //    if (stockCat == null)
        //    {
        //        stockCat = new StockCat();
        //        stockCat.Show();
        //        stockCat.Dock = DockStyle.Fill;
        //        groupSet.Controls.Add(stockCat);
        //    }
        //    if (stockUnit == null)
        //    {
        //        stockUnit = new StockUnit();
        //        stockUnit.Show();
        //        stockUnit.Dock = DockStyle.Fill;
        //        groupSet.Controls.Add(stockUnit);
        //    }
        //    if (stockHouse == null)
        //    {
        //        stockHouse = new StockHouse();
        //        stockHouse.Show();
        //        stockHouse.Dock = DockStyle.Fill;
        //        groupSet.Controls.Add(stockHouse);
        //    }
        //}

        #region 函数区
        /// <summary>
        /// 根据数字加载不同界面
        /// </summary>
        /// <param name="num"></param>
        public void LoadForm(int num)
        {
            switch (num)
            {
                case 1:
                    if (stockCat == null)
                    {
                        stockCat = new StockCat();                        
                    }
                    stockCat.Show();
                    stockCat.Dock = DockStyle.Fill;
                    groupSet.Controls.Add(stockCat);
                    break;
                case 2:
                    if (stockUnit == null)
                    {
                        stockUnit = new StockUnit();                        
                    }
                    stockUnit.Show();
                    stockUnit.Dock = DockStyle.Fill;
                    groupSet.Controls.Add(stockUnit);
                    break;
                case 3:
                    if (stockHouse == null)
                    {
                        stockHouse = new StockHouse();                       
                    }
                    stockHouse.Show();
                    stockHouse.Dock = DockStyle.Fill;
                    groupSet.Controls.Add(stockHouse);
                    break;
                default:
                    if (stockCat == null)
                    {
                        stockCat = new StockCat();
                    }
                    stockCat.Show();
                    stockCat.Dock = DockStyle.Fill;
                    groupSet.Controls.Add(stockCat);
                    break;   
            }
        }
        #endregion


        #region 以前的代码
        /// <summary>
        /// 商品类目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navStockCat_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //if (stockCat == null)
            //    stockCat = new StockCat();
            //stockCat.Show();
            //stockCat.Dock = DockStyle.Fill;
            //panelShow.Controls.Clear();
            //panelShow.Controls.Add(stockCat);           
        }

        /// <summary>
        /// 计量单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navStockUnit_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //if (stockUnit == null)
            //    stockUnit = new StockUnit();
            //stockUnit.Show();
            //stockUnit.Dock = DockStyle.Fill;
            //panelShow.Controls.Clear();
            //panelShow.Controls.Add(stockUnit);                    
        }

        /// <summary>
        /// 仓库管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navStockHouse_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //if (stockHouse == null)
            //    stockHouse = new StockHouse();
            //stockHouse.Show();
            //stockHouse.Dock = DockStyle.Fill;
            //panelShow.Controls.Clear();
            //panelShow.Controls.Add(stockHouse);
        }

        #endregion
    }
}