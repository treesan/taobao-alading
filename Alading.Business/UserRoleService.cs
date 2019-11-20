using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class UserRoleService
    {

        public static ReturnType AddUserRole(UserRole userrole)
        {
            return DataProviderClass.Instance().AddUserRole(userrole);
        }

        public static ReturnType AddUserRole(List<UserRole> userroleList)
        {
            return DataProviderClass.Instance().AddUserRole(userroleList);
        }
    
        public static ReturnType RemoveAllUserRole()
        {
            return DataProviderClass.Instance().RemoveAllUserRole();
        }
    
        public static ReturnType RemoveUserRole(Func<UserRole, bool> func)
        {
            return DataProviderClass.Instance().RemoveUserRole(func);
        }
        
        public static ReturnType RemoveUserRole(string userroleCode)
        {
            return DataProviderClass.Instance().RemoveUserRole(userroleCode);
        }       
        
        /*
        public static ReturnType RemoveUserRole(int userroleID)
        {
            return DataProviderClass.Instance().RemoveUserRole(userroleID);
        }
        */
    
        public static ReturnType RemoveUserRole(List<string> userroleCodeList)
        {
            return DataProviderClass.Instance().RemoveUserRole(userroleCodeList);
        }        
        
        /*
        public static ReturnType RemoveUserRole(List<int> userroleIDList)
        {
            return DataProviderClass.Instance().RemoveUserRole(userroleIDList);
        }
        */
    
        public static ReturnType UpdateUserRole(UserRole userrole)
        {
            return DataProviderClass.Instance().UpdateUserRole(userrole);
        }
    
        public static ReturnType UpdateUserRole(string userroleCode, UserRole userrole)
        {
            return DataProviderClass.Instance().UpdateUserRole(userroleCode, userrole);
        }
        
        /*
        public static ReturnType UpdateUserRole(int userroleID, UserRole userrole)
        {
            return DataProviderClass.Instance().UpdateUserRole(userroleID, userrole);
        }
        */
    
        public static List<UserRole> GetAllUserRole()
        {
            return DataProviderClass.Instance().GetAllUserRole();
        }
    
        public static List<UserRole> GetUserRole(Func<UserRole, bool> func)
        {
            return DataProviderClass.Instance().GetUserRole(func);
        }
    
        public static UserRole GetUserRole(string userroleCode)
        {
            return DataProviderClass.Instance().GetUserRole(userroleCode);
        }
        
        /*
        public static UserRole GetUserRole(int userroleID)
        {
            return DataProviderClass.Instance().GetUserRole(userroleID);
        }
        */
    
        public static List<UserRole> GetUserRole(List<string> userroleCodeList)
        {
            return DataProviderClass.Instance().GetUserRole(userroleCodeList);
        }
        
        /*
        public static List<UserRole> GetUserRole(List<int> userroleIDList)
        {
            return DataProviderClass.Instance().GetUserRole(userroleIDList);
        }
        */
    
        public static List<UserRole> GetUserRole(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetUserRole(pageIndex, pageSize, out rowCount);
        }
        
        public static List<UserRole> GetUserRole(Func<UserRole, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetUserRole(func, pageIndex, pageSize, out rowCount);
        }
    }
}
