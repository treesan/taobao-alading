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
     
        public ReturnType AddTradeRefundMessage(TradeRefundMessage traderefundmessage)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToTradeRefundMessage(traderefundmessage);
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
                
        public ReturnType AddTradeRefundMessage(List<TradeRefundMessage> traderefundmessageList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (TradeRefundMessage traderefundmessage in traderefundmessageList)
                    {
                        alading.AddToTradeRefundMessage(traderefundmessage);
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
       
        public ReturnType RemoveAllTradeRefundMessage()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<TradeRefundMessage> list = alading.TradeRefundMessage.ToList();
                    foreach (TradeRefundMessage traderefundmessage in list)
                    {
                        alading.DeleteObject(traderefundmessage);
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
       
        public ReturnType RemoveTradeRefundMessage(Func<TradeRefundMessage, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<TradeRefundMessage> list = alading.TradeRefundMessage.Where(func).ToList();
                    foreach (TradeRefundMessage traderefundmessage in list)
                    {
                        alading.DeleteObject(traderefundmessage);
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

        public List<TradeRefundMessage> GetTradeRefundMessage(List<int> refundidList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.TradeRefundMessage.Where(BuildWhereInExpression<TradeRefundMessage, int>(v => v.TradeRefundMessageID, traderefundmessageIDList));*/
                    var result = alading.TradeRefundMessage.Where(BuildWhereInExpression<TradeRefundMessage, int>(v => (int)v.refund_id, refundidList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveTradeRefundMessage(List<int> refundidList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.TradeRefundMessage.Where(BuildWhereInExpression<TradeRefundMessage, int>(v => v.TradeRefundMessageID, traderefundmessageIDList));*/
                    var result = alading.TradeRefundMessage.Where(BuildWhereInExpression<TradeRefundMessage, int>(v => (int)v.refund_id, refundidList));
                    foreach (TradeRefundMessage s in result)
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

    
        public ReturnType RemoveTradeRefundMessage(int refundid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<TradeRefundMessage> list = alading.TradeRefundMessage.Where(p => p.TradeRefundMessageID == traderefundmessageID).ToList();*/
                    List<TradeRefundMessage> list = alading.TradeRefundMessage.Where(p => p.refund_id == refundid).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        TradeRefundMessage sy = list.First();
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
      
        public ReturnType UpdateTradeRefundMessage(TradeRefundMessage traderefundmessage)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*TradeRefundMessage result = alading.TradeRefundMessage.Where(p => p.TradeRefundMessageID == traderefundmessage.TradeRefundMessageID).FirstOrDefault();*/
                    TradeRefundMessage result = alading.TradeRefundMessage.Where(p => p.refund_id == traderefundmessage.refund_id).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("TradeRefundMessage", traderefundmessage);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.id = traderefundmessage.id;
                    
                        result.refund_id = traderefundmessage.refund_id;
                    
                        result.owner_id = traderefundmessage.owner_id;
                    
                        result.owner_nick = traderefundmessage.owner_nick;
                    
                        result.owner_role = traderefundmessage.owner_role;
                    
                        result.content = traderefundmessage.content;
                    
                        result.pic_urls = traderefundmessage.pic_urls;
                    
                        result.created = traderefundmessage.created;
			
                    */
                    #endregion  
					if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
                    }
                    return ReturnType.OthersError;
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
       
        public ReturnType UpdateTradeRefundMessage(int refundid, TradeRefundMessage traderefundmessage)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.TradeRefundMessage.Where(p => p.TradeRefundMessageID == traderefundmessageID).ToList();*/
                    var result = alading.TradeRefundMessage.Where(p => p.refund_id == refundid).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    TradeRefundMessage ob = result.First();
                    ob.id = traderefundmessage.id;
                    ob.refund_id = traderefundmessage.refund_id;
                    ob.owner_id = traderefundmessage.owner_id;
                    ob.owner_nick = traderefundmessage.owner_nick;
                    ob.owner_role = traderefundmessage.owner_role;
                    ob.content = traderefundmessage.content;
                    ob.pic_urls = traderefundmessage.pic_urls;
                    ob.created = traderefundmessage.created;
                    
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
     
        public List<TradeRefundMessage> GetAllTradeRefundMessage()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<TradeRefundMessage> list = alading.TradeRefundMessage.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<TradeRefundMessage> GetTradeRefundMessage(Func<TradeRefundMessage, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<TradeRefundMessage> list = alading.TradeRefundMessage.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public TradeRefundMessage GetTradeRefundMessage(int refundid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<TradeRefundMessage> list = alading.TradeRefundMessage.Where(p => p.TradeRefundMessageID == traderefundmessageID).ToList();*/
                    List<TradeRefundMessage> list = alading.TradeRefundMessage.Where(p => p.refund_id == refundid).ToList();
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
        
        public List<TradeRefundMessage> GetTradeRefundMessage(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.TradeRefundMessage orderby u.RefundMessageID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.TradeRefundMessage.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<TradeRefundMessage> GetTradeRefundMessage(Func<TradeRefundMessage, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<TradeRefundMessage> list = alading.TradeRefundMessage.Where(func).OrderByDescending(a=>a.RefundMessageID);
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

