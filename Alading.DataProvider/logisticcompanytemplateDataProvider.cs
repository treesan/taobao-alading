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
     
        public ReturnType AddLogisticCompanyTemplate(LogisticCompanyTemplate logisticcompanytemplate)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToLogisticCompanyTemplate(logisticcompanytemplate);
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
                
        public ReturnType AddLogisticCompanyTemplate(List<LogisticCompanyTemplate> logisticcompanytemplateList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (LogisticCompanyTemplate logisticcompanytemplate in logisticcompanytemplateList)
                    {
                        alading.AddToLogisticCompanyTemplate(logisticcompanytemplate);
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
       
        public ReturnType RemoveAllLogisticCompanyTemplate()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<LogisticCompanyTemplate> list = alading.LogisticCompanyTemplate.ToList();
                    foreach (LogisticCompanyTemplate logisticcompanytemplate in list)
                    {
                        alading.DeleteObject(logisticcompanytemplate);
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
       
        public ReturnType RemoveLogisticCompanyTemplate(Func<LogisticCompanyTemplate, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<LogisticCompanyTemplate> list = alading.LogisticCompanyTemplate.Where(func).ToList();
                    foreach (LogisticCompanyTemplate logisticcompanytemplate in list)
                    {
                        alading.DeleteObject(logisticcompanytemplate);
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

        public List<LogisticCompanyTemplate> GetLogisticCompanyTemplate(List<string> logisticcompanytemplateCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.LogisticCompanyTemplate.Where(BuildWhereInExpression<LogisticCompanyTemplate, int>(v => v.LogisticCompanyTemplateID, logisticcompanytemplateIDList));*/
                    var result = alading.LogisticCompanyTemplate.Where(BuildWhereInExpression<LogisticCompanyTemplate, string>(v => v.LogisticCompanyCode, logisticcompanytemplateCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveLogisticCompanyTemplate(List<string> logisticcompanytemplateCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.LogisticCompanyTemplate.Where(BuildWhereInExpression<LogisticCompanyTemplate, int>(v => v.LogisticCompanyTemplateID, logisticcompanytemplateIDList));*/
                    var result = alading.LogisticCompanyTemplate.Where(BuildWhereInExpression<LogisticCompanyTemplate, string>(v => v.LogisticCompanyCode, logisticcompanytemplateCodeList));
                    foreach (LogisticCompanyTemplate s in result)
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

    
        public ReturnType RemoveLogisticCompanyTemplate(string logisticcompanytemplateCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<LogisticCompanyTemplate> list = alading.LogisticCompanyTemplate.Where(p => p.LogisticCompanyTemplateID == logisticcompanytemplateID).ToList();*/
                    List<LogisticCompanyTemplate> list = alading.LogisticCompanyTemplate.Where(p => p.LogisticCompanyCode == logisticcompanytemplateCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        LogisticCompanyTemplate sy = list.First();
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
      
        public ReturnType UpdateLogisticCompanyTemplate(LogisticCompanyTemplate logisticcompanytemplate)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*LogisticCompanyTemplate result = alading.LogisticCompanyTemplate.Where(p => p.LogisticCompanyTemplateID == logisticcompanytemplate.LogisticCompanyTemplateID).FirstOrDefault();*/
                    LogisticCompanyTemplate result = alading.LogisticCompanyTemplate.Where(p => p.LogisticCompanyCode == logisticcompanytemplate.LogisticCompanyCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    //alading.Attach(result);
                    //alading.ApplyPropertyChanges("LogisticCompanyTemplate", logisticcompanytemplate);

                    //Article oldArticle = mixEntity.Article.FirstOrDefault(cc => cc.ArticleCode == article.ArticleCode);
                    //mixEntity.Attach(oldArticle);
                    //mixEntity.ApplyPropertyChanges("Article", article);
                    //return (mixEntity.SaveChanges() == 1);

                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse



                    result.TemplateCode = logisticcompanytemplate.TemplateCode;

                    result.TemplateName = logisticcompanytemplate.TemplateName;

                    result.LogisticCompanyCode = logisticcompanytemplate.LogisticCompanyCode;

                    result.LogisticCompanyName = logisticcompanytemplate.LogisticCompanyName;

                    result.CoverAreaList = logisticcompanytemplate.CoverAreaList;

                    result.TemplateData = logisticcompanytemplate.TemplateData;

                    result.PaperWidth = logisticcompanytemplate.PaperWidth;

                    result.PaperHeight = logisticcompanytemplate.PaperHeight;

                    result.TemplateLeftOffset = logisticcompanytemplate.TemplateLeftOffset;

                    result.TemplateRightOffset = logisticcompanytemplate.TemplateRightOffset;

                    result.TemplateTopOffset = logisticcompanytemplate.TemplateTopOffset;

                    result.TemplateBottomOffset = logisticcompanytemplate.TemplateBottomOffset;

                    result.Landscape = logisticcompanytemplate.Landscape;

                    result.DefaultPrinter = logisticcompanytemplate.DefaultPrinter;
                    
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
       
        public ReturnType UpdateLogisticCompanyTemplate(string logisticcompanytemplateCode, LogisticCompanyTemplate logisticcompanytemplate)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.LogisticCompanyTemplate.Where(p => p.LogisticCompanyTemplateID == logisticcompanytemplateID).ToList();*/
                    var result = alading.LogisticCompanyTemplate.Where(p => p.LogisticCompanyCode == logisticcompanytemplateCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    LogisticCompanyTemplate ob = result.First();
                    ob.TemplateCode = logisticcompanytemplate.TemplateCode;
                    ob.TemplateName = logisticcompanytemplate.TemplateName;
                    ob.LogisticCompanyCode = logisticcompanytemplate.LogisticCompanyCode;
                    ob.LogisticCompanyName = logisticcompanytemplate.LogisticCompanyName;
                    ob.CoverAreaList = logisticcompanytemplate.CoverAreaList;
                    ob.TemplateData = logisticcompanytemplate.TemplateData;
                    ob.PaperWidth = logisticcompanytemplate.PaperWidth;
                    ob.PaperHeight = logisticcompanytemplate.PaperHeight;
                    ob.TemplateLeftOffset = logisticcompanytemplate.TemplateLeftOffset;
                    ob.TemplateRightOffset = logisticcompanytemplate.TemplateRightOffset;
                    ob.TemplateTopOffset = logisticcompanytemplate.TemplateTopOffset;
                    ob.TemplateBottomOffset = logisticcompanytemplate.TemplateBottomOffset;
                    ob.Landscape = logisticcompanytemplate.Landscape;
                    ob.DefaultPrinter = logisticcompanytemplate.DefaultPrinter;
                    
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
     
        public List<LogisticCompanyTemplate> GetAllLogisticCompanyTemplate()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<LogisticCompanyTemplate> list = alading.LogisticCompanyTemplate.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<LogisticCompanyTemplate> GetLogisticCompanyTemplate(Func<LogisticCompanyTemplate, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<LogisticCompanyTemplate> list = alading.LogisticCompanyTemplate.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public LogisticCompanyTemplate GetLogisticTemplate(string logistiCompanyTemplateCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<LogisticCompanyTemplate> list = alading.LogisticCompanyTemplate.Where(p => p.LogisticCompanyCode == logistiCompanyTemplateCode).ToList();
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
        
        public List<LogisticCompanyTemplate> GetLogisticCompanyTemplate(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.LogisticCompanyTemplate orderby u.TemplateID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.LogisticCompanyTemplate.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<LogisticCompanyTemplate> GetLogisticCompanyTemplate(Func<LogisticCompanyTemplate, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<LogisticCompanyTemplate> list = alading.LogisticCompanyTemplate.Where(func).OrderByDescending(a=>a.TemplateID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<LogisticCompanyTemplate> GetLogisticCompanyTemplate(string LogisticCompanyCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<LogisticCompanyTemplate> list = alading.LogisticCompanyTemplate.Where(p => p.LogisticCompanyTemplateID == logisticcompanytemplateID).ToList();*/
                    List<LogisticCompanyTemplate> list = alading.LogisticCompanyTemplate.Where(p => p.LogisticCompanyCode == LogisticCompanyCode).ToList();
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

