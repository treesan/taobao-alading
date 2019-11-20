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
     
        public ReturnType AddTradeRate(TradeRate traderate)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToTradeRate(traderate);
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
                
        public ReturnType AddTradeRate(List<TradeRate> traderateList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (TradeRate traderate in traderateList)
                    {
                        alading.AddToTradeRate(traderate);
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
       
        public ReturnType RemoveAllTradeRate()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<TradeRate> list = alading.TradeRate.ToList();
                    foreach (TradeRate traderate in list)
                    {
                        alading.DeleteObject(traderate);
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
       
        public ReturnType RemoveTradeRate(Func<TradeRate, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<TradeRate> list = alading.TradeRate.Where(func).ToList();
                    foreach (TradeRate traderate in list)
                    {
                        alading.DeleteObject(traderate);
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

        public List<TradeRate> GetTradeRate(List<string> traderateCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.TradeRate.Where(BuildWhereInExpression<TradeRate, int>(v => v.TradeRateID, traderateIDList));*/
                    var result = alading.TradeRate.Where(BuildWhereInExpression<TradeRate, string>(v => v.TradeRateCode, traderateCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public List<TradeRate> GetTradeRateByTid(List<string> tidList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.TradeRate.Where(BuildWhereInExpression<TradeRate, int>(v => v.TradeRateID, traderateIDList));*/
                    var result = alading.TradeRate.Where(BuildWhereInExpression<TradeRate, string>(v => v.tid, tidList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveTradeRate(List<string> traderateCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.TradeRate.Where(BuildWhereInExpression<TradeRate, int>(v => v.TradeRateID, traderateIDList));*/
                    var result = alading.TradeRate.Where(BuildWhereInExpression<TradeRate, string>(v => v.TradeRateCode, traderateCodeList));
                    foreach (TradeRate s in result)
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
    
        public ReturnType RemoveTradeRate(string traderateCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<TradeRate> list = alading.TradeRate.Where(p => p.TradeRateID == traderateID).ToList();*/
                    List<TradeRate> list = alading.TradeRate.Where(p => p.TradeRateCode == traderateCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        TradeRate sy = list.First();
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
      
        public ReturnType UpdateTradeRate(TradeRate traderate)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*TradeRate result = alading.TradeRate.Where(p => p.TradeRateID == traderate.TradeRateID).FirstOrDefault();*/
                    TradeRate result = alading.TradeRate.Where(p => p.TradeRateCode == traderate.TradeRateCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("TradeRate", traderate);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.tid = traderate.tid;
                    
                        result.oid = traderate.oid;
                    
                        result.role = traderate.role;
                    
                        result.nick = traderate.nick;
                    
                        result.result = traderate.result;
                    
                        result.created = traderate.created;
                    
                        result.rated_nick = traderate.rated_nick;
                    
                        result.item_title = traderate.item_title;
                    
                        result.item_price = traderate.item_price;
                    
                        result.content = traderate.content;
                    
                        result.reply = traderate.reply;
			
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
       
        public ReturnType UpdateTradeRate(string traderateCode, TradeRate traderate)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.TradeRate.Where(p => p.TradeRateID == traderateID).ToList();*/
                    var result = alading.TradeRate.Where(p => p.TradeRateCode == traderateCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    TradeRate ob = result.First();
                    ob.tid = traderate.tid;
                    ob.oid = traderate.oid;
                    ob.role = traderate.role;
                    ob.nick = traderate.nick;
                    ob.result = traderate.result;
                    ob.created = traderate.created;
                    ob.rated_nick = traderate.rated_nick;
                    ob.item_title = traderate.item_title;
                    ob.item_price = traderate.item_price;
                    ob.content = traderate.content;
                    ob.reply = traderate.reply;
                    
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
     
        public List<TradeRate> GetAllTradeRate()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<TradeRate> list = alading.TradeRate.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<TradeRate> GetTradeRate(Func<TradeRate, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<TradeRate> list = alading.TradeRate.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public TradeRate GetTradeRate(string traderateCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<TradeRate> list = alading.TradeRate.Where(p => p.TradeRateID == traderateID).ToList();*/
                    List<TradeRate> list = alading.TradeRate.Where(p => p.TradeRateCode == traderateCode).ToList();
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
        
        public List<TradeRate> GetTradeRate(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.TradeRate orderby u.TradeRateID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.TradeRate.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<TradeRate> GetTradeRate(Func<TradeRate, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<TradeRate> list = alading.TradeRate.Where(func).OrderByDescending(a=>a.TradeRateID);
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

