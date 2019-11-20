using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Business;
using Alading.Entity;
using Alading.Taobao;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.Utils;

namespace Alading.Forms.StaffManager
{
    public partial class PermissionManager : DevExpress.XtraEditors.XtraForm
    {
        public PermissionManager()
        {
            InitializeComponent();
        }

        //全局变量
        int RoleType = 0;//角色类型
        string RoleCode = string.Empty;//角色编码

        #region 事件
        /// <summary>
        /// 加载界面时触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PermissionManager_Load(object sender, EventArgs e)
        {
            try
            {
                //加载所有角色 展示在NavBarGroup中
                LoadAllRoles();
                treeListPermission.ClearNodes();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 加载所有权限列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void navBarItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                DevExpress.XtraNavBar.NavBarItem item = (DevExpress.XtraNavBar.NavBarItem)sender;
                if (item != null)
                {
                    // 保存当前角色类型
                    LoadPermission(item.Tag.ToString());
                }
                waitFrm.Close();
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 新建角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnAddRole_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Alading.Forms.Staff.AddRole addRole = new Alading.Forms.Staff.AddRole();
                addRole.ShowDialog();
                //加载所有角色 展示在NavBarGroup中
                LoadAllRoles();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnDeleteRole_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(RoleCode))
                {
                    XtraMessageBox.Show("请先选中角色", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (XtraMessageBox.Show("是否确定删除", Constants.SYSTEM_PROMPT, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    == DialogResult.Yes)
                {
                    //判断是否是内置数据
                    Role role = RoleService.GetRole(RoleCode);
                    List<RolePermission> rolePerList = RolePermissionService.GetRolePermission(c => c.RoleCode == role.RoleCode);
                    //内置数据
                    if (role.RoleTag)
                    {
                        XtraMessageBox.Show("内置数据不可删除", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else if (rolePerList != null || rolePerList.Count != 0)
                    {
                        XtraMessageBox.Show("此角色还存在权限，不可删除", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        RoleService.RemoveRole(RoleCode);
                        //加载所有角色 展示在NavBarGroup中
                        LoadAllRoles();
                        XtraMessageBox.Show("删除成功", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            try
            {
                SaveRolePermission();
                waitFrm.Close();
                XtraMessageBox.Show("保存成功", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                waitFrm.Close();
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 勾选或取消勾选节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListPermission_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            try
            {
                if (e.Node.Checked)
                {
                    setChildNodeCheckedState(e.Node, true);
                    if (e.Node.ParentNode != null)
                    {
                        setParentNodeCheckedState(e.Node.ParentNode, true);
                    }
                }
                else
                {
                    setChildNodeCheckedState(e.Node, false);
                    if (e.Node.ParentNode != null)
                    {
                        setParentNodeCheckedState(e.Node.ParentNode, false);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBtnReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNodes nodes = treeListPermission.Nodes;
            foreach (TreeListNode node in nodes)
            {
                setChildNodeCheckedState(node, false);
                node.Checked = false;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 加载角色
        /// </summary>
        public void LoadAllRoles()
        {
            navBarCtrlRoles.Items.Clear();
            List<Role> roleList = RoleService.GetAllRole();

            foreach (Role role in roleList)
            {
                DevExpress.XtraNavBar.NavBarItem navBarItem = new DevExpress.XtraNavBar.NavBarItem();
                navBarItem.Caption = role.RoleName;
                navBarItem.Tag = role.RoleCode;
                navBarItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItem_LinkClicked);
                navBarGroupRoles.ItemLinks.Add(navBarItem);
            }
        }

        /// <summary>
        /// 加载权限
        /// </summary>
        /// <param name="roleType"></param>
        public void LoadPermission(string roleCode)
        {
            treeListPermission.ClearNodes();
            RoleCode = roleCode;

            SortedList<string, List<Permission>> perList = new SortedList<string, List<Permission>>();
            //所有权限
            perList = RolePermissionService.GetPermissionList(roleCode, out RoleType);
            //获取有勾选的权限
            List<string> roleProList = RolePermissionService.GetSelectPermissionList(roleCode);
            

            //加载第一级
            foreach (Permission firstPer in perList[string.Empty])
            {
                TreeListNode firstTreeList = treeListPermission.AppendNode(new object[] { firstPer.Name, firstPer.PermissionRemark }, null, firstPer.PermissionCode);

                //加载第二级
                foreach (Permission secoundPer in perList[firstPer.PermissionCode])
                {
                    TreeListNode secoundTreeList = treeListPermission.AppendNode(new object[] { secoundPer.Name, secoundPer.PermissionRemark }, firstTreeList, secoundPer.PermissionCode);

                    //加载第三级
                    foreach (Permission thirdPer in perList[secoundPer.PermissionCode])
                    {
                        TreeListNode thirdTreeList = treeListPermission.AppendNode(new object[] { thirdPer.Name, thirdPer.PermissionRemark }, secoundTreeList, thirdPer.PermissionCode);
                        if (roleProList.Exists(c => c.Equals(thirdPer.PermissionCode)))
                        {
                            thirdTreeList.Checked = true;
                        }
                    }

                    if (roleProList.Exists(c => c.Equals(secoundPer.PermissionCode)))
                    {
                        secoundTreeList.Checked = true;
                    }
                }

                if (roleProList.Exists(c => c.Equals(firstPer.PermissionCode)))
                {
                    firstTreeList.Checked = true;
                }
            }
        }

        /// <summary>
        /// 设置子节点
        /// </summary>
        /// <param name="currNode"></param>
        /// <param name="state"></param>
        private static void setChildNodeCheckedState(TreeListNode currNode, bool state)
        {
            TreeListNodes nodes = currNode.Nodes;
            if (nodes.Count > 0)
            {
                foreach (TreeListNode node in nodes)
                {
                    node.Checked = state;
                    setChildNodeCheckedState(node, state);
                }
            }
        }

        /// <summary>
        /// 设置父节点
        /// </summary>
        /// <param name="fnode"></param>
        /// <param name="state"></param>
        private static void setParentNodeCheckedState(TreeListNode fnode, bool state)
        {
            if (state == true)
            {
                fnode.Checked = true;
                if (fnode.ParentNode != null)
                {
                    setParentNodeCheckedState(fnode.ParentNode, true);
                }
            }
            else
            {
                fnode.Checked = false;
                foreach (TreeListNode node in fnode.Nodes)
                {
                    if (node.Checked == true)
                    {
                        fnode.Checked = true;
                        break;
                    }
                }
                if (fnode.ParentNode != null)
                {
                    setParentNodeCheckedState(fnode.ParentNode, fnode.Checked);
                }
            }
        }

        /// <summary>
        /// 保存权限
        /// </summary>
        private void SaveRolePermission()
        {
            List<RolePermission> permissionList = new List<RolePermission>();
            foreach (TreeListNode FNode in treeListPermission.Nodes)
            {
                //加第一级
                if (FNode.Checked == true)
                {
                    AddRolePermission(permissionList, FNode);
                    foreach (TreeListNode SNode in FNode.Nodes)
                    {
                        //加第二级
                        if (SNode.Checked == true)
                        {
                            AddRolePermission(permissionList, SNode);
                            foreach (TreeListNode TNode in SNode.Nodes)
                            {
                                //加第三级
                                if (TNode.Checked == true)
                                {
                                    AddRolePermission(permissionList, TNode);
                                }
                            }
                        }
                    }
                }
            }
            //删除原有的，添加新勾选的
            RolePermissionService.RemoveAndAddRolePermission(RoleCode, permissionList);
        }

        /// <summary>
        /// 加RolePermission
        /// </summary>
        /// <param name="permissionList"></param>
        /// <param name="node"></param>
        public void AddRolePermission(List<RolePermission> permissionList, TreeListNode node)
        {
            RolePermission rolePer = new RolePermission();
            rolePer.RoleCode = RoleCode;
            rolePer.PermissionCode = node.Tag.ToString();
            rolePer.RolePermissionCode = Guid.NewGuid().ToString();
            permissionList.Add(rolePer);
        }
        #endregion
    }
}