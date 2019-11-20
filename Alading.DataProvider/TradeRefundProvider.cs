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

        public ReturnType AddTradeRefund(TradeRefund traderefund)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToTradeRefund(traderefund);
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

        public ReturnType AddTradeRefund(List<TradeRefund> traderefundList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (TradeRefund traderefund in traderefundList)
                    {
                        alading.AddToTradeRefund(traderefund);
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

        public ReturnType RemoveAllTradeRefund()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<TradeRefund> list = alading.TradeRefund.ToList();
                    foreach (TradeRefund traderefund in list)
                    {
                        alading.DeleteObject(traderefund);
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

        public ReturnType RemoveTradeRefund(Func<TradeRefund, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<TradeRefund> list = alading.TradeRefund.Where(func).ToList();
                    foreach (TradeRefund traderefund in list)
                    {
                        alading.DeleteObject(traderefund);
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

        public List<TradeRefund> GetTradeRefund(List<string> traderefundCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.TradeRefund.Where(BuildWhereInExpression<TradeRefund, int>(v => v.TradeRefundID, traderefundIDList));*/
                    var result = alading.TradeRefund.Where(BuildWhereInExpression<TradeRefund, string>(v => v.refund_id, traderefundCodeList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public List<View_RefundTradeStock> GetRefundTradeStock(List<string> oidList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.TradeRefund.Where(BuildWhereInExpression<TradeRefund, int>(v => v.TradeRefundID, traderefundIDList));*/
                    var result = alading.View_RefundTradeStock.Where(BuildWhereInExpression<View_RefundTradeStock, string>(v => v.oid, oidList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveTradeRefund(List<string> traderefundCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.TradeRefund.Where(BuildWhereInExpression<TradeRefund, int>(v => v.TradeRefundID, traderefundIDList));*/
                    var result = alading.TradeRefund.Where(BuildWhereInExpression<TradeRefund, string>(v => v.refund_id, traderefundCodeList));
                    foreach (TradeRefund s in result)
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


        public ReturnType RemoveTradeRefund(string traderefundCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<TradeRefund> list = alading.TradeRefund.Where(p => p.TradeRefundID == traderefundID).ToList();*/
                    List<TradeRefund> list = alading.TradeRefund.Where(p => p.refund_id == traderefundCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        TradeRefund sy = list.First();
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

        public ReturnType UpdateTradeRefund(TradeRefund traderefund)
        {
            throw new NotImplementedException();
            //try
            //{
            //    using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
            //    {
            //        /*TradeRefund result = alading.TradeRefund.Where(p => p.TradeRefundID == traderefund.TradeRefundID).FirstOrDefault();*/
            //        TradeRefund result = alading.TradeRefund.Where(p => p.refund_id == traderefund.refund_id).FirstOrDefault();
            //        if (result == null)
            //        {
            //            return ReturnType.NotExisted;
            //        }
            //        #region   Using Attach() Function Update,Default USE;          
            //        alading.Attach(result);
            //        alading.ApplyPropertyChanges("TradeRefund", traderefund);
            //        #endregion

            //        #region    Using All Items Replace To Update ,Default UnUse
            //        /*		

            //            result.refund_id = traderefund.refund_id;

            //            result.tid = traderefund.tid;

            //            result.oid = traderefund.oid;

            //            result.alipay_no = traderefund.alipay_no;

            //            result.total_fee = traderefund.total_fee;

            //            result.buyer_nick = traderefund.buyer_nick;

            //            result.seller_nick = traderefund.seller_nick;

            //            result.created = traderefund.created;

            //            result.modified = traderefund.modified;

            //            result.order_status = traderefund.order_status;

            //            result.status = traderefund.status;

            //            result.good_status = traderefund.good_status;

            //            result.has_good_return = traderefund.has_good_return;

            //            result.refund_fee = traderefund.refund_fee;

            //            result.payment = traderefund.payment;

            //            result.reason = traderefund.reason;

            //            result.desc = traderefund.desc;

            //            result.iid = traderefund.iid;

            //            result.title = traderefund.title;

            //            result.price = traderefund.price;

            //            result.num = traderefund.num;

            //            result.good_return_time = traderefund.good_return_time;

            //            result.company_name = traderefund.company_name;

            //            result.sid = traderefund.sid;

            //            result.address = traderefund.address;

            //            result.shipping_type = traderefund.shipping_type;

            //            result.refund_remind_timeout = traderefund.refund_remind_timeout;

            //            result.LocalPrivyC = traderefund.LocalPrivyC;

            //            result.IsRecieved = traderefund.IsRecieved;

            //        */
            //        #endregion  
            //        if (alading.SaveChanges() == 1)
            //        {
            //            return ReturnType.Success;
            //        }
            //    }
            //}
            //catch (SqlException sex)
            //{
            //    return ReturnType.ConnFailed;
            //}
            //catch (Exception ex)
            //{
            //    return ReturnType.OthersError;
            //}
        }

        public ReturnType UpdateTradeRefund(string traderefundCode, TradeRefund traderefund)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.TradeRefund.Where(p => p.TradeRefundID == traderefundID).ToList();*/
                    var result = alading.TradeRefund.Where(p => p.refund_id == traderefundCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    TradeRefund ob = result.First();
                    ob.refund_id = traderefund.refund_id;
                    ob.tid = traderefund.tid;
                    ob.oid = traderefund.oid;
                    ob.alipay_no = traderefund.alipay_no;
                    ob.total_fee = traderefund.total_fee;
                    ob.buyer_nick = traderefund.buyer_nick;
                    ob.seller_nick = traderefund.seller_nick;
                    ob.created = traderefund.created;
                    ob.modified = traderefund.modified;
                    ob.order_status = traderefund.order_status;
                    ob.status = traderefund.status;
                    ob.good_status = traderefund.good_status;
                    ob.has_good_return = traderefund.has_good_return;
                    ob.refund_fee = traderefund.refund_fee;
                    ob.payment = traderefund.payment;
                    ob.reason = traderefund.reason;
                    ob.desc = traderefund.desc;
                    ob.iid = traderefund.iid;
                    ob.title = traderefund.title;
                    ob.price = traderefund.price;
                    ob.num = traderefund.num;
                    ob.good_return_time = traderefund.good_return_time;
                    ob.company_name = traderefund.company_name;
                    ob.sid = traderefund.sid;
                    ob.address = traderefund.address;
                    ob.shipping_type = traderefund.shipping_type;
                    ob.refund_remind_timeout = traderefund.refund_remind_timeout;
                    ob.LocalPrivyC = traderefund.LocalPrivyC;
                    ob.IsRecieved = traderefund.IsRecieved;

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

        public ReturnType UpdateTradeRefund(List<TradeRefund> tradeRefundList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (TradeRefund tradeRefund in tradeRefundList)
                    {
                        TradeRefund result = alading.TradeRefund.Where(p => p.refund_id == tradeRefund.refund_id).FirstOrDefault();
                        if (result == null)
                        {
                            return ReturnType.NotExisted;
                        }
                        TradeRefundCopydata(result, tradeRefund);
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

        /// <summary>
        /// 数据赋值
        /// </summary>
        /// <param name="result"></param>
        /// <param name="trade"></param>
        private void TradeRefundCopydata(TradeRefund ob, TradeRefund traderefund)
        {
            ob.refund_id = traderefund.refund_id;
            ob.tid = traderefund.tid;
            ob.oid = traderefund.oid;
            ob.alipay_no = traderefund.alipay_no;
            ob.total_fee = traderefund.total_fee;
            ob.buyer_nick = traderefund.buyer_nick;
            ob.seller_nick = traderefund.seller_nick;
            ob.created = traderefund.created;
            ob.modified = traderefund.modified;
            ob.order_status = traderefund.order_status;
            ob.status = traderefund.status;
            ob.good_status = traderefund.good_status;
            ob.has_good_return = traderefund.has_good_return;
            ob.refund_fee = traderefund.refund_fee;
            ob.payment = traderefund.payment;
            ob.reason = traderefund.reason;
            ob.desc = traderefund.desc;
            ob.iid = traderefund.iid;
            ob.title = traderefund.title;
            ob.price = traderefund.price;
            ob.num = traderefund.num;
            ob.good_return_time = traderefund.good_return_time;
            ob.company_name = traderefund.company_name;
            ob.sid = traderefund.sid;
            ob.address = traderefund.address;
            ob.shipping_type = traderefund.shipping_type;
            ob.refund_remind_timeout = traderefund.refund_remind_timeout;
            ob.LocalPrivyC = traderefund.LocalPrivyC;
            ob.IsRecieved = traderefund.IsRecieved;
        }

        public List<TradeRefund> GetAllTradeRefund()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<TradeRefund> list = alading.TradeRefund.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<TradeRefund> GetTradeRefund(Func<TradeRefund, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<TradeRefund> list = alading.TradeRefund.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public TradeRefund GetTradeRefund(string traderefundCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<TradeRefund> list = alading.TradeRefund.Where(p => p.TradeRefundID == traderefundID).ToList();*/
                    List<TradeRefund> list = alading.TradeRefund.Where(p => p.refund_id == traderefundCode).ToList();
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

        public List<TradeRefund> GetTradeRefund(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var ob = (from u in alading.TradeRefund orderby u.TradeRefundID descending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    rowCount = alading.TradeRefund.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<TradeRefund> GetTradeRefund(Func<TradeRefund, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<TradeRefund> list = alading.TradeRefund.Where(func).OrderByDescending(a => a.TradeRefundID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<View_RefundTradeStock> GetTradeRefundByView(Func<View_RefundTradeStock, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    //IOrderedEnumerable<View_RefundTradeStock> list = alading.View_RefundTradeStock.Where(func).OrderByDescending(a => a.refund_id);
                    List<View_RefundTradeStock> list = alading.View_RefundTradeStock.Where(func).ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType UpdateTradeRefund(List<TradeRefund> tradeRefundList, List<StockProduct> stockProductList, List<StockHouseProduct> houseProList
            , List<StockInOut> stockInOutList, List<StockDetail> stockDetailList, PayCharge payCharge,List<string> refundIdList
            , List<string> outerSkuIdList, List<string> outerIdList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*更新TradeRefund*/
                    var refundResult = alading.TradeRefund.Where(BuildWhereInExpression<TradeRefund, string>(v => v.refund_id, refundIdList));
                    if (refundResult == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    
                    foreach (TradeRefund tradeRefund in tradeRefundList)//需要更新的新数据
                    {
                        TradeRefund oldRefund = refundResult.Where(c => c.refund_id == tradeRefund.refund_id).FirstOrDefault();
                        //TradeRefundCopydata(oldRefund, tradeRefund);
                        oldRefund.IsRecieved = tradeRefund.IsRecieved;
                        oldRefund.LocalPrivyC = tradeRefund.LocalPrivyC;
                    }

                    /*更新StockProduct 和 StockItem*/
                    var stockProductResult = alading.StockProduct.Where(BuildWhereInExpression<StockProduct, string>(v => v.SkuOuterID, outerSkuIdList));
                    var stockItemResult = alading.StockItem.Where(BuildWhereInExpression<StockItem, string>(v => v.OuterID, outerIdList));
                    if (stockProductResult == null || stockItemResult==null)
                    {
                        return ReturnType.NotExisted;
                    }

                    foreach (StockProduct stockProduct in stockProductList)
                    {
                        //获取StockProduct的原始数据
                        StockProduct oldStockProduct = stockProductResult.Where(c => c.SkuOuterID == stockProduct.SkuOuterID).FirstOrDefault();
                        //获取StockItem的原始数据
                        StockItem oldStockItem = stockItemResult.Where(c => c.OuterID == stockProduct.OuterID).FirstOrDefault();
                        if (oldStockProduct != null && oldStockItem != null)
                        {
                            //获取StockProduct的相关原始数据的商品数量
                            oldStockProduct.SkuQuantity = oldStockProduct.SkuQuantity + stockProduct.SkuQuantity;
                            //获取oldStockItem的相关原始数据的商品数量
                            oldStockItem.TotalQuantity = oldStockItem.TotalQuantity + stockProduct.SkuQuantity;
                        }
                        else
                        {
                            return ReturnType.NotExisted;
                        }
                    }

                    /*添加或更新StockHouseProduct*/
                    var stockHouseProResult = alading.StockHouseProduct.Where(BuildWhereInExpression<StockHouseProduct, string>(v => v.SkuOuterID, outerSkuIdList));
                    foreach (StockHouseProduct stockHousePro in houseProList)
                    {
                        StockHouseProduct oldPro = stockHouseProResult.Where(c => c.SkuOuterID == stockHousePro.SkuOuterID
                            && c.HouseCode == stockHousePro.HouseCode && c.LayoutCode == stockHousePro.LayoutCode).FirstOrDefault();
                        if (oldPro != null)
                        {
                            oldPro.Num += stockHousePro.Num;
                        }
                        else
                        {
                            stockHousePro.HouseProductCode = Guid.NewGuid().ToString();
                            alading.AddToStockHouseProduct(stockHousePro);
                        }
                    }

                    /*添加StockInOut*/
                    foreach (StockInOut stockInOut in stockInOutList)
                    {
                        alading.AddToStockInOut(stockInOut);
                    }

                    /*添加StockDetail*/
                    foreach (StockDetail stockDetail in stockDetailList)
                    {
                        alading.AddToStockDetail(stockDetail);
                    }

                    alading.AddToPayCharge(payCharge);

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
    }
}

