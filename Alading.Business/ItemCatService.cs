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
    public static class ItemCatService 
    {
        public static ReturnType UpdateItemCatPropTag(List<string> cidlist)
        {
            return DataProviderClass.Instance().UpdateItemCatPropTag(cidlist);
        }

        /// <summary>
        /// 批量添加ItemCat，添加之前执行清空ItemCat
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static bool AddItemCatSqlBulkCopy(DataTable dataTable)
        {
            return DataProviderClass.Instance().AddItemCatSqlBulkCopy(dataTable);
        }

        public static ReturnType AddItemCat(ItemCat itemcat)
        {
            return DataProviderClass.Instance().AddItemCat(itemcat);
        }

        public static ReturnType AddItemCat(List<ItemCat> itemcatList)
        {
            return DataProviderClass.Instance().AddItemCat(itemcatList);
        }
    
        public static ReturnType RemoveAllItemCat()
        {
            return DataProviderClass.Instance().RemoveAllItemCat();
        }
    
        public static ReturnType RemoveItemCat(Func<ItemCat, bool> func)
        {
            return DataProviderClass.Instance().RemoveItemCat(func);
        }
        
        public static ReturnType RemoveItemCat(string cid)
        {
            return DataProviderClass.Instance().RemoveItemCat(cid);
        }       
        
        /*
        public static ReturnType RemoveItemCat(int itemcatID)
        {
            return DataProviderClass.Instance().RemoveItemCat(itemcatID);
        }
        */
    
        public static ReturnType RemoveItemCat(List<string> cidList)
        {
            return DataProviderClass.Instance().RemoveItemCat(cidList);
        }        
        
        /*
        public static ReturnType RemoveItemCat(List<int> itemcatIDList)
        {
            return DataProviderClass.Instance().RemoveItemCat(itemcatIDList);
        }
        */
    
        public static ReturnType UpdateItemCat(ItemCat itemcat)
        {
            return DataProviderClass.Instance().UpdateItemCat(itemcat);
        }

        public static ReturnType UpdateItemCat(string itemcatCode, ItemCat itemcat)
        {
            return DataProviderClass.Instance().UpdateItemCat(itemcatCode, itemcat);
        }

        public static ReturnType UpdateItemCatPropTag(string cid, bool value)
         {
             return DataProviderClass.Instance().UpdateItemCatPropTag(cid, value);
         }

        /*
        public static ReturnType UpdateItemCat(int itemcatID, ItemCat itemcat)
        {
            return DataProviderClass.Instance().UpdateItemCat(itemcatID, itemcat);
        }
        */

        /// <summary>
        /// 获取cidlist下的所有类目及父类目
        /// </summary>
        /// <param name="ListItemCatCid"></param>
        /// <returns></returns>
        public static List<ItemCat> GetAllItemCat(List<string> cidlist)
        {
            return DataProviderClass.Instance().GetAllItemCat(cidlist);
        }
   
        /// <summary>
        /// 获取尚未下载属性的类目
        /// </summary>
        /// <returns></returns>
        public static List<string> GetNotDownPropCids()
        {
            return DataProviderClass.Instance().GetNotDownCids();
        }

        /// <summary>
        /// 获取所有ItemCat，按照cid排序
        /// </summary>
        /// <returns></returns>
        public static List<ItemCat> GetAllItemCat()
        {
            return DataProviderClass.Instance().GetAllItemCat();
        }
    
        public static List<ItemCat> GetItemCat(Func<ItemCat, bool> func)
        {
            return DataProviderClass.Instance().GetItemCat(func);
        }

        public static ItemCat GetItemCat(string cid)
        {
            return DataProviderClass.Instance().GetItemCat(cid);
        }

        public static List<ItemCat> GetItemCat(string parent_cid, string status)
        {
            return DataProviderClass.Instance().GetItemCat(parent_cid, status);
        }
        
        /*
        public static ItemCat GetItemCat(int itemcatID)
        {
            return DataProviderClass.Instance().GetItemCat(itemcatID);
        }
        */

        public static List<ItemCat> GetItemCat(List<string> cidList)
        {
            return DataProviderClass.Instance().GetItemCat(cidList);
        }
        
        /*
        public static List<ItemCat> GetItemCat(List<int> itemcatIDList)
        {
            return DataProviderClass.Instance().GetItemCat(itemcatIDList);
        }
        */
    
        public static List<ItemCat> GetItemCat(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetItemCat(pageIndex, pageSize, out rowCount);
        }
        
        public static List<ItemCat> GetItemCat(Func<ItemCat, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetItemCat(func, pageIndex, pageSize, out rowCount);
        }
    }
}
