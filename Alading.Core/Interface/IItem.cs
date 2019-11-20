using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;
using System.Data;

namespace Alading.Core.Interface
{
    public interface IItem
    {
        List<AssembleItem> SearchAssembleItem(string keyword, int pageIndex, int pageSize, out int rowCount);

        ReturnType AddItem(Item item);

        ReturnType AddItem(Item item, bool update);
       
        ReturnType AddItem(List<Item> itemList);
        
        ReturnType RemoveAllItem();
       
        ReturnType RemoveItem(Func<Item, bool> func);
              
        ReturnType RemoveItem(string itemCode);
        
        ReturnType RemoveItem(List<string> itemCodeList);
       
        ReturnType UpdateItem(Item item);
       
        ReturnType UpdateItem(string iid,Item item);

        ReturnType UpdateItemIsUpdate(string iid, bool isUpdate);

        ReturnType UpdateItemsListTime(Dictionary<string, string> itemdic);

        ReturnType UpdateItemsOuterId(Dictionary<string, string> itemdic, bool? isAssociate);

        ReturnType UpdateItemsAssociate(List<string> iidlist, bool isAssociate);
       
        List<Item> GetAllItem();
      
        List<Item> GetItem(Func<Item, bool> func);
      
        List<Item> GetItems(List<string> cidList);

        List<Item> GetItems(string cid);
       
        List<Item> GetItem(int pageIndex, int pageSize, out int rowCount);

        List<Item> GetItem(string sellercid, string nick, out int rowCount);

        List<Item> GetItem(string sellercid, string nick, string aprrovestatus, out int rowCount);

        List<Item> GetItem(string sellercid, string sellerNick, string approvestatus, int pageIndex, int pageSize, out int rowCount);
        
        List<Item> GetItem(Func<Item, bool> func, int pageIndex, int pageSize, out int rowCount);

        ReturnType IsItemExisted(string iid);

        List<string> GetItemCids();

        List<AssembleItem> GetAllAssembleItem();

        List<AssembleItem> GetAssembleItem(Func<AssembleItem, bool> func);

        List<AssembleItem> GetAssembleItem(Func<AssembleItem, bool> func, int pageIndex, int pageSize, out int rowCount);

        List<AssembleItem> GetAllAssembleItem(int pageIndex, int pageSize, out int rowCount);

        ReturnType RemoveAssembleItem(string assembleCode);


        ReturnType UpdateAssembleItemBase(AssembleItem assembleItem);

        ReturnType UpdateAssembleItemDetails(string assembleCode, List<AssembleDetail> detailList);

        ReturnType UpdateAssembleItemProps(AssembleItem assembleItem);


        ReturnType AddAssembleItemDetails(List<AssembleItem> itemList, List<AssembleDetail> detailList);
        
        ReturnType UpdateAssembleItemDetails(AssembleItem itemAssemble,List<AssembleDetail> detailList);

        /// <summary>
        /// 判断StockItem中此OuterID是否存在,AssembleItem中是否存在
        /// </summary>
        /// <param name="outerID"></param>
        /// <param name="skuProps"></param>
        /// <returns></returns>
        ReturnType IsAssembleStockItemExisted(string outerID,string skuProps);
       
     
    }
}
