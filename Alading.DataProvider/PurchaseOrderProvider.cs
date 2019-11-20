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
     
        public ReturnType AddPurchaseOrder(PurchaseOrder purchaseorder)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToPurchaseOrder(purchaseorder);
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
                
        public ReturnType AddPurchaseOrder(List<PurchaseOrder> purchaseorderList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (PurchaseOrder purchaseorder in purchaseorderList)
                    {
                        alading.AddToPurchaseOrder(purchaseorder);
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
       
        public ReturnType RemoveAllPurchaseOrder()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<PurchaseOrder> list = alading.PurchaseOrder.ToList();
                    foreach (PurchaseOrder purchaseorder in list)
                    {
                        alading.DeleteObject(purchaseorder);
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
       
        public ReturnType RemovePurchaseOrder(Func<PurchaseOrder, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<PurchaseOrder> list = alading.PurchaseOrder.Where(func).ToList();
                    foreach (PurchaseOrder purchaseorder in list)
                    {
                        alading.DeleteObject(purchaseorder);
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

        public List<PurchaseOrder> GetPurchaseOrder(List<string> purchaseorderCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.PurchaseOrder.Where(BuildWhereInExpression<PurchaseOrder, int>(v => v.PurchaseOrderID, purchaseorderIDList));*/
                    var result = alading.PurchaseOrder.Where(BuildWhereInExpression<PurchaseOrder, string>(v => v.PurchaseOrderCode, purchaseorderCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public List<PurchaseOrder> GetStatusPurchaseOrder(int status)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var orderList = alading.PurchaseOrder.Where(p => p.OrderStatus == status);
                    return orderList == null ? null : orderList.ToList();
                 }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public ReturnType RemovePurchaseOrder(List<string> purchaseorderCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.PurchaseOrder.Where(BuildWhereInExpression<PurchaseOrder, int>(v => v.PurchaseOrderID, purchaseorderIDList));*/
                    var result = alading.PurchaseOrder.Where(BuildWhereInExpression<PurchaseOrder, string>(v => v.PurchaseOrderCode, purchaseorderCodeList));
                    foreach (PurchaseOrder s in result)
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

    
        public ReturnType RemovePurchaseOrder(string purchaseorderCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<PurchaseOrder> list = alading.PurchaseOrder.Where(p => p.PurchaseOrderID == purchaseorderID).ToList();*/
                    List<PurchaseOrder> list = alading.PurchaseOrder.Where(p => p.PurchaseOrderCode == purchaseorderCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        PurchaseOrder sy = list.First();
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
      
        public ReturnType UpdatePurchaseOrder(PurchaseOrder purchaseorder)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*PurchaseOrder result = alading.PurchaseOrder.Where(p => p.PurchaseOrderID == purchaseorder.PurchaseOrderID).FirstOrDefault();*/
                    PurchaseOrder result = alading.PurchaseOrder.Where(p => p.PurchaseOrderCode == purchaseorder.PurchaseOrderCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("PurchaseOrder", purchaseorder);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.OrderTime = purchaseorder.OrderTime;
                    
                        result.OrderStatus = purchaseorder.OrderStatus;
                    
                        result.PurchaserCode = purchaseorder.PurchaserCode;
                    
                        result.PurchaserName = purchaseorder.PurchaserName;
                    
                        result.SupplierCode = purchaseorder.SupplierCode;
                    
                        result.SupplierName = purchaseorder.SupplierName;
                    
                        result.ProductSkuOuterId = purchaseorder.ProductSkuOuterId;
                    
                        result.ProductName = purchaseorder.ProductName;
                    
                        result.ProductType = purchaseorder.ProductType;
                    
                        result.PurchasePrice = purchaseorder.PurchasePrice;
                    
                        result.PurchaseQuantity = purchaseorder.PurchaseQuantity;
                    
                        result.PurchaseTotalFee = purchaseorder.PurchaseTotalFee;
                    
                        result.PurchaseRemark = purchaseorder.PurchaseRemark;
			
                    */
                    #endregion  
					if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
                    }
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
       
        public ReturnType UpdatePurchaseOrder(string purchaseorderCode, PurchaseOrder purchaseorder)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.PurchaseOrder.Where(p => p.PurchaseOrderID == purchaseorderID).ToList();*/
                    var result = alading.PurchaseOrder.Where(p => p.PurchaseOrderCode == purchaseorderCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    PurchaseOrder ob = result.First();
                    ob.OrderTime = purchaseorder.OrderTime;
                    ob.OrderStatus = purchaseorder.OrderStatus;
                    ob.PurchaserCode = purchaseorder.PurchaserCode;
                    ob.PurchaserName = purchaseorder.PurchaserName;
                    ob.SupplierCode = purchaseorder.SupplierCode;
                    ob.SupplierName = purchaseorder.SupplierName;
                    ////ob.SkuOuterID = purchaseorder.SkuOuterID;
                    ////ob.ProductName = purchaseorder.ProductName;
                    ////ob.ProductType = purchaseorder.ProductType;
                    ////ob.PurchasePrice = purchaseorder.PurchasePrice;
                    ////ob.PurchaseQuantity = purchaseorder.PurchaseQuantity;
                    ////ob.PurchaseTotalFee = purchaseorder.PurchaseTotalFee;
                    ////ob.PurchaseRemark = purchaseorder.PurchaseRemark;
                    
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
     
        public List<PurchaseOrder> GetAllPurchaseOrder()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<PurchaseOrder> list = alading.PurchaseOrder.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<PurchaseOrder> GetPurchaseOrder(Func<PurchaseOrder, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<PurchaseOrder> list = alading.PurchaseOrder.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public PurchaseOrder GetPurchaseOrder(string purchaseorderCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<PurchaseOrder> list = alading.PurchaseOrder.Where(p => p.PurchaseOrderID == purchaseorderID).ToList();*/
                    List<PurchaseOrder> list = alading.PurchaseOrder.Where(p => p.PurchaseOrderCode == purchaseorderCode).ToList();
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

        #region   按时间进行查询
        public List<PurchaseOrder> GetPurchaseOrder(DateTime startTime, DateTime endTime)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var purOrder = from order in alading.PurchaseOrder 
                                   where order.OrderTime >= startTime && order.OrderTime <= endTime 
                                   select order;
                    return purOrder == null ? null : purOrder.ToList();

                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public List<PurchaseOrder> GetPurchaseOrder(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.PurchaseOrder orderby u.PurchaseOrderID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.PurchaseOrder.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<PurchaseOrder> GetPurchaseOrder(Func<PurchaseOrder, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<PurchaseOrder> list = alading.PurchaseOrder.Where(func).OrderByDescending(a=>a.PurchaseOrderID);
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

