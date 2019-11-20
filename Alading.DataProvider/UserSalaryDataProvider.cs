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

            public ReturnType AddUserSalary(UserSalary usersalary)
            {
                try
                {
                    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                    {

                        alading.AddToUserSalary(usersalary);
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

            public ReturnType AddUserSalary(List<UserSalary> usersalaryList)
            {
                try
                {
                    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                    {
                        foreach (UserSalary usersalary in usersalaryList)
                        {
                            alading.AddToUserSalary(usersalary);
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

            public ReturnType RemoveAllUserSalary()
            {
                try
                {
                    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                    {
                        List<UserSalary> list = alading.UserSalary.ToList();
                        foreach (UserSalary usersalary in list)
                        {
                            alading.DeleteObject(usersalary);
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

            public ReturnType RemoveUserSalary(Func<UserSalary, bool> func)
            {
                try
                {
                    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                    {
                        List<UserSalary> list = alading.UserSalary.Where(func).ToList();
                        foreach (UserSalary usersalary in list)
                        {
                            alading.DeleteObject(usersalary);
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

            public List<UserSalary> GetUserSalary(List<string> usersalaryCodeList)
            {
                try
                {
                    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                    {
                        /*var result = alading.UserSalary.Where(BuildWhereInExpression<UserSalary, int>(v => v.UserSalaryID, usersalaryIDList));*/
                        var result = alading.UserSalary.Where(BuildWhereInExpression<UserSalary, string>(v => v.UserSalaryCode, usersalaryCodeList));
                        return result.ToList();
                    }
                }
                catch (System.Exception ex)
                {
                    return null;
                }
            }

            public ReturnType RemoveUserSalary(List<string> usersalaryCodeList)
            {
                try
                {
                    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                    {
                        /*var result = alading.UserSalary.Where(BuildWhereInExpression<UserSalary, int>(v => v.UserSalaryID, usersalaryIDList));*/
                        var result = alading.UserSalary.Where(BuildWhereInExpression<UserSalary, string>(v => v.UserSalaryCode, usersalaryCodeList));
                        foreach (UserSalary s in result)
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


            public ReturnType RemoveUserSalary(string usersalaryCode)
            {
                try
                {
                    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                    {
                        /*List<UserSalary> list = alading.UserSalary.Where(p => p.UserSalaryID == usersalaryID).ToList();*/
                        List<UserSalary> list = alading.UserSalary.Where(p => p.UserSalaryCode == usersalaryCode).ToList();
                        if (list.Count == 0)
                        {
                            return ReturnType.NotExisted;
                        }
                        else
                        {
                            UserSalary sy = list.First();
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

            public ReturnType UpdateUserSalary(UserSalary usersalary)
            {
                try
                {
                    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                    {
                        /*UserSalary result = alading.UserSalary.Where(p => p.UserSalaryID == usersalary.UserSalaryID).FirstOrDefault();*/
                        UserSalary result = alading.UserSalary.Where(p => p.UserSalaryCode == usersalary.UserSalaryCode).FirstOrDefault();
                        if (result == null)
                        {
                            return ReturnType.NotExisted;
                        }
                        #region   Using Attach() Function Update,Default USE;          
                        alading.Attach(result);
                        alading.ApplyPropertyChanges("UserSalary", usersalary);
                        #endregion

                        #region    Using All Items Replace To Update ,Default UnUse
                        /*		
                            result.UserSalaryCode = usersalary.UserSalaryCode;
                            result.nick = usersalary.nick;
                            result.UserCode = usersalary.UserCode;
                            result.Seller_nick = usersalary.Seller_nick;
                            result.Buyer_nick = usersalary.Buyer_nick;
                            result.iid = usersalary.iid;
                            result.Price = usersalary.Price;
                            result.Num = usersalary.Num;
                            result.TotlePrice = usersalary.TotlePrice;
                            result.Discount_fee = usersalary.Discount_fee;
                            result.Adjust_fee = usersalary.Adjust_fee;
                            result.Post_fee = usersalary.Post_fee;
                            result.Payment = usersalary.Payment;
                            result.Salary = usersalary.Salary;
                            result.TradeDate = usersalary.TradeDate;

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

            public ReturnType UpdateUserSalary(string usersalaryCode, UserSalary usersalary)
            {
                try
                {
                    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                    {
                        /*var result = alading.UserSalary.Where(p => p.UserSalaryID == usersalaryID).ToList();*/
                        var result = alading.UserSalary.Where(p => p.UserSalaryCode == usersalaryCode).ToList();
                        if (result.Count == 0)
                        {
                            return ReturnType.NotExisted;
                        }

                        UserSalary ob = result.First();
                        ob.UserSalaryCode = usersalary.UserSalaryCode;
                        ob.nick = usersalary.nick;
                        ob.UserCode = usersalary.UserCode;
                        ob.Seller_nick = usersalary.Seller_nick;
                        ob.Buyer_nick = usersalary.Buyer_nick;
                        ob.iid = usersalary.iid;
                        ob.Price = usersalary.Price;
                        ob.Num = usersalary.Num;
                        ob.TotlePrice = usersalary.TotlePrice;
                        ob.Discount_fee = usersalary.Discount_fee;
                        ob.Adjust_fee = usersalary.Adjust_fee;
                        ob.Post_fee = usersalary.Post_fee;
                        ob.Payment = usersalary.Payment;
                        ob.Salary = usersalary.Salary;
                        ob.TradeDate = usersalary.TradeDate;

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

            public List<UserSalary> GetAllUserSalary()
            {
                try
                {
                    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                    {
                        List<UserSalary> list = alading.UserSalary.ToList();
                        return list;
                    }
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }

            public List<UserSalary> GetUserSalary(Func<UserSalary, bool> func)
            {
                try
                {
                    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                    {
                        List<UserSalary> list = alading.UserSalary.Where(func).ToList();
                        return list;
                    }

                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }

            public UserSalary GetUserSalary(string usersalaryCode)
            {
                try
                {
                    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                    {
                        /*List<UserSalary> list = alading.UserSalary.Where(p => p.UserSalaryID == usersalaryID).ToList();*/
                        List<UserSalary> list = alading.UserSalary.Where(p => p.UserSalaryCode == usersalaryCode).ToList();
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

            public List<UserSalary> GetUserSalary(int pageIndex, int pageSize, out int rowCount)
            {
                try
                {
                    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                    {                    
                        var ob = (from u in alading.UserSalary orderby u.UserSalaryID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                        rowCount = alading.UserSalary.Count();
                        return ob.ToList();
                    }
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }

            public List<UserSalary> GetUserSalary(Func<UserSalary, bool> func, int pageIndex, int pageSize, out int rowCount)
            {
                try
                {
                    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                    {
                        IOrderedEnumerable<UserSalary> list = alading.UserSalary.Where(func).OrderByDescending(a=>a.UserSalaryID);
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


