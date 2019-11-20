using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Core.Enum;
using Alading.Entity;
using Alading.Business;
using System.Linq;
using DevExpress.Utils;
using Alading.Taobao;

namespace Alading.Forms.Stock.SettingUp
{
    public partial class ModifyUnitForm : DevExpress.XtraEditors.XtraForm
    {
        public ModifyUnitForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 计量单位编码
        /// </summary>
        string stockunitCode;

        public ModifyUnitForm(string stockUnitCode,string unitGroupName,string baseUnitName)
        {
            InitializeComponent();
            textUnitGroup.Text = unitGroupName;
            labelBaseUnit.Text = baseUnitName;
            this.stockunitCode = stockUnitCode;
            List<string> listStockUnitCode=new List<string>();
            List<Alading.Entity.StockUnit> stockUnitlist = StockUnitService.GetStockUnit(c => c.StockUnitCode == stockUnitCode);
            if (stockUnitlist != null && stockUnitlist.Count > 0)
            {
                Alading.Entity.StockUnit stockunit = stockUnitlist.First();
                textEditUnitName.Text = stockunit.StockUnitName;
                textEditNum.Text = stockunit.Conversion.ToString();
                memoExEditRemark.Text = stockunit.Remark;
                textEditCode.Text = stockunit.StockUnitCode;
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textEditUnitName.Text))
            {
                XtraMessageBox.Show("请输入计量单位名称！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textEditUnitName.Focus();
                return;
            }
            //验证比例为1-5位小数的正实数
            if (string.IsNullOrEmpty(textEditNum.Text) || textEditNum.Text == "0")
            {
                XtraMessageBox.Show("请输入正确的比例关系！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textEditNum.Focus();
                return;
            }

            WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
            waitForm.Show();
            Alading.Entity.StockUnit stockUnit = new Alading.Entity.StockUnit();
            stockUnit.StockUnitName = textEditUnitName.Text;
            stockUnit.Conversion = double.Parse(textEditNum.Text);
            stockUnit.Remark = memoExEditRemark.Text;
            if (StockUnitService.UpdateStockUnit(stockunitCode, stockUnit) == ReturnType.Success)
            {
                waitForm.Close();
                XtraMessageBox.Show("计量单位修改成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                waitForm.Close();
                XtraMessageBox.Show("计量单位修改失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

       
    }
}