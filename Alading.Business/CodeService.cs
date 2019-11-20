using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class CodeService
    {

        public static ReturnType AddCode(Code code)
        {
            return DataProviderClass.Instance().AddCode(code);
        }

        public static ReturnType AddCode(List<Code> codeList)
        {
            return DataProviderClass.Instance().AddCode(codeList);
        }
    
        public static ReturnType RemoveAllCode()
        {
            return DataProviderClass.Instance().RemoveAllCode();
        }
    
        public static ReturnType RemoveCode(Func<Code, bool> func)
        {
            return DataProviderClass.Instance().RemoveCode(func);
        }
        
        public static ReturnType RemoveCode(string codeCode)
        {
            return DataProviderClass.Instance().RemoveCode(codeCode);
        }       
        
        /*
        public static ReturnType RemoveCode(int codeID)
        {
            return DataProviderClass.Instance().RemoveCode(codeID);
        }
        */
    
        public static ReturnType RemoveCode(List<string> codeCodeList)
        {
            return DataProviderClass.Instance().RemoveCode(codeCodeList);
        }        
        
        /*
        public static ReturnType RemoveCode(List<int> codeIDList)
        {
            return DataProviderClass.Instance().RemoveCode(codeIDList);
        }
        */
    
        public static ReturnType UpdateCode(Code code)
        {
            return DataProviderClass.Instance().UpdateCode(code);
        }
    
        public static ReturnType UpdateCode(string codeCode, Code code)
        {
            return DataProviderClass.Instance().UpdateCode(codeCode, code);
        }
        
        /*
        public static ReturnType UpdateCode(int codeID, Code code)
        {
            return DataProviderClass.Instance().UpdateCode(codeID, code);
        }
        */
    
        public static List<Code> GetAllCode()
        {
            return DataProviderClass.Instance().GetAllCode();
        }
    
        public static List<Code> GetCode(Func<Code, bool> func)
        {
            return DataProviderClass.Instance().GetCode(func);
        }
    
        public static Code GetCode(string codeCode)
        {
            return DataProviderClass.Instance().GetCode(codeCode);
        }
        
        /*
        public static Code GetCode(int codeID)
        {
            return DataProviderClass.Instance().GetCode(codeID);
        }
        */
    
        public static List<Code> GetCode(List<string> codeCodeList)
        {
            return DataProviderClass.Instance().GetCode(codeCodeList);
        }
        
        /*
        public static List<Code> GetCode(List<int> codeIDList)
        {
            return DataProviderClass.Instance().GetCode(codeIDList);
        }
        */
    
        public static List<Code> GetCode(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetCode(pageIndex, pageSize, out rowCount);
        }
        
        public static List<Code> GetCode(Func<Code, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetCode(func, pageIndex, pageSize, out rowCount);
        }
    }
}
