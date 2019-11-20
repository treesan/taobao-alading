using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IEmailTemplate
    {
        ReturnType AddEmailTemplate(EmailTemplate emailtemplate);

        ReturnType AddEmailTemplate(List<EmailTemplate> emailtemplateList);

        ReturnType RemoveAllEmailTemplate();

        ReturnType RemoveEmailTemplate(Func<EmailTemplate, bool> func);

        ReturnType RemoveEmailTemplate(string emailtemplateCode);

        ReturnType RemoveEmailTemplate(List<string> emailtemplateCodeList);

        ReturnType UpdateEmailTemplate(EmailTemplate emailtemplate);

        ReturnType UpdateEmailTemplate(string emailtemplateCode, EmailTemplate emailtemplate);

        List<EmailTemplate> GetAllEmailTemplate();

        List<EmailTemplate> GetEmailTemplate(Func<EmailTemplate, bool> func);

        List<EmailTemplate> GetEmailTemplate(List<string> emailtemplateCodeList);

        List<EmailTemplate> GetEmailTemplate(int pageIndex, int pageSize, out int rowCount);

        List<EmailTemplate> GetEmailTemplate(Func<EmailTemplate, bool> func, int pageIndex, int pageSize, out int rowCount);
    }
}
