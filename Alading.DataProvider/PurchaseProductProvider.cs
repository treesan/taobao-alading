using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Core.Interface;
using Alading.Core.Enum;
using Alading.Entity;
using System.Data.SqlClient;
using Alading.Core;
namespace Alading.DataProvider
{
    public partial class DataProviderClass : IAlading
    {
        public ReturnType AddPurchaseProduct(PurchaseProduct puchaseProduct)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToPurchaseProduct(puchaseProduct);
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

        public ReturnType AddPurchaseProduct(List<PurchaseProduct> purchaseProductList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (PurchaseProduct purchaseorder in purchaseProductList)
                    {
                        alading.AddToPurchaseProduct(purchaseorder);
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



        public List<PurchaseProduct> GetPurchaseProduct(string purchaseOrderCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<PurchaseOrder> list = alading.PurchaseOrder.Where(p => p.PurchaseOrderID == purchaseorderID).ToList();*/
                    List<PurchaseProduct> list = alading.PurchaseProduct.Where(p => p.PurchaseOrderCode == purchaseOrderCode).ToList();
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

        public List<PurchaseProduct> GetPurchaseProductByCode(string purOrderCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var purProductList = from purProduct in alading.PurchaseProduct
                                         where purProduct.PurchaseOrderCode == purOrderCode
                                         select purProduct;
                    return purProductList == null ? null : purProductList.ToList();

                   // var purProduct = alading.PurchaseProduct.Where(c => c.PurchaseOrderCode == purOrderCode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ReturnType RemovePurchaseProduct(string purchaseProductCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<PurchaseOrder> list = alading.PurchaseOrder.Where(p => p.PurchaseOrderID == purchaseorderID).ToList();*/
                    List<PurchaseProduct> list = alading.PurchaseProduct.Where(p => p.PurchaseProductCode == purchaseProductCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        PurchaseProduct sy = list.First();
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

        public ReturnType UpdatePurchaseProduct(PurchaseProduct purchaseProduct)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    PurchaseProduct oldPurchaseProduct = alading.PurchaseProduct.FirstOrDefault(cc => cc.PurchaseProductCode == purchaseProduct.PurchaseProductCode);
                    alading.Attach(oldPurchaseProduct);
                    alading.ApplyPropertyChanges("PurchaseProduct", purchaseProduct);
                    if (alading.SaveChanges() == 1)
                        return ReturnType.Success;
                    else
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
}
