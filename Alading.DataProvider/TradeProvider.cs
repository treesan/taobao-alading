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
        public ReturnType AddTrade(Trade trade)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    alading.AddToTrade(trade);

                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (SqlException sex)
            {
                throw sex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
                
        public ReturnType AddTrade(List<Trade> tradeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (Trade trade in tradeList)
                    {
                        alading.AddToTrade(trade);
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

        public ReturnType AddTradeOrderBuyer(Alading.Taobao.Entity.Trade trade)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    #region 更新交易 以及 订单
                    Trade trade_des = alading.Trade.Where(q => q.tid == trade.Tid).FirstOrDefault();

                    /*trade_des == null 说明数据库没有相同的交易*/
                    if (trade_des == null)
                    {
                        Trade TradeInDb = new Trade();
                        TradeCopyData(TradeInDb, trade, false);
                        #region  物流相关信息
                        /*物流方式为ems*/
                        if (TradeInDb.shipping_type.ToLower() == ShippingType.ems)
                        {
                            TradeInDb.LogisticCompanyCode = ShippingType.ems.ToUpper();
                            TradeInDb.TemplateCode = ShippingType.ems.ToUpper();
                        }
                        else if (TradeInDb.shipping_type.ToLower() == ShippingType.post)/*物流方式为平邮*/
                        {
                            TradeInDb.LogisticCompanyCode = ShippingType.post.ToUpper();
                            TradeInDb.TemplateCode = ShippingType.post.ToUpper();
                        }
                        else if (TradeInDb.shipping_type == ShippingType.express || TradeInDb.shipping_type == ShippingType.free)/*物流方式为快递或者 free*/
                        {
                            LogisticCompany logistic_com = alading.LogisticCompany.Where(q => q.shippingType == ShippingType.express && q.isdefault == true).FirstOrDefault();
                            if (logistic_com != null)
                            {
                                /*添加companyCode*/
                                TradeInDb.LogisticCompanyCode = logistic_com.code;
                                LogisticCompanyTemplate Template = alading.LogisticCompanyTemplate.Where(q => q.LogisticCompanyCode
                                    /*templateCode*/                                                                                                                            == logistic_com.code).FirstOrDefault();
                                if (Template != null)
                                {
                                    TradeInDb.TemplateCode = Template.TemplateCode;
                                }
                            }
                        }/*else if*/
                        else/*虚拟物品*/
                        {
                            TradeInDb.LogisticCompanyCode = string.Empty;
                            TradeInDb.TemplateCode = string.Empty;
                        }
                        #endregion
                        alading.AddToTrade(TradeInDb);

                        /* 添加订单*/
                        foreach (Alading.Taobao.Entity.Order orderObj in trade.Orders.Order)
                        {
                            TradeOrder OrderInDb = new TradeOrder();
                            OrderCopyData(OrderInDb, orderObj, TradeInDb.CustomTid, false);
                            alading.AddToTradeOrder(OrderInDb);
                        }/*foreach*/
                    }/*if (trade_des == null)*/
                    else/* 数据库中已经有了相同的交易*/
                    {
                        TradeCopyData(trade_des, trade, true);
                        /*更新 除了本地数据库自己加的字段以外的所有字段*/

                        /*此时订单更新*/
                        foreach (Alading.Taobao.Entity.Order orderObj in trade.Orders.Order)
                        {
                            TradeOrder OrderInDb = alading.TradeOrder.Where(q => q.oid == orderObj.Oid).FirstOrDefault();
                            if (OrderInDb != null)
                            {
                                OrderCopyData(OrderInDb, orderObj, OrderInDb.CustomTid, true);
                            }
                        }/*foreach*/
                    }/*else*/
                    #endregion

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

        #region 辅助函数
        #region  地址字符串生成
        private string CreatStr(ConsumerAddress consumer_add)
        {
            StringBuilder str = new StringBuilder();
            str.Append(consumer_add.location_address);
            str.Append(consumer_add.location_city);
            str.Append(consumer_add.location_country);
            str.Append(consumer_add.location_district);
            str.Append(consumer_add.location_state);
            str.Append(consumer_add.location_zip);
            return str.ToString();
        }
        #endregion

        #region  TradeCopyData
        /// <summary>
        /// 淘宝的交易数据转换成本地数据库的数据
        /// </summary>
        /// <param name="destObj"></param>
        /// <param name="srcObj"></param>
        public void  TradeCopyData(Alading.Entity.Trade destObj, Alading.Taobao.Entity.Trade srcObj,bool HasExit)
        {
            destObj.status = srcObj.Status;
            destObj.tid = srcObj.Tid;
            destObj.CustomTid = GetShopByNick(srcObj.SellerNick).ShopID + "_" + srcObj.Tid;
            destObj.iid = srcObj.Iid??string.Empty;
            destObj.seller_nick = srcObj.SellerNick ?? string.Empty;
            destObj.buyer_nick = srcObj.BuyerNick ?? string.Empty;
            destObj.title = srcObj.Title ?? string.Empty;
            destObj.type = srcObj.Type ?? string.Empty;
            destObj.created = DateTime.Parse(srcObj.Created ?? Alading.Taobao.Constants.DEFAULT_TIME);
            destObj.price = srcObj.ItemPrice;
            destObj.pic_path = srcObj.ItemUrl ?? string.Empty;
            destObj.num = srcObj.ItemNum;
            destObj.buyer_message = srcObj.BuyerMessage ?? string.Empty;
            destObj.buyer_rate = srcObj.BuyerRate;
            destObj.buyer_memo = srcObj.BuyerMemo ?? string.Empty;
            destObj.seller_rate = srcObj.SellerRate;
            destObj.seller_memo = srcObj.SellerMemo ?? string.Empty;
            destObj.shipping_type = srcObj.ShippingType ?? string.Empty;
            destObj.alipay_no = srcObj.AlipayNo ?? string.Empty;
            destObj.payment = srcObj.Payment;
            destObj.discount_fee = srcObj.DiscountFee;
            destObj.adjust_fee = srcObj.AdjustFee;
            destObj.snapshot_url = srcObj.SnapshotUrl;
            destObj.snapshot = srcObj.Snapshot ?? string.Empty;
            destObj.pay_time = DateTime.Parse(srcObj.PayTime ?? Alading.Taobao.Constants.DEFAULT_TIME);
            destObj.end_time = DateTime.Parse(srcObj.EndTime ?? Alading.Taobao.Constants.DEFAULT_TIME);
            destObj.modified = DateTime.Parse(srcObj.Modified ?? Alading.Taobao.Constants.DEFAULT_TIME);
            destObj.buyer_obtain_point_fee = srcObj.BuyerObtainPointFee;
            destObj.point_fee = string.Empty;
            destObj.real_point_fee = string.Empty;
            destObj.total_fee = srcObj.TotalFee;
            destObj.post_fee = srcObj.PostFee;
            destObj.buyer_alipay_no = srcObj.BuyerAlipayNo ?? string.Empty;
            destObj.receiver_name = srcObj.ReceiverName ?? string.Empty;
            destObj.receiver_state = srcObj.ReceiverState ?? string.Empty;
            destObj.receiver_city = srcObj.ReceiverCity ?? string.Empty;
            destObj.receiver_district = srcObj.ReceiverDistrict ?? string.Empty;
            destObj.receiver_address = srcObj.ReceiverAddress ?? string.Empty;
            destObj.receiver_zip = srcObj.ReceiverZip ?? string.Empty;
            destObj.receiver_mobile = srcObj.ReceiverMobile ?? string.Empty;
            destObj.receiver_phone = srcObj.ReceiverPhone ?? string.Empty;
            destObj.consign_time = DateTime.Parse(srcObj.ConsignTime ?? Alading.Taobao.Constants.DEFAULT_TIME);
            destObj.buyer_email = srcObj.BuyerEmail ?? string.Empty;
            destObj.commission_fee = srcObj.CommissionFee ?? string.Empty;
            destObj.seller_alipay_no = srcObj.SellerAlipayNo ?? string.Empty;
            destObj.seller_mobile = srcObj.SellerMobile ?? string.Empty;
            destObj.seller_phone = srcObj.SellerPhone ?? string.Empty;
            destObj.seller_name = srcObj.SellerName ?? string.Empty;
            destObj.seller_email = srcObj.SellerEmail ?? string.Empty;
            destObj.available_confirm_fee = srcObj.AvailableConfirmFee ?? string.Empty;
            destObj.has_post_fee = srcObj.HasPostFee;
            destObj.received_payment = srcObj.ReceivedPayment;
            destObj.cod_fee = srcObj.CodFee;
            destObj.cod_status = srcObj.CodStatus ?? string.Empty;
            destObj.timeout_action_time = DateTime.Parse(srcObj.Timeout ?? Alading.Taobao.Constants.DEFAULT_TIME);
            destObj.is_3D = false;
            destObj.BuyerType = 1;//?
            destObj.type = srcObj.Type ?? string.Empty;
            destObj.SellerType = 0;
            //destObj.LogisticCompanyCode = Constants.DEFAULT_SHIPPING_COMPANY/*申通*/;
            destObj.TradeSourceType = TradeSourceType.TAOBAOAPI;
            if (!HasExit)/*不存在时*/
            {   
                /*本地字段 不能更新*/
                destObj.LocalStatus = LocalTradeStatus.HasNotSummit;
                destObj.LastShippingType = destObj.shipping_type;
                destObj.ShippingCode = string.Empty;
                destObj.LockedUserName = UNLOCKED.VALUE;
                destObj.TradeTimeStamp = new byte[2];
                destObj.ParentCustomTid = "0";
                destObj.LockedUserCode = string.Empty;
                destObj.LogisticCompanyCode = string.Empty;
                destObj.TemplateCode = string.Empty;
                destObj.HasInvoice = false;
                destObj.IsSplited = false;
                destObj.IsCombined = false;
                destObj.CombineCode = string.Empty;
                destObj.LockedTime = DateTime.MinValue;
                destObj.RealPostFee = 0.0;
                destObj.IsSelected = false;
            }
        }
        #endregion

        #region OrderCopyData
        /// <summary>
        /// 淘宝订单数据转换成本地数据库订单数据
        /// </summary>
        /// <param name="destObj"></param>
        /// <param name="srcObj"></param>
        private void OrderCopyData(Alading.Entity.TradeOrder destObj, Alading.Taobao.Entity.Order srcObj,string custom_tid,bool IsExit)
        {
            destObj.iid = srcObj.Iid;
            destObj.TradeOrderCode = Guid.NewGuid().ToString();
            destObj.outer_sku_id = srcObj.OuterSkuId ?? string.Empty;
            destObj.sku_id = srcObj.SkuId ?? string.Empty;
            destObj.oid = srcObj.Oid;
            destObj.outer_iid = srcObj.OuterIid ?? string.Empty;
            destObj.sku_properties_name = Resolve(srcObj.SkuProps) ?? string.Empty;
            destObj.price = double.Parse(srcObj.ItemPrice);
            destObj.total_fee = srcObj.TotalFee;
            destObj.discount_fee = srcObj.DiscountFee;
            destObj.adjust_fee = srcObj.AdjustFee;
            destObj.payment = srcObj.Payment;
            destObj.item_meal_name = srcObj.ItemMealName ?? string.Empty;
            destObj.num = srcObj.ItemNum;
            destObj.title = srcObj.ItemTitle;
            destObj.pic_path = srcObj.SnapshotUrl ?? string.Empty;
            destObj.seller_nick = srcObj.SellerNick ?? string.Empty;
            destObj.seller_type = srcObj.SellerType ?? string.Empty;
            destObj.buyer_nick = srcObj.BuyerNick ?? string.Empty;
            destObj.created = DateTime.Parse(Alading.Taobao.Constants.DEFAULT_TIME);
            destObj.refund_status = srcObj.RefundStatus;
            destObj.status = TradeEnum.WAIT_SELLER_SEND_GOODS;
            destObj.snapshot_url = srcObj.SnapshotUrl ?? string.Empty;
            destObj.snapshot = srcObj.Snapshot ?? string.Empty;
            destObj.timeout_action_time = DateTime.Parse(srcObj.OrderTimeout ?? Alading.Taobao.Constants.DEFAULT_TIME);
            destObj.type = srcObj.OrderType ?? string.Empty;

            if (!IsExit)/*本地存在此订单 不更新一下本地字段*/
            {
                destObj.OrderType = Alading.Core.Enum.emumOrderType.SellGoods;
                destObj.CustomTid = custom_tid;
                destObj.HouseCode = string.Empty;
                destObj.LayoutCode = string.Empty;
                destObj.OrderTimeStamp = new byte[2];
            }
        }
        /// <summary>
        /// 重构字符串
        /// </summary>
        /// <param name="skuPros"></param>
        /// <returns></returns>
        private string Resolve(string skuPros)
        {
            List<string> str_list = new List<string>();
            string[] str1 = null;
            StringBuilder strBuilder = new StringBuilder();
            str1 = skuPros.Split(';');
            foreach (string str in str1)
            {
                str_list.Add(str);
            }
            str_list.Sort();
            foreach (string str in str_list)
            {
                strBuilder.Append(str);
                strBuilder.Append(";");
            }
            return strBuilder.ToString().Substring(0, strBuilder.ToString().Length - 1);
        }
        #endregion

        #region UserCopyData
        /// <summary>
        /// 淘宝user数据转换成本地Consumer数据
        /// </summary>
        /// <param name="desObj"></param>
        /// <param name="srcObj"></param>
        private void UserCopyData(Alading.Entity.Consumer desObj, Alading.Entity.Consumer srcObj, List<Alading.Taobao.Entity.Trade> TradeList)
        {
            List<Alading.Taobao.Entity.Trade> tradeList = TradeList.Where(q=>q.BuyerNick == srcObj.nick && q.ReceiverName == srcObj.buyer_name).ToList();
            if (tradeList.Count > 0)
            {
                desObj.location_city = srcObj.location_city??tradeList.FirstOrDefault().ReceiverCity;
                desObj.location_state = srcObj.location_state ?? tradeList.FirstOrDefault().ReceiverState;
                desObj.location_district = srcObj.location_district ?? tradeList.FirstOrDefault().ReceiverDistrict;
                desObj.location_address = srcObj.location_address ?? tradeList.FirstOrDefault().ReceiverAddress;
                desObj.location_country = "中国";
                desObj.buyer_zip = srcObj.buyer_zip ?? tradeList.FirstOrDefault().ReceiverZip;
            }
            desObj.sex = srcObj.sex ?? "";
            desObj.nick = srcObj.nick;
            desObj.birthday = srcObj.birthday;
            desObj.credit = srcObj.credit.ToString();
            desObj.level = srcObj.level;
            desObj.score = srcObj.score;
            desObj.status = srcObj.status ?? "";
            desObj.created = srcObj.created;
            desObj.last_visit = srcObj.last_visit;
            desObj.mobilephone = srcObj.mobilephone;
            desObj.phone = srcObj.phone;
            desObj.email = srcObj.email;
            desObj.buyer_name = srcObj.buyer_name;
            desObj.buyer_zip = srcObj.buyer_zip;
            desObj.alipay = srcObj.alipay;
        }
        #endregion
        #endregion

        public ReturnType RemoveAllTrade()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Trade> list = alading.Trade.ToList();
                    foreach (Trade trade in list)
                    {
                        alading.DeleteObject(trade);
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
       
        public ReturnType RemoveTrade(Func<Trade, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Trade> list = alading.Trade.Where(func).ToList();
                    foreach (Trade trade in list)
                    {
                        alading.DeleteObject(trade);
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

        public List<Trade> GetTrade(List<string> customTidList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*var result = alading.Trade.Where(BuildWhereInExpression<Trade, int>(v => v.TradeID, tradeIDList));*/
                    var result = alading.Trade.Where(BuildWhereInExpression<Trade, string>(v => v.CustomTid, customTidList));

                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveTrade(List<string> customTidList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.Trade.Where(BuildWhereInExpression<Trade, int>(v => v.TradeID, tradeIDList));*/
                    var result = alading.Trade.Where(BuildWhereInExpression<Trade, string>(v => v.CustomTid, customTidList));
                    foreach (Trade s in result)
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

        public ReturnType RemoveTrade(string customTid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
					/*List<Trade> list = alading.Trade.Where(p => p.TradeID == tradeID).ToList();*/
                    List<Trade> list = alading.Trade.Where(p => p.CustomTid == customTid).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    else
                    {
                        Trade sy = list.First();
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

        //public override void AutoCopyData(object destObj, object srcObj)
        //{
        //    PropertyInfo[] destProperties = destObj.GetType().GetProperties();
        //    PropertyInfo[] srcProperties = srcObj.GetType().GetProperties();

        //    foreach (PropertyInfo pi in srcProperties)
        //    {
        //        if (pi.ToString().Contains("ID") || pi.ToString().Contains("System.Data"))
        //            continue;
        //        else
        //        {
        //            string properyName = pi.Name;
        //            IEnumerable<PropertyInfo> properyList = destProperties.Where(p => p.Name == properyName);
        //            if (properyList.Count() == 1)
        //            {
        //                object value = srcObj.GetType().GetProperty(properyName).GetValue(srcObj, null);
        //                if (value != null && !string.IsNullOrEmpty(value.ToString()))
        //                    destObj.GetType().GetProperty(properyName).SetValue(destObj, value, null);
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 用此方法 要特别小心
        /// </summary>
        /// <param name="trade"></param>
        /// <returns></returns>
        public ReturnType UpdateTrade(Trade trade)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*Trade result = alading.Trade.Where(p => p.TradeID == trade.TradeID).FirstOrDefault();*/
                    Trade result = alading.Trade.Where(p => p.CustomTid == trade.CustomTid).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;          
                    //alading.Attach(result);
                    //alading.ApplyPropertyChanges("Trade", trade);
                    //Type type = trade.GetType();
                    //var pro = type.GetProperties();
                    //foreach (var pi in pro)
                    //{
                    //    if (pi.ToString().Contains("ID") || pi.ToString().Contains("System.Data"))
                    //        continue;
                    //        pi.SetValue(result, pi.GetValue(trade, null), null);

                    //}
                    #endregion
                    TradeCopydata(result,trade);

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
        private void TradeCopydata(Trade result, Trade trade)
        {
            #region    Using All Items Replace To Update ,Default UnUse
            result.RealPostFee = trade.RealPostFee;
            result.tid = trade.tid;
            result.iid = trade.iid;
            result.seller_nick = trade.seller_nick;
            result.buyer_nick = trade.buyer_nick;
            result.title = trade.title;
            result.type = trade.type;
            result.created = trade.created;
            result.price = trade.price;
            result.status = trade.status;
            result.pic_path = trade.pic_path;
            result.num = trade.num;
            result.buyer_message = trade.buyer_message;
            result.buyer_rate = trade.buyer_rate;
            result.buyer_memo = trade.buyer_memo;
            result.seller_rate = trade.seller_rate;
            result.seller_memo = trade.seller_memo;
            result.alipay_no = trade.alipay_no;
            result.payment = trade.payment;
            result.discount_fee = trade.discount_fee;
            result.adjust_fee = trade.adjust_fee;
            result.snapshot_url = trade.snapshot_url;
            result.snapshot = trade.snapshot;
            result.pay_time = trade.pay_time;
            result.end_time = trade.end_time;
            result.modified = trade.modified;
            result.buyer_obtain_point_fee = trade.buyer_obtain_point_fee;
            result.point_fee = trade.point_fee;
            result.real_point_fee = trade.real_point_fee;
            result.total_fee = trade.total_fee;
            result.post_fee = trade.post_fee;
            result.buyer_alipay_no = trade.buyer_alipay_no;
            result.receiver_name = trade.receiver_name;
            result.receiver_state = trade.receiver_state;
            result.receiver_city = trade.receiver_city;
            result.receiver_district = trade.receiver_district;
            result.receiver_address = trade.receiver_address;
            result.receiver_zip = trade.receiver_zip;
            result.receiver_mobile = trade.receiver_mobile;
            result.receiver_phone = trade.receiver_phone;
            result.consign_time = trade.consign_time;
            result.buyer_email = trade.buyer_email;
            result.commission_fee = trade.commission_fee;
            result.seller_alipay_no = trade.seller_alipay_no;
            result.seller_mobile = trade.seller_mobile;
            result.seller_phone = trade.seller_phone;
            result.seller_name = trade.seller_name;
            result.seller_email = trade.seller_email;
            result.available_confirm_fee = trade.available_confirm_fee;
            result.has_post_fee = trade.has_post_fee;
            result.received_payment = trade.received_payment;
            result.cod_fee = trade.cod_fee;
            result.cod_status = trade.cod_status;
            result.timeout_action_time = trade.timeout_action_time;
            result.is_3D = trade.is_3D;
            result.shipping_type = trade.shipping_type;
            result.CustomTid = trade.CustomTid;
            result.ParentCustomTid = trade.ParentCustomTid;
            result.LocalStatus = trade.LocalStatus;
            result.LastShippingType = trade.LastShippingType;
            result.ShippingCode = trade.ShippingCode;
            result.LogisticCompanyCode = trade.LogisticCompanyCode;
            result.TemplateCode = trade.TemplateCode;
            result.CombineCode = trade.CombineCode;
            result.BuyerType = trade.BuyerType;
            result.SellerType = trade.SellerType;
            result.LockedUserCode = trade.LockedUserCode;
            result.LockedUserName = trade.LockedUserName;
            result.LockedTime = trade.LockedTime;
            result.IsCombined = trade.IsCombined;
            result.IsSplited = trade.IsSplited;
            result.IsSelected = trade.IsSelected;
            result.TradeSourceType = trade.TradeSourceType;
            result.HasInvoice = trade.HasInvoice;
            result.ConsignStatus = trade.ConsignStatus;
            result.TradeTimeStamp = trade.TradeTimeStamp;
            #endregion  
        }

        public ReturnType UpdateTrade(List<Trade> tradeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (Trade trade in tradeList)
                    {
                        Trade result = alading.Trade.Where(p => p.CustomTid == trade.CustomTid).FirstOrDefault();
                        if (result == null)
                        {
                            return ReturnType.NotExisted;
                        }
                        TradeCopydata(result, trade);
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
        /// CombineTradeList 合并单的custom_tid列表
        /// </summary>TradeCustomList 单个单的custom_tid列表
        /// <param name="CombineTradeList"></param>
        /// <param name="TradeCustomList"></param>
        /// <returns></returns>
        public ReturnType UpdateTrade(List<string> CombineTradeList, List<string> TradeCustomList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    #region 单个订单处理
                    foreach (string custom_tid in TradeCustomList)
                    {
                        Trade trade = alading.Trade.Where(q => q.CustomTid == custom_tid).FirstOrDefault();
                        if (trade != null)
                        {   /*修改本地信息*/
                            trade.LocalStatus = LocalTradeStatus.HasNotSummit;
                            trade.status = TradeEnum.WAIT_SELLER_SEND_GOODS;
                        }
                    }
                    #endregion

                    #region 合并单处理
                    foreach (string custom_tid in CombineTradeList)
                    {
                        Trade trade = alading.Trade.Where(q => q.CustomTid == custom_tid).FirstOrDefault();
                        if (trade != null)
                        {
                            List<Trade> TradeList = alading.Trade.Where(q => q.CombineCode == trade.CombineCode).ToList();
                            if (TradeList.Count > 0)
                            {
                                 foreach (Trade TradeObj in TradeList)
                                {   /*将子交易的CombineCode 制空，LocalStatus 还原为HasNotSummit*/
                                    TradeObj.CombineCode = string.Empty;
                                    TradeObj.LocalStatus = LocalTradeStatus.HasNotSummit;
                                 }/*foreach*/

                                  TradeOrder trade_order = alading.TradeOrder.Where(q => q.CustomTid == custom_tid).FirstOrDefault();
                                  alading.DeleteObject(trade);
                                  alading.DeleteObject(trade_order);
                            }
                        }/*if*/
                    }/*foreach*/
                    #endregion

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

        public ReturnType UpdateTrade(string tradeCode, Trade trade)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.Trade.Where(p => p.TradeID == tradeID).ToList();*/
                    var result = alading.Trade.Where(p => p.CustomTid == tradeCode).FirstOrDefault();
                    if (result!=null)
                    {
                        return ReturnType.NotExisted;
                    }
                  
                    TradeCopydata(result,trade);
                    
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

        public ReturnType UpdateTradeLocalStatus(string CustomTid, string LocalStatus)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*Trade result = alading.Trade.Where(p => p.TradeID == trade.TradeID).FirstOrDefault();*/
                    Trade result = alading.Trade.Where(p => p.CustomTid == CustomTid).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }

                    result.LocalStatus = LocalStatus;//更新本地状态

                    alading.SaveChanges();                   
                    return ReturnType.Success;                   
                    
                }
            }            
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType UpdateTradeLocalStatus(string CustomTid, string LocalStatus,string shippingCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*Trade result = alading.Trade.Where(p => p.TradeID == trade.TradeID).FirstOrDefault();*/
                    Trade result = alading.Trade.Where(p => p.CustomTid == CustomTid).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }

                    result.LocalStatus = LocalStatus;//更新本地状态
                    result.ShippingCode = shippingCode;//更新物流单号

                    alading.SaveChanges();
                    return ReturnType.Success;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReturnType UpdateTradeLocalStatus(List<string> customTidList, string LocalStatus)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*Trade result = alading.Trade.Where(p => p.TradeID == trade.TradeID).FirstOrDefault();*/
                    var resultTrade = alading.Trade.Where(BuildWhereInExpression<Trade, string>(v => v.CustomTid, customTidList));
                    if (resultTrade == null || resultTrade.Count() == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                    foreach (Trade trade in resultTrade)
                    {
                        trade.LocalStatus = LocalStatus;//更新本地状态                      
                    }                  
                 
                    alading.SaveChanges();
                    return ReturnType.Success;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 更新本地状态并出库
        /// </summary>
        /// <param name="customTidList"></param>
        /// <param name="LocalStatus"></param>
        /// <param name="OrderList"></param>
        /// <returns></returns>
        public ReturnType UpdateTradeLocalStatus(string userCode,string userName,List<string> customTidList, string LocalStatus, List<TradeOrder> OrderList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*Trade result = alading.Trade.Where(p => p.TradeID == trade.TradeID).FirstOrDefault();*/
                    var resultTrade = alading.Trade.Where(BuildWhereInExpression<Trade, string>(v => v.CustomTid, customTidList));
                    if (resultTrade == null || resultTrade.Count()==0)
                    {
                        return ReturnType.NotExisted;
                    }

                    System.Collections.Hashtable tradeInoutCodeList = new System.Collections.Hashtable();
                    foreach (Trade trade in resultTrade)
                    {
                        trade.LocalStatus = LocalStatus;//更新本地状态

                        #region 添加到StockInOut
                        StockInOut stockInOut = new StockInOut();
                        stockInOut.AmountTax = 0;
                        stockInOut.DiscountFee = 0;
                        stockInOut.DueFee = 0;
                        stockInOut.FreightCode = string.Empty;
                        stockInOut.FreightCompany = string.Empty;
                        stockInOut.InOutCode = Guid.NewGuid().ToString();

                        tradeInoutCodeList.Add(trade.CustomTid,stockInOut.InOutCode);

                        stockInOut.InOutTime = DateTime.Now;
                        stockInOut.InOutType = (int)InOutType.SaleOut;
                        stockInOut.TradeOrderCode = trade.CustomTid;
                        stockInOut.OperatorCode = userCode;
                        stockInOut.OperatorName = userName;
                        stockInOut.PayType = (int)PayType.CASH;
                        stockInOut.IsSettled = true;
                        stockInOut.PayTerm = 0;
                        stockInOut.IncomeTime = DateTime.MinValue;
                        stockInOut.PayThisTime = 0;

                        alading.AddToStockInOut(stockInOut);

                        #endregion
                    }
                    var q = from i in OrderList
                            select i.oid;
                    var resultOrder = alading.TradeOrder.Where(BuildWhereInExpression<TradeOrder, string>(v => v.oid, q.Distinct().ToList()));
                    if (resultOrder == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    foreach (TradeOrder order in resultOrder)
                    {
                        TradeOrder src = OrderList.FirstOrDefault(i=>i.oid==order.oid);
                        if (src == null)
                            continue;
                        order.HouseCode = src.HouseCode;
                        order.LayoutCode = src.LayoutCode;
                        //库位一定要有
                        StockHouseProduct product = alading.StockHouseProduct.FirstOrDefault(i => i.SkuOuterID == order.outer_sku_id && i.HouseCode == src.HouseCode && i.LayoutCode == src.LayoutCode);
                        if (product != null)
                        {
                            product.Num -= order.num;

                            #region 添加到StockDetail
                            StockDetail stockDetail = new StockDetail();
                            stockDetail.Tax =string.Empty;
                            stockDetail.TotalFee = 0;
                            stockDetail.DetailRemark = string.Empty;
                            stockDetail.DurabilityDate = DateTime.Now;
                            if (tradeInoutCodeList.ContainsKey(order.CustomTid))
                            {
                                stockDetail.InOutCode = tradeInoutCodeList[order.CustomTid].ToString();
                            }
                            stockDetail.Price = 0;
                            stockDetail.Quantity = order.num;
                            stockDetail.ProductSkuOuterId = product.SkuOuterID;
                            stockDetail.DetailType = (int)DetailType.TaobaoSaleOut;
                            stockDetail.StockDetailCode = Guid.NewGuid().ToString();
                            stockDetail.StockHouseCode = src.HouseCode;
                            stockDetail.StockLayOutCode = src.LayoutCode;

                            alading.AddToStockDetail(stockDetail);

                            #endregion
                        }                       
                    }

                    //#region 出库
                    //var OrderOut = alading.TradeOrder.Where(BuildWhereInExpression<TradeOrder, string>(v => v.CustomTid, customTidList));
                    //if (OrderOut == null || OrderOut.Count() == 0)
                    //{
                    //    return ReturnType.NotExisted;
                    //}
                    //var qSku = from i in OrderOut
                    //        select i.outer_sku_id;

                    //var resultProduct = alading.StockHouseProduct.Where(BuildWhereInExpression<StockHouseProduct, string>(v => v.SkuOuterID, qSku.Distinct()));
                    //if (resultProduct == null || resultProduct.Count() == 0)
                    //{
                    //    return ReturnType.NotExisted;
                    //}
                    //foreach (TradeOrder order in OrderOut)
                    //{
                    //    StockHouseProduct product = resultProduct.FirstOrDefault(i => i.SkuOuterID == order.outer_sku_id);
                    //    if (product != null)
                    //    {
                    //        product.Num -= order.num;
                    //    }
                    //}
                    //#endregion

                    //#region 出库详情
                    //foreach (Trade trade in resultTrade)
                    //{



                    //    foreach (TradeOrder tOrder in OrderOut)
                    //    {

                    //    }
                    //}

                    //#endregion


                    alading.SaveChanges();
                    return ReturnType.Success;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Trade> GetAllTrade()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Trade> list = alading.Trade.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
        public List<Trade> GetTrade(Func<Trade, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Trade> list = alading.Trade.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
     
        public Trade GetTrade(string customtid)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<Trade> list = alading.Trade.Where(p => p.TradeID == tradeID).ToList();*/
                    List<Trade> list = alading.Trade.Where(p => p.CustomTid == customtid).ToList();
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
        
        public List<Trade> GetTrade(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {                    
					var ob = (from u in alading.Trade orderby u.TradeID descending select u).Skip((pageIndex-1) * pageSize).Take(pageSize);
                    rowCount = alading.Trade.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        public List<Trade> GetTrade(Func<Trade, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<Trade> list = alading.Trade.Where(func).OrderByDescending(a=>a.TradeID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /*同时更新Trade TradeOrder Consumer*/
        public ReturnType TradeOrderConsumer(List<Alading.Entity.Trade> tradeAddFullInfoList, List<Alading.Entity.Trade> tradeUpdateFullInfoList
            , List<Alading.Entity.TradeOrder> orderAddList, List<Alading.Entity.TradeOrder> orderUpdateList
            , List<Alading.Entity.Consumer> consumerAddList, List<Alading.Entity.Consumer> consumerUpdateList, List<string> customtidList) 
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*添加到Trade表*/
                    foreach (Alading.Entity.Trade trade in tradeAddFullInfoList)
                    {
                        alading.AddToTrade(trade);
                    }

                    /*添加到TradeOrder表*/
                    foreach (Alading.Entity.TradeOrder tradeOrder in orderAddList)
                    {
                        alading.AddToTradeOrder(tradeOrder);
                    }

                    /*添加到TradeOrder表*/
                    foreach (Alading.Entity.Consumer consumer in consumerAddList)
                    {
                        alading.AddToConsumer(consumer);
                    }

                    /*更新到Trade表*/
                    var resultTrade = alading.Trade.Where(BuildWhereInExpression<Trade, string>(v => v.CustomTid, customtidList));
                    foreach (Alading.Entity.Trade trade in tradeUpdateFullInfoList)
                    {
                        var resultTemp = resultTrade.Where(p => p.CustomTid == trade.CustomTid).FirstOrDefault();
                        if (resultTemp != null)
                        {
                            continue;
                        }
                        TradeCopydata(resultTemp, trade);
                    }

                    /*更新到TradeOrder表*/
                    var resultTradeOrder = alading.TradeOrder.Where(BuildWhereInExpression<TradeOrder, string>(v => v.CustomTid, customtidList));
                    foreach (Alading.Entity.TradeOrder tradeOrder in orderUpdateList)
                    {
                        var resultTemp = resultTradeOrder.Where(p => p.outer_sku_id == tradeOrder.outer_sku_id).FirstOrDefault();
                        if (resultTemp != null)
                        {
                            continue;
                        }
                        copyTradeOrder(resultTemp, tradeOrder);
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
        /// 获取交易数据
        /// </summary>
        /// <param name="status">交易状态</param>
        /// <param name="seller_rate">卖家是否已评价</param>
        /// <returns></returns>
        public List<Trade> GetTrade(string status, bool seller_rate)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    return alading.Trade.Where(p => p.status == status && p.seller_rate == seller_rate).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        //通过传CustomTid的方法在存储过程中实现交易的提交
        public int SummitCommonTrade(string customTid, string oldStatus, string newStatus, string lockUser)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    string connectionString = ((EntityConnection)alading.Connection).StoreConnection.ConnectionString;
                    //选取执行存储过程
                    SqlCommand cmd = new SqlCommand("SP_SummitCommonTrade",
                        new SqlConnection(connectionString));

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customTid", customTid);
                    cmd.Parameters.AddWithValue("@oldStatus", oldStatus);
                    cmd.Parameters.AddWithValue("@newStatus", newStatus);
                    cmd.Parameters.AddWithValue("@lockUser", lockUser);
                    //传入输出参数 获得返回值
                    SqlParameter returnValue =
                        cmd.Parameters.AddWithValue("@returnValue",new int());
                    returnValue.Direction = ParameterDirection.Output;

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();

                    return (int)returnValue.Value;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<Trade> GetTradesByStatus(string localStatus,string status)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Trade> list = alading.SP_GetTradeByStstus(localStatus, status).ToList();
                    return list;
                }
            }
            catch(System.Exception ex)
            {
                throw ex;
            }
        }

         //通过传CustomTid的方法在存储过程中实现交易的取消
        public int RebackCommonTrade(string customTid, string oldStatus, string newStatus)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    string connectionString = ((EntityConnection)alading.Connection).StoreConnection.ConnectionString;
                    //选取执行存储过程
                    SqlCommand cmd = new SqlCommand("SP_RebackCommonTrade",
                        new SqlConnection(connectionString));

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customTid", customTid);
                    cmd.Parameters.AddWithValue("@oldStatus", oldStatus);
                    cmd.Parameters.AddWithValue("@newStatus", newStatus);
                    //传入输出参数 获得返回值
                    SqlParameter returnValue =
                        cmd.Parameters.AddWithValue("@returnValue",new int());
                    returnValue.Direction = ParameterDirection.Output;

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();

                    return (int)returnValue.Value;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

         //通过传CustomTid的方法在存储过程中实现交易的提交
        public int SummitCombineTrade(string customTidList, string oldStatus, string combineCode, string specialStatus,string lockUser)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    string connectionString = ((EntityConnection)alading.Connection).StoreConnection.ConnectionString;
                    //选取执行存储过程
                    SqlCommand cmd = new SqlCommand("SP_SummitCombineTrade",
                        new SqlConnection(connectionString));

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customTidList", customTidList);
                    cmd.Parameters.AddWithValue("@oldStatus", oldStatus);
                    cmd.Parameters.AddWithValue("@combineCode ", combineCode);
                    cmd.Parameters.AddWithValue("@specialStatus", specialStatus);
                    cmd.Parameters.AddWithValue("@lockUser", lockUser);
                    //传入输出参数 获得返回值
                    SqlParameter returnValue =
                        cmd.Parameters.AddWithValue("@returnValue",new int());
                    returnValue.Direction = ParameterDirection.Output;

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();

                    return (int)returnValue.Value;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        
        /// <summary>
        /// ReturnValue:1:更新成功  2:物流单号已经被占用 3：交易状态已经改变
        /// </summary>
       //通过传CustomTid的方法在存储过程中实现交易的提交
        public int SummitShippingCode(string customTid, string oldStatus, string newStatus, string shippingCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    string connectionString = ((EntityConnection)alading.Connection).StoreConnection.ConnectionString;
                    //选取执行存储过程
                    SqlCommand cmd = new SqlCommand("SP_SummitShippingCode",
                        new SqlConnection(connectionString));

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customTid", customTid);
                    cmd.Parameters.AddWithValue("@oldStatus", oldStatus);
                    cmd.Parameters.AddWithValue("@newStatus", newStatus);
                    cmd.Parameters.AddWithValue("@shippingCode", shippingCode);
                    //传入输出参数 获得返回值
                    SqlParameter returnValue =
                        cmd.Parameters.AddWithValue("@returnValue", new int());
                    returnValue.Direction = ParameterDirection.Output;

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();

                    return (int)returnValue.Value;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


    }
}

