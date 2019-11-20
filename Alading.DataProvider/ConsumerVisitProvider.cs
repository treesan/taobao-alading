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
using System.Data;
using System.Data.EntityClient;
using Alading.Core;
namespace Alading.DataProvider
{
   
    public partial class DataProviderClass : IAlading
    {
        public ReturnType AddConsumerVisitSqlBulkCopy(DataTable dataTable)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    //获得连接字符串
                    string connectionString = ((EntityConnection)alading.Connection).StoreConnection.ConnectionString;

                    SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connectionString);
                    sqlBulkCopy.DestinationTableName = alading.ConsumerVisit.CommandText;
                    sqlBulkCopy.BatchSize = dataTable.Rows.Count;
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();
                        if (dataTable != null && dataTable.Rows.Count != 0)
                        {
                            sqlBulkCopy.WriteToServer(dataTable);
                        }
                    }
                    return ReturnType.Success;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType AddConsumerVisit(ConsumerVisit consumervisit)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var u1 = alading.Consumer.FirstOrDefault(c => c.nick == consumervisit.ConsumerNick);
                    if (u1 == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    else
                    {
                        u1.last_visit = consumervisit.VisitTime;
                        alading.AddToConsumerVisit(consumervisit);
                        if (alading.SaveChanges() == 2)
                        {
                            return ReturnType.Success;
                        }
                        else
                        {
                            return ReturnType.PropertyExisted;
                        }
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

        public ReturnType AddConsumerVisit(List<ConsumerVisit> consumervisitList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (ConsumerVisit consumervisit in consumervisitList)
                    {
                        var u1 = alading.Consumer.FirstOrDefault(c => c.nick == consumervisit.ConsumerNick);
                        if (u1 != null)
                        {
                            u1.last_visit = consumervisit.VisitTime;
                            alading.AddToConsumerVisit(consumervisit);
                        }
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

        public ReturnType RemoveAllConsumerVisit()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ConsumerVisit> list = alading.ConsumerVisit.ToList();
                    foreach (ConsumerVisit consumervisit in list)
                    {
                        alading.DeleteObject(consumervisit);
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

        public ReturnType RemoveConsumerVisit(Func<ConsumerVisit, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ConsumerVisit> list = alading.ConsumerVisit.Where(func).ToList();
                    foreach (ConsumerVisit consumervisit in list)
                    {
                        alading.DeleteObject(consumervisit);
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

        public List<ConsumerVisit> GetConsumerVisit(List<string> consumervisitCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.ConsumerVisit.Where(BuildWhereInExpression<ConsumerVisit, int>(v => v.ConsumerVisitID, consumervisitIDList));*/
                    var result = alading.ConsumerVisit.Where(BuildWhereInExpression<ConsumerVisit, string>(v => v.VisitCode, consumervisitCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveConsumerVisit(List<string> consumervisitCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.ConsumerVisit.Where(BuildWhereInExpression<ConsumerVisit, int>(v => v.ConsumerVisitID, consumervisitIDList));*/
                    var result = alading.ConsumerVisit.Where(BuildWhereInExpression<ConsumerVisit, string>(v => v.VisitCode, consumervisitCodeList));
                    foreach (ConsumerVisit s in result)
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


        public ReturnType RemoveConsumerVisit(string consumervisitCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<ConsumerVisit> list = alading.ConsumerVisit.Where(p => p.ConsumerVisitID == consumervisitID).ToList();*/
                    List<ConsumerVisit> list = alading.ConsumerVisit.Where(p => p.VisitCode == consumervisitCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        ConsumerVisit sy = list.First();
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

        public ReturnType UpdateConsumerVisit(ConsumerVisit consumervisit)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*ConsumerVisit result = alading.ConsumerVisit.Where(p => p.ConsumerVisitID == consumervisit.ConsumerVisitID).FirstOrDefault();*/
                    ConsumerVisit result = alading.ConsumerVisit.Where(p => p.VisitCode == consumervisit.VisitCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("ConsumerVisit", consumervisit);
                    #endregion

                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.Id = consumervisit.Id;
                    
                        result.VisitCode = consumervisit.VisitCode;
                    
                        result.ConsumerNick = consumervisit.ConsumerNick;
                    
                        result.Type = consumervisit.Type;
                    
                        result.Subject = consumervisit.Subject;
                    
                        result.Content = consumervisit.Content;
                    
                        result.VisitTime = consumervisit.VisitTime;
                    
                        result.Receiver = consumervisit.Receiver;
			
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

        public ReturnType UpdateConsumerVisit(string consumervisitCode, ConsumerVisit consumervisit)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.ConsumerVisit.Where(p => p.ConsumerVisitID == consumervisitID).ToList();*/
                    var result = alading.ConsumerVisit.Where(p => p.VisitCode == consumervisitCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    ConsumerVisit ob = result.First();
                    ob.Id = consumervisit.Id;
                    ob.VisitCode = consumervisit.VisitCode;
                    ob.ConsumerNick = consumervisit.ConsumerNick;
                    ob.Type = consumervisit.Type;
                    ob.Subject = consumervisit.Subject;
                    ob.Content = consumervisit.Content;
                    ob.VisitTime = consumervisit.VisitTime;
                    ob.Receiver = consumervisit.Receiver;

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

        public List<ConsumerVisit> GetAllConsumerVisit()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ConsumerVisit> list = alading.ConsumerVisit.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<ConsumerVisit> GetConsumerVisit(Func<ConsumerVisit, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ConsumerVisit> list = alading.ConsumerVisit.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public ConsumerVisit GetConsumerVisit(string consumervisitCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<ConsumerVisit> list = alading.ConsumerVisit.Where(p => p.ConsumerVisitID == consumervisitID).ToList();*/
                    List<ConsumerVisit> list = alading.ConsumerVisit.Where(p => p.VisitCode == consumervisitCode).ToList();
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

        public List<ConsumerVisit> GetConsumerVisit(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var ob = (from u in alading.ConsumerVisit orderby u.Id descending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    rowCount = alading.ConsumerVisit.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<ConsumerVisit> GetConsumerVisit(Func<ConsumerVisit, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<ConsumerVisit> list = alading.ConsumerVisit.Where(func).OrderByDescending(a => a.Id);
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

