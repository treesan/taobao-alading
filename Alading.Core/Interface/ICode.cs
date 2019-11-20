using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface ICode
    {       
        ReturnType AddCode(Code code);
       
        ReturnType AddCode(List<Code> codeList);
        
        ReturnType RemoveAllCode();
       
        ReturnType RemoveCode(Func<Code, bool> func);
              
        ReturnType RemoveCode(string codeNo);
        
        ReturnType RemoveCode(List<string> codeNoList);
       
        ReturnType UpdateCode(Code code);
       
        ReturnType UpdateCode(string codeNo,Code code);
       
        List<Code> GetAllCode();
      
        List<Code> GetCode(Func<Code, bool> func);
      
        List<Code> GetCode(List<string> codeNoList);
       
        List<Code> GetCode(int pageIndex, int pageSize, out int rowCount);
        
        List<Code> GetCode(Func<Code, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveCode(int codeID);
        
        ReturnType RemoveCode(List<int> codeIDList);
        
        ReturnType UpdateCode(int codeID,Code code);
        
        List<Code> GetCode(List<int> codeIDList);
        */
    }
}
