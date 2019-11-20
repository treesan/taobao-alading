using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class PayChargeService
    {

        public static ReturnType AddPayCharge(PayCharge paycharge)
        {
            return DataProviderClass.Instance().AddPayCharge(paycharge);
        }

        public static ReturnType AddPayCharge(List<PayCharge> paychargeList)
        {
            return DataProviderClass.Instance().AddPayCharge(paychargeList);
        }
    
        public static ReturnType RemoveAllPayCharge()
        {
            return DataProviderClass.Instance().RemoveAllPayCharge();
        }
    
        public static ReturnType RemovePayCharge(Func<PayCharge, bool> func)
        {
            return DataProviderClass.Instance().RemovePayCharge(func);
        }
        
        public static ReturnType RemovePayCharge(string paychargeCode)
        {
            return DataProviderClass.Instance().RemovePayCharge(paychargeCode);
        }       
        
        /*
        public static ReturnType RemovePayCharge(int paychargeID)
        {
            return DataProviderClass.Instance().RemovePayCharge(paychargeID);
        }
        */
    
        public static ReturnType RemovePayCharge(List<string> paychargeCodeList)
        {
            return DataProviderClass.Instance().RemovePayCharge(paychargeCodeList);
        }        
        
        /*
        public static ReturnType RemovePayCharge(List<int> paychargeIDList)
        {
            return DataProviderClass.Instance().RemovePayCharge(paychargeIDList);
        }
        */
    
        public static ReturnType UpdatePayCharge(PayCharge paycharge)
        {
            return DataProviderClass.Instance().UpdatePayCharge(paycharge);
        }
    
        public static ReturnType UpdatePayCharge(string paychargeCode, PayCharge paycharge)
        {
            return DataProviderClass.Instance().UpdatePayCharge(paychargeCode, paycharge);
        }
        
        /*
        public static ReturnType UpdatePayCharge(int paychargeID, PayCharge paycharge)
        {
            return DataProviderClass.Instance().UpdatePayCharge(paychargeID, paycharge);
        }
        */
    
        public static List<PayCharge> GetAllPayCharge()
        {
            return DataProviderClass.Instance().GetAllPayCharge();
        }
    
        public static List<PayCharge> GetPayCharge(Func<PayCharge, bool> func)
        {
            return DataProviderClass.Instance().GetPayCharge(func);
        }
    
        public static PayCharge GetPayCharge(string paychargeCode)
        {
            return DataProviderClass.Instance().GetPayCharge(paychargeCode);
        }
        
        /*
        public static PayCharge GetPayCharge(int paychargeID)
        {
            return DataProviderClass.Instance().GetPayCharge(paychargeID);
        }
        */
    
        public static List<PayCharge> GetPayCharge(List<string> paychargeCodeList)
        {
            return DataProviderClass.Instance().GetPayCharge(paychargeCodeList);
        }
        
        /*
        public static List<PayCharge> GetPayCharge(List<int> paychargeIDList)
        {
            return DataProviderClass.Instance().GetPayCharge(paychargeIDList);
        }
        */
    
        public static List<PayCharge> GetPayCharge(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetPayCharge(pageIndex, pageSize, out rowCount);
        }
        
        public static List<PayCharge> GetPayCharge(Func<PayCharge, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetPayCharge(func, pageIndex, pageSize, out rowCount);
        }
    }
}
