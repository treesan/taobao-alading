using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class EmailTemplateCatService
    {
        public static ReturnType AddEmailTemplateCat(EmailTemplateCat emailtemplatecat)
        {
            return DataProviderClass.Instance().AddEmailTemplateCat(emailtemplatecat);
        }

        public static ReturnType AddEmailTemplateCat(List<EmailTemplateCat> emailtemplatecatList)
        {
            return DataProviderClass.Instance().AddEmailTemplateCat(emailtemplatecatList);
        }

        public static ReturnType RemoveAllEmailTemplateCat()
        {
            return DataProviderClass.Instance().RemoveAllEmailTemplateCat();
        }

        public static ReturnType RemoveEmailTemplateCat(Func<EmailTemplateCat, bool> func)
        {
            return DataProviderClass.Instance().RemoveEmailTemplateCat(func);
        }

        public static ReturnType RemoveEmailTemplateCat(string emailtemplatecatCode)
        {
            return DataProviderClass.Instance().RemoveEmailTemplateCat(emailtemplatecatCode);
        }

        /*
        public static ReturnType RemoveEmailTemplateCat(int emailtemplatecatID)
        {
            return DataProviderClass.Instance().RemoveEmailTemplateCat(emailtemplatecatID);
        }
        */

        public static ReturnType RemoveEmailTemplateCat(List<string> emailtemplatecatCodeList)
        {
            return DataProviderClass.Instance().RemoveEmailTemplateCat(emailtemplatecatCodeList);
        }

        /*
        public static ReturnType RemoveEmailTemplateCat(List<int> emailtemplatecatIDList)
        {
            return DataProviderClass.Instance().RemoveEmailTemplateCat(emailtemplatecatIDList);
        }
        */

        public static ReturnType UpdateEmailTemplateCat(EmailTemplateCat emailtemplatecat)
        {
            return DataProviderClass.Instance().UpdateEmailTemplateCat(emailtemplatecat);
        }

        public static ReturnType UpdateEmailTemplateCat(string emailtemplatecatCode, EmailTemplateCat emailtemplatecat)
        {
            return DataProviderClass.Instance().UpdateEmailTemplateCat(emailtemplatecatCode, emailtemplatecat);
        }

        /*
        public static ReturnType UpdateEmailTemplateCat(int emailtemplatecatID, EmailTemplateCat emailtemplatecat)
        {
            return DataProviderClass.Instance().UpdateEmailTemplateCat(emailtemplatecatID, emailtemplatecat);
        }
        */

        public static List<EmailTemplateCat> GetAllEmailTemplateCat()
        {
            return DataProviderClass.Instance().GetAllEmailTemplateCat();
        }

        public static List<EmailTemplateCat> GetEmailTemplateCat(Func<EmailTemplateCat, bool> func)
        {
            return DataProviderClass.Instance().GetEmailTemplateCat(func);
        }

        public static EmailTemplateCat GetEmailTemplateCat(string emailtemplatecatCode)
        {
            return DataProviderClass.Instance().GetEmailTemplateCat(emailtemplatecatCode);
        }

        /*
        public static EmailTemplateCat GetEmailTemplateCat(int emailtemplatecatID)
        {
            return DataProviderClass.Instance().GetEmailTemplateCat(emailtemplatecatID);
        }
        */

        public static List<EmailTemplateCat> GetEmailTemplateCat(List<string> emailtemplatecatCodeList)
        {
            return DataProviderClass.Instance().GetEmailTemplateCat(emailtemplatecatCodeList);
        }

        /*
        public static List<EmailTemplateCat> GetEmailTemplateCat(List<int> emailtemplatecatIDList)
        {
            return DataProviderClass.Instance().GetEmailTemplateCat(emailtemplatecatIDList);
        }
        */

        public static List<EmailTemplateCat> GetEmailTemplateCat(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetEmailTemplateCat(pageIndex, pageSize, out rowCount);
        }

        public static List<EmailTemplateCat> GetEmailTemplateCat(Func<EmailTemplateCat, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetEmailTemplateCat(func, pageIndex, pageSize, out rowCount);
        }
    }
}
