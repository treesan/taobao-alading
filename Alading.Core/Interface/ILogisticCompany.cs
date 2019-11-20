using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface ILogisticCompany
    {       
        ReturnType AddLogisticCompany(LogisticCompany logisticcompany);
       
        ReturnType AddLogisticCompany(List<LogisticCompany> logisticcompanyList);
        
        ReturnType RemoveAllLogisticCompany();
       
        ReturnType RemoveLogisticCompany(Func<LogisticCompany, bool> func);

        ReturnType RemoveLogisticCompany(string id);

        ReturnType RemoveLogisticCompany(List<string> idList);
       
        ReturnType UpdateLogisticCompany(LogisticCompany logisticcompany);

        ReturnType UpdateLogisticCompany(string id, LogisticCompany logisticcompany);
       
        List<LogisticCompany> GetAllLogisticCompany();
      
        List<LogisticCompany> GetLogisticCompany(Func<LogisticCompany, bool> func);

        List<LogisticCompany> GetLogisticCompany(List<string> idList);
       
        List<LogisticCompany> GetLogisticCompany(int pageIndex, int pageSize, out int rowCount);
        
        List<LogisticCompany> GetLogisticCompany(Func<LogisticCompany, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveLogisticCompany(int logisticcompanyID);
        
        ReturnType RemoveLogisticCompany(List<int> logisticcompanyIDList);
        
        ReturnType UpdateLogisticCompany(int logisticcompanyID,LogisticCompany logisticcompany);
        
        List<LogisticCompany> GetLogisticCompany(List<int> logisticcompanyIDList);
        */
    }
}
