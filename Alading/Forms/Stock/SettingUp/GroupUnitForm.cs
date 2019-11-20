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
using Alading.Taobao;
using Alading.Core.Enum;
using DevExpress.Utils;

namespace Alading.Forms.Stock.SettingUp
{
    public partial class GroupUnitForm : DevExpress.XtraEditors.XtraForm
    {
        public GroupUnitForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            WaitDialogForm waitForm = new WaitDialogForm(Constants.OPERATE_DB_DATA);
            waitForm.Show();
            try
            {
                #region 验证
                if (string.IsNullOrEmpty(textEditGroupName.Text) || string.IsNullOrEmpty(textEditGroupName.Text.Trim()))
                {
                    waitForm.Close();
                    XtraMessageBox.Show("请输入单位组名称！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textEditGroupName.Focus();                   
                    return;
                }
                if (string.IsNullOrEmpty(textEditBaseUnitName.Text) || string.IsNullOrEmpty(textEditBaseUnitName.Text.Trim()))
                {
                    waitForm.Close();
                    XtraMessageBox.Show("请输入基本单位名称！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textEditBaseUnitName.Focus();
                    return;
                }
                
                #endregion

                StockUnitGroup sug = new StockUnitGroup();
                Alading.Entity.StockUnit unit = new Alading.Entity.StockUnit();
                /*基本单位编码*/
                if (string.IsNullOrEmpty(textEditBaseUnitCode.Text) || string.IsNullOrEmpty(textEditBaseUnitCode.Text.Trim()))
                {
                    unit.StockUnitCode = System.Guid.NewGuid().ToString();
                }
                else
                {
                    unit.StockUnitCode = textEditBaseUnitCode.Text;
                }

                /*单位组编码*/
                if (string.IsNullOrEmpty(textEditGroupCode.Text) || string.IsNullOrEmpty(textEditGroupCode.Text.Trim()))
                {
                    sug.StockUnitGroupCode = System.Guid.NewGuid().ToString();
                }
                else
                {
                    sug.StockUnitGroupCode = textEditGroupCode.Text;
                }

                if (!StockUnitGroupService.IsCodeOnly(unit.StockUnitCode, sug.StockUnitGroupCode))
                {
                    XtraMessageBox.Show("单位或单位组编码输入与数据库中重复，请重输！", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    waitForm.Close();
                    return;
                }

                unit.Conversion = 1;
                unit.IsBaseUnit = true;
                unit.Remark = memoExEdit1.Text;
                unit.StockUnitGroupCode = sug.StockUnitGroupCode;
                unit.StockUnitName = textEditBaseUnitName.Text;
                unit.StockUnitSource = "手动添加";

                sug.StockUnitGroupName = textEditGroupName.Text;
                sug.BaseUnit = unit.StockUnitName;//
                sug.Remark = memoRemark.Text;
                waitForm.Close();
                if (StockUnitGroupService.AddStockUnitGroup(sug, unit) == ReturnType.Success)
                {
                    XtraMessageBox.Show("添加单位组成功！", Constants.SYSTEM_PROMPT);
                }
                else
                {
                    XtraMessageBox.Show("添加单位组失败！", Constants.SYSTEM_PROMPT);
                }
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                waitForm.Close();
                XtraMessageBox.Show(ex.ToString(), Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}