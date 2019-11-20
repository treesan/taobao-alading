using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IRole
    {       
        ReturnType AddRole(Role role);
       
        ReturnType AddRole(List<Role> roleList);
        
        ReturnType RemoveAllRole();
       
        ReturnType RemoveRole(Func<Role, bool> func);
              
        ReturnType RemoveRole(string roleCode);
        
        ReturnType RemoveRole(List<string> roleCodeList);
       
        ReturnType UpdateRole(Role role);
       
        ReturnType UpdateRole(string roleCode,Role role);
       
        List<Role> GetAllRole();
      
        List<Role> GetRole(Func<Role, bool> func);
      
        List<Role> GetRole(List<string> roleCodeList);
       
        List<Role> GetRole(int pageIndex, int pageSize, out int rowCount);
        
        List<Role> GetRole(Func<Role, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveRole(int roleID);
        
        ReturnType RemoveRole(List<int> roleIDList);
        
        ReturnType UpdateRole(int roleID,Role role);
        
        List<Role> GetRole(List<int> roleIDList);
        */
    }
}
