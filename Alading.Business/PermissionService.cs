using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class PermissionService
    {

        public static ReturnType AddPermission(Permission permission)
        {
            return DataProviderClass.Instance().AddPermission(permission);
        }

        public static ReturnType AddPermission(List<Permission> permissionList)
        {
            return DataProviderClass.Instance().AddPermission(permissionList);
        }
    
        public static ReturnType RemoveAllPermission()
        {
            return DataProviderClass.Instance().RemoveAllPermission();
        }
    
        public static ReturnType RemovePermission(Func<Permission, bool> func)
        {
            return DataProviderClass.Instance().RemovePermission(func);
        }
        
        public static ReturnType RemovePermission(string permissionCode)
        {
            return DataProviderClass.Instance().RemovePermission(permissionCode);
        }       
        
        /*
        public static ReturnType RemovePermission(int permissionID)
        {
            return DataProviderClass.Instance().RemovePermission(permissionID);
        }
        */
    
        public static ReturnType RemovePermission(List<string> permissionCodeList)
        {
            return DataProviderClass.Instance().RemovePermission(permissionCodeList);
        }        
        
        /*
        public static ReturnType RemovePermission(List<int> permissionIDList)
        {
            return DataProviderClass.Instance().RemovePermission(permissionIDList);
        }
        */
    
        public static ReturnType UpdatePermission(Permission permission)
        {
            return DataProviderClass.Instance().UpdatePermission(permission);
        }
    
        public static ReturnType UpdatePermission(string permissionCode, Permission permission)
        {
            return DataProviderClass.Instance().UpdatePermission(permissionCode, permission);
        }
        
        /*
        public static ReturnType UpdatePermission(int permissionID, Permission permission)
        {
            return DataProviderClass.Instance().UpdatePermission(permissionID, permission);
        }
        */
    
        public static List<Permission> GetAllPermission()
        {
            return DataProviderClass.Instance().GetAllPermission();
        }
    
        public static List<Permission> GetPermission(Func<Permission, bool> func)
        {
            return DataProviderClass.Instance().GetPermission(func);
        }
    
        public static Permission GetPermission(string permissionCode)
        {
            return DataProviderClass.Instance().GetPermission(permissionCode);
        }
        
        /*
        public static Permission GetPermission(int permissionID)
        {
            return DataProviderClass.Instance().GetPermission(permissionID);
        }
        */
    
        public static List<Permission> GetPermission(List<string> permissionCodeList)
        {
            return DataProviderClass.Instance().GetPermission(permissionCodeList);
        }
        
        /*
        public static List<Permission> GetPermission(List<int> permissionIDList)
        {
            return DataProviderClass.Instance().GetPermission(permissionIDList);
        }
        */
    
        public static List<Permission> GetPermission(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetPermission(pageIndex, pageSize, out rowCount);
        }
        
        public static List<Permission> GetPermission(Func<Permission, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetPermission(func, pageIndex, pageSize, out rowCount);
        }
    }
}
