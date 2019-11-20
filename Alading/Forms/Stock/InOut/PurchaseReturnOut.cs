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
    public partial class PurchaseReturnOut : DevExpress.XtraEditors.XtraForm
    {
        public PurchaseReturnOut()
        {
            InitializeComponent();
        }

        #region 全局变量
        Alading.Forms.Stock.Control.PurchaseReturnOut purchaseReturnOut = new Alading.Forms.Stock.Control.PurchaseReturnOut();
        #endregion

        private void PurchaseReturnOut_Load(object sender, EventArgs e)
        {
            try
            {
                purchaseReturnOut.Parent = panelPurchaseOutCtrl;
                purchaseReturnOut.Dock = DockStyle.Fill;

                purchaseReturnOut.dateEditOutTime.EditValueChanged += new EventHandler(EditValueChanged);
                purchaseReturnOut.pceOperator.EditValueChanged += new EventHandler(EditValueChanged);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(purchaseReturnOut.dateEditOutTime.Text) || string.IsNullOrEmpty(purchaseReturnOut.pceOperator.Text))
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

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                bool isSuccess = purchaseReturnOut.SaveData();
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

        /// <summary>
        /// 保存并新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAndCreateBtn_Click(object sender, EventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                bool isSuccess = purchaseReturnOut.SaveData();
                waitFrm.Close();
                if (isSuccess)
                {
                    XtraMessageBox.Show("保存成功", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    purchaseReturnOut.Clear();
                }
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}