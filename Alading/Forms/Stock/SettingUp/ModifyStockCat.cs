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
using DevExpress.XtraTreeList.Nodes;
using Alading.Taobao;
using DevExpress.Utils;

namespace Alading.Forms.Stock.SettingUp
{
    public partial class ModifyStockCat : DevExpress.XtraEditors.XtraForm
    {
        string stockCatCid = string.Empty;

        string stockCatName = string.Empty;

        public ModifyStockCat(string stockCatName,string stockCatCid,string fatherName,string fatherCode)
        {
            InitializeComponent();
            this.stockCatName = stockCatName;
            this.stockCatCid = stockCatCid;
            textCatName.Text = stockCatName;
            textEditCid.Text = stockCatCid;
            textEditFatherCode.Text = fatherCode;
            textEditFatherName.Text = fatherName;
        }

        private void simpleConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textCatName.Text))
            {
                XtraMessageBox.Show("请输入类目名称！", Constants.SYSTEM_PROMPT);
                textCatName.Focus();
                return;
            }
            WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
            waitForm.Show();
            try
            {
                stockCatName = textCatName.Text;
                if (StockCatService.UpdateStockCat(stockCatCid, stockCatName) == ReturnType.Success)
                {
                    waitForm.Close();
                    XtraMessageBox.Show("修改类目成功！", Constants.SYSTEM_PROMPT);
                    this.Close();
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    waitForm.Close();
                    XtraMessageBox.Show("修改类目失败！", Constants.SYSTEM_PROMPT);
                }
            }
            catch (Exception ex)
            {
                waitForm.Close();
                XtraMessageBox.Show(ex.ToString(), Constants.SYSTEM_PROMPT);
            }
        }

        private void simpleCancel_Click(object sender, EventArgs e)
        {            
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }
    }
}