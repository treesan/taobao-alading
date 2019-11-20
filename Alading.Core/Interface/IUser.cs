using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IUser
    {       
        ReturnType AddUser(User user);
       
        ReturnType AddUser(List<User> userList);
        
        ReturnType RemoveAllUser();
       
        ReturnType RemoveUser(Func<User, bool> func);
              
        ReturnType RemoveUser(string userCode);
        
        ReturnType RemoveUser(List<string> userCodeList);
       
        ReturnType UpdateUser(User user);

        ReturnType UpdateUser(User user, List<Role> roles, List<Shop> shops, List<StockHouse> houses);
       
        ReturnType UpdateUser(string userCode,User user);
       
        List<User> GetAllUser();
      
        List<User> GetUser(Func<User, bool> func);
      
        List<User> GetUser(List<string> userCodeList);
       
        List<User> GetUser(int pageIndex, int pageSize, out int rowCount);
        
        List<User> GetUser(Func<User, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveUser(int userID);
        
        ReturnType RemoveUser(List<int> userIDList);
        
        ReturnType UpdateUser(int userID,User user);
        
        List<User> GetUser(List<int> userIDList);
        */
    }
}
