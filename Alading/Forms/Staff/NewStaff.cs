using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using Alading.Entity;
using Alading.Taobao;
using System.Text.RegularExpressions;

namespace Alading.Forms.StaffManager
{
    public partial class NewStaff : DevExpress.XtraEditors.XtraForm
    {
        private List<Role> role = new List<Role>();
        private List<Alading.Entity.Shop> shop = new List<Alading.Entity.Shop>();
        private List<StockHouse> stockHouse = new List<StockHouse>();
        private User selectedUser = null;

        public NewStaff()
        {
            InitializeComponent();
            InitRoleTree();
        }

        /// <summary>
        /// 初始化角色树
        /// </summary>
        private void InitRoleTree()
        {
            TreeListNode node = null;
            role = Alading.Business.RoleService.GetAllRole();
            shop = Alading.Business.ShopService.GetAllShop();
            stockHouse = Alading.Business.StockHouseService.GetAllStockHouse();
            foreach (Role r in role)
            {
                node = tLRole.AppendNode(new object[] { r.RoleName }, null, r.RoleCode);
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
                        tLRole.AppendNode(new object[] { i.HouseName }, node, i.StockHouseCode);
                    }
                }
            }
            tLRole.ExpandAll();
        }

        /// <summary>
        /// 必填项验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditValueChanged(object sender, EventArgs e)
        {
            if (txName.Text != string.Empty && txDept.Text != string.Empty && txPassword.Text != string.Empty)
            {
                bntSave.Enabled = true;
            }
            else
            {
                bntSave.Enabled = false;
            }
        }

        /// <summary>
        /// 保存新增员工信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntSave_Click(object sender, EventArgs e)
        {
            Alading.Entity.User user = new Alading.Entity.User();

            user.UserCode = System.Guid.NewGuid().ToString();
            user.nick = txName.Text;
            user.account = txAccount.Text;
            user.bankAddr = txBankAddr.Text;
            user.password = txPassword.Text;
            user.department = txDept.Text;
            user.email = txMail.Text;
            user.created = DateTime.Now;
            user.last_visit = DateTime.Now;
            if (dateEdit.Text != string.Empty)
            {
                user.birthday = Convert.ToDateTime(dateEdit.Text);
            }
            user.Remark = memo_Remark.Text;
            user.tel = txPhone.Text;
            user.addr = txAddr.Text;
            user.status = "0";
            if (rgSex.SelectedIndex == 0)
            {
                user.sex = "男";
            }
            else
            {
                user.sex = "女";
            }

            Alading.Business.UserService.AddUser(user);
            selectedUser = user;
            SaveRole();
            SaveUserShop();
            SaveUserStockHouse();
        }

        /// <summary>
        /// 保存员工角色
        /// </summary>
        private void SaveRole()
        {
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
    }
}