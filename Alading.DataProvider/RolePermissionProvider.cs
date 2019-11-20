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
     
        public ReturnType AddRolePermission(RolePermission rolepermission)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToRolePermission(rolepermission);
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
                
        public ReturnType AddRolePermission(List<RolePermission> rolepermissionList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (RolePermission rolepermission in rolepermissionList)
                    {
                        alading.AddToRolePermission(rolepermission);
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
       
        public ReturnType RemoveAllRolePermission()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<RolePermission> list = alading.RolePermission.ToList();
                    foreach (RolePermission rolepermission in list)
                    {
                        alading.DeleteObject(rolepermission);
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
       
        public ReturnType RemoveRolePermission(Func<RolePermission, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<RolePermission> list = alading.RolePermission.Where(func).ToList();
                    foreach (RolePermission rolepermission in list)
                    {
                        alading.DeleteObject(rolepermission);
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

        public List<RolePermission> GetRolePermission(List<string> rolepermissionCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.RolePermission.Where(BuildWhereInExpression<RolePermission, int>(v => v.RolePermissionID, rolepermissionIDList));*/
                    var result = alading.RolePermission.Where(BuildWhereInExpression<RolePermission, string>(v => v.RolePermissionCode, rolepermissionCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveRolePermission(List<string> rolepermissionCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.RolePermission.Where(BuildWhereInExpression<RolePermission, int>(v => v.RolePermissionID, rolepermissionIDList));*/
                    var result = alading.RolePermission.Where(BuildWhereInExpression<RolePermission, string>(v => v.RolePermissionCode, rolepermissionCodeList));
                    foreach (RolePermission s in result)
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

    
        public ReturnType RemoveRolePermission(string rolepermissionCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<RolePermission> list = alading.RolePermission.Where(p => p.RolePermissionID == rolepermissionID).ToList();*/
                    List<RolePermission> list = alading.RolePermission.Where(p => p.RolePermissionCode == rolepermissionCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        RolePermission sy = list.First();
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
      
        public ReturnType UpdateRolePermission(RolePermission rolepermission)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*RolePermission result = alading.RolePermission.Where(p => p.RolePermissionID == rolepermission.RolePermissionID).FirstOrDefault();*/
                    RolePermission result = alading.RolePermission.Where(p => p.RolePermissionCode == rolepermission.RolePermissionCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("RolePermission", rolepermission);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.PermissionCode = rolepermission.PermissionCode;
                    
                        result.RoleCode = rolepermission.RoleCode;
			
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
       
        public ReturnType UpdateRolePermission(string rolepermissionCode, RolePermission rolepermission)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.RolePermission.Where(p => p.RolePermissionID == rolepermissionID).ToList();*/
                    var result = alading.RolePermission.Where(p => p.RolePermissionCode == rolepermissionCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    RolePermission ob = result.First();
                    ob.PermissionCode = rolepermission.PermissionCode;
                    ob.RoleCode = rolepermission.RoleCode;
                    
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
     
        public List<RolePermission> GetAllRolePermission()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<RolePermission> list = alading.RolePermission.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<RolePermission> GetRolePermission(Func<RolePermission, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<RolePermission> list = alading.RolePermission.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public RolePermission GetRolePermission(string rolepermissionCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<RolePermission> list = alading.RolePermission.Where(p => p.RolePermissionID == rolepermissionID).ToList();*/
                    List<RolePermission> list = alading.RolePermission.Where(p => p.RolePermissionCode == rolepermissionCode).ToList();
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
        
        public List<RolePermission> GetRolePermission(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.RolePermission orderby u.RolePermissionID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.RolePermission.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<RolePermission> GetRolePermission(Func<RolePermission, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<RolePermission> list = alading.RolePermission.Where(func).OrderByDescending(a=>a.RolePermissionID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取角色所有权限列表
        /// </summary>
        /// <param name="roleType"></param>
        /// <returns></returns>
        public SortedList<string, List<Permission>> GetPermissionList(string roleCode, out int RoleType)
        {
            try
            {
                SortedList<string, List<Permission>> perSortedList = new SortedList<string, List<Permission>>();
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    Role role = alading.Role.Where(c => c.RoleCode == roleCode).FirstOrDefault();
                    //不存在Role
                    if (role == null)
                    {
                        RoleType = 0;
                        return null;
                    }
                    RoleType = role.RoleType;

                    //加载第一级
                    List<Permission> FirstPerlist = new List<Permission>();
                    foreach (Alading.Entity.Permission firstPer in alading.Permission.Where(c => c.RoleType == role.RoleType 
                        && c.PermissionCode.Length == 4).OrderBy(p=>p.PermissionOrder))
                    {
                        FirstPerlist.Add(firstPer);
                       
                        //加载第二级 根据RoleType 和 父类的PermissionCode
                        List<Permission> secountPerlist = new List<Permission>();
                        foreach (Alading.Entity.Permission secountPer in alading.Permission.Where(c => c.RoleType == role.RoleType
                            && c.PermissionCode.StartsWith(firstPer.PermissionCode) && c.PermissionCode.Length == 9).OrderBy(p => p.PermissionOrder))
                        {
                            secountPerlist.Add(secountPer);

                            //加载第三级
                            List<Permission> thirdPerlist = new List<Permission>();
                            foreach (Alading.Entity.Permission thirdPer in alading.Permission.Where(c => c.RoleType == role.RoleType
                            && c.PermissionCode.StartsWith(secountPer.PermissionCode) && c.PermissionCode.Length == 14).OrderBy(p => p.PermissionOrder))
                            {
                                thirdPerlist.Add(thirdPer);
                            }
                            //第三级的key为第二级的PermissionCode
                            perSortedList.Add(secountPer.PermissionCode, thirdPerlist);

                        }
                        //第二级的key为第一级的PermissionCode
                        perSortedList.Add(firstPer.PermissionCode, secountPerlist);

                    }
                    //第一级的key为string.empty
                    perSortedList.Add(string.Empty, FirstPerlist);
                    
                }
                return perSortedList;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取已勾选中的权限
        /// </summary>
        /// <param name="RoleCode"></param>
        public List<string> GetSelectPermissionList(string RoleCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<string> roleProList = new List<string>();

                    foreach (RolePermission rolePromission in alading.RolePermission.Where(c => c.RoleCode == RoleCode))
                    {
                        roleProList.Add(rolePromission.PermissionCode);
                    }
                    return roleProList;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType RemoveAndAddRolePermission(string roleCode,List<RolePermission> addRolePerList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    //删除
                    var result = alading.RolePermission.Where(c => c.RoleCode == roleCode);
                    foreach (RolePermission s in result.ToList())
                    {
                        alading.DeleteObject(s);
                    }

                    //添加
                    foreach (RolePermission rolepermission in addRolePerList)
                    {
                        alading.AddToRolePermission(rolepermission);
                    }
                    alading.SaveChanges();
                }
                return ReturnType.Success;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}

