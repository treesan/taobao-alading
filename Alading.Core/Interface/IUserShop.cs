using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Core.Interface
{
    public interface IUserShop
    {
        ReturnType AddUserShop(UserShop usershop);

        ReturnType AddUserShop(List<UserShop> usershopList);

        ReturnType RemoveAllUserShop();

        ReturnType RemoveUserShop(Func<UserShop, bool> func);

        ReturnType RemoveUserShop(long usershopId);

        ReturnType RemoveUserShop(List<long> usershopIdList);

        ReturnType UpdateUserShop(UserShop usershop);

        ReturnType UpdateUserShop(long usershopId, UserShop usershop);

        List<UserShop> GetAllUserShop();

        List<UserShop> GetUserShop(Func<UserShop, bool> func);

        List<UserShop> GetUserShop(List<long> usershopIdList);

        List<UserShop> GetUserShop(int pageIndex, int pageSize, out int rowCount);

        List<UserShop> GetUserShop(Func<UserShop, bool> func, int pageIndex, int pageSize, out int rowCount);
    }
}
