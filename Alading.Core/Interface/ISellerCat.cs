using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface ISellerCat
    {       
        ReturnType AddSellerCat(SellerCat sellercat);
       
        ReturnType AddSellerCat(List<SellerCat> sellercatList);
        
        ReturnType RemoveAllSellerCat();
       
        ReturnType RemoveSellerCat(Func<SellerCat, bool> func);
              
        ReturnType RemoveSellerCat(string sellercatCode);
        
        ReturnType RemoveSellerCat(List<string> sellercatCodeList);
       
        ReturnType UpdateSellerCat(SellerCat sellercat);
       
        ReturnType UpdateSellerCat(string sellercatCode,SellerCat sellercat);
       
        List<SellerCat> GetAllSellerCat();
      
        List<SellerCat> GetSellerCat(Func<SellerCat, bool> func);
      
        List<SellerCat> GetSellerCat(List<string> sellercatCodeList);
       
        List<SellerCat> GetSellerCat(int pageIndex, int pageSize, out int rowCount);
        
        List<SellerCat> GetSellerCat(Func<SellerCat, bool> func, int pageIndex, int pageSize, out int rowCount);

        ReturnType IsSellercatExisted(string nick, string cid);
        /*        
        ReturnType RemoveSellerCat(int sellercatID);
        
        ReturnType RemoveSellerCat(List<int> sellercatIDList);
        
        ReturnType UpdateSellerCat(int sellercatID,SellerCat sellercat);
        
        List<SellerCat> GetSellerCat(List<int> sellercatIDList);
        */
    }
}
