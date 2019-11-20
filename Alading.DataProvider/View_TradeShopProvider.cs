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
        public List<View_TradeShop> GetView_TradeShop(List<string> view_tradeshopCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    ///*var result = alading.View_TradeShop.Where(BuildWhereInExpression<View_TradeShop, int>(v => v.View_TradeShopID, view_tradeshopIDList));*/
                    //var result = alading.View_TradeShop.Where(BuildWhereInExpression<View_TradeShop, string>(v => v., view_tradeshopCodeList));

                    //return result.ToList();
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public List<View_TradeShop> GetAllView_TradeShop()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_TradeShop> list = alading.View_TradeShop.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_TradeShop> GetView_TradeShop(Func<View_TradeShop, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_TradeShop> list = alading.View_TradeShop.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public View_TradeShop GetView_TradeShop(string view_tradeshopCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    //List<View_TradeShop> list = alading.View_TradeShop.Where(p => p.View_TradeShopCode == view_tradeshopCode).ToList();
                    //if (list.Count == 0)
                    //{
                    //    return null;
                    //}
                    //else
                    //{
                    //    return list.First();
                    //}
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_TradeShop> GetView_TradeShop(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    //var ob = (from u in alading.View_TradeShop orderby u.View_TradeShopID descending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    //rowCount = alading.View_TradeShop.Count();
                    //return ob.ToList();
                    rowCount = 0;
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_TradeShop> GetView_TradeShop(Func<View_TradeShop, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    //IOrderedEnumerable<View_TradeShop> list = alading.View_TradeShop.Where(func).OrderByDescending(a => a.View_TradeShopID);
                    //rowCount = list.Count();
                    //return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(); 
                    rowCount = 0;
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}

