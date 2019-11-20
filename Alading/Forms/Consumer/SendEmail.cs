using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetOpenMail;
using DotNetOpenMail.Logging;
using DotNetOpenMail.Encoding;
using DotNetOpenMail.Resources;
using DotNetOpenMail.SmtpAuth;

namespace Alading.Forms.Consumer
{
    public class SendEmail
    {
        public static bool Send(string from, string to, string subject, string content, string smtpServerAddress, string smtpAccount, string smtpPassword)
        {
            EmailMessage emailMessage = new EmailMessage();
            emailMessage.FromAddress = new EmailAddress(from);
            emailMessage.AddToAddress(new EmailAddress(to));
            emailMessage.Subject = subject;
            emailMessage.HtmlPart = new HtmlAttachment(content);

            emailMessage.HeaderCharSet = System.Text.Encoding.GetEncoding("GB2312");
            emailMessage.HeaderEncoding = EncodingType.Base64;
            emailMessage.HtmlPart.CharSet = System.Text.Encoding.GetEncoding("gb2312");

            SmtpServer smtpServer = new SmtpServer(smtpServerAddress);
            smtpServer.SmtpAuthToken=new SmtpAuthToken(smtpAccount, smtpPassword);
            smtpServer.ServerTimeout = 3000;
            emailMessage.ContentType = "TEXT/HTML";
            try
            {
                emailMessage.Send(smtpServer);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
