using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alading.Forms.Email
{
    [Serializable]
    public class SmtpConfiguration
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }

        public string SmtpAccount { get; set; }
        public string SmtpPassword { get; set; }

        public int Interval { get; set; }
        public int ConnectionTimeout { get; set; }
        public int ThreadCount { get; set; }
        public int RetryCount { get; set; }
        public int RetryInterval { get; set; }
    }
}
