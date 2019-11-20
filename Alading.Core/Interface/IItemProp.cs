using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;
using System.Data;

namespace Alading.Core.Interface
{
    public interface IItemProp
    {
        ReturnType AddItemPropSqlBulkCopy(DataTable dataTable);

        ReturnType AddItemProp(ItemProp itemprop);
       
        ReturnType AddItemProp(List<ItemProp> itempropList);
        
        ReturnType RemoveAllItemProp();
       
        ReturnType RemoveItemProp(Func<ItemProp, bool> func);

        ReturnType RemoveItemProp(string cid);
        
        ReturnType RemoveItemProp(List<string> itempropCodeList);
       
        ReturnType UpdateItemProp(ItemProp itemprop);
       
        ReturnType UpdateItemProp(string itempropCode,ItemProp itemprop);
       
        List<ItemProp> GetAllItemProp();
      
        List<ItemProp> GetItemProp(Func<ItemProp, bool> func);

        ItemProp GetItemProp(string cid, string pid);
      
        List<ItemProp> GetItemProp(List<string> itempropCodeList);
       
        List<ItemProp> GetItemProp(int pageIndex, int pageSize, out int rowCount);
        
        List<ItemProp> GetItemProp(Func<ItemProp, bool> func, int pageIndex, int pageSize, out int rowCount);

        List<string> GetKeyPropPid(string cid);

        bool IsPropExistedCid(string cid);

        List<string> GetPropWhereInCids(List<string> cidlist);
        /*        
        ReturnType RemoveItemProp(int itempropID);
        
        ReturnType RemoveItemProp(List<int> itempropIDList);
        
        ReturnType UpdateItemProp(int itempropID,ItemProp itemprop);
        
        List<ItemProp> GetItemProp(List<int> itempropIDList);
        */
    }
}
