using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IConfig
    {
        ReturnType AddConfig(Config config);

        ReturnType AddConfig(List<Config> configList);

        ReturnType RemoveAllConfig();

        ReturnType RemoveConfig(Func<Config, bool> func);

        ReturnType RemoveConfig(string configCode);

        ReturnType RemoveConfig(List<string> configCodeList);

        ReturnType UpdateConfig(Config config);

        ReturnType UpdateConfig(string configCode, Config config);

        List<Config> GetAllConfig();

        List<Config> GetConfig(Func<Config, bool> func);

        List<Config> GetConfig(List<string> configCodeList);

        List<Config> GetConfig(int pageIndex, int pageSize, out int rowCount);

        List<Config> GetConfig(Func<Config, bool> func, int pageIndex, int pageSize, out int rowCount);
    }
}
