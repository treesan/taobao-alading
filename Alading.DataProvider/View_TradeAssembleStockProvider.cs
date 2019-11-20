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
using System.Data.EntityClient;
using System.Data;
using Alading.Core;

namespace Alading.DataProvider
{

    public partial class DataProviderClass : IAlading
    {
        public List<View_TradeAssembleStock> GetAllView_TradeAssembleStock()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_TradeAssembleStock> list = alading.View_TradeAssembleStock.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_TradeAssembleStock> GetView_TradeAssembleStock(Func<View_TradeAssembleStock, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_TradeAssembleStock> list = alading.View_TradeAssembleStock.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public View_TradeAssembleStock GetView_TradeAssembleStock(string AssembleCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_TradeAssembleStock> list = alading.View_TradeAssembleStock.Where(p => p.AssembleCode == AssembleCode).ToList();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customTid"></param>
        /// <param name="oldStatus"></param>
        /// <param name="newStatus"></param>
        /// <param name="shippingCode"></param>
        /// <returns></returns>
        public DataSet GetViewTradeAssembleDataSet(string tradeOrderCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    DataSet set = new DataSet();
                    string connectionString = ((EntityConnection)alading.Connection).StoreConnection.ConnectionString;
                    SqlDataAdapter adapter = new SqlDataAdapter("SP_GetView_TradeAssemble",connectionString);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.AddWithValue("@tradeOrderCode", tradeOrderCode);
                    adapter.Fill(set);
                    return set;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}

