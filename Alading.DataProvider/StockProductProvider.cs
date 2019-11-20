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
        public List<View_CheckDetail> GetViewCheckDetail(Func<View_CheckDetail, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_CheckDetail> viewList = alading.View_CheckDetail.Where(func).ToList();
                    return viewList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<View_CheckDetail> GetViewCheckDetail(string stockCheckCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_CheckDetail> viewList = alading.View_CheckDetail.Where(i=>i.StockCheckCode==stockCheckCode).ToList();
                    return viewList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetSkuOuterID(string stockHouseProduct_HouseCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var q = from i in alading.StockHouseProduct
                            where i.HouseCode == stockHouseProduct_HouseCode
                            select i.SkuOuterID;
                    return q.Distinct().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType AddStockCheckAndDetails(StockCheck stockCheck, List<StockCheckDetail> CheckDetails)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    alading.AddToStockCheck(stockCheck);
                    foreach (StockCheckDetail detail in CheckDetails)
                    {
                        alading.AddToStockCheckDetail(detail);

                        #region StockInOut
                        Alading.Entity.StockInOut stockInOut = new Alading.Entity.StockInOut();
                        stockInOut.AmountTax = 0;
                        stockInOut.DiscountFee = 0;
                        stockInOut.DueFee = 0;
                        stockInOut.FreightCode = string.Empty;
                        stockInOut.FreightCompany = string.Empty;
                        stockInOut.InOutCode = Guid.NewGuid().ToString();
                        stockInOut.InOutTime = DateTime.Now;
                        if (detail.ProfitType == (int)ProfitType.PROFIT)
                            stockInOut.InOutType = (int)InOutType.ProfitIn;
                        else
                            stockInOut.InOutType = (int)InOutType.LossOut;
                        stockInOut.TradeOrderCode = string.Empty;

                        stockInOut.OperatorCode = stockCheck.OperatorCode;
                        stockInOut.OperatorName = string.Empty;
                        stockInOut.PayType = (int)PayType.CASH;

                        stockInOut.IsSettled = true;
                        stockInOut.PayTerm = 0;
                        stockInOut.IncomeTime = DateTime.MinValue;
                        stockInOut.PayThisTime = 0;
                        #endregion

                        #region StockDetail
                        StockDetail stockDetailOut = new StockDetail();
                        stockDetailOut.ProductSkuOuterId = detail.SkuOuterID;
                        stockDetailOut.DetailRemark = string.Empty;
                        stockDetailOut.DetailType = (int)DetailType.AllocateOut;
                        stockDetailOut.DurabilityDate = DateTime.Now;
                        stockDetailOut.InOutCode = stockInOut.InOutCode;
                        stockDetailOut.Price = 0;
                        stockDetailOut.Quantity = detail.CheckQuantity;
                        stockDetailOut.StockDetailCode = Guid.NewGuid().ToString();
                        stockDetailOut.StockHouseCode = stockCheck.StockHouseCode;
                        stockDetailOut.StockLayOutCode = detail.LayoutCode;
                        stockDetailOut.Tax = string.Empty;
                        stockDetailOut.TotalFee = 0;
                        #endregion

                        StockProduct product = alading.StockProduct.FirstOrDefault(i => i.SkuOuterID == detail.SkuOuterID);
                        if (product != null)
                        {
                            product.SkuQuantity = detail.Quantity;//归档
                        }
                        alading.AddToStockInOut(stockInOut);
                        alading.AddToStockDetail(stockDetailOut);
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
        public List<View_StockCheck> GetAllViewStockCheck()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    return alading.View_StockCheck.ToList();                   
                }
            }         
            catch (Exception ex)
            {
                return new List<View_StockCheck>();
            }
        }
     
        public ReturnType AddStockProduct(StockProduct stockproduct)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToStockProduct(stockproduct);
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
                
        public ReturnType AddStockProduct(List<StockProduct> stockproductList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (StockProduct stockproduct in stockproductList)
                    {
                        alading.AddToStockProduct(stockproduct);
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
       
        public ReturnType RemoveAllStockProduct()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockProduct> list = alading.StockProduct.ToList();
                    foreach (StockProduct stockproduct in list)
                    {
                        alading.DeleteObject(stockproduct);
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
       
        public ReturnType RemoveStockProduct(Func<StockProduct, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockProduct> list = alading.StockProduct.Where(func).ToList();
                    foreach (StockProduct stockproduct in list)
                    {
                        alading.DeleteObject(stockproduct);
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

        public List<StockProduct> GetStockProduct(List<string> skuOuterIDList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.StockProduct.Where(BuildWhereInExpression<StockProduct, int>(v => v.StockProductID, stockproductIDList));*/
                    var result = alading.StockProduct.Where(BuildWhereInExpression<StockProduct, string>(v => v.SkuOuterID, skuOuterIDList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveStockProduct(List<string> skuOutIDList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.StockProduct.Where(BuildWhereInExpression<StockProduct, int>(v => v.StockProductID, stockproductIDList));*/
                    var result = alading.StockProduct.Where(BuildWhereInExpression<StockProduct, string>(v => v.SkuOuterID, skuOutIDList));
                    foreach (StockProduct s in result)
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

    
        public ReturnType RemoveStockProduct(string skuOutID)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<StockProduct> list = alading.StockProduct.Where(p => p.StockProductID == stockproductID).ToList();*/
                    List<StockProduct> list = alading.StockProduct.Where(p => p.SkuOuterID == skuOutID).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        StockProduct sy = list.First();
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

        public ReturnType RemoveStockProduct(string skuOuterID, string outerID)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

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
      
        public ReturnType UpdateStockProduct(StockProduct stockproduct)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*StockProduct result = alading.StockProduct.Where(p => p.StockProductID == stockproduct.StockProductID).FirstOrDefault();*/
                    StockProduct result = alading.StockProduct.Where(p => p.SkuOuterID == stockproduct.SkuOuterID).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;
                    /*
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("StockProduct", stockproduct);
                      */
                    #endregion

                    #region    Using All Items Replace To Update ,Default UnUse
                        result.OuterID = stockproduct.OuterID;
                        result.SkuOuterID = stockproduct.SkuOuterID;
                        result.SkuProps = stockproduct.SkuProps;
                        result.SkuProps_Str = stockproduct.SkuProps_Str;
                        result.SkuQuantity = stockproduct.SkuQuantity;
                        result.MarketPrice = stockproduct.MarketPrice;
                        result.SkuPrice = stockproduct.SkuPrice;
                        result.MinSkuPrice = stockproduct.MinSkuPrice;
                        result.MaxSkuPrice = stockproduct.MaxSkuPrice;
                        result.CommissionPrice = stockproduct.CommissionPrice;
                        result.WholeSalePrice = stockproduct.WholeSalePrice;
                        result.LastStockPrice = stockproduct.LastStockPrice;
                        result.AvgStockPrice = stockproduct.AvgStockPrice;
                        result.IsUsingWarn = stockproduct.IsUsingWarn;
                        result.LowestNum = stockproduct.LowestNum;
                        result.HighestNum = stockproduct.HighestNum;
                        result.OccupiedQuantity = stockproduct.OccupiedQuantity;
                        result.StockProductRemark = stockproduct.StockProductRemark;
                        result.ProductStatus = stockproduct.ProductStatus;
                        result.WarningCount = stockproduct.WarningCount;
                        result.PropsAlias = stockproduct.PropsAlias;
                    #endregion
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
            catch (Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

        /// <summary>
        /// copydata  3.21 fxg建
        /// </summary>
        /// <param name="result"></param>
        /// <param name="stockproduct"></param>
        public void StockProductCopyData(StockProduct result, StockProduct stockproduct)
        {
            result.OuterID = stockproduct.OuterID;

            result.SkuOuterID = stockproduct.SkuOuterID;

            result.SkuProps = stockproduct.SkuProps;

            result.SkuQuantity = stockproduct.SkuQuantity;

            result.SkuPrice = stockproduct.SkuPrice;

            //result.LastInputPrice = stockproduct.LastInputPrice;

            //result.AvgInputPrice = stockproduct.AvgInputPrice;

            result.WarningCount = stockproduct.WarningCount;

            result.OccupiedQuantity = stockproduct.OccupiedQuantity;

            //result.BarCode = stockproduct.BarCode;

            //result.BarCodeUrl = stockproduct.BarCodeUrl;

            //result.BuyTaxRate = stockproduct.BuyTaxRate;

            //result.SaleTaxRate = stockproduct.SaleTaxRate;

            result.StockProductRemark = stockproduct.StockProductRemark;

            //result.ProductModel = stockproduct.ProductModel;

            //result.ProductSpecification = stockproduct.ProductSpecification;

            //result.ProductUnit = stockproduct.ProductUnit;
			
        }

        public ReturnType UpdateStockProduct(string skuOutID, StockProduct stockproduct)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.StockProduct.Where(p => p.StockProductID == stockproductID).ToList();*/
                    var result = alading.StockProduct.Where(p => p.SkuOuterID == skuOutID).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    //StockProduct ob = result.First();
                    //ob.OuterID = stockproduct.OuterID;
                    //ob.SkuOuterID = stockproduct.SkuOuterID;
                    //ob.SkuProps = stockproduct.SkuProps;
                    //ob.SkuQuantity = stockproduct.SkuQuantity;
                    //ob.SkuPrice = stockproduct.SkuPrice;
                    //ob.LastInputPrice = stockproduct.LastInputPrice;
                    //ob.AvgInputPrice = stockproduct.AvgInputPrice;
                    //ob.WarningCount = stockproduct.WarningCount;
                    //ob.OccupiedQuantity = stockproduct.OccupiedQuantity;
                    //ob.BCode = stockproduct.BarCode;
                    //ob.BarCodeUrl = stockproduct.BarCodeUrl;
                    //ob.BuyTaxRate = stockproduct.BuyTaxRate;
                    //ob.SaleTaxRate = stockproduct.SaleTaxRate;
                    //ob.StockProductRemark = stockproduct.StockProductRemark;
                    //ob.ProductModel = stockproduct.ProductModel;
                    //ob.ProductSpecification = stockproduct.ProductSpecification;
                    //ob.ProductUnit = stockproduct.ProductUnit;
                    
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
     
        public List<StockProduct> GetAllStockProduct()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockProduct> list = alading.StockProduct.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<StockProduct> GetStockProduct(Func<StockProduct, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockProduct> list = alading.StockProduct.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<StockProduct> GetStockProductByOuterId(string outer_id)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockProduct> list = alading.StockProduct.Where(s=>s.OuterID==outer_id).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_StockProductHouse> GetViewStockProduct(string outerID)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_StockProductHouse> list = alading.View_StockProductHouse.Where(i => i.OuterID == outerID).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public StockProduct GetStockProduct(string skuOutID)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<StockProduct> list = alading.StockProduct.Where(p => p.StockProductID == stockproductID).ToList();*/
                    List<StockProduct> list = alading.StockProduct.Where(p => p.SkuOuterID == skuOutID).ToList();
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
        
        public List<StockProduct> GetStockProduct(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.StockProduct orderby u.StockProductID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.StockProduct.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<StockProduct> GetStockProduct(Func<StockProduct, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<StockProduct> list = alading.StockProduct.Where(func).OrderByDescending(a=>a.StockProductID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        public ReturnType UpdateStock(List<StockProduct> stockProductList, List<StockHouseProduct> stockHouseProList, List<StockInOut> stockInOutList
            , List<StockDetail> stockDetailList,PayCharge payCharge, List<string> outerSkuIdList, List<string> outerIdList)//zxl
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    #region 更新StockProduct 和 StockItem
                    var stockProductResult = alading.StockProduct.Where(BuildWhereInExpression<StockProduct, string>(v => v.SkuOuterID, outerSkuIdList));
                    var stockItemResult = alading.StockItem.Where(BuildWhereInExpression<StockItem, string>(v => v.OuterID, outerIdList));
                    if (stockProductResult == null || stockItemResult == null)
                    {
                        return ReturnType.NotExisted;
                    }

                    foreach (StockProduct stockProduct in stockProductList)
                    {
                        //获取StockProduct的原始数据
                        StockProduct oldStockProduct = stockProductResult.Where(c => c.SkuOuterID == stockProduct.SkuOuterID).FirstOrDefault();
                        //获取StockItem的原始数据
                        StockItem oldStockItem = stockItemResult.Where(c => c.OuterID == stockProduct.OuterID).FirstOrDefault();
                        //获取StockProduct的相关原始数据的商品数量
                        oldStockProduct.SkuQuantity += stockProduct.SkuQuantity;
                        //获取oldStockItem的相关原始数据的商品数量
                        oldStockItem.TotalQuantity += stockProduct.SkuQuantity;
                    }
                    #endregion

                    #region 添加或更新StockHouseProduct
                    var stockHouseProResult=alading.StockHouseProduct.Where(BuildWhereInExpression<StockHouseProduct, string>(v => v.SkuOuterID, outerSkuIdList));
                    foreach(StockHouseProduct stockHousePro in stockHouseProList)
                    {
                        StockHouseProduct oldPro=stockHouseProResult.Where(c=>c.SkuOuterID==stockHousePro.SkuOuterID
                            && c.HouseCode==stockHousePro.HouseCode && c.LayoutCode==stockHousePro.LayoutCode).FirstOrDefault();
                        if(oldPro!=null)
                        {
                            oldPro.Num+=stockHousePro.Num;
                        }
                        else
                        {
                            stockHousePro.HouseProductCode = Guid.NewGuid().ToString();
                            alading.AddToStockHouseProduct(stockHousePro);
                        }
                    }
                    #endregion

                    #region 添加StockInOut
                    foreach (StockInOut stockInOut in stockInOutList)
                    {
                        alading.AddToStockInOut(stockInOut);
                    }
                    #endregion

                    #region 添加StockDetail
                    foreach (StockDetail stockDetail in stockDetailList)
                    {
                        alading.AddToStockDetail(stockDetail);
                    }
                    #endregion

                    #region 添加PayCharge
                    alading.AddToPayCharge(payCharge);
                    #endregion
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

        public ReturnType SkuOutIdIsOnly(List<string> skuOutIdList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (string skuOutId in skuOutIdList)
                    {
                        List<StockProduct> stockProductList = alading.StockProduct.Where(c => c.SkuOuterID == skuOutId).ToList();
                        if (stockProductList.Count == 0)
                        {
                            continue;
                        }
                        else
                        {
                            return ReturnType.PropertyExisted;
                        }
                    }
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

