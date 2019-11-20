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
        public ReturnType AddUserShop(UserShop usershop)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToUserShop(usershop);
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

        public ReturnType AddUserShop(List<UserShop> usershopList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (UserShop usershop in usershopList)
                    {
                        alading.AddToUserShop(usershop);
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

        public ReturnType RemoveAllUserShop()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<UserShop> list = alading.UserShop.ToList();
                    foreach (UserShop usershop in list)
                    {
                        alading.DeleteObject(usershop);
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

        public ReturnType RemoveUserShop(Func<UserShop, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<UserShop> list = alading.UserShop.Where(func).ToList();
                    foreach (UserShop usershop in list)
                    {
                        alading.DeleteObject(usershop);
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

        public List<UserShop> GetUserShop(List<long> usershopIdList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.UserShop.Where(BuildWhereInExpression<UserShop, int>(v => v.UserShopID, usershopIDList));*/
                    var result = alading.UserShop.Where(BuildWhereInExpression<UserShop, long>(v => v.Id, usershopIdList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveUserShop(List<long> usershopIdList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.UserShop.Where(BuildWhereInExpression<UserShop, int>(v => v.UserShopID, usershopIDList));*/
                    var result = alading.UserShop.Where(BuildWhereInExpression<UserShop, long>(v => v.Id, usershopIdList));
                    foreach (UserShop s in result)
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

        public ReturnType RemoveUserShop(long usershopId)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<UserShop> list = alading.UserShop.Where(p => p.UserShopID == usershopID).ToList();*/
                    List<UserShop> list = alading.UserShop.Where(p => p.Id == usershopId).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        UserShop sy = list.First();
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

        public ReturnType UpdateUserShop(UserShop usershop)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*UserShop result = alading.UserShop.Where(p => p.UserShopID == usershop.UserShopID).FirstOrDefault();*/
                    UserShop result = alading.UserShop.Where(p => p.Id == usershop.Id).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("UserShop", usershop);
                    #endregion

                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.Id = usershop.Id;
                    
                        result.UserCode = usershop.UserCode;
                    
                        result.ShopId = usershop.ShopId;
                    
                        result.Enable = usershop.Enable;
			
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

        public ReturnType UpdateUserShop(long usershopId, UserShop usershop)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.UserShop.Where(p => p.UserShopID == usershopID).ToList();*/
                    var result = alading.UserShop.Where(p => p.Id == usershopId).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    UserShop ob = result.First();
                    ob.Id = usershop.Id;
                    ob.UserCode = usershop.UserCode;
                    ob.ShopId = usershop.ShopId;

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

        public List<UserShop> GetAllUserShop()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<UserShop> list = alading.UserShop.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<UserShop> GetUserShop(Func<UserShop, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<UserShop> list = alading.UserShop.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public UserShop GetUserShop(long usershopId)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<UserShop> list = alading.UserShop.Where(p => p.UserShopID == usershopID).ToList();*/
                    List<UserShop> list = alading.UserShop.Where(p => p.Id == usershopId).ToList();
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

        public List<UserShop> GetUserShop(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var ob = (from u in alading.UserShop orderby u.Id descending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    rowCount = alading.UserShop.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<UserShop> GetUserShop(Func<UserShop, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<UserShop> list = alading.UserShop.Where(func).OrderByDescending(a => a.Id);
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

