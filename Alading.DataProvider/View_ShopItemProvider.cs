using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Core.Interface;
using Alading.Entity;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Data.Objects;
using Alading.Core.Enum;
using System.Linq.Expressions;
using Alading.Core;

namespace Alading.DataProvider
{
   
    public partial class DataProviderClass : IAlading
    {
        public List<View_ShopItem> GetView_ShopItemList(List<string> listCid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_ShopItem> result = alading.View_ShopItem.Where(BuildWhereInExpression<View_ShopItem, string>(v => v.cid, listCid)).ToList();
                    return result;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_ShopItem> GetView_ShopItemShowCaseList(string nick, bool history, bool showCase)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_ShopItem> result = alading.SP_GetView_ShopItemShowCase(nick, history, showCase).ToList();
                    return result;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_ShopItem> GetView_ShopItemHistoryList(string nick, bool history)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_ShopItem> result = alading.View_ShopItem.Where(p => p.nick == nick && p.IsHistory == history).ToList();
                    return result;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_ShopItem> GetView_ShopItem(List<string> listCid, bool isAssociate)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_ShopItem> result = alading.View_ShopItem.Where(BuildWhereInExpression<View_ShopItem, string>(v => v.cid, listCid)).Where(u => u.IsAsociate == isAssociate).OrderBy(p => p.outer_id).OrderBy(r => r.price).ToList();
                    //List<View_ShopItem> shopItemList=alading.SP_GetView_ShopItemToStock
                    return result;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_ShopItem> GetView_ShopItemToStock(string cids, string isAssociate)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_ShopItem> shopItemList = alading.SP_GetView_ShopItemToStock(cids, isAssociate).ToList();
                    return shopItemList;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_ShopItem> GetView_ShopItem(string cid, bool isAssociate)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_ShopItem> result = alading.View_ShopItem.Where(v=>v.cid==cid).OrderBy(p => p.outer_id).OrderBy(r => r.price).ToList();
                    return result;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_ShopItem> GetView_ShopItem(List<string> listCid,string nick, bool isAssociate)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_ShopItem> result = alading.View_ShopItem.Where(BuildWhereInExpression<View_ShopItem, string>(v => v.cid, listCid)).Where(u => u.IsAsociate == isAssociate && u.nick == nick).OrderBy(p => p.outer_id).OrderBy(r => r.price).ToList();
                    return result;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_ShopItem> GetView_ShopItem( string nick,string cid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_ShopItem> result = alading.View_ShopItem.Where(p => p.nick == nick && p.seller_cids.Contains(cid)).ToList();
                    return result;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        public List<View_ShopItem> GetView_ShopItemBySellerCatCid(List<string> sellerCatCid, string nick, bool isAssociate)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_ShopItem> itemlist = alading.View_ShopItem.Where(p => p.nick == nick && p.IsAsociate == isAssociate).ToList();
                    List<View_ShopItem> listItem = new List<View_ShopItem>();
                    foreach (string sellercid in sellerCatCid)
                    {
                        foreach (View_ShopItem shopItem in itemlist)
                        {
                            if (shopItem.seller_cids.Contains(sellercid))
                            {
                                listItem.Add(shopItem);
                            }
                        }
                    }
                    return listItem;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_ShopItem> GetView_ShopItem(List<string> view_shopitemCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.View_ShopItem.Where(BuildWhereInExpression<View_ShopItem, int>(v => v.View_ShopItemID, view_shopitemIDList));*/
                    var result = alading.View_ShopItem.Where(BuildWhereInExpression<View_ShopItem, string>(v => v.iid, view_shopitemCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }        
     
        public List<View_ShopItem> GetAllView_ShopItem()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_ShopItem> list = alading.View_ShopItem.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<View_ShopItem> GetView_ShopItem(Func<View_ShopItem, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_ShopItem> list = alading.View_ShopItem.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public View_ShopItem GetView_ShopItem(string view_shopitemCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_ShopItem> list = alading.View_ShopItem.Where(p => p.iid == view_shopitemCode).ToList();
                    if (list.Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return list.First();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<View_ShopItem> GetView_ShopItem(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    
					var ob = (from u in alading.View_ShopItem orderby u.ItemID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.View_ShopItem.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<View_ShopItem> GetView_ShopItem(Func<View_ShopItem, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<View_ShopItem> list = alading.View_ShopItem.Where(func).OrderByDescending(a=>a.ItemID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        //public List<View_ShopItem> GetView_ShopItems(string nick, string aprrovestatus,bool isHistory)
        //{
        //    try
        //    {
        //        using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
        //        {
        //            List<View_ShopItem> shopItemList = alading.SP_GetView_ShopItemNick(nick, aprrovestatus, isHistory).ToList();
        //            return shopItemList;
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<View_ShopItem> GetView_ShopItem(string sellercid, string nick, string aprrovestatus)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_ShopItem> itemlist = new List<View_ShopItem>();
                    if (sellercid==null)
                    {
                        itemlist = alading.View_ShopItem.Where(i => i.nick == nick && i.approve_status == aprrovestatus && i.IsHistory==false).ToList();
                    }
                    else if (sellercid==string.Empty)
                    {
                        itemlist = alading.View_ShopItem.Where(i => i.nick == nick && i.approve_status == aprrovestatus && i.seller_cids == sellercid && i.IsHistory == false).ToList();
                    } 
                    else
                    {
                        itemlist = alading.View_ShopItem.Where(i => i.nick == nick && i.approve_status == aprrovestatus && i.seller_cids.Contains(sellercid) && i.IsHistory == false).ToList();
                    }
                    return itemlist;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_ShopItem> GetView_ShopItem(List<string> listChildCid, string nick, string aprrovestatus)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_ShopItem> itemlist = alading.View_ShopItem.Where(p => p.nick == nick && p.approve_status == aprrovestatus && p.IsHistory == false).ToList();
                    List<View_ShopItem> listItem = new List<View_ShopItem>();
                    foreach (string sellercid in listChildCid)
                    {
                        foreach (View_ShopItem shopItem in itemlist)
                        {
                            if (shopItem.seller_cids.Contains(sellercid))
                            {
                                listItem.Add(shopItem);
                            }
                        }
                    }
                    return listItem;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_ShopItem> GetView_ShopItemInStock(string nick, string approve_status, bool isHistory)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_ShopItem> shopItemList = alading.SP_GetView_ShopItemInStock(nick, approve_status, isHistory).ToList();
                    return shopItemList;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}

