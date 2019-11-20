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
     
        public ReturnType AddUser(User user)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToUser(user);
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
                
        public ReturnType AddUser(List<User> userList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (User user in userList)
                    {
                        alading.AddToUser(user);
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
       
        public ReturnType RemoveAllUser()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<User> list = alading.User.ToList();
                    foreach (User user in list)
                    {
                        alading.DeleteObject(user);
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
       
        public ReturnType RemoveUser(Func<User, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<User> list = alading.User.Where(func).ToList();
                    foreach (User user in list)
                    {
                        alading.DeleteObject(user);
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

        public List<User> GetUser(List<string> userCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.User.Where(BuildWhereInExpression<User, int>(v => v.UserID, userIDList));*/
                    var result = alading.User.Where(BuildWhereInExpression<User, string>(v => v.UserCode, userCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveUser(List<string> userCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.User.Where(BuildWhereInExpression<User, int>(v => v.UserID, userIDList));*/
                    var result = alading.User.Where(BuildWhereInExpression<User, string>(v => v.UserCode, userCodeList));
                    foreach (User s in result)
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
   
        public ReturnType RemoveUser(string userCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<User> list = alading.User.Where(p => p.UserID == userID).ToList();*/
                    List<User> list = alading.User.Where(p => p.UserCode == userCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        User sy = list.First();
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
      
        public ReturnType UpdateUser(User user)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*User result = alading.User.Where(p => p.UserID == user.UserID).FirstOrDefault();*/
                    User result = alading.User.Where(p => p.UserCode == user.UserCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("User", user);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.UserCode = user.UserCode;
                    
                        result.nick = user.nick;
                    
                        result.sex = user.sex;
                    
                        result.created = user.created;
                    
                        result.last_visit = user.last_visit;
                    
                        result.birthday = user.birthday;
                    
                        result.type = user.type;
                    
                        result.has_more_pic = user.has_more_pic;
                    
                        result.item_img_num = user.item_img_num;
                    
                        result.item_img_size = user.item_img_size;
                    
                        result.prop_img_num = user.prop_img_num;
                    
                        result.prop_img_size = user.prop_img_size;
                    
                        result.auto_repost = user.auto_repost;
                    
                        result.promoted_type = user.promoted_type;
                    
                        result.status = user.status;
                    
                        result.alipay_bind = user.alipay_bind;
                    
                        result.consumer_protection = user.consumer_protection;
                    
                        result.alipay_account = user.alipay_account;
                    
                        result.alipay_no = user.alipay_no;
                    
                        result.buyer_zip = user.buyer_zip;
                    
                        result.buyer_address = user.buyer_address;
                    
                        result.buyer_city = user.buyer_city;
                    
                        result.buyer_state = user.buyer_state;
                    
                        result.buyer_country = user.buyer_country;
                    
                        result.buyer_district = user.buyer_district;
                    
                        result.buyer_level = user.buyer_level;
                    
                        result.buyer_score = user.buyer_score;
                    
                        result.buyer_total_num = user.buyer_total_num;
                    
                        result.buyer_good_num = user.buyer_good_num;
                    
                        result.buyer_credit = user.buyer_credit;
                    
                        result.seller_credit = user.seller_credit;
                    
                        result.seller_zip = user.seller_zip;
                    
                        result.seller_address = user.seller_address;
                    
                        result.seller_city = user.seller_city;
                    
                        result.seller_state = user.seller_state;
                    
                        result.seller_country = user.seller_country;
                    
                        result.seller_district = user.seller_district;
                    
                        result.seller_level = user.seller_level;
                    
                        result.seller_score = user.seller_score;
                    
                        result.seller_total_num = user.seller_total_num;
                    
                        result.seller_good_num = user.seller_good_num;
			
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

        public ReturnType UpdateUser(User user, List<Role> roles, List<Shop> shops, List<StockHouse> houses)
        {
            System.Data.Common.DbTransaction tran = null;
            using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            {
                try
                {
                    alading.Connection.Open();
                    tran = alading.Connection.BeginTransaction();

                    User u1 = alading.User.FirstOrDefault(c => c.UserCode == user.UserCode);

                    if (u1 != null)
                    {
                        //更新用户
                        alading.Attach(u1);
                        alading.ApplyPropertyChanges("User", user);

                        //更新用户角色
                        var result1 = alading.UserRole.Where(c => c.UserCode == u1.UserCode);
                        foreach (UserRole s1 in result1)
                        {
                            alading.DeleteObject(s1);
                        }
                        alading.SaveChanges();

                        foreach (var i1 in roles)
                        {
                            UserRole e1 = new UserRole
                            {
                                UserRoleCode = Guid.NewGuid().ToString(),
                                UserCode = u1.UserCode,
                                RoleCode = i1.RoleCode,
                                RoleType = i1.RoleType,
                            };
                            alading.AddToUserRole(e1);
                        }
                        alading.SaveChanges();

                        //更新用户和店铺的关联
                        var return2 = alading.UserShop.Where(c => c.UserCode == u1.UserCode);
                        foreach (UserShop s2 in return2)
                        {
                            alading.DeleteObject(s2);
                        }
                        alading.SaveChanges();

                        foreach (var i2 in shops)
                        {
                            UserShop e2 = new UserShop
                            {
                                UserCode = u1.UserCode,
                                ShopId = i2.sid,
                            };
                            alading.AddToUserShop(e2);
                        }
                        alading.SaveChanges();

                        //更新用户和仓库的关联
                        var return3 = alading.UserStockHouse.Where(c => c.UserCode == u1.UserCode);
                        foreach (UserStockHouse s3 in return3)
                        {
                            alading.DeleteObject(s3);
                        }
                        alading.SaveChanges();

                        foreach (var i3 in houses)
                        {
                            UserStockHouse e3 = new UserStockHouse
                            {
                                UserCode = u1.UserCode,
                                StockHouseCode = i3.StockHouseCode,
                            };
                            alading.AddToUserStockHouse(e3);
                        }
                        alading.SaveChanges();

                        tran.Commit();
                        return ReturnType.Success;
                    }
                    else
                    {
                        return ReturnType.NotExisted;
                    }
                }
                catch (System.Exception ex)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    return ReturnType.SaveFailed;

                }
                finally
                {
                    if (alading != null && alading.Connection.State != System.Data.ConnectionState.Closed)
                    {
                        alading.Connection.Close();
                    }
                }
            }
        }
       
        public ReturnType UpdateUser(string userCode, User user)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.User.Where(p => p.UserID == userID).ToList();*/
                    var result = alading.User.Where(p => p.UserCode == userCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    User ob = result.First();
                    ob.UserCode = user.UserCode;
                    ob.nick = user.nick;
                    //ob.sex = user.sex;
                    ob.created = user.created;
                    ob.last_visit = user.last_visit;
                    ob.birthday = user.birthday;
                    ob.type = user.type;
                    //ob.has_more_pic = user.has_more_pic;
                    //ob.item_img_num = user.item_img_num;
                    //ob.item_img_size = user.item_img_size;
                    //ob.prop_img_num = user.prop_img_num;
                    //ob.prop_img_size = user.prop_img_size;
                    //ob.auto_repost = user.auto_repost;
                    //ob.promoted_type = user.promoted_type;
                    ob.status = user.status;
                    //ob.alipay_bind = user.alipay_bind;
                    //ob.consumer_protection = user.consumer_protection;
                    //ob.alipay_account = user.alipay_account;
                    //ob.alipay_no = user.alipay_no;
                    //ob.buyer_zip = user.buyer_zip;
                    //ob.buyer_address = user.buyer_address;
                    //ob.buyer_city = user.buyer_city;
                    //ob.buyer_state = user.buyer_state;
                    //ob.buyer_country = user.buyer_country;
                    //ob.buyer_district = user.buyer_district;
                    //ob.buyer_level = user.buyer_level;
                    //ob.buyer_score = user.buyer_score;
                    //ob.buyer_total_num = user.buyer_total_num;
                    //ob.buyer_good_num = user.buyer_good_num;
                    //ob.buyer_credit = user.buyer_credit;
                    //ob.seller_credit = user.seller_credit;
                    //ob.seller_zip = user.seller_zip;
                    //ob.seller_address = user.seller_address;
                    //ob.seller_city = user.seller_city;
                    //ob.seller_state = user.seller_state;
                    //ob.seller_country = user.seller_country;
                    //ob.seller_district = user.seller_district;
                    //ob.seller_level = user.seller_level;
                    //ob.seller_score = user.seller_score;
                    //ob.seller_total_num = user.seller_total_num;
                    //ob.seller_good_num = user.seller_good_num;
                    
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
     
        public List<User> GetAllUser()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<User> list = alading.User.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<User> GetUser(Func<User, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<User> list = alading.User.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public User GetUser(string userCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<User> list = alading.User.Where(p => p.UserID == userID).ToList();*/
                    List<User> list = alading.User.Where(p => p.UserCode == userCode).ToList();
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
        
        public List<User> GetUser(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.User orderby u.UserID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.User.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<User> GetUser(Func<User, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<User> list = alading.User.Where(func).OrderByDescending(a=>a.UserID);
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

