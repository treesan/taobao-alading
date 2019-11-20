using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Alading.Forms.Email
{
    public partial class SmtpSetting : DevExpress.XtraEditors.XtraForm
    {
        public SmtpSetting()
        {
            InitializeComponent();
        }

        public SmtpConfiguration SmtpConfiguration
        {
            get
            {
                SmtpConfiguration config = new SmtpConfiguration
                {
                    SmtpServer = txSmtp.Text,
                    SmtpPort = Convert.ToInt32(spPort.Value),
                    SmtpAccount = txAccount.Text,
                    SmtpPassword = txPassword.Text,
                    ThreadCount = Convert.ToInt32(spThread.Value),
                    RetryCount = Convert.ToInt32(spTry.Value),
                    RetryInterval = Convert.ToInt32(spRetryInterval.Value),
                    ConnectionTimeout = Convert.ToInt32(spTimeout.Value),
                    Interval = Convert.ToInt32(spSendInterval.Value),
                };

                return config;
            }

            set
            {
                if (value != null)
                {
                    txSmtp.Text = value.SmtpServer;
                    spPort.Value = Convert.ToDecimal(value.SmtpPort);
                    txAccount.Text = value.SmtpAccount;
                    txPassword.Text = value.SmtpPassword;
                    spThread.Value = Convert.ToDecimal(value.ThreadCount);
                    spTry.Value = Convert.ToDecimal(value.RetryCount);
                    spTimeout.Value = Convert.ToDecimal(value.ConnectionTimeout);
                    spRetryInterval.Value = Convert.ToDecimal(value.RetryInterval);
                    spSendInterval.Value = Convert.ToDecimal(value.Interval);
                }
            }
        }
    }
}