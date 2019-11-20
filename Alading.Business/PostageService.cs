using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class PostageService
    {

        public static ReturnType AddPAndPM(List<Postage> postageList, List<PostageMode> postageModeList)
        {
            return DataProviderClass.Instance().AddPAndPM(postageList, postageModeList);
        }
        
        public static ReturnType AddPostage(Postage postage)
        {
            return DataProviderClass.Instance().AddPostage(postage);
        }

        public static ReturnType AddPostage(List<Postage> postageList)
        {
            return DataProviderClass.Instance().AddPostage(postageList);
        }
    
        public static ReturnType RemoveAllPostage()
        {
            return DataProviderClass.Instance().RemoveAllPostage();
        }
    
        public static ReturnType RemovePostage(Func<Postage, bool> func)
        {
            return DataProviderClass.Instance().RemovePostage(func);
        }
        
        public static ReturnType RemovePostage(string postage_id)
        {
            return DataProviderClass.Instance().RemovePostage(postage_id);
        }

        public static ReturnType RemovePtAndPtM(string postage_id)
        {
            return DataProviderClass.Instance().RemovePtAndPtM(postage_id);
        } 

        /*
        public static ReturnType RemovePostage(int postageID)
        {
            return DataProviderClass.Instance().RemovePostage(postageID);
        }
        */
    
        public static ReturnType RemovePostage(List<string> postageCodeList)
        {
            return DataProviderClass.Instance().RemovePostage(postageCodeList);
        }        
        
        /*
        public static ReturnType RemovePostage(List<int> postageIDList)
        {
            return DataProviderClass.Instance().RemovePostage(postageIDList);
        }
        */
    
        public static ReturnType UpdatePostage(Postage postage)
        {
            return DataProviderClass.Instance().UpdatePostage(postage);
        }
    
        public static ReturnType UpdatePostage(string postageCode, Postage postage)
        {
            return DataProviderClass.Instance().UpdatePostage(postageCode, postage);
        }
        
        /*
        public static ReturnType UpdatePostage(int postageID, Postage postage)
        {
            return DataProviderClass.Instance().UpdatePostage(postageID, postage);
        }
        */
    
        public static List<Postage> GetAllPostage()
        {
            return DataProviderClass.Instance().GetAllPostage();
        }
    
        public static List<Postage> GetPostage(Func<Postage, bool> func)
        {
            return DataProviderClass.Instance().GetPostage(func);
        }
    
        /*
        public static Postage GetPostage(string postageCode)
        {
            return DataProviderClass.Instance().GetPostage(postageCode);
        }
        */

        /*
        public static Postage GetPostage(int postageID)
        {
            return DataProviderClass.Instance().GetPostage(postageID);
        }
        */
    
        public static List<Postage> GetPostage(List<string> postageCodeList)
        {
            return DataProviderClass.Instance().GetPostage(postageCodeList);
        }

        public static Postage GetPostage(string postage_id)
        {
            return DataProviderClass.Instance().GetPostage(postage_id);
        }
        
        /*
        public static List<Postage> GetPostage(List<int> postageIDList)
        {
            return DataProviderClass.Instance().GetPostage(postageIDList);
        }
        */
    
        public static List<Postage> GetPostage(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetPostage(pageIndex, pageSize, out rowCount);
        }
        
        public static List<Postage> GetPostage(Func<Postage, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetPostage(func, pageIndex, pageSize, out rowCount);
        }

        public static Postage GetPostageByName(string name)
        {
            return DataProviderClass.Instance().GetPostageByName(name);
        }
    }
}
