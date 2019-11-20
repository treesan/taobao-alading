using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Alading.Forms.Backup
{
    public class MySqlBackupHelper
    {
        /// <summary>
        /// mysql 数据库备份方法
        /// </summary>
        /// <param name="serverName">服务器名称(localhost)</param>
        /// <param name="userName">用户名(root)</param>
        /// <param name="passWord">密码</param>
        /// <param name="DBName">要备份的数据库名称</param>
        /// <param name="backupUrl">备份文件的路径(.sql)</param>
        public static void Backup(string serverName, string port, string userName, string passWord, string DBName, string backupUrl)
        {
            string cmdStr = string.Format("/c mysqldump -h{0} -P{1} -u{2} -p{3} {4} > {5}", serverName, port, userName, passWord, DBName, backupUrl);
            try
            {
                System.Diagnostics.Process.Start("cmd", cmdStr);
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// mysql 数据库的恢复
        /// </summary>
        /// <param name="serverName">服务器名称(localhost)</param>
        /// <param name="userName">用户名(root)</param>
        /// <param name="passWord">密码</param>
        /// <param name="DBName">要备份的数据库名称</param>
        /// <param name="backupUrl">备份文件的路径(.sql)</param>
        public static void Restore(string serverName, string port, string userName, string passWord, string DBName, string backupUrl)
        {
            string cmdStr = string.Format("/c mysql -h{0} -P{1} -u{2} -p{3} {4} < {5}", serverName, port, userName, passWord, DBName, backupUrl);
            try
            {
                System.Diagnostics.Process.Start("cmd", cmdStr);
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// mysql批量插入数据
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="port"></param>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="DBName"></param>
        /// <param name="tableName"></param>
        /// <param name="sourceFile"></param>
        public static void LotSizeInsert(string serverName, string port, string userName, string passWord, string DBName, string tableName, string sourceFile)
        {
            /*IGNORE 跳过有唯一键的现有行的重复行的输入。
             *Fields Terminated By ',' 导入的数据字段间以逗号为间隔
             *Lines Terminated By '\r\n' 导入的数据文件以换行符分行
             */
            MySqlConnection conn = null;
            MySqlCommand command = null;
            try
            {
                string constr = string.Format("Server={0};Port={1};User Id={2};Password={3};Persist Security Info=True;Database='{4}'", serverName, port, userName, passWord, DBName);
                conn = new MySqlConnection(constr);
                command = conn.CreateCommand();
                command.CommandText = string.Format("Load Data InFile '{0}' IGNORE Into Table {1} Fields Terminated By ',' Lines Terminated By '\r\n' ", sourceFile, DBName + "." + tableName);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message, "数据库通信出错");
            }
        }
    }
}
