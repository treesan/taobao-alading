using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IPurchaseOrder
    {       
        ReturnType AddPurchaseOrder(PurchaseOrder purchaseorder);
       
        ReturnType AddPurchaseOrder(List<PurchaseOrder> purchaseorderList);
        
        ReturnType RemoveAllPurchaseOrder();
       
        ReturnType RemovePurchaseOrder(Func<PurchaseOrder, bool> func);
              
        ReturnType RemovePurchaseOrder(string purchaseorderCode);
        
        ReturnType RemovePurchaseOrder(List<string> purchaseorderCodeList);
       
        ReturnType UpdatePurchaseOrder(PurchaseOrder purchaseorder);
       
        ReturnType UpdatePurchaseOrder(string purchaseorderCode,PurchaseOrder purchaseorder);
       
        List<PurchaseOrder> GetAllPurchaseOrder();
      
        List<PurchaseOrder> GetPurchaseOrder(Func<PurchaseOrder, bool> func);
      
        List<PurchaseOrder> GetPurchaseOrder(List<string> purchaseorderCodeList);
       
        List<PurchaseOrder> GetPurchaseOrder(int pageIndex, int pageSize, out int rowCount);
        
        List<PurchaseOrder> GetPurchaseOrder(Func<PurchaseOrder, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemovePurchaseOrder(int purchaseorderID);
        
        ReturnType RemovePurchaseOrder(List<int> purchaseorderIDList);
        
        ReturnType UpdatePurchaseOrder(int purchaseorderID,PurchaseOrder purchaseorder);
        
        List<PurchaseOrder> GetPurchaseOrder(List<int> purchaseorderIDList);
        */
    }
}
