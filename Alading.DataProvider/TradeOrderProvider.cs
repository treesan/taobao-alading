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
     
        public ReturnType AddTradeOrder(TradeOrder tradeorder)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToTradeOrder(tradeorder);
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
                
        public ReturnType AddTradeOrder(List<TradeOrder> tradeorderList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (TradeOrder tradeorder in tradeorderList)
                    {
                        alading.AddToTradeOrder(tradeorder);
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
       
        public ReturnType RemoveAllTradeOrder()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<TradeOrder> list = alading.TradeOrder.ToList();
                    foreach (TradeOrder tradeorder in list)
                    {
                        alading.DeleteObject(tradeorder);
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
       
        public ReturnType RemoveTradeOrder(Func<TradeOrder, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<TradeOrder> list = alading.TradeOrder.Where(func).ToList();
                    foreach (TradeOrder tradeorder in list)
                    {
                        alading.DeleteObject(tradeorder);
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

        public List<TradeOrder> GetTradeOrder(List<string> tradeorderCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.TradeOrder.Where(BuildWhereInExpression<TradeOrder, int>(v => v.TradeOrderID, tradeorderIDList));*/
                    var result = alading.TradeOrder.Where(BuildWhereInExpression<TradeOrder, string>(v => v.TradeOrderCode, tradeorderCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public List<TradeOrder> GetTradeOrderByCTid(List<string> customTidList)//zxl
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.TradeOrder.Where(BuildWhereInExpression<TradeOrder, int>(v => v.TradeOrderID, tradeorderIDList));*/
                    var result = alading.TradeOrder.Where(BuildWhereInExpression<TradeOrder, string>(v => v.CustomTid, customTidList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveTradeOrder(List<string> tradeorderCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.TradeOrder.Where(BuildWhereInExpression<TradeOrder, int>(v => v.TradeOrderID, tradeorderIDList));*/
                    var result = alading.TradeOrder.Where(BuildWhereInExpression<TradeOrder, string>(v => v.TradeOrderCode, tradeorderCodeList));
                    foreach (TradeOrder s in result)
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


        public ReturnType RemoveTradeOrder(string customTid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<TradeOrder> list = alading.TradeOrder.Where(p => p.TradeOrderID == tradeorderID).ToList();*/
                    List<TradeOrder> list = alading.TradeOrder.Where(p => p.CustomTid == customTid).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        TradeOrder sy = list.First();
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
      
        public ReturnType UpdateTradeOrder(TradeOrder tradeorder)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*TradeOrder result = alading.TradeOrder.Where(p => p.TradeOrderID == tradeorder.TradeOrderID).FirstOrDefault();*/
                    TradeOrder result = alading.TradeOrder.Where(p => p.TradeOrderCode == tradeorder.TradeOrderCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    //alading.Attach(result);
                    //alading.ApplyPropertyChanges("TradeOrder", tradeorder);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    result.CustomTid = tradeorder.CustomTid;
                    result.iid = tradeorder.iid;
                    result.sku_id = tradeorder.sku_id;
                    result.oid = tradeorder.oid;
                    result.outer_sku_id = tradeorder.outer_sku_id;
                    result.outer_iid = tradeorder.outer_iid;
                    result.sku_properties_name = tradeorder.sku_properties_name;
                    result.price = tradeorder.price;
                    result.total_fee = tradeorder.total_fee;
                    result.discount_fee = tradeorder.discount_fee;
                    result.adjust_fee = tradeorder.adjust_fee;
                    result.payment = tradeorder.payment;
                    result.item_meal_name = tradeorder.item_meal_name;
                    result.num = tradeorder.num;
                    result.title = tradeorder.title;
                    result.pic_path = tradeorder.pic_path;
                    result.seller_nick = tradeorder.seller_nick;
                    result.buyer_nick = tradeorder.buyer_nick;
                    result.type = tradeorder.type;
                    result.created = tradeorder.created;
                    result.refund_status = tradeorder.refund_status;
                    result.status = tradeorder.status;
                    result.seller_type = tradeorder.seller_type;
                    result.snapshot_url = tradeorder.snapshot_url;
                    result.snapshot = tradeorder.snapshot;
                    result.timeout_action_time = tradeorder.timeout_action_time;
                    result.OrderType = tradeorder.OrderType;
                    result.OrderTimeStamp = tradeorder.OrderTimeStamp;
                    result.HouseCode = tradeorder.HouseCode;
                    result.LayoutCode = tradeorder.LayoutCode;
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

        public void copyTradeOrder(TradeOrder result, TradeOrder tradeorder)
        {
            result.CustomTid = tradeorder.CustomTid;
            result.TradeOrderCode = tradeorder.TradeOrderCode;
            result.iid = tradeorder.iid;
            result.sku_id = tradeorder.sku_id;
            result.oid = tradeorder.oid;
            result.outer_sku_id = tradeorder.outer_sku_id;
            result.outer_iid = tradeorder.outer_iid;
            result.sku_properties_name = tradeorder.sku_properties_name;
            result.price = tradeorder.price;
            result.total_fee = tradeorder.total_fee;
            result.discount_fee = tradeorder.discount_fee;
            result.adjust_fee = tradeorder.adjust_fee;
            result.payment = tradeorder.payment;
            result.item_meal_name = tradeorder.item_meal_name;
            result.num = tradeorder.num;
            result.title = tradeorder.title;
            result.pic_path = tradeorder.pic_path;
            result.seller_nick = tradeorder.seller_nick;
            result.buyer_nick = tradeorder.buyer_nick;
            result.type = tradeorder.type;
            result.created = tradeorder.created;
            result.refund_status = tradeorder.refund_status;
            result.status = tradeorder.status;
            result.seller_type = tradeorder.seller_type;
            result.snapshot_url = tradeorder.snapshot_url;
            result.snapshot = tradeorder.snapshot;
            result.timeout_action_time = tradeorder.timeout_action_time;
            result.OrderType = tradeorder.OrderType;
            result.OrderTimeStamp = tradeorder.OrderTimeStamp;
            result.HouseCode = tradeorder.HouseCode;
            result.LayoutCode = tradeorder.LayoutCode;
        }

        public ReturnType UpdateTradeOrderPicCode(string TradeOrderCode, string pictureCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var result = alading.TradeOrder.Where(p => p.TradeOrderCode == TradeOrderCode).FirstOrDefault();
                    if (result==null)
                    {
                        return ReturnType.NotExisted;
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

        public ReturnType UpdateTradeOrder(string TradeOrderCode, TradeOrder tradeorder)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.TradeOrder.Where(p => p.TradeOrderID == tradeorderID).ToList();*/
                    var result = alading.TradeOrder.Where(p => p.TradeOrderCode == TradeOrderCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    TradeOrder ob = result.First();
                    ob.CustomTid = tradeorder.CustomTid;
                    ob.iid = tradeorder.iid;
                    ob.sku_id = tradeorder.sku_id;
                    ob.oid = tradeorder.oid;
                    ob.outer_sku_id = tradeorder.outer_sku_id;
                    ob.outer_iid = tradeorder.outer_iid;
                    ob.sku_properties_name = tradeorder.sku_properties_name;
                    ob.price = tradeorder.price;
                    ob.total_fee = tradeorder.total_fee;
                    ob.discount_fee = tradeorder.discount_fee;
                    ob.adjust_fee = tradeorder.adjust_fee;
                    ob.payment = tradeorder.payment;
                    ob.item_meal_name = tradeorder.item_meal_name;
                    ob.num = tradeorder.num;
                    ob.title = tradeorder.title;
                    ob.pic_path = tradeorder.pic_path;
                    ob.seller_nick = tradeorder.seller_nick;
                    ob.buyer_nick = tradeorder.buyer_nick;
                    ob.type = tradeorder.type;
                    ob.created = tradeorder.created;
                    ob.refund_status = tradeorder.refund_status;
                    ob.status = tradeorder.status;
                    ob.snapshot_url = tradeorder.snapshot_url;
                    ob.snapshot = tradeorder.snapshot;
                    ob.timeout_action_time = tradeorder.timeout_action_time;
                    
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
     
        public List<TradeOrder> GetAllTradeOrder()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<TradeOrder> list = alading.TradeOrder.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<TradeOrder> GetTradeOrder(Func<TradeOrder, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<TradeOrder> list = alading.TradeOrder.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public TradeOrder GetTradeOrder(string TradeOrderCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<TradeOrder> list = alading.TradeOrder.Where(p => p.TradeOrderID == tradeorderID).ToList();*/
                    List<TradeOrder> list = alading.TradeOrder.Where(p => p.TradeOrderCode == TradeOrderCode).ToList();
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
        
        public List<TradeOrder> GetTradeOrder(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.TradeOrder orderby u.TradeOrderID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.TradeOrder.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<TradeOrder> GetTradeOrder(Func<TradeOrder, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<TradeOrder> list = alading.TradeOrder.Where(func).OrderByDescending(a=>a.TradeOrderID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }        
    }
}

