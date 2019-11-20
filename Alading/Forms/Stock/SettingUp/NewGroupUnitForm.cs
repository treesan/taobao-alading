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

namespace Alading.Forms.Stock.SettingUp
{
    public partial class NewGroupUnitForm : DevExpress.XtraEditors.XtraForm
    {
        public NewGroupUnitForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonConfirm_Click(object sender, EventArgs e)
        {
            Alading.Entity.StockUnitGroup stockUnitGroup = new Alading.Entity.StockUnitGroup();
            Alading.Entity.StockUnit stockUnit = new Alading.Entity.StockUnit();
            if (!string.IsNullOrEmpty(textEditGroupName.Text) && !string.IsNullOrEmpty(textEditGroupUnit.Text))
            {
                //添加计量单位组
                stockUnitGroup.StockUnitGroupName = textEditGroupName.Text;
                stockUnitGroup.BaseUnit = textEditGroupUnit.Text;
                stockUnitGroup.StockUnitGroupCode = System.Guid.NewGuid().ToString();
                //添加基本计量单位
                stockUnit.StockUnitCode = System.Guid.NewGuid().ToString();
                stockUnit.StockUnitName = textEditGroupUnit.Text;
                stockUnit.StockUnitSource = "手动新增";
                stockUnit.IsBaseUnit = true;
                stockUnit.StockUnitGroupCode = stockUnitGroup.StockUnitGroupCode;
                
                //if (StockUnitGroupService.AddStockUnitGroup(stockUnitGroup) == ReturnType.Success &&
                //    StockUnitService.AddStockUnit(stockUnit) == ReturnType.Success)
                //{
                //    XtraMessageBox.Show("计量单位组添加成功！", "系统提示", MessageBoxButtons.OK);
                //    this.Close();
                //}
                //else
                //{
                //    XtraMessageBox.Show("计量单位组添加失败！", "系统提示", MessageBoxButtons.OK);
                //}
            }
            else
            {
                XtraMessageBox.Show("请将信息填写完整！", "系统提示", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}