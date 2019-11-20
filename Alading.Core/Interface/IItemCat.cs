using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;
using System.Data;

namespace Alading.Core.Interface
{
    public interface IItemCat
    {
        bool AddItemCatSqlBulkCopy(DataTable dataTable);

        ReturnType UpdateItemCatPropTag(List<string> cidlist);

        ReturnType AddItemCat(ItemCat itemcat);
       
        ReturnType AddItemCat(List<ItemCat> itemcatList);
        
        ReturnType RemoveAllItemCat();
       
        ReturnType RemoveItemCat(Func<ItemCat, bool> func);
              
        ReturnType RemoveItemCat(string cid);

        ReturnType RemoveItemCat(List<string> cidList);

        ReturnType UpdateItemCatPropTag(string cid, bool value);
       
        ReturnType UpdateItemCat(ItemCat itemcat);

        ReturnType UpdateItemCat(string cid, ItemCat itemcat);

        List<string> GetNotDownCids();
      
        List<ItemCat> GetItemCat(Func<ItemCat, bool> func);

        List<ItemCat> GetItemCat(string parent_cid, string status);

        List<ItemCat> GetItemCat(List<string> cidList);
       
        List<ItemCat> GetItemCat(int pageIndex, int pageSize, out int rowCount);
        
        List<ItemCat> GetItemCat(Func<ItemCat, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveItemCat(int itemcatID);
        
        ReturnType RemoveItemCat(List<int> itemcatIDList);
        
        ReturnType UpdateItemCat(int itemcatID,ItemCat itemcat);
        
        List<ItemCat> GetItemCat(List<int> itemcatIDList);
        */
    }
}
