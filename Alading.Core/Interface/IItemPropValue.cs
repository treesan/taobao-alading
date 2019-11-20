using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;
using System.Data;
using Alading.Taobao.Entity;

namespace Alading.Core.Interface
{
    public interface IItemPropValue
    {
        ReturnType UpdateItemPropValueDataParameters(List<PropValue> propValues);

        ReturnType AddItemPropValueSqlBulkCopy(DataTable dataTable);

        ReturnType AddItemPropValue(ItemPropValue itempropvalue);
       
        ReturnType AddItemPropValue(List<ItemPropValue> itempropvalueList);
        
        ReturnType RemoveAllItemPropValue();
       
        ReturnType RemoveItemPropValue(Func<ItemPropValue, bool> func);
              
        ReturnType RemoveItemPropValue(string itempropvalueCode);
        
        ReturnType RemoveItemPropValue(List<string> itempropvalueCodeList);

        ReturnType UpdateItemPropValueIsParent(string cid, string pid, string vid, bool value);

        ReturnType UpdateItemPropValue(ItemPropValue itempropvalue);
       
        ReturnType UpdateItemPropValue(string itempropvalueCode,ItemPropValue itempropvalue);
       
        List<ItemPropValue> GetAllItemPropValue();
      
        List<ItemPropValue> GetItemPropValue(Func<ItemPropValue, bool> func);

        ItemPropValue GetItemPropValue(string cid,string pid,string vid);
      
        List<ItemPropValue> GetItemPropValue(List<string> itempropvalueCodeList);
       
        List<ItemPropValue> GetItemPropValue(int pageIndex, int pageSize, out int rowCount);
        
        List<ItemPropValue> GetItemPropValue(Func<ItemPropValue, bool> func, int pageIndex, int pageSize, out int rowCount);

        List<string> GetItemPropvalueDownCids();

        bool IsExistedPropValueName(string cid, string pid, string vname);

        bool IsPropValueExistedCid(string cid);

        List<string> GetPropValueWhereInCids(List<string> cidlist);
        /*        
        ReturnType RemoveItemPropValue(int itempropvalueID);
        
        ReturnType RemoveItemPropValue(List<int> itempropvalueIDList);
        
        ReturnType UpdateItemPropValue(int itempropvalueID,ItemPropValue itempropvalue);
        
        List<ItemPropValue> GetItemPropValue(List<int> itempropvalueIDList);
        */
    }
}
