using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetOpenMail;
using DotNetOpenMail.Logging;
using DotNetOpenMail.Encoding;
using DotNetOpenMail.Resources;
using DotNetOpenMail.SmtpAuth;

namespace Alading.Forms.Email
{
    public class SendEmailTask
    {
        private int currentIndex;
        private bool state;
        private SmtpServer server;
        private Encoding messageEncoding;
        private string messagetype;
        private bool isInitialized;
        private int success;

        public bool TaskState
        {
            get { return state; }
        }

        public bool HasNext
        {
            get { return (Total > 0) && (Current < Total); }
        }

        public int Current
        {
            get { return currentIndex; }
        }

        public int Total
        {
            get
            {
                if (TaskList != null) return TaskList.Count;
                return 0;
            }
        }

        public int Percentage
        {
            get
            {
                if (Total == 0) return 0;
                return Convert.ToInt32((double)Current / Total);
            }
        }

        public int SuccessCount
        {
            get { return success; }
        }

        public string TaskCode { get; set; }

        public SmtpConfiguration Configuration { get; set; }

        public List<Alading.Entity.ConsumerVisit> TaskList { get; set; }

        public bool InitializeTask()
        {
            state = false;
            currentIndex = 0;

            if (Configuration != null)
            {
                server = new SmtpServer(Configuration.SmtpServer);
                server.SmtpAuthToken = new SmtpAuthToken(Configuration.SmtpAccount, Configuration.SmtpPassword);
                server.ServerTimeout = Configuration.ConnectionTimeout;
                messageEncoding = System.Text.Encoding.GetEncoding("GB2312");
                messagetype = "TEXT/HTML";

                return true;
            }
            else
            {
                isInitialized = false;
                state = false;
                return false;
            }
        }

        public void StepNext()
        {
            if (TaskList.Count > 0 && currentIndex < TaskList.Count)
            {
                Alading.Entity.ConsumerVisit currentObject = TaskList[currentIndex];

                EmailMessage message = BuildEmailMessage(
                    currentObject.Subject, 
                    currentObject.Content, 
                    Configuration.SmtpAccount, 
                    currentObject.Receiver);

                int tryCount = 0;
                bool result = false;

                while (tryCount < Configuration.RetryCount)
                {
                    result = this.SnedEmail(message, server);

                    if (result) break;

                    tryCount++;
                    System.Threading.Thread.Sleep(Configuration.RetryInterval);
                }

                if (result) success++;
                currentObject.Status = result ? "已发送" : "发送失败";
                Alading.Business.ConsumerVisitService.UpdateConsumerVisit(currentObject);

                currentIndex++;
            }
        }

        public void DoAllWork()
        {
            if (InitializeTask())
            {
                while (HasNext)
                {
                    StepNext();
                    System.Threading.Thread.Sleep(1000);
                }

                state = true;
            }
            else
            {
                state = false;
            }
        }

        private EmailMessage BuildEmailMessage(string subject, string content, string from, string to)
        {
            EmailMessage emailMessage = new EmailMessage();
            emailMessage.FromAddress = new EmailAddress(from);
            emailMessage.AddToAddress(new EmailAddress(to));
            emailMessage.Subject = subject;
            emailMessage.HtmlPart = new HtmlAttachment(content);
            emailMessage.HeaderCharSet = messageEncoding;
            emailMessage.HeaderEncoding = EncodingType.Base64;
            emailMessage.HtmlPart.CharSet = messageEncoding;
            emailMessage.ContentType = messagetype;

            return emailMessage;
        }

        private bool SnedEmail(EmailMessage message, SmtpServer server)
        {
            if (message != null && server != null)
            {
                try
                {
                    message.Send(server);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
