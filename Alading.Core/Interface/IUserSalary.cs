using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IUserSalary
    {
        ReturnType AddUserSalary(UserSalary usersalary);

        ReturnType AddUserSalary(List<UserSalary> usersalaryList);

        ReturnType RemoveAllUserSalary();

        ReturnType RemoveUserSalary(Func<UserSalary, bool> func);

        ReturnType RemoveUserSalary(string usersalaryCode);

        ReturnType RemoveUserSalary(List<string> usersalaryCodeList);

        ReturnType UpdateUserSalary(UserSalary usersalary);

        ReturnType UpdateUserSalary(string usersalaryCode, UserSalary usersalary);

        List<UserSalary> GetAllUserSalary();

        List<UserSalary> GetUserSalary(Func<UserSalary, bool> func);

        List<UserSalary> GetUserSalary(List<string> usersalaryCodeList);

        List<UserSalary> GetUserSalary(int pageIndex, int pageSize, out int rowCount);

        List<UserSalary> GetUserSalary(Func<UserSalary, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*
        ReturnType RemoveUserSalary(int usersalaryID);
        
        ReturnType RemoveUserSalary(List<int> usersalaryIDList);
        
        ReturnType UpdateUserSalary(int usersalaryID,UserSalary usersalary);
        
        List<UserSalary> GetUserSalary(List<int> usersalaryIDList);
        */
    }
}
