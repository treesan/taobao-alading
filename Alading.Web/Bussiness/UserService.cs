using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alading.Web.Entity;

namespace Alading.Web.Bussiness
{
    public class UserService
    {
        public static void AddUser(Alading.Web.Entity.User user)
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                if (user.Account.Contains(":"))
                {
                    string mainAccount = user.Account.Substring(0, user.Account.IndexOf(":"));
                    var x = context.User.Where(p => p.Account == mainAccount).FirstOrDefault();
                    x.HasUser++;
                }
                context.AddToUser(user);
                context.SaveChanges();
            }
        }

        public static List<User> GetUser(Func<User, bool> func)
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                List<User> list = context.User.Where(func).ToList();
                return list;
            }
        }

        public static User GetUser(string userCode)
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                var user = context.User.Where(p => p.UserCode == userCode).FirstOrDefault();
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public static User GetUser(string account, string psw)
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                var user = context.User.Where(p => p.Account == account&&p.Password==psw).FirstOrDefault();
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public static User GetUserByAccount(string account)
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                var user = context.User.Where(p => p.Account == account).FirstOrDefault();
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public static void RemoveUser(string userCode)
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                var user = context.User.FirstOrDefault(p => p.UserCode == userCode);
                if (user != null)
                {
                    if (user.Account.Contains(":"))
                    {
                        string mainAccount = user.Account.Substring(0, user.Account.IndexOf(":"));
                        var x = context.User.Where(p => p.Account == mainAccount).FirstOrDefault();
                        x.HasUser--;
                    }
                    context.DeleteObject(user);
                    context.SaveChanges();
                }
            }
        }

        public static void UpdateUser(User user)
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                var old = context.User.FirstOrDefault(p => p.UserCode == user.UserCode);
                if (old != null)
                {
                    context.Attach(old);
                    context.ApplyPropertyChanges("User", user);
                    context.SaveChanges();
                }
            }
        }

        public static List<User> GetUsers(string account) //根据主号找子号
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                var result = context.User.Where(p => p.Account.StartsWith(account + ":"));
                if (result.Count() > 0)
                {
                    return result.ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        public static bool ChecMainkUser(string account, string pwd)
        {
            return (!account.Contains(":") && GetUser(p => p.Account == account && p.Password == pwd).FirstOrDefault() != null);              
        }

        public static void UpdateFirstRun(string account, string pwd)
        {
            User u = GetUserByAccount(account);
            if (u != null && u.Password ==pwd)
            {
                u.FirstRun = false;
                UpdateUser(u);
            }
        }
    }
}
