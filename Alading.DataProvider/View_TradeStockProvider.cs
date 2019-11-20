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
using System.Data;
using System.Data.EntityClient;
using System.Data.Common;
using Alading.Core;

namespace Alading.DataProvider
{
   
    public partial class DataProviderClass : IAlading
    {

        public ReturnType AddTradeOrderSqlBulkCopy(DataTable dataTable)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    //获得连接字符串
                    string connectionString = ((EntityConnection)alading.Connection).StoreConnection.ConnectionString;

                    SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connectionString);
                    //sqlBulkCopy.DestinationTableName = alading.ItemPropValue.CommandText;
                    sqlBulkCopy.DestinationTableName = alading.TradeOrder.CommandText;
                    sqlBulkCopy.BatchSize = dataTable.Rows.Count;
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();
                        if (dataTable != null && dataTable.Rows.Count != 0)
                        {
                            sqlBulkCopy.ColumnMappings.Add("CustomTid", "CustomTid");
                            sqlBulkCopy.ColumnMappings.Add("oid", "oid");
                            sqlBulkCopy.ColumnMappings.Add("num", "num");
                            sqlBulkCopy.ColumnMappings.Add("name", "name");
                            sqlBulkCopy.ColumnMappings.Add("sku_properties_name", "sku_properties_name");
                            sqlBulkCopy.ColumnMappings.Add("price", "price");
                            sqlBulkCopy.ColumnMappings.Add("total_fee", "total_fee");
                            sqlBulkCopy.ColumnMappings.Add("OrderType", "OrderType");
                            sqlBulkCopy.WriteToServer(dataTable);
                        }
                    }
                    return ReturnType.Success;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //获得数据库交易及订单数据，通过存储过程查询，返回两张表，提高性能
        //如果想查询多个
        public DataSet GetView_TradeStockDataSet(string localStatus, string status, DateTime startTime, DateTime endTime)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    DataSet set = new DataSet();
                    //获得连接字符串
                    string connectionString = ((EntityConnection)alading.Connection).StoreConnection.ConnectionString;
                    SqlDataAdapter adapter = new SqlDataAdapter("SP_GetView_TradeStock", connectionString);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@LocalStatus", localStatus);
                    adapter.SelectCommand.Parameters.AddWithValue("@status", status);
                    adapter.SelectCommand.Parameters.AddWithValue("@startTime", startTime);
                    adapter.SelectCommand.Parameters.AddWithValue("@endTime", endTime);
                    adapter.Fill(set);
                    return set;
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        //获得数据库交易及订单数据，通过存储过程查询，返回两张表，提高性能
        //如果想查询多个
        public DataSet GetView_TradeStockNormalDataSet(string localStatus, string status, DateTime startTime, DateTime endTime)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    DataSet set = new DataSet();
                    //获得连接字符串
                    string connectionString = ((EntityConnection)alading.Connection).StoreConnection.ConnectionString;
                    SqlDataAdapter adapter = new SqlDataAdapter("SP_GetView_TradeStockNormal", connectionString);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@LocalStatus", localStatus);
                    adapter.SelectCommand.Parameters.AddWithValue("@status", status);
                    adapter.SelectCommand.Parameters.AddWithValue("@startTime", startTime);
                    adapter.SelectCommand.Parameters.AddWithValue("@endTime", endTime);
                    adapter.Fill(set);
                    return set;
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }


        //查询当前交易下的所有商品信息
        public DataSet GetView_TradeDetailItemsDataSet(string customTid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    DataSet set = new DataSet();
                    //获得连接字符串
                    string connectionString = ((EntityConnection)alading.Connection).StoreConnection.ConnectionString;
                    SqlDataAdapter adapter = new SqlDataAdapter("SP_GetView_TradeDetailItems", connectionString);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@customTid", customTid);
                    adapter.Fill(set);
                    return set;
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType Update_TradeOrder(List<View_TradeStock> _parentOrderList, List<View_TradeStock> _childOrderList, Trade ParentTrade, Trade ChildTrade)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    Trade resultUpdate = alading.Trade.Where(q => q.CustomTid == ParentTrade.CustomTid).FirstOrDefault();
                    Trade resultAdd = new Trade();
                    /*修改父交易的相关信息*/
                    TradeCopydata(resultUpdate, ParentTrade);

                    TradeCopydata(resultAdd, ChildTrade);
                    /*添加子交易*/
                    alading.AddToTrade(resultAdd);

                    /*父订单相关信息修改*/
                    foreach (View_TradeStock tradeOrderObj in _parentOrderList)
                    {
                        TradeOrder order = alading.TradeOrder.Where(q => q.TradeOrderCode == tradeOrderObj.TradeOrderCode).FirstOrDefault();
                        /*数据修改*/
                        TradeOrderCopyData(order, tradeOrderObj);
                    }//foreach
                    /*子订单相关信息修改*/
                    foreach (View_TradeStock tradeOrderObj in _childOrderList)
                    {
                        //去掉子TradeOrderCode前的"child"
                        TradeOrder orderData = alading.TradeOrder.Where(q => q.TradeOrderCode == tradeOrderObj.TradeOrderCode).FirstOrDefault();
                        TradeOrder order = new TradeOrder();
                        copyTradeOrder(order, orderData);
                        /*数据修改*/
                        TradeOrderCopyData(order, tradeOrderObj);
                        alading.AddToTradeOrder(order);
                    }//foreach
                    alading.SaveChanges();
                    return ReturnType.Success;
                }//using
            }//try
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customtid">子交易的customtid</param>
        /// <param name="trade">父交易</param>
        /// <param name="trade_order_list">父交易的列表</param>
        /// <returns></returns>
        public ReturnType Update_TradeOrder(string customtid,Alading.Entity.Trade trade,List<Alading.Entity.TradeOrder> trade_order_list)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*删除数据库中原子订单*/
                    List<Alading.Entity.TradeOrder> order_list = alading.TradeOrder.Where(q=>q.CustomTid == customtid).ToList();
                    foreach (TradeOrder order in order_list)
                    {
                        alading.DeleteObject(order);
                    }

                    /*修改订单*/
                    foreach (TradeOrder orderObj in trade_order_list)
                    {
                        TradeOrder orderDes = alading.TradeOrder.Where(q=>q.TradeOrderCode == orderObj.TradeOrderCode).FirstOrDefault();
                        copyTradeOrder(orderDes, orderObj);
                    }

                    /*修改交易*/
                    Trade tradeDes = alading.Trade.Where(q=>q.CustomTid == trade.CustomTid).FirstOrDefault();
                    TradeCopydata(tradeDes, trade);

                    /*删除无用的子交易*/
                    Trade tradeDest = alading.Trade.Where(q => q.CustomTid == "child"+trade.CustomTid).FirstOrDefault();

                    alading.DeleteObject(tradeDest);
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

        public ReturnType Update_TradeOrder(Alading.Entity.Trade trade, List<TradeOrder> orderList, List<Trade> tradeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    Alading.Entity.Trade TradeDest = new Alading.Entity.Trade();
                    //交易赋值
                    TradeCopydata(TradeDest, trade);
                    /*添加合并后的交易*/
                    alading.AddToTrade(TradeDest);

                    /*修改子交易*/
                    foreach (Alading.Entity.Trade tradeObj in tradeList)
                    {
                        Alading.Entity.Trade tradeObject = alading.Trade.Where(q => q.CustomTid == tradeObj.CustomTid).FirstOrDefault();
                        TradeCopydata(tradeObject, tradeObj);
                    }

                    /*订单副本添加到数据库*/
                    foreach (TradeOrder order in orderList)
                    {
                        Alading.Entity.TradeOrder TradeOrderDes = new Alading.Entity.TradeOrder();
                        copyTradeOrder(TradeOrderDes, order);
                        alading.AddToTradeOrder(TradeOrderDes);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

        /// <summary>
        /// order数据复制
        /// </summary>
        /// <param name="result"></param>
        /// <param name="tradeorder"></param>
        private void TradeOrderCopyData(TradeOrder result, View_TradeStock tradeorder)
        {
            #region    Using All Items Replace To Update ,Default UnUse
                    
                        result.CustomTid = tradeorder.CustomTid;

                        result.iid = tradeorder.iid;
                    
                        result.sku_id = tradeorder.sku_id;
                    
                        result.oid = tradeorder.oid;
                    
                        result.outer_sku_id = tradeorder.outer_sku_id;
                    
                        result.outer_iid = tradeorder.outer_iid;
                    
                        result.sku_properties_name = tradeorder.sku_properties_name;
                    
                        result.price = (double)tradeorder.price;

                        result.total_fee = (double)tradeorder.total_fee;

                        result.discount_fee = (double)tradeorder.discount_fee;

                        result.adjust_fee = (double)tradeorder.adjust_fee;

                        result.payment = (double)tradeorder.payment;
                    
                        result.item_meal_name = tradeorder.item_meal_name;

                        result.num = (int)tradeorder.num;
                    
                        result.title = tradeorder.title;
                    
                        result.pic_path = tradeorder.pic_path;
                    
                        result.seller_nick = tradeorder.seller_nick;
                    
                        result.buyer_nick = tradeorder.buyer_nick;
                    
                        result.type = tradeorder.type;

                        result.created = (DateTime)tradeorder.created;
                        
                        result.status = tradeorder.status;
                        result.OrderType = tradeorder.OrderType;
                        result.OrderTimeStamp = tradeorder.TradeTimeStamp;
                    
            #endregion
        }

        public List<View_TradeStock> GetView_TradeStock(List<string> customtidlist)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					/*var result = alading.View_TradeStock.Where(BuildWhereInExpression<View_TradeStock, int>(v => v.View_TradeStockID, view_tradestockIDList));*/
                    var result = alading.View_TradeStock.Where(BuildWhereInExpression<View_TradeStock, string>(v => v.CustomTid, customtidlist));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }        
     
        public List<View_TradeStock> GetAllView_TradeStock()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_TradeStock> list = alading.View_TradeStock.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<View_TradeStock> GetView_TradeStock(Func<View_TradeStock, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_TradeStock> list = alading.View_TradeStock.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public View_TradeStock GetView_TradeStock(string customtid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<View_TradeStock> list = alading.View_TradeStock.Where(p => p.CustomTid == customtid).ToList();
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
        
        public List<View_TradeStock> GetView_TradeStock(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    
					var ob = (from u in alading.View_TradeStock orderby u.created descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.View_TradeStock.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<View_TradeStock> GetView_TradeStock(Func<View_TradeStock, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<View_TradeStock> list = alading.View_TradeStock.Where(func).OrderByDescending(a=>a.created);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckStockProduct(string customTid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    int recorder = alading.View_TradeStock.Where(p => p.CustomTid == customTid &&(p.SkuQuantity - p.OccupiedQuantity - p.num)<0).Count();
                    if (recorder ==0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 用于还原合并但
        /// </summary>
        /// <param name="combineCode">副交易的CustomTid</param>
        /// <returns></returns>
        public ReturnType Update_TradeOrder(string combineCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*删除订单副本*/
                    List<Alading.Entity.TradeOrder> tradeOrderList = alading.TradeOrder.Where(q => q.CustomTid == combineCode).ToList();
                    foreach (Alading.Entity.TradeOrder tradeOrder in tradeOrderList)
                    {
                        TradeOrder order = alading.TradeOrder.Where(q => q.TradeOrderCode == tradeOrder.TradeOrderCode).FirstOrDefault();
                        alading.DeleteObject(order);
                    }/*foreach*/

                    /*修改子交易LocalStatus*/
                    List<Alading.Entity.Trade> tradeList = alading.Trade.Where(q => q.CombineCode == combineCode).ToList();
                    foreach (Alading.Entity.Trade trade in tradeList )
                    {
                        if (alading.Trade.Where(q => q.CustomTid == trade.CustomTid).FirstOrDefault() != null)
                        {
                            Trade tradeDb = alading.Trade.Where(q => q.CustomTid == trade.CustomTid).FirstOrDefault();
                            tradeDb.LocalStatus = LocalTradeStatus.HasNotSummit;
                            tradeDb.CombineCode = string.Empty;
                        }
                    }/*foreach*/

                    //删除父交易
                    if (alading.Trade.Where(q => q.CustomTid == combineCode).FirstOrDefault() != null)
                    {
                        Alading.Entity.Trade fatherTrade = alading.Trade.Where(q => q.CustomTid == combineCode).FirstOrDefault();
                        alading.DeleteObject(fatherTrade);
                    }

                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}

