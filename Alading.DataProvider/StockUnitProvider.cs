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
        #region Tax
        public ReturnType AddTax(Tax tax)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    alading.AddToTax(tax);
                    alading.SaveChanges();                    
                    return ReturnType.Success;                    
                }
            }          
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ReturnType UpdateTax(Tax tax)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    Tax result = alading.Tax.FirstOrDefault(i => i.TaxCode == tax.TaxCode);
                    if (result == null)
                        return ReturnType.NotExisted;

                    //默认税率只有一个
                    if (tax.IsDefault == true)
                    {
                        Tax taxDefault = alading.Tax.FirstOrDefault(i => i.IsDefault == true);
                        if (taxDefault != null)
                        {
                            taxDefault.IsDefault = false;
                        }
                    }

                    result.TaxName = tax.TaxName;
                    result.TaxRemark = tax.TaxRemark;
                    result.OutPutTax = tax.OutPutTax;
                    result.IsDefault = tax.IsDefault;
                    result.InputTax = tax.InputTax;
                    result.Formula = tax.Formula;    

                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Tax> GetTax(Func<Tax, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {   
                    return alading.Tax.Where(func).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Tax> GetAllTax()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    return alading.Tax.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public ReturnType AddStockUnit(StockUnit stockunit)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToStockUnit(stockunit);
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
                
        public ReturnType AddStockUnit(List<StockUnit> stockunitList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (StockUnit stockunit in stockunitList)
                    {
                        alading.AddToStockUnit(stockunit);
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
       
        public ReturnType RemoveAllStockUnit()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockUnit> list = alading.StockUnit.ToList();
                    foreach (StockUnit stockunit in list)
                    {
                        alading.DeleteObject(stockunit);
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
       
        public ReturnType RemoveStockUnit(Func<StockUnit, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockUnit> list = alading.StockUnit.Where(func).ToList();
                    foreach (StockUnit stockunit in list)
                    {
                        alading.DeleteObject(stockunit);
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

        public List<StockUnit> GetStockUnit(List<string> stockunitCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.StockUnit.Where(BuildWhereInExpression<StockUnit, int>(v => v.StockUnitID, stockunitIDList));*/
                    var result = alading.StockUnit.Where(BuildWhereInExpression<StockUnit, string>(v => v.StockUnitCode, stockunitCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveStockUnit(List<string> stockunitCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.StockUnit.Where(BuildWhereInExpression<StockUnit, int>(v => v.StockUnitID, stockunitIDList));*/
                    var result = alading.StockUnit.Where(BuildWhereInExpression<StockUnit, string>(v => v.StockUnitCode, stockunitCodeList));
                    foreach (StockUnit s in result)
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

    
        public ReturnType RemoveStockUnit(string stockunitCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<StockUnit> list = alading.StockUnit.Where(p => p.StockUnitID == stockunitID).ToList();*/
                    List<StockUnit> list = alading.StockUnit.Where(p => p.StockUnitCode == stockunitCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        StockUnit sy = list.First();
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
      
        public ReturnType UpdateStockUnit(StockUnit stockunit)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*StockUnit result = alading.StockUnit.Where(p => p.StockUnitID == stockunit.StockUnitID).FirstOrDefault();*/
                    StockUnit result = alading.StockUnit.Where(p => p.StockUnitCode == stockunit.StockUnitCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("StockUnit", stockunit);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.StockUnitCode = stockunit.StockUnitCode;
                    
                        result.StockUnitName = stockunit.StockUnitName;
                    
                        result.IsBaseUnit = stockunit.IsBaseUnit;
                    
                        result.StockUnitGroupCode = stockunit.StockUnitGroupCode;
                    
                        result.Conversion = stockunit.Conversion;
                    
                        result.StockUnitSource = stockunit.StockUnitSource;
			
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
       
        public ReturnType UpdateStockUnit(string stockunitCode, StockUnit stockunit)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.StockUnit.Where(p => p.StockUnitID == stockunitID).ToList();*/
                    var result = alading.StockUnit.Where(p => p.StockUnitCode == stockunitCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    StockUnit ob = result.First();
                    //ob.StockUnitCode = stockunit.StockUnitCode;
                    ob.StockUnitName = stockunit.StockUnitName;
                    //ob.IsBaseUnit = stockunit.IsBaseUnit;
                    //ob.StockUnitGroupCode = stockunit.StockUnitGroupCode;
                    ob.Conversion = stockunit.Conversion;
                    //ob.StockUnitSource = stockunit.StockUnitSource;
                    ob.Remark = stockunit.Remark;
                    
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
     
        public List<StockUnit> GetAllStockUnit()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockUnit> list = alading.StockUnit.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<StockUnit> GetStockUnit(Func<StockUnit, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockUnit> list = alading.StockUnit.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        #region View_GroupUnit

        public List<View_GroupUnit> GetAllView_GroupUnit()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    return alading.View_GroupUnit.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<View_GroupUnit> GetView_GroupUnit(Func<View_GroupUnit, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    return alading.View_GroupUnit.Where(func).OrderBy(c=>c.StockUnitGroupCode).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        /*
        public StockUnit GetStockUnit(string stockunitCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    //List<StockUnit> list = alading.StockUnit.Where(p => p.StockUnitID == stockunitID).ToList();
                    List<StockUnit> list = alading.StockUnit.Where(p => p.StockUnitCode == stockunitCode).ToList();
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

        public List<StockUnit> GetStockUnit(string stockGroupUnitCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<StockUnit> list = alading.StockUnit.Where(p => p.StockUnitID == stockunitID).ToList();*/
                    List<StockUnit> list = alading.StockUnit.Where(p => p.StockUnitGroupCode == stockGroupUnitCode).ToList();
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
        
        public List<StockUnit> GetStockUnit(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.StockUnit orderby u.StockUnitID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.StockUnit.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<StockUnit> GetStockUnit(Func<StockUnit, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<StockUnit> list = alading.StockUnit.Where(func).OrderByDescending(a=>a.StockUnitID);
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

