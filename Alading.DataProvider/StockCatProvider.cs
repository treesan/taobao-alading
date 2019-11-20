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
     
        public ReturnType AddStockCat(StockCat stockcat)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    alading.AddToStockCat(stockcat);
                    if (stockcat.ParentCid != "0")
                    {
                        StockCat result = alading.StockCat.FirstOrDefault(i => i.StockCid == stockcat.ParentCid);
                        if (result != null)
                        {
                            result.IsParent = true;
                        }
                    }
                    alading.SaveChanges();                   
                    return ReturnType.Success; 
                }
            }           
            catch (Exception ex)
            {
                throw ex;
            }
        }
                
        public ReturnType AddStockCat(List<StockCat> stockcatList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (StockCat stockcat in stockcatList)
                    {
                        alading.AddToStockCat(stockcat);
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
       
        public ReturnType RemoveAllStockCat()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockCat> list = alading.StockCat.ToList();
                    foreach (StockCat stockcat in list)
                    {
                        alading.DeleteObject(stockcat);
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
       
        public ReturnType RemoveStockCat(Func<StockCat, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockCat> list = alading.StockCat.Where(func).ToList();
                    foreach (StockCat stockcat in list)
                    {
                        alading.DeleteObject(stockcat);
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

        public List<StockCat> GetStockCat(List<string> stockcatCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.StockCat.Where(BuildWhereInExpression<StockCat, int>(v => v.StockCatID, stockcatIDList));*/
                    var result = alading.StockCat.Where(BuildWhereInExpression<StockCat, string>(v => v.StockCid, stockcatCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveStockCat(List<string> stockcatCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.StockCat.Where(BuildWhereInExpression<StockCat, int>(v => v.StockCatID, stockcatIDList));*/
                    var result = alading.StockCat.Where(BuildWhereInExpression<StockCat, string>(v => v.StockCid, stockcatCodeList));
                    foreach (StockCat s in result)
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


        public ReturnType RemoveStockCat(string stockCid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    StockCat result = alading.StockCat.FirstOrDefault(p => p.StockCid == stockCid);
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    else
                    {
                        if (result.ParentCid != "0")
                        {
                            //查找此类目的父类目是否有其他子类目,没有则父类目IsParent为false
                            List<StockCat> catList = alading.StockCat.Where(i => i.ParentCid == result.ParentCid).ToList();
                            //只有一个子类目即本stockCid
                            if (catList.Count == 1)
                            {
                                StockCat parentCat = alading.StockCat.FirstOrDefault(i => i.StockCid == result.ParentCid);
                                if (parentCat != null)
                                {
                                    parentCat.IsParent = false;
                                }
                            }
                        }
                    }
                    alading.DeleteObject(result);
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

        public ReturnType UpdateStockCat(string stockcatCode, string catName)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    StockCat result = alading.StockCat.Where(p => p.StockCid == stockcatCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }

                    result.StockCatName = catName;

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

        public ReturnType UpdateStockCat(StockCat stockcat)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*StockCat result = alading.StockCat.Where(p => p.StockCatID == stockcat.StockCatID).FirstOrDefault();*/
                    StockCat result = alading.StockCat.Where(p => p.StockCid == stockcat.StockCid).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }                  

                    //result.StockCid = stockcat.StockCid;

                    result.ParentCid = stockcat.ParentCid;

                    result.StockCatName = stockcat.StockCatName;

                    result.IsParent = stockcat.IsParent;  

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
       
        public ReturnType UpdateStockCat(string stockcatCode, StockCat stockcat)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.StockCat.Where(p => p.StockCatID == stockcatID).ToList();*/
                    var result = alading.StockCat.Where(p => p.StockCid == stockcatCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    StockCat ob = result.First();
                    ob.StockCid = stockcat.StockCid;
                    ob.ParentCid = stockcat.ParentCid;
                    ob.StockCatName = stockcat.StockCatName;
                    ob.IsParent = stockcat.IsParent;
                    
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
     
        public List<StockCat> GetAllStockCat()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockCat> list = alading.StockCat.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<StockCat> GetStockCat(Func<StockCat, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockCat> list = alading.StockCat.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public StockCat GetStockCat(string stockcatCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<StockCat> list = alading.StockCat.Where(p => p.StockCatID == stockcatID).ToList();*/
                    List<StockCat> list = alading.StockCat.Where(p => p.StockCid == stockcatCode).ToList();
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
        
        public List<StockCat> GetStockCat(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.StockCat orderby u.StockCatID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.StockCat.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<StockCat> GetStockCat(Func<StockCat, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<StockCat> list = alading.StockCat.Where(func).OrderByDescending(a=>a.StockCatID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType DeleteStockCat(string cid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    StockCat stockCat = alading.StockCat.FirstOrDefault(c => c.StockCid == cid);
                    if (stockCat != null)
                    {
                        if (stockCat.IsParent)
                        {
                            return ReturnType.Conflicted;
                        }
                        IEnumerable<StockProp> propList = alading.StockProp.Where(c => c.StockCid == cid);
                        IEnumerable<StockPropValue> valueList = alading.StockPropValue.Where(c => c.StockCid == cid);
                        foreach (StockProp stockProp in propList)
                        {
                            alading.DeleteObject(stockProp);
                        }
                        foreach (StockPropValue value in valueList)
                        {
                            alading.DeleteObject(value);
                        }
                        List<StockCat> catList = alading.StockCat.Where(c => c.ParentCid == stockCat.ParentCid).ToList();
                        if (catList.Count == 1)
                        {
                            StockCat fatherStockCat = alading.StockCat.FirstOrDefault(c => c.StockCid == stockCat.ParentCid);
                            fatherStockCat.IsParent = false;
                            alading.SaveChanges();
                        }
                        alading.DeleteObject(stockCat);
                        alading.SaveChanges();
                        return ReturnType.Success;
                    }
                    else
                    {
                        return ReturnType.NotExisted;
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}

