using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alading.Web.Entity;

namespace Alading.Web.Bussiness
{
    public class ShopService
    {
        public static void AddShop(Shop shop)
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                var user = context.User.FirstOrDefault(c => c.UserCode == shop.UserCode);
                if (user == null) throw new ServiceException("用户不存在！");
                if (user.HasShop > user.MaxShop) throw new ServiceException("已有店铺数量不大于允许的最大店铺数量！");
                user.HasShop++;
                context.AddToShop(shop);
                context.SaveChanges();
            }
        }

        public static void UpdateShop(Shop shop)
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                var old = context.Shop.FirstOrDefault(c => c.ShopCode == shop.ShopCode);
                if (old != null)
                {
                    context.Attach(old);
                    context.ApplyPropertyChanges("Shop", shop);
                    context.SaveChanges();
                }
            }
        }

        public static void RemoveShop(string shopCode)
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                var old = context.Shop.FirstOrDefault(c => c.ShopCode == shopCode);
                if (old == null) throw new ServiceException("店铺不存在！");
                var user = context.User.FirstOrDefault(c => c.UserCode == old.UserCode);
                if (user == null) throw new ServiceException("用户不存在！");
                user.HasShop--;
                context.DeleteObject(old);
                context.SaveChanges();
            }
        }

        public static Shop GetShop(string shopCode)
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                return context.Shop.FirstOrDefault(c => c.ShopCode == shopCode);
            }
        }

        public static List<Shop> GetShopList(string userCode)
        {
            using (AladingWebEntities context = ContextProvider.DataContext(ConnectionHelper.ConnectionString))
            {
                return context.Shop.Where(c => c.UserCode == userCode).ToList();
            }
        }
    }
}
