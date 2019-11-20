using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class StockUnitGroupService
    {

        public static ReturnType AddStockUnitGroup(StockUnitGroup stockunitgroup, StockUnit unit)
        {
            return DataProviderClass.Instance().AddStockUnitGroup(stockunitgroup,unit);
        }

        public static bool IsCodeOnly(string unitCode, string unitGroupCode)
        {
            return DataProviderClass.Instance().IsCodeOnly(unitCode, unitGroupCode);
        }

        public static ReturnType AddStockUnitGroup(List<StockUnitGroup> stockunitgroupList)
        {
            return DataProviderClass.Instance().AddStockUnitGroup(stockunitgroupList);
        }
    
        public static ReturnType RemoveAllStockUnitGroup()
        {
            return DataProviderClass.Instance().RemoveAllStockUnitGroup();
        }
    
        public static ReturnType RemoveStockUnitGroup(Func<StockUnitGroup, bool> func)
        {
            return DataProviderClass.Instance().RemoveStockUnitGroup(func);
        }
        
        public static ReturnType RemoveStockUnitGroup(string stockunitgroupCode)
        {
            return DataProviderClass.Instance().RemoveStockUnitGroup(stockunitgroupCode);
        }       
        
        /*
        public static ReturnType RemoveStockUnitGroup(int stockunitgroupID)
        {
            return DataProviderClass.Instance().RemoveStockUnitGroup(stockunitgroupID);
        }
        */
    
        public static ReturnType RemoveStockUnitGroup(List<string> stockunitgroupCodeList)
        {
            return DataProviderClass.Instance().RemoveStockUnitGroup(stockunitgroupCodeList);
        }        
        
        /*
        public static ReturnType RemoveStockUnitGroup(List<int> stockunitgroupIDList)
        {
            return DataProviderClass.Instance().RemoveStockUnitGroup(stockunitgroupIDList);
        }
        */
    
        public static ReturnType UpdateStockUnitGroup(StockUnitGroup stockunitgroup)
        {
            return DataProviderClass.Instance().UpdateStockUnitGroup(stockunitgroup);
        }

        /*
        public static ReturnType UpdateStockUnitGroup(string stockunitgroupCode, StockUnitGroup stockunitgroup)
        {
            return DataProviderClass.Instance().UpdateStockUnitGroup(stockunitgroupCode, stockunitgroup);
        }
        */

        public static ReturnType UpdateStockUnitGroup(string stockUnitGroupName, StockUnitGroup stockunitgroup)
        {
            return DataProviderClass.Instance().UpdateStockUnitGroup(stockUnitGroupName, stockunitgroup);
        }
        
        /*
        public static ReturnType UpdateStockUnitGroup(int stockunitgroupID, StockUnitGroup stockunitgroup)
        {
            return DataProviderClass.Instance().UpdateStockUnitGroup(stockunitgroupID, stockunitgroup);
        }
        */

        public static List<StockUnitGroup> GetAllStockUnitGroup()
        {
            return DataProviderClass.Instance().GetAllStockUnitGroup();
        }
    
        public static List<StockUnitGroup> GetStockUnitGroup(Func<StockUnitGroup, bool> func)
        {
            return DataProviderClass.Instance().GetStockUnitGroup(func);
        }

        /*
        public static StockUnitGroup GetStockUnitGroup(string stockunitgroupCode)
        {
            return DataProviderClass.Instance().GetStockUnitGroup(stockunitgroupCode);
        }
        */

        public static StockUnitGroup GetStockUnitGroup(string stockunitgroupName)
        {
            return DataProviderClass.Instance().GetStockUnitGroup(stockunitgroupName);
        }

        /*
        public static StockUnitGroup GetStockUnitGroup(int stockunitgroupID)
        {
            return DataProviderClass.Instance().GetStockUnitGroup(stockunitgroupID);
        }
        */
    
        public static List<StockUnitGroup> GetStockUnitGroup(List<string> stockunitgroupCodeList)
        {
            return DataProviderClass.Instance().GetStockUnitGroup(stockunitgroupCodeList);
        }
        
        /*
        public static List<StockUnitGroup> GetStockUnitGroup(List<int> stockunitgroupIDList)
        {
            return DataProviderClass.Instance().GetStockUnitGroup(stockunitgroupIDList);
        }
        */
    
        public static List<StockUnitGroup> GetStockUnitGroup(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockUnitGroup(pageIndex, pageSize, out rowCount);
        }
        
        public static List<StockUnitGroup> GetStockUnitGroup(Func<StockUnitGroup, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetStockUnitGroup(func, pageIndex, pageSize, out rowCount);
        }
    }
}
