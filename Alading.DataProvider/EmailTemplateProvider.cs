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
        public ReturnType AddEmailTemplate(EmailTemplate emailtemplate)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToEmailTemplate(emailtemplate);
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
                
        public ReturnType AddEmailTemplate(List<EmailTemplate> emailtemplateList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (EmailTemplate emailtemplate in emailtemplateList)
                    {
                        alading.AddToEmailTemplate(emailtemplate);
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
       
        public ReturnType RemoveAllEmailTemplate()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<EmailTemplate> list = alading.EmailTemplate.ToList();
                    foreach (EmailTemplate emailtemplate in list)
                    {
                        alading.DeleteObject(emailtemplate);
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
       
        public ReturnType RemoveEmailTemplate(Func<EmailTemplate, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<EmailTemplate> list = alading.EmailTemplate.Where(func).ToList();
                    foreach (EmailTemplate emailtemplate in list)
                    {
                        alading.DeleteObject(emailtemplate);
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

        public List<EmailTemplate> GetEmailTemplate(List<string> emailtemplateCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.EmailTemplate.Where(BuildWhereInExpression<EmailTemplate, int>(v => v.EmailTemplateID, emailtemplateIDList));*/
                    var result = alading.EmailTemplate.Where(BuildWhereInExpression<EmailTemplate, string>(v => v.EmailTemplateCode, emailtemplateCodeList));
                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveEmailTemplate(List<string> emailtemplateCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.EmailTemplate.Where(BuildWhereInExpression<EmailTemplate, int>(v => v.EmailTemplateID, emailtemplateIDList));*/
                    var result = alading.EmailTemplate.Where(BuildWhereInExpression<EmailTemplate, string>(v => v.EmailTemplateCode, emailtemplateCodeList));
                    foreach (EmailTemplate s in result)
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
    
        public ReturnType RemoveEmailTemplate(string emailtemplateCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<EmailTemplate> list = alading.EmailTemplate.Where(p => p.EmailTemplateID == emailtemplateID).ToList();*/
                    List<EmailTemplate> list = alading.EmailTemplate.Where(p => p.EmailTemplateCode == emailtemplateCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                    else
                    {
                        EmailTemplate sy = list.First();
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
      
        public ReturnType UpdateEmailTemplate(EmailTemplate emailtemplate)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*EmailTemplate result = alading.EmailTemplate.Where(p => p.EmailTemplateID == emailtemplate.EmailTemplateID).FirstOrDefault();*/
                    EmailTemplate result = alading.EmailTemplate.Where(p => p.EmailTemplateCode == emailtemplate.EmailTemplateCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("EmailTemplate", emailtemplate);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                        result.EmailTemplateCode = emailtemplate.EmailTemplateCode;
                        result.Name = emailtemplate.Name;
                        result.Type = emailtemplate.Type;
                        result.Creator = emailtemplate.Creator;
                        result.Content = emailtemplate.Content;
                        result.CreationTime = emailtemplate.CreationTime;
			
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
       
        public ReturnType UpdateEmailTemplate(string emailtemplateCode, EmailTemplate emailtemplate)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.EmailTemplate.Where(p => p.EmailTemplateID == emailtemplateID).ToList();*/
                    var result = alading.EmailTemplate.Where(p => p.EmailTemplateCode == emailtemplateCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    EmailTemplate ob = result.First();
                    ob.EmailTemplateCode = emailtemplate.EmailTemplateCode;
                    ob.Name = emailtemplate.Name;
                    ob.Type = emailtemplate.Type;
                    ob.Creator = emailtemplate.Creator;
                    ob.Content = emailtemplate.Content;
                    ob.CreationTime = emailtemplate.CreationTime;
                    
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
     
        public List<EmailTemplate> GetAllEmailTemplate()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<EmailTemplate> list = alading.EmailTemplate.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<EmailTemplate> GetEmailTemplate(Func<EmailTemplate, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<EmailTemplate> list = alading.EmailTemplate.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public EmailTemplate GetEmailTemplate(string emailtemplateCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<EmailTemplate> list = alading.EmailTemplate.Where(p => p.EmailTemplateID == emailtemplateID).ToList();*/
                    List<EmailTemplate> list = alading.EmailTemplate.Where(p => p.EmailTemplateCode == emailtemplateCode).ToList();
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
        
        public List<EmailTemplate> GetEmailTemplate(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.EmailTemplate orderby u.EmailTemplateID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.EmailTemplate.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<EmailTemplate> GetEmailTemplate(Func<EmailTemplate, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<EmailTemplate> list = alading.EmailTemplate.Where(func).OrderByDescending(a=>a.EmailTemplateID);
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

