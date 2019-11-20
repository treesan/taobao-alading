using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Alading.Entity;
using DevExpress.XtraTreeList.Nodes;

namespace Alading.Forms.Staff
{
    public partial class AchievementAssess : DevExpress.XtraEditors.XtraForm
    {
        private List<UserSalary> userSalary = new List<UserSalary>();
        private List<View_UserRole> view_userRole = new List<View_UserRole>();
        private List<UserSalary> dataSource = new List<UserSalary>();
        private List<Data> data = new List<Data>();
        private string userCode = string.Empty;
        
        public AchievementAssess()
        {
            InitializeComponent();            
        }

        private void AchievementAssess_Load(object sender, EventArgs e)
        {
            InitData();            
            InitDate();
            InitTree();
        }

        /// <summary>
        /// 初始化日期
        /// </summary>
        private void InitDate()
        {
            dEEnd.DateTime = DateTime.Now;
            dEEnd.Properties.EditMask = "D";
            dEEnd.Properties.EditFormat.FormatString = "D";
            dEEnd.Properties.DisplayFormat.FormatString = "D";
            dEEnd.Properties.MaxValue = DateTime.Now;
            
            dEBegin.DateTime = DateTime.Now.AddDays(-30);
            dEBegin.Properties.EditMask = "D";
            dEBegin.Properties.EditFormat.FormatString = "D";
            dEBegin.Properties.DisplayFormat.FormatString = "D";
            dEBegin.Properties.MaxValue = dEEnd.DateTime;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            userSalary = Alading.Business.UserSalaryService.GetAllUserSalary();
            view_userRole = Alading.Business.View_UserRoleService.GetView_UserRole(p => p.RoleCode == "4");
        }

        /// <summary>
        /// 初始化员工树
        /// </summary>
        private void InitTree()
        {
            TreeListNode node = tLRole.AppendNode(new object[] { "客服" }, null, string.Empty);
            foreach (View_UserRole us in view_userRole)
            {
                tLRole.AppendNode(new object[] { us.nick, string.Format("{0}元", CountSalary(us.UserCode)) }, node, us.UserCode);
            }
            tLRole.ExpandAll();
        }       

        /// <summary>
        /// 数据转换
        /// </summary>
        private void makeData()
        {
            data.Clear();

            foreach (UserSalary us in dataSource)
            {                
                Data d = new Data();
                d.Date = us.TradeDate.ToLongDateString();
                d.Price = us.Payment.ToString();
                d.Summary = string.Format("商品编号为{0},单价为{1},数量为{2},总价为{3},系统优惠{4}元，商店优惠{5}元，实付{6}元。应获提成{7}元！", us.iid, us.Price,
                    us.Num, us.TotlePrice, us.Discount_fee, us.Adjust_fee, us.Payment, us.Salary);
                data.Add(d);
            }
        }

        /// <summary>
        /// 日期更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateEdit_EditValueChanged(object sender, EventArgs e)
        {
            DateEdit de = sender as DateEdit;
            if (!string.IsNullOrEmpty(de.Text))
            {
                if (de.Name == "dEBegin")
                {

                }
                else if (de.Name == "dEEnd")
                {
                    dEBegin.Properties.MaxValue = dEEnd.DateTime;
                    dEBegin.DateTime = dEEnd.DateTime.AddDays(-30);
                }
            }
        }

        /// <summary>
        /// 查询按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            dataSource = userSalary.FindAll(p => p.UserCode == userCode && p.TradeDate <= dEEnd.DateTime && p.TradeDate >= dEBegin.DateTime);
            gcDetail.DataSource = null;
            makeData();
            gcDetail.DataSource = data;
        }

        /// <summary>
        /// 选择用户事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tLRole_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            userCode = e.Node.Tag.ToString();
            dataSource = userSalary.FindAll(p => p.UserCode == userCode);
            gcDetail.DataSource = null;
            makeData();
            gcDetail.DataSource = data;
        }

        /// <summary>
        /// 计算用户提成
        /// </summary>
        /// <returns></returns>
        private double CountSalary(string userCode)
        {
            dataSource = userSalary.FindAll(p => p.UserCode == userCode);
            double sum = 0;
            foreach (UserSalary us in dataSource)
            {
                sum += us.Salary;
            }
            return sum;
        }
        
    }

    /// <summary>
    /// 数据类
    /// </summary>
    public class Data
    {
        private string date = string.Empty; //日期
        private string price = string.Empty; //价格
        private string summary = string.Empty; //摘要

        public string Date
        {
            set { date = value; }
            get { return this.date; }
        }

        public string Price
        {
            set { price = value; }
            get { return price; }
        }

        public string Summary
        {
            set { summary = value; }
            get { return summary; }
        }
    }
}