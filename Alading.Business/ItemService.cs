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
    public static class ItemService
    {

        public static List<AssembleItem> SearchAssembleItem(string keyword, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().SearchAssembleItem(keyword, pageIndex, pageSize, out rowCount);
        }

        public static List<AssembleItem> GetAllUsedAssembleItem()
        {
            return DataProviderClass.Instance().GetAllUsedAssembleItem();
        }

        public static ReturnType UpdateItemType(string iid, string outer_id, string itemType)
        {
            return DataProviderClass.Instance().UpdateItemType(iid, outer_id, itemType);
        }

        public static ReturnType UpdateItemStockUnit(string iid, string stockUnitcode, string ItemType)
        {
            return DataProviderClass.Instance().UpdateItemStockUnit(iid, stockUnitcode, ItemType);
        }

       /// <summary>
       /// 判断StockItem中此OuterID是否存在,AssembleItem中是否存在
       /// </summary>
       /// <param name="outerID"></param>
       /// <param name="skuProps"></param>
       /// <returns></returns>
        public static ReturnType IsAssembleStockItemExisted(string outerID, string skuProps)
        {
            return DataProviderClass.Instance().IsAssembleStockItemExisted(outerID, skuProps);
        }

        public static ReturnType UpdateItemsStatus(List<string> iidList, string status)
        {
            return DataProviderClass.Instance().UpdateItemsStatus(iidList, status);
        }

        public static ReturnType UpdateItem(string iid)
        {
            return DataProviderClass.Instance().UpdateItem(iid);
        }
        public static ReturnType RemoveAssembleItem(string assembleCode)
        {
            return DataProviderClass.Instance().RemoveAssembleItem(assembleCode);
        }
        public static List<AssembleItem> GetAllAssembleItem()
        {
            return DataProviderClass.Instance().GetAllAssembleItem();
        }
        public static List<AssembleItem> GetAssembleItem(Func<AssembleItem, bool> func)
        {
            return DataProviderClass.Instance().GetAssembleItem(func);
        }
        public static ReturnType AddAssembleItem(AssembleItem itemAssemble)
        {
            return DataProviderClass.Instance().AddAssembleItem(itemAssemble);
        }

        public static List<AssembleItem> SelectAssembleItem(string keyword)
        {
            return DataProviderClass.Instance().SelectAssembleItem(keyword);
        }

        public static List<AssembleItem> GetAssembleItem(Func<AssembleItem, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetAssembleItem(func, pageIndex, pageSize, out rowCount);
        }

        public static List<AssembleItem> GetAllAssembleItem(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetAllAssembleItem(pageIndex, pageSize, out rowCount);
        }

        public static ReturnType AddAssembleItem(List<AssembleItem> assembleItemList)
        {
            return DataProviderClass.Instance().AddAssembleItem(assembleItemList);
        }

        public static ReturnType UpdateAssembleItem(AssembleItem itemAssemble)
        {
            return DataProviderClass.Instance().UpdateAssembleItem(itemAssemble);
        }

        public static ReturnType AddAssembleItemDetails(List<AssembleItem> itemList, List<AssembleDetail> detailList)
        {
            return DataProviderClass.Instance().AddAssembleItemDetails(itemList, detailList);
        }

        public static ReturnType UpdateAssembleItemDetails(AssembleItem itemAssemble,List<AssembleDetail> detailList)
        {
            return DataProviderClass.Instance().UpdateAssembleItemDetails(itemAssemble,detailList);
        }


        public static ReturnType UpdateAssembleItemBase(AssembleItem assembleItem)
        {
            return DataProviderClass.Instance().UpdateAssembleItemBase(assembleItem);
        }

        public static ReturnType UpdateAssembleItemDetails(string assembleCode, List<AssembleDetail> detailList)
        {
            return DataProviderClass.Instance().UpdateAssembleItemDetails(assembleCode, detailList);
        }

        public static ReturnType UpdateAssembleItemProps(AssembleItem assembleItem)
        {
            return DataProviderClass.Instance().UpdateAssembleItemProps(assembleItem);
        }



        public static List<AssembleItem> GetAssembleItem(List<string> outerIDList)
        {
            return DataProviderClass.Instance().GetAssembleItem(outerIDList);
        }
     
        /// <summary>
        /// 如果数据库中不存在此商品则添加，存在就只更新淘宝字段,
        /// 如果cid发生变化，则将item与库存关联解除
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static ReturnType AddItem(Item item)
        {
            return DataProviderClass.Instance().AddItem(item);
        }

        /// <summary>
        /// 商品不存在则添加，存在则update为true时更新，为false时跳过
        /// </summary>
        /// <param name="item"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public static ReturnType AddItem(Item item, bool update)
        {
            return DataProviderClass.Instance().AddItem(item,update);
        }

        public static ReturnType AddItem(List<Item> itemList)
        {
            return DataProviderClass.Instance().AddItem(itemList);
        }
    
        public static ReturnType RemoveAllItem()
        {
            return DataProviderClass.Instance().RemoveAllItem();
        }
    
        public static ReturnType RemoveItem(Func<Item, bool> func)
        {
            return DataProviderClass.Instance().RemoveItem(func);
        }
        
        public static ReturnType RemoveItem(string itemCode)
        {
            return DataProviderClass.Instance().RemoveItem(itemCode);
        }       
        
        /*
        public static ReturnType RemoveItem(int itemID)
        {
            return DataProviderClass.Instance().RemoveItem(itemID);
        }
        */
    
        public static ReturnType RemoveItem(List<string> itemCodeList)
        {
            return DataProviderClass.Instance().RemoveItem(itemCodeList);
        }        
        
        /*
        public static ReturnType RemoveItem(List<int> itemIDList)
        {
            return DataProviderClass.Instance().RemoveItem(itemIDList);
        }
        */
    
        public static ReturnType UpdateItem(Item item)
        {
            return DataProviderClass.Instance().UpdateItem(item);
        }
    
        public static ReturnType UpdateItem(string iid, Item item)
        {
            return DataProviderClass.Instance().UpdateItem(iid, item);
        }

        public static ReturnType UpdateItemIsUpdate(string iid, bool isUpdate)
         {
             return DataProviderClass.Instance().UpdateItemIsUpdate(iid, isUpdate);
         }
        
        public static ReturnType UpdateItemsListTime(Dictionary<string,string> itemdic)
        {
            return DataProviderClass.Instance().UpdateItemsListTime(itemdic);
        }

        /// <summary>
        /// 批量更新Item的OuterId,同时更新isAssociate字段,如果不更新此字段请置为null
        /// </summary>
        /// <param name="itemdic"></param>
        /// <returns></returns>
        public static ReturnType UpdateItemsOuterId(Dictionary<string, string> itemdic,bool? isAssociate)
         {
             return DataProviderClass.Instance().UpdateItemsOuterId(itemdic, isAssociate);
         }

        /// <summary>
        /// 批量更新Item的IsAssociate
        /// </summary>
        /// <param name="iidlist"></param>
        /// <returns></returns>
        public static ReturnType UpdateItemsAssociate(List<string> iidlist,bool value)
        {
            return DataProviderClass.Instance().UpdateItemsAssociate(iidlist, value);
        }

        public static List<Item> GetItem(List<string> listCid, bool isAssociate)
        {
            return DataProviderClass.Instance().GetItem(listCid, isAssociate);
        }

        public static List<Item> GetItems(List<string> listCid)
        {
            return DataProviderClass.Instance().GetItem(listCid);
        }

        public static List<Item> GetItems(string cid)
        {
            return DataProviderClass.Instance().GetItems(cid);
        }
    
        public static List<Item> GetAllItem()
        {
            return DataProviderClass.Instance().GetAllItem();
        }
    
        public static List<Item> GetItem(Func<Item, bool> func)
        {
            return DataProviderClass.Instance().GetItem(func);
        }
    
        public static Item GetItem(string iid)
        {
            return DataProviderClass.Instance().GetItem(iid);
        }
        
        /*
        public static Item GetItem(int itemID)
        {
            return DataProviderClass.Instance().GetItem(itemID);
        }
        */
    
        public static List<Item> GetItem(List<string> itemCodeList)
        {
            return DataProviderClass.Instance().GetItem(itemCodeList);
        }
        
        /*
        public static List<Item> GetItem(List<int> itemIDList)
        {
            return DataProviderClass.Instance().GetItem(itemIDList);
        }
        */

        public static List<Item> GetItem(string cid, bool IsAssociate)
        {
            return DataProviderClass.Instance().GetItem(cid, IsAssociate);
        }

        public static List<string> GetItemCidByNick(string nick)
        {
            return DataProviderClass.Instance().GetItemCidByNick(nick);
        }

        public static List<Item> GetItem(string cid, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetItem(cid, pageIndex, pageSize,out rowCount);
        }

        public static List<Item> GetItem(string sellercid, string nick, out int rowCount)
        {
            return DataProviderClass.Instance().GetItem(sellercid, nick, out  rowCount);
        }

        public static List<Item> GetItem(string sellercid, string nick, string aprrovestatus, out int rowCount)
        {
            return DataProviderClass.Instance().GetItem( sellercid, nick , aprrovestatus,out  rowCount);
        }

        public static List<Item> GetItem(string sellercid, string sellerNick, string approvestatus, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetItem(sellercid, sellerNick, approvestatus, pageIndex, pageSize, out  rowCount);
        }

        public static List<Item> GetItem(string sellercid, string sellerNick, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetItem(sellercid, sellerNick, pageIndex, pageSize, out rowCount);
        }

    
        public static List<Item> GetItem(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetItem(pageIndex, pageSize, out rowCount);
        }
        
        public static List<Item> GetItem(Func<Item, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetItem(func, pageIndex, pageSize, out rowCount);
        }

        /// <summary>
        /// 判断商品是否存在，存在返回PropertyExisted
        /// </summary>
        public static ReturnType IsItemExisted(string iid)
         {
             return DataProviderClass.Instance().IsItemExisted(iid);
         }

        /// <summary>
        /// 获取所有Item的Cid
        /// </summary>
        /// <returns></returns>
         public static List<string> GetItemCids()
         {
             return DataProviderClass.Instance().GetItemCids();
         }

         public static List<View_AssembleProduct> GetViewAssembleProduct(string assemblecode)
         {
             return DataProviderClass.Instance().GetViewAssembleProduct(assemblecode);
         }

         public static List<string> GetAllItemOuterId()
         {
             return DataProviderClass.Instance().GetAllItemOuterId();
         }

         public static List<Alading.Entity.View_ShopItem> Get_ViewShopItem(string sellerNick, string keyWords)
         {
             return DataProviderClass.Instance().Get_ViewShopItem(sellerNick, keyWords);
         }
    }
}
