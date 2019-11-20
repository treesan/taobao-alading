using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class LogisticCompanyTemplateService
    {

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

        public static List<LogisticCompanyTemplate> GetLogisticCompanyTemplate(string logisticcompanytemplateCode)
        {
            return DataProviderClass.Instance().GetLogisticCompanyTemplate(logisticcompanytemplateCode);
        }

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
    }
}
