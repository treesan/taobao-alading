using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;
using System.Data;
using Alading.Taobao.Entity;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace Alading.Business
{
    public static class ItemPropValueService
    {
        static ICacheManager cache = CacheFactory.GetCacheManager();

        public static ReturnType UpdateItemPropValueDataParameters(List<PropValue> propValues)
        {
            return DataProviderClass.Instance().UpdateItemPropValueDataParameters(propValues);
        }

        public static ReturnType AddItemPropValueSqlBulkCopy(DataTable dataTable)
        {
            return DataProviderClass.Instance().AddItemPropValueSqlBulkCopy(dataTable);
        }

        public static ItemPropValue GetItemPropValue(string cid, string pid, string vid)
        {            
            return DataProviderClass.Instance().GetItemPropValue(cid, pid, vid);
        }
        public static ReturnType AddItemPropValue(ItemPropValue itempropvalue)
        {
            return DataProviderClass.Instance().AddItemPropValue(itempropvalue);
        }

        public static ReturnType AddItemPropValue(List<ItemPropValue> itempropvalueList)
        {
            return DataProviderClass.Instance().AddItemPropValue(itempropvalueList);
        }

        public static ReturnType RemoveAllItemPropValue()
        {
            return DataProviderClass.Instance().RemoveAllItemPropValue();
        }

        public static ReturnType RemoveItemPropValue(Func<ItemPropValue, bool> func)
        {
            return DataProviderClass.Instance().RemoveItemPropValue(func);
        }

        public static ReturnType RemoveItemPropValue(string itempropvalueCode)
        {
            return DataProviderClass.Instance().RemoveItemPropValue(itempropvalueCode);
        }

        /*
        public static ReturnType RemoveItemPropValue(int itempropvalueID)
        {
            return DataProviderClass.Instance().RemoveItemPropValue(itempropvalueID);
        }
        */

        public static ReturnType RemoveItemPropValue(List<string> itempropvalueCodeList)
        {
            return DataProviderClass.Instance().RemoveItemPropValue(itempropvalueCodeList);
        }

        /*
        public static ReturnType RemoveItemPropValue(List<int> itempropvalueIDList)
        {
            return DataProviderClass.Instance().RemoveItemPropValue(itempropvalueIDList);
        }
        */

        /// <summary>
        /// 根据cid,pid,vid查询更新Itempropvalue的is_parent值为value
        /// </summary>
        public static ReturnType UpdateItemPropValueIsParent(string cid, string pid, string vid, bool value)
        {
            return DataProviderClass.Instance().UpdateItemPropValueIsParent(cid, pid, vid, value);
        }

        public static ReturnType UpdateItemPropValue(ItemPropValue itempropvalue)
        {
            return DataProviderClass.Instance().UpdateItemPropValue(itempropvalue);
        }

        public static ReturnType UpdateItemPropValue(string itempropvalueCode, ItemPropValue itempropvalue)
        {
            return DataProviderClass.Instance().UpdateItemPropValue(itempropvalueCode, itempropvalue);
        }

        /*
        public static ReturnType UpdateItemPropValue(int itempropvalueID, ItemPropValue itempropvalue)
        {
            return DataProviderClass.Instance().UpdateItemPropValue(itempropvalueID, itempropvalue);
        }
        */

        public static List<ItemPropValue> GetAllItemPropValue()
        {
            return DataProviderClass.Instance().GetAllItemPropValue();
        }

        public static List<ItemPropValue> GetItemPropValue(Func<ItemPropValue, bool> func)
        {
            return DataProviderClass.Instance().GetItemPropValue(func);
        }

        public static ItemPropValue GetItemPropValue(string itempropvalueCode)
        {
            return DataProviderClass.Instance().GetItemPropValue(itempropvalueCode);
        }

        /*
        public static ItemPropValue GetItemPropValue(int itempropvalueID)
        {
            return DataProviderClass.Instance().GetItemPropValue(itempropvalueID);
        }
        */

        public static List<ItemPropValue> GetItemPropValue(List<string> itempropvalueCodeList)
        {
            return DataProviderClass.Instance().GetItemPropValue(itempropvalueCodeList);
        }

        /*
        public static List<ItemPropValue> GetItemPropValue(List<int> itempropvalueIDList)
        {
            return DataProviderClass.Instance().GetItemPropValue(itempropvalueIDList);
        }
        */

        public static List<ItemPropValue> GetItemPropValue(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetItemPropValue(pageIndex, pageSize, out rowCount);
        }

        public static List<ItemPropValue> GetItemPropValue(Func<ItemPropValue, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetItemPropValue(func, pageIndex, pageSize, out rowCount);
        }

        public static List<View_ItemPropValue> GetAllView_ItemPropValue()
        {
            return DataProviderClass.Instance().GetAllView_ItemPropValue();
        }

        public static List<View_ItemPropValue> GetView_ItemPropValue(Func<View_ItemPropValue, bool> func)
        {
            return DataProviderClass.Instance().GetView_ItemPropValue(func);
        }

        public static List<View_ItemPropValue> GetView_ItemPropValue(string cid)
        {
            return DataProviderClass.Instance().GetView_ItemPropValue(cid);
        }

        public static View_ItemPropValue GetView_ItemPropValue(string cid, string pid, string vid)
        {
            return DataProviderClass.Instance().GetView_ItemPropValue(cid, pid, vid);
        }

        /// <summary>
        /// 用存储过程获取cid下的属性及值，后两个参数均置为-1可获取所有属性及属性值
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="parentPid"></param>
        /// <param name="parentVid"></param>
        /// <returns></returns>
        public static List<View_ItemPropValue> GetView_ItemPropValueList(string cid, string parentPid, string parentVid)
        {
            string cacheKey = string.Format("{0}-{1}-{2}", cid, parentPid, parentVid);
            if (cache[cacheKey] != null)
            {
                return cache[cacheKey] as List<View_ItemPropValue>;
            }
            else
            {
                List<View_ItemPropValue> viewItemPropValues = DataProviderClass.Instance().GetView_ItemPropValueList(cid, parentPid, parentVid);
                if (viewItemPropValues != null && viewItemPropValues.Count > 0)
                {
                    cache.Add(cacheKey, viewItemPropValues, CacheItemPriority.Normal, null, new AbsoluteTime(DateTime.Now.AddMinutes(20)));
                }
                return viewItemPropValues;
            }
        }

        /*
        public static View_ItemPropValue GetView_ItemPropValue(int view_itempropvalueID)
        {
            return DataProviderClass.Instance().GetView_ItemPropValue(view_itempropvalueID);
        }
        */

        public static List<View_ItemPropValue> GetView_ItemPropValue(List<string> cidList)
        {
            return DataProviderClass.Instance().GetView_ItemPropValue(cidList);
        }

        /*
        public static List<View_ItemPropValue> GetView_ItemPropValue(List<int> view_itempropvalueIDList)
        {
            return DataProviderClass.Instance().GetView_ItemPropValue(view_itempropvalueIDList);
        }
        */

        public static List<View_ItemPropValue> GetView_ItemPropValue(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetView_ItemPropValue(pageIndex, pageSize, out rowCount);
        }

        public static List<View_ItemPropValue> GetView_ItemPropValue(Func<View_ItemPropValue, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetView_ItemPropValue(func, pageIndex, pageSize, out rowCount);
        }

        public static List<string> GetItemPropvalueDownCids()
        {
            return DataProviderClass.Instance().GetItemPropvalueDownCids();
        }

        public static bool IsExistedPropValueName(string cid, string pid, string vname)
        {
            return DataProviderClass.Instance().IsExistedPropValueName(cid, pid, vname);
        }

        public static bool IsExistedCid(string cid)
        {
            return DataProviderClass.Instance().IsPropValueExistedCid(cid);
        }

        public static List<string> GetPropValueWhereInCids(List<string> cidlist)
        {
            return DataProviderClass.Instance().GetPropValueWhereInCids(cidlist);
        }

    }
}
