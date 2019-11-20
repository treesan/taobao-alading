using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IPostageMode
    {       
        ReturnType AddPostageMode(PostageMode postagemode);
       
        ReturnType AddPostageMode(List<PostageMode> postagemodeList);
        
        ReturnType RemoveAllPostageMode();
       
        ReturnType RemovePostageMode(Func<PostageMode, bool> func);
              
        
        ReturnType RemovePostageMode(List<string> postageidList);
       
        ReturnType UpdatePostageMode(PostageMode postagemode);
       
        ReturnType UpdatePostageMode(string postageid,PostageMode postagemode);
       
        List<PostageMode> GetAllPostageMode();
      
        List<PostageMode> GetPostageMode(Func<PostageMode, bool> func);
      
        List<PostageMode> GetPostageMode(List<string> postageidList);
       
        List<PostageMode> GetPostageMode(int pageIndex, int pageSize, out int rowCount);
        
        List<PostageMode> GetPostageMode(Func<PostageMode, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemovePostageMode(int postagemodeID);
        
        ReturnType RemovePostageMode(List<int> postagemodeIDList);
        
        ReturnType UpdatePostageMode(int postagemodeID,PostageMode postagemode);
        
        List<PostageMode> GetPostageMode(List<int> postagemodeIDList);
        */
    }
}
