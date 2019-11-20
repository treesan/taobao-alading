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
using DevExpress.Utils;

namespace Alading.Forms.Staff
{
    public partial class AddRole : DevExpress.XtraEditors.XtraForm
    {
        public AddRole()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ensureBtn_Click(object sender, EventArgs e)
        {
            WaitDialogForm waitFrm = null;
            try
            {
                if (string.IsNullOrEmpty(tERoleName.Text))
                {
                    XtraMessageBox.Show("角色名称不可为空", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tERoleName.Focus();
                    return;
                }
                else
                {
                    waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                    //已有角色数
                    List<Role> roleList = RoleService.GetAllRole();
                    foreach (Role role in roleList)
                    {
                        if (role.RoleName == tERoleName.Text.Trim())
                        {
                            waitFrm.Close();
                            XtraMessageBox.Show("已存在此角色", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tERoleName.Focus();
                            return;
                        }
                    }

                    Role oldRole = roleList.Find(c => c.RoleName == comboBoxEditRoles.Text);
                    if (oldRole != null)
                    {
                        int roleCount = roleList.Count;
                        Role newRole = new Role();
                        newRole.RoleCode = Guid.NewGuid().ToString();
                        newRole.RoleName = tERoleName.Text;
                        newRole.RoleType = oldRole.RoleType;
                        newRole.RoleTag = false;
                        RoleService.AddRole(newRole);
                        waitFrm.Close();
                        XtraMessageBox.Show("角色添加成功", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        waitFrm.Close();
                        XtraMessageBox.Show("角色添加失败", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tERoleName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 确定保存按钮是否可点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tERoleName_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(comboBoxEditRoles.Text) || string.IsNullOrEmpty(tERoleName.Text))
                {
                    ensureBtn.Enabled = false;
                }
                else
                {
                    ensureBtn.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 确定保存按钮是否可点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxEditRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxEditRoles.Text) || string.IsNullOrEmpty(tERoleName.Text))
            {
                ensureBtn.Enabled = false;
            }
            else
            {
                ensureBtn.Enabled = true;
            }
        }
    }
}