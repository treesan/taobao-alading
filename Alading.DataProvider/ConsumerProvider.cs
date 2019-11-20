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
     
        public ReturnType AddConsumer(Consumer consumer)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    alading.AddToConsumer(consumer);
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
                
        public ReturnType AddConsumer(List<Consumer> consumerList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (Consumer consumer in consumerList)
                    {
                        alading.AddToConsumer(consumer);
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
       
        public ReturnType RemoveAllConsumer()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Consumer> list = alading.Consumer.ToList();
                    foreach (Consumer consumer in list)
                    {
                        alading.DeleteObject(consumer);
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
       
        public ReturnType RemoveConsumer(Func<Consumer, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Consumer> list = alading.Consumer.Where(func).ToList();
                    foreach (Consumer consumer in list)
                    {
                        alading.DeleteObject(consumer);
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

        public List<Consumer> GetConsumer(List<string> consumerNickList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.Consumer.Where(BuildWhereInExpression<Consumer, int>(v => v.ConsumerID, consumerIDList));*/
                    var result = alading.Consumer.Where(BuildWhereInExpression<Consumer, string>(v => v.nick, consumerNickList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveConsumer(List<string> consumerNickList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.Consumer.Where(BuildWhereInExpression<Consumer, int>(v => v.ConsumerID, consumerIDList));*/
                    var result = alading.Consumer.Where(BuildWhereInExpression<Consumer, string>(v => v.nick, consumerNickList));
                    foreach (Consumer s in result)
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
    
        public ReturnType RemoveConsumer(string consumerNick)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<Consumer> list = alading.Consumer.Where(p => p.ConsumerID == consumerID).ToList();*/
                    List<Consumer> list = alading.Consumer.Where(p => p.nick == consumerNick).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        Consumer sy = list.First();
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
      
        public ReturnType UpdateConsumer(Consumer consumer)
        {
            consumer.@checked = false;
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*Consumer result = alading.Consumer.Where(p => p.ConsumerID == consumer.ConsumerID).FirstOrDefault();*/
                    Consumer result = alading.Consumer.Where(p => p.nick == consumer.nick).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("Consumer", consumer);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.nick = consumer.nick;
                    
                        result.sex = consumer.sex;
                    
                        result.location_city = consumer.location_city;
                    
                        result.location_state = consumer.location_state;
                    
                        result.birthday = consumer.birthday;
                    
                        result.mobilephone = consumer.mobilephone;
                    
                        result.phone = consumer.phone;
                    
                        result.email = consumer.email;
                    
                        result.credit = consumer.credit;
                    
                        result.level = consumer.level;
                    
                        result.score = consumer.score;
                    
                        result.historytradetimes = consumer.historytradetimes;
                    
                        result.historyexpense = consumer.historyexpense;
                    
                        result.comments = consumer.comments;
                    
                        result.vip = consumer.vip;
                    
                        result.alipay = consumer.alipay;
                    
                        result.created = consumer.created;
			
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
       
        public ReturnType UpdateConsumer(string consumerNick, Consumer consumer)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.Consumer.Where(p => p.ConsumerID == consumerID).ToList();*/
                    var result = alading.Consumer.Where(p => p.nick ==consumerNick) .ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    Consumer ob = result.First();
                    ob.nick = consumer.nick;
                    ob.sex = consumer.sex;
                    ob.location_city = consumer.location_city;
                    ob.location_state = consumer.location_state;
                    ob.birthday = consumer.birthday;
                    ob.mobilephone = consumer.mobilephone;
                    ob.phone = consumer.phone;
                    ob.email = consumer.email;
                    ob.credit = consumer.credit;
                    ob.level = consumer.level;
                    ob.score = consumer.score;
                    ob.historytradetimes = consumer.historytradetimes;
                    ob.historyexpense = consumer.historyexpense;
                    ob.comments = consumer.comments;
                    ob.vip = consumer.vip;
                    ob.alipay = consumer.alipay;
                    ob.created = consumer.created;
                    
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
     
        public List<Consumer> GetAllConsumer()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Consumer> list = alading.Consumer.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<Consumer> GetConsumer(Func<Consumer, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Consumer> list = alading.Consumer.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public Consumer GetConsumer(string consumerNick)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<Consumer> list = alading.Consumer.Where(p => p.ConsumerID == consumerID).ToList();*/
                    List<Consumer> list = alading.Consumer.Where(p => p.nick == consumerNick).ToList();
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
        
        public List<Consumer> GetConsumer(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.Consumer orderby u.ConsumerID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.Consumer.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<Consumer> GetConsumer(Func<Consumer, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<Consumer> list = alading.Consumer.Where(func).OrderByDescending(a=>a.ConsumerID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<Consumer> GetConsumer(Func<Consumer, bool> func, string orderType, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    string orderby = string.Format("it.{0} DESC", orderType);
                    var list = alading.Consumer.OrderBy(orderby).Where(func);
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

