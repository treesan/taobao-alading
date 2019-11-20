using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IStockUnitGroup
    {       
        ReturnType AddStockUnitGroup(StockUnitGroup stockunitgroup,StockUnit unit);

        bool IsCodeOnly(string unitCode, string unitGroupCode);

        ReturnType AddStockUnitGroup(List<StockUnitGroup> stockunitgroupList);
        
        ReturnType RemoveAllStockUnitGroup();
       
        ReturnType RemoveStockUnitGroup(Func<StockUnitGroup, bool> func);
              
        ReturnType RemoveStockUnitGroup(string stockunitgroupCode);
        
        ReturnType RemoveStockUnitGroup(List<string> stockunitgroupCodeList);
       
        ReturnType UpdateStockUnitGroup(StockUnitGroup stockunitgroup);
       
        ReturnType UpdateStockUnitGroup(string stockunitgroupCode,StockUnitGroup stockunitgroup);
       
        List<StockUnitGroup> GetAllStockUnitGroup();
      
        List<StockUnitGroup> GetStockUnitGroup(Func<StockUnitGroup, bool> func);
      
        List<StockUnitGroup> GetStockUnitGroup(List<string> stockunitgroupCodeList);
       
        List<StockUnitGroup> GetStockUnitGroup(int pageIndex, int pageSize, out int rowCount);
        
        List<StockUnitGroup> GetStockUnitGroup(Func<StockUnitGroup, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveStockUnitGroup(int stockunitgroupID);
        
        ReturnType RemoveStockUnitGroup(List<int> stockunitgroupIDList);
        
        ReturnType UpdateStockUnitGroup(int stockunitgroupID,StockUnitGroup stockunitgroup);
        
        List<StockUnitGroup> GetStockUnitGroup(List<int> stockunitgroupIDList);
        */
    }
}
