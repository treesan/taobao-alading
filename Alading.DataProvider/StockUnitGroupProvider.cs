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

        public ReturnType AddStockUnitGroup(StockUnitGroup stockunitgroup, StockUnit unit)
        {
            System.Data.Common.DbTransaction tran = null;            
            using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            {
                try
                {
                    alading.Connection.Open();
                    tran = alading.Connection.BeginTransaction();
                    alading.AddToStockUnitGroup(stockunitgroup);
                    alading.AddToStockUnit(unit);
                    alading.SaveChanges();
                    tran.Commit();                   
                    return ReturnType.Success;
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

        public bool IsCodeOnly(string unitCode, string unitGroupCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    if (alading.StockUnit.FirstOrDefault(c => c.StockUnitCode == unitCode) != null)
                    {
                        return false;
                    }
                    if (alading.StockUnitGroup.FirstOrDefault(c => c.StockUnitGroupCode == unitGroupCode)!=null)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (SqlException sex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ReturnType AddStockUnitGroup(List<StockUnitGroup> stockunitgroupList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (StockUnitGroup stockunitgroup in stockunitgroupList)
                    {
                        alading.AddToStockUnitGroup(stockunitgroup);
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
       
        public ReturnType RemoveAllStockUnitGroup()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockUnitGroup> list = alading.StockUnitGroup.ToList();
                    foreach (StockUnitGroup stockunitgroup in list)
                    {
                        alading.DeleteObject(stockunitgroup);
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
       
        public ReturnType RemoveStockUnitGroup(Func<StockUnitGroup, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockUnitGroup> list = alading.StockUnitGroup.Where(func).ToList();
                    foreach (StockUnitGroup stockunitgroup in list)
                    {
                        alading.DeleteObject(stockunitgroup);
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

        public List<StockUnitGroup> GetStockUnitGroup(List<string> stockunitgroupCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.StockUnitGroup.Where(BuildWhereInExpression<StockUnitGroup, int>(v => v.StockUnitGroupID, stockunitgroupIDList));*/
                    var result = alading.StockUnitGroup.Where(BuildWhereInExpression<StockUnitGroup, string>(v => v.StockUnitGroupCode, stockunitgroupCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveStockUnitGroup(List<string> stockunitgroupCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.StockUnitGroup.Where(BuildWhereInExpression<StockUnitGroup, int>(v => v.StockUnitGroupID, stockunitgroupIDList));*/
                    var result = alading.StockUnitGroup.Where(BuildWhereInExpression<StockUnitGroup, string>(v => v.StockUnitGroupCode, stockunitgroupCodeList));
                    foreach (StockUnitGroup s in result)
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

    
        public ReturnType RemoveStockUnitGroup(string stockunitgroupCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<StockUnitGroup> list = alading.StockUnitGroup.Where(p => p.StockUnitGroupID == stockunitgroupID).ToList();*/
                    List<StockUnitGroup> list = alading.StockUnitGroup.Where(p => p.StockUnitGroupCode == stockunitgroupCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        StockUnitGroup sy = list.First();
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

        public ReturnType UpdateStockUnitGroup(StockUnitGroup stockunitgroup)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*StockUnitGroup result = alading.StockUnitGroup.Where(p => p.StockUnitGroupID == stockunitgroup.StockUnitGroupID).FirstOrDefault();*/
                    StockUnitGroup result = alading.StockUnitGroup.Where(p => p.StockUnitGroupCode == stockunitgroup.StockUnitGroupCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    //alading.Attach(result);
                    //alading.ApplyPropertyChanges("StockUnitGroup", stockunitgroup);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    		                    
                    result.Remark = stockunitgroup.Remark;
                
                    result.StockUnitGroupName = stockunitgroup.StockUnitGroupName;
                                   
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
       
        /*
        public ReturnType UpdateStockUnitGroup(string stockunitgroupCode, StockUnitGroup stockunitgroup)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    //var result = alading.StockUnitGroup.Where(p => p.StockUnitGroupID == stockunitgroupID).ToList();
                    var result = alading.StockUnitGroup.Where(p => p.StockUnitGroupCode == stockunitgroupCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    StockUnitGroup ob = result.First();
                    ob.StockUnitGroupCode = stockunitgroup.StockUnitGroupCode;
                    ob.StockUnitGroupName = stockunitgroup.StockUnitGroupName;
                    ob.BaseUnit = stockunitgroup.BaseUnit;
                    
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
        */

        public ReturnType UpdateStockUnitGroup(string stockUnitGroupName, StockUnitGroup stockunitgroup)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var result = alading.StockUnitGroup.Where(p => p.StockUnitGroupName == stockUnitGroupName).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    StockUnitGroup ob = result.First();
                    //ob.StockUnitGroupCode = stockunitgroup.StockUnitGroupCode;
                    ob.StockUnitGroupName = stockunitgroup.StockUnitGroupName;
                    //ob.BaseUnit = stockunitgroup.BaseUnit;

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

     
        public List<StockUnitGroup> GetAllStockUnitGroup()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockUnitGroup> list = alading.StockUnitGroup.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<StockUnitGroup> GetStockUnitGroup(Func<StockUnitGroup, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockUnitGroup> list = alading.StockUnitGroup.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /*
        public StockUnitGroup GetStockUnitGroup(string stockunitgroupCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                   // List<StockUnitGroup> list = alading.StockUnitGroup.Where(p => p.StockUnitGroupID == stockunitgroupID).ToList();
                    List<StockUnitGroup> list = alading.StockUnitGroup.Where(p => p.StockUnitGroupCode == stockunitgroupCode).ToList();
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
        */
        public StockUnitGroup GetStockUnitGroup(string stockunitgroupName)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockUnitGroup> list = alading.StockUnitGroup.Where(p => p.StockUnitGroupName == stockunitgroupName).ToList();
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
        
        public List<StockUnitGroup> GetStockUnitGroup(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.StockUnitGroup orderby u.StockUnitGroupID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.StockUnitGroup.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<StockUnitGroup> GetStockUnitGroup(Func<StockUnitGroup, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<StockUnitGroup> list = alading.StockUnitGroup.Where(func).OrderByDescending(a=>a.StockUnitGroupID);
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

