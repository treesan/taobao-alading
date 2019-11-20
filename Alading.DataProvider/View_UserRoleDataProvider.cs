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
        public List<View_UserRole> GetView_UserRole(List<string> view_userroleCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.View_UserRole.Where(BuildWhereInExpression<View_UserRole, int>(v => v.View_UserRoleID, view_userroleIDList));*/
                    var result = alading.View_UserRole.Where(BuildWhereInExpression<View_UserRole, string>(v => v.RoleCode, view_userroleCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public List<View_UserRole> GetAllView_UserRole()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_UserRole> list = alading.View_UserRole.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_UserRole> GetView_UserRole(Func<View_UserRole, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_UserRole> list = alading.View_UserRole.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public View_UserRole GetView_UserRole(string view_userroleCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_UserRole> list = alading.View_UserRole.Where(p => p.UserCode == view_userroleCode).ToList();
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

        public List<View_UserRole> GetView_UserRole(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    var ob = (from u in alading.View_UserRole orderby u.UserID descending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    rowCount = alading.View_UserRole.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_UserRole> GetView_UserRole(Func<View_UserRole, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<View_UserRole> list = alading.View_UserRole.Where(func).OrderByDescending(a => a.UserID);
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

