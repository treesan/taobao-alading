using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Entity;
using DevExpress.XtraNavBar;
using Alading.Core.Enum;
using DevExpress.XtraTreeList.Nodes;
using Alading.Forms.Staff;
using DevExpress.XtraBars;
using DevExpress.Utils;
using Alading.Taobao;
using System.Text.RegularExpressions;

namespace Alading.Forms.StaffManager
{
    public partial class StaffManager : DevExpress.XtraEditors.XtraForm
    {
        private List<User> userList = new List<User>();
        private List<UserRole> userRole = new List<UserRole>();
        private List<Role> role = new List<Role>();
        private List<UserShop> userShop = new List<UserShop>();
        private List<UserStockHouse> userStockHouse = new List<UserStockHouse>();
        private List<Alading.Entity.Shop> shop = new List<Alading.Entity.Shop>();
        private List<StockHouse> stockHouse = new List<StockHouse>();
        private User selectedUser = null;
        private string seletedNavBar = "nBAllUser";

        public StaffManager()
        {
            InitializeComponent();            
            InitRoleTree();
        }

        /// <summary>
        /// 展示角色树
        /// </summary>
        private void InitRoleTree()
        {
            TreeListNode node = null;
            role = Alading.Business.RoleService.GetAllRole();
            shop = Alading.Business.ShopService.GetAllShop();
            stockHouse = Alading.Business.StockHouseService.GetAllStockHouse();
            foreach (Role r in role)
            {
                node = tLRole.AppendNode(new object[] { r.RoleName, r.RoleTag }, null, r.RoleCode);
                if (r.RoleName == "客服")
                {
                    foreach (var i in shop)
                    {
                        tLRole.AppendNode(new object[] { i.nick }, node, i.sid);
                    }
                }
                if (r.RoleName == "仓管员")
                {
                    foreach (var i in stockHouse)
                    {
                        tLRole.AppendNode(new object[] { i.HouseName}, node, i.StockHouseCode);
                    }
                }
            }
            tLRole.ExpandAll();
        }
        
        /// <summary>
        /// 装载所有员工
        /// </summary>
        private void LoadAllUser()
        {
            userList = Alading.Business.UserService.GetAllUser();
            gcUserList.DataSource = userList;
        }

        /// <summary>
        /// 绑定用户角色
        /// </summary>
        private void LoadUserRole()
        {
            if (selectedUser != null)
            {                
                FillRoleTree();
            }
        }

