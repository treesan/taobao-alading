using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class RoleService
    {

        public static ReturnType AddRole(Role role)
        {
            return DataProviderClass.Instance().AddRole(role);
        }

        public static ReturnType AddRole(List<Role> roleList)
        {
            return DataProviderClass.Instance().AddRole(roleList);
        }
    
        public static ReturnType RemoveAllRole()
        {
            return DataProviderClass.Instance().RemoveAllRole();
        }
    
        public static ReturnType RemoveRole(Func<Role, bool> func)
        {
            return DataProviderClass.Instance().RemoveRole(func);
        }
        
        public static ReturnType RemoveRole(string roleCode)
        {
            return DataProviderClass.Instance().RemoveRole(roleCode);
        }       
        
        /*
        public static ReturnType RemoveRole(int roleID)
        {
            return DataProviderClass.Instance().RemoveRole(roleID);
        }
        */
    
        public static ReturnType RemoveRole(List<string> roleCodeList)
        {
            return DataProviderClass.Instance().RemoveRole(roleCodeList);
        }        
        
        /*
        public static ReturnType RemoveRole(List<int> roleIDList)
        {
            return DataProviderClass.Instance().RemoveRole(roleIDList);
        }
        */
    
        public static ReturnType UpdateRole(Role role)
        {
            return DataProviderClass.Instance().UpdateRole(role);
        }
    
        public static ReturnType UpdateRole(string roleCode, Role role)
        {
            return DataProviderClass.Instance().UpdateRole(roleCode, role);
        }
        
        /*
        public static ReturnType UpdateRole(int roleID, Role role)
        {
            return DataProviderClass.Instance().UpdateRole(roleID, role);
        }
        */
    
        public static List<Role> GetAllRole()
        {
            return DataProviderClass.Instance().GetAllRole();
        }
    
        public static List<Role> GetRole(Func<Role, bool> func)
        {
            return DataProviderClass.Instance().GetRole(func);
        }
    
        public static Role GetRole(string roleCode)
        {
            return DataProviderClass.Instance().GetRole(roleCode);
        }
        
        /*
        public static Role GetRole(int roleID)
        {
            return DataProviderClass.Instance().GetRole(roleID);
        }
        */
    
        public static List<Role> GetRole(List<string> roleCodeList)
        {
            return DataProviderClass.Instance().GetRole(roleCodeList);
        }
        
        /*
        public static List<Role> GetRole(List<int> roleIDList)
        {
            return DataProviderClass.Instance().GetRole(roleIDList);
        }
        */
    
        public static List<Role> GetRole(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetRole(pageIndex, pageSize, out rowCount);
        }
        
        public static List<Role> GetRole(Func<Role, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetRole(func, pageIndex, pageSize, out rowCount);
        }
    }
}
