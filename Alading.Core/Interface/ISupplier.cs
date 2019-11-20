using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface ISupplier
    {       
        ReturnType AddSupplier(Supplier supplier);
       
        ReturnType AddSupplier(List<Supplier> supplierList);
        
        ReturnType RemoveAllSupplier();
       
        ReturnType RemoveSupplier(Func<Supplier, bool> func);
              
        ReturnType RemoveSupplier(string supplierCode);
        
        ReturnType RemoveSupplier(List<string> supplierCodeList);
       
        ReturnType UpdateSupplier(Supplier supplier);
       
        ReturnType UpdateSupplier(string supplierCode,Supplier supplier);
       
        List<Supplier> GetAllSupplier();
      
        List<Supplier> GetSupplier(Func<Supplier, bool> func);
      
        List<Supplier> GetSupplier(List<string> supplierCodeList);
       
        List<Supplier> GetSupplier(int pageIndex, int pageSize, out int rowCount);
        
        List<Supplier> GetSupplier(Func<Supplier, bool> func, int pageIndex, int pageSize, out int rowCount);

        Supplier GetSupplierN(string supplierName);
         
        /*        
        ReturnType RemoveSupplier(int supplierID);
        
        ReturnType RemoveSupplier(List<int> supplierIDList);
        
        ReturnType UpdateSupplier(int supplierID,Supplier supplier);
        
        List<Supplier> GetSupplier(List<int> supplierIDList);
        */
    }
}
