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
     
        public ReturnType AddItemSellerAuthorize(ItemSellerAuthorize itemsellerauthorize)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    ItemSellerAuthorize result = alading.ItemSellerAuthorize.Where(p => p.SellerNick == itemsellerauthorize.SellerNick && p.Cid == itemsellerauthorize.Cid).FirstOrDefault();
                    if (result == null)
                    {
                        alading.AddToItemSellerAuthorize(itemsellerauthorize);
                    }
                    else
                    {
                        result.Name = itemsellerauthorize.Name;
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
                
        public ReturnType AddItemSellerAuthorize(List<ItemSellerAuthorize> itemsellerauthorizeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (ItemSellerAuthorize itemsellerauthorize in itemsellerauthorizeList)
                    {
                        alading.AddToItemSellerAuthorize(itemsellerauthorize);
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
       
        public ReturnType RemoveAllItemSellerAuthorize()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ItemSellerAuthorize> list = alading.ItemSellerAuthorize.ToList();
                    foreach (ItemSellerAuthorize itemsellerauthorize in list)
                    {
                        alading.DeleteObject(itemsellerauthorize);
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
       
        public ReturnType RemoveItemSellerAuthorize(Func<ItemSellerAuthorize, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ItemSellerAuthorize> list = alading.ItemSellerAuthorize.Where(func).ToList();
                    foreach (ItemSellerAuthorize itemsellerauthorize in list)
                    {
                        alading.DeleteObject(itemsellerauthorize);
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

        public List<ItemSellerAuthorize> GetItemSellerAuthorize(List<string> itemsellerauthorizeCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.ItemSellerAuthorize.Where(BuildWhereInExpression<ItemSellerAuthorize, int>(v => v.ItemSellerAuthorizeID, itemsellerauthorizeIDList));*/
                    var result = alading.ItemSellerAuthorize.Where(BuildWhereInExpression<ItemSellerAuthorize, string>(v => v.ItemSellerAuthorizeCode, itemsellerauthorizeCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveItemSellerAuthorize(List<string> itemsellerauthorizeCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.ItemSellerAuthorize.Where(BuildWhereInExpression<ItemSellerAuthorize, int>(v => v.ItemSellerAuthorizeID, itemsellerauthorizeIDList));*/
                    var result = alading.ItemSellerAuthorize.Where(BuildWhereInExpression<ItemSellerAuthorize, string>(v => v.ItemSellerAuthorizeCode, itemsellerauthorizeCodeList));
                    foreach (ItemSellerAuthorize s in result)
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

    
        public ReturnType RemoveItemSellerAuthorize(string itemsellerauthorizeCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<ItemSellerAuthorize> list = alading.ItemSellerAuthorize.Where(p => p.ItemSellerAuthorizeID == itemsellerauthorizeID).ToList();*/
                    List<ItemSellerAuthorize> list = alading.ItemSellerAuthorize.Where(p => p.ItemSellerAuthorizeCode == itemsellerauthorizeCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        ItemSellerAuthorize sy = list.First();
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
      
        public ReturnType UpdateItemSellerAuthorize(ItemSellerAuthorize itemsellerauthorize)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    ItemSellerAuthorize result = alading.ItemSellerAuthorize.Where(p => p.SellerNick == itemsellerauthorize.SellerNick&&p.Cid==itemsellerauthorize.Cid).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }

                   result.Name = itemsellerauthorize.Name;
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
       
        public ReturnType UpdateItemSellerAuthorize(string itemsellerauthorizeCode, ItemSellerAuthorize itemsellerauthorize)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.ItemSellerAuthorize.Where(p => p.ItemSellerAuthorizeID == itemsellerauthorizeID).ToList();*/
                    var result = alading.ItemSellerAuthorize.Where(p => p.ItemSellerAuthorizeCode == itemsellerauthorizeCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                    ItemSellerAuthorize ob = result.First();
                    ob.ItemSellerAuthorizeCode = itemsellerauthorize.ItemSellerAuthorizeCode;
                    ob.Cid = itemsellerauthorize.Cid;
                    ob.Name = itemsellerauthorize.Name;
                    ob.SellerNick = itemsellerauthorize.SellerNick;
                    
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
     
        public List<ItemSellerAuthorize> GetAllItemSellerAuthorize()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ItemSellerAuthorize> list = alading.ItemSellerAuthorize.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<ItemSellerAuthorize> GetItemSellerAuthorize(Func<ItemSellerAuthorize, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<ItemSellerAuthorize> list = alading.ItemSellerAuthorize.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public ItemSellerAuthorize GetItemSellerAuthorize(string itemsellerauthorizeCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<ItemSellerAuthorize> list = alading.ItemSellerAuthorize.Where(p => p.ItemSellerAuthorizeID == itemsellerauthorizeID).ToList();*/
                    List<ItemSellerAuthorize> list = alading.ItemSellerAuthorize.Where(p => p.ItemSellerAuthorizeCode == itemsellerauthorizeCode).ToList();
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
        
        public List<ItemSellerAuthorize> GetItemSellerAuthorize(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.ItemSellerAuthorize orderby u.ItemSellerAuthorizeID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.ItemSellerAuthorize.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<ItemSellerAuthorize> GetItemSellerAuthorize(Func<ItemSellerAuthorize, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<ItemSellerAuthorize> list = alading.ItemSellerAuthorize.Where(func).OrderByDescending(a=>a.ItemSellerAuthorizeID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType IsAuthorizeExisted(string nick, string cid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    ItemSellerAuthorize result = alading.ItemSellerAuthorize.Where(a => a.SellerNick == nick && a.Cid == cid).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
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
    }
}

