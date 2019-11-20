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
     
        public ReturnType AddStockProp(StockProp stockprop)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToStockProp(stockprop);
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
                
        public ReturnType AddStockProp(List<StockProp> stockpropList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (StockProp stockprop in stockpropList)
                    {
                        alading.AddToStockProp(stockprop);
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
       
        public ReturnType RemoveAllStockProp()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockProp> list = alading.StockProp.ToList();
                    foreach (StockProp stockprop in list)
                    {
                        alading.DeleteObject(stockprop);
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
       
        public ReturnType RemoveStockProp(Func<StockProp, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockProp> list = alading.StockProp.Where(func).ToList();
                    foreach (StockProp stockprop in list)
                    {
                        alading.DeleteObject(stockprop);
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

        public List<StockProp> GetStockProp(List<string> stockpropCodeList)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*var result = alading.StockProp.Where(BuildWhereInExpression<StockProp, int>(v => v.StockPropID, stockpropIDList));*/
            //        var result = alading.StockProp.Where(BuildWhereInExpression<StockProp, string>(v => v.StockPropCode, stockpropCodeList));

            //        return result.ToList();
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    return null;
            //}
        }

        public ReturnType RemoveStockProp(List<string> stockpropCodeList)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*var result = alading.StockProp.Where(BuildWhereInExpression<StockProp, int>(v => v.StockPropID, stockpropIDList));*/
            //        var result = alading.StockProp.Where(BuildWhereInExpression<StockProp, string>(v => v.StockPropCode, stockpropCodeList));
            //        foreach (StockProp s in result)
            //        {
            //            alading.DeleteObject(s);
            //        }
            //        alading.SaveChanges();
            //        return ReturnType.Success;
            //    }
            //}
            //catch (SqlException sex)
            //{
            //    return ReturnType.ConnFailed;
            //}
            //catch (System.Exception ex)
            //{
            //    return ReturnType.OthersError;
            //}
        }

    
        public ReturnType RemoveStockProp(string stockpropCode)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*List<StockProp> list = alading.StockProp.Where(p => p.StockPropID == stockpropID).ToList();*/
            //        List<StockProp> list = alading.StockProp.Where(p => p.StockPropCode == stockpropCode).ToList();
            //        if (list.Count == 0)
            //        {
            //            return ReturnType.NotExisted;
            //        }

            //        else
            //        {
            //            StockProp sy = list.First();
            //            alading.DeleteObject(sy);
            //            alading.SaveChanges();
            //            return ReturnType.Success;
            //        }
            //    }
            //}
            //catch (SqlException sex)
            //{
            //    return ReturnType.ConnFailed;
            //}
            //catch (System.Exception ex)
            //{
            //    return ReturnType.OthersError;
            //}
        }
      
        public ReturnType UpdateStockProp(StockProp stockprop)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    StockProp sp = alading.StockProp.FirstOrDefault(s => s.StockPid == stockprop.StockPid);
                    sp.Name = stockprop.Name;
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*StockProp result = alading.StockProp.Where(p => p.StockPropID == stockprop.StockPropID).FirstOrDefault();*/
            //        StockProp result = alading.StockProp.Where(p => p.StockPropCode == stockprop.StockPropCode).FirstOrDefault();
            //        if (result == null)
            //        {
            //            return ReturnType.NotExisted;
            //        }
            //        #region   Using Attach() Function Update,Default USE;          
            //        alading.Attach(result);
            //        alading.ApplyPropertyChanges("StockProp", stockprop);
            //        #endregion
                    
            //        #region    Using All Items Replace To Update ,Default UnUse
            //        /*		
                    
            //            result.StockCid = stockprop.StockCid;
                    
            //            result.StockPid = stockprop.StockPid;
                    
            //            result.ParentPid = stockprop.ParentPid;
                    
            //            result.ParentVid = stockprop.ParentVid;
                    
            //            result.Name = stockprop.Name;
                    
            //            result.PropValues = stockprop.PropValues;
                    
            //            result.Status = stockprop.Status;
			
            //        */
            //        #endregion  
            //        if (alading.SaveChanges() == 1)
            //        {
            //            return ReturnType.Success;
            //        }
            //    }
            //}
            //catch (SqlException sex)
            //{
            //    return ReturnType.ConnFailed;
            //}
            //catch (Exception ex)
            //{
            //    return ReturnType.OthersError;
            //}
        }
       
        public ReturnType UpdateStockProp(string stockpropCode, StockProp stockprop)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*var result = alading.StockProp.Where(p => p.StockPropID == stockpropID).ToList();*/
            //        var result = alading.StockProp.Where(p => p.StockPropCode == stockpropCode).ToList();
            //        if (result.Count == 0)
            //        {
            //            return ReturnType.NotExisted;
            //        }
                  
            //        StockProp ob = result.First();
            //        ob.StockCid = stockprop.StockCid;
            //        ob.StockPid = stockprop.StockPid;
            //        ob.ParentPid = stockprop.ParentPid;
            //        ob.ParentVid = stockprop.ParentVid;
            //        ob.Name = stockprop.Name;
            //        ob.PropValues = stockprop.PropValues;
            //        ob.Status = stockprop.Status;
                    
            //        if (alading.SaveChanges() == 1)
            //        {
            //            return ReturnType.Success;
            //        }  
            //        else
            //        {
            //            return ReturnType.OthersError;
            //        }
            //    }
            //}
            //catch (SqlException sex)
            //{
            //    return ReturnType.ConnFailed;
            //}
            //catch (System.Exception ex)
            //{
            //    return ReturnType.OthersError;
            //}
        }
     
        public List<StockProp> GetAllStockProp()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockProp> list = alading.StockProp.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<StockProp> GetStockProp(Func<StockProp, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockProp> list = alading.StockProp.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public StockProp GetStockProp(string stockpropCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<StockProp> list = alading.StockProp.Where(p => p.StockPropID == stockpropID).ToList();*/
                    List<StockProp> list = alading.StockProp.Where(p => p.StockPid == stockpropCode).ToList();
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
        
        public List<StockProp> GetStockProp(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.StockProp orderby u.StockPropID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.StockProp.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<StockProp> GetStockProp(Func<StockProp, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<StockProp> list = alading.StockProp.Where(func).OrderByDescending(a=>a.StockPropID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType DeleteStockPropAndValue(StockProp stockProp)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    StockProp result = alading.StockProp.FirstOrDefault(c => c.StockPid == stockProp.StockPid);
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    List<StockPropValue> propValueList = alading.StockPropValue.Where(c => c.StockPid == result.StockPid).ToList();
                    if (propValueList != null && propValueList.Count>0)
                    {
                        foreach (StockPropValue stockPropValue in propValueList)
                        {
                            if (stockPropValue.IsParent)
                            {
                                List<StockProp> list = alading.StockProp.Where(c => c.ParentPid == stockPropValue.StockPid && c.ParentVid == stockPropValue.StockVid).ToList();
                                if (list != null && list.Count > 0)
                                {
                                    foreach (StockProp prop in list)
                                    {
                                        List<StockPropValue> temp = alading.StockPropValue.Where(c => c.StockPid == prop.StockPid).ToList();
                                        if (temp != null && temp.Count > 0)
                                        {
                                            foreach (StockPropValue t in temp)
                                            {
                                                alading.DeleteObject(t);
                                            }
                                        }
                                        alading.DeleteObject(prop);
                                    }
                                }
                            }
                            alading.DeleteObject(stockPropValue);
                        }
                    }
                    StockPropValue fatherPropValue = alading.StockPropValue.FirstOrDefault(c => c.StockCid == result.StockCid && c.StockPid == result.ParentPid && c.StockVid == result.ParentVid);
                    if (fatherPropValue!=null && alading.StockProp.Where(c => c.StockCid == fatherPropValue.StockCid && c.ParentPid == fatherPropValue.StockPid && c.ParentVid == fatherPropValue.StockVid).Count() == 1)
                    {
                        fatherPropValue.IsParent = false;
                    }
                    alading.DeleteObject(result);
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}

