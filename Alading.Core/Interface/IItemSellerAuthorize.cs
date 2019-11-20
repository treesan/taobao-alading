using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IItemSellerAuthorize
    {       
        ReturnType AddItemSellerAuthorize(ItemSellerAuthorize itemsellerauthorize);
       
        ReturnType AddItemSellerAuthorize(List<ItemSellerAuthorize> itemsellerauthorizeList);
        
        ReturnType RemoveAllItemSellerAuthorize();
       
        ReturnType RemoveItemSellerAuthorize(Func<ItemSellerAuthorize, bool> func);
              
        ReturnType RemoveItemSellerAuthorize(string itemsellerauthorizeCode);
        
        ReturnType RemoveItemSellerAuthorize(List<string> itemsellerauthorizeCodeList);
       
        ReturnType UpdateItemSellerAuthorize(ItemSellerAuthorize itemsellerauthorize);
       
        ReturnType UpdateItemSellerAuthorize(string itemsellerauthorizeCode,ItemSellerAuthorize itemsellerauthorize);
       
        List<ItemSellerAuthorize> GetAllItemSellerAuthorize();
      
        List<ItemSellerAuthorize> GetItemSellerAuthorize(Func<ItemSellerAuthorize, bool> func);
      
        List<ItemSellerAuthorize> GetItemSellerAuthorize(List<string> itemsellerauthorizeCodeList);
       
        List<ItemSellerAuthorize> GetItemSellerAuthorize(int pageIndex, int pageSize, out int rowCount);
        
        List<ItemSellerAuthorize> GetItemSellerAuthorize(Func<ItemSellerAuthorize, bool> func, int pageIndex, int pageSize, out int rowCount);

        ReturnType IsAuthorizeExisted(string nick, string cid);
        /*        
        ReturnType RemoveItemSellerAuthorize(int itemsellerauthorizeID);
        
        ReturnType RemoveItemSellerAuthorize(List<int> itemsellerauthorizeIDList);
        
        ReturnType UpdateItemSellerAuthorize(int itemsellerauthorizeID,ItemSellerAuthorize itemsellerauthorize);
        
        List<ItemSellerAuthorize> GetItemSellerAuthorize(List<int> itemsellerauthorizeIDList);
        */
    }
}
