using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class EmailTemplateService
    {
        public static ReturnType AddEmailTemplate(EmailTemplate emailtemplate)
        {
            return DataProviderClass.Instance().AddEmailTemplate(emailtemplate);
        }

        public static ReturnType AddEmailTemplate(List<EmailTemplate> emailtemplateList)
        {
            return DataProviderClass.Instance().AddEmailTemplate(emailtemplateList);
        }

        public static ReturnType RemoveAllEmailTemplate()
        {
            return DataProviderClass.Instance().RemoveAllEmailTemplate();
        }

        public static ReturnType RemoveEmailTemplate(Func<EmailTemplate, bool> func)
        {
            return DataProviderClass.Instance().RemoveEmailTemplate(func);
        }

        public static ReturnType RemoveEmailTemplate(string emailtemplateCode)
        {
            return DataProviderClass.Instance().RemoveEmailTemplate(emailtemplateCode);
        }

        /*
        public static ReturnType RemoveEmailTemplate(int emailtemplateID)
        {
            return DataProviderClass.Instance().RemoveEmailTemplate(emailtemplateID);
        }
        */

        public static ReturnType RemoveEmailTemplate(List<string> emailtemplateCodeList)
        {
            return DataProviderClass.Instance().RemoveEmailTemplate(emailtemplateCodeList);
        }

        /*
        public static ReturnType RemoveEmailTemplate(List<int> emailtemplateIDList)
        {
            return DataProviderClass.Instance().RemoveEmailTemplate(emailtemplateIDList);
        }
        */

        public static ReturnType UpdateEmailTemplate(EmailTemplate emailtemplate)
        {
            return DataProviderClass.Instance().UpdateEmailTemplate(emailtemplate);
        }

        public static ReturnType UpdateEmailTemplate(string emailtemplateCode, EmailTemplate emailtemplate)
        {
            return DataProviderClass.Instance().UpdateEmailTemplate(emailtemplateCode, emailtemplate);
        }

        /*
        public static ReturnType UpdateEmailTemplate(int emailtemplateID, EmailTemplate emailtemplate)
        {
            return DataProviderClass.Instance().UpdateEmailTemplate(emailtemplateID, emailtemplate);
        }
        */

        public static List<EmailTemplate> GetAllEmailTemplate()
        {
            return DataProviderClass.Instance().GetAllEmailTemplate();
        }

        public static List<EmailTemplate> GetEmailTemplate(Func<EmailTemplate, bool> func)
        {
            return DataProviderClass.Instance().GetEmailTemplate(func);
        }

        public static EmailTemplate GetEmailTemplate(string emailtemplateCode)
        {
            return DataProviderClass.Instance().GetEmailTemplate(emailtemplateCode);
        }

        /*
        public static EmailTemplate GetEmailTemplate(int emailtemplateID)
        {
            return DataProviderClass.Instance().GetEmailTemplate(emailtemplateID);
        }
        */

        public static List<EmailTemplate> GetEmailTemplate(List<string> emailtemplateCodeList)
        {
            return DataProviderClass.Instance().GetEmailTemplate(emailtemplateCodeList);
        }

        /*
        public static List<EmailTemplate> GetEmailTemplate(List<int> emailtemplateIDList)
        {
            return DataProviderClass.Instance().GetEmailTemplate(emailtemplateIDList);
        }
        */

        public static List<EmailTemplate> GetEmailTemplate(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetEmailTemplate(pageIndex, pageSize, out rowCount);
        }

        public static List<EmailTemplate> GetEmailTemplate(Func<EmailTemplate, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetEmailTemplate(func, pageIndex, pageSize, out rowCount);
        }
    }
}
