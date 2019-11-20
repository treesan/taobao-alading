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
        public ReturnType AddEmailTemplateCat(EmailTemplateCat emailtemplatecat)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToEmailTemplateCat(emailtemplatecat);
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

        public ReturnType AddEmailTemplateCat(List<EmailTemplateCat> emailtemplatecatList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (EmailTemplateCat emailtemplatecat in emailtemplatecatList)
                    {
                        alading.AddToEmailTemplateCat(emailtemplatecat);
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

        public ReturnType RemoveAllEmailTemplateCat()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<EmailTemplateCat> list = alading.EmailTemplateCat.ToList();
                    foreach (EmailTemplateCat emailtemplatecat in list)
                    {
                        alading.DeleteObject(emailtemplatecat);
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

        public ReturnType RemoveEmailTemplateCat(Func<EmailTemplateCat, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<EmailTemplateCat> list = alading.EmailTemplateCat.Where(func).ToList();
                    foreach (EmailTemplateCat emailtemplatecat in list)
                    {
                        alading.DeleteObject(emailtemplatecat);
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

        public List<EmailTemplateCat> GetEmailTemplateCat(List<string> emailtemplatecatCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.EmailTemplateCat.Where(BuildWhereInExpression<EmailTemplateCat, int>(v => v.EmailTemplateCatID, emailtemplatecatIDList));*/
                    var result = alading.EmailTemplateCat.Where(BuildWhereInExpression<EmailTemplateCat, string>(v => v.Code, emailtemplatecatCodeList));
                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveEmailTemplateCat(List<string> emailtemplatecatCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.EmailTemplateCat.Where(BuildWhereInExpression<EmailTemplateCat, int>(v => v.EmailTemplateCatID, emailtemplatecatIDList));*/
                    var result = alading.EmailTemplateCat.Where(BuildWhereInExpression<EmailTemplateCat, string>(v => v.Code, emailtemplatecatCodeList));
                    foreach (EmailTemplateCat s in result)
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

        public ReturnType RemoveEmailTemplateCat(string emailtemplatecatCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<EmailTemplateCat> list = alading.EmailTemplateCat.Where(p => p.EmailTemplateCatID == emailtemplatecatID).ToList();*/
                    List<EmailTemplateCat> list = alading.EmailTemplateCat.Where(p => p.Code == emailtemplatecatCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                    else
                    {
                        EmailTemplateCat sy = list.First();

                        //remove all template in the category
                        if (sy != null)
                        {
                            var query = alading.EmailTemplate.Where(c => c.Type == sy.Code);
                            foreach (var i in query)
                            {
                                alading.DeleteObject(i);
                            }
                        }

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

        public ReturnType UpdateEmailTemplateCat(EmailTemplateCat emailtemplatecat)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*EmailTemplateCat result = alading.EmailTemplateCat.Where(p => p.EmailTemplateCatID == emailtemplatecat.EmailTemplateCatID).FirstOrDefault();*/
                    EmailTemplateCat result = alading.EmailTemplateCat.Where(p => p.Code == emailtemplatecat.Code).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("EmailTemplateCat", emailtemplatecat);
                    #endregion

                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                        result.Code = emailtemplatecat.Code;
                        result.Name = emailtemplatecat.Name;
			
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

        public ReturnType UpdateEmailTemplateCat(string emailtemplatecatCode, EmailTemplateCat emailtemplatecat)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.EmailTemplateCat.Where(p => p.EmailTemplateCatID == emailtemplatecatID).ToList();*/
                    var result = alading.EmailTemplateCat.Where(p => p.Code == emailtemplatecatCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    EmailTemplateCat ob = result.First();
                    ob.Code = emailtemplatecat.Code;
                    ob.Name = emailtemplatecat.Name;

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

        public List<EmailTemplateCat> GetAllEmailTemplateCat()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<EmailTemplateCat> list = alading.EmailTemplateCat.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<EmailTemplateCat> GetEmailTemplateCat(Func<EmailTemplateCat, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<EmailTemplateCat> list = alading.EmailTemplateCat.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public EmailTemplateCat GetEmailTemplateCat(string emailtemplatecatCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<EmailTemplateCat> list = alading.EmailTemplateCat.Where(p => p.EmailTemplateCatID == emailtemplatecatID).ToList();*/
                    List<EmailTemplateCat> list = alading.EmailTemplateCat.Where(p => p.Code == emailtemplatecatCode).ToList();
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

        public List<EmailTemplateCat> GetEmailTemplateCat(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var ob = (from u in alading.EmailTemplateCat orderby u.ID descending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    rowCount = alading.EmailTemplateCat.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<EmailTemplateCat> GetEmailTemplateCat(Func<EmailTemplateCat, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<EmailTemplateCat> list = alading.EmailTemplateCat.Where(func).OrderByDescending(a => a.ID);
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

