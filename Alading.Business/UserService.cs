using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class UserService
    {

        public static ReturnType AddUser(User user)
        {
            return DataProviderClass.Instance().AddUser(user);
        }

        public static ReturnType AddUser(List<User> userList)
        {
            return DataProviderClass.Instance().AddUser(userList);
        }
    
        public static ReturnType RemoveAllUser()
        {
            return DataProviderClass.Instance().RemoveAllUser();
        }
    
        public static ReturnType RemoveUser(Func<User, bool> func)
        {
            return DataProviderClass.Instance().RemoveUser(func);
        }
        
        public static ReturnType RemoveUser(string userCode)
        {
            return DataProviderClass.Instance().RemoveUser(userCode);
        }       
        
        /*
        public static ReturnType RemoveUser(int userID)
        {
            return DataProviderClass.Instance().RemoveUser(userID);
        }
        */
    
        public static ReturnType RemoveUser(List<string> userCodeList)
        {
            return DataProviderClass.Instance().RemoveUser(userCodeList);
        }        
        
        /*
        public static ReturnType RemoveUser(List<int> userIDList)
        {
            return DataProviderClass.Instance().RemoveUser(userIDList);
        }
        */
    
        public static ReturnType UpdateUser(User user)
        {
            return DataProviderClass.Instance().UpdateUser(user);
        }

        public static ReturnType UpdateUser(User user, List<Role> roles, List<Shop> shops, List<StockHouse> houses)
        {
            return DataProviderClass.Instance().UpdateUser(user, roles, shops, houses);
        }
    
        public static ReturnType UpdateUser(string userCode, User user)
        {
            return DataProviderClass.Instance().UpdateUser(userCode, user);
        }
        
        /*
        public static ReturnType UpdateUser(int userID, User user)
        {
            return DataProviderClass.Instance().UpdateUser(userID, user);
        }
        */
    
        public static List<User> GetAllUser()
        {
            return DataProviderClass.Instance().GetAllUser();
        }
    
        public static List<User> GetUser(Func<User, bool> func)
        {
            return DataProviderClass.Instance().GetUser(func);
        }
    
        public static User GetUser(string userCode)
        {
            return DataProviderClass.Instance().GetUser(userCode);
        }
        
        /*
        public static User GetUser(int userID)
        {
            return DataProviderClass.Instance().GetUser(userID);
        }
        */
    
        public static List<User> GetUser(List<string> userCodeList)
        {
            return DataProviderClass.Instance().GetUser(userCodeList);
        }
        
        /*
        public static List<User> GetUser(List<int> userIDList)
        {
            return DataProviderClass.Instance().GetUser(userIDList);
        }
        */
    
        public static List<User> GetUser(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetUser(pageIndex, pageSize, out rowCount);
        }
        
        public static List<User> GetUser(Func<User, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetUser(func, pageIndex, pageSize, out rowCount);
        }
    }
}
