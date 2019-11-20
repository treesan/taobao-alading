using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IView_LogisticCompanyTemplate
    {       
        List<View_LogisticCompanyTemplate> GetAllView_LogisticCompanyTemplate();
      
        List<View_LogisticCompanyTemplate> GetView_LogisticCompanyTemplate(Func<View_LogisticCompanyTemplate, bool> func);
      
        List<View_LogisticCompanyTemplate> GetView_LogisticCompanyTemplate(List<string> view_logisticcompanytemplateCodeList);
       
        List<View_LogisticCompanyTemplate> GetView_LogisticCompanyTemplate(int pageIndex, int pageSize, out int rowCount);
        
        List<View_LogisticCompanyTemplate> GetView_LogisticCompanyTemplate(Func<View_LogisticCompanyTemplate, bool> func, int pageIndex, int pageSize, out int rowCount);
        
        /*        
        ReturnType RemoveView_LogisticCompanyTemplate(int view_logisticcompanytemplateID);
        
        ReturnType RemoveView_LogisticCompanyTemplate(List<int> view_logisticcompanytemplateIDList);
        
        ReturnType UpdateView_LogisticCompanyTemplate(int view_logisticcompanytemplateID,View_LogisticCompanyTemplate view_logisticcompanytemplate);
        
        List<View_LogisticCompanyTemplate> GetView_LogisticCompanyTemplate(List<int> view_logisticcompanytemplateIDList);
        */
    }
}
