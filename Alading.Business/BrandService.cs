using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class BrandService
    {

        public static ReturnType AddBrand(Brand brand)
        {
            return DataProviderClass.Instance().AddBrand(brand);
        }

        public static ReturnType AddBrand(List<Brand> brandList)
        {
            return DataProviderClass.Instance().AddBrand(brandList);
        }
    
        public static ReturnType RemoveAllBrand()
        {
            return DataProviderClass.Instance().RemoveAllBrand();
        }
    
        public static ReturnType RemoveBrand(Func<Brand, bool> func)
        {
            return DataProviderClass.Instance().RemoveBrand(func);
        }
        
        public static ReturnType RemoveBrand(string brandCode)
        {
            return DataProviderClass.Instance().RemoveBrand(brandCode);
        }       
        
        /*
        public static ReturnType RemoveBrand(int brandID)
        {
            return DataProviderClass.Instance().RemoveBrand(brandID);
        }
        */
    
        public static ReturnType RemoveBrand(List<string> brandCodeList)
        {
            return DataProviderClass.Instance().RemoveBrand(brandCodeList);
        }        
        
        /// <summary>
        /// 判断品牌是否已存在，存在返回propertyExisted
        /// </summary>
         public static ReturnType IsBrandExisted(string nick, string vid)
         {
             return DataProviderClass.Instance().IsBrandExisted(nick, vid);
         }
        /*
        public static ReturnType RemoveBrand(List<int> brandIDList)
        {
            return DataProviderClass.Instance().RemoveBrand(brandIDList);
        }
        */
    
        public static ReturnType UpdateBrand(Brand brand)
        {
            return DataProviderClass.Instance().UpdateBrand(brand);
        }
    
        public static ReturnType UpdateBrand(string brandCode, Brand brand)
        {
            return DataProviderClass.Instance().UpdateBrand(brandCode, brand);
        }
        
        /*
        public static ReturnType UpdateBrand(int brandID, Brand brand)
        {
            return DataProviderClass.Instance().UpdateBrand(brandID, brand);
        }
        */
    
        public static List<Brand> GetAllBrand()
        {
            return DataProviderClass.Instance().GetAllBrand();
        }
    
        public static List<Brand> GetBrand(Func<Brand, bool> func)
        {
            return DataProviderClass.Instance().GetBrand(func);
        }
    
        public static Brand GetBrand(string brandCode)
        {
            return DataProviderClass.Instance().GetBrand(brandCode);
        }
        
        /*
        public static Brand GetBrand(int brandID)
        {
            return DataProviderClass.Instance().GetBrand(brandID);
        }
        */
    
        public static List<Brand> GetBrand(List<string> brandCodeList)
        {
            return DataProviderClass.Instance().GetBrand(brandCodeList);
        }
        
        /*
        public static List<Brand> GetBrand(List<int> brandIDList)
        {
            return DataProviderClass.Instance().GetBrand(brandIDList);
        }
        */
    
        public static List<Brand> GetBrand(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetBrand(pageIndex, pageSize, out rowCount);
        }
        
        public static List<Brand> GetBrand(Func<Brand, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetBrand(func, pageIndex, pageSize, out rowCount);
        }
    }
}
