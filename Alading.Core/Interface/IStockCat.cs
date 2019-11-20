using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IStockCat
    {       
        ReturnType AddStockCat(StockCat stockcat);
       
        ReturnType AddStockCat(List<StockCat> stockcatList);
        
        ReturnType RemoveAllStockCat();
       
        ReturnType RemoveStockCat(Func<StockCat, bool> func);

        ReturnType RemoveStockCat(string stockCid);

        ReturnType RemoveStockCat(List<string> stockcatCodeList);
       
        ReturnType UpdateStockCat(StockCat stockcat);

        ReturnType UpdateStockCat(string stockcatCode, StockCat stockcat);

        ReturnType UpdateStockCat(string stockcatCode, string catName);

        List<StockCat> GetAllStockCat();
      
        List<StockCat> GetStockCat(Func<StockCat, bool> func);

        List<StockCat> GetStockCat(List<string> stockcatCodeList);

        List<StockCat> GetStockCat(int pageIndex, int pageSize, out int rowCount);
        
        List<StockCat> GetStockCat(Func<StockCat, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveStockCat(int stockcatID);
        
        ReturnType RemoveStockCat(List<int> stockcatIDList);
        
        ReturnType UpdateStockCat(int stockcatID,StockCat stockcat);
        
        List<StockCat> GetStockCat(List<int> stockcatIDList);
        */

        ReturnType DeleteStockCat(string cid);
    }
}
