using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IEmailTemplateCat
    {
        ReturnType AddEmailTemplateCat(EmailTemplateCat emailtemplatecat);

        ReturnType AddEmailTemplateCat(List<EmailTemplateCat> emailtemplatecatList);

        ReturnType RemoveAllEmailTemplateCat();

        ReturnType RemoveEmailTemplateCat(Func<EmailTemplateCat, bool> func);

        ReturnType RemoveEmailTemplateCat(string emailtemplatecatCode);

        ReturnType RemoveEmailTemplateCat(List<string> emailtemplatecatCodeList);

        ReturnType UpdateEmailTemplateCat(EmailTemplateCat emailtemplatecat);

        ReturnType UpdateEmailTemplateCat(string emailtemplatecatCode, EmailTemplateCat emailtemplatecat);

        List<EmailTemplateCat> GetAllEmailTemplateCat();

        List<EmailTemplateCat> GetEmailTemplateCat(Func<EmailTemplateCat, bool> func);

        List<EmailTemplateCat> GetEmailTemplateCat(List<string> emailtemplatecatCodeList);

        List<EmailTemplateCat> GetEmailTemplateCat(int pageIndex, int pageSize, out int rowCount);

        List<EmailTemplateCat> GetEmailTemplateCat(Func<EmailTemplateCat, bool> func, int pageIndex, int pageSize, out int rowCount);
    }
}
