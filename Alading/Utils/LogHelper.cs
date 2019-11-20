using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "App.config", Watch = true)]

namespace Alading.Utils
{
    /// <summary>
    /// 日志操作
    /// </summary>
    public static class LogHelper
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger("AladingLog");
        public static void Debug(string message)
        {            
            if (log.IsDebugEnabled)
            {
                log.Debug("调试信息：\r\n     " + message + "\r\n\r\n");
            }
        }
        public static void Info(string message)
        {
            if (log.IsInfoEnabled)
            {
                log.Info("信息：\r\n     " + message + "\r\n\r\n");
            }
        }
        public static void Error(string message)
        {
            if (log.IsErrorEnabled)
            {
                log.Error("错误信息：\r\n      " + message + "\r\n\r\n");
            }
        }
    }
}
