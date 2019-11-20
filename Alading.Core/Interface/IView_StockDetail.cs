using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;

namespace Alading.Core.Interface
{
    public interface IView_StockDetail
    {
        List<View_StockDetail> GetAllView_StockDetail();

        List<View_StockDetail> GetView_StockDetail(Func<View_StockDetail, bool> func);       
       
    }
}
