using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IView_ShopItem
    {
        List<View_ShopItem> GetView_ShopItemList(List<string> listCid);

        List<View_ShopItem> GetView_ShopItem(List<string> listCid, bool isAssociate);

        List<View_ShopItem> GetView_ShopItem(string cid, bool isAssociate);

        List<View_ShopItem> GetView_ShopItem(string nick, string cid);
        
        List<View_ShopItem> GetAllView_ShopItem();

        List<View_ShopItem> GetView_ShopItem(Func<View_ShopItem, bool> func);

        List<View_ShopItem> GetView_ShopItem(List<string> view_shopitemCodeList);

        List<View_ShopItem> GetView_ShopItem(int pageIndex, int pageSize, out int rowCount);

        List<View_ShopItem> GetView_ShopItem(Func<View_ShopItem, bool> func, int pageIndex, int pageSize, out int rowCount);

        /*        
        ReturnType RemoveView_ShopItem(int view_shopitemID);
        
        ReturnType RemoveView_ShopItem(List<int> view_shopitemIDList);
        
        ReturnType UpdateView_ShopItem(int view_shopitemID,View_ShopItem view_shopitem);
        
        List<View_ShopItem> GetView_ShopItem(List<int> view_shopitemIDList);
        */
    }
}
