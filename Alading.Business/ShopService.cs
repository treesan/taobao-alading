using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class ShopService
    {

        public static ReturnType AddShop(Shop shop)
        {
            return DataProviderClass.Instance().AddShop(shop);
        }

        public static ReturnType AddShop(List<Shop> shopList)
        {
            return DataProviderClass.Instance().AddShop(shopList);
        }
    
        public static ReturnType RemoveAllShop()
        {
            return DataProviderClass.Instance().RemoveAllShop();
        }
    
        public static ReturnType RemoveShop(Func<Shop, bool> func)
        {
            return DataProviderClass.Instance().RemoveShop(func);
        }

        public static ReturnType RemoveShop(string shopCode)
        {
            return DataProviderClass.Instance().RemoveShop(shopCode);
        }       
        
        /*
        public static ReturnType RemoveShop(int shopID)
        {
            return DataProviderClass.Instance().RemoveShop(shopID);
        }
        */

        public static ReturnType RemoveShop(List<string> shopCodeList)
        {
            return DataProviderClass.Instance().RemoveShop(shopCodeList);
        }        
        
        /*
        public static ReturnType RemoveShop(List<int> shopIDList)
        {
            return DataProviderClass.Instance().RemoveShop(shopIDList);
        }
        */
    
        public static ReturnType UpdateShop(Shop shop)
        {
            return DataProviderClass.Instance().UpdateShop(shop);
        }

        public static ReturnType UpdateShop(string shopCode, Shop shop)
        {
            return DataProviderClass.Instance().UpdateShop(shopCode, shop);
        }
        /*
        public static ReturnType UpdateShop(int shopID, Shop shop)
        {
            return DataProviderClass.Instance().UpdateShop(shopID, shop);
        }
        */
    
        public static List<Shop> GetAllShop()
        {
            return DataProviderClass.Instance().GetAllShop();
        }
    
        public static List<Shop> GetShop(Func<Shop, bool> func)
        {
            return DataProviderClass.Instance().GetShop(func);
        }

        public static Shop GetShop(string sid)
        {
            return DataProviderClass.Instance().GetShop(sid);
        }
        
        /*
        public static Shop GetShop(int shopID)
        {
            return DataProviderClass.Instance().GetShop(shopID);
        }
        */

        public static List<Shop> GetShop(List<string> shopCodeList)
        {
            return DataProviderClass.Instance().GetShop(shopCodeList);
        }
        
        /*
        public static List<Shop> GetShop(List<int> shopIDList)
        {
            return DataProviderClass.Instance().GetShop(shopIDList);
        }
        */
    
        public static List<Shop> GetShop(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetShop(pageIndex, pageSize, out rowCount);
        }
        
        public static List<Shop> GetShop(Func<Shop, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetShop(func, pageIndex, pageSize, out rowCount);
        }

        public static Shop GetShopByNick(string nick)
        {
            return DataProviderClass.Instance().GetShopByNick(nick);
        }

        public static ReturnType UpdateSessionkey(string nick, string sessionkey,DateTime sessiontime)
        {
            return DataProviderClass.Instance().UpdateSessionkey(nick, sessionkey, sessiontime);
        }

    }
}
