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
    public partial class SelledReturnIn : DevExpress.XtraEditors.XtraForm
    {
        public SelledReturnIn()
        {
            InitializeComponent();
        }

        #region 点击控件触发事件
        private void SelledReturnIn_Load(object sender, EventArgs e)
        {
            try
            {
                selledReturnInCtrl.dateEditInTime.EditValueChanged += new EventHandler(EditValueChanged);
                selledReturnInCtrl.pceOperator.EditValueChanged += new EventHandler(EditValueChanged);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveBtn_Click(object sender, EventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                bool isSuccess=selledReturnInCtrl.SaveRefund();
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
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancleBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selledReturnInCtrl.dateEditInTime.Text) || string.IsNullOrEmpty(selledReturnInCtrl.pceOperator.Text))
            {
                saveBtn.Enabled = false;
            }
            else
            {
                saveBtn.Enabled = true;
            }
        }
        #endregion
    }
}