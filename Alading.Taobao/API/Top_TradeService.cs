using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Alading.Taobao.API.Common;
using Alading.Taobao.Entity;
using Alading.Taobao.Entity.Extend;

namespace Alading.Taobao.API
{
    public partial class TopService
    {
        /// <summary>
        /// 搜索当前会话用户作为买家达成的交易记录
        /// 所有参数 都可以为空
        /// </summary>
        public static TradeRsp TradesBoughtGet(string session, TradeReq tradereq)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.trades.bought.get");
                paramsTable.Add("fields", "seller_nick,buyer_nick,title,type,created,sid,tid,seller_rate,buyer_rate,status,payment,discount_fee,adjust_fee,post_fee,total_fee,pay_time,end_time,modified,consign_time,buyer_obtain_point_fee,point_fee,real_point_fee,received_payment,commission_fee,alipay_no,pic_path,iid,num,price,cod_fee,cod_status,shipping_type,orders.title,orders.pic_path,orders.price,orders.num,orders.iid,orders.sku_id,orders.refund_status,orders.status,orders.oid,orders.total_fee,orders.payment,orders.discount_fee,orders.adjust_fee,orders.sku_properties_name,orders.item_meal_name,orders");
                paramsTable.Add("start_created", tradereq.StartCreated);
                paramsTable.Add("end_created", tradereq.EndCreated);
                paramsTable.Add("status", tradereq.Status);
                paramsTable.Add("title", tradereq.Title);
                paramsTable.Add("seller_nick", tradereq.SellerNick);
                paramsTable.Add("page_no", tradereq.PageNo);
                paramsTable.Add("page_size", tradereq.PageSize);
                paramsTable.Add("type", tradereq.Type);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<TradeRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }
      
        /// <summary>
        /// 搜索当前会话用户作为卖家已卖出的交易数据
        /// 所有参数都可以为空
        /// </summary>
        /// <param name="tradereq"></param>
        /// <returns></returns>
        public static TradeRsp TradesSoldGet(string session, TradeReq tradereq)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.trades.sold.get");
                paramsTable.Add("fields", "seller_nick,buyer_nick,title,type,tid,status");
                paramsTable.Add("start_created", tradereq.StartCreated);
                paramsTable.Add("end_created", tradereq.EndCreated);
                paramsTable.Add("page_no", tradereq.PageNo);
                paramsTable.Add("page_size", tradereq.PageSize);
                paramsTable.Add("title", tradereq.Title);
                paramsTable.Add("status", tradereq.Status);
                paramsTable.Add("buyer_nick", tradereq.BuyerNick);
                paramsTable.Add("rate_status", tradereq.RateStatus);
                paramsTable.Add("type", tradereq.Type);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<TradeRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 搜索当前会话用户作为卖家已卖出的增量交易数据
        /// start_modified,end_modified 必须填，其余可以为空
        /// </summary>
        public static TradeRsp TradesSoldIncrementGet(string session, TradeReq tradereq)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.trades.sold.increment.get");
                paramsTable.Add("fields", "seller_nick,buyer_nick,title,type,created,sid,tid,seller_rate,buyer_rate,status,payment,discount_fee,adjust_fee,post_fee,total_fee,pay_time,end_time,modified,consign_time,buyer_obtain_point_fee,point_fee,real_point_fee,received_payment,commission_fee,alipay_no,pic_path,iid,num,price,cod_fee,cod_status,shipping_type,orders.title,orders.pic_path,orders.price,orders.num,orders.iid,orders.sku_id,orders.refund_status,orders.status,orders.oid,orders.total_fee,orders.payment,orders.discount_fee,orders.adjust_fee,orders.sku_properties_name,orders.item_meal_name,orders");
                paramsTable.Add("start_modified", tradereq.StartCreated);
                paramsTable.Add("end_modified", tradereq.EndCreated);
                paramsTable.Add("status", tradereq.Status);
                paramsTable.Add("page_no", tradereq.PageNo);
                paramsTable.Add("page_size", tradereq.PageSize);
                paramsTable.Add("type", tradereq.Type);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<TradeRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// 获取单笔交易的部分信息(性能高)
        /// 参数 全部必填
        /// </summary>
        public static TradeRsp TradeGet(string session, string tid)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.trades.get");
                paramsTable.Add("fields", "seller_nick,buyer_nick,title,type,created,sid,tid,seller_rate,buyer_rate,status,payment,discount_fee,adjust_fee,post_fee,total_fee,pay_time,end_time,modified,consign_time,buyer_obtain_point_fee,point_fee,real_point_fee,received_payment,commission_fee,buyer_memo,seller_memo,alipay_no,buyer_message,pic_path,iid,num,price,cod_fee,cod_status,shipping_type,orders.title,orders.pic_path,orders.price,orders.num,orders.iid,orders.sku_id,orders.refund_status,orders.status,orders.oid,orders.total_fee,orders.payment,orders.discount_fee,orders.adjust_fee,orders.sku_properties_name,orders.item_meal_name,orders.outer_sku_id,orders.outer_iid,orders");
                paramsTable.Add("tid", tid);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<TradeRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>
        /// 获取单笔交易的详细信息
        /// 参数必填
        /// </summary>
        public static TradeRsp TradeFullinfoGet(string session, string tid)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                string fileds = "orders,seller_nick,buyer_nick,title,type,created,sid,tid,seller_rate,buyer_rate,status,payment,discount_fee,adjust_fee,post_fee,total_fee,pay_time,end_time,modified,consign_time,buyer_obtain_point_fee,point_fee,real_point_fee,received_payment,commission_fee,buyer_memo,seller_memo,alipay_no,buyer_message,pic_path,iid,num,price,buyer_alipay_no,receiver_name,receiver_state,receiver_city,receiver_district,receiver_address,receiver_zip,receiver_mobile,receiver_phone,buyer_email,seller_alipay_no,seller_mobile,seller_phone,seller_name,seller_email,available_confirm_fee,has_post_fee,timeout_action_time,snapshot_url,cod_fee,cod_status,shipping_type,trade_memo,is_3D";
                paramsTable.Add("method", "taobao.trade.fullinfo.get");
                paramsTable.Add("fields", fileds);
                paramsTable.Add("tid", tid);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<TradeRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// 卖家关闭一笔交易
        /// 所有参数必填
        /// </summary>

