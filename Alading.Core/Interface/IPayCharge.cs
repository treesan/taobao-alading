using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IPayCharge
    {       
        ReturnType AddPayCharge(PayCharge paycharge);
       
        ReturnType AddPayCharge(List<PayCharge> paychargeList);
        
        ReturnType RemoveAllPayCharge();
       
        ReturnType RemovePayCharge(Func<PayCharge, bool> func);
              
        ReturnType RemovePayCharge(string paychargeCode);
        
        ReturnType RemovePayCharge(List<string> paychargeCodeList);
       
        ReturnType UpdatePayCharge(PayCharge paycharge);
       
        ReturnType UpdatePayCharge(string paychargeCode,PayCharge paycharge);
       
        List<PayCharge> GetAllPayCharge();
      
        List<PayCharge> GetPayCharge(Func<PayCharge, bool> func);
      
        List<PayCharge> GetPayCharge(List<string> paychargeCodeList);
       
        List<PayCharge> GetPayCharge(int pageIndex, int pageSize, out int rowCount);
        
        List<PayCharge> GetPayCharge(Func<PayCharge, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemovePayCharge(int paychargeID);
        
        ReturnType RemovePayCharge(List<int> paychargeIDList);
        
        ReturnType UpdatePayCharge(int paychargeID,PayCharge paycharge);
        
        List<PayCharge> GetPayCharge(List<int> paychargeIDList);
        */
    }
}
