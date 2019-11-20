using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;
using System.Data;

namespace Alading.Business
{
    public static class ItemPropService
    {
        public static ReturnType AddItemPropSqlBulkCopy(DataTable dataTable)
        {
            return DataProviderClass.Instance().AddItemPropSqlBulkCopy(dataTable);
        }

        public static ReturnType AddItemProp(ItemProp itemprop)
        {
            return DataProviderClass.Instance().AddItemProp(itemprop);
        }

        public static ReturnType AddItemProp(List<ItemProp> itempropList)
        {
            return DataProviderClass.Instance().AddItemProp(itempropList);
        }
    
        public static ReturnType RemoveAllItemProp()
        {
            return DataProviderClass.Instance().RemoveAllItemProp();
        }
    
        public static ReturnType RemoveItemProp(Func<ItemProp, bool> func)
        {
            return DataProviderClass.Instance().RemoveItemProp(func);
        }

        public static ReturnType RemoveItemProp(string cid)
        {
            return DataProviderClass.Instance().RemoveItemProp(cid);
        }       
        
        /*
        public static ReturnType RemoveItemProp(int itempropID)
        {
            return DataProviderClass.Instance().RemoveItemProp(itempropID);
        }
        */
    
        public static ReturnType RemoveItemProp(List<string> itempropCodeList)
        {
            return DataProviderClass.Instance().RemoveItemProp(itempropCodeList);
        }        
        
        /*
        public static ReturnType RemoveItemProp(List<int> itempropIDList)
        {
            return DataProviderClass.Instance().RemoveItemProp(itempropIDList);
        }
        */
    
        public static ReturnType UpdateItemProp(ItemProp itemprop)
        {
            return DataProviderClass.Instance().UpdateItemProp(itemprop);
        }
    
        public static ReturnType UpdateItemProp(string itempropCode, ItemProp itemprop)
        {
            return DataProviderClass.Instance().UpdateItemProp(itempropCode, itemprop);
        }
        
        /*
        public static ReturnType UpdateItemProp(int itempropID, ItemProp itemprop)
        {
            return DataProviderClass.Instance().UpdateItemProp(itempropID, itemprop);
        }
        */
    
        public static List<ItemProp> GetAllItemProp()
        {
            return DataProviderClass.Instance().GetAllItemProp();
        }
    
        public static List<ItemProp> GetItemProp(Func<ItemProp, bool> func)
        {
            return DataProviderClass.Instance().GetItemProp(func);
        }

        public static ItemProp GetItemProp(string cid, string pid)
        {
            return DataProviderClass.Instance().GetItemProp(cid, pid);
        }

        public static ItemProp GetItemProp(string itempropCode)
        {
            return DataProviderClass.Instance().GetItemProp(itempropCode);
        }
        
        /*
        public static ItemProp GetItemProp(int itempropID)
        {
            return DataProviderClass.Instance().GetItemProp(itempropID);
        }
        */
    
        public static List<ItemProp> GetItemProp(List<string> itempropCodeList)
        {
            return DataProviderClass.Instance().GetItemProp(itempropCodeList);
        }
        
        /*
        public static List<ItemProp> GetItemProp(List<int> itempropIDList)
        {
            return DataProviderClass.Instance().GetItemProp(itempropIDList);
        }
        */
    
        public static List<ItemProp> GetItemProp(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetItemProp(pageIndex, pageSize, out rowCount);
        }
        
        public static List<ItemProp> GetItemProp(Func<ItemProp, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetItemProp(func, pageIndex, pageSize, out rowCount);
        }

        /// <summary>
        /// 获取类目cid下的关键属性的pid列表
        /// </summary>
        public static List<string> GetKeyPropPid(string cid)
        {
            return DataProviderClass.Instance().GetKeyPropPid(cid);
        }

        public static bool IsPropExistedCid(string cid)
         {
             return DataProviderClass.Instance().IsPropExistedCid(cid);
         }

        public static List<string> GetPropWhereInCids(List<string> cidlist)
         {
             return DataProviderClass.Instance().GetPropWhereInCids(cidlist);
         }
    }
}
