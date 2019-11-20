using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class PurchaseOrderService
    {

        public static ReturnType AddPurchaseOrder(PurchaseOrder purchaseorder)
        {
            return DataProviderClass.Instance().AddPurchaseOrder(purchaseorder);
        }

        public static ReturnType AddPurchaseOrder(List<PurchaseOrder> purchaseorderList)
        {
            return DataProviderClass.Instance().AddPurchaseOrder(purchaseorderList);
        }
    
        public static ReturnType RemoveAllPurchaseOrder()
        {
            return DataProviderClass.Instance().RemoveAllPurchaseOrder();
        }
    
        public static ReturnType RemovePurchaseOrder(Func<PurchaseOrder, bool> func)
        {
            return DataProviderClass.Instance().RemovePurchaseOrder(func);
        }
        
        public static ReturnType RemovePurchaseOrder(string purchaseorderCode)
        {
            return DataProviderClass.Instance().RemovePurchaseOrder(purchaseorderCode);
        }       
        
        /*
        public static ReturnType RemovePurchaseOrder(int purchaseorderID)
        {
            return DataProviderClass.Instance().RemovePurchaseOrder(purchaseorderID);
        }
        */
    
        public static ReturnType RemovePurchaseOrder(List<string> purchaseorderCodeList)
        {
            return DataProviderClass.Instance().RemovePurchaseOrder(purchaseorderCodeList);
        }        
        
        /*
        public static ReturnType RemovePurchaseOrder(List<int> purchaseorderIDList)
        {
            return DataProviderClass.Instance().RemovePurchaseOrder(purchaseorderIDList);
        }
        */
    
        public static ReturnType UpdatePurchaseOrder(PurchaseOrder purchaseorder)
        {
            return DataProviderClass.Instance().UpdatePurchaseOrder(purchaseorder);
        }
    
        public static ReturnType UpdatePurchaseOrder(string purchaseorderCode, PurchaseOrder purchaseorder)
        {
            return DataProviderClass.Instance().UpdatePurchaseOrder(purchaseorderCode, purchaseorder);
        }
        
        /*
        public static ReturnType UpdatePurchaseOrder(int purchaseorderID, PurchaseOrder purchaseorder)
        {
            return DataProviderClass.Instance().UpdatePurchaseOrder(purchaseorderID, purchaseorder);
        }
        */
    
        public static List<PurchaseOrder> GetAllPurchaseOrder()
        {
            return DataProviderClass.Instance().GetAllPurchaseOrder();
        }
    
        public static List<PurchaseOrder> GetPurchaseOrder(Func<PurchaseOrder, bool> func)
        {
            return DataProviderClass.Instance().GetPurchaseOrder(func);
        }
    
        public static PurchaseOrder GetPurchaseOrder(string purchaseorderCode)
        {
            return DataProviderClass.Instance().GetPurchaseOrder(purchaseorderCode);
        }
        
        /*
        public static PurchaseOrder GetPurchaseOrder(int purchaseorderID)
        {
            return DataProviderClass.Instance().GetPurchaseOrder(purchaseorderID);
        }
        */
    
        public static List<PurchaseOrder> GetPurchaseOrder(List<string> purchaseorderCodeList)
        {
            return DataProviderClass.Instance().GetPurchaseOrder(purchaseorderCodeList);
        }
        
        /*
        public static List<PurchaseOrder> GetPurchaseOrder(List<int> purchaseorderIDList)
        {
            return DataProviderClass.Instance().GetPurchaseOrder(purchaseorderIDList);
        }
        */

        public static List<PurchaseOrder> GetPurchaseOrder(DateTime startTime, DateTime endTime)
        {
            return DataProviderClass.Instance().GetPurchaseOrder(startTime, endTime);
        }

        public static List<PurchaseOrder> GetStatusPurchaseOrder(int status)
        {
            return DataProviderClass.Instance().GetStatusPurchaseOrder(status);
        }
    
        public static List<PurchaseOrder> GetPurchaseOrder(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetPurchaseOrder(pageIndex, pageSize, out rowCount);
        }
        
        public static List<PurchaseOrder> GetPurchaseOrder(Func<PurchaseOrder, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetPurchaseOrder(func, pageIndex, pageSize, out rowCount);
        }
    }
}
