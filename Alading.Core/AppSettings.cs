using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.EntityClient;

namespace Alading.Core
{
    /// <summary>
    /// 系统静态配置
    /// </summary>
    public static class AppSettings
    {
        private static string m_DataBase = "SqlServer";
        private static string orderprintername="NULL";
        public static string DataBase
        {
            set
            {
                m_DataBase = value;
            }
            get
            {
                return m_DataBase;
            }
        }
        public static string OrderPrintName
        {
            set
            {
                orderprintername = value;
            }
            get
            {
                return orderprintername;
            }
        }

        public static string GetConnectionString()
        {
           return GetConnectionString(m_DataBase);
        }

        private static string GetConnectionString(string strPrefix)
        {
            string value = string.Empty;
            string strDataSource = ConfigurationManager.AppSettings[strPrefix + "DataSource"];
            string strInitialCatalog = ConfigurationManager.AppSettings[strPrefix + "InitialCatalog"];
            string strUserID = ConfigurationManager.AppSettings[strPrefix + "UserID"];
            string strPassword = ConfigurationManager.AppSettings[strPrefix + "Password"];

            string strDataBaseType = ConfigurationManager.AppSettings["DataBaseType"];

            if (ConfigurationManager.AppSettings["ValidateType"] == "0")
            {
                if (strDataBaseType == "SQLServer2008 Express")
                {
                    value = string.Format("Data Source={0}\\SQLEXPRESS;Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};MultipleActiveResultSets=True", strDataSource, strInitialCatalog, strUserID, strPassword);
                }
                else if (strDataBaseType == "SQLServer2008 Enterprise")
                {
                    value = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};MultipleActiveResultSets=True", strDataSource, strInitialCatalog, strUserID, strPassword);
                }

            }
            else
            {
                if (strDataBaseType == "SQLServer2008 Express")
                {
                    value = string.Format("Data Source={0}\\SQLEXPRESS;Initial Catalog={1};Integrated Security=True;MultipleActiveResultSets=True", strDataSource, strInitialCatalog);
                }
                else if (strDataBaseType == "SQLServer2008 Enterprise")
                {
                    value = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;MultipleActiveResultSets=True", strDataSource, strInitialCatalog);
                }
            } 

            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
            entityBuilder.ProviderConnectionString = value;
            entityBuilder.Provider = "System.Data.SqlClient";
            entityBuilder.Metadata = "res://*/AladingModel.csdl|res://*/AladingModel.ssdl|res://*/AladingModel.msl";
            
            entityBuilder.Provider = ConfigurationManager.AppSettings[strPrefix + "ProviderName"];
            // Set the Metadata location.            
            entityBuilder.Metadata = ConfigurationManager.AppSettings[strPrefix + "MetaDataName"];
            return entityBuilder.ToString();
        }
    }
}
