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
     
        public ReturnType AddShop(Shop shop)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToShop(shop);
                    if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
                    }
                    else
                    {
                        return ReturnType.PropertyExisted;
                    }
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (Exception ex)
            {
                return ReturnType.OthersError;
            }

        }
                
        public ReturnType AddShop(List<Shop> shopList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (Shop shop in shopList)
                    {
                        alading.AddToShop(shop);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (Exception ex)
            {
                return ReturnType.OthersError;
            }

        }
       
        public ReturnType RemoveAllShop()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Shop> list = alading.Shop.ToList();
                    foreach (Shop shop in list)
                    {
                        alading.DeleteObject(shop);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;

                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }
       
        public ReturnType RemoveShop(Func<Shop, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Shop> list = alading.Shop.Where(func).ToList();
                    foreach (Shop shop in list)
                    {
                        alading.DeleteObject(shop);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }

            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

        public List<Shop> GetShop(List<string> sidList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.Shop.Where(BuildWhereInExpression<Shop, int>(v => v.ShopID, shopIDList));*/
                    var result = alading.Shop.Where(BuildWhereInExpression<Shop, string>(v => v.sid, sidList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveShop(List<string> sidList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.Shop.Where(BuildWhereInExpression<Shop, int>(v => v.ShopID, shopIDList));*/
                    var result = alading.Shop.Where(BuildWhereInExpression<Shop, string>(v => v.sid, sidList));
                    foreach (Shop s in result)
                    {
                        alading.DeleteObject(s);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

    
        public ReturnType RemoveShop(string sid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<Shop> list = alading.Shop.Where(p => p.ShopID == shopID).ToList();*/
                    List<Shop> list = alading.Shop.Where(p => p.sid == sid).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        Shop sy = list.First();
                        alading.DeleteObject(sy);
                        alading.SaveChanges();
                        return ReturnType.Success;
                    }
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }
      
        public ReturnType UpdateShop(Shop shop)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*Shop result = alading.Shop.Where(p => p.ShopID == shop.ShopID).FirstOrDefault();*/
                    Shop result = alading.Shop.Where(p => p.nick == shop.nick).FirstOrDefault(); // 这里要用nick查找，否则不能同步
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    alading.Attach(result);

                    #region    Using All Items Replace To Update ,Default UnUse
                    result.sid = shop.sid;
                    result.cid = shop.cid;
                    result.nick = shop.nick;
                    result.title = shop.title;
                    result.desc = shop.desc;
                    result.bulletin = shop.bulletin;
                    result.pic_path = shop.pic_path;
                    result.created = shop.created;
                    result.modified = shop.modified;
                    result.password = shop.password;
                    //result.item_score = shop.item_score;
                    //result.service_score = shop.service_score;
                    //result.delivery_score = shop.delivery_score;
                    result.sex = shop.sex;
                    result.user_created = shop.user_created;
                    result.last_visit = shop.last_visit;
                    result.birthday = shop.birthday;
                    result.type = shop.type;
                    result.has_more_pic = shop.has_more_pic;
                    result.item_img_num = shop.item_img_num;
                    result.item_img_size = shop.item_img_size;
                    result.prop_img_num = shop.prop_img_num;
                    result.prop_img_size = shop.prop_img_size;
                    result.auto_repost = shop.auto_repost;
                    result.promoted_type = shop.promoted_type;
                    result.status = shop.status;
                    result.alipay_bind = shop.alipay_bind;
                    result.consumer_protection = shop.consumer_protection;
                    result.alipay_account = shop.alipay_account;
                    result.alipay_no = shop.alipay_no;

                    #region buyer Info
                    result.buyer_zip = shop.buyer_zip;
                    result.buyer_address = shop.buyer_address;
                    result.buyer_city = shop.buyer_city;
                    result.buyer_state = shop.buyer_state;
                    result.buyer_country = shop.buyer_country;
                    result.buyer_district = shop.buyer_district;
                    result.buyer_level = shop.buyer_level;
                    result.buyer_score = shop.buyer_score;
                    result.buyer_total_num = shop.buyer_total_num;
                    result.buyer_good_num = shop.buyer_good_num;
                    result.buyer_credit = shop.buyer_credit;
                    #endregion

                    #region seller Info
                    result.seller_name = shop.seller_name;
                    result.seller_company = shop.seller_company;
                    result.seller_mobile = shop.seller_mobile;
                    result.seller_tel = shop.seller_tel;
                    result.seller_credit = shop.seller_credit;
                    result.seller_zip = shop.seller_zip;
                    result.seller_address = shop.seller_address;
                    result.seller_city = shop.seller_city;
                    result.seller_state = shop.seller_state;
                    result.seller_country = shop.seller_country;
                    result.seller_district = shop.seller_district;
                    result.seller_level = shop.seller_level;
                    result.seller_score = shop.seller_score;
                    result.seller_total_num = shop.seller_total_num;
                    result.seller_good_num = shop.seller_good_num;
                    #endregion

                    /*
                    #region db Info
                    result.db_ip = shop.db_ip;
                    result.db_name = shop.db_name;
                    result.db_user = shop.db_user;
                    result.db_password = shop.db_password;
                    result.db_port = shop.db_port;
                    result.db_prefix = shop.db_prefix;

                    result.SessionKey = shop.SessionKey;
                    result.SessionTime = shop.SessionTime;*/
                    result.ShopType = shop.ShopType;
                    result.ShopTypeName = shop.ShopTypeName;
                    /*result.LastSyncTime = shop.LastSyncTime;
                    result.LastSyncUser = shop.LastSyncUser;
                    #endregion
                    */
                    
                    #endregion  

                    alading.ApplyPropertyChanges("Shop", result);

                    #endregion
                    
                    if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
                    }
                    else
                    {
                        return ReturnType.OthersError;
                    }
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

        public ReturnType UpdateShop(string sid, Shop shop)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.Shop.Where(p => p.ShopID == shopID).ToList();*/
                    var result = alading.Shop.Where(p => p.sid == sid).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    Shop ob = result.First();
                    ob.sid = shop.sid;
                    ob.cid = shop.cid;
                    ob.nick = shop.nick;
                    ob.title = shop.title;
                    ob.desc = shop.desc;
                    ob.bulletin = shop.bulletin;
                    ob.pic_path = shop.pic_path;
                    ob.created = shop.created;
                    ob.modified = shop.modified;
                    ob.item_score = shop.item_score;
                    ob.service_score = shop.service_score;
                    ob.delivery_score = shop.delivery_score;
                    ob.SessionKey = shop.SessionKey;
                    ob.SessionTime = shop.SessionTime;
                    ob.ShopType = shop.ShopType;
                    ob.ShopTypeName = shop.ShopTypeName;
                    
                    if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
                    }  
                    else
                    {
                        return ReturnType.OthersError;
                    }
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

        public List<Shop> GetAllShop()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Shop> list = alading.Shop.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<Shop> GetShop(Func<Shop, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var Shop = alading.Shop.Where(func);
                    if (Shop != null)
                    {
                        return Shop.ToList();
                    }
                    else
                    {
                        return null;
                    }
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public Shop GetShop(string sid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<Shop> list = alading.Shop.Where(p => p.ShopID == shopID).ToList();*/
                    List<Shop> list = alading.Shop.Where(p => p.sid == sid).ToList();
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

        public Shop GetShopByNick(string nick)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                   return alading.Shop.FirstOrDefault(p => p.nick==nick);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<Shop> GetShop(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.Shop orderby u.ShopID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.Shop.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<Shop> GetShop(Func<Shop, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<Shop> list = alading.Shop.Where(func).OrderByDescending(a=>a.ShopID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType UpdateSessionkey(string nick, string sessionkey,DateTime sessiontime)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var result = alading.Shop.FirstOrDefault(s => s.nick == nick);
                   if (result==null)
                   {
                       return ReturnType.NotExisted;
                   }
                   else
                   {
                       result.SessionKey = sessionkey;
                       result.SessionTime = sessiontime;
                   }
                    if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
                    }
                    else
                    {
                        return ReturnType.OthersError;
                    }
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        } 
       
    }
}

