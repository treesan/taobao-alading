using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IShop
    {       
        ReturnType AddShop(Shop shop);
       
        ReturnType AddShop(List<Shop> shopList);
        
        ReturnType RemoveAllShop();
       
        ReturnType RemoveShop(Func<Shop, bool> func);

        ReturnType RemoveShop(string sid);

        ReturnType RemoveShop(List<string> sidList);
       
        ReturnType UpdateShop(Shop shop);

        ReturnType UpdateShop(string sid, Shop shop);
       
        List<Shop> GetAllShop();

        Shop GetShopByNick(string nick);

        List<Shop> GetShop(Func<Shop, bool> func);

        List<Shop> GetShop(List<string> sidList);
       
        List<Shop> GetShop(int pageIndex, int pageSize, out int rowCount);
        
        List<Shop> GetShop(Func<Shop, bool> func, int pageIndex, int pageSize, out int rowCount);

        ReturnType UpdateSessionkey(string nick, string sessionkey, DateTime sessiontime);
        /*        
        ReturnType RemoveShop(int shopID);
        
        ReturnType RemoveShop(List<int> shopIDList);
        
        ReturnType UpdateShop(int shopID,Shop shop);
        
        List<Shop> GetShop(List<int> shopIDList);
        */
    }
}
