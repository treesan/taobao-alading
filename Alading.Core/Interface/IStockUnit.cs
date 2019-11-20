using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IStockUnit
    {
        #region Tax
        ReturnType AddTax(Tax tax);
        ReturnType UpdateTax(Tax tax);
        List<Tax> GetTax(Func<Tax, bool> func);
        List<Tax> GetAllTax();
        #endregion

        ReturnType AddStockUnit(StockUnit stockunit);
       
        ReturnType AddStockUnit(List<StockUnit> stockunitList);
        
        ReturnType RemoveAllStockUnit();
       
        ReturnType RemoveStockUnit(Func<StockUnit, bool> func);
              
        ReturnType RemoveStockUnit(string stockunitCode);
        
        ReturnType RemoveStockUnit(List<string> stockunitCodeList);
       
        ReturnType UpdateStockUnit(StockUnit stockunit);
       
        ReturnType UpdateStockUnit(string stockunitCode,StockUnit stockunit);
       
        List<StockUnit> GetAllStockUnit();
      
        List<StockUnit> GetStockUnit(Func<StockUnit, bool> func);
      
        List<StockUnit> GetStockUnit(List<string> stockunitCodeList);
       
        List<StockUnit> GetStockUnit(int pageIndex, int pageSize, out int rowCount);
        
        List<StockUnit> GetStockUnit(Func<StockUnit, bool> func, int pageIndex, int pageSize, out int rowCount);

        #region View_GroupUnit

        List<View_GroupUnit> GetAllView_GroupUnit();

        List<View_GroupUnit> GetView_GroupUnit(Func<View_GroupUnit, bool> func);

        #endregion

        /*        
        ReturnType RemoveStockUnit(int stockunitID);
        
        ReturnType RemoveStockUnit(List<int> stockunitIDList);
        
        ReturnType UpdateStockUnit(int stockunitID,StockUnit stockunit);
        
        List<StockUnit> GetStockUnit(List<int> stockunitIDList);
        */
    }
}
