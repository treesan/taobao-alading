using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IPostage
    {       
        ReturnType AddPostage(Postage postage);
       
        ReturnType AddPostage(List<Postage> postageList);
        
        ReturnType RemoveAllPostage();
       
        ReturnType RemovePostage(Func<Postage, bool> func);
              
        ReturnType RemovePostage(string postageCode);
        
        ReturnType RemovePostage(List<string> postageCodeList);
       
        ReturnType UpdatePostage(Postage postage);
       
        ReturnType UpdatePostage(string postageCode,Postage postage);
       
        List<Postage> GetAllPostage();
      
        List<Postage> GetPostage(Func<Postage, bool> func);
      
        List<Postage> GetPostage(List<string> postageCodeList);
       
        List<Postage> GetPostage(int pageIndex, int pageSize, out int rowCount);
        
        List<Postage> GetPostage(Func<Postage, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemovePostage(int postageID);
        
        ReturnType RemovePostage(List<int> postageIDList);
        
        ReturnType UpdatePostage(int postageID,Postage postage);
        
        List<Postage> GetPostage(List<int> postageIDList);
        */
    }
}
