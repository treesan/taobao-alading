using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IShipping
    {       
        ReturnType AddShipping(Shipping shipping);
       
        ReturnType AddShipping(List<Shipping> shippingList);
        
        ReturnType RemoveAllShipping();
       
        ReturnType RemoveShipping(Func<Shipping, bool> func);
              
        ReturnType RemoveShipping(string shippingCode);
        
        ReturnType RemoveShipping(List<string> shippingCodeList);
       
        ReturnType UpdateShipping(Shipping shipping);
       
        ReturnType UpdateShipping(string shippingCode,Shipping shipping);
       
        List<Shipping> GetAllShipping();
      
        List<Shipping> GetShipping(Func<Shipping, bool> func);
      
        List<Shipping> GetShipping(List<string> shippingCodeList);
       
        List<Shipping> GetShipping(int pageIndex, int pageSize, out int rowCount);
        
        List<Shipping> GetShipping(Func<Shipping, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveShipping(int shippingID);
        
        ReturnType RemoveShipping(List<int> shippingIDList);
        
        ReturnType UpdateShipping(int shippingID,Shipping shipping);
        
        List<Shipping> GetShipping(List<int> shippingIDList);
        */
    }
}
