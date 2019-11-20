using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IStockLayout
    {
        View_HouseLayout GetViewHouseLayout(string layoutCode);

        ReturnType AddStockLayout(StockLayout stocklayout);
       
        ReturnType AddStockLayout(List<StockLayout> stocklayoutList);
        
        ReturnType RemoveAllStockLayout();
       
        ReturnType RemoveStockLayout(Func<StockLayout, bool> func);
              
        ReturnType RemoveStockLayout(string stocklayoutCode);
        
        ReturnType RemoveStockLayout(List<string> stocklayoutCodeList);
       
        ReturnType UpdateStockLayout(StockLayout stocklayout);
       
        ReturnType UpdateStockLayout(string stocklayoutCode,StockLayout stocklayout);
       
        List<StockLayout> GetAllStockLayout();
      
        List<StockLayout> GetStockLayout(Func<StockLayout, bool> func);
      
        List<StockLayout> GetStockLayout(List<string> stocklayoutCodeList);
       
        List<StockLayout> GetStockLayout(int pageIndex, int pageSize, out int rowCount);
        
        List<StockLayout> GetStockLayout(Func<StockLayout, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveStockLayout(int stocklayoutID);
        
        ReturnType RemoveStockLayout(List<int> stocklayoutIDList);
        
        ReturnType UpdateStockLayout(int stocklayoutID,StockLayout stocklayout);
        
        List<StockLayout> GetStockLayout(List<int> stocklayoutIDList);
        */
    }
}
