using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class UserShopService
    {
        public static ReturnType AddUserShop(UserShop usershop)
        {
            return DataProviderClass.Instance().AddUserShop(usershop);
        }

        public static ReturnType AddUserShop(List<UserShop> usershopList)
        {
            return DataProviderClass.Instance().AddUserShop(usershopList);
        }

        public static ReturnType RemoveAllUserShop()
        {
            return DataProviderClass.Instance().RemoveAllUserShop();
        }

        public static ReturnType RemoveUserShop(Func<UserShop, bool> func)
        {
            return DataProviderClass.Instance().RemoveUserShop(func);
        }

        /*
        public static ReturnType RemoveUserShop(string usershopCode)
        {
            return DataProviderClass.Instance().RemoveUserShop(usershopCode);
        }
        */

        public static ReturnType RemoveUserShop(long usershopID)
        {
            return DataProviderClass.Instance().RemoveUserShop(usershopID);
        }

        /*
        public static ReturnType RemoveUserShop(List<string> usershopCodeList)
        {
            return DataProviderClass.Instance().RemoveUserShop(usershopCodeList);
        }
        */


        public static ReturnType RemoveUserShop(List<long> usershopIDList)
        {
            return DataProviderClass.Instance().RemoveUserShop(usershopIDList);
        }
        

        public static ReturnType UpdateUserShop(UserShop usershop)
        {
            return DataProviderClass.Instance().UpdateUserShop(usershop);
        }

        /*
        public static ReturnType UpdateUserShop(string usershopCode, UserShop usershop)
        {
            return DataProviderClass.Instance().UpdateUserShop(usershopCode, usershop);
        }
        */

        public static ReturnType UpdateUserShop(long usershopID, UserShop usershop)
        {
            return DataProviderClass.Instance().UpdateUserShop(usershopID, usershop);
        }

        public static List<UserShop> GetAllUserShop()
        {
            return DataProviderClass.Instance().GetAllUserShop();
        }

        public static List<UserShop> GetUserShop(Func<UserShop, bool> func)
        {
            return DataProviderClass.Instance().GetUserShop(func);
        }

        /*
        public static UserShop GetUserShop(string usershopCode)
        {
            return DataProviderClass.Instance().GetUserShop(usershopCode);
        }
        */

        public static UserShop GetUserShop(long usershopID)
        {
            return DataProviderClass.Instance().GetUserShop(usershopID);
        }

        /*
        public static List<UserShop> GetUserShop(List<string> usershopCodeList)
        {
            return DataProviderClass.Instance().GetUserShop(usershopCodeList);
        }
        */
        public static List<UserShop> GetUserShop(List<long> usershopIDList)
        {
            return DataProviderClass.Instance().GetUserShop(usershopIDList);
        }        

        public static List<UserShop> GetUserShop(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetUserShop(pageIndex, pageSize, out rowCount);
        }

        public static List<UserShop> GetUserShop(Func<UserShop, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetUserShop(func, pageIndex, pageSize, out rowCount);
        }
    }
}
