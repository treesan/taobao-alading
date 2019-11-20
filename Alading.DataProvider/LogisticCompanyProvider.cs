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
using Alading.Core;
namespace Alading.DataProvider
{
   
    public partial class DataProviderClass : IAlading
    {        
     
        public ReturnType AddLogisticCompany(LogisticCompany logisticcompany)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToLogisticCompany(logisticcompany);
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
                
        public ReturnType AddLogisticCompany(List<LogisticCompany> logisticcompanyList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (LogisticCompany logisticcompany in logisticcompanyList)
                    {
                        alading.AddToLogisticCompany(logisticcompany);
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
       
        public ReturnType RemoveAllLogisticCompany()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<LogisticCompany> list = alading.LogisticCompany.ToList();
                    foreach (LogisticCompany logisticcompany in list)
                    {
                        alading.DeleteObject(logisticcompany);
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
       
        public ReturnType RemoveLogisticCompany(Func<LogisticCompany, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<LogisticCompany> list = alading.LogisticCompany.Where(func).ToList();
                    foreach (LogisticCompany logisticcompany in list)
                    {
                        alading.DeleteObject(logisticcompany);
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

        public List<LogisticCompany> GetLogisticCompany(List<string> idList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.LogisticCompany.Where(BuildWhereInExpression<LogisticCompany, int>(v => v.LogisticCompanyID, logisticcompanyIDList));*/
                    var result = alading.LogisticCompany.Where(BuildWhereInExpression<LogisticCompany, string>(v => v.id, idList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveLogisticCompany(List<string> idList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.LogisticCompany.Where(BuildWhereInExpression<LogisticCompany, int>(v => v.LogisticCompanyID, logisticcompanyIDList));*/
                    var result = alading.LogisticCompany.Where(BuildWhereInExpression<LogisticCompany, string>(v => v.id, idList));
                    foreach (LogisticCompany s in result)
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


        public ReturnType RemoveLogisticCompany(string id)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<LogisticCompany> list = alading.LogisticCompany.Where(p => p.LogisticCompanyID == logisticcompanyID).ToList();*/
                    List<LogisticCompany> list = alading.LogisticCompany.Where(p => p.id == id).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        LogisticCompany sy = list.First();
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
      
        public ReturnType UpdateLogisticCompany(LogisticCompany logisticcompany)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*LogisticCompany result = alading.LogisticCompany.Where(p => p.LogisticCompanyID == logisticcompany.LogisticCompanyID).FirstOrDefault();*/
                    LogisticCompany result = alading.LogisticCompany.Where(p => p.id == logisticcompany.id).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("LogisticCompany", logisticcompany);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.id = logisticcompany.id;
                    
                        result.code = logisticcompany.code;
                    
                        result.name = logisticcompany.name;
			
                    */
                    #endregion  
					if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
                    }
                    return ReturnType.OthersError;
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

        public ReturnType UpdateLogisticCompany(string id, LogisticCompany logisticcompany)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.LogisticCompany.Where(p => p.LogisticCompanyID == logisticcompanyID).ToList();*/
                    var result = alading.LogisticCompany.Where(p => p.id == id).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    LogisticCompany ob = result.First();
                    ob.id = logisticcompany.id;
                    ob.code = logisticcompany.code;
                    ob.name = logisticcompany.name;
                    
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
     
        public List<LogisticCompany> GetAllLogisticCompany()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<LogisticCompany> list = alading.LogisticCompany.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<LogisticCompany> GetLogisticCompany(Func<LogisticCompany, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<LogisticCompany> list = alading.LogisticCompany.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public LogisticCompany GetLogisticCompany(string id)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<LogisticCompany> list = alading.LogisticCompany.Where(p => p.LogisticCompanyID == logisticcompanyID).ToList();*/
                    List<LogisticCompany> list = alading.LogisticCompany.Where(p => p.id == id).ToList();
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
        
        public List<LogisticCompany> GetLogisticCompany(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.LogisticCompany orderby u.LogisticCompanyID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.LogisticCompany.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<LogisticCompany> GetLogisticCompany(Func<LogisticCompany, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<LogisticCompany> list = alading.LogisticCompany.Where(func).OrderByDescending(a=>a.LogisticCompanyID);
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

