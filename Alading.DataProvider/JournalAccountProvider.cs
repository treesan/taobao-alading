using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Core.Interface;
using Alading.Entity;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Data.Objects;
using Alading.Core.Enum;
using System.Linq.Expressions;
using Alading.Core;
namespace Alading.DataProvider
{
   
    public partial class DataProviderClass : IAlading
    {        
     
        public ReturnType AddJournalAccount(JournalAccount journalaccount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToJournalAccount(journalaccount);
                    if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
                    }
                    else
                    {
                        return ReturnType.PropertyExisted;
                    }
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (Exception ex)
            {
                return ReturnType.OthersError;
            }

        }
                
        public ReturnType AddJournalAccount(List<JournalAccount> journalaccountList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (JournalAccount journalaccount in journalaccountList)
                    {
                        alading.AddToJournalAccount(journalaccount);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (Exception ex)
            {
                return ReturnType.OthersError;
            }

        }
       
        public ReturnType RemoveAllJournalAccount()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<JournalAccount> list = alading.JournalAccount.ToList();
                    foreach (JournalAccount journalaccount in list)
                    {
                        alading.DeleteObject(journalaccount);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;

                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }
       
        public ReturnType RemoveJournalAccount(Func<JournalAccount, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<JournalAccount> list = alading.JournalAccount.Where(func).ToList();
                    foreach (JournalAccount journalaccount in list)
                    {
                        alading.DeleteObject(journalaccount);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }

            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

        public List<JournalAccount> GetJournalAccount(List<string> journalaccountCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.JournalAccount.Where(BuildWhereInExpression<JournalAccount, int>(v => v.JournalAccountID, journalaccountIDList));*/
                    var result = alading.JournalAccount.Where(BuildWhereInExpression<JournalAccount, string>(v => v.JournalAccountCode, journalaccountCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveJournalAccount(List<string> journalaccountCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.JournalAccount.Where(BuildWhereInExpression<JournalAccount, int>(v => v.JournalAccountID, journalaccountIDList));*/
                    var result = alading.JournalAccount.Where(BuildWhereInExpression<JournalAccount, string>(v => v.JournalAccountCode, journalaccountCodeList));
                    foreach (JournalAccount s in result)
                    {
                        alading.DeleteObject(s);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

    
        public ReturnType RemoveJournalAccount(string journalaccountCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<JournalAccount> list = alading.JournalAccount.Where(p => p.JournalAccountID == journalaccountID).ToList();*/
                    List<JournalAccount> list = alading.JournalAccount.Where(p => p.JournalAccountCode == journalaccountCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        JournalAccount sy = list.First();
                        alading.DeleteObject(sy);
                        alading.SaveChanges();
                        return ReturnType.Success;
                    }
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }
      
        public ReturnType UpdateJournalAccount(JournalAccount journalaccount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*JournalAccount result = alading.JournalAccount.Where(p => p.JournalAccountID == journalaccount.JournalAccountID).FirstOrDefault();*/
                    JournalAccount result = alading.JournalAccount.Where(p => p.JournalAccountCode == journalaccount.JournalAccountCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("JournalAccount", journalaccount);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.time = journalaccount.time;
                    
                        result.money = journalaccount.money;
                    
                        result.type = journalaccount.type;
                    
                        result.person = journalaccount.person;
                    
                        result.memo = journalaccount.memo;
			
                    */
                    #endregion  
					if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
                    }
                    return ReturnType.OthersError;
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (Exception ex)
            {
                return ReturnType.OthersError;
            }
        }
       
        public ReturnType UpdateJournalAccount(string journalaccountCode, JournalAccount journalaccount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.JournalAccount.Where(p => p.JournalAccountID == journalaccountID).ToList();*/
                    var result = alading.JournalAccount.Where(p => p.JournalAccountCode == journalaccountCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    JournalAccount ob = result.First();
                    ob.time = journalaccount.time;
                    ob.money = journalaccount.money;
                    ob.type = journalaccount.type;
                    ob.person = journalaccount.person;
                    ob.memo = journalaccount.memo;
                    
                    if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
                    }  
                    else
                    {
                        return ReturnType.OthersError;
                    }
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }
     
        public List<JournalAccount> GetAllJournalAccount()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<JournalAccount> list = alading.JournalAccount.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<JournalAccount> GetJournalAccount(Func<JournalAccount, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<JournalAccount> list = alading.JournalAccount.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public JournalAccount GetJournalAccount(string journalaccountCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<JournalAccount> list = alading.JournalAccount.Where(p => p.JournalAccountID == journalaccountID).ToList();*/
                    List<JournalAccount> list = alading.JournalAccount.Where(p => p.JournalAccountCode == journalaccountCode).ToList();
                    if (list.Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return list.First();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<JournalAccount> GetJournalAccount(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.JournalAccount orderby u.JournalAccountID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.JournalAccount.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<JournalAccount> GetJournalAccount(Func<JournalAccount, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<JournalAccount> list = alading.JournalAccount.Where(func).OrderByDescending(a=>a.JournalAccountID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }        
    }
}

