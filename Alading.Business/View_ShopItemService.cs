using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;
using System.Data.Objects;

namespace Alading.Business
{
    public static class View_ShopItemService
    {
        /// <summary>
        /// 获取所有淘宝类目列表listcid下的所有数据
        /// </summary>
        /// <param name="listCid"></param>
        /// <returns></returns>
        public static List<View_ShopItem> GetView_ShopItemList(List<string> listCid)
        {
            return DataProviderClass.Instance().GetView_ShopItemList(listCid);
        }

        /// <summary>
        /// 获取店铺Nick下的是否为橱窗推荐的商品
        /// </summary>
        /// <param name="nick"></param>
        /// <param name="history"></param>
        /// <param name="showCase"></param>
        /// <returns></returns>
        public static List<View_ShopItem> GetView_ShopItemShowCaseList(string nick, bool history, bool showCase)
        {
            return DataProviderClass.Instance().GetView_ShopItemShowCaseList(nick, history, showCase);
        }

        /// <summary>
        /// 获取店铺Nick的回收站中的商品，即history=TRUE
        /// </summary>
        /// <param name="nick"></param>
        /// <param name="history"></param>
        /// <returns></returns>
        public static List<View_ShopItem> GetView_ShopItemHistoryList(string nick, bool history)
        {
            return DataProviderClass.Instance().GetView_ShopItemHistoryList(nick, history);
        }

        /// <summary>
        /// 获取淘宝类目列表listcid下的是否已关联的商品
        /// </summary>
        /// <param name="listCid"></param>
        /// <param name="isAssociate"></param>
        /// <returns></returns>
        public static List<View_ShopItem> GetView_ShopItem(List<string> listCid, bool isAssociate)
        {
            return DataProviderClass.Instance().GetView_ShopItem(listCid, isAssociate);
        }

        public static List<View_ShopItem> GetView_ShopItemToStock(string cids, string isAssociate)
        {
            return DataProviderClass.Instance().GetView_ShopItemToStock(cids, isAssociate);
        }

        public static List<View_ShopItem> GetView_ShopItem(string cid, bool isAssociate)
        {
            return DataProviderClass.Instance().GetView_ShopItem(cid, isAssociate);
        }

        /// <summary>
        /// 获取店铺Nick的淘宝类目列表listcid下的是否已关联的商品
        /// </summary>
        /// <param name="listCid"></param>
        /// <param name="nick"></param>
        /// <param name="isAssociate"></param>
        /// <returns></returns>
        public static List<View_ShopItem> GetView_ShopItem(List<string> listCid,string nick, bool isAssociate)
        {
            return DataProviderClass.Instance().GetView_ShopItem(listCid,nick, isAssociate);
        }

        /// <summary>
        /// 获取店铺Nick的自定义类目sellercid下的所有商品
        /// </summary>
        /// <param name="nick"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static List<View_ShopItem> GetView_ShopItem(string nick, string sellercid)
        {
            return DataProviderClass.Instance().GetView_ShopItem(nick, sellercid);
        }

        /// <summary>
        /// 获取所有的商品
        /// </summary>
        /// <returns></returns>
        public static List<View_ShopItem> GetAllView_ShopItem()
        {
            return DataProviderClass.Instance().GetAllView_ShopItem();
        }

        /// <summary>
        /// 获取商品自定义参数方法，性能极低，慎用
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static List<View_ShopItem> GetView_ShopItem(Func<View_ShopItem, bool> func)
        {
            return DataProviderClass.Instance().GetView_ShopItem(func);
        }

        
        public static View_ShopItem GetView_ShopItem(string view_shopitemID)
        {
            return DataProviderClass.Instance().GetView_ShopItem(view_shopitemID);
        }
 

        /*
        public static List<View_ShopItem> GetView_ShopItem(List<int> view_shopitemIDList)
        {
            return DataProviderClass.Instance().GetView_ShopItem(view_shopitemIDList);
        }
        */

        public static List<View_ShopItem> GetView_ShopItem(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetView_ShopItem(pageIndex, pageSize, out rowCount);
        }

        /// <summary>
        /// 性能极低，慎用
        /// </summary>
        /// <param name="func"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<View_ShopItem> GetView_ShopItem(Func<View_ShopItem, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetView_ShopItem(func, pageIndex, pageSize, out rowCount);
        }

        /// <summary>
        /// 获取店铺Nick的自定义类目下出售中或仓库中的商品
        /// </summary>
        /// <param name="sellercid"></param>
        /// <param name="nick"></param>
        /// <param name="aprrovestatus"></param>
        /// <returns></returns>
        public static List<View_ShopItem> GetView_ShopItem(string sellercid, string nick, string aprrovestatus)
        {
            return DataProviderClass.Instance().GetView_ShopItem(sellercid, nick, aprrovestatus);
        }

        /// <summary>
        /// 获取店铺Nick的存在于自定义类目列表listSellerCid下的所有出售中或仓库中的商品
        /// </summary>
        /// <param name="listSellerCid"></param>
        /// <param name="nick"></param>
        /// <param name="aprrovestatus"></param>
        /// <returns></returns>
        public static List<View_ShopItem> GetView_ShopItem(List<string> listSellerCid, string nick, string aprrovestatus)
        {
            return DataProviderClass.Instance().GetView_ShopItem(listSellerCid, nick, aprrovestatus);
        }

        /// <summary>
        /// 获取店铺Nick的存在于自定义类目列表listSellerCid下的所有关联或为关联的商品
        /// </summary>
        /// <param name="sellerCid"></param>
        /// <param name="nick"></param>
        /// <param name="isAssociate"></param>
        /// <returns></returns>
        public static List<View_ShopItem> GetView_ShopItemBySellerCatCid(List<string> sellerCid, string nick, bool isAssociate)
        {
            return DataProviderClass.Instance().GetView_ShopItemBySellerCatCid(sellerCid, nick, isAssociate);
        }

        public static List<View_ShopItem> GetView_ShopItemInStock(string nick, string approve_status, bool isHistory)
        {
            return DataProviderClass.Instance().GetView_ShopItemInStock(nick, approve_status, isHistory);
        }

        //public static List<View_ShopItem> GetView_ShopItems(string nick, string aprrovestatus, bool isHistory)
        //{
        //    return DataProviderClass.Instance().Get_ViewShopItems(nick, aprrovestatus, isHistory);
        //}
    }


}
