using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Core.Interface
{
    public interface IRateContent
    {
        ReturnType AddRateContent(RateContent rateContent);

        ReturnType RemoveRateContent(RateContent rateContent);

        List<RateContent> GetRateContent(string result);
    }
}
