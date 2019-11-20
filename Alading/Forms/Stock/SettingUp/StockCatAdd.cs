using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Taobao;
using DevExpress.Utils;
using Alading.Business;
using Alading.Core.Enum;

namespace Alading.Forms.Stock.SettingUp
{
    public partial class StockCatAdd : DevExpress.XtraEditors.XtraForm
    {
        public StockCatAdd(string fatherName,string fatherCode)
        {
            InitializeComponent();
            textEditFatherCode.Text = fatherCode;
            textEditFatherName.Text = fatherName;
        }

        private void simpleConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textEditCid.Text))
            {
                XtraMessageBox.Show("请输入类目编码！", Constants.SYSTEM_PROMPT);
                textEditCid.Focus();
                return;
            }
            if (string.IsNullOrEmpty(textCatName.Text))
            {
                XtraMessageBox.Show("请输入类目名称！", Constants.SYSTEM_PROMPT);
                textCatName.Focus();
                return;
            }

            WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
            waitForm.Show();
            string cid = textEditCid.Text;
            List<Alading.Entity.StockCat> stockCatList=StockCatService.GetStockCat(c=>c.StockCid==cid);
            if (stockCatList != null && stockCatList.Count > 0)
            {
                waitForm.Close();
                XtraMessageBox.Show("类目编码与数据库中已有类目编码重复，请重输！", Constants.SYSTEM_PROMPT);
                return;
            }
            else
            {
                try
                {
                    Alading.Entity.StockCat stockCat = new Alading.Entity.StockCat();
                    stockCat.IsParent = false;
                    stockCat.ParentCid = textEditFatherCode.Text;
                    stockCat.StockCatName = textCatName.Text;
                    stockCat.StockCid = textEditCid.Text;
                    if (StockCatService.AddStockCat(stockCat) == ReturnType.Success)
                    {
                        waitForm.Close();
                        XtraMessageBox.Show("添加类目成功！", Constants.SYSTEM_PROMPT);
                        this.Close();
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        waitForm.Close();
                        XtraMessageBox.Show("添加类目失败！", Constants.SYSTEM_PROMPT);
                    }
                }
                catch (Exception ex)
                {
                    waitForm.Close();
                    XtraMessageBox.Show(ex.ToString(), Constants.SYSTEM_PROMPT);
                }
            }
        }

        private void simpleCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }
    }
}