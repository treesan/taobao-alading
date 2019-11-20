using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Collections;

namespace Alading.Utils
{
    //DBCreate.ExecuteSqlFile(@"E:\笔记\currentAlading413_am.sql");        
    
    public class DBCreate
    {
        public DBCreate() { }

        private static string ConStr = "";
        private static string ConString
        {
            get
            {
                if (ConStr == "")
                {
                    try
                    {
                        ConStr = "Data Source=.;Initial Catalog=master;Integrated Security=True";
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                return ConStr;
            }
        }
        private static SqlConnection Con;

        public static SqlConnection MyConnection
        {

            get
            {
                if (Con == null)
                {
                    Con = new SqlConnection(ConString);
                }
                return Con;
            }
        }
        ///<summary>
        ///执行Sql文件
        ///</summary>
        ///<paramname="varFileName"></param>
        ///<returns></returns>
        public static bool ExecuteSqlFile(string varFileName)
        {
            if (!File.Exists(varFileName))
            {
                return false;
            }
            StreamReader sr = File.OpenText(varFileName);

            ArrayList alSql = new ArrayList();

            string commandText = "";

            string varLine = "";

            while (sr.Peek() > -1)
            {
                varLine = sr.ReadLine();
                if (varLine == "")
                {
                    continue;
                }
                if (varLine != "GO")
                {
                    commandText += varLine;
                    commandText += "\r\n";
                }
                else
                {
                    //commandText += "\r\n";
                    alSql.Add(commandText);
                    commandText = "";
                }
            }
            sr.Close();
            try
            {
                ExecuteCommand(alSql);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public static void ExecuteCommand(ArrayList varSqlList)
        {
            try
            {
                MyConnection.Open();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            // SqlTransaction varTrans = MyConnection.BeginTransaction();
            SqlCommand command = new SqlCommand();
            command.Connection = MyConnection;
            MyConnection.Close();
            try
            {
                command.CommandText = "";
                foreach (string varcommandText in varSqlList)
                {
                    command.CommandText = varcommandText;
                    command.ExecuteNonQuery();
                }
                //varTrans.Commit();
            }
            catch (Exception ex)
            {
                // varTrans.Rollback();
                throw ex;
            }
            finally
            {
                MyConnection.Close();
            }
        }
    }
}
