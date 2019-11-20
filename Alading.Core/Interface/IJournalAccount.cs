using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface IJournalAccount
    {       
        ReturnType AddJournalAccount(JournalAccount journalaccount);
       
        ReturnType AddJournalAccount(List<JournalAccount> journalaccountList);
        
        ReturnType RemoveAllJournalAccount();
       
        ReturnType RemoveJournalAccount(Func<JournalAccount, bool> func);
              
        ReturnType RemoveJournalAccount(string journalaccountCode);
        
        ReturnType RemoveJournalAccount(List<string> journalaccountCodeList);
       
        ReturnType UpdateJournalAccount(JournalAccount journalaccount);
       
        ReturnType UpdateJournalAccount(string journalaccountCode,JournalAccount journalaccount);
       
        List<JournalAccount> GetAllJournalAccount();
      
        List<JournalAccount> GetJournalAccount(Func<JournalAccount, bool> func);
      
        List<JournalAccount> GetJournalAccount(List<string> journalaccountCodeList);
       
        List<JournalAccount> GetJournalAccount(int pageIndex, int pageSize, out int rowCount);
        
        List<JournalAccount> GetJournalAccount(Func<JournalAccount, bool> func, int pageIndex, int pageSize, out int rowCount);
         
        /*        
        ReturnType RemoveJournalAccount(int journalaccountID);
        
        ReturnType RemoveJournalAccount(List<int> journalaccountIDList);
        
        ReturnType UpdateJournalAccount(int journalaccountID,JournalAccount journalaccount);
        
        List<JournalAccount> GetJournalAccount(List<int> journalaccountIDList);
        */
    }
}
