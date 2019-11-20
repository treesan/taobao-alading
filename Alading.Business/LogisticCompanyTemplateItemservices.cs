using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class LogisticCompanyTemplateItemService
    {

        public static ReturnType AddLogisticCompanyTemplateItem(LogisticCompanyTemplateItem logisticcompanytemplateitem)
        {
            return DataProviderClass.Instance().AddLogisticCompanyTemplateItem(logisticcompanytemplateitem);
        }

        public static ReturnType AddLogisticCompanyTemplateItem(List<LogisticCompanyTemplateItem> logisticcompanytemplateitemList)
        {
            return DataProviderClass.Instance().AddLogisticCompanyTemplateItem(logisticcompanytemplateitemList);
        }

        public static ReturnType RemoveLogisticCompanyTemplateItems(string logisticCompanyTemplateCode)
        {
            return DataProviderClass.Instance().RemoveLogisticCompanyTemplateItems(logisticCompanyTemplateCode);
        }
    
        public static ReturnType RemoveLogisticCompanyTemplateItem(Func<LogisticCompanyTemplateItem, bool> func)
        {
            return DataProviderClass.Instance().RemoveLogisticCompanyTemplateItem(func);
        }
        
        public static ReturnType RemoveLogisticCompanyTemplateItem(string logisticcompanytemplateitemCode)
        {
            return DataProviderClass.Instance().RemoveLogisticCompanyTemplateItem(logisticcompanytemplateitemCode);
        }       
        
        /*
        public static ReturnType RemoveLogisticCompanyTemplateItem(int logisticcompanytemplateitemID)
        {
            return DataProviderClass.Instance().RemoveLogisticCompanyTemplateItem(logisticcompanytemplateitemID);
        }
        */
    
        public static ReturnType RemoveLogisticCompanyTemplateItem(List<string> logisticcompanytemplateitemCodeList)
        {
            return DataProviderClass.Instance().RemoveLogisticCompanyTemplateItem(logisticcompanytemplateitemCodeList);
        }        
        
        /*
        public static ReturnType RemoveLogisticCompanyTemplateItem(List<int> logisticcompanytemplateitemIDList)
        {
            return DataProviderClass.Instance().RemoveLogisticCompanyTemplateItem(logisticcompanytemplateitemIDList);
        }
        */
    
        public static ReturnType UpdateLogisticCompanyTemplateItem(LogisticCompanyTemplateItem logisticcompanytemplateitem)
        {
            return DataProviderClass.Instance().UpdateLogisticCompanyTemplateItem(logisticcompanytemplateitem);
        }
    
        public static ReturnType UpdateLogisticCompanyTemplateItem(string logisticcompanytemplateitemCode, LogisticCompanyTemplateItem logisticcompanytemplateitem)
        {
            return DataProviderClass.Instance().UpdateLogisticCompanyTemplateItem(logisticcompanytemplateitemCode, logisticcompanytemplateitem);
        }
        
        /*
        public static ReturnType UpdateLogisticCompanyTemplateItem(int logisticcompanytemplateitemID, LogisticCompanyTemplateItem logisticcompanytemplateitem)
        {
            return DataProviderClass.Instance().UpdateLogisticCompanyTemplateItem(logisticcompanytemplateitemID, logisticcompanytemplateitem);
        }
        */
    
        public static List<LogisticCompanyTemplateItem> GetAllLogisticCompanyTemplateItem()
        {
            return DataProviderClass.Instance().GetAllLogisticCompanyTemplateItem();
        }
    
        public static List<LogisticCompanyTemplateItem> GetLogisticCompanyTemplateItem(Func<LogisticCompanyTemplateItem, bool> func)
        {
            return DataProviderClass.Instance().GetLogisticCompanyTemplateItem(func);
        }
    
        public static LogisticCompanyTemplateItem GetLogisticCompanyTemplateItem(string logisticcompanytemplateitemCode)
        {
            return DataProviderClass.Instance().GetLogisticCompanyTemplateItem(logisticcompanytemplateitemCode);
        }
        
        /*
        public static LogisticCompanyTemplateItem GetLogisticCompanyTemplateItem(int logisticcompanytemplateitemID)
        {
            return DataProviderClass.Instance().GetLogisticCompanyTemplateItem(logisticcompanytemplateitemID);
        }
        */
    
        public static List<LogisticCompanyTemplateItem> GetLogisticCompanyTemplateItem(List<string> logisticcompanytemplateitemCodeList)
        {
            return DataProviderClass.Instance().GetLogisticCompanyTemplateItem(logisticcompanytemplateitemCodeList);
        }
        
        /*
        public static List<LogisticCompanyTemplateItem> GetLogisticCompanyTemplateItem(List<int> logisticcompanytemplateitemIDList)
        {
            return DataProviderClass.Instance().GetLogisticCompanyTemplateItem(logisticcompanytemplateitemIDList);
        }
        */
    
        public static List<LogisticCompanyTemplateItem> GetLogisticCompanyTemplateItem(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetLogisticCompanyTemplateItem(pageIndex, pageSize, out rowCount);
        }
        
        public static List<LogisticCompanyTemplateItem> GetLogisticCompanyTemplateItem(Func<LogisticCompanyTemplateItem, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetLogisticCompanyTemplateItem(func, pageIndex, pageSize, out rowCount);
        }

        public static List<LogisticCompanyTemplateItem> GetLogisticTemplateItems(string LogisticTemplateCode)
        {
            return DataProviderClass.Instance().GetLogisticTemplateItems(LogisticTemplateCode);
        }
    }
}
