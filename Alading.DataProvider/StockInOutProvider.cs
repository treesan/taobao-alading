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
        public ReturnType RemoveStockInOutDetail(string stockInOutCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var resultInOut = alading.StockInOut.Where(i => i.InOutCode == stockInOutCode);
                    var resultDetails = alading.StockDetail.Where(v => v.InOutCode==stockInOutCode);
                    if (resultInOut != null && resultInOut.Count() > 0)
                    {
                        foreach (StockInOut inOut in resultInOut)
                        {
                            alading.DeleteObject(inOut);
                        }
                    }
                    foreach (StockDetail detail in resultDetails)
                    {
                        alading.DeleteObject(detail);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }

            }
            catch (Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

        public List<View_StockDetailInOut> GetStockDetailInOut(Func<View_StockDetailInOut, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_StockDetailInOut> list = alading.View_StockDetailInOut.Where(func).ToList();

                    return list;
                }

            }
            catch (Exception ex)
            {
                return new List<View_StockDetailInOut>();
            }
        }
        public ReturnType AddStockInOutDetail(List<Alading.Entity.StockInOut> stockInOutList, List<StockDetail> stockDetailList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    StockInOut stock = alading.StockInOut.FirstOrDefault(i => i.InOutCode == stockInOutList[0].InOutCode);
                    if (stock == null)
                        return ReturnType.PropertyExisted;
                    foreach (StockInOut stockinout in stockInOutList)
                    {
                        alading.AddToStockInOut(stockinout);
                    }
               
                    foreach (StockDetail stockDetail in stockDetailList)
                    {
                        StockHouseProduct stockProduct = alading.StockHouseProduct.FirstOrDefault(i => i.SkuOuterID == stockDetail.ProductSkuOuterId && i.HouseCode == stockDetail.StockHouseCode && i.LayoutCode == stockDetail.StockLayOutCode);
                        if (stockProduct != null)
                        {
                            if (stockDetail.DetailType == (int)DetailType.AllocateIn)
                            {
                                stockProduct.Num += stockDetail.Quantity;
                            }
                            else if (stockDetail.DetailType == (int)DetailType.AllocateOut)
                            {
                                stockProduct.Num -= stockDetail.Quantity;
                            }
                        }
                        else
                        {
                            //从一个仓库调拨到另一个仓库中,此仓库没有此product
                            StockHouseProduct newProduct = new StockHouseProduct();
                            newProduct.HouseName = stockDetail.HouseName;
                            newProduct.HouseCode = stockDetail.StockHouseCode;
                            newProduct.HouseProductCode = Guid.NewGuid().ToString();
                            newProduct.LayoutCode = stockDetail.StockLayOutCode;
                            newProduct.LayoutName = stockDetail.LayoutName;
                            newProduct.Num = stockDetail.Quantity;
                            newProduct.SkuOuterID = stockDetail.ProductSkuOuterId;                          
                            alading.AddToStockHouseProduct(newProduct);
                        }
                        alading.AddToStockDetail(stockDetail);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

        public ReturnType AddStockInOut(StockInOut stockinout)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToStockInOut(stockinout);
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
                
        public ReturnType AddStockInOut(List<StockInOut> stockinoutList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (StockInOut stockinout in stockinoutList)
                    {
                        alading.AddToStockInOut(stockinout);
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
       
        public ReturnType RemoveAllStockInOut()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockInOut> list = alading.StockInOut.ToList();
                    foreach (StockInOut stockinout in list)
                    {
                        alading.DeleteObject(stockinout);
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
       
        public ReturnType RemoveStockInOut(Func<StockInOut, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockInOut> list = alading.StockInOut.Where(func).ToList();
                    foreach (StockInOut stockinout in list)
                    {
                        alading.DeleteObject(stockinout);
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

        public List<StockInOut> GetStockInOut(List<string> stockinoutCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.StockInOut.Where(BuildWhereInExpression<StockInOut, int>(v => v.StockInOutID, stockinoutIDList));*/
                    var result = alading.StockInOut.Where(BuildWhereInExpression<StockInOut, string>(v => v.InOutCode, stockinoutCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveStockInOut(List<string> stockinoutCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.StockInOut.Where(BuildWhereInExpression<StockInOut, int>(v => v.StockInOutID, stockinoutIDList));*/
                    var result = alading.StockInOut.Where(BuildWhereInExpression<StockInOut, string>(v => v.InOutCode, stockinoutCodeList));
                    foreach (StockInOut s in result)
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

    
        public ReturnType RemoveStockInOut(string stockinoutCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<StockInOut> list = alading.StockInOut.Where(p => p.StockInOutID == stockinoutID).ToList();*/
                    List<StockInOut> list = alading.StockInOut.Where(p => p.InOutCode == stockinoutCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        StockInOut sy = list.First();
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
      
        public ReturnType UpdateStockInOut(StockInOut stockinout)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*StockInOut result = alading.StockInOut.Where(p => p.StockInOutID == stockinout.StockInOutID).FirstOrDefault();*/
                    StockInOut result = alading.StockInOut.Where(p => p.InOutCode == stockinout.InOutCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("StockInOut", stockinout);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.InOutCode = stockinout.InOutCode;
                    
                        result.InOutTime = stockinout.InOutTime;
                    
                        result.OperatorCode = stockinout.OperatorCode;
                    
                        result.OperatorName = stockinout.OperatorName;
                    
                        result.InOutType = stockinout.InOutType;
                    
                        result.TradeOrderCode = stockinout.TradeOrderCode;
                    
                        result.DiscountFee = stockinout.DiscountFee;
                    
                        result.DueFee = stockinout.DueFee;
                    
                        result.InOutStatus = stockinout.InOutStatus;
                    
                        result.IsSettled = stockinout.IsSettled;
			
                    */
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
       
        public ReturnType UpdateStockInOut(string stockinoutCode, StockInOut stockinout)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.StockInOut.Where(p => p.StockInOutID == stockinoutID).ToList();*/
                    var result = alading.StockInOut.Where(p => p.InOutCode == stockinoutCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    StockInOut ob = result.First();
                    ob.InOutCode = stockinout.InOutCode;
                    ob.InOutTime = stockinout.InOutTime;
                    ob.OperatorCode = stockinout.OperatorCode;
                    ob.OperatorName = stockinout.OperatorName;
                    ob.InOutType = stockinout.InOutType;
                    ob.TradeOrderCode = stockinout.TradeOrderCode;
                    ob.DiscountFee = stockinout.DiscountFee;
                    ob.DueFee = stockinout.DueFee;
                    ob.InOutStatus = stockinout.InOutStatus;
                    ob.IsSettled = stockinout.IsSettled;
                    
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
     
        public List<StockInOut> GetAllStockInOut()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockInOut> list = alading.StockInOut.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<StockInOut> GetStockInOut(Func<StockInOut, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockInOut> list = alading.StockInOut.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public StockInOut GetStockInOut(string stockinoutCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<StockInOut> list = alading.StockInOut.Where(p => p.StockInOutID == stockinoutID).ToList();*/
                    List<StockInOut> list = alading.StockInOut.Where(p => p.InOutCode == stockinoutCode).ToList();
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
        
        public List<StockInOut> GetStockInOut(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.StockInOut orderby u.StockInOutID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.StockInOut.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<StockInOut> GetStockInOut(Func<StockInOut, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<StockInOut> list = alading.StockInOut.Where(func).OrderByDescending(a=>a.StockInOutID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_InOutDetailProduct> GetViewInOutDetailProduct(Func<View_InOutDetailProduct, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<View_InOutDetailProduct> list = alading.View_InOutDetailProduct.Where(func).OrderByDescending(a => a.StockDetailID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType AddInOutAndDetails(StockInOut stockInOut,PayCharge payChage, List<StockDetail> sdList, List<StockHouseProduct> shpList, List<View_StockItemProduct> vsipList)
        {
            System.Data.Common.DbTransaction tran = null;
            using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            {
                try
                {
                    alading.Connection.Open();
                    tran = alading.Connection.BeginTransaction();
                    alading.AddToPayCharge(payChage);
                    alading.AddToStockInOut(stockInOut);
                    foreach (StockDetail sd in sdList)
                    {
                        alading.AddToStockDetail(sd);
                    }
                    foreach (StockHouseProduct shp in shpList)
                    {
                        StockHouseProduct tmpshp = alading.StockHouseProduct.FirstOrDefault(c => c.HouseProductCode == shp.HouseProductCode);
                        if (tmpshp != null)
                        {
                            tmpshp.Num = shp.Num;
                        }
                        else
                        {
                            alading.AddToStockHouseProduct(shp);
                        }
                    }
                    foreach (View_StockItemProduct vsip in vsipList)
                    {
                        StockItem stockItem = alading.StockItem.FirstOrDefault(s => s.OuterID == vsip.OuterID);
                        StockProduct stockProduct = alading.StockProduct.FirstOrDefault(s => s.SkuOuterID == vsip.SkuOuterID);
                        stockItem.TotalQuantity = Math.Round(vsip.TotalQuantity,3);
                        stockProduct.SkuQuantity = vsip.SkuQuantity;
                        stockProduct.LastStockPrice = vsip.LastStockPrice;
                        stockProduct.AvgStockPrice = vsip.AvgStockPrice;
                    }
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

        #region View_InOutDetailProduct

        public List<View_InOutDetailProduct> GetAllView_InOutDetailProducts()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    return alading.View_InOutDetailProduct.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_InOutDetailProduct> GetView_InOutDetailProduct(Func<View_InOutDetailProduct, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    return alading.View_InOutDetailProduct.Where(func).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType RemoveInOutAndDetails(string inOutCode)
        {
            System.Data.Common.DbTransaction tran = null;
            using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            {
                try
                {
                    alading.Connection.Open();
                    tran = alading.Connection.BeginTransaction();
                    List<StockInOut> list = alading.StockInOut.Where(p => p.InOutCode == inOutCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        StockInOut sy = list.First();
                        foreach (StockDetail sd in alading.StockDetail.Where(c => c.InOutCode == inOutCode))
                        {
                            alading.DeleteObject(sd);
                        }
                        alading.DeleteObject(sy);
                        alading.SaveChanges();                        
                    }
                    alading.SaveChanges();
                    tran.Commit();
                    return ReturnType.Success;
                }
                catch (SqlException sex)
                {
                    return ReturnType.ConnFailed;
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

        /// <summary>
        /// 标记为配货并出库
        /// </summary>
        /// <param name="houseProductList">数量即为出库数量</param>
        /// <param name="customtid">交易的唯一标识</param>
        /// <returns></returns>
        public ReturnType AllocationAndOutput(List<StockHouseProduct> houseProductList,string customtid)
        {
            System.Data.Common.DbTransaction tran = null;
            using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            {
                try
                {
                    alading.Connection.Open();
                    tran = alading.Connection.BeginTransaction();
                    StockInOut stockInOut = new StockInOut();
                    stockInOut.InOutCode = System.Guid.NewGuid().ToString();//GUID??

                    foreach (StockHouseProduct shp in houseProductList)
                    {
                        StockProduct stockProduct = alading.StockProduct.FirstOrDefault(c => c.SkuOuterID == shp.SkuOuterID);
                        stockProduct.SkuQuantity -= shp.Num;

                        StockItem item = alading.StockItem.FirstOrDefault(c => c.OuterID == stockProduct.OuterID);
                        item.TotalQuantity -= shp.Num;

                        StockHouseProduct stockHouseProduct = alading.StockHouseProduct.FirstOrDefault(c => c.SkuOuterID == shp.SkuOuterID && c.HouseCode == shp.HouseCode && c.LayoutCode == shp.LayoutCode);
                        stockHouseProduct.Num -= shp.Num;

                        #region StockDetail

                        StockDetail stockDetail = new StockDetail();
                        stockDetail.DetailRemark = "销售自动出库";
                        stockDetail.DetailType = (int)DetailType.TaobaoSaleOut;
                        stockDetail.DurabilityDate = DateTime.Now;
                        stockDetail.HouseName = stockHouseProduct.HouseName;
                        stockDetail.InOutCode = stockInOut.InOutCode;//////////
                        stockDetail.LayoutName = stockHouseProduct.LayoutCode;
                        stockDetail.Price = float.Parse(stockProduct.SkuPrice.ToString());
                        stockDetail.ProductSkuOuterId = stockProduct.SkuOuterID;
                        stockDetail.Quantity = shp.Num;//出库数量
                        stockDetail.SearchText = string.Empty;//搜索字段
                        stockDetail.StockDetailCode = System.Guid.NewGuid().ToString();//
                        stockDetail.StockHouseCode = stockHouseProduct.HouseCode;
                        stockDetail.StockLayOutCode = stockHouseProduct.LayoutCode;
                        stockDetail.Tax = item.Tax;//税率
                        stockDetail.TotalFee=float.Parse((stockProduct.SkuPrice * shp.Num).ToString());
                        alading.AddToStockDetail(stockDetail);

                        #endregion

                        stockInOut.DiscountFee += stockDetail.TotalFee;//这么算可对？
                    }

                    Trade trade = alading.Trade.FirstOrDefault(c => c.CustomTid == customtid);
                    trade.LocalStatus = LocalTradeStatus.AssortedNotSent;
                    
                    stockInOut.AmountTax = 0.0f;//税率怎么算？                   
                    stockInOut.DueFee = stockInOut.DiscountFee;//??
                    stockInOut.FreightCode = string.Empty;
                    stockInOut.FreightCompany = string.Empty;
                    stockInOut.HouseCodeIn = string.Empty;
                    stockInOut.HouseCodeOut = string.Empty;
                    stockInOut.HouseNameIn = string.Empty;
                    stockInOut.HouseNameOut = string.Empty;
                    stockInOut.IncomeTime = DateTime.Now;                    
                    stockInOut.InOutStatus = (int)InOutStatus.AllSend;
                    stockInOut.InOutTime = DateTime.Now;
                    stockInOut.InOutType = (int)InOutType.SaleOut;
                    stockInOut.IsSettled = true;
                    stockInOut.OperatorCode = string.Empty;//操作人编码？？？
                    stockInOut.OperatorName = string.Empty;//操作人姓名？？
                    stockInOut.PayTerm = 0; ;
                    stockInOut.PayThisTime = stockInOut.DueFee;
                    stockInOut.PayType = (int)PayType.ALIPAY;
                    stockInOut.SearchText = string.Empty;//搜索字段
                    stockInOut.TradeOrderCode = trade.CustomTid;
                    stockInOut.TransportCode=string.Empty;
                    alading.AddToStockInOut(stockInOut);

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
    }
}

