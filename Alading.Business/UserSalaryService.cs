using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class UserSalaryService
    {

        public static ReturnType AddUserSalary(UserSalary usersalary)
        {
            return DataProviderClass.Instance().AddUserSalary(usersalary);
        }

        public static ReturnType AddUserSalary(List<UserSalary> usersalaryList)
        {
            return DataProviderClass.Instance().AddUserSalary(usersalaryList);
        }

        public static ReturnType RemoveAllUserSalary()
        {
            return DataProviderClass.Instance().RemoveAllUserSalary();
        }

        public static ReturnType RemoveUserSalary(Func<UserSalary, bool> func)
        {
            return DataProviderClass.Instance().RemoveUserSalary(func);
        }

        public static ReturnType RemoveUserSalary(string usersalaryCode)
        {
            return DataProviderClass.Instance().RemoveUserSalary(usersalaryCode);
        }

        /*
        public static ReturnType RemoveUserSalary(int usersalaryID)
        {
            return DataProviderClass.Instance().RemoveUserSalary(usersalaryID);
        }
        */

        public static ReturnType RemoveUserSalary(List<string> usersalaryCodeList)
        {
            return DataProviderClass.Instance().RemoveUserSalary(usersalaryCodeList);
        }

        /*
        public static ReturnType RemoveUserSalary(List<int> usersalaryIDList)
        {
            return DataProviderClass.Instance().RemoveUserSalary(usersalaryIDList);
        }
        */

        public static ReturnType UpdateUserSalary(UserSalary usersalary)
        {
            return DataProviderClass.Instance().UpdateUserSalary(usersalary);
        }

        public static ReturnType UpdateUserSalary(string usersalaryCode, UserSalary usersalary)
        {
            return DataProviderClass.Instance().UpdateUserSalary(usersalaryCode, usersalary);
        }

        /*
        public static ReturnType UpdateUserSalary(int usersalaryID, UserSalary usersalary)
        {
            return DataProviderClass.Instance().UpdateUserSalary(usersalaryID, usersalary);
        }
        */

        public static List<UserSalary> GetAllUserSalary()
        {
            return DataProviderClass.Instance().GetAllUserSalary();
        }

        public static List<UserSalary> GetUserSalary(Func<UserSalary, bool> func)
        {
            return DataProviderClass.Instance().GetUserSalary(func);
        }

        public static UserSalary GetUserSalary(string usersalaryCode)
        {
            return DataProviderClass.Instance().GetUserSalary(usersalaryCode);
        }

        /*
        public static UserSalary GetUserSalary(int usersalaryID)
        {
            return DataProviderClass.Instance().GetUserSalary(usersalaryID);
        }
        */

        public static List<UserSalary> GetUserSalary(List<string> usersalaryCodeList)
        {
            return DataProviderClass.Instance().GetUserSalary(usersalaryCodeList);
        }

        /*
        public static List<UserSalary> GetUserSalary(List<int> usersalaryIDList)
        {
            return DataProviderClass.Instance().GetUserSalary(usersalaryIDList);
        }
        */

        public static List<UserSalary> GetUserSalary(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetUserSalary(pageIndex, pageSize, out rowCount);
        }

        public static List<UserSalary> GetUserSalary(Func<UserSalary, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetUserSalary(func, pageIndex, pageSize, out rowCount);
        }
    }
}
