using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class LogisticCompanyService
    {

        public static ReturnType AddLogisticCompany(LogisticCompany logisticcompany)
        {
            return DataProviderClass.Instance().AddLogisticCompany(logisticcompany);
        }

        public static ReturnType AddLogisticCompany(List<LogisticCompany> logisticcompanyList)
        {
            return DataProviderClass.Instance().AddLogisticCompany(logisticcompanyList);
        }
    
        public static ReturnType RemoveAllLogisticCompany()
        {
            return DataProviderClass.Instance().RemoveAllLogisticCompany();
        }
    
        public static ReturnType RemoveLogisticCompany(Func<LogisticCompany, bool> func)
        {
            return DataProviderClass.Instance().RemoveLogisticCompany(func);
        }

        public static ReturnType RemoveLogisticCompany(string id)
        {
            return DataProviderClass.Instance().RemoveLogisticCompany(id);
        }       
        
        /*
        public static ReturnType RemoveLogisticCompany(int logisticcompanyID)
        {
            return DataProviderClass.Instance().RemoveLogisticCompany(logisticcompanyID);
        }
        */

        public static ReturnType RemoveLogisticCompany(List<string> idList)
        {
            return DataProviderClass.Instance().RemoveLogisticCompany(idList);
        }        
        
        /*
        public static ReturnType RemoveLogisticCompany(List<int> logisticcompanyIDList)
        {
            return DataProviderClass.Instance().RemoveLogisticCompany(logisticcompanyIDList);
        }
        */
    
        public static ReturnType UpdateLogisticCompany(LogisticCompany logisticcompany)
        {
            return DataProviderClass.Instance().UpdateLogisticCompany(logisticcompany);
        }

        public static ReturnType UpdateLogisticCompany(string id, LogisticCompany logisticcompany)
        {
            return DataProviderClass.Instance().UpdateLogisticCompany(id, logisticcompany);
        }
        
        /*
        public static ReturnType UpdateLogisticCompany(int logisticcompanyID, LogisticCompany logisticcompany)
        {
            return DataProviderClass.Instance().UpdateLogisticCompany(logisticcompanyID, logisticcompany);
        }
        */
    
        public static List<LogisticCompany> GetAllLogisticCompany()
        {
            return DataProviderClass.Instance().GetAllLogisticCompany();
        }
    
        public static List<LogisticCompany> GetLogisticCompany(Func<LogisticCompany, bool> func)
        {
            return DataProviderClass.Instance().GetLogisticCompany(func);
        }

        public static LogisticCompany GetLogisticCompany(string id)
        {
            return DataProviderClass.Instance().GetLogisticCompany(id);
        }
        
        /*
        public static LogisticCompany GetLogisticCompany(int logisticcompanyID)
        {
            return DataProviderClass.Instance().GetLogisticCompany(logisticcompanyID);
        }
        */

        public static List<LogisticCompany> GetLogisticCompany(List<string> idList)
        {
            return DataProviderClass.Instance().GetLogisticCompany(idList);
        }
        
        /*
        public static List<LogisticCompany> GetLogisticCompany(List<int> logisticcompanyIDList)
        {
            return DataProviderClass.Instance().GetLogisticCompany(logisticcompanyIDList);
        }
        */
    
        public static List<LogisticCompany> GetLogisticCompany(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetLogisticCompany(pageIndex, pageSize, out rowCount);
        }
        
        public static List<LogisticCompany> GetLogisticCompany(Func<LogisticCompany, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetLogisticCompany(func, pageIndex, pageSize, out rowCount);
        }

        public static ReturnType AddLogisticCompanyTemplate(LogisticCompanyTemplate logisticcompanytemplate)
        {
            return DataProviderClass.Instance().AddLogisticCompanyTemplate(logisticcompanytemplate);
        }

        public static ReturnType AddLogisticCompanyTemplate(List<LogisticCompanyTemplate> logisticcompanytemplateList)
        {
            return DataProviderClass.Instance().AddLogisticCompanyTemplate(logisticcompanytemplateList);
        }

        public static ReturnType RemoveAllLogisticCompanyTemplate()
        {
            return DataProviderClass.Instance().RemoveAllLogisticCompanyTemplate();
        }

        public static ReturnType RemoveLogisticCompanyTemplate(Func<LogisticCompanyTemplate, bool> func)
        {
            return DataProviderClass.Instance().RemoveLogisticCompanyTemplate(func);
        }

        public static ReturnType RemoveLogisticCompanyTemplate(string logisticcompanytemplateCode)
        {
            return DataProviderClass.Instance().RemoveLogisticCompanyTemplate(logisticcompanytemplateCode);
        }

        /*
        public static ReturnType RemoveLogisticCompanyTemplate(int logisticcompanytemplateID)
        {
            return DataProviderClass.Instance().RemoveLogisticCompanyTemplate(logisticcompanytemplateID);
        }
        */

        public static ReturnType RemoveLogisticCompanyTemplate(List<string> logisticcompanytemplateCodeList)
        {
            return DataProviderClass.Instance().RemoveLogisticCompanyTemplate(logisticcompanytemplateCodeList);
        }

        /*
        public static ReturnType RemoveLogisticCompanyTemplate(List<int> logisticcompanytemplateIDList)
        {
            return DataProviderClass.Instance().RemoveLogisticCompanyTemplate(logisticcompanytemplateIDList);
        }
        */

        public static ReturnType UpdateLogisticCompanyTemplate(LogisticCompanyTemplate logisticcompanytemplate)
        {
            return DataProviderClass.Instance().UpdateLogisticCompanyTemplate(logisticcompanytemplate);
        }

        public static ReturnType UpdateLogisticCompanyTemplate(string logisticcompanytemplateCode, LogisticCompanyTemplate logisticcompanytemplate)
        {
            return DataProviderClass.Instance().UpdateLogisticCompanyTemplate(logisticcompanytemplateCode, logisticcompanytemplate);
        }

        /*
        public static ReturnType UpdateLogisticCompanyTemplate(int logisticcompanytemplateID, LogisticCompanyTemplate logisticcompanytemplate)
        {
            return DataProviderClass.Instance().UpdateLogisticCompanyTemplate(logisticcompanytemplateID, logisticcompanytemplate);
        }
        */

        public static List<LogisticCompanyTemplate> GetAllLogisticCompanyTemplate()
        {
            return DataProviderClass.Instance().GetAllLogisticCompanyTemplate();
        }

        public static List<LogisticCompanyTemplate> GetLogisticCompanyTemplate(Func<LogisticCompanyTemplate, bool> func)
        {
            return DataProviderClass.Instance().GetLogisticCompanyTemplate(func);
        }

        //public static LogisticCompanyTemplate GetLogisticCompanyTemplate(string logisticcompanytemplateCode)
        //{
        //    return DataProviderClass.Instance().GetLogisticCompanyTemplate(logisticcompanytemplateCode);
        //}

        /*
        public static LogisticCompanyTemplate GetLogisticCompanyTemplate(int logisticcompanytemplateID)
        {
            return DataProviderClass.Instance().GetLogisticCompanyTemplate(logisticcompanytemplateID);
        }
        */

        public static List<LogisticCompanyTemplate> GetLogisticCompanyTemplate(List<string> logisticcompanytemplateCodeList)
        {
            return DataProviderClass.Instance().GetLogisticCompanyTemplate(logisticcompanytemplateCodeList);
        }

        /*
        public static List<LogisticCompanyTemplate> GetLogisticCompanyTemplate(List<int> logisticcompanytemplateIDList)
        {
            return DataProviderClass.Instance().GetLogisticCompanyTemplate(logisticcompanytemplateIDList);
        }
        */

        public static List<LogisticCompanyTemplate> GetLogisticCompanyTemplate(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetLogisticCompanyTemplate(pageIndex, pageSize, out rowCount);
        }

        public static List<LogisticCompanyTemplate> GetLogisticCompanyTemplate(Func<LogisticCompanyTemplate, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetLogisticCompanyTemplate(func, pageIndex, pageSize, out rowCount);
        }

        public static List<LogisticCompanyTemplate> GetLogisticCompanyTemplate(string logisticCompanyCode)
        {
            return DataProviderClass.Instance().GetLogisticCompanyTemplate(logisticCompanyCode);
        }
        public static LogisticCompanyTemplate GetLogisticTemplate(string LogisticTemplateCode)
        {
            return DataProviderClass.Instance().GetLogisticTemplate(LogisticTemplateCode);
        }
    }
}
