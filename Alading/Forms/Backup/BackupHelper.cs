using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Alading.Core;
using System.Data.SqlClient;
using System.Windows.Forms;


/**********************************************************
 * 简单说明：msadox.dll 与msjro.dll 备份access数据库
 * msadox.dll包含ADOX命名空间  该命名空间包含创建ACCESS的类(方法)--
 * msjro.dll 包含JRO命名空间       该命名空间包含压缩ACCESS的类(方法)
 ******************************************************** **/
namespace Alading.Forms.Backup
{
    public class BackupHelper
    {
        #region DMO做法
        ///// <summary>
        ///// sql server 2005备份数据库代码
        ///// </summary>
        ///// <param name="serverName">服务器名称</param>
        ///// <param name="userName">用户名</param>
        ///// <param name="passWord">密码</param>
        ///// <param name="dataBaseName">要备份的数据库名称</param>
        ///// <param name="backupDataBaseFileName">备份文件夹的名称</param>
        ///// <param name="backupDescription">备份描述： 如XXX数据库的备份</param>
        //public void Backup(string serverName,string userName,string passWord,string dataBaseName ,string backupDataBaseFileName,string backupDescription)
        //{
        //    SQLDMO.Backup oBackup = new SQLDMO.BackupClass();
        //    SQLDMO.SQLServer oSQLServer = new SQLDMO.SQLServerClass();
        //    try
        //    {
        //        oSQLServer.LoginSecure = false;
        //        oSQLServer.Connect(serverName, userName, passWord);
        //        oBackup.Action = SQLDMO.SQLDMO_BACKUP_TYPE.SQLDMOBackup_Database;
        //        oBackup.Database = dataBaseName;
        //        oBackup.Files = backupDataBaseFileName;
        //        oBackup.BackupSetName = dataBaseName;
        //        oBackup.BackupSetDescription = backupDescription;
        //        oBackup.Initialize = true;
        //        oBackup.SQLBackup(oSQLServer);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        oSQLServer.DisConnect();
        //    }
        //}

        ///// <summary>
        ///// sql server 2005恢复数据库的方法
        ///// </summary>
        ///// <param name="serverName">服务器名称</param>
        ///// <param name="userName">用户名</param>
        ///// <param name="passWord">密码</param>
        ///// <param name="RestoreDBName">要恢复的数据库名称</param>
        ///// <param name="backupFileUrl">数据库备份文件路径</param>
        //public void Restore(string serverName, string userName, string passWord,string RestoreDBName,string backupFileUrl)
        //{
        //    SQLDMO.Restore oRestore = new SQLDMO.RestoreClass();
        //    SQLDMO.SQLServer oSQLServer = new SQLDMO.SQLServerClass();
        //    try
        //    {
        //        oSQLServer.LoginSecure = false;
        //        oSQLServer.Connect(serverName, userName, passWord);
        //        oRestore.Action = SQLDMO.SQLDMO_RESTORE_TYPE.SQLDMORestore_Database;
        //        oRestore.Database = RestoreDBName;
        //        oRestore.Files = backupFileUrl;
        //        oRestore.FileNumber = 1;
        //        oRestore.ReplaceDatabase = true;
        //        oRestore.SQLRestore(oSQLServer);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        oSQLServer.DisConnect();
        //    }
        //}
        #endregion

        /// <summary>
        /// sql server备份方法
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="databaseName"></param>
        /// <param name="backupUrl"></param>
        public static void Backup(string serverName, string userName, string passWord, string databaseName, string backupUrl)
        {
            string constr = string.Format("user id={0};password={1};initial catalog={2};data source={3}", userName, passWord, databaseName, serverName);

            SqlConnection conn = new SqlConnection(constr);
            try
            {
                conn.Open();

                string commandStr = string.Format("backup database {0} to disk='{1}'", databaseName, backupUrl);
                SqlCommand command = new SqlCommand(commandStr);
                command.Connection = conn;
                int query = command.ExecuteNonQuery();
                if (query == -1)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("备份成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("备份失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                conn.Close();
            }
            catch (Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(e.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// sql server恢复方法
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="databaseName"></param>
        /// <param name="backupUrl"></param>
        public static void Restore(string serverName, string userName, string passWord, string databaseName, string backupUrl)
        {
            string constr = string.Format("user id={0};password={1};initial catalog={2};data source={3}", userName, passWord, "master", serverName);
            SqlConnection conn = new SqlConnection(constr);
            try
            {
                conn.Open();

                string commandStr = string.Format("restore database {0} from disk='{1}' with replace", databaseName, backupUrl);
                SqlCommand command = new SqlCommand(commandStr);
                command.Connection = conn;
                int query = command.ExecuteNonQuery();
                if (query == -1)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("恢复成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("恢复失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                conn.Close();
            }
            catch (Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(e.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
