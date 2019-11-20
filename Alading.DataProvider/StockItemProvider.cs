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
using System.Data;
using System.Data.EntityClient;
using Alading.Core;
namespace Alading.DataProvider
{
   
    public partial class DataProviderClass : IAlading
    {
        public bool IsOuterIDExisted(string outerID)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockItem> itemList = alading.StockItem.Where(i => i.OuterID == outerID).ToList();
                    if (itemList != null && itemList.Count > 0)
                        return true;
                    List<AssembleItem> assList = alading.AssembleItem.Where(i => i.OuterID == outerID).ToList();
                    if (assList != null && assList.Count > 0)
                        return true;

                    return false;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ItemCat> GetStockItemCid(Func<StockItem, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<string> cidList = new List<string>();
                    var q = from c in alading.StockItem.Where(func)                          
                            select c.Cid;
                    if (q != null && q.Count() > 0)
                    {
                        foreach (string cid in q.Distinct())
                        {
                            cidList.Add(cid);
                            GetItemCatParentCid(cidList, cid);
                        }
                    }
                    //List<int> cidIntList = new List<int>();
                    //foreach (string cid in cidList)
                    //{
                    //    cidIntList.Add(int.Parse(cid));
                    //}
                    return GetItemCat(cidList);

                }
            }
            catch (Exception ex)
            {
                return new List<ItemCat>();
            }
        }
        public List<ItemCat> GetAllStockItemCid()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                   List<string> cidList = new List<string>();
                   var q = from c in alading.StockItem
                           select c.Cid;
                   if (q != null && q.Count() > 0)
                   {
                       foreach (string cid in q.Distinct())
                       {
                           cidList.Add(cid);
                           GetItemCatParentCid(cidList, cid);
                       }                      
                   }                  
                   return GetItemCat(cidList);                    
                }
            }           
            catch (Exception ex)
            {
                return new List<ItemCat>();
            }
        }

        /// <summary>
        /// 循环获取父类ID
        /// </summary>
        /// <param name="cidList"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public List<string> GetItemCatParentCid(List<string> cidList,string cid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    ItemCat itemCat = alading.ItemCat.FirstOrDefault(i => i.cid == cid);
                    if (itemCat.parent_cid != "0")
                    {
                        cidList.Add(itemCat.parent_cid);
                        GetItemCatParentCid(cidList, itemCat.parent_cid);
                    }                   
                    return cidList;
                }
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        public ReturnType AddStockItemProducts(StockItem item, List<StockProduct> products, List<StockDetail> sdList, List<StockHouseProduct> shpList)
        {
            System.Data.Common.DbTransaction tran = null;
            using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            {
                try
                {
                    alading.Connection.Open();
                    tran = alading.Connection.BeginTransaction();
                    alading.AddToStockItem(item);
                    foreach (StockProduct product in products)
                    {
                        alading.AddToStockProduct(product);
                    }
                    foreach (StockDetail sd in sdList)
                    {
                        alading.AddToStockDetail(sd);
                    }
                    foreach (StockHouseProduct shp in shpList)
                    {
                        alading.AddToStockHouseProduct(shp);
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
     
        public ReturnType AddStockItem(StockItem stockitem)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToStockItem(stockitem);
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
                
        public ReturnType AddStockItem(List<StockItem> stockitemList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (StockItem stockitem in stockitemList)
                    {
                        alading.AddToStockItem(stockitem);
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
       
        public ReturnType RemoveAllStockItem()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockItem> list = alading.StockItem.ToList();
                    foreach (StockItem stockitem in list)
                    {
                        alading.DeleteObject(stockitem);
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
       
        public ReturnType RemoveStockItem(Func<StockItem, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockItem> list = alading.StockItem.Where(func).ToList();
                    foreach (StockItem stockitem in list)
                    {
                        alading.DeleteObject(stockitem);
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

        public List<StockItem> GetStockItem(List<string> stockitemCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.StockItem.Where(BuildWhereInExpression<StockItem, int>(v => v.StockItemID, stockitemIDList));*/
                    var result = alading.StockItem.Where(BuildWhereInExpression<StockItem, string>(v => v.StockItemCode, stockitemCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public List<StockItem> GetStockItem(List<string> cidList, bool IsCidTrueOrStockCid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                  
                    if (IsCidTrueOrStockCid == true)
                    {
                        var result = alading.StockItem.Where(BuildWhereInExpression<StockItem, string>(v => v.Cid, cidList));
                        return result.ToList();
                    }
                    else
                    {
                        var result = alading.StockItem.Where(BuildWhereInExpression<StockItem, string>(v => v.StockCid, cidList));
                        return result.ToList();
                    }                    
                }
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
        }

        public List<StockItem> GetStockItemLocal(List<string> outeridList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IQueryable<StockItem> stockitemList = alading.StockItem.Where(BuildWhereInExpression<StockItem,string>(p=>p.OuterID,outeridList)).AsQueryable();
                    List<StockItem> stockitemAllList = alading.StockItem.ToList();
                    List<StockItem> stockitemLocalList = stockitemAllList.Except(stockitemList).ToList();
                    return stockitemLocalList;
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveStockItem(List<string> stockitemCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.StockItem.Where(BuildWhereInExpression<StockItem, int>(v => v.StockItemID, stockitemIDList));*/
                    var result = alading.StockItem.Where(BuildWhereInExpression<StockItem, string>(v => v.StockItemCode, stockitemCodeList));
                    foreach (StockItem s in result)
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

        public ReturnType RemoveStockItem(string stockitemCode)
        {
            using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            {
                System.Data.Common.DbTransaction tran = null;
                try
                {
                    alading.Connection.Open();
                    tran = alading.Connection.BeginTransaction();

                    /*List<StockItem> list = alading.StockItem.Where(p => p.StockItemID == stockitemID).ToList();*/
                    List<StockItem> list = alading.StockItem.Where(p => p.StockItemCode == stockitemCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                    else
                    {
                        StockItem sy = list.First();

                        var productList = alading.StockProduct.Where(i => i.OuterID == sy.OuterID);
                        var skuList = from i in productList
                                      select i.SkuOuterID;
                        List<string> skuIDList = skuList.Distinct().ToList();
                        var houseProductList = alading.StockHouseProduct.Where(BuildWhereInExpression<StockHouseProduct, string>(v => v.SkuOuterID, skuIDList));

                        var detailList = alading.StockDetail.Where(BuildWhereInExpression<StockDetail, string>(v => v.ProductSkuOuterId, skuIDList));

                        //删除StockDetail
                        foreach (StockDetail detail in detailList)
                        {
                            alading.DeleteObject(detail);
                        }

                        //删除StockHouseProduct
                        foreach (StockHouseProduct product in houseProductList)
                        {
                            alading.DeleteObject(product);
                        }

                        //删除StockProduct
                        foreach (StockProduct product in productList)
                        {
                            alading.DeleteObject(product);
                        }

                        //删除StockItem
                        alading.DeleteObject(sy);

                        alading.SaveChanges();
                        tran.Commit();
                        return ReturnType.Success;
                    }
                }

                catch (Exception ex)
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
      
        public ReturnType UpdateStockItem(StockItem stockitem)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*StockItem result = alading.StockItem.Where(p => p.StockItemID == stockitem.StockItemID).FirstOrDefault();*/
                    StockItem result = alading.StockItem.Where(p => p.StockItemCode == stockitem.StockItemCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("StockItem", stockitem);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.OuterID = stockitem.OuterID;
                    
                        result.Name = stockitem.Name;
                    
                        result.SimpleName = stockitem.SimpleName;
                    
                        result.Cid = stockitem.Cid;
                    
                        result.CatName = stockitem.CatName;
                    
                        result.StockCid = stockitem.StockCid;
                    
                        result.StockCatName = stockitem.StockCatName;
                    
                        result.StockProps = stockitem.StockProps;
                    
                        result.KeyProps = stockitem.KeyProps;
                    
                        result.NotKeyProps = stockitem.NotKeyProps;
                    
                        result.SaleProps = stockitem.SaleProps;
                    
                        result.InputPids = stockitem.InputPids;
                    
                        result.InputStr = stockitem.InputStr;
                    
                        result.DefaultPrice = stockitem.DefaultPrice;
                    
                        result.StockDesc = stockitem.StockDesc;
                    
                        result.PicUrl = stockitem.PicUrl;
                    
                        result.StockItemImgs = stockitem.StockItemImgs;
                    
                        result.TotalCount = stockitem.TotalCount;
                    
                        result.BarCode = stockitem.BarCode;
                    
                        result.BarCodeUrl = stockitem.BarCodeUrl;
                    
                        result.StockItemRemark = stockitem.StockItemRemark;
                    
                        result.Created = stockitem.Created;
                    
                        result.Modified = stockitem.Modified;
			
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
       
        public ReturnType UpdateStockItem(string stockitemCode, StockItem stockitem)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.StockItem.Where(p => p.StockItemID == stockitemID).ToList();*/
                    var result = alading.StockItem.Where(p => p.StockItemCode == stockitemCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    StockItem ob = result.First();
                    if (ob.OuterID.ToUpper() != stockitem.OuterID.ToUpper())
                    {
                        StockItem stock = alading.StockItem.FirstOrDefault(p => p.OuterID == stockitem.OuterID);
                        if (stock != null)
                        {
                            return ReturnType.PropertyExisted;
                        }
                    }

                    ob.OuterID = stockitem.OuterID;
                    ob.UnitCode=stockitem.UnitCode;
                    ob.Specification=stockitem.Specification;
                    ob.Model=stockitem.Model;
                    ob.TaxName=stockitem.TaxName;
                    ob.Tax=stockitem.Tax;
                    ob.ProductID=stockitem.ProductID;
                    ob.Name = stockitem.Name;
                    ob.SimpleName = stockitem.SimpleName;
                    ob.Cid = stockitem.Cid;
                    ob.CatName = stockitem.CatName;
                    ob.StockCid = stockitem.StockCid;
                    ob.StockCatName = stockitem.StockCatName;
                    ob.StockProps = stockitem.StockProps;
                    ob.KeyProps = stockitem.KeyProps;
                    ob.NotKeyProps = stockitem.NotKeyProps;
                    ob.SaleProps = stockitem.SaleProps;
                    ob.HasSaleProps=stockitem.HasSaleProps;
                    ob.StockProps=stockitem.StockProps;
                    ob.InputPids = stockitem.InputPids;
                    ob.InputStr = stockitem.InputStr;            
                    ob.PicUrl = stockitem.PicUrl;
                    ob.StockItemImgs = stockitem.StockItemImgs;
                    ob.TotalQuantity = stockitem.TotalQuantity;
                    ob.IsConsignment = stockitem.IsConsignment;
                    ob.StockCheckUrl = stockitem.StockCheckUrl;
                    ob.Created = stockitem.Created;
                    ob.Modified = stockitem.Modified;
                    ob.StockItemRemark = stockitem.StockItemRemark;
                    ob.StockItemType=stockitem.StockItemType;
                    ob.Props=stockitem.Props;
                    ob.Property_Alias = stockitem.Property_Alias;
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

        public ReturnType UpdateStockItemProps(string stockitemCode, string props,string inputPids,string inputStr)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.StockItem.Where(p => p.StockItemID == stockitemID).ToList();*/
                    var result = alading.StockItem.Where(p => p.StockItemCode == stockitemCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                    StockItem ob = result.First();                    
                    ob.Props = props;
                    ob.InputPids = inputPids;
                    ob.InputStr = inputStr;
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

        public ReturnType UpdateStockItem(string stockitemCode, View_StockItemUnit stockitem)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.StockItem.Where(p => p.StockItemID == stockitemID).ToList();*/
                    var result = alading.StockItem.Where(p => p.StockItemCode == stockitemCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    StockItem ob = result.First();
                    ob.OuterID = stockitem.OuterID;
                    ob.UnitCode = stockitem.UnitCode;
                    ob.Specification = stockitem.Specification;
                    ob.Model = stockitem.Model;
                    ob.TaxName = stockitem.TaxName;
                    ob.Tax = stockitem.Tax;
                    ob.ProductID = stockitem.ProductID;
                    ob.Name = stockitem.Name;
                    ob.SimpleName = stockitem.SimpleName;
                    ob.Cid = stockitem.Cid;
                    ob.CatName = stockitem.CatName;
                    ob.StockCid = stockitem.StockCid;
                    ob.StockCatName = stockitem.StockCatName;
                    ob.StockProps = stockitem.StockProps;
                    ob.KeyProps = stockitem.KeyProps;
                    ob.NotKeyProps = stockitem.NotKeyProps;
                    ob.SaleProps = stockitem.SaleProps;
                    ob.HasSaleProps = stockitem.HasSaleProps;
                    ob.StockProps = stockitem.StockProps;
                    ob.InputPids = stockitem.InputPids;
                    ob.InputStr = stockitem.InputStr;
                    ob.PicUrl = stockitem.PicUrl;
                    ob.StockItemImgs = stockitem.StockItemImgs;
                    ob.TotalQuantity = stockitem.TotalQuantity;
                    ob.IsConsignment = stockitem.IsConsignment;
                    ob.StockCheckUrl = stockitem.StockCheckUrl;
                    ob.Created = stockitem.Created;
                    ob.Modified = stockitem.Modified;
                    ob.StockItemRemark = stockitem.StockItemRemark;
                    ob.StockItemType = stockitem.StockItemType;
                    ob.Props = stockitem.Props;
                    ob.Property_Alias = stockitem.Property_Alias;
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

        /// <summary>
        /// 更新StockItem的StockProduct
        /// </summary>
        /// <param name="stockitemCode"></param>
        /// <param name="productList"></param>
        /// <returns></returns>
        public ReturnType UpdateStockItem(string outerID, List<StockProduct> productList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var result = alading.StockProduct.Where(i => i.OuterID == outerID);
                    if (result != null && result.Count() > 0)
                    {
                        foreach (StockProduct product in result)
                        {
                            StockProduct src = productList.FirstOrDefault(i => i.StockProductID == product.StockProductID);
                            if (src != null)
                            {
                                product.OuterID = src.OuterID;
                                product.SkuOuterID = src.SkuOuterID;
                                //product.SkuProps = src.SkuProps;
                                //product.SkuProps_Str = src.SkuProps_Str;
                               // product.SkuQuantity = src.SkuQuantity;
                                product.MarketPrice = src.MarketPrice;
                                product.SkuPrice = src.SkuPrice;
                                product.MinSkuPrice = src.MinSkuPrice;
                                product.MaxSkuPrice = src.MaxSkuPrice;
                                product.CommissionPrice = src.CommissionPrice;
                                product.WholeSalePrice = src.WholeSalePrice;
                                product.LastStockPrice = src.LastStockPrice;
                                product.AvgStockPrice = src.AvgStockPrice;
                                product.IsUsingWarn = src.IsUsingWarn;
                                product.LowestNum = src.LowestNum;
                                product.HighestNum = src.HighestNum;
                                //product.OccupiedQuantity = src.OccupiedQuantity;
                                product.StockProductRemark = src.StockProductRemark;
                                product.ProductStatus = src.ProductStatus;
                                product.WarningCount = src.WarningCount;
                                //product.PropsAlias = src.PropsAlias;
                            }                           
                        }
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<StockItem> GetAllStockItem()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockItem> list = alading.StockItem.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<StockItem> GetStockItem(Func<StockItem, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockItem> list = alading.StockItem.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public StockItem GetStockItem(string outer_id)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<StockItem> list = alading.StockItem.Where(p => p.StockItemID == stockitemID).ToList();*/
                    List<StockItem> list = alading.StockItem.Where(p => p.OuterID == outer_id).ToList();
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
        
        public List<StockItem> GetStockItem(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.StockItem orderby u.StockID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.StockItem.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<StockItem> GetStockItem(Func<StockItem, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<StockItem> list = alading.StockItem.Where(func).OrderByDescending(a => a.StockID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_StockItemUnit> GetStockItems(Func<View_StockItemUnit, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<View_StockItemUnit> list = alading.View_StockItemUnit.Where(func).OrderByDescending(a => a.StockID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public StockItem GetStockItemByOutId(string outId)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<StockItem> list = alading.StockItem.Where(c => c.OuterID == outId).ToList();
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

        public ReturnType InitInput(StockItem stockItem, List<StockProduct> stockProductList, List<StockDetail> stockDetailList, List<StockHouseProduct> houseProductList)
        {
            System.Data.Common.DbTransaction tran = null;
            using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            {
                try
                {
                    alading.Connection.Open();
                    tran = alading.Connection.BeginTransaction();
                    alading.AddToStockItem(stockItem);
                    foreach (StockProduct sp in stockProductList)
                    {
                        alading.AddToStockProduct(sp);
                    }
                    foreach (StockDetail sd in stockDetailList)
                    {
                        alading.AddToStockDetail(sd);
                    }
                    foreach (StockHouseProduct sh in houseProductList)
                    {
                        alading.AddToStockHouseProduct(sh);
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

        public List<View_StockItemProduct> GetLocalStockItem()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    //DataTable table = new DataTable();
                    //string connectionString = ((EntityConnection)alading.Connection).StoreConnection.ConnectionString;
                    //SqlDataAdapter adapter = new SqlDataAdapter("SP_GetView_StockItemProducts", connectionString);
                    //adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    //adapter.Fill(table);
                    //return table;
                    List<View_StockItemProduct> stockItemProductList = alading.SP_GetView_StockItemProducts().ToList();
                    return stockItemProductList;

                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetWhereInOuterIds(List<string> outerIDList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<string> repeatList = alading.StockItem.Where(BuildWhereInExpression<StockItem, string>(v => v.OuterID, outerIDList)).Select(v => v.OuterID).ToList();
                    return repeatList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

