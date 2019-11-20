using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Core.Enum;
using Alading.DataProvider;
using Alading.Entity;

namespace Alading.Business
{
    public static class RateContentService
    {
        /*添加*/
        public static ReturnType AddRateContent(RateContent rateContent)
        {
            return DataProviderClass.Instance().AddRateContent(rateContent);
        }

        /*删除*/
        public static ReturnType RemoveRateContent(RateContent rateContent)
        {
            return DataProviderClass.Instance().RemoveRateContent(rateContent);
        }

        /*获取*/
        public static List<RateContent> GetRateContent(string result)
        {
            return DataProviderClass.Instance().GetRateContent(result);
        }
    }
}
