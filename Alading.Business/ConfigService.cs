using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class ConfigService
    {
        public static ReturnType AddConfig(Config config)
        {
            return DataProviderClass.Instance().AddConfig(config);
        }

        public static ReturnType AddConfig(List<Config> configList)
        {
            return DataProviderClass.Instance().AddConfig(configList);
        }

        public static ReturnType RemoveAllConfig()
        {
            return DataProviderClass.Instance().RemoveAllConfig();
        }

        public static ReturnType RemoveConfig(Func<Config, bool> func)
        {
            return DataProviderClass.Instance().RemoveConfig(func);
        }

        public static ReturnType RemoveConfig(string configCode)
        {
            return DataProviderClass.Instance().RemoveConfig(configCode);
        }

        /*
        public static ReturnType RemoveConfig(int configID)
        {
            return DataProviderClass.Instance().RemoveConfig(configID);
        }
        */

        public static ReturnType RemoveConfig(List<string> configCodeList)
        {
            return DataProviderClass.Instance().RemoveConfig(configCodeList);
        }

        /*
        public static ReturnType RemoveConfig(List<int> configIDList)
        {
            return DataProviderClass.Instance().RemoveConfig(configIDList);
        }
        */

        public static ReturnType UpdateConfig(Config config)
        {
            return DataProviderClass.Instance().UpdateConfig(config);
        }

        public static ReturnType UpdateConfig(string configCode, Config config)
        {
            return DataProviderClass.Instance().UpdateConfig(configCode, config);
        }

        /*
        public static ReturnType UpdateConfig(int configID, Config config)
        {
            return DataProviderClass.Instance().UpdateConfig(configID, config);
        }
        */

        public static List<Config> GetAllConfig()
        {
            return DataProviderClass.Instance().GetAllConfig();
        }

        public static List<Config> GetConfig(Func<Config, bool> func)
        {
            return DataProviderClass.Instance().GetConfig(func);
        }

        public static Config GetConfig(string configCode)
        {
            return DataProviderClass.Instance().GetConfig(configCode);
        }

        /*
        public static Config GetConfig(int configID)
        {
            return DataProviderClass.Instance().GetConfig(configID);
        }
        */

        public static List<Config> GetConfig(List<string> configCodeList)
        {
            return DataProviderClass.Instance().GetConfig(configCodeList);
        }

        /*
        public static List<Config> GetConfig(List<int> configIDList)
        {
            return DataProviderClass.Instance().GetConfig(configIDList);
        }
        */

        public static List<Config> GetConfig(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetConfig(pageIndex, pageSize, out rowCount);
        }

        public static List<Config> GetConfig(Func<Config, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetConfig(func, pageIndex, pageSize, out rowCount);
        }
    }
}
