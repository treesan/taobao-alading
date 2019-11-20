using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IView_UserRole
    {
        List<View_UserRole> GetAllView_UserRole();

        List<View_UserRole> GetView_UserRole(Func<View_UserRole, bool> func);

        List<View_UserRole> GetView_UserRole(List<string> view_userroleCodeList);

        List<View_UserRole> GetView_UserRole(int pageIndex, int pageSize, out int rowCount);

        List<View_UserRole> GetView_UserRole(Func<View_UserRole, bool> func, int pageIndex, int pageSize, out int rowCount);
        
        /*        
        ReturnType RemoveView_UserRole(int view_userroleID);
        
        ReturnType RemoveView_UserRole(List<int> view_userroleIDList);
        
        ReturnType UpdateView_UserRole(int view_userroleID,View_UserRole view_userrole);
        
        List<View_UserRole> GetView_UserRole(List<int> view_userroleIDList);
        */
    }
}
