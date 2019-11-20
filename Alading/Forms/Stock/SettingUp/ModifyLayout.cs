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
using Alading.Core.Enum;
using DevExpress.XtraEditors.Controls;
using Alading.Taobao;

namespace Alading.Forms.Stock.SettingUp
{
    public partial class ModifyLayout : DevExpress.XtraEditors.XtraForm
    {
        #region 全局变量
        /// <summary>
        /// 表明是修改库位的标志
        /// </summary>
        bool modifyFlag = true;
        string stockHouseCode = string.Empty;
        string stockLayoutCode = string.Empty;
        #endregion
        public ModifyLayout(string stockHouseCode,string houseName)
        {
            InitializeComponent();
            this.Text = "新增库位";
            modifyFlag = false;
            this.stockHouseCode = stockHouseCode;
            comboBoxHouse.Text = houseName;
        }
        public ModifyLayout(StockLayout stockLayout,string houseName)
        {
            InitializeComponent();
            stockLayoutCode = stockLayout.StockLayoutCode;
            textLayoutName.Text = stockLayout.LayoutName;
            memoLayoutRemark.Text = stockLayout.LayoutRemark;
            //仓库名
            comboBoxHouse.Text = houseName;
            textEditLayoutCode.Text = stockLayout.StockLayoutCode;
            textEditLayoutCode.Properties.ReadOnly = true;
        }


        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleConfirm_Click(object sender, EventArgs e)
        {
            StockLayout stockLayout = new StockLayout();          
            stockLayout.LayoutName = textLayoutName.Text;
            stockLayout.LayoutRemark = memoLayoutRemark.Text;

            if (modifyFlag == true)
            {
                stockLayout.StockLayoutCode = stockLayoutCode;
                if (StockLayoutService.UpdateStockLayout(stockLayout) == ReturnType.Success)
                {
                    XtraMessageBox.Show("修改库位信息成功",Constants.SYSTEM_PROMPT);
                    this.Close();
                }
                else
                    XtraMessageBox.Show("修改库位信息失败", Constants.SYSTEM_PROMPT);
            }
            else
            {
                stockLayout.StockHouseCode = stockHouseCode;
                if (textEditLayoutCode.Text == string.Empty)
                {
                    stockLayout.StockLayoutCode = Guid.NewGuid().ToString();
                }
                else
                {
                    stockLayout.StockLayoutCode = textEditLayoutCode.Text;
                }
                ReturnType result=StockLayoutService.AddStockLayout(stockLayout);
                if (result == ReturnType.Success)
                {
                    XtraMessageBox.Show("新增库位成功", Constants.SYSTEM_PROMPT);
                    this.Close();
                }
                else if (result == ReturnType.PropertyExisted)
                {
                    XtraMessageBox.Show("库位编码重复,请重输", Constants.SYSTEM_PROMPT);
                    textEditLayoutCode.Focus();
                }
                else
                {
                    XtraMessageBox.Show("新增库位失败", Constants.SYSTEM_PROMPT);
                }                    
            }            
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxHouse.Tag != null)
            {
                string[] codeList = comboBoxHouse.Tag.ToString().Split(',');
                if (codeList.Length > comboBoxHouse.SelectedIndex)
                {
                    stockHouseCode = codeList[comboBoxHouse.SelectedIndex];
                }
            }
        }

        private void ModifyLayout_Load(object sender, EventArgs e)
        {
            if (modifyFlag == true)
            {
                comboBoxHouse.Properties.ReadOnly = true;
            }
            else
            {
                //加载仓库
                List<Alading.Entity.StockHouse> houseList = StockHouseService.GetAllStockHouse();
                if (houseList != null && houseList.Count > 0)
                {
                    string tag = string.Empty;
                    foreach (Alading.Entity.StockHouse house in houseList)
                    {
                        if (string.IsNullOrEmpty(house.HouseName))
                            continue;
                        comboBoxHouse.Properties.Items.Add(house.HouseName);
                        tag += house.StockHouseCode + ",";
                    }
                    comboBoxHouse.Tag = tag.Trim(',');
                }
            }
        }

        //private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (checkEdit1.Checked)
        //    {
        //        textEditLayoutCode.Text = System.Guid.NewGuid().ToString();
        //        textEditLayoutCode.Properties.ReadOnly = true;
        //    }
        //    else
        //    {
        //        textEditLayoutCode.Text = string.Empty;
        //        textEditLayoutCode.Properties.ReadOnly = false;
        //    }
        //}
    }
}