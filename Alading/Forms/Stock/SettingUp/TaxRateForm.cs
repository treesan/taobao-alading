using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Entity;
using Alading.Taobao;
using Alading.Business;
using Alading.Core.Enum;

namespace Alading.Forms.Stock.SettingUp
{
    public partial class TaxRateForm : DevExpress.XtraEditors.XtraForm
    {
        public TaxRateForm()
        {
            InitializeComponent();
        }

        private void TaxRateForm_Load(object sender, EventArgs e)
        {

        }

        private Tax GetTax()
        {
            Tax tax = new Tax();
            if (textEditTaxCode.Text == string.Empty || textEditTaxName.Text == string.Empty
                || textEditInputText.Text == string.Empty || textEditOutputTax.Text == string.Empty)
            {
                XtraMessageBox.Show("带*号的为必填项",Constants.SYSTEM_PROMPT);
                return null;
            }
            
            tax.Formula = radioGroup1.SelectedIndex;
            tax.InputTax =double.Parse(textEditInputText.Text);
            tax.OutPutTax = double.Parse(textEditOutputTax.Text);
            tax.TaxCode = textEditTaxCode.Text;
            tax.TaxName = textEditTaxName.Text;
            tax.IsDefault = checkEditIsDefault.Checked;
            tax.TaxRemark = memoEditRemark.Text;
            return tax;
        }

        private void ComponentInit()
        {
            radioGroup1.SelectedIndex=1;
            textEditInputText.Text=string.Empty;
            textEditOutputTax.Text = string.Empty;
            textEditTaxCode.Text = string.Empty;
            textEditTaxName.Text = string.Empty;
            checkEditIsDefault.Checked=false;
            memoEditRemark.Text = string.Empty;
        }

        private void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            Tax tax = GetTax();
            if (tax == null)
                return;
            if (StockUnitService.AddTax(tax) == ReturnType.Success)
            {
                XtraMessageBox.Show("新建税率成功", Constants.SYSTEM_PROMPT);
            }
            else
            {
                XtraMessageBox.Show("新建税率失败", Constants.SYSTEM_PROMPT);
            }

            //清空组建值
            ComponentInit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Tax tax = GetTax();
            if (tax == null)
                return;
            if (StockUnitService.AddTax(tax) == ReturnType.Success)
            {
                XtraMessageBox.Show("新建税率成功", Constants.SYSTEM_PROMPT);
            }
            else
            {
                XtraMessageBox.Show("新建税率失败", Constants.SYSTEM_PROMPT);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}