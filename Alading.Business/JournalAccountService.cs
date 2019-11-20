using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class JournalAccountService
    {

        public static ReturnType AddJournalAccount(JournalAccount journalaccount)
        {
            return DataProviderClass.Instance().AddJournalAccount(journalaccount);
        }

        public static ReturnType AddJournalAccount(List<JournalAccount> journalaccountList)
        {
            return DataProviderClass.Instance().AddJournalAccount(journalaccountList);
        }
    
        public static ReturnType RemoveAllJournalAccount()
        {
            return DataProviderClass.Instance().RemoveAllJournalAccount();
        }
    
        public static ReturnType RemoveJournalAccount(Func<JournalAccount, bool> func)
        {
            return DataProviderClass.Instance().RemoveJournalAccount(func);
        }
        
        public static ReturnType RemoveJournalAccount(string journalaccountCode)
        {
            return DataProviderClass.Instance().RemoveJournalAccount(journalaccountCode);
        }       
        
        /*
        public static ReturnType RemoveJournalAccount(int journalaccountID)
        {
            return DataProviderClass.Instance().RemoveJournalAccount(journalaccountID);
        }
        */
    
        public static ReturnType RemoveJournalAccount(List<string> journalaccountCodeList)
        {
            return DataProviderClass.Instance().RemoveJournalAccount(journalaccountCodeList);
        }        
        
        /*
        public static ReturnType RemoveJournalAccount(List<int> journalaccountIDList)
        {
            return DataProviderClass.Instance().RemoveJournalAccount(journalaccountIDList);
        }
        */
    
        public static ReturnType UpdateJournalAccount(JournalAccount journalaccount)
        {
            return DataProviderClass.Instance().UpdateJournalAccount(journalaccount);
        }
    
        public static ReturnType UpdateJournalAccount(string journalaccountCode, JournalAccount journalaccount)
        {
            return DataProviderClass.Instance().UpdateJournalAccount(journalaccountCode, journalaccount);
        }
        
        /*
        public static ReturnType UpdateJournalAccount(int journalaccountID, JournalAccount journalaccount)
        {
            return DataProviderClass.Instance().UpdateJournalAccount(journalaccountID, journalaccount);
        }
        */
    
        public static List<JournalAccount> GetAllJournalAccount()
        {
            return DataProviderClass.Instance().GetAllJournalAccount();
        }
    
        public static List<JournalAccount> GetJournalAccount(Func<JournalAccount, bool> func)
        {
            return DataProviderClass.Instance().GetJournalAccount(func);
        }
    
        public static JournalAccount GetJournalAccount(string journalaccountCode)
        {
            return DataProviderClass.Instance().GetJournalAccount(journalaccountCode);
        }
        
        /*
        public static JournalAccount GetJournalAccount(int journalaccountID)
        {
            return DataProviderClass.Instance().GetJournalAccount(journalaccountID);
        }
        */
    
        public static List<JournalAccount> GetJournalAccount(List<string> journalaccountCodeList)
        {
            return DataProviderClass.Instance().GetJournalAccount(journalaccountCodeList);
        }
        
        /*
        public static List<JournalAccount> GetJournalAccount(List<int> journalaccountIDList)
        {
            return DataProviderClass.Instance().GetJournalAccount(journalaccountIDList);
        }
        */
    
        public static List<JournalAccount> GetJournalAccount(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetJournalAccount(pageIndex, pageSize, out rowCount);
        }
        
        public static List<JournalAccount> GetJournalAccount(Func<JournalAccount, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetJournalAccount(func, pageIndex, pageSize, out rowCount);
        }
    }
}
