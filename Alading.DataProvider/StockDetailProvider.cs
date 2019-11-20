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
     
        public ReturnType AddStockDetail(StockDetail stockdetail)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToStockDetail(stockdetail);
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
                
        public ReturnType AddStockDetail(List<StockDetail> stockdetailList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (StockDetail stockdetail in stockdetailList)
                    {
                        alading.AddToStockDetail(stockdetail);
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
       
        public ReturnType RemoveAllStockDetail()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockDetail> list = alading.StockDetail.ToList();
                    foreach (StockDetail stockdetail in list)
                    {
                        alading.DeleteObject(stockdetail);
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
       
        public ReturnType RemoveStockDetail(Func<StockDetail, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockDetail> list = alading.StockDetail.Where(func).ToList();
                    foreach (StockDetail stockdetail in list)
                    {
                        alading.DeleteObject(stockdetail);
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

        public List<StockDetail> GetStockDetail(List<string> stockdetailCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.StockDetail.Where(BuildWhereInExpression<StockDetail, int>(v => v.StockDetailID, stockdetailIDList));*/
                    var result = alading.StockDetail.Where(BuildWhereInExpression<StockDetail, string>(v => v.StockDetailCode, stockdetailCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveStockDetail(List<string> stockdetailCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.StockDetail.Where(BuildWhereInExpression<StockDetail, int>(v => v.StockDetailID, stockdetailIDList));*/
                    var result = alading.StockDetail.Where(BuildWhereInExpression<StockDetail, string>(v => v.StockDetailCode, stockdetailCodeList));
                    foreach (StockDetail s in result)
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

    
        public ReturnType RemoveStockDetail(string stockdetailCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<StockDetail> list = alading.StockDetail.Where(p => p.StockDetailID == stockdetailID).ToList();*/
                    List<StockDetail> list = alading.StockDetail.Where(p => p.StockDetailCode == stockdetailCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        StockDetail sy = list.First();
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
      
        public ReturnType UpdateStockDetail(StockDetail stockdetail)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*StockDetail result = alading.StockDetail.Where(p => p.StockDetailID == stockdetail.StockDetailID).FirstOrDefault();*/
                    StockDetail result = alading.StockDetail.Where(p => p.StockDetailCode == stockdetail.StockDetailCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("StockDetail", stockdetail);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.StockDetailCode = stockdetail.StockDetailCode;
                    
                        result.ProductSkuOuterId = stockdetail.ProductSkuOuterId;
                    
                        result.InOutCode = stockdetail.InOutCode;
                    
                        result.StockHouseCode = stockdetail.StockHouseCode;
                    
                        result.Price = stockdetail.Price;
                    
                        result.Quantity = stockdetail.Quantity;
                    
                        result.DetailType = stockdetail.DetailType;
                    
                        result.TaxFee = stockdetail.TaxFee;
                    
                        result.TotalFee = stockdetail.TotalFee;
                    
                        result.DetailRemark = stockdetail.DetailRemark;
			
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
       
        public ReturnType UpdateStockDetail(string stockdetailCode, StockDetail stockdetail)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.StockDetail.Where(p => p.StockDetailID == stockdetailID).ToList();*/
                    var result = alading.StockDetail.Where(p => p.StockDetailCode == stockdetailCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    StockDetail ob = result.First();
                    ob.StockDetailCode = stockdetail.StockDetailCode;
                    ob.ProductSkuOuterId = stockdetail.ProductSkuOuterId;
                    ob.InOutCode = stockdetail.InOutCode;
                    ob.StockHouseCode = stockdetail.StockHouseCode;
                    ob.Price = stockdetail.Price;
                    ob.Quantity = stockdetail.Quantity;
                    ob.DetailType = stockdetail.DetailType;
                    ob.Tax = stockdetail.Tax;
                    ob.TotalFee = stockdetail.TotalFee;
                    ob.DetailRemark = stockdetail.DetailRemark;
                    
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
     
        public List<StockDetail> GetAllStockDetail()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockDetail> list = alading.StockDetail.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<StockDetail> GetStockDetail(Func<StockDetail, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockDetail> list = alading.StockDetail.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public StockDetail GetStockDetail(string stockdetailCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<StockDetail> list = alading.StockDetail.Where(p => p.StockDetailID == stockdetailID).ToList();*/
                    List<StockDetail> list = alading.StockDetail.Where(p => p.StockDetailCode == stockdetailCode).ToList();
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
        
        public List<StockDetail> GetStockDetail(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.StockDetail orderby u.StockDetailID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.StockDetail.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<StockDetail> GetStockDetail(Func<StockDetail, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<StockDetail> list = alading.StockDetail.Where(func).OrderByDescending(a=>a.StockDetailID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        #region 历史明细

        public List<HistoryStockDetail> GetHistoryDetail(string inoutCode)
        {
             try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IEnumerable<HistoryStockDetail> list = alading.HistoryStockDetail.Where(c=>c.InOutCode==inoutCode);
                    return list.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoryStockDetail> GetHistoryDetail(Func<HistoryStockDetail, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IEnumerable<HistoryStockDetail> list = alading.HistoryStockDetail.Where(func).OrderBy(c=>c.HistoryStockDetailID);
                    return list.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 盘点部分

        public ReturnType AddStockCheck(StockCheck stockCheck)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToStockCheck(stockCheck);
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

        public ReturnType Check(int num, List<StockDetail> stockDetailList, StockDetail checkDetail, StockCheckDetail stockCheckDetail)
        {
            System.Data.Common.DbTransaction tran = null;
            using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            {
                try
                {
                    alading.Connection.Open();
                    tran = alading.Connection.BeginTransaction();
                    StockProduct stockProduct = alading.StockProduct.FirstOrDefault(c => c.SkuOuterID == checkDetail.ProductSkuOuterId);
                    StockItem stockItem = alading.StockItem.FirstOrDefault(c => c.OuterID == stockProduct.OuterID);
                    StockHouseProduct stockHouseProduct = alading.StockHouseProduct.FirstOrDefault(c => c.HouseCode == checkDetail.StockHouseCode && c.LayoutCode == checkDetail.StockLayOutCode && c.SkuOuterID == checkDetail.ProductSkuOuterId);
                    /*修改相关数量，若报溢则加，报损则减*/
                    stockProduct.SkuQuantity += num;
                    stockItem.TotalQuantity += num;
                    if (stockHouseProduct != null)
                    {
                        stockHouseProduct.Num += num;
                    }
                    else
                    {
                        stockHouseProduct = new StockHouseProduct();
                        stockHouseProduct.HouseCode = checkDetail.StockHouseCode;
                        stockHouseProduct.HouseName = checkDetail.HouseName;
                        stockHouseProduct.HouseProductCode = System.Guid.NewGuid().ToString();
                        stockHouseProduct.LayoutCode = checkDetail.StockLayOutCode;
                        stockHouseProduct.LayoutName = checkDetail.LayoutName;
                        stockHouseProduct.Num = num;
                        stockHouseProduct.SkuOuterID = checkDetail.ProductSkuOuterId;
                        alading.AddToStockHouseProduct(stockHouseProduct);
                    }

                    /*将出入库详情添加到历史表中并在StockDetail表中删除该数据*/
                    foreach (StockDetail stockDetail in stockDetailList)
                    {
                        HistoryStockDetail hisStockDetail = new HistoryStockDetail();
                        hisStockDetail.DetailRemark = stockDetail.DetailRemark;
                        hisStockDetail.DetailType = stockDetail.DetailType;
                        hisStockDetail.DurabilityDate = stockDetail.DurabilityDate;
                        hisStockDetail.HistoryStockDetailCode = stockDetail.StockDetailCode;
                        hisStockDetail.HouseName = stockDetail.HouseName;
                        hisStockDetail.InOutCode = stockDetail.InOutCode;
                        hisStockDetail.LayoutName = stockDetail.LayoutName;
                        hisStockDetail.Price = stockDetail.Price;
                        hisStockDetail.ProductSkuOuterId = stockDetail.ProductSkuOuterId;
                        hisStockDetail.Quantity = stockDetail.Quantity;
                        hisStockDetail.SearchText = stockDetail.SearchText;
                        hisStockDetail.StockHouseCode = stockDetail.StockHouseCode;
                        hisStockDetail.StockLayOutCode = stockDetail.StockLayOutCode;
                        hisStockDetail.Tax = stockDetail.Tax;
                        hisStockDetail.TotalFee = stockDetail.TotalFee;
                        alading.AddToHistoryStockDetail(hisStockDetail);

                        StockDetail temp = alading.StockDetail.FirstOrDefault(c => c.StockDetailCode == stockDetail.StockDetailCode);
                        if (temp != null)
                        {
                            alading.DeleteObject(temp);
                        }
                    }

                    /*将盘点生成的入库详情加入StockDetail表中，做为初始数据*/
                    alading.AddToStockDetail(checkDetail);
                    alading.AddToStockCheckDetail(stockCheckDetail);
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

        #endregion
    }
}

