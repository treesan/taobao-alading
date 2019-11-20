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
        public ReturnType AddConsumerAddress(ConsumerAddress consumeraddress)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToConsumerAddress(consumeraddress);
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

        public ReturnType AddConsumerAddress(List<ConsumerAddress> consumeraddressList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (ConsumerAddress consumeraddress in consumeraddressList)
                    {
                        alading.AddToConsumerAddress(consumeraddress);
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

        public ReturnType RemoveAllConsumerAddress()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ConsumerAddress> list = alading.ConsumerAddress.ToList();
                    foreach (ConsumerAddress consumeraddress in list)
                    {
                        alading.DeleteObject(consumeraddress);
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

        public ReturnType RemoveConsumerAddress(Func<ConsumerAddress, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ConsumerAddress> list = alading.ConsumerAddress.Where(func).ToList();
                    foreach (ConsumerAddress consumeraddress in list)
                    {
                        alading.DeleteObject(consumeraddress);
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

        public List<ConsumerAddress> GetConsumerAddress(List<int> consumeraddressIDList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var result = alading.ConsumerAddress.Where(BuildWhereInExpression<ConsumerAddress, int>(v => v.ConsumerAddressID, consumeraddressIDList));
                    //var result = alading.ConsumerAddress.Where(BuildWhereInExpression<ConsumerAddress, string>(v => v.ConsumerAddressCode, consumeraddressCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveConsumerAddress(List<int> consumeraddressIDList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var result = alading.ConsumerAddress.Where(BuildWhereInExpression<ConsumerAddress, int>(v => v.ConsumerAddressID, consumeraddressIDList));
                    //var result = alading.ConsumerAddress.Where(BuildWhereInExpression<ConsumerAddress, int>(v => v.ConsumerAddressID, consumeraddressIDList));
                    foreach (ConsumerAddress s in result)
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

        public ReturnType RemoveConsumerAddress(int consumeraddressID)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ConsumerAddress> list = alading.ConsumerAddress.Where(p => p.ConsumerAddressID == consumeraddressID).ToList();
                    //List<ConsumerAddress> list = alading.ConsumerAddress.Where(p => p.ConsumerAddressCode == consumeraddressCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        ConsumerAddress sy = list.First();
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

        public ReturnType UpdateConsumerAddress(ConsumerAddress consumeraddress)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    ConsumerAddress result = alading.ConsumerAddress.Where(p => p.ConsumerAddressID == consumeraddress.ConsumerAddressID).FirstOrDefault();
                    //ConsumerAddress result = alading.ConsumerAddress.Where(p => p.ConsumerAddressCode == consumeraddress.ConsumerAddressCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("ConsumerAddress", consumeraddress);
                    #endregion

                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.buyer_nick = consumeraddress.buyer_nick;
                    
                        result.location_state = consumeraddress.location_state;
                    
                        result.location_city = consumeraddress.location_city;
                    
                        result.location_district = consumeraddress.location_district;
                    
                        result.location_address = consumeraddress.location_address;
                    
                        result.location_zip = consumeraddress.location_zip;
                    
                        result.location_country = consumeraddress.location_country;
			
                    */
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

        public ReturnType UpdateConsumerAddress(int consumeraddressID, ConsumerAddress consumeraddress)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var result = alading.ConsumerAddress.Where(p => p.ConsumerAddressID == consumeraddressID).ToList();
                    //var result = alading.ConsumerAddress.Where(p => p.ConsumerAddressCode == consumeraddressCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    ConsumerAddress ob = result.First();
                    ob.buyer_nick = consumeraddress.buyer_nick;
                    ob.location_state = consumeraddress.location_state;
                    ob.location_city = consumeraddress.location_city;
                    ob.location_district = consumeraddress.location_district;
                    ob.location_address = consumeraddress.location_address;
                    ob.location_zip = consumeraddress.location_zip;
                    ob.location_country = consumeraddress.location_country;

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

        public List<ConsumerAddress> GetAllConsumerAddress()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ConsumerAddress> list = alading.ConsumerAddress.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<ConsumerAddress> GetConsumerAddress(Func<ConsumerAddress, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ConsumerAddress> list = alading.ConsumerAddress.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public ConsumerAddress GetConsumerAddress(int consumeraddressID)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ConsumerAddress> list = alading.ConsumerAddress.Where(p => p.ConsumerAddressID == consumeraddressID).ToList();
                    //List<ConsumerAddress> list = alading.ConsumerAddress.Where(p => p.ConsumerAddressCode == consumeraddressCode).ToList();
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

        public List<ConsumerAddress> GetConsumerAddress(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var ob = (from u in alading.ConsumerAddress orderby u.ConsumerAddressID descending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    rowCount = alading.ConsumerAddress.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<ConsumerAddress> GetConsumerAddress(Func<ConsumerAddress, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<ConsumerAddress> list = alading.ConsumerAddress.Where(func).OrderByDescending(a => a.ConsumerAddressID);
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

