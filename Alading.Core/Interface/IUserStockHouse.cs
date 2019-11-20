using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Core.Interface
{
    public interface IUserStockHouse
    {
        ReturnType AddUserStockHouse(UserStockHouse userstockhouse);

        ReturnType AddUserStockHouse(List<UserStockHouse> userstockhouseList);

        ReturnType RemoveAllUserStockHouse();

        ReturnType RemoveUserStockHouse(Func<UserStockHouse, bool> func);

        ReturnType RemoveUserStockHouse(long userstockhouseId);

        ReturnType RemoveUserStockHouse(List<long> userstockhouseIdList);

        ReturnType UpdateUserStockHouse(UserStockHouse userstockhouse);

        ReturnType UpdateUserStockHouse(long userstockhouseId, UserStockHouse userstockhouse);

        List<UserStockHouse> GetAllUserStockHouse();

        List<UserStockHouse> GetUserStockHouse(Func<UserStockHouse, bool> func);

        List<UserStockHouse> GetUserStockHouse(List<long> userstockhouseIdList);

        List<UserStockHouse> GetUserStockHouse(int pageIndex, int pageSize, out int rowCount);

        List<UserStockHouse> GetUserStockHouse(Func<UserStockHouse, bool> func, int pageIndex, int pageSize, out int rowCount);
    }
}
