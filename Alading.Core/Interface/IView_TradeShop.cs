using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IView_TradeShop
    {
        List<View_TradeShop> GetAllView_TradeShop();

        List<View_TradeShop> GetView_TradeShop(Func<View_TradeShop, bool> func);

        List<View_TradeShop> GetView_TradeShop(List<string> view_tradeshopCodeList);

        List<View_TradeShop> GetView_TradeShop(int pageIndex, int pageSize, out int rowCount);

        List<View_TradeShop> GetView_TradeShop(Func<View_TradeShop, bool> func, int pageIndex, int pageSize, out int rowCount);

        /*        
        ReturnType RemoveView_TradeShop(int view_tradeshopID);
        
        ReturnType RemoveView_TradeShop(List<int> view_tradeshopIDList);
        
        ReturnType UpdateView_TradeShop(int view_tradeshopID,View_TradeShop view_tradeshop);
        
        List<View_TradeShop> GetView_TradeShop(List<int> view_tradeshopIDList);
        */
    }
}
