using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IRolePermission
    {       
        ReturnType AddRolePermission(RolePermission rolepermission);
       
        ReturnType AddRolePermission(List<RolePermission> rolepermissionList);
        
        ReturnType RemoveAllRolePermission();
       
        ReturnType RemoveRolePermission(Func<RolePermission, bool> func);
              
        ReturnType RemoveRolePermission(string rolepermissionCode);
        
        ReturnType RemoveRolePermission(List<string> rolepermissionCodeList);
       
        ReturnType UpdateRolePermission(RolePermission rolepermission);
       
        ReturnType UpdateRolePermission(string rolepermissionCode,RolePermission rolepermission);
       
        List<RolePermission> GetAllRolePermission();
      
        List<RolePermission> GetRolePermission(Func<RolePermission, bool> func);
      
        List<RolePermission> GetRolePermission(List<string> rolepermissionCodeList);
       
        List<RolePermission> GetRolePermission(int pageIndex, int pageSize, out int rowCount);
        
        List<RolePermission> GetRolePermission(Func<RolePermission, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveRolePermission(int rolepermissionID);
        
        ReturnType RemoveRolePermission(List<int> rolepermissionIDList);
        
        ReturnType UpdateRolePermission(int rolepermissionID,RolePermission rolepermission);
        
        List<RolePermission> GetRolePermission(List<int> rolepermissionIDList);
        */
    }
}
