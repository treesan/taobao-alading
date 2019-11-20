using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class PurchaseProductService
    {
        public static ReturnType AddPurchaseProduct(PurchaseProduct puchaseProduct)
        {
            return DataProviderClass.Instance().AddPurchaseProduct(puchaseProduct);
        }

        public static ReturnType AddPurchaseProduct(List<PurchaseProduct> purchaseProductList)
        {
            return DataProviderClass.Instance().AddPurchaseProduct(purchaseProductList);
        }

        public static List<PurchaseProduct> GetPurchasePorduct(string purchaseOrderCode)
        {
            return DataProviderClass.Instance().GetPurchaseProduct(purchaseOrderCode);
        }

        public static ReturnType RemovePurchaseProduct(string purchaseOrderCode)
        {
            List<PurchaseProduct> nowPurchaseProductList = new List<PurchaseProduct>();
            List<string> purchaseProductCodeList = new List<string>();
            nowPurchaseProductList = GetPurchasePorduct(purchaseOrderCode);
            ReturnType result = new ReturnType();
            if (nowPurchaseProductList != null)
            {
                foreach (var purchaseProduct in nowPurchaseProductList)
                {
                    result = DataProviderClass.Instance().RemovePurchaseProduct(purchaseProduct.PurchaseProductCode);
                    if (result != ReturnType.Success)
                        break;
                }
                return result;
            }
            else
                return ReturnType.Success;

        }

        public static ReturnType UpdatePurchaseProduct(List<PurchaseProduct> purchaseProductList)
        {
            ReturnType result = new ReturnType();
            if (purchaseProductList != null)
            {
                foreach (var purchaseProduct in purchaseProductList)
                {
                    result = DataProviderClass.Instance().UpdatePurchaseProduct(purchaseProduct);
                    if (result != ReturnType.Success)
                    break;
                }
                return result;
            }
            else
                return ReturnType.Success;
              
        }

        public static List<PurchaseProduct> GetPurchaseProductByCode(string purOrderCode)
        {
            return DataProviderClass.Instance().GetPurchaseProductByCode(purOrderCode);
        }
            
    }
}
