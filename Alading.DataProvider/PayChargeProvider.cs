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
     
        public ReturnType AddPayCharge(PayCharge paycharge)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToPayCharge(paycharge);
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
                
        public ReturnType AddPayCharge(List<PayCharge> paychargeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (PayCharge paycharge in paychargeList)
                    {
                        alading.AddToPayCharge(paycharge);
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
       
        public ReturnType RemoveAllPayCharge()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<PayCharge> list = alading.PayCharge.ToList();
                    foreach (PayCharge paycharge in list)
                    {
                        alading.DeleteObject(paycharge);
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
       
        public ReturnType RemovePayCharge(Func<PayCharge, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<PayCharge> list = alading.PayCharge.Where(func).ToList();
                    foreach (PayCharge paycharge in list)
                    {
                        alading.DeleteObject(paycharge);
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

        public List<PayCharge> GetPayCharge(List<string> paychargeCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.PayCharge.Where(BuildWhereInExpression<PayCharge, int>(v => v.PayChargeID, paychargeIDList));*/
                    var result = alading.PayCharge.Where(BuildWhereInExpression<PayCharge, string>(v => v.PayChargeCode, paychargeCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemovePayCharge(List<string> paychargeCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.PayCharge.Where(BuildWhereInExpression<PayCharge, int>(v => v.PayChargeID, paychargeIDList));*/
                    var result = alading.PayCharge.Where(BuildWhereInExpression<PayCharge, string>(v => v.PayChargeCode, paychargeCodeList));
                    foreach (PayCharge s in result)
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

    
        public ReturnType RemovePayCharge(string paychargeCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<PayCharge> list = alading.PayCharge.Where(p => p.PayChargeID == paychargeID).ToList();*/
                    List<PayCharge> list = alading.PayCharge.Where(p => p.PayChargeCode == paychargeCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        PayCharge sy = list.First();
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
      
        public ReturnType UpdatePayCharge(PayCharge paycharge)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*PayCharge result = alading.PayCharge.Where(p => p.PayChargeID == paycharge.PayChargeID).FirstOrDefault();*/
                    PayCharge result = alading.PayCharge.Where(p => p.PayChargeCode == paycharge.PayChargeCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("PayCharge", paycharge);
                    #endregion
                    
                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                    
                        result.PayChargeCode = paycharge.PayChargeCode;
                    
                        result.PayChargeType = paycharge.PayChargeType;
                    
                        result.InOutCode = paycharge.InOutCode;
                    
                        result.PayerCode = paycharge.PayerCode;
                    
                        result.PayerName = paycharge.PayerName;
                    
                        result.ChargerCode = paycharge.ChargerCode;
                    
                        result.ChargerName = paycharge.ChargerName;
                    
                        result.OperateTime = paycharge.OperateTime;
                    
                        result.OperatorCode = paycharge.OperatorCode;
                    
                        result.OperatorName = paycharge.OperatorName;
                    
                        result.RealFee = paycharge.RealFee;
                    
                        result.PayChargeRemark = paycharge.PayChargeRemark;
			
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
       
        public ReturnType UpdatePayCharge(string paychargeCode, PayCharge paycharge)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.PayCharge.Where(p => p.PayChargeID == paychargeID).ToList();*/
                    var result = alading.PayCharge.Where(p => p.PayChargeCode == paychargeCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    PayCharge ob = result.First();
                    ob.PayChargeCode = paycharge.PayChargeCode;
                    ob.PayChargeType = paycharge.PayChargeType;
                    ob.InOutCode = paycharge.InOutCode;
                    ob.PayerCode = paycharge.PayerCode;
                    ob.PayerName = paycharge.PayerName;
                    ob.ChargerCode = paycharge.ChargerCode;
                    ob.ChargerName = paycharge.ChargerName;
                    ob.OperateTime = paycharge.OperateTime;
                    ob.OperatorCode = paycharge.OperatorCode;
                    ob.OperatorName = paycharge.OperatorName;
                    ob.PayThisTime = paycharge.PayThisTime;
                    ob.PayerName = paycharge.PayerName;
                    ob.AmountTax = paycharge.AmountTax;
                    ob.TotalFee = paycharge.TotalFee;
                    ob.DiscountFee = paycharge.DiscountFee;
                    ob.NeedToPay = paycharge.NeedToPay;
                    ob.PayThisTime = paycharge.PayThisTime;
                    ob.PayChargeRemark = paycharge.PayChargeRemark;
                    
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
     
        public List<PayCharge> GetAllPayCharge()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<PayCharge> list = alading.PayCharge.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<PayCharge> GetPayCharge(Func<PayCharge, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<PayCharge> list = alading.PayCharge.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public PayCharge GetPayCharge(string paychargeCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<PayCharge> list = alading.PayCharge.Where(p => p.PayChargeID == paychargeID).ToList();*/
                    List<PayCharge> list = alading.PayCharge.Where(p => p.PayChargeCode == paychargeCode).ToList();
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
        
        public List<PayCharge> GetPayCharge(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.PayCharge orderby u.PayChargeID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.PayCharge.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<PayCharge> GetPayCharge(Func<PayCharge, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<PayCharge> list = alading.PayCharge.Where(func).OrderByDescending(a=>a.PayChargeID);
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

