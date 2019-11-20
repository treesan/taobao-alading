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
     
        public ReturnType AddSellerCat(SellerCat sellercat)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    SellerCat result = alading.SellerCat.Where(p => p.SellerNick == sellercat.SellerNick && p.cid == sellercat.cid).FirstOrDefault();
                    if (result == null)
                    {
                        alading.AddToSellerCat(sellercat);
                    }
                    else
                    {
                        result.parent_cid = sellercat.parent_cid;

                        result.name = sellercat.name;

                        result.pic_url = sellercat.pic_url;

                        result.sort_order = sellercat.sort_order;

                        result.num = sellercat.num;
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
                
        public ReturnType AddSellerCat(List<SellerCat> sellercatList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (SellerCat sellercat in sellercatList)
                    {
                        alading.AddToSellerCat(sellercat);
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
       
        public ReturnType RemoveAllSellerCat()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<SellerCat> list = alading.SellerCat.ToList();
                    foreach (SellerCat sellercat in list)
                    {
                        alading.DeleteObject(sellercat);
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
       
        public ReturnType RemoveSellerCat(Func<SellerCat, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<SellerCat> list = alading.SellerCat.Where(func).ToList();
                    foreach (SellerCat sellercat in list)
                    {
                        alading.DeleteObject(sellercat);
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

        public List<SellerCat> GetSellerCatByCid(string[] SellerCatArray)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<SellerCat> ListSellerCat = new List<SellerCat>();
                    foreach(string sellerCat in SellerCatArray)
                    {
                        if (!string.IsNullOrEmpty(sellerCat))
                        {
                            ListSellerCat.Add(alading.SellerCat.FirstOrDefault(p => p.cid == sellerCat));
                        }
                    }
                    return ListSellerCat;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<SellerCat> GetSellerCat(List<string> sellercatCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.SellerCat.Where(BuildWhereInExpression<SellerCat, int>(v => v.SellerCatID, sellercatIDList));*/
                    var result = alading.SellerCat.Where(BuildWhereInExpression<SellerCat, string>(v => v.SellerCatCode, sellercatCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveSellerCat(List<string> sellercatCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.SellerCat.Where(BuildWhereInExpression<SellerCat, int>(v => v.SellerCatID, sellercatIDList));*/
                    var result = alading.SellerCat.Where(BuildWhereInExpression<SellerCat, string>(v => v.SellerCatCode, sellercatCodeList));
                    foreach (SellerCat s in result)
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
    
        public ReturnType RemoveSellerCat(string sellercatCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<SellerCat> list = alading.SellerCat.Where(p => p.SellerCatID == sellercatID).ToList();*/
                    List<SellerCat> list = alading.SellerCat.Where(p => p.SellerCatCode == sellercatCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        SellerCat sy = list.First();
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
      
        public ReturnType UpdateSellerCat(SellerCat sellercat)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    SellerCat result = alading.SellerCat.Where(p => p.SellerNick == sellercat.SellerNick && p.cid == sellercat.cid).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }

                    result.parent_cid = sellercat.parent_cid;

                    result.name = sellercat.name;

                    result.pic_url = sellercat.pic_url;

                    result.sort_order = sellercat.sort_order;

                    result.num = sellercat.num;
                    
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
       
        public ReturnType UpdateSellerCat(string sellercatCode, SellerCat sellercat)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.SellerCat.Where(p => p.SellerCatID == sellercatID).ToList();*/
                    var result = alading.SellerCat.Where(p => p.SellerCatCode == sellercatCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    SellerCat ob = result.First();
                    ob.cid = sellercat.cid;
                    ob.parent_cid = sellercat.parent_cid;
                    ob.name = sellercat.name;
                    ob.pic_url = sellercat.pic_url;
                    ob.sort_order = sellercat.sort_order;
                    
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
     
        public List<SellerCat> GetAllSellerCat()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<SellerCat> list = alading.SellerCat.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<SellerCat> GetSellerCat(Func<SellerCat, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<SellerCat> list = alading.SellerCat.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public List<SellerCat> GetSellerCatOrdered(string sellerNick)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<SellerCat> list = alading.SellerCat.Where(p => p.SellerNick == sellerNick).OrderBy(p=>p.parent_cid).OrderBy(p=>p.sort_order).ToList();
                    if (list.Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return list;
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<SellerCat> GetSellerCat(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.SellerCat orderby u.SellerCatID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.SellerCat.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<SellerCat> GetSellerCat(Func<SellerCat, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<SellerCat> list = alading.SellerCat.Where(func).OrderByDescending(a=>a.SellerCatID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType IsSellercatExisted(string nick,string cid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    SellerCat result = alading.SellerCat.Where(p => p.SellerNick==nick&&p.cid==cid).FirstOrDefault();
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