        /// <summary>
        /// 填充角色树
        /// </summary>
        private void FillRoleTree()
        {
            ClearRoleTree();
            if (selectedUser != null)
            {
                userRole = Alading.Business.UserRoleService.GetUserRole(p => p.UserCode == selectedUser.UserCode);
                userShop = Alading.Business.UserShopService.GetUserShop(p => p.UserCode == selectedUser.UserCode);
                userStockHouse = Alading.Business.UserStockHouseService.GetUserStockHouse(p => p.UserCode == selectedUser.UserCode);
                foreach (TreeListNode node in tLRole.Nodes) //选商店
                {
                    if (node.GetDisplayText(0) == "客服" && node.Nodes.Count > 0)
                    {
                        foreach (TreeListNode x in node.Nodes)
                        {
                            foreach (UserShop s in userShop)
                            {
                                if (x.Tag.ToString() == s.ShopId)
                                {
                                    x.Checked = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (node.GetDisplayText(0) == "仓管员" && node.Nodes.Count > 0)  //选仓库
                    {
                        foreach (TreeListNode x in node.Nodes)
                        {
                            foreach (UserStockHouse s in userStockHouse)
                            {
                                if (x.Tag.ToString() == s.StockHouseCode)
                                {
                                    x.Checked = true;
                                    break;
                                }
                            }
                        }
                    }
                    foreach (UserRole r in userRole)  //选角色
                    {
                        if (node.Tag.ToString() == r.RoleCode)
                        {
                            node.Checked = true;
                            break;
                        }
                    }
                }

            }
        }

        /// <summary>
        /// 清理角色树，让所有的角色均未被选中
        /// </summary>
        /// <param name="node"></param>
        private void ClearRoleTree()
        {
            foreach (TreeListNode node in tLRole.Nodes)
            {
                if (node.Nodes.Count > 0)
                {
                    foreach (TreeListNode x in node.Nodes)
                    {
                        x.Checked = false;
                    }
                }
                node.Checked = false;
            }
        }        

        /// <summary>
        /// 新建员工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAddUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NewStaff newStaff = new NewStaff();
            if (DialogResult.OK == newStaff.ShowDialog())
            {
                RefreshInfo();
            }
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StaffManager_Load(object sender, EventArgs e)
        {
            LoadAllUser();
            groupControl2.Text = "所有员工信息列表";
        }

        /// <summary>
        /// 显示员工信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvStaff_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int[] rows = gvStaff.GetSelectedRows();
            if (rows.Length > 0)
            {
                selectedUser = userList[e.FocusedRowHandle];
                FillUserInfo(selectedUser);
                LoadUserRole();
            }
        }

        /// <summary>
        /// 填充员工基本信息与账户信息
        /// </summary>
        /// <param name="user"></param>
        private void FillUserInfo(User user)
        {
            txName.Text = user.nick;
            txDept.Text = user.department;
            txMail.Text = user.email;
            if (user.birthday != null)
            {
                dateEdit.Text = user.birthday.ToString();
            }
            txPhone.Text = user.tel;
            txAddr.Text = user.addr;
            memo_Remark.Text = user.Remark;

            if (user.sex == "男")
            {
                rgSex.SelectedIndex = 0;
            }
            else
            {
                rgSex.SelectedIndex = 1;
            }
            cbStatus.SelectedIndex = Convert.ToInt32(user.status);
            txAccount.Text = user.account;
            txBankAddr.Text = user.bankAddr;
        }

        /// <summary>
        /// 清除界面信息
        /// </summary>
        private void ClearInfoField()
        {
            txAccount.Text = string.Empty;
            txAddr.Text = string.Empty;
            txBankAddr.Text = string.Empty;
            txDept.Text = string.Empty;
            txMail.Text = string.Empty;
            txName.Text = string.Empty;
            txPhone.Text = string.Empty;
            dateEdit.Text = string.Empty;
            memo_Remark.Text = string.Empty;
            rgSex.SelectedIndex = 0;
            cbStatus.SelectedIndex = -1;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (selectedUser != null)
            {                
                if (userRole.Count > 0)
                {
                    XtraMessageBox.Show("该用户还存在角色，不能删除！若要删除用户，请先去除用户的角色！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (DialogResult.OK == XtraMessageBox.Show(string.Format("确定要删除用户{0}？", selectedUser.nick), "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    Alading.Business.UserService.RemoveUser(selectedUser.UserCode);
                    ClearInfoField();
                    LoadAllUser();
                }
            }
            else if (selectedUser == null)
            {
                XtraMessageBox.Show("没有选中用户！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 导航栏事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>j
        private void navBar_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            NavBarItem nbItem = (NavBarItem)sender;
            gcUserList.DataSource = null;
            seletedNavBar = nbItem.Name;
            if (seletedNavBar == "nBAllUser")
            {
                groupControl2.Text = "所有员工信息列表";
                userList = Alading.Business.UserService.GetAllUser();
            }
            else if (seletedNavBar == "nBNormalUser")
            {
                groupControl2.Text = "在职员工信息列表";
                int s = (int)Alading.Core.Enum.UserStatus.Normal;
                string status = s.ToString();
                userList = Alading.Business.UserService.GetUser(c => c.status == status);
            }
            else if (seletedNavBar == "nBFireUser")
            {
                groupControl2.Text = "离职员工信息列表";
                int s = (int)Alading.Core.Enum.UserStatus.Fire;
                string status = s.ToString();
                userList = Alading.Business.UserService.GetUser(c => c.status == status);
            }
            else if (seletedNavBar == "nBInactive")
            {
                groupControl2.Text = "未激活员工信息列表";
                int s = (int)Alading.Core.Enum.UserStatus.Inactive;
                string status = s.ToString();
                userList = Alading.Business.UserService.GetUser(c => c.status == status);
            }
            else if (seletedNavBar == "nBReeze")
            {
                groupControl2.Text = "冻结的员工信息列表";
                int s = (int)Alading.Core.Enum.UserStatus.Reeze;
                string status = s.ToString();
                userList = Alading.Business.UserService.GetUser(c => c.status == status);                
            }

            gcUserList.DataSource = userList;

            if (userList.Count > 0)
            {
                selectedUser = userList[0];
                FillUserInfo(selectedUser);
                FillRoleTree();
            }
            else
            {
                selectedUser = null;
                ClearInfoField();
                ClearRoleTree();
            }

            waitFrm.Close();
        }

        /// <summary>
        /// 刷新员工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshInfo();
            ClearRoleTree();
        }

        private void RefreshInfo()
        {
            WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
            gcUserList.DataSource = null;

            if (seletedNavBar == "nBAllUser")
            {
                groupControl2.Text = "所有员工信息列表";
                userList = Alading.Business.UserService.GetAllUser();
            }
            else if (seletedNavBar == "nBNormalUser")
            {
                groupControl2.Text = "在职员工信息列表";
                int s = (int)Alading.Core.Enum.UserStatus.Normal;
                string status = s.ToString();
                userList = Alading.Business.UserService.GetUser(c => c.status == status);
            }
            else if (seletedNavBar == "nBFireUser")
            {
                groupControl2.Text = "离职员工信息列表";
                int s = (int)Alading.Core.Enum.UserStatus.Fire;
                string status = s.ToString();
                userList = Alading.Business.UserService.GetUser(c => c.status == status);
            }
            else if (seletedNavBar == "nBInactive")
            {
                groupControl2.Text = "未激活员工信息列表";
                int s = (int)Alading.Core.Enum.UserStatus.Inactive;
                string status = s.ToString();
                userList = Alading.Business.UserService.GetUser(c => c.status == status);
            }
            else if (seletedNavBar == "nBReeze")
            {
                groupControl2.Text = "冻结的员工信息列表";
                int s = (int)Alading.Core.Enum.UserStatus.Reeze;
                string status = s.ToString();
                userList = Alading.Business.UserService.GetUser(c => c.status == status);                
            }

            gcUserList.DataSource = userList;

            if (userList.Count > 0)
            {
                selectedUser = userList[0];
                FillUserInfo(selectedUser);
                FillRoleTree();
            }
            else
            {
                selectedUser = null;
                ClearRoleTree();
                ClearInfoField();
            }
            waitFrm.Close();
        }

        /// <summary>
        /// 保存员工基本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            updateUserInfo();
            RefreshInfo();
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        private void updateUserInfo()
        {            
            selectedUser.account = txAccount.Text;
            selectedUser.addr = txAddr.Text;
            selectedUser.bankAddr = txBankAddr.Text;
            if (dateEdit.Text != string.Empty)
            {
                selectedUser.birthday = Convert.ToDateTime(dateEdit.Text);
            }
            selectedUser.created = selectedUser.created;
            selectedUser.department = txDept.Text;
            selectedUser.email = txMail.Text;
            selectedUser.nick = txName.Text;
            selectedUser.Remark = memo_Remark.Text;
            selectedUser.tel = txPhone.Text;
            selectedUser.UserCode = selectedUser.UserCode;
            selectedUser.status = cbStatus.SelectedIndex.ToString();
            if (rgSex.SelectedIndex == 0)
            {
                selectedUser.sex = "男";
            }
            else
            {
                selectedUser.sex = "女";
            }
            Alading.Business.UserService.UpdateUser(selectedUser);
        }

        /// <summary>
        /// 更新账号信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntSaveAccount_Click(object sender, EventArgs e)
        {
            updateUserInfo();
        }

        /// <summary>
        /// 新建角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntAddRole_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddRole addRole = new AddRole();
            if (DialogResult.OK == addRole.ShowDialog())
            {
                InitRoleTree();
            }
        }

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntSaveRole_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            if (selectedUser != null)
            {
                WaitDialogForm waitFrm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                SaveRole();
                SaveUserShop();
                SaveUserStockHouse();
                waitFrm.Close();
            }
            else 
            {
                XtraMessageBox.Show("没有选中用户！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }

        /// <summary>
        /// 保存员工角色
        /// </summary>
        private void SaveRole()
        {
            userRole.Clear();
            Alading.Business.UserRoleService.RemoveUserRole(p => p.UserCode == selectedUser.UserCode);
            
            foreach (TreeListNode node in tLRole.Nodes)
            {
                if (node.Checked == true)
                {
                    UserRole role = new UserRole();
                    role.UserRoleCode = System.Guid.NewGuid().ToString();
                    role.UserCode = selectedUser.UserCode;
                    role.RoleCode = node.Tag.ToString();
                    role.RoleType = Convert.ToInt32(role.RoleCode);
                    Alading.Business.UserRoleService.AddUserRole(role);
                }
            }
        }

        /// <summary>
        /// 保存客服商店
        /// </summary>
        private void SaveUserShop()
        {
            TreeListNode node = tLRole.Nodes[3];
            Alading.Business.UserShopService.RemoveUserShop(p => p.UserCode == selectedUser.UserCode);

            if (node.Nodes.Count > 0)
            {
                foreach (TreeListNode x in node.Nodes)
                {
                    if (x.Checked == true)
                    {
                        UserShop us = new UserShop();
                        us.ShopId = x.Tag.ToString();
                        us.UserCode = selectedUser.UserCode;
                        Alading.Business.UserShopService.AddUserShop(us);
                    }
                }
            }
        }

        /// <summary>
        /// 保存仓管
        /// </summary>
        private void SaveUserStockHouse()
        {
            TreeListNode node = tLRole.Nodes[5];
            Alading.Business.UserStockHouseService.RemoveUserStockHouse(p => p.UserCode == selectedUser.UserCode);

            if (node.Nodes.Count > 0)
            {
                foreach (TreeListNode x in node.Nodes)
                {
                    if (x.Checked == true)
                    {
                        UserStockHouse ush = new UserStockHouse();
                        ush.UserCode = selectedUser.UserCode;
                        ush.StockHouseCode = x.Tag.ToString();
                        Alading.Business.UserStockHouseService.AddUserStockHouse(ush);
                    }
                }
            }
        }

        /// <summary>
        /// 点击结点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tLRole_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
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
        /// 设置子结点状态
        /// </summary>
        /// <param name="currNode"></param>
        /// <param name="state"></param>
        private void setChildNodeCheckedState(TreeListNode currNode, bool state)
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
        /// 设置父结点状态
        /// </summary>
        /// <param name="fnode"></param>
        /// <param name="state"></param>
        private void setParentNodeCheckedState(TreeListNode fnode, bool state)
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
        /// 导出XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntExportXML_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gvStaff.RowCount > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "导出Excel";
                saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
                DialogResult dialogResult = saveFileDialog.ShowDialog(this);
                if (dialogResult == DialogResult.OK)
                {
                    DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                    gcUserList.ExportToExcelOld(saveFileDialog.FileName);
                    DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("没有数据可以导出！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        /// <summary>
        /// 必填项验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditValueChanged(object sender, EventArgs e)
        {
            if (txName.Text != string.Empty && txDept.Text != string.Empty )
            {
                btnSaveInfo.Enabled = true;
            }
            else
            {
                btnSaveInfo.Enabled = false;
            }
        }

        /// <summary>
        /// 删除非系统角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntDelRole_ItemClick(object sender, ItemClickEventArgs e)
        {
            TreeListNode node = tLRole.FocusedNode;
            if (node.ParentNode != null) //不是父结点不能删除
            {
                XtraMessageBox.Show(string.Format("无法删除{0}", node.GetDisplayText(0)), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (node.ParentNode == null) //非系统默认角色
            {
                if (node.GetDisplayText(1) == "False" && DialogResult.OK == XtraMessageBox.Show("确定删除角色？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    Alading.Business.RoleService.RemoveRole(p => p.RoleCode == node.Tag.ToString());
                }
            }
            else //系统默认角色
            {
                XtraMessageBox.Show("该角色是系统角色，无法删除！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            InitRoleTree();
            FillRoleTree();
        }

        /// <summary>
        /// 关键字查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txKeyWord.Text))
            {
                XtraMessageBox.Show("请先输入关键字", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                WaitDialogForm waitForm = new WaitDialogForm(Constants.WAIT_LOAD_DATA);
                bool hasData = KeyWordQuery(txKeyWord.Text);
                if (hasData) //有数据
                {
                    groupControl2.Text = "查找结果员工信息列表";
                    selectedUser = null;
                    gcUserList.DataSource = userList;
                    ClearInfoField();
                    waitForm.Close();
                }
                else
                {
                    waitForm.Close();
                    XtraMessageBox.Show("不存在相应数据", Constants.SYSTEM_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// 查询关键字数据
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        private bool KeyWordQuery(string keyWord)
        {
            userList = Alading.Business.UserService.GetUser(p => p.addr.Contains(keyWord) || p.account.Contains(keyWord) || p.bankAddr.Contains(keyWord)
                || p.department.Contains(keyWord) || p.nick.Contains(keyWord));
            if (userList.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
    }
}