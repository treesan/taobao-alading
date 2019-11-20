using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;
using System.Data.SqlClient;
using Alading.Core.Interface;
using Alading.Core;
namespace Alading.DataProvider
{
    /*添加*/
    public partial class DataProviderClass : IAlading
    {
        public ReturnType AddRateContent(RateContent rateContent)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    alading.AddToRateContent(rateContent);
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

        /*删除*/
        public ReturnType RemoveRateContent(RateContent rateContent)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var result = alading.RateContent.Where(v => v.TradeRateContent == rateContent.TradeRateContent);
                    foreach (RateContent s in result.ToList())
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

        /*获取*/
        public List<RateContent> GetRateContent(string result)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<RateContent> list = alading.RateContent.Where(c => c.Result == result).ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
