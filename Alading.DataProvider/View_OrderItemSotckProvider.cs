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
        public List<View_OrderItemStockProduct> GetAllView_OrderItemStockProduct()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_OrderItemStockProduct> list = alading.View_OrderItemStockProduct.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_OrderItemStockProduct> GetView_OrderItemStockProduct(Func<View_OrderItemStockProduct, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_OrderItemStockProduct> list = alading.View_OrderItemStockProduct.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_OrderItemStockProduct> GetView_OrderItemStockProduct(Func<View_OrderItemStockProduct, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        IOrderedEnumerable<View_OrderItemStockProduct> list = alading.View_OrderItemStockProduct.Where(func).OrderByDescending(a => a.View_OrderItemStockProductID);
            //        rowCount = list.Count();
            //        return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    throw ex;
            //}
            rowCount = 0;
            return new List<View_OrderItemStockProduct>();
        }
    }
}

