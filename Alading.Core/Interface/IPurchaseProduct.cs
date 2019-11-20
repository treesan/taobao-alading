using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IPurchaseProduct
    {
        ReturnType AddPurchaseProduct(PurchaseProduct puchaseProduct);
        
        ReturnType AddPurchaseProduct(List<PurchaseProduct> purchaseProductList);

        List<PurchaseProduct> GetPurchaseProduct(string purchaseOrderCode);

        ReturnType UpdatePurchaseProduct(PurchaseProduct purchaseProduct);

        ReturnType RemovePurchaseProduct(string purchaseOrderCode);

    }
}
