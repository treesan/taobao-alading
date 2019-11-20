using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface ILogisticCompanyTemplateItem
    {       
        ReturnType AddLogisticCompanyTemplateItem(LogisticCompanyTemplateItem logisticcompanytemplateitem);
       
        ReturnType AddLogisticCompanyTemplateItem(List<LogisticCompanyTemplateItem> logisticcompanytemplateitemList);
        
        ReturnType RemoveLogisticCompanyTemplateItems(string logisticCompanyTemplateCode);
       
        ReturnType RemoveLogisticCompanyTemplateItem(Func<LogisticCompanyTemplateItem, bool> func);
              
        ReturnType RemoveLogisticCompanyTemplateItem(string logisticcompanytemplateitemCode);
        
        ReturnType RemoveLogisticCompanyTemplateItem(List<string> logisticcompanytemplateitemCodeList);
       
        ReturnType UpdateLogisticCompanyTemplateItem(LogisticCompanyTemplateItem logisticcompanytemplateitem);
       
        ReturnType UpdateLogisticCompanyTemplateItem(string logisticcompanytemplateitemCode,LogisticCompanyTemplateItem logisticcompanytemplateitem);
       
        List<LogisticCompanyTemplateItem> GetAllLogisticCompanyTemplateItem();

        List<LogisticCompanyTemplateItem> GetLogisticTemplateItems(string LogisticTemplateCode);
      
        List<LogisticCompanyTemplateItem> GetLogisticCompanyTemplateItem(Func<LogisticCompanyTemplateItem, bool> func);
      
        List<LogisticCompanyTemplateItem> GetLogisticCompanyTemplateItem(List<string> logisticcompanytemplateitemCodeList);
       
        List<LogisticCompanyTemplateItem> GetLogisticCompanyTemplateItem(int pageIndex, int pageSize, out int rowCount);
        
        List<LogisticCompanyTemplateItem> GetLogisticCompanyTemplateItem(Func<LogisticCompanyTemplateItem, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveLogisticCompanyTemplateItem(int logisticcompanytemplateitemID);
        
        ReturnType RemoveLogisticCompanyTemplateItem(List<int> logisticcompanytemplateitemIDList);
        
        ReturnType UpdateLogisticCompanyTemplateItem(int logisticcompanytemplateitemID,LogisticCompanyTemplateItem logisticcompanytemplateitem);
        
        List<LogisticCompanyTemplateItem> GetLogisticCompanyTemplateItem(List<int> logisticcompanytemplateitemIDList);
        */
    }
}
