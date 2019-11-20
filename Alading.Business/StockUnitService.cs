using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class StockUnitService
    {
        #region Tax
        public static ReturnType AddTax(Tax tax)
        {
            return DataProviderClass.Instance().AddTax(tax);
        }
        public static ReturnType UpdateTax(Tax tax)
        {
            return DataProviderClass.Instance().UpdateTax(tax);
        }
        public static List<Tax> GetTax(Func<Tax, bool> func)
        {
            return DataProviderClass.Instance().GetTax(func);
        }
        public static List<Tax> GetAllTax()
        {
            return DataProviderClass.Instance().GetAllTax();
        }
        #endregion


        public static ReturnType AddStockUnit(StockUnit stockunit)
        {
            return DataProviderClass.Instance().AddStockUnit(stockunit);
        }

        public static ReturnType AddStockUnit(List<StockUnit> stockunitList)
        {
            return DataProviderClass.Instance().AddStockUnit(stockunitList);
        }
    
        public static ReturnType RemoveAllStockUnit()
        {
            return DataProviderClass.Instance().RemoveAllStockUnit();
        }
    
        public static ReturnType RemoveStockUnit(Func<StockUnit, bool> func)
        {
            return DataProviderClass.Instance().RemoveStockUnit(func);
        }
        
        public static ReturnType RemoveStockUnit(string stockunitCode)
        {
            return DataProviderClass.Instance().RemoveStockUnit(stockunitCode);
        }       
        
        /*
        public static ReturnType RemoveStockUnit(int stockunitID)
        {
            return DataProviderClass.Instance().RemoveStockUnit(stockunitID);
        }
        */
    
        public static ReturnType RemoveStockUnit(List<string> stockunitCodeList)
        {
            return DataProviderClass.Instance().RemoveStockUnit(stockunitCodeList);
        }        
        
        /*
        public static ReturnType RemoveStockUnit(List<int> stockunitIDList)
        {
            return DataProviderClass.Instance().RemoveStockUnit(stockunitIDList);
        }
        */
    
        public static ReturnType UpdateStockUnit(StockUnit stockunit)
        {
            return DataProviderClass.Instance().UpdateStockUnit(stockunit);
        }
    
        public static ReturnType UpdateStockUnit(string stockunitCode, StockUnit stockunit)
        {
            return DataProviderClass.Instance().UpdateStockUnit(stockunitCode, stockunit);
        }
        
        /*
        public static ReturnType UpdateStockUnit(int stockunitID, StockUnit stockunit)
        {
            return DataProviderClass.Instance().UpdateStockUnit(stockunitID, stockunit);
        }
        */
    
        public static List<StockUnit> GetAllStockUnit()
        {
            return DataProviderClass.Instance().GetAllStockUnit();
        }
    
        public static List<StockUnit> GetStockUnit(Func<StockUnit, bool> func)
        {
            return DataProviderClass.Instance().GetStockUnit(func);
        }
    
        public static List<StockUnit> GetStockUnit(string stockGroupUnitCode)
        {
            return DataProviderClass.Instance().GetStockUnit(stockGroupUnitCode);
        }

        /*
        public static StockUnit GetStockUnit(int stockunitID)
        {
            return DataProviderClass.Instance().GetStockUnit(stockunitID);
        }
        */
    
        public static List<StockUnit> GetStockUnit(List<string> stockunitCodeList)
        {
            return DataProviderClass.Instance().GetStockUnit(stockunitCodeList);
        }
        
        /*
        public static List<StockUnit> GetStockUnit(List<int> stockunitIDList)
        {
            return DataProviderClass.Instance().GetStockUnit(stockunitIDList);
        }
        */
    
        public static List<StockUnit> GetStockUnit(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockUnit(pageIndex, pageSize, out rowCount);
        }
        
        public static List<StockUnit> GetStockUnit(Func<StockUnit, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockUnit(func, pageIndex, pageSize, out rowCount);
        }

        public static List<View_GroupUnit> GetAllView_GroupUnit()
        {
            return DataProviderClass.Instance().GetAllView_GroupUnit();
        }

        public static List<View_GroupUnit> GetView_GroupUnit(Func<View_GroupUnit, bool> func)
        {
            return DataProviderClass.Instance().GetView_GroupUnit(func);
        }
    }
}
