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
using DevExpress.Utils;
using Alading.Taobao;

namespace Alading.Forms.Stock.SettingUp
{
    public partial class ModifyGroupUnitForm : DevExpress.XtraEditors.XtraForm
    {
        public ModifyGroupUnitForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 修改前的计量单位组
        /// </summary>
        Alading.Entity.StockUnitGroup stockUnitGroup;

        public ModifyGroupUnitForm(Alading.Entity.StockUnitGroup stockunitGroup)
        {
            InitializeComponent();
            this.stockUnitGroup = stockunitGroup;
            textEditGroupName.Text = stockUnitGroup.StockUnitGroupName;
            textEditGroupUnit.Text = stockUnitGroup.BaseUnit;
            memoRemark.Text = stockUnitGroup.Remark;
            textEditUnitGroupCode.Text = stockunitGroup.StockUnitGroupCode;
        }

        /// <summary>
        /// 加载修改前原始信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifyGroupUnitForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonConfirm_Click(object sender, EventArgs e)
        {
            WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
            waitForm.Show();
            if (string.IsNullOrEmpty(textEditGroupName.Text) || string.IsNullOrEmpty(textEditGroupName.Text.Trim()))
            {
                waitForm.Close();
                XtraMessageBox.Show("请输入单位组名称！", Constants.SYSTEM_PROMPT);
                return;
            }
            stockUnitGroup.StockUnitGroupName = textEditGroupName.Text;
            stockUnitGroup.Remark = memoRemark.Text; 
            if (StockUnitGroupService.UpdateStockUnitGroup(stockUnitGroup) == ReturnType.Success)
            {
                waitForm.Close();
                XtraMessageBox.Show("更新成功！", "系统提示", MessageBoxButtons.OK);
            }
            else
            {
                waitForm.Close();
                XtraMessageBox.Show("更新失败！", "系统提示", MessageBoxButtons.OK);
            }
            this.Close();
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonBack_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }    
    }
}