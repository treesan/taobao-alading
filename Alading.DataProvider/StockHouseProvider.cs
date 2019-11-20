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
        #region StockHouse部分

        public ReturnType AddStockHouse(StockHouse stockhouse)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    StockHouse result = alading.StockHouse.FirstOrDefault(i => i.StockHouseCode == stockhouse.StockHouseCode);
                    if (result != null)
                    {
                        return ReturnType.PropertyExisted;
                    }
                    StockLayout stockLayout = new StockLayout();
                    stockLayout.LayoutName = "默认库位";
                    stockLayout.LayoutRemark = string.Empty;
                    stockLayout.StockHouseCode = stockhouse.StockHouseCode;
                    stockLayout.StockLayoutCode = Guid.NewGuid().ToString();

                    alading.AddToStockHouse(stockhouse);
                    alading.AddToStockLayout(stockLayout);

                    alading.SaveChanges();
                   
                    return ReturnType.Success;
                   
                }
            }            
            catch (Exception ex)
            {
                throw ex;
            }

        }
                
        public ReturnType AddStockHouse(List<StockHouse> stockhouseList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (StockHouse stockhouse in stockhouseList)
                    {
                        alading.AddToStockHouse(stockhouse);
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
       
        public ReturnType RemoveAllStockHouse()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockHouse> list = alading.StockHouse.ToList();
                    foreach (StockHouse stockhouse in list)
                    {
                        alading.DeleteObject(stockhouse);
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
       
        public ReturnType RemoveStockHouse(Func<StockHouse, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockHouse> list = alading.StockHouse.Where(func).ToList();
                    foreach (StockHouse stockhouse in list)
                    {
                        alading.DeleteObject(stockhouse);
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

        public List<StockHouse> GetStockHouse(List<string> stockhouseCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.StockHouse.Where(BuildWhereInExpression<StockHouse, int>(v => v.StockHouseID, stockhouseIDList));*/
                    var result = alading.StockHouse.Where(BuildWhereInExpression<StockHouse, string>(v => v.StockHouseCode, stockhouseCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveStockHouse(List<string> stockhouseCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.StockHouse.Where(BuildWhereInExpression<StockHouse, int>(v => v.StockHouseID, stockhouseIDList));*/
                    var result = alading.StockHouse.Where(BuildWhereInExpression<StockHouse, string>(v => v.StockHouseCode, stockhouseCodeList));
                    foreach (StockHouse s in result)
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


        public ReturnType RemoveStockHouse(string stockhouseCode)
        {
            System.Data.Common.DbTransaction tran = null;
            using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            {                
                try
                {                    
                    /*List<StockHouse> list = alading.StockHouse.Where(p => p.StockHouseID == stockhouseID).ToList();*/
                    List<StockHouse> list = alading.StockHouse.Where(p => p.StockHouseCode == stockhouseCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                    else
                    {
                        alading.Connection.Open();
                        tran = alading.Connection.BeginTransaction();
                        List<StockLayout> layoutList = alading.StockLayout.Where(i => i.StockHouseCode == stockhouseCode).ToList();
                        if (layoutList != null)
                        {
                            foreach (StockLayout layout in layoutList)
                            {
                                alading.DeleteObject(layout);
                            }
                        }
                        StockHouse sy = list.First();
                        alading.DeleteObject(sy);
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

        }
      
        public ReturnType UpdateStockHouse(StockHouse stockhouse)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*StockHouse result = alading.StockHouse.Where(p => p.StockHouseID == stockhouse.StockHouseID).FirstOrDefault();*/
                    StockHouse result = alading.StockHouse.Where(p => p.StockHouseCode == stockhouse.StockHouseCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    result.HouseAddress = stockhouse.HouseAddress;
                    result.HouseContact = stockhouse.HouseContact;
                    result.HouseName = stockhouse.HouseName;
                    result.HouseRemark = stockhouse.HouseRemark;
                    result.HouseTel = stockhouse.HouseTel;
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.HouseCode = stockhouse.HouseCode;
                    
                        result.HouseName = stockhouse.HouseName;
                    
                        result.HouseAddress = stockhouse.HouseAddress;
                    
                        result.HouseRemark = stockhouse.HouseRemark;
			
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
       
        public ReturnType UpdateStockHouse(string stockhouseCode, StockHouse stockhouse)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*var result = alading.StockHouse.Where(p => p.StockHouseID == stockhouseID).ToList();*/
            //        var result = alading.StockHouse.Where(p => p.StockHouseCode == stockhouseCode).ToList();
            //        if (result.Count == 0)
            //        {
            //            return ReturnType.NotExisted;
            //        }
                  
            //        StockHouse ob = result.First();
            //        ob.HouseCode = stockhouse.HouseCode;
            //        ob.HouseName = stockhouse.HouseName;
            //        ob.HouseAddress = stockhouse.HouseAddress;
            //        ob.HouseRemark = stockhouse.HouseRemark;
                    
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
     
        public List<StockHouse> GetAllStockHouse()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockHouse> list = alading.StockHouse.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<StockHouse> GetStockHouse(Func<StockHouse, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockHouse> list = alading.StockHouse.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public StockHouse GetStockHouse(string stockhouseCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<StockHouse> list = alading.StockHouse.Where(p => p.StockHouseID == stockhouseID).ToList();*/
                    List<StockHouse> list = alading.StockHouse.Where(p => p.StockHouseCode == stockhouseCode).ToList();
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
        
        public List<StockHouse> GetStockHouse(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.StockHouse orderby u.StockHouseID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.StockHouse.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<StockHouse> GetStockHouse(Func<StockHouse, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<StockHouse> list = alading.StockHouse.Where(func).OrderByDescending(a=>a.StockHouseID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region StockHouseProduct部分
        public int GetQuantity(string skuOuterID, string houseCode, string layoutCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    StockHouseProduct product = alading.StockHouseProduct.FirstOrDefault(i => i.SkuOuterID == skuOuterID && i.HouseCode == houseCode && i.LayoutCode == layoutCode);
                    if (product != null)
                        return product.Num;
                    else
                        return 0;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<StockHouseProduct> GetAllStockHouseProduct()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockHouseProduct> list = alading.StockHouseProduct.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType AddStockHouseProduct(StockHouseProduct stockHouseProduct)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToStockHouseProduct(stockHouseProduct);
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

        public ReturnType AddStockHouseProduct(List<StockHouseProduct> StockHouseProductList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (StockHouseProduct stockhouseProduct in StockHouseProductList)
                    {
                        alading.AddToStockHouseProduct(stockhouseProduct);
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

        public List<StockHouseProduct> GetStockHouseProduct(Func<StockHouseProduct, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockHouseProduct> list = alading.StockHouseProduct.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<StockHouseProduct> GetStockHouseProduct(List<string> StockHouseProductCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.StockHouse.Where(BuildWhereInExpression<StockHouse, int>(v => v.StockHouseID, stockhouseIDList));*/
                    var result = alading.StockHouseProduct.Where(BuildWhereInExpression<StockHouseProduct, string>(v => v.HouseProductCode, StockHouseProductCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public StockHouseProduct GetStockHouseProduct(string SkuOuterID)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.StockHouse.Where(BuildWhereInExpression<StockHouse, int>(v => v.StockHouseID, stockhouseIDList));*/
                    return alading.StockHouseProduct.Where(c => c.SkuOuterID == SkuOuterID).FirstOrDefault();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public List<StockHouseProduct> GetSHProBySkuOuterID(List<string> SkuOuterIDList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var result = alading.StockHouseProduct.Where(BuildWhereInExpression<StockHouseProduct, string>(v => v.SkuOuterID, SkuOuterIDList));
                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}

