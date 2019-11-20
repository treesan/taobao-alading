using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class SupplierService
    {

        public static ReturnType AddSupplier(Supplier supplier)
        {
            return DataProviderClass.Instance().AddSupplier(supplier);
        }

        public static ReturnType AddSupplier(List<Supplier> supplierList)
        {
            return DataProviderClass.Instance().AddSupplier(supplierList);
        }
    
        public static ReturnType RemoveAllSupplier()
        {
            return DataProviderClass.Instance().RemoveAllSupplier();
        }
    
        public static ReturnType RemoveSupplier(Func<Supplier, bool> func)
        {
            return DataProviderClass.Instance().RemoveSupplier(func);
        }
        
        public static ReturnType RemoveSupplier(string supplierCode)
        {
            return DataProviderClass.Instance().RemoveSupplier(supplierCode);
        }       
        
        /*
        public static ReturnType RemoveSupplier(int supplierID)
        {
            return DataProviderClass.Instance().RemoveSupplier(supplierID);
        }
        */
    
        public static ReturnType RemoveSupplier(List<string> supplierCodeList)
        {
            return DataProviderClass.Instance().RemoveSupplier(supplierCodeList);
        }        
        
        /*
        public static ReturnType RemoveSupplier(List<int> supplierIDList)
        {
            return DataProviderClass.Instance().RemoveSupplier(supplierIDList);
        }
        */
    
        public static ReturnType UpdateSupplier(Supplier supplier)
        {
            return DataProviderClass.Instance().UpdateSupplier(supplier);
        }
    
        public static ReturnType UpdateSupplier(string supplierCode, Supplier supplier)
        {
            return DataProviderClass.Instance().UpdateSupplier(supplierCode, supplier);
        }
        
        /*
        public static ReturnType UpdateSupplier(int supplierID, Supplier supplier)
        {
            return DataProviderClass.Instance().UpdateSupplier(supplierID, supplier);
        }
        */
    
        public static List<Supplier> GetAllSupplier()
        {
            return DataProviderClass.Instance().GetAllSupplier();
        }
    
        public static List<Supplier> GetSupplier(Func<Supplier, bool> func)
        {
            return DataProviderClass.Instance().GetSupplier(func);
        }
    
        public static Supplier GetSupplier(string supplierCode)
        {
            return DataProviderClass.Instance().GetSupplier(supplierCode);
        }

        public static Supplier GetSupplierByName(string supplierNmae)
        {
            return DataProviderClass.Instance().GetSupplierN(supplierNmae);
        }
        
        /*
        public static Supplier GetSupplier(int supplierID)
        {
            return DataProviderClass.Instance().GetSupplier(supplierID);
        }
        */
    
        public static List<Supplier> GetSupplier(List<string> supplierCodeList)
        {
            return DataProviderClass.Instance().GetSupplier(supplierCodeList);
        }
        
        /*
        public static List<Supplier> GetSupplier(List<int> supplierIDList)
        {
            return DataProviderClass.Instance().GetSupplier(supplierIDList);
        }
        */
    
        public static List<Supplier> GetSupplier(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetSupplier(pageIndex, pageSize, out rowCount);
        }
        
        public static List<Supplier> GetSupplier(Func<Supplier, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetSupplier(func, pageIndex, pageSize, out rowCount);
        }
    }
}
