using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class UserStockHouseService
    {
        public static ReturnType AddUserStockHouse(UserStockHouse userstockhouse)
        {
            return DataProviderClass.Instance().AddUserStockHouse(userstockhouse);
        }

        public static ReturnType AddUserStockHouse(List<UserStockHouse> userstockhouseList)
        {
            return DataProviderClass.Instance().AddUserStockHouse(userstockhouseList);
        }

        public static ReturnType RemoveAllUserStockHouse()
        {
            return DataProviderClass.Instance().RemoveAllUserStockHouse();
        }

        public static ReturnType RemoveUserStockHouse(Func<UserStockHouse, bool> func)
        {
            return DataProviderClass.Instance().RemoveUserStockHouse(func);
        }

        public static ReturnType RemoveUserStockHouse(long userstockhouseID)
        {
            return DataProviderClass.Instance().RemoveUserStockHouse(userstockhouseID);
        }

        public static ReturnType RemoveUserStockHouse(List<long> userstockhouseIDList)
        {
            return DataProviderClass.Instance().RemoveUserStockHouse(userstockhouseIDList);
        }

        public static ReturnType UpdateUserStockHouse(UserStockHouse userstockhouse)
        {
            return DataProviderClass.Instance().UpdateUserStockHouse(userstockhouse);
        }

        public static ReturnType UpdateUserStockHouse(long userstockhouseID, UserStockHouse userstockhouse)
        {
            return DataProviderClass.Instance().UpdateUserStockHouse(userstockhouseID, userstockhouse);
        }

        public static List<UserStockHouse> GetAllUserStockHouse()
        {
            return DataProviderClass.Instance().GetAllUserStockHouse();
        }

        public static List<UserStockHouse> GetUserStockHouse(Func<UserStockHouse, bool> func)
        {
            return DataProviderClass.Instance().GetUserStockHouse(func);
        }

        public static UserStockHouse GetUserStockHouse(long userstockhouseID)
        {
            return DataProviderClass.Instance().GetUserStockHouse(userstockhouseID);
        }

        public static List<UserStockHouse> GetUserStockHouse(List<long> userstockhouseIDList)
        {
            return DataProviderClass.Instance().GetUserStockHouse(userstockhouseIDList);
        }

        public static List<UserStockHouse> GetUserStockHouse(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetUserStockHouse(pageIndex, pageSize, out rowCount);
        }

        public static List<UserStockHouse> GetUserStockHouse(Func<UserStockHouse, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetUserStockHouse(func, pageIndex, pageSize, out rowCount);
        }
    }
}
