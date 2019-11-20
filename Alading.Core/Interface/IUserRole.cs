using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IUserRole
    {       
        ReturnType AddUserRole(UserRole userrole);
       
        ReturnType AddUserRole(List<UserRole> userroleList);
        
        ReturnType RemoveAllUserRole();
       
        ReturnType RemoveUserRole(Func<UserRole, bool> func);
              
        ReturnType RemoveUserRole(string userroleCode);
        
        ReturnType RemoveUserRole(List<string> userroleCodeList);
       
        ReturnType UpdateUserRole(UserRole userrole);
       
        ReturnType UpdateUserRole(string userroleCode,UserRole userrole);
       
        List<UserRole> GetAllUserRole();
      
        List<UserRole> GetUserRole(Func<UserRole, bool> func);
      
        List<UserRole> GetUserRole(List<string> userroleCodeList);
       
        List<UserRole> GetUserRole(int pageIndex, int pageSize, out int rowCount);
        
        List<UserRole> GetUserRole(Func<UserRole, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveUserRole(int userroleID);
        
        ReturnType RemoveUserRole(List<int> userroleIDList);
        
        ReturnType UpdateUserRole(int userroleID,UserRole userrole);
        
        List<UserRole> GetUserRole(List<int> userroleIDList);
        */
    }
}
