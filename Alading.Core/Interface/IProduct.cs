using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IProduct
    {       
        ReturnType AddProduct(Product product);
       
        ReturnType AddProduct(List<Product> productList);
        
        ReturnType RemoveAllProduct();
       
        ReturnType RemoveProduct(Func<Product, bool> func);
              
        ReturnType RemoveProduct(string productid);

        ReturnType RemoveProduct(List<string> productidList);
       
        ReturnType UpdateProduct(Product product);

        ReturnType UpdateProduct(string productid, Product product);
       
        List<Product> GetAllProduct();
      
        List<Product> GetProduct(Func<Product, bool> func);

        List<Product> GetProduct(List<string> productidList);
       
        List<Product> GetProduct(int pageIndex, int pageSize, out int rowCount);
        
        List<Product> GetProduct(Func<Product, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveProduct(int productID);
        
        ReturnType RemoveProduct(List<int> productIDList);
        
        ReturnType UpdateProduct(int productID,Product product);
        
        List<Product> GetProduct(List<int> productIDList);
        */
    }
}
