using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class PostageModeService
    {

        public static ReturnType AddPostageMode(PostageMode postagemode)
        {
            return DataProviderClass.Instance().AddPostageMode(postagemode);
        }

        public static ReturnType AddPostageMode(List<PostageMode> postagemodeList)
        {
            return DataProviderClass.Instance().AddPostageMode(postagemodeList);
        }
    
        public static ReturnType RemoveAllPostageMode()
        {
            return DataProviderClass.Instance().RemoveAllPostageMode();
        }
    
        public static ReturnType RemovePostageMode(Func<PostageMode, bool> func)
        {
            return DataProviderClass.Instance().RemovePostageMode(func);
        }
        
        public static ReturnType RemovePostageMode(string postageid)
        {
            return DataProviderClass.Instance().RemovePostageMode(postageid);
        }       
        
        /*
        public static ReturnType RemovePostageMode(int postagemodeID)
        {
            return DataProviderClass.Instance().RemovePostageMode(postagemodeID);
        }
        */
    
        public static ReturnType RemovePostageMode(List<string> postageidList)
        {
            return DataProviderClass.Instance().RemovePostageMode(postageidList);
        }        
        
        /*
        public static ReturnType RemovePostageMode(List<int> postagemodeIDList)
        {
            return DataProviderClass.Instance().RemovePostageMode(postagemodeIDList);
        }
        */
    
        public static ReturnType UpdatePostageMode(PostageMode postagemode)
        {
            return DataProviderClass.Instance().UpdatePostageMode(postagemode);
        }
    
        public static ReturnType UpdatePostageMode(string postageid, PostageMode postagemode)
        {
            return DataProviderClass.Instance().UpdatePostageMode(postageid, postagemode);
        }
        
        /*
        public static ReturnType UpdatePostageMode(int postagemodeID, PostageMode postagemode)
        {
            return DataProviderClass.Instance().UpdatePostageMode(postagemodeID, postagemode);
        }
        */
    
        public static List<PostageMode> GetAllPostageMode()
        {
            return DataProviderClass.Instance().GetAllPostageMode();
        }
    
        public static List<PostageMode> GetPostageMode(Func<PostageMode, bool> func)
        {
            return DataProviderClass.Instance().GetPostageMode(func);
        }

        public static List<PostageMode> GetPostageMode(string postageid)
        {
            return DataProviderClass.Instance().GetPostageMode(postageid);
        }
        
        /*
        public static PostageMode GetPostageMode(int postagemodeID)
        {
            return DataProviderClass.Instance().GetPostageMode(postagemodeID);
        }
        */
    
        public static List<PostageMode> GetPostageMode(List<string> postageidList)
        {
            return DataProviderClass.Instance().GetPostageMode(postageidList);
        }
        
        /*
        public static List<PostageMode> GetPostageMode(List<int> postagemodeIDList)
        {
            return DataProviderClass.Instance().GetPostageMode(postagemodeIDList);
        }
        */
    
        public static List<PostageMode> GetPostageMode(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetPostageMode(pageIndex, pageSize, out rowCount);
        }
        
        public static List<PostageMode> GetPostageMode(Func<PostageMode, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetPostageMode(func, pageIndex, pageSize, out rowCount);
        }
    }
}
