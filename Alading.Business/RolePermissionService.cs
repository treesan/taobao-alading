using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class RolePermissionService
    {

        public static ReturnType AddRolePermission(RolePermission rolepermission)
        {
            return DataProviderClass.Instance().AddRolePermission(rolepermission);
        }

        public static ReturnType AddRolePermission(List<RolePermission> rolepermissionList)
        {
            return DataProviderClass.Instance().AddRolePermission(rolepermissionList);
        }
    
        public static ReturnType RemoveAllRolePermission()
        {
            return DataProviderClass.Instance().RemoveAllRolePermission();
        }
    
        public static ReturnType RemoveRolePermission(Func<RolePermission, bool> func)
        {
            return DataProviderClass.Instance().RemoveRolePermission(func);
        }
        
        public static ReturnType RemoveRolePermission(string rolepermissionCode)
        {
            return DataProviderClass.Instance().RemoveRolePermission(rolepermissionCode);
        }       
        
        /*
        public static ReturnType RemoveRolePermission(int rolepermissionID)
        {
            return DataProviderClass.Instance().RemoveRolePermission(rolepermissionID);
        }
        */
    
        public static ReturnType RemoveRolePermission(List<string> rolepermissionCodeList)
        {
            return DataProviderClass.Instance().RemoveRolePermission(rolepermissionCodeList);
        }        
        
        /*
        public static ReturnType RemoveRolePermission(List<int> rolepermissionIDList)
        {
            return DataProviderClass.Instance().RemoveRolePermission(rolepermissionIDList);
        }
        */
    
        public static ReturnType UpdateRolePermission(RolePermission rolepermission)
        {
            return DataProviderClass.Instance().UpdateRolePermission(rolepermission);
        }
    
        public static ReturnType UpdateRolePermission(string rolepermissionCode, RolePermission rolepermission)
        {
            return DataProviderClass.Instance().UpdateRolePermission(rolepermissionCode, rolepermission);
        }
        
        /*
        public static ReturnType UpdateRolePermission(int rolepermissionID, RolePermission rolepermission)
        {
            return DataProviderClass.Instance().UpdateRolePermission(rolepermissionID, rolepermission);
        }
        */
    
        public static List<RolePermission> GetAllRolePermission()
        {
            return DataProviderClass.Instance().GetAllRolePermission();
        }
    
        public static List<RolePermission> GetRolePermission(Func<RolePermission, bool> func)
        {
            return DataProviderClass.Instance().GetRolePermission(func);
        }
    
        public static RolePermission GetRolePermission(string rolepermissionCode)
        {
            return DataProviderClass.Instance().GetRolePermission(rolepermissionCode);
        }
        
        /*
        public static RolePermission GetRolePermission(int rolepermissionID)
        {
            return DataProviderClass.Instance().GetRolePermission(rolepermissionID);
        }
        */
    
        public static List<RolePermission> GetRolePermission(List<string> rolepermissionCodeList)
        {
            return DataProviderClass.Instance().GetRolePermission(rolepermissionCodeList);
        }
        
        /*
        public static List<RolePermission> GetRolePermission(List<int> rolepermissionIDList)
        {
            return DataProviderClass.Instance().GetRolePermission(rolepermissionIDList);
        }
        */
    
        public static List<RolePermission> GetRolePermission(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetRolePermission(pageIndex, pageSize, out rowCount);
        }
        
        public static List<RolePermission> GetRolePermission(Func<RolePermission, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetRolePermission(func, pageIndex, pageSize, out rowCount);
        }

        public static SortedList<string, List<Permission>> GetPermissionList(string roleCode, out int roleType)
        {
            return DataProviderClass.Instance().GetPermissionList(roleCode,out roleType);
        }

        public static List<string> GetSelectPermissionList(string RoleCode)
        {
            return DataProviderClass.Instance().GetSelectPermissionList(RoleCode);
        }

        public static ReturnType RemoveAndAddRolePermission(string roleCode, List<RolePermission> addRolePerList)
        {
            return DataProviderClass.Instance().RemoveAndAddRolePermission(roleCode, addRolePerList);
        }
    }
}
