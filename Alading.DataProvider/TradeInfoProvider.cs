using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Core.Interface;
using Alading.Entity;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Data.Objects;
using Alading.Core.Enum;
using System.Linq.Expressions;
using System.Data.EntityClient;
using System.Data;
using Alading.Core;
namespace Alading.DataProvider
{
   
    public partial class DataProviderClass : IAlading
    {        
     
        public ReturnType AddTradeInfo(TradeInfo tradeinfo)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToTradeInfo(tradeinfo);
                    if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
                    }
                    else
                    {
                        return ReturnType.PropertyExisted;
                    }
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (Exception ex)
            {
                return ReturnType.OthersError;
            }

        }

        public ReturnType AddTradeInfoSqlBulkCopy(DataTable dataTable)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    //获得连接字符串
                    string connectionString = ((EntityConnection)alading.Connection).StoreConnection.ConnectionString;

                    SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connectionString);
                    sqlBulkCopy.DestinationTableName = alading.TradeInfo.CommandText;
                    sqlBulkCopy.BatchSize = dataTable.Rows.Count;
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                    if (dataTable != null && dataTable.Rows.Count != 0)
                    {
                        sqlBulkCopy.ColumnMappings.Add("TradeInfoCode", "TradeInfoCode");
                        sqlBulkCopy.ColumnMappings.Add("CustomTid", "CustomTid");
                        sqlBulkCopy.ColumnMappings.Add("Title", "Title");
                        sqlBulkCopy.ColumnMappings.Add("Content", "Content");
                        sqlBulkCopy.ColumnMappings.Add("AppendUserCode", "AppendUserCode");
                        sqlBulkCopy.ColumnMappings.Add("AppendUserName", "AppendUserName");
                        sqlBulkCopy.ColumnMappings.Add("AppendDepartment", "AppendDepartment");
                        sqlBulkCopy.ColumnMappings.Add("AppendTime", "AppendTime");
                        sqlBulkCopy.WriteToServer(dataTable);
                    }
                    return ReturnType.Success;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType AddTradeInfo(List<TradeInfo> tradeinfoList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (TradeInfo tradeinfo in tradeinfoList)
                    {
                        alading.AddToTradeInfo(tradeinfo);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (Exception ex)
            {
                return ReturnType.OthersError;
            }

        }
       
        public ReturnType RemoveAllTradeInfo()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<TradeInfo> list = alading.TradeInfo.ToList();
                    foreach (TradeInfo tradeinfo in list)
                    {
                        alading.DeleteObject(tradeinfo);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;

                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }
       
        public ReturnType RemoveTradeInfo(Func<TradeInfo, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<TradeInfo> list = alading.TradeInfo.Where(func).ToList();
                    foreach (TradeInfo tradeinfo in list)
                    {
                        alading.DeleteObject(tradeinfo);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }

            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

        public List<TradeInfo> GetTradeInfo(List<string> tradeinfoCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.TradeInfo.Where(BuildWhereInExpression<TradeInfo, int>(v => v.TradeInfoID, tradeinfoIDList));*/
                    var result = alading.TradeInfo.Where(BuildWhereInExpression<TradeInfo, string>(v => v.TradeInfoCode, tradeinfoCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveTradeInfo(List<string> tradeinfoCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.TradeInfo.Where(BuildWhereInExpression<TradeInfo, int>(v => v.TradeInfoID, tradeinfoIDList));*/
                    var result = alading.TradeInfo.Where(BuildWhereInExpression<TradeInfo, string>(v => v.TradeInfoCode, tradeinfoCodeList));
                    foreach (TradeInfo s in result)
                    {
                        alading.DeleteObject(s);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

    
        public ReturnType RemoveTradeInfo(string tradeinfoCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<TradeInfo> list = alading.TradeInfo.Where(p => p.TradeInfoID == tradeinfoID).ToList();*/
                    List<TradeInfo> list = alading.TradeInfo.Where(p => p.TradeInfoCode == tradeinfoCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        TradeInfo sy = list.First();
                        alading.DeleteObject(sy);
                        alading.SaveChanges();
                        return ReturnType.Success;
                    }
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }
      
        public ReturnType UpdateTradeInfo(TradeInfo tradeinfo)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*TradeInfo result = alading.TradeInfo.Where(p => p.TradeInfoID == tradeinfo.TradeInfoID).FirstOrDefault();*/
                    TradeInfo result = alading.TradeInfo.Where(p => p.TradeInfoCode == tradeinfo.TradeInfoCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("TradeInfo", tradeinfo);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.TradeInfoCode = tradeinfo.TradeInfoCode;
                    
                        result.CustomTid = tradeinfo.CustomTid;
                    
                        result.Title = tradeinfo.Title;
                    
                        result.Content = tradeinfo.Content;
                    
                        result.AppendUserCode = tradeinfo.AppendUserCode;
                    
                        result.AppendUserName = tradeinfo.AppendUserName;
                    
                        result.AppendDepartment = tradeinfo.AppendDepartment;
                    
                        result.AppendTime = tradeinfo.AppendTime;
			
                    */
                    #endregion  
					if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
                    }
                    else
                    {
                        return ReturnType.OthersError;
                    }
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (Exception ex)
            {
                return ReturnType.OthersError;
            }
        }
       
        public ReturnType UpdateTradeInfo(string tradeinfoCode, TradeInfo tradeinfo)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.TradeInfo.Where(p => p.TradeInfoID == tradeinfoID).ToList();*/
                    var result = alading.TradeInfo.Where(p => p.TradeInfoCode == tradeinfoCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    TradeInfo ob = result.First();
                    ob.TradeInfoCode = tradeinfo.TradeInfoCode;
                    ob.CustomTid = tradeinfo.CustomTid;
                    ob.Title = tradeinfo.Title;
                    ob.Content = tradeinfo.Content;
                    ob.AppendUserCode = tradeinfo.AppendUserCode;
                    ob.AppendUserName = tradeinfo.AppendUserName;
                    ob.AppendDepartment = tradeinfo.AppendDepartment;
                    ob.AppendTime = tradeinfo.AppendTime;
                    
                    if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
                    }  
                    else
                    {
                        return ReturnType.OthersError;
                    }
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }
     
        public List<TradeInfo> GetAllTradeInfo()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<TradeInfo> list = alading.TradeInfo.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<TradeInfo> GetTradeInfo(Func<TradeInfo, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<TradeInfo> list = alading.TradeInfo.Where(func).OrderByDescending(t=>t.AppendTime).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public TradeInfo GetTradeInfo(string tradeinfoCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<TradeInfo> list = alading.TradeInfo.Where(p => p.TradeInfoID == tradeinfoID).ToList();*/
                    List<TradeInfo> list = alading.TradeInfo.Where(p => p.TradeInfoCode == tradeinfoCode).ToList();
                    if (list.Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return list.First();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<TradeInfo> GetTradeInfo(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.TradeInfo orderby u.TradeInfoID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.TradeInfo.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<TradeInfo> GetTradeInfo(Func<TradeInfo, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<TradeInfo> list = alading.TradeInfo.Where(func).OrderByDescending(a=>a.TradeInfoID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }        
    }
}

