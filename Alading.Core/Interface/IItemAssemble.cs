using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Core.Interface
{
    public interface IItemAssemble
    {
        ReturnType AddAssembleItem(AssembleItem itemAssemble);

        ReturnType AddAssembleItem(List<AssembleItem> itemAssembleList);

        ReturnType UpdateAssembleItem(AssembleItem itemAssemble);
    }
}
