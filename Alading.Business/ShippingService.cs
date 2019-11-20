using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class ShippingService
    {

        public static ReturnType AddShipping(Shipping shipping)
        {
            return DataProviderClass.Instance().AddShipping(shipping);
        }

        public static ReturnType AddShipping(List<Shipping> shippingList)
        {
            return DataProviderClass.Instance().AddShipping(shippingList);
        }
    
        public static ReturnType RemoveAllShipping()
        {
            return DataProviderClass.Instance().RemoveAllShipping();
        }
    
        public static ReturnType RemoveShipping(Func<Shipping, bool> func)
        {
            return DataProviderClass.Instance().RemoveShipping(func);
        }
        
        public static ReturnType RemoveShipping(string shippingCode)
        {
            return DataProviderClass.Instance().RemoveShipping(shippingCode);
        }       
        
        /*
        public static ReturnType RemoveShipping(int shippingID)
        {
            return DataProviderClass.Instance().RemoveShipping(shippingID);
        }
        */
    
        public static ReturnType RemoveShipping(List<string> shippingCodeList)
        {
            return DataProviderClass.Instance().RemoveShipping(shippingCodeList);
        }        
        
        /*
        public static ReturnType RemoveShipping(List<int> shippingIDList)
        {
            return DataProviderClass.Instance().RemoveShipping(shippingIDList);
        }
        */
    
        public static ReturnType UpdateShipping(Shipping shipping)
        {
            return DataProviderClass.Instance().UpdateShipping(shipping);
        }
    
        public static ReturnType UpdateShipping(string shippingCode, Shipping shipping)
        {
            return DataProviderClass.Instance().UpdateShipping(shippingCode, shipping);
        }
        
        /*
        public static ReturnType UpdateShipping(int shippingID, Shipping shipping)
        {
            return DataProviderClass.Instance().UpdateShipping(shippingID, shipping);
        }
        */
    
        public static List<Shipping> GetAllShipping()
        {
            return DataProviderClass.Instance().GetAllShipping();
        }
    
        public static List<Shipping> GetShipping(Func<Shipping, bool> func)
        {
            return DataProviderClass.Instance().GetShipping(func);
        }
    
        public static Shipping GetShipping(string shippingCode)
        {
            return DataProviderClass.Instance().GetShipping(shippingCode);
        }
        
        /*
        public static Shipping GetShipping(int shippingID)
        {
            return DataProviderClass.Instance().GetShipping(shippingID);
        }
        */
    
        public static List<Shipping> GetShipping(List<string> shippingCodeList)
        {
            return DataProviderClass.Instance().GetShipping(shippingCodeList);
        }
        
        /*
        public static List<Shipping> GetShipping(List<int> shippingIDList)
        {
            return DataProviderClass.Instance().GetShipping(shippingIDList);
        }
        */
    
        public static List<Shipping> GetShipping(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetShipping(pageIndex, pageSize, out rowCount);
        }
        
        public static List<Shipping> GetShipping(Func<Shipping, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetShipping(func, pageIndex, pageSize, out rowCount);
        }
    }
}
