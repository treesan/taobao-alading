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
     
        public ReturnType AddStockPropValue(StockPropValue stockpropvalue)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToStockPropValue(stockpropvalue);
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
                
        public ReturnType AddStockPropValue(List<StockPropValue> stockpropvalueList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (StockPropValue stockpropvalue in stockpropvalueList)
                    {
                        alading.AddToStockPropValue(stockpropvalue);
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
       
        public ReturnType RemoveAllStockPropValue()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockPropValue> list = alading.StockPropValue.ToList();
                    foreach (StockPropValue stockpropvalue in list)
                    {
                        alading.DeleteObject(stockpropvalue);
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
       
        public ReturnType RemoveStockPropValue(Func<StockPropValue, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockPropValue> list = alading.StockPropValue.Where(func).ToList();
                    foreach (StockPropValue stockpropvalue in list)
                    {
                        alading.DeleteObject(stockpropvalue);
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

        public List<StockPropValue> GetStockPropValue(List<string> stockpropvalueCodeList)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*var result = alading.StockPropValue.Where(BuildWhereInExpression<StockPropValue, int>(v => v.StockPropValueID, stockpropvalueIDList));*/
            //        var result = alading.StockPropValue.Where(BuildWhereInExpression<StockPropValue, string>(v => v.StockPropValueCode, stockpropvalueCodeList));

            //        return result.ToList();
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    return null;
            //}
        }

        public ReturnType RemoveStockPropValue(List<string> stockpropvalueCodeList)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*var result = alading.StockPropValue.Where(BuildWhereInExpression<StockPropValue, int>(v => v.StockPropValueID, stockpropvalueIDList));*/
            //        var result = alading.StockPropValue.Where(BuildWhereInExpression<StockPropValue, string>(v => v.StockPropValueCode, stockpropvalueCodeList));
            //        foreach (StockPropValue s in result)
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


        public ReturnType RemoveStockPropValue(StockPropValue propvalue)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    StockPropValue stockPropValue = alading.StockPropValue.FirstOrDefault(p => p.StockPid == propvalue.StockPid && p.StockVid == propvalue.StockVid);
                    if (stockPropValue == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    else
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
                        alading.DeleteObject(stockPropValue);
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
      
        public ReturnType UpdateStockPropValue(StockPropValue stockpropvalue)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*StockPropValue result = alading.StockPropValue.Where(p => p.StockPropValueID == stockpropvalue.StockPropValueID).FirstOrDefault();*/
                    StockPropValue result = alading.StockPropValue.Where(p => p.StockPid==stockpropvalue.StockPid && p.StockVid == stockpropvalue.StockVid).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    result.Name = stockpropvalue.Name;
                    result.IsParent = stockpropvalue.IsParent;
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
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*StockPropValue result = alading.StockPropValue.Where(p => p.StockPropValueID == stockpropvalue.StockPropValueID).FirstOrDefault();*/
            //        StockPropValue result = alading.StockPropValue.Where(p => p.StockPropValueCode == stockpropvalue.StockPropValueCode).FirstOrDefault();
            //        if (result == null)
            //        {
            //            return ReturnType.NotExisted;
            //        }
            //        #region   Using Attach() Function Update,Default USE;          
            //        alading.Attach(result);
            //        alading.ApplyPropertyChanges("StockPropValue", stockpropvalue);
            //        #endregion
                    
            //        #region    Using All Items Replace To Update ,Default UnUse
            //        /*		
                    
            //            result.StockCid = stockpropvalue.StockCid;
                    
            //            result.StockPid = stockpropvalue.StockPid;
                    
            //            result.PropName = stockpropvalue.PropName;
                    
            //            result.StockVid = stockpropvalue.StockVid;
                    
            //            result.Name = stockpropvalue.Name;
                    
            //            result.IsParent = stockpropvalue.IsParent;
                    
            //            result.Status = stockpropvalue.Status;
                    
            //            result.SortOrder = stockpropvalue.SortOrder;
			
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
       
        public ReturnType UpdateStockPropValue(string stockpropvalueCode, StockPropValue stockpropvalue)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*var result = alading.StockPropValue.Where(p => p.StockPropValueID == stockpropvalueID).ToList();*/
            //        var result = alading.StockPropValue.Where(p => p.StockPropValueCode == stockpropvalueCode).ToList();
            //        if (result.Count == 0)
            //        {
            //            return ReturnType.NotExisted;
            //        }
                  
            //        StockPropValue ob = result.First();
            //        ob.StockCid = stockpropvalue.StockCid;
            //        ob.StockPid = stockpropvalue.StockPid;
            //        ob.PropName = stockpropvalue.PropName;
            //        ob.StockVid = stockpropvalue.StockVid;
            //        ob.Name = stockpropvalue.Name;
            //        ob.IsParent = stockpropvalue.IsParent;
            //        ob.Status = stockpropvalue.Status;
            //        ob.SortOrder = stockpropvalue.SortOrder;
                    
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
     
        public List<StockPropValue> GetAllStockPropValue()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockPropValue> list = alading.StockPropValue.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<StockPropValue> GetStockPropValue(Func<StockPropValue, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockPropValue> list = alading.StockPropValue.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public StockPropValue GetStockPropValue(string stockpropvalueCode)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*List<StockPropValue> list = alading.StockPropValue.Where(p => p.StockPropValueID == stockpropvalueID).ToList();*/
            //        List<StockPropValue> list = alading.StockPropValue.Where(p => p.StockPropValueCode == stockpropvalueCode).ToList();
            //        if (list.Count == 0)
            //        {
            //            return null;
            //        }
            //        else
            //        {
            //            return list.First();
            //        }
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    throw ex;
            //}
        }
        
        public List<StockPropValue> GetStockPropValue(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.StockPropValue orderby u.StockPropValueID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.StockPropValue.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<StockPropValue> GetStockPropValue(Func<StockPropValue, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<StockPropValue> list = alading.StockPropValue.Where(func).OrderByDescending(a=>a.StockPropValueID);
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

