using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Business;
using Alading.Core.Enum;
using Alading.Taobao;

namespace Alading.Forms.Stock.SettingUp
{
    public partial class ModifyHouse : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 表明是修改仓库的标志
        /// </summary>
        bool modifyFlag = true;

        /// <summary>
        /// 待修改的StockHouse
        /// </summary>
        Alading.Entity.StockHouse stockHouse;

        public ModifyHouse()
        {
            InitializeComponent();
            stockHouse = new Alading.Entity.StockHouse();
            this.Text = "新增仓库";
            modifyFlag = false;//表明是新增仓库
        }
        public ModifyHouse(Alading.Entity.StockHouse stockHouse)
        {
            InitializeComponent();
            textHouseName.Text = stockHouse.HouseName;
            textHouseAddr.Text = stockHouse.HouseAddress;
            textHouseCode.Text = stockHouse.StockHouseCode;
            textEditHouseContact.Text = stockHouse.HouseContact;
            textEditHouseTel.Text = stockHouse.HouseTel;
            textHouseCode.Properties.ReadOnly = true;
            memoHouseRemark.Text = stockHouse.HouseRemark;
            this.stockHouse = stockHouse;
        }
        
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleConfirm_Click(object sender, EventArgs e)
        {
            if (textHouseName.Text == string.Empty)
            {
                XtraMessageBox.Show("请输入仓库名称", Constants.SYSTEM_PROMPT);
                return;
            }
            
            if (modifyFlag == true)
            {
                /*新建一个实例，防止这边更新失败，但是还是修改了原来传进来的那个实例*/
                Alading.Entity.StockHouse newStockHouse = new Alading.Entity.StockHouse();
                newStockHouse.HouseAddress = textHouseAddr.Text;
                newStockHouse.HouseName = textHouseName.Text;
                newStockHouse.HouseRemark = memoHouseRemark.Text;
                newStockHouse.StockHouseCode = stockHouse.StockHouseCode;
                newStockHouse.HouseTel = textEditHouseTel.Text;
                newStockHouse.HouseContact = textEditHouseContact.Text;
                if (StockHouseService.UpdateStockHouse(newStockHouse) == ReturnType.Success)
                {
                    XtraMessageBox.Show("修改仓库信息成功", Constants.SYSTEM_PROMPT);
                    /*修改成功同时修改传进来的实例*/
                    stockHouse.HouseAddress = textHouseAddr.Text;
                    stockHouse.HouseName = textHouseName.Text;
                    stockHouse.HouseRemark = memoHouseRemark.Text;
                    stockHouse.HouseTel = textEditHouseTel.Text;
                    stockHouse.HouseContact = textEditHouseContact.Text;
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show("修改仓库信息失败", Constants.SYSTEM_PROMPT);
                }
            }
            else
            {
                stockHouse.HouseAddress = textHouseAddr.Text;
                stockHouse.HouseName = textHouseName.Text;
                stockHouse.HouseRemark = memoHouseRemark.Text;
                stockHouse.HouseTel = textEditHouseTel.Text;
                stockHouse.HouseContact = textEditHouseContact.Text;
                if(!string.IsNullOrEmpty(textHouseCode.Text))
                {
                    stockHouse.StockHouseCode = textHouseCode.Text;
                    List<Alading.Entity.StockHouse> houseList=StockHouseService.GetStockHouse(c => c.StockHouseCode == stockHouse.StockHouseCode);
                    if (houseList != null && houseList.Count > 0)
                    {
                        XtraMessageBox.Show("仓库编码重复，请重输！", Constants.SYSTEM_PROMPT);
                        return;
                    }
                }
                else
                {
                    //XtraMessageBox.Show("请输入仓库编码！", Constants.SYSTEM_PROMPT);
                    //return;
                    stockHouse.StockHouseCode = Guid.NewGuid().ToString();
                }

                if (StockHouseService.AddStockHouse(stockHouse) == ReturnType.Success)
                {
                    XtraMessageBox.Show("新增仓库成功", Constants.SYSTEM_PROMPT);
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show("新增仓库失败", Constants.SYSTEM_PROMPT);
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
    }
}