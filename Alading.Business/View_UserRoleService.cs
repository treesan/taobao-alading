using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class View_UserRoleService
    {
        public static List<View_UserRole> GetAllView_UserRole()
        {
            return DataProviderClass.Instance().GetAllView_UserRole();
        }

        public static List<View_UserRole> GetView_UserRole(Func<View_UserRole, bool> func)
        {
            return DataProviderClass.Instance().GetView_UserRole(func);
        }

        public static View_UserRole GetView_UserRole(string view_userroleCode)
        {
            return DataProviderClass.Instance().GetView_UserRole(view_userroleCode);
        }

        /*
        public static View_UserRole GetView_UserRole(int view_userroleID)
        {
            return DataProviderClass.Instance().GetView_UserRole(view_userroleID);
        }
        */

        public static List<View_UserRole> GetView_UserRole(List<string> view_userroleCodeList)
        {
            return DataProviderClass.Instance().GetView_UserRole(view_userroleCodeList);
        }

        /*
        public static List<View_UserRole> GetView_UserRole(List<int> view_userroleIDList)
        {
            return DataProviderClass.Instance().GetView_UserRole(view_userroleIDList);
        }
        */

        public static List<View_UserRole> GetView_UserRole(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetView_UserRole(pageIndex, pageSize, out rowCount);
        }

        public static List<View_UserRole> GetView_UserRole(Func<View_UserRole, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetView_UserRole(func, pageIndex, pageSize, out rowCount);
        }
    }
}
