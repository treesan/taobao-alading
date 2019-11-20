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
    public partial class ProduceInForm : DevExpress.XtraEditors.XtraForm
    {
        public ProduceInForm()
        {
            InitializeComponent();
        }

        #region 点击控件触发事件
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
                bool isSuccess=produceInCtrl.SaveData();
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
        /// 保持并新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAndCreateBtn_Click(object sender, EventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                bool isSuccess=produceInCtrl.SaveData();
                waitFrm.Close();
                if (isSuccess)
                {
                    XtraMessageBox.Show("保存成功", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    produceInCtrl.Clear();
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
        #endregion

        #region 确定是否可保存
        private void ProduceInForm_Load(object sender, EventArgs e)
        {
            try
            {
                produceInCtrl.dateEditInTime.EditValueChanged += new EventHandler(EditValueChanged);
                produceInCtrl.pceOperator.EditValueChanged += new EventHandler(EditValueChanged);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(produceInCtrl.dateEditInTime.Text) || string.IsNullOrEmpty(produceInCtrl.pceOperator.Text))
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