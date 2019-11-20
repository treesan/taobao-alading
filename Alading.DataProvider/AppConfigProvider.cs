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

namespace Alading.DataProvider
{
   
    public partial class DataProviderClass : IAlading
    {        
     
        public ReturnType AddAppConfig(AppConfig appconfig)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities())
                {

                    alading.AddToAppConfig(appconfig);
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
                
        public ReturnType AddAppConfig(List<AppConfig> appconfigList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities())
                {
                    foreach (AppConfig appconfig in appconfigList)
                    {
                        alading.AddToAppConfig(appconfig);
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
       
        public ReturnType RemoveAllAppConfig()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities())
                {
                    List<AppConfig> list = alading.AppConfig.ToList();
                    foreach (AppConfig appconfig in list)
                    {
                        alading.DeleteObject(appconfig);
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
       
        public ReturnType RemoveAppConfig(Func<AppConfig, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities())
                {
                    List<AppConfig> list = alading.AppConfig.Where(func).ToList();
                    foreach (AppConfig appconfig in list)
                    {
                        alading.DeleteObject(appconfig);
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

        public List<AppConfig> GetAppConfig(List<string> appconfigCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities())
                {
					/*var result = alading.AppConfig.Where(BuildWhereInExpression<AppConfig, int>(v => v.AppConfigID, appconfigIDList));*/
                    var result = alading.AppConfig.Where(BuildWhereInExpression<AppConfig, string>(v => v.Code, appconfigCodeList));
                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveAppConfig(List<string> appconfigCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities())
                {
                    /*var result = alading.AppConfig.Where(BuildWhereInExpression<AppConfig, int>(v => v.AppConfigID, appconfigIDList));*/
                    var result = alading.AppConfig.Where(BuildWhereInExpression<AppConfig, string>(v => v.Code, appconfigCodeList));
                    foreach (AppConfig s in result)
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

    
        public ReturnType RemoveAppConfig(string appconfigCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities())
                {
					/*List<AppConfig> list = alading.AppConfig.Where(p => p.AppConfigID == appconfigID).ToList();*/
                    List<AppConfig> list = alading.AppConfig.Where(p => p.Code == appconfigCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                    else
                    {
                        AppConfig sy = list.First();
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
      
        public ReturnType UpdateAppConfig(AppConfig appconfig)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities())
                {
                    /*AppConfig result = alading.AppConfig.Where(p => p.AppConfigID == appconfig.AppConfigID).FirstOrDefault();*/
                    AppConfig result = alading.AppConfig.Where(p => p.Code == appconfig.Code).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("AppConfig", appconfig);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                        result.Code = appconfig.Code;
                        result.ConfigContent = appconfig.ConfigContent;
			
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
       
        public ReturnType UpdateAppConfig(string appconfigCode, AppConfig appconfig)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities())
                {
                    /*var result = alading.AppConfig.Where(p => p.AppConfigID == appconfigID).ToList();*/
                    var result = alading.AppConfig.Where(p => p.Code == appconfigCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    AppConfig ob = result.First();
                    ob.Code = appconfig.Code;
                    ob.ConfigContent = appconfig.ConfigContent;
                    
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
     
        public List<AppConfig> GetAllAppConfig()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities())
                {
                    List<AppConfig> list = alading.AppConfig.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<AppConfig> GetAppConfig(Func<AppConfig, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities())
                {
                    List<AppConfig> list = alading.AppConfig.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public AppConfig GetAppConfig(string appconfigCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities())
                {
                    /*List<AppConfig> list = alading.AppConfig.Where(p => p.AppConfigID == appconfigID).ToList();*/
                    List<AppConfig> list = alading.AppConfig.Where(p => p.Code == appconfigCode).ToList();
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
        
        public List<AppConfig> GetAppConfig(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities())
                {                    
					var ob = (from u in alading.AppConfig orderby u.ID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.AppConfig.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<AppConfig> GetAppConfig(Func<AppConfig, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities())
                {
                    IOrderedEnumerable<AppConfig> list = alading.AppConfig.Where(func).OrderByDescending(a=>a.ID);
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

