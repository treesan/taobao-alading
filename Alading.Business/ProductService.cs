using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class ProductService
    {

        public static ReturnType AddProduct(Product product)
        {
            return DataProviderClass.Instance().AddProduct(product);
        }

        public static ReturnType AddProduct(List<Product> productList)
        {
            return DataProviderClass.Instance().AddProduct(productList);
        }
    
        public static ReturnType RemoveAllProduct()
        {
            return DataProviderClass.Instance().RemoveAllProduct();
        }
    
        public static ReturnType RemoveProduct(Func<Product, bool> func)
        {
            return DataProviderClass.Instance().RemoveProduct(func);
        }

        public static ReturnType RemoveProduct(string productCode)
        {
            return DataProviderClass.Instance().RemoveProduct(productCode);
        }       
        
        /*
        public static ReturnType RemoveProduct(int productID)
        {
            return DataProviderClass.Instance().RemoveProduct(productID);
        }
        */

        public static ReturnType RemoveProduct(List<string> productCodeList)
        {
            return DataProviderClass.Instance().RemoveProduct(productCodeList);
        }        
        
        /*
        public static ReturnType RemoveProduct(List<int> productIDList)
        {
            return DataProviderClass.Instance().RemoveProduct(productIDList);
        }
        */
    
        public static ReturnType UpdateProduct(Product product)
        {
            return DataProviderClass.Instance().UpdateProduct(product);
        }

        public static ReturnType UpdateProduct(string productCode, Product product)
        {
            return DataProviderClass.Instance().UpdateProduct(productCode, product);
        }
        
        /*
        public static ReturnType UpdateProduct(int productID, Product product)
        {
            return DataProviderClass.Instance().UpdateProduct(productID, product);
        }
        */
    
        public static List<Product> GetAllProduct()
        {
            return DataProviderClass.Instance().GetAllProduct();
        }
    
        public static List<Product> GetProduct(Func<Product, bool> func)
        {
            return DataProviderClass.Instance().GetProduct(func);
        }

        public static Product GetProduct(string productCode)
        {
            return DataProviderClass.Instance().GetProduct(productCode);
        }
        
        /*
        public static Product GetProduct(int productID)
        {
            return DataProviderClass.Instance().GetProduct(productID);
        }
        */

        public static List<Product> GetProduct(List<string> productCodeList)
        {
            return DataProviderClass.Instance().GetProduct(productCodeList);
        }
        
        /*
        public static List<Product> GetProduct(List<int> productIDList)
        {
            return DataProviderClass.Instance().GetProduct(productIDList);
        }
        */
    
        public static List<Product> GetProduct(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetProduct(pageIndex, pageSize, out rowCount);
        }
        
        public static List<Product> GetProduct(Func<Product, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetProduct(func, pageIndex, pageSize, out rowCount);
        }
    }
}
