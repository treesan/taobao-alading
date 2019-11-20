using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class ItemSellerAuthorizeService
    {
        /// <summary>
        /// 存在即更新，不存在则添加
        /// </summary>
        /// <param name="itemsellerauthorize"></param>
        /// <returns></returns>
        public static ReturnType AddItemSellerAuthorize(ItemSellerAuthorize itemsellerauthorize)
        {
            return DataProviderClass.Instance().AddItemSellerAuthorize(itemsellerauthorize);
        }

        public static ReturnType AddItemSellerAuthorize(List<ItemSellerAuthorize> itemsellerauthorizeList)
        {
            return DataProviderClass.Instance().AddItemSellerAuthorize(itemsellerauthorizeList);
        }
    
        public static ReturnType RemoveAllItemSellerAuthorize()
        {
            return DataProviderClass.Instance().RemoveAllItemSellerAuthorize();
        }
    
        public static ReturnType RemoveItemSellerAuthorize(Func<ItemSellerAuthorize, bool> func)
        {
            return DataProviderClass.Instance().RemoveItemSellerAuthorize(func);
        }
        
        public static ReturnType RemoveItemSellerAuthorize(string itemsellerauthorizeCode)
        {
            return DataProviderClass.Instance().RemoveItemSellerAuthorize(itemsellerauthorizeCode);
        }       
        
        /*
        public static ReturnType RemoveItemSellerAuthorize(int itemsellerauthorizeID)
        {
            return DataProviderClass.Instance().RemoveItemSellerAuthorize(itemsellerauthorizeID);
        }
        */
    
        public static ReturnType RemoveItemSellerAuthorize(List<string> itemsellerauthorizeCodeList)
        {
            return DataProviderClass.Instance().RemoveItemSellerAuthorize(itemsellerauthorizeCodeList);
        }        
        
        /*
        public static ReturnType RemoveItemSellerAuthorize(List<int> itemsellerauthorizeIDList)
        {
            return DataProviderClass.Instance().RemoveItemSellerAuthorize(itemsellerauthorizeIDList);
        }
        */
    
        public static ReturnType UpdateItemSellerAuthorize(ItemSellerAuthorize itemsellerauthorize)
        {
            return DataProviderClass.Instance().UpdateItemSellerAuthorize(itemsellerauthorize);
        }
    
        public static ReturnType UpdateItemSellerAuthorize(string itemsellerauthorizeCode, ItemSellerAuthorize itemsellerauthorize)
        {
            return DataProviderClass.Instance().UpdateItemSellerAuthorize(itemsellerauthorizeCode, itemsellerauthorize);
        }
        
        /*
        public static ReturnType UpdateItemSellerAuthorize(int itemsellerauthorizeID, ItemSellerAuthorize itemsellerauthorize)
        {
            return DataProviderClass.Instance().UpdateItemSellerAuthorize(itemsellerauthorizeID, itemsellerauthorize);
        }
        */
    
        public static List<ItemSellerAuthorize> GetAllItemSellerAuthorize()
        {
            return DataProviderClass.Instance().GetAllItemSellerAuthorize();
        }
    
        public static List<ItemSellerAuthorize> GetItemSellerAuthorize(Func<ItemSellerAuthorize, bool> func)
        {
            return DataProviderClass.Instance().GetItemSellerAuthorize(func);
        }
    
        public static ItemSellerAuthorize GetItemSellerAuthorize(string itemsellerauthorizeCode)
        {
            return DataProviderClass.Instance().GetItemSellerAuthorize(itemsellerauthorizeCode);
        }
        
        /*
        public static ItemSellerAuthorize GetItemSellerAuthorize(int itemsellerauthorizeID)
        {
            return DataProviderClass.Instance().GetItemSellerAuthorize(itemsellerauthorizeID);
        }
        */
    
        public static List<ItemSellerAuthorize> GetItemSellerAuthorize(List<string> itemsellerauthorizeCodeList)
        {
            return DataProviderClass.Instance().GetItemSellerAuthorize(itemsellerauthorizeCodeList);
        }
        
        /*
        public static List<ItemSellerAuthorize> GetItemSellerAuthorize(List<int> itemsellerauthorizeIDList)
        {
            return DataProviderClass.Instance().GetItemSellerAuthorize(itemsellerauthorizeIDList);
        }
        */
    
        public static List<ItemSellerAuthorize> GetItemSellerAuthorize(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetItemSellerAuthorize(pageIndex, pageSize, out rowCount);
        }
        
        public static List<ItemSellerAuthorize> GetItemSellerAuthorize(Func<ItemSellerAuthorize, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetItemSellerAuthorize(func, pageIndex, pageSize, out rowCount);
        }

        /// <summary>
        /// 判断授权是否已存在，存在返回propertyExisted
        /// </summary>
        public static ReturnType IsAuthorizeExisted(string nick, string cid)
        {
            return DataProviderClass.Instance().IsAuthorizeExisted(nick, cid);
        }
    }
}
