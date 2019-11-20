using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class SellerCatService
    {
        /// <summary>
        /// 存在更新，不存在添加
        /// </summary>
        /// <param name="sellercat"></param>
        /// <returns></returns>
        public static ReturnType AddSellerCat(SellerCat sellercat)
        {
            return DataProviderClass.Instance().AddSellerCat(sellercat);
        }

        public static ReturnType AddSellerCat(List<SellerCat> sellercatList)
        {
            return DataProviderClass.Instance().AddSellerCat(sellercatList);
        }
    
        public static ReturnType RemoveAllSellerCat()
        {
            return DataProviderClass.Instance().RemoveAllSellerCat();
        }
    
        public static ReturnType RemoveSellerCat(Func<SellerCat, bool> func)
        {
            return DataProviderClass.Instance().RemoveSellerCat(func);
        }
        
        public static ReturnType RemoveSellerCat(string sellercatCode)
        {
            return DataProviderClass.Instance().RemoveSellerCat(sellercatCode);
        }       
        
        /*
        public static ReturnType RemoveSellerCat(int sellercatID)
        {
            return DataProviderClass.Instance().RemoveSellerCat(sellercatID);
        }
        */
    
        public static ReturnType RemoveSellerCat(List<string> sellercatCodeList)
        {
            return DataProviderClass.Instance().RemoveSellerCat(sellercatCodeList);
        }        
        
        /*
        public static ReturnType RemoveSellerCat(List<int> sellercatIDList)
        {
            return DataProviderClass.Instance().RemoveSellerCat(sellercatIDList);
        }
        */
    
        public static ReturnType UpdateSellerCat(SellerCat sellercat)
        {
            return DataProviderClass.Instance().UpdateSellerCat(sellercat);
        }
    
        public static ReturnType UpdateSellerCat(string sellercatCode, SellerCat sellercat)
        {
            return DataProviderClass.Instance().UpdateSellerCat(sellercatCode, sellercat);
        }
        
        /*
        public static ReturnType UpdateSellerCat(int sellercatID, SellerCat sellercat)
        {
            return DataProviderClass.Instance().UpdateSellerCat(sellercatID, sellercat);
        }
        */

        public static List<SellerCat> GetSellerCatByCid(string[] SellerCatArray)
        {
            return DataProviderClass.Instance().GetSellerCatByCid(SellerCatArray);
        }
    
        public static List<SellerCat> GetAllSellerCat()
        {
            return DataProviderClass.Instance().GetAllSellerCat();
        }
    
        public static List<SellerCat> GetSellerCat(Func<SellerCat, bool> func)
        {
            return DataProviderClass.Instance().GetSellerCat(func);
        }

        /*
        public static SellerCat GetSellerCat(string sellercatCode)
        {
            return DataProviderClass.Instance().GetSellerCat(sellercatCode);
        }
        */

        public static List<SellerCat> GetSellerCatOrdered(string sellerNick)
        {
            return DataProviderClass.Instance().GetSellerCatOrdered(sellerNick);
        }
        
        /*
        public static SellerCat GetSellerCat(int sellercatID)
        {
            return DataProviderClass.Instance().GetSellerCat(sellercatID);
        }
        */
    
        public static List<SellerCat> GetSellerCat(List<string> sellercatCodeList)
        {
            return DataProviderClass.Instance().GetSellerCat(sellercatCodeList);
        }
        
        /*
        public static List<SellerCat> GetSellerCat(List<int> sellercatIDList)
        {
            return DataProviderClass.Instance().GetSellerCat(sellercatIDList);
        }
        */
    
        public static List<SellerCat> GetSellerCat(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetSellerCat(pageIndex, pageSize, out rowCount);
        }
        
        public static List<SellerCat> GetSellerCat(Func<SellerCat, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetSellerCat(func, pageIndex, pageSize, out rowCount);
        }

        public static ReturnType IsSellercatExisted(string nick, string cid)
        {
            return DataProviderClass.Instance().IsSellercatExisted(nick, cid);
        }
    }
}
