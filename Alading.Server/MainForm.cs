using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.EntityClient;
using System.Data.SqlClient;
using DevExpress;

namespace Alading.Server
{
    public partial class MainForm : Form
    {
        public string value { get; set; }
        public string value2 { get; set; }
        public bool dbconn { get; set; }
        public string dbUrl { get; set; }
        private BackgroundWorker backworker = null;
        private DevExpress.XtraWizard.BaseWizardPage page = null;


        public MainForm()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (textIP.Text.Trim() == string.Empty)
            {
                XtraMessageBox.Show("数据库IP地址不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //if (textDBName.Text.Trim() == string.Empty)
            //{
            //    XtraMessageBox.Show("数据库名称不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            if (radioGroupType.SelectedIndex == 0)
            {
                if (textID.Text.Trim() == string.Empty && radioGroupType.SelectedIndex == 0)
                {
                    XtraMessageBox.Show("数据库用户名不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (textPSW.Text.Trim() == string.Empty && radioGroupType.SelectedIndex == 0)
                {
                    XtraMessageBox.Show("数据库密码不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (cbeDBType.Text == "SQLServer2008 Express")
                {
                    value = string.Format("Data Source={0}\\SQLEXPRESS;Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};MultipleActiveResultSets=True", textIP.Text.Trim(), "tempdb", textID.Text.Trim(), textPSW.Text.Trim());
                    value2 = string.Format("Data Source={0}\\SQLEXPRESS;Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};MultipleActiveResultSets=True", textIP.Text.Trim(), txtDBName.Text, textID.Text.Trim(), textPSW.Text.Trim());
                }
                else if (cbeDBType.Text == "SQLServer2008 Enterprise")
                {
                    value = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};MultipleActiveResultSets=True", textIP.Text.Trim(), "tempdb", textID.Text.Trim(), textPSW.Text.Trim());
                    value2 = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};MultipleActiveResultSets=True", textIP.Text.Trim(), txtDBName.Text, textID.Text.Trim(), textPSW.Text.Trim());
                }

            }
            else
            {
                if (cbeDBType.Text == "SQLServer2008 Express")
                {
                    value = string.Format("Data Source={0}\\SQLEXPRESS;Initial Catalog={1};Integrated Security=True;MultipleActiveResultSets=True", textIP.Text.Trim(), "tempdb");
                    value2 = string.Format("Data Source={0}\\SQLEXPRESS;Initial Catalog={1};Integrated Security=True;MultipleActiveResultSets=True", textIP.Text.Trim(), txtDBName.Text);
                }
                else if (cbeDBType.Text == "SQLServer2008 Enterprise")
                {
                    value = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;MultipleActiveResultSets=True", textIP.Text.Trim(), "tempdb");
                    value2 = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;MultipleActiveResultSets=True", textIP.Text.Trim(), txtDBName.Text);
                }
            }

            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
            entityBuilder.ProviderConnectionString = value;
            entityBuilder.Provider = "System.Data.SqlClient";

            entityBuilder.Metadata = "res://*/AladingModel.csdl|res://*/AladingModel.ssdl|res://*/AladingModel.msl";


            try
            {
                SqlConnection conn = null;
                using(conn = new SqlConnection(value))
                {                    
                    XtraMessageBox.Show("连接SqlServer数据库服务器成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dbconn = true;
                    if (page != null)
                    {
                        page.AllowNext = true;
                    }
                    conn.Close();
                }
            }
            catch
            {
                XtraMessageBox.Show("连接SqlServer数据库服务器失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dbconn = false;
            }
        }

        private void radioGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroupType.SelectedIndex == 1)
            {
                textID.Enabled = false;
                textPSW.Enabled = false;
            }
            else
            {
                textID.Enabled = true;
                textPSW.Enabled = true;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {            
            if (txtDBName.Text.Trim() == string.Empty)
            {
                XtraMessageBox.Show("数据库名称不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }            

            if (dbconn == true)
            {
                simpleButton1.Enabled = false;
                backworker = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
                backworker.DoWork += new DoWorkEventHandler(backworker_DoWork);
                backworker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backworker_RunWorkerCompleted);                
                backworker.RunWorkerAsync();               
            }
            else
            {
                XtraMessageBox.Show("请先返回上一步测试数据库连接是否成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void backworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                marqueeProgressBarControl1.Properties.Stopped = true;
                simpleButton1.Enabled = true;
            }));
        }

        void backworker_DoWork(object sender, DoWorkEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                marqueeProgressBarControl1.Properties.Stopped = false;
            }));

            string bakFileUrl = Application.StartupPath + "\\DataBase\\Alading.bak";           

            if (radioGroupType.SelectedIndex == 0)
            {
                if (textID.Text.Trim() == string.Empty && radioGroupType.SelectedIndex == 0)
                {
                    XtraMessageBox.Show("数据库用户名不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (textPSW.Text.Trim() == string.Empty && radioGroupType.SelectedIndex == 0)
                {
                    XtraMessageBox.Show("数据库密码不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (cbeDBType.Text == "SQLServer2008 Express")
                {
                    value = string.Format("Data Source={0}\\SQLEXPRESS;Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};MultipleActiveResultSets=True", textIP.Text.Trim(), "tempdb", textID.Text.Trim(), textPSW.Text.Trim());
                    value2 = string.Format("Data Source={0}\\SQLEXPRESS;Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};MultipleActiveResultSets=True", textIP.Text.Trim(), txtDBName.Text.Trim(), textID.Text.Trim(), textPSW.Text.Trim());
                }
                else if (cbeDBType.Text == "SQLServer2008 Enterprise")
                {
                    value = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};MultipleActiveResultSets=True", textIP.Text.Trim(), "tempdb", textID.Text.Trim(), textPSW.Text.Trim());
                    value2 = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};MultipleActiveResultSets=True", textIP.Text.Trim(), txtDBName.Text.Trim(), textID.Text.Trim(), textPSW.Text.Trim());
                }

            }
            else
            {
                if (cbeDBType.Text == "SQLServer2008 Express")
                {
                    value = string.Format("Data Source={0}\\SQLEXPRESS;Initial Catalog={1};Integrated Security=True;MultipleActiveResultSets=True", textIP.Text.Trim(), "tempdb");
                    value2 = string.Format("Data Source={0}\\SQLEXPRESS;Initial Catalog={1};Integrated Security=True;MultipleActiveResultSets=True", textIP.Text.Trim(), txtDBName.Text.Trim());
                }
                else if (cbeDBType.Text == "SQLServer2008 Enterprise")
                {
                    value = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;MultipleActiveResultSets=True", textIP.Text.Trim(), "tempdb");
                    value2 = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;MultipleActiveResultSets=True", textIP.Text.Trim(), txtDBName.Text.Trim());
                }
            }

            value += ";Connect Timeout=30";  //设置超时为30秒
            SqlConnection conn = new SqlConnection(value2);
            try
            {
                conn.Open();
                XtraMessageBox.Show("本地已经存在名字为" + txtDBName.Text.Trim() + "的数据库，不能进行数据库初始化！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
            catch
            {
                //BackupHelper.Restore(value, bakUrl, txtDBName.Text.Trim());
                BackupHelper.Restore(value, bakFileUrl,txtDBName.Text.Trim(), dbUrl);
            }            
        }

        private void wizardControl1_FinishClick(object sender, CancelEventArgs e)
        {
            this.Close();
        }

        private void wizardControl1_CancelClick(object sender, CancelEventArgs e)
        {
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonEdit1_Properties_Click(object sender, EventArgs e)
        {
            
        }

        private void txtDBAddress_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (txtDBName.Text.Trim() == string.Empty)
            {
                XtraMessageBox.Show("数据库名称不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
            {
                txtDBAddress.Text = folderBrowserDialog1.SelectedPath;
                dbUrl = txtDBAddress.Text;
            }
        }

        private void wizardControl1_SelectedPageChanged(object sender, DevExpress.XtraWizard.WizardPageChangedEventArgs e)
        {
            if (e.Page == wizardPage1 && dbconn != true)
            {
                page = e.Page;
                e.Page.AllowNext = false;
            }
        }
        
    }
}
