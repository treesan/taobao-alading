using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using Alading.Taobao;

namespace Alading.Forms.Stock.InOut
{
    public partial class ProductSoldOut : DevExpress.XtraEditors.XtraForm
    {
        public ProductSoldOut()
        {
            InitializeComponent();
        }

        #region 全局变量
        Alading.Forms.Stock.Control.ProductSoldOut proSoldOut = new Alading.Forms.Stock.Control.ProductSoldOut();
        #endregion

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                bool isSuccess = proSoldOut.SaveData();
                waitFrm.Close();
                if (isSuccess)
                {
                    XtraMessageBox.Show("保存成功", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SaveAndCreateBtn_Click(object sender, EventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                bool isSuccess = proSoldOut.SaveData();
                waitFrm.Close();
                if (isSuccess)
                {
                    XtraMessageBox.Show("保存成功", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    proSoldOut.Clear();
                }
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 是否保存
        private void ProductSoldOut_Load(object sender, EventArgs e)
        {
            try
            {
                proSoldOut.Parent = panelSoldOutCtrl;
                proSoldOut.Dock = DockStyle.Fill;

                proSoldOut.dateEditOutTime.EditValueChanged += new EventHandler(EditValueChanged);
                proSoldOut.pceOperator.EditValueChanged += new EventHandler(EditValueChanged);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(proSoldOut.dateEditOutTime.Text) || string.IsNullOrEmpty(proSoldOut.pceOperator.Text))
            {
                SaveBtn.Enabled = false;
                SaveAndCreateBtn.Enabled = false;
            }
            else
            {
                SaveBtn.Enabled = true;
                SaveAndCreateBtn.Enabled = true;
            }
        }
        #endregion

    }
}