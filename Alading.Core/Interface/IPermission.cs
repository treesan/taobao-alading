using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IPermission
    {       
        ReturnType AddPermission(Permission permission);
       
        ReturnType AddPermission(List<Permission> permissionList);
        
        ReturnType RemoveAllPermission();
       
        ReturnType RemovePermission(Func<Permission, bool> func);
              
        ReturnType RemovePermission(string permissionCode);
        
        ReturnType RemovePermission(List<string> permissionCodeList);
       
        ReturnType UpdatePermission(Permission permission);
       
        ReturnType UpdatePermission(string permissionCode,Permission permission);
       
        List<Permission> GetAllPermission();
      
        List<Permission> GetPermission(Func<Permission, bool> func);
      
        List<Permission> GetPermission(List<string> permissionCodeList);
       
        List<Permission> GetPermission(int pageIndex, int pageSize, out int rowCount);
        
        List<Permission> GetPermission(Func<Permission, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemovePermission(int permissionID);
        
        ReturnType RemovePermission(List<int> permissionIDList);
        
        ReturnType UpdatePermission(int permissionID,Permission permission);
        
        List<Permission> GetPermission(List<int> permissionIDList);
        */
    }
}
