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
        public List<View_LogisticCompanyTemplate> GetView_LogisticCompanyTemplate(List<string> view_logisticcompanytemplateCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities())
                {
					/*var result = alading.View_LogisticCompanyTemplate.Where(BuildWhereInExpression<View_LogisticCompanyTemplate, int>(v => v.View_LogisticCompanyTemplateID, view_logisticcompanytemplateIDList));*/
                    var result = alading.View_LogisticCompanyTemplate.Where(BuildWhereInExpression<View_LogisticCompanyTemplate, string>(v => v.LogisticCompanyCode, view_logisticcompanytemplateCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }        
     
        public List<View_LogisticCompanyTemplate> GetAllView_LogisticCompanyTemplate()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities())
                {
                    List<View_LogisticCompanyTemplate> list = alading.View_LogisticCompanyTemplate.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<View_LogisticCompanyTemplate> GetView_LogisticCompanyTemplate(Func<View_LogisticCompanyTemplate, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities())
                {
                    List<View_LogisticCompanyTemplate> list = alading.View_LogisticCompanyTemplate.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public View_LogisticCompanyTemplate GetView_LogisticCompanyTemplate(string view_logisticcompanytemplateCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities())
                {
                    List<View_LogisticCompanyTemplate> list = alading.View_LogisticCompanyTemplate.Where(p => p.LogisticCompanyCode == view_logisticcompanytemplateCode).ToList();
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
        
        public List<View_LogisticCompanyTemplate> GetView_LogisticCompanyTemplate(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities())
                {
                    
                    //var ob = (from u in alading.View_LogisticCompanyTemplate orderby u. descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    //rowCount = alading.View_LogisticCompanyTemplate.Count();
                    rowCount = 0;
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<View_LogisticCompanyTemplate> GetView_LogisticCompanyTemplate(Func<View_LogisticCompanyTemplate, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
        //    try
        //    {
        //        using (AladingEntities alading = new AladingEntities())
        //        {
        //            //IOrderedEnumerable<View_LogisticCompanyTemplate> list = alading.View_LogisticCompanyTemplate.Where(func).OrderByDescending(a=>a.View_LogisticCompanyTemplateID);
        //            //rowCount = list.Count();
        //            //return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        //            return null;
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ex;
        //    }
             rowCount = 0;
            return new List<View_LogisticCompanyTemplate>() ;
        }        
    }
}