        public static TradeRsp TradeClose(string session, string tid, string close_reason)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.trade.close");
                paramsTable.Add("tid", tid);
                paramsTable.Add("close_reason", close_reason);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<TradeRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            

        }
        /// <summary>
        /// 对一笔交易添加备注
        /// 所有参数必填
        /// </summary>
        /// <returns></returns>
        public static TradeRsp TradeMemoAdd(string session, string tid, string memo)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.trade.memo.add");
                paramsTable.Add("memo", memo);
                paramsTable.Add("tid", tid);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<TradeRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }
        /// <summary>
        /// 修改一笔交易备注
        /// 所有参数必填
        /// </summary>
        /// <returns></returns>
        public static TradeRsp TradeMemoUpdate(string session, string tid, string memo)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.trade.memo.update");
                paramsTable.Add("memo", memo);
                paramsTable.Add("tid", tid);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<TradeRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// 查询买家申请的退款列表
        /// 所有参数可以为空
        /// </summary>
        /// <returns></returns>
        public static TradeRsp RefundsApplyGet(string session, TradeReq tradereq)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.refunds.apply.get");
                paramsTable.Add("fields", "refund_id,tid,title,buyer_nick,seller_nick,total_fee,status,created,refund_fee");
                paramsTable.Add("status", tradereq.Status);
                paramsTable.Add("seller_nick", tradereq.SellerNick);
                paramsTable.Add("page_no", tradereq.PageNo);
                paramsTable.Add("page_size", tradereq.PageSize);
                paramsTable.Add("type", tradereq.Type);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<TradeRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }
        /// <summary>
        /// 查询卖家收到的退款列表
        /// 所有参数都可以为空
        /// </summary>
        /// <returns></returns>
        public static TradeRsp RefundsReceiveGet(string session, TradeReq tradereq)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.refunds.receive.get");
                paramsTable.Add("status", tradereq.Status);
                paramsTable.Add("buyer_nick", tradereq.BuyerNick);
                paramsTable.Add("page_no", tradereq.PageNo);
                paramsTable.Add("page_size", tradereq.PageSize);
                paramsTable.Add("fields", "refund_id,tid,title,buyer_nick,seller_nick,total_fee,status,created,refund_fee,oid,good_status,company_name,sid,payment,reason,desc,has_good_return,modified,order_status");
                paramsTable.Add("type", tradereq.Type);
                paramsTable.Add("start_modified", tradereq.StartModified);
                paramsTable.Add("end_modified", tradereq.EndModified);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<TradeRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }
        /// <summary>
        /// 查询卖家收到的退款列表
        /// 所有参数都可以为空
        /// </summary>
        /// <returns></returns>
        public static TradeRsp RefundsReceiveGet(string session,string fields, TradeReq tradereq)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.refunds.receive.get");
                paramsTable.Add("status", tradereq.Status);
                paramsTable.Add("buyer_nick", tradereq.BuyerNick);
                paramsTable.Add("page_no", tradereq.PageNo);
                paramsTable.Add("page_size", tradereq.PageSize);
                paramsTable.Add("fields", fields);
                paramsTable.Add("type", tradereq.Type);
                paramsTable.Add("start_modified", tradereq.StartModified);
                paramsTable.Add("end_modified", tradereq.EndModified);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<TradeRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 单笔退款详情
        /// 所以参数必填
        /// </summary>
        /// <returns></returns>
        public static TradeRsp RefundGet(string session, string refund_id)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.refund.get");
                paramsTable.Add("fields", "refund_id,alipay_no,tid,oid,buyer_nick,seller_nick,total_fee,status,created,refund_fee,good_status,has_good_return,payment,reason,desc,iid,title,price,num,good_return_time,company_name,sid,address,shipping_type,refund_remind_timeout");
                paramsTable.Add("refund_id", refund_id);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<TradeRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// 获取交易确认收货费用
        /// 所以参数必填
        /// </summary>
        /// <returns></returns>
        public static TradeConfirmFee TradeConfirmfeeGet(string session, string tid, string is_detail)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.trade.confirmfee.get");
                paramsTable.Add("tid", tid);
                paramsTable.Add("is_detail", is_detail);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<TradeConfirmFee>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// 创建退款留言/凭证
        /// refund_id,content必填 ，其余 可以不填
        /// </summary>
        /// <returns></returns>
        public static TradeRsp RefundMessageAdd(string session, string refund_id, string content, byte[] image)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.refund.message.add");
                paramsTable.Add("refund_id", refund_id);
                paramsTable.Add("content", content);
                paramsTable.Add("image", image);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<TradeRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        
        }
        /// <summary>
        /// 退款留言/凭证列表查询
        /// refund_id必填，其余可以为空
        /// </summary>
        /// <returns></returns>
        public static TradeRsp RefundMessagesGet(string session, string refund_id, int page_no, int page_size)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.refund.messages.get");
                paramsTable.Add("fields", "id,refund_id,owner_id,owner_nick,owner_role,content,pic_urls,created");
                paramsTable.Add("refund_id", refund_id);
                paramsTable.Add("page_no", page_no);
                paramsTable.Add("page_size", page_size);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<TradeRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }
        /// <summary>
        /// 交易快照查询
        /// 所以参数必填
        /// </summary>
        /// <returns></returns>
        public static Trade TradeSnapShotGet(string session, string tid)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.trade.snapshot.get");
                paramsTable.Add("fields", "snapshot");
                paramsTable.Add("tid", tid);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<Trade>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// 更改交易订单的销售属性
        /// oid必填，其余可以不填
        /// </summary>
        /// <returns></returns>s
        public static TradeRsp TradeOrderSkuUpdate(string session,string oid, string sku_id, string sku_props) 
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.trade.ordersku.update");
                paramsTable.Add("oid", oid);
                paramsTable.Add("sku_id", sku_id);
                paramsTable.Add("sku_props", sku_props);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<TradeRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>
        /// 更改交易订单的销售属性,按sku_props来更新
        /// oid必填，其余可以不填
        /// </summary>
        /// <returns></returns>
        public static TradeRsp TradeOrderSkuUpdate(string session, string oid, string  sku_props)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.trade.ordersku.update");
                paramsTable.Add("oid", oid);
                paramsTable.Add("sku_props", sku_props);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<TradeRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 更改交易的收货地址
        /// tid必填，其余可以不填
        /// </summary>
        /// <returns></returns>
        public static TradeRsp TradeShippingAddressUpdate(string session, Trade trade)  
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.trade.shippingaddress.update");
                paramsTable.Add("fields", "snapshot");
                paramsTable.Add("tid", trade.Tid);
                paramsTable.Add("receiver_name", trade.ReceiverName);
                paramsTable.Add("receiver_phone", trade.ReceiverPhone);
                paramsTable.Add("receiver_mobile", trade.ReceiverMobile);
                paramsTable.Add("receiver_state", trade.ReceiverState);
                paramsTable.Add("receiver_city", trade.ReceiverCity);
                paramsTable.Add("receiver_district", trade.ReceiverDistrict);
                paramsTable.Add("receiver_address", trade.ReceiverAddress);
                paramsTable.Add("receiver_zip", trade.ReceiverZip);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<TradeRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }
    }
}
