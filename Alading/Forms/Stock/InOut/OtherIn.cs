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
    public partial class OtherIn : DevExpress.XtraEditors.XtraForm
    {
        public OtherIn()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 点击保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                bool isSuccess = otherInCtrl.SaveData();
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
                bool isSuccess = otherInCtrl.SaveData();
                waitFrm.Close();
                if (isSuccess)
                {
                    XtraMessageBox.Show("保存成功", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    otherInCtrl.Clear();
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

        #region 确定是否可保存
        private void OtherIn_Load(object sender, EventArgs e)
        {
            try
            {
                otherInCtrl.dateEditInTime.EditValueChanged += new EventHandler(EditValueChanged);
                otherInCtrl.pceOperator.EditValueChanged += new EventHandler(EditValueChanged);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(otherInCtrl.dateEditInTime.Text) || string.IsNullOrEmpty(otherInCtrl.pceOperator.Text))
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