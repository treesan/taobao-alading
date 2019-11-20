using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface ILogisticCompanyTemplate
    {       
        ReturnType AddLogisticCompanyTemplate(LogisticCompanyTemplate logisticcompanytemplate);
       
        ReturnType AddLogisticCompanyTemplate(List<LogisticCompanyTemplate> logisticcompanytemplateList);
        
        ReturnType RemoveAllLogisticCompanyTemplate();
       
        ReturnType RemoveLogisticCompanyTemplate(Func<LogisticCompanyTemplate, bool> func);
              
        ReturnType RemoveLogisticCompanyTemplate(string logisticcompanytemplateCode);
        
        ReturnType RemoveLogisticCompanyTemplate(List<string> logisticcompanytemplateCodeList);
       
        ReturnType UpdateLogisticCompanyTemplate(LogisticCompanyTemplate logisticcompanytemplate);
       
        ReturnType UpdateLogisticCompanyTemplate(string logisticcompanytemplateCode,LogisticCompanyTemplate logisticcompanytemplate);
       
        List<LogisticCompanyTemplate> GetAllLogisticCompanyTemplate();

        LogisticCompanyTemplate GetLogisticTemplate(string LogisticTemplateCode);

        List<LogisticCompanyTemplate> GetLogisticCompanyTemplate(string LogisticCompanyCode);
      
        List<LogisticCompanyTemplate> GetLogisticCompanyTemplate(Func<LogisticCompanyTemplate, bool> func);
      
        List<LogisticCompanyTemplate> GetLogisticCompanyTemplate(List<string> logisticcompanytemplateCodeList);
       
        List<LogisticCompanyTemplate> GetLogisticCompanyTemplate(int pageIndex, int pageSize, out int rowCount);
        
        List<LogisticCompanyTemplate> GetLogisticCompanyTemplate(Func<LogisticCompanyTemplate, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveLogisticCompanyTemplate(int logisticcompanytemplateID);
        
        ReturnType RemoveLogisticCompanyTemplate(List<int> logisticcompanytemplateIDList);
        
        ReturnType UpdateLogisticCompanyTemplate(int logisticcompanytemplateID,LogisticCompanyTemplate logisticcompanytemplate);
        
        List<LogisticCompanyTemplate> GetLogisticCompanyTemplate(List<int> logisticcompanytemplateIDList);
        */
    }
}
