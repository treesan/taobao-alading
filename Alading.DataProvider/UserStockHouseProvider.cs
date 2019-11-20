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
        public ReturnType AddUserStockHouse(UserStockHouse userstockhouse)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToUserStockHouse(userstockhouse);
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

        public ReturnType AddUserStockHouse(List<UserStockHouse> userstockhouseList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (UserStockHouse userstockhouse in userstockhouseList)
                    {
                        alading.AddToUserStockHouse(userstockhouse);
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

        public ReturnType RemoveAllUserStockHouse()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<UserStockHouse> list = alading.UserStockHouse.ToList();
                    foreach (UserStockHouse userstockhouse in list)
                    {
                        alading.DeleteObject(userstockhouse);
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

        public ReturnType RemoveUserStockHouse(Func<UserStockHouse, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<UserStockHouse> list = alading.UserStockHouse.Where(func).ToList();
                    foreach (UserStockHouse userstockhouse in list)
                    {
                        alading.DeleteObject(userstockhouse);
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

        public List<UserStockHouse> GetUserStockHouse(List<long> userstockhouseIdList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.UserStockHouse.Where(BuildWhereInExpression<UserStockHouse, int>(v => v.UserStockHouseID, userstockhouseIDList));*/
                    var result = alading.UserStockHouse.Where(BuildWhereInExpression<UserStockHouse, long>(v => v.Id, userstockhouseIdList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveUserStockHouse(List<long> userstockhouseIdList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.UserStockHouse.Where(BuildWhereInExpression<UserStockHouse, int>(v => v.UserStockHouseID, userstockhouseIDList));*/
                    var result = alading.UserStockHouse.Where(BuildWhereInExpression<UserStockHouse, long>(v => v.Id, userstockhouseIdList));
                    foreach (UserStockHouse s in result)
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

        public ReturnType RemoveUserStockHouse(long userstockhouseId)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<UserStockHouse> list = alading.UserStockHouse.Where(p => p.UserStockHouseID == userstockhouseID).ToList();*/
                    List<UserStockHouse> list = alading.UserStockHouse.Where(p => p.Id == userstockhouseId).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        UserStockHouse sy = list.First();
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

        public ReturnType UpdateUserStockHouse(UserStockHouse userstockhouse)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*UserStockHouse result = alading.UserStockHouse.Where(p => p.UserStockHouseID == userstockhouse.UserStockHouseID).FirstOrDefault();*/
                    UserStockHouse result = alading.UserStockHouse.Where(p => p.Id == userstockhouse.Id).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("UserStockHouse", userstockhouse);
                    #endregion

                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.Id = userstockhouse.Id;
                    
                        result.UserId = userstockhouse.UserId;
                    
                        result.StockHouseId = userstockhouse.StockHouseId;
                    
                        result.Enable = userstockhouse.Enable;
			
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

        public ReturnType UpdateUserStockHouse(long userstockhouseId, UserStockHouse userstockhouse)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.UserStockHouse.Where(p => p.UserStockHouseID == userstockhouseID).ToList();*/
                    var result = alading.UserStockHouse.Where(p => p.Id == userstockhouseId).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    UserStockHouse ob = result.First();
                    ob.Id = userstockhouse.Id;
                    ob.UserCode = userstockhouse.UserCode;
                    ob.StockHouseCode = userstockhouse.StockHouseCode;

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

        public List<UserStockHouse> GetAllUserStockHouse()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<UserStockHouse> list = alading.UserStockHouse.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<UserStockHouse> GetUserStockHouse(Func<UserStockHouse, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<UserStockHouse> list = alading.UserStockHouse.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public UserStockHouse GetUserStockHouse(long userstockhouseId)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<UserStockHouse> list = alading.UserStockHouse.Where(p => p.UserStockHouseID == userstockhouseID).ToList();*/
                    List<UserStockHouse> list = alading.UserStockHouse.Where(p => p.Id == userstockhouseId).ToList();
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

        public List<UserStockHouse> GetUserStockHouse(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var ob = (from u in alading.UserStockHouse orderby u.Id descending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    rowCount = alading.UserStockHouse.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<UserStockHouse> GetUserStockHouse(Func<UserStockHouse, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<UserStockHouse> list = alading.UserStockHouse.Where(func).OrderByDescending(a => a.Id);
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

