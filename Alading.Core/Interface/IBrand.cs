using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IBrand
    {       
        ReturnType AddBrand(Brand brand);
       
        ReturnType AddBrand(List<Brand> brandList);
        
        ReturnType RemoveAllBrand();
       
        ReturnType RemoveBrand(Func<Brand, bool> func);
              
        ReturnType RemoveBrand(string brandCode);
        
        ReturnType RemoveBrand(List<string> brandCodeList);
       
        ReturnType UpdateBrand(Brand brand);
       
        ReturnType UpdateBrand(string brandCode,Brand brand);
       
        List<Brand> GetAllBrand();
      
        List<Brand> GetBrand(Func<Brand, bool> func);
      
        List<Brand> GetBrand(List<string> brandCodeList);
       
        List<Brand> GetBrand(int pageIndex, int pageSize, out int rowCount);
        
        List<Brand> GetBrand(Func<Brand, bool> func, int pageIndex, int pageSize, out int rowCount);

        ReturnType IsBrandExisted(string nick, string vid);
         
        /*        
        ReturnType RemoveBrand(int brandID);
        
        ReturnType RemoveBrand(List<int> brandIDList);
        
        ReturnType UpdateBrand(int brandID,Brand brand);
        
        List<Brand> GetBrand(List<int> brandIDList);
        */
    }
}
