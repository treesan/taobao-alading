using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;
using System.Data;

namespace Alading.Core.Interface
{
    public interface IView_TradeAssembleStock
    {       
        List<View_TradeAssembleStock> GetAllView_TradeAssembleStock();
      
        List<View_TradeAssembleStock> GetView_TradeAssembleStock(Func<View_TradeAssembleStock, bool> func);

        View_TradeAssembleStock GetView_TradeAssembleStock(string AssembleCode);

        DataSet GetViewTradeAssembleDataSet(string tradeOrderCode);

    }
}
