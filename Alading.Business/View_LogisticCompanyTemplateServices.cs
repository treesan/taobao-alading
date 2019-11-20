using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class View_LogisticCompanyTemplateService
    {         
        public static List<View_LogisticCompanyTemplate> GetAllView_LogisticCompanyTemplate()
        {
            return DataProviderClass.Instance().GetAllView_LogisticCompanyTemplate();
        }
    
        public static List<View_LogisticCompanyTemplate> GetView_LogisticCompanyTemplate(Func<View_LogisticCompanyTemplate, bool> func)
        {
            return DataProviderClass.Instance().GetView_LogisticCompanyTemplate(func);
        }
    
        public static View_LogisticCompanyTemplate GetView_LogisticCompanyTemplate(string view_logisticcompanytemplateCode)
        {
            return DataProviderClass.Instance().GetView_LogisticCompanyTemplate(view_logisticcompanytemplateCode);
        }
        
        /*
        public static View_LogisticCompanyTemplate GetView_LogisticCompanyTemplate(int view_logisticcompanytemplateID)
        {
            return DataProviderClass.Instance().GetView_LogisticCompanyTemplate(view_logisticcompanytemplateID);
        }
        */
    
        public static List<View_LogisticCompanyTemplate> GetView_LogisticCompanyTemplate(List<string> view_logisticcompanytemplateCodeList)
        {
            return DataProviderClass.Instance().GetView_LogisticCompanyTemplate(view_logisticcompanytemplateCodeList);
        }
        
        /*
        public static List<View_LogisticCompanyTemplate> GetView_LogisticCompanyTemplate(List<int> view_logisticcompanytemplateIDList)
        {
            return DataProviderClass.Instance().GetView_LogisticCompanyTemplate(view_logisticcompanytemplateIDList);
        }
        */
    
        public static List<View_LogisticCompanyTemplate> GetView_LogisticCompanyTemplate(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetView_LogisticCompanyTemplate(pageIndex, pageSize, out rowCount);
        }
        
        public static List<View_LogisticCompanyTemplate> GetView_LogisticCompanyTemplate(Func<View_LogisticCompanyTemplate, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetView_LogisticCompanyTemplate(func, pageIndex, pageSize, out rowCount);
        }
    }
}
