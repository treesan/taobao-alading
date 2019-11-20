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
     
        public ReturnType AddLogisticCompanyTemplateItem(LogisticCompanyTemplateItem logisticcompanytemplateitem)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToLogisticCompanyTemplateItem(logisticcompanytemplateitem);
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
                
        public ReturnType AddLogisticCompanyTemplateItem(List<LogisticCompanyTemplateItem> logisticcompanytemplateitemList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (LogisticCompanyTemplateItem logisticcompanytemplateitem in logisticcompanytemplateitemList)
                    {
                        alading.AddToLogisticCompanyTemplateItem(logisticcompanytemplateitem);
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

        public ReturnType RemoveLogisticCompanyTemplateItems(string logisticCompanyTemplateCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<LogisticCompanyTemplateItem> list = alading.LogisticCompanyTemplateItem.Where(p => p.LogisticCompanyTemplateCode == logisticCompanyTemplateCode).ToList();
                    foreach (LogisticCompanyTemplateItem logisticcompanytemplateitem in list)
                    {
                        alading.DeleteObject(logisticcompanytemplateitem);
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
       
        public ReturnType RemoveLogisticCompanyTemplateItem(Func<LogisticCompanyTemplateItem, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<LogisticCompanyTemplateItem> list = alading.LogisticCompanyTemplateItem.Where(func).ToList();
                    foreach (LogisticCompanyTemplateItem logisticcompanytemplateitem in list)
                    {
                        alading.DeleteObject(logisticcompanytemplateitem);
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

        public List<LogisticCompanyTemplateItem> GetLogisticCompanyTemplateItem(List<string> logisticcompanytemplateitemCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.LogisticCompanyTemplateItem.Where(BuildWhereInExpression<LogisticCompanyTemplateItem, int>(v => v.LogisticCompanyTemplateItemID, logisticcompanytemplateitemIDList));*/
                    var result = alading.LogisticCompanyTemplateItem.Where(BuildWhereInExpression<LogisticCompanyTemplateItem, string>(v => v.LogisticCompanyTemplateCode, logisticcompanytemplateitemCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveLogisticCompanyTemplateItem(List<string> logisticcompanytemplateitemCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.LogisticCompanyTemplateItem.Where(BuildWhereInExpression<LogisticCompanyTemplateItem, int>(v => v.LogisticCompanyTemplateItemID, logisticcompanytemplateitemIDList));*/
                    //var result = alading.LogisticCompanyTemplateItem.Where(BuildWhereInExpression<LogisticCompanyTemplateItem, string>(v => v.LogisticCompanyTemplateItemCode, logisticcompanytemplateitemCodeList));
                    //foreach (LogisticCompanyTemplateItem s in result)
                    //{
                    //    alading.DeleteObject(s);
                    //}
                    //alading.SaveChanges();
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

    
        public ReturnType RemoveLogisticCompanyTemplateItem(string logisticcompanytemplateitemCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<LogisticCompanyTemplateItem> list = alading.LogisticCompanyTemplateItem.Where(p => p.LogisticCompanyTemplateItemID == logisticcompanytemplateitemID).ToList();*/
                    //List<LogisticCompanyTemplateItem> list = alading.LogisticCompanyTemplateItem.Where(p => p.LogisticCompanyTemplateItemCode == logisticcompanytemplateitemCode).ToList();
                    //if (list.Count == 0)
                    //{
                    //    return ReturnType.NotExisted;
                    //}

                    //else
                    //{
                    //    LogisticCompanyTemplateItem sy = list.First();
                    //    alading.DeleteObject(sy);
                    //    alading.SaveChanges();
                    //    return ReturnType.Success;
                    //}
                    return ReturnType.SaveFailed;
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
      
        public ReturnType UpdateLogisticCompanyTemplateItem(LogisticCompanyTemplateItem logisticcompanytemplateitem)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*LogisticCompanyTemplateItem result = alading.LogisticCompanyTemplateItem.Where(p => p.LogisticCompanyTemplateItemID == logisticcompanytemplateitem.LogisticCompanyTemplateItemID).FirstOrDefault();*/
                    LogisticCompanyTemplateItem result = alading.LogisticCompanyTemplateItem.Where(p => p.LogisticCompanyItemCode == logisticcompanytemplateitem.LogisticCompanyItemCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;
                    alading.Attach(result);                    
                    alading.ApplyPropertyChanges("LogisticCompanyTemplateItem", logisticcompanytemplateitem);
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
       
        public ReturnType UpdateLogisticCompanyTemplateItem(string logisticcompanytemplateitemCode, LogisticCompanyTemplateItem logisticcompanytemplateitem)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    ///*var result = alading.LogisticCompanyTemplateItem.Where(p => p.LogisticCompanyTemplateItemID == logisticcompanytemplateitemID).ToList();*/
                    //var result = alading.LogisticCompanyTemplateItem.Where(p => p.LogisticCompanyTemplateItemCode == logisticcompanytemplateitemCode).ToList();
                    //if (result.Count == 0)
                    //{
                    //    return ReturnType.NotExisted;
                    //}
                  
                    //LogisticCompanyTemplateItem ob = result.First();
                    //ob.LogisticCompanyItemCode = logisticcompanytemplateitem.LogisticCompanyItemCode;
                    //ob.LogisticCompanyTemplateCode = logisticcompanytemplateitem.LogisticCompanyTemplateCode;
                    //ob.ItemName = logisticcompanytemplateitem.ItemName;
                    //ob.ItemValue = logisticcompanytemplateitem.ItemValue;
                    //ob.ItemX = logisticcompanytemplateitem.ItemX;
                    //ob.ItemY = logisticcompanytemplateitem.ItemY;
                    
                    //if (alading.SaveChanges() == 1)
                    //{
                    //    return ReturnType.Success;
                    //}  
                    //else
                    //{
                    //    return ReturnType.OthersError;
                    //}
                    return ReturnType.SaveFailed;
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
     
        public List<LogisticCompanyTemplateItem> GetAllLogisticCompanyTemplateItem()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<LogisticCompanyTemplateItem> list = alading.LogisticCompanyTemplateItem.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<LogisticCompanyTemplateItem> GetLogisticCompanyTemplateItem(Func<LogisticCompanyTemplateItem, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<LogisticCompanyTemplateItem> list = alading.LogisticCompanyTemplateItem.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public LogisticCompanyTemplateItem GetLogisticCompanyTemplateItem(string logisticcompanytemplateitemCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    ///*List<LogisticCompanyTemplateItem> list = alading.LogisticCompanyTemplateItem.Where(p => p.LogisticCompanyTemplateItemID == logisticcompanytemplateitemID).ToList();*/
                    //List<LogisticCompanyTemplateItem> list = alading.LogisticCompanyTemplateItem.Where(p => p.LogisticCompanyTemplateItemCode == logisticcompanytemplateitemCode).ToList();
                    //if (list.Count == 0)
                    //{
                    //    return null;
                    //}
                    //else
                    //{
                    //    return list.First();
                    //}
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<LogisticCompanyTemplateItem> GetLogisticCompanyTemplateItem(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                //using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                //{                    
                //    var ob = (from u in alading.LogisticCompanyTemplateItem orderby u.LogisticCompanyTemplateItemID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                //    rowCount = alading.LogisticCompanyTemplateItem.Count();
                //    return ob.ToList();
                //}
                rowCount = 0;
                return null;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<LogisticCompanyTemplateItem> GetLogisticCompanyTemplateItem(Func<LogisticCompanyTemplateItem, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    //IOrderedEnumerable<LogisticCompanyTemplateItem> list = alading.LogisticCompanyTemplateItem.Where(func).OrderByDescending(a=>a.LogisticCompanyTemplateItemID);
                    //rowCount = list.Count();
                    //return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    rowCount = 0;
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<LogisticCompanyTemplateItem> GetLogisticTemplateItems(string LogisticTemplateCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<LogisticCompanyTemplateItem> list = alading.LogisticCompanyTemplateItem.Where(p => p.LogisticCompanyTemplateCode == LogisticTemplateCode).ToList();
                    if (list.Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return list;
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}

