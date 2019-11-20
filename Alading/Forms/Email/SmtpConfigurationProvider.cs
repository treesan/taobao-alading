using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace Alading.Forms.Email
{
    public class SmtpConfigurationProvider
    {
        private SmtpConfigurationProvider()
        {
        }

        private static SmtpConfiguration instance;
        private static Alading.Entity.Config smtpServer = new Alading.Entity.Config();
        private static Alading.Entity.Config smtpAccount = new Alading.Entity.Config();
        private static Alading.Entity.Config smtpPassword = new Alading.Entity.Config();
        private static Alading.Entity.Config smtpPort = new Alading.Entity.Config();
        private static Alading.Entity.Config conTimeOut = new Alading.Entity.Config();
        private static Alading.Entity.Config threadCount = new Alading.Entity.Config();
        private static Alading.Entity.Config interval = new Alading.Entity.Config();
        private static Alading.Entity.Config retryCount = new Alading.Entity.Config();
        private static Alading.Entity.Config retryInterval = new Alading.Entity.Config();

        public static SmtpConfiguration Configuration
        {
            get
            {
                if (instance == null)
                {
                    Load();
                }
                return instance;
            }
            set
            {
                if (value != null)
                {
                    instance = value;
                }
            }
        }

        private static void Load()
        {
            instance = new SmtpConfiguration();

            smtpServer = Alading.Business.ConfigService.GetConfig("SmtpServer");
            smtpAccount = Alading.Business.ConfigService.GetConfig("SmtpAccount");
            smtpPassword = Alading.Business.ConfigService.GetConfig("SmtpPassword");
            smtpPort = Alading.Business.ConfigService.GetConfig("SmtpPort");
            conTimeOut = Alading.Business.ConfigService.GetConfig("SendEmailTimeOut");
            threadCount = Alading.Business.ConfigService.GetConfig("SendEmailThreadCount");
            interval = Alading.Business.ConfigService.GetConfig("SendEmailInterval");
            retryCount = Alading.Business.ConfigService.GetConfig("SendEmailRetryCount");
            retryInterval = Alading.Business.ConfigService.GetConfig("SendEmailRetryInterval");

            instance.SmtpServer = smtpServer.ConfigValue;
            instance.SmtpAccount = smtpAccount.ConfigValue;
            instance.SmtpPassword = smtpPassword.ConfigValue;
            try
            {
                instance.SmtpPort = Convert.ToInt32(smtpPort.ConfigValue);
            }
            catch (Exception)
            {
                instance.SmtpPort = 25;
            }
            try
            {
                instance.ThreadCount = Convert.ToInt32(threadCount.ConfigValue);
            }
            catch (Exception)
            {
                instance.SmtpPort = 5;
            }
            try
            {
                instance.ConnectionTimeout = Convert.ToInt32(conTimeOut.ConfigValue);
            }
            catch (Exception)
            {
                instance.ConnectionTimeout = 10000;
            }
            try
            {
                instance.Interval = Convert.ToInt32(interval.ConfigValue);
            }
            catch (Exception)
            {
                instance.Interval = 1000;
            }
            try
            {
                instance.RetryCount = Convert.ToInt32(retryCount.ConfigValue);
            }
            catch (Exception)
            {
                instance.Interval = 5;
            }
            try
            {
                instance.RetryInterval = Convert.ToInt32(retryInterval.ConfigValue);
            }
            catch (Exception)
            {
                instance.Interval = 500;
            }
        }

        public static void Save()
        {
            if (instance == null)
            {
                instance = new SmtpConfiguration
                {
                    SmtpAccount = string.Empty,
                    SmtpPassword = string.Empty,
                    SmtpServer = string.Empty,
                    SmtpPort = 25,
                    ConnectionTimeout = 3000,
                    ThreadCount = 5,
                    Interval = 1000,
                    RetryCount = 5,
                    RetryInterval = 500,
                };
            }

            smtpServer.ConfigValue = instance.SmtpServer;
            smtpAccount.ConfigValue = instance.SmtpAccount;
            smtpPassword.ConfigValue = instance.SmtpPassword;
            smtpPort.ConfigValue = instance.SmtpPort.ToString();
            conTimeOut.ConfigValue = instance.ConnectionTimeout.ToString();
            threadCount.ConfigValue = instance.ThreadCount.ToString();
            interval.ConfigValue = instance.Interval.ToString();
            retryCount.ConfigValue = instance.RetryCount.ToString();
            retryInterval.ConfigValue = instance.RetryInterval.ToString();

            Alading.Business.ConfigService.UpdateConfig(smtpServer);
            Alading.Business.ConfigService.UpdateConfig(smtpAccount);
            Alading.Business.ConfigService.UpdateConfig(smtpPassword);
            Alading.Business.ConfigService.UpdateConfig(smtpPort);
            Alading.Business.ConfigService.UpdateConfig(conTimeOut);
            Alading.Business.ConfigService.UpdateConfig(threadCount);
            Alading.Business.ConfigService.UpdateConfig(interval);
            Alading.Business.ConfigService.UpdateConfig(retryCount);
            Alading.Business.ConfigService.UpdateConfig(retryInterval);
        }
    }
}
