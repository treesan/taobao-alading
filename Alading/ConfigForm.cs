using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.EntityClient;
using Alading.Core.Helper;
using System.Configuration;
using System.Xml;

namespace Alading
{
    public partial class ConfigForm : DevExpress.XtraEditors.XtraForm
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (textIP.Text.Trim() == string.Empty)
            {
                XtraMessageBox.Show("数据库IP地址不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (textDBName.Text.Trim() == string.Empty)
            {
                XtraMessageBox.Show("数据库名称不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }            

            string value = string.Empty;
            if (radioGroupType.SelectedIndex == 0)
            {
                if (textID.Text.Trim() == string.Empty)
                {
                    XtraMessageBox.Show("数据库用户名不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (textPSW.Text.Trim() == string.Empty)
                {
                    XtraMessageBox.Show("数据库密码不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (cbeDBType.Text == "SQLServer2008 Express")
                {
                    value = string.Format("Data Source={0}\\SQLEXPRESS;Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};MultipleActiveResultSets=True", textIP.Text.Trim(), textDBName.Text.Trim(), textID.Text.Trim(), textPSW.Text.Trim());
                }
                else if (cbeDBType.Text == "SQLServer2008 Enterprise")
                {
                    value = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};MultipleActiveResultSets=True", textIP.Text.Trim(), textDBName.Text.Trim(), textID.Text.Trim(), textPSW.Text.Trim());
                }

            }
            else
            {
                if (cbeDBType.Text == "SQLServer2008 Express")
                {
                    value = string.Format("Data Source={0}\\SQLEXPRESS;Initial Catalog={1};Integrated Security=True;MultipleActiveResultSets=True", textIP.Text.Trim(), textDBName.Text.Trim());
                }
                else if (cbeDBType.Text == "SQLServer2008 Enterprise")
                {
                    value = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;MultipleActiveResultSets=True", textIP.Text.Trim(), textDBName.Text.Trim());
                }
            } 
            
            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
            entityBuilder.ProviderConnectionString = value;
            entityBuilder.Provider = "System.Data.SqlClient";
           
            entityBuilder.Metadata = "res://*/AladingModel.csdl|res://*/AladingModel.ssdl|res://*/AladingModel.msl";
            
            using (Alading.Entity.AladingEntities alading = new Alading.Entity.AladingEntities(entityBuilder.ToString()))
            {
                try
                {
                    //打开数据库库连接
                    alading.Connection.Open();
                    ConfigHelper configHelper = new ConfigHelper();
                    configHelper.SetConfigName("\\Alading.exe.config");
                    SaveConfig("SqlServerDataSource", textIP.Text.Trim());
                    SaveConfig("SqlServerInitialCatalog", textDBName.Text.Trim());
                    SaveConfig("SqlServerUserID", textID.Text.Trim());
                    SaveConfig("SqlServerPassword", textPSW.Text.Trim());
                    SaveConfig("DataBaseType", cbeDBType.Text.Trim());
                    SaveConfig("ValidateType", radioGroupType.SelectedIndex.ToString());
                    

                    //XtraMessageBox.Show("连接SqlServer数据库服务器成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                       
                    alading.Connection.Close();
                    this.Close();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                
            }
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            ConfigHelper configHelper = new ConfigHelper();
            configHelper.SetConfigName("\\Alading.exe.config");
            textIP.Text = configHelper.ReadConfig("SqlServerDataSource");// ConfigurationManager.AppSettings["SqlServerDataSource"];
            textDBName.Text = configHelper.ReadConfig("SqlServerInitialCatalog"); //ConfigurationManager.AppSettings["SqlServerInitialCatalog"];
            textID.Text = configHelper.ReadConfig("SqlServerUserID"); //ConfigurationManager.AppSettings["SqlServerUserID"];
            textPSW.Text = configHelper.ReadConfig("SqlServerPassword"); //ConfigurationManager.AppSettings["SqlServerPassword"];
            radioGroupType.SelectedIndex = int.Parse(configHelper.ReadConfig("ValidateType"));
            cbeDBType.Text = configHelper.ReadConfig("DataBaseType");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveConfig(string key, string value)
        {
            string appUrl = Application.StartupPath + "\\Alading.exe.config";
            XmlDocument doc = new XmlDocument();
            doc.Load(appUrl);
            //找出名称为“add”的所有元素
            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                if (nodes[i].Attributes["key"] != null)
                {
                    XmlAttribute att = nodes[i].Attributes["key"];
                    //根据元素的第一个属性来判断当前的元素是不是目标元素
                    if (att.Value == "" + key + "")
                    {
                        //对目标元素中的第二个属性赋值
                        att = nodes[i].Attributes["value"];
                        att.Value = value;
                        break;
                    }
                }
            }
            //保存上面的修改
            doc.Save(appUrl);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (textIP.Text.Trim() == string.Empty)
            {
                XtraMessageBox.Show("数据库IP地址不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (textDBName.Text.Trim() == string.Empty)
            {
                XtraMessageBox.Show("数据库名称不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (textID.Text.Trim() == string.Empty)
            {
                XtraMessageBox.Show("数据库用户名不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (textPSW.Text.Trim() == string.Empty)
            {
                XtraMessageBox.Show("数据库密码不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string value = string.Empty;
            if (radioGroupType.SelectedIndex == 0)
            {
                if (cbeDBType.Text == "SQLServer2008 Express")
                {
                    value = string.Format("Data Source={0}\\SQLEXPRESS;Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};MultipleActiveResultSets=True", textIP.Text.Trim(), textDBName.Text.Trim(), textID.Text.Trim(), textPSW.Text.Trim());
                }
                else if (cbeDBType.Text == "SQLServer2008 Enterprise")
                {
                    value = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};MultipleActiveResultSets=True", textIP.Text.Trim(), textDBName.Text.Trim(), textID.Text.Trim(), textPSW.Text.Trim());
                }

            }
            else
            {
                if (cbeDBType.Text == "SQLServer2008 Express")
                {
                    value = string.Format("Data Source={0}\\SQLEXPRESS;Initial Catalog={1};Integrated Security=True;MultipleActiveResultSets=True", textIP.Text.Trim(), textDBName.Text.Trim());
                }
                else if (cbeDBType.Text == "SQLServer2008 Enterprise")
                {
                    value = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;MultipleActiveResultSets=True", textIP.Text.Trim(), textDBName.Text.Trim());
                }
            }         
                
            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
            entityBuilder.ProviderConnectionString = value;
            entityBuilder.Provider = "System.Data.SqlClient";

            entityBuilder.Metadata = "res://*/AladingModel.csdl|res://*/AladingModel.ssdl|res://*/AladingModel.msl";
            

            try
            {
                using (Alading.Entity.AladingEntities alading = new Alading.Entity.AladingEntities(entityBuilder.ToString()))
                {
                    alading.Connection.Open();
                    ConfigHelper configHelper = new ConfigHelper();
                    configHelper.SetConfigName("\\Alading.exe.config");
                    SaveConfig("SqlServerDataSource", textIP.Text.Trim());
                    SaveConfig("SqlServerInitialCatalog", textDBName.Text.Trim());
                    SaveConfig("SqlServerUserID", textID.Text.Trim());
                    SaveConfig("SqlServerPassword", textPSW.Text.Trim());
                    SaveConfig("DataBaseType", cbeDBType.Text.Trim());
                    SaveConfig("ValidateType", radioGroupType.SelectedIndex.ToString());
                    XtraMessageBox.Show("连接SqlServer数据库服务器成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alading.Connection.Close();
                }
            }
            catch
            {
                XtraMessageBox.Show("连接SqlServer数据库服务器失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void radioGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*windows认证 数据库账号，密码不可编辑*/
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
    }
}