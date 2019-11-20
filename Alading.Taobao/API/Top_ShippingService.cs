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
        /// 查询地址区域
        ///fields 是必填项
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static ShippingRsp AreasGet()
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.areas.get");
                paramsTable.Add("fields", "id,type,name,parent_id,zip");
                return TopUtils.DeserializeObject<ShippingRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Public));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }
        /// <summary>
        /// 查询物流公司信息
        /// 除fields是必填项之外其余两项为选填
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="is_recommended"></param>
        /// <param name="order_mode"></param>
        /// <returns></returns>
        public static ShippingRsp LogisticCompaniesGet(Boolean is_recommended, string order_mode)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.logistics.companies.get");
                paramsTable.Add("fields", "company_id,company_code,company_name");
                paramsTable.Add("is_recommended", is_recommended);
                paramsTable.Add("order_mode", order_mode);
                return TopUtils.DeserializeObject<ShippingRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Public));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }
        /// <summary>
        /// 发货处理
        /// 所有项均为必填项
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="order_type"></param>
        /// <param name="company_code"></param>
        /// <param name="out_sid"></param>
        /// <param name="seller_name"></param>
        /// <param name="seller_area_id"></param>
        /// <param name="seller_address"></param>
        /// <param name="seller_zip"></param>
        /// <param name="seller_phone"></param>
        /// <param name="seller_mobile"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public static bool DeliverySend(string session, ShippingReq shippingreq)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.delivery.send");
                paramsTable.Add("fields", "is_success");
                paramsTable.Add("tid", shippingreq.Tid);
                paramsTable.Add("order_type", shippingreq.OrderType);
                paramsTable.Add("company_code", shippingreq.CompanyCode);
                paramsTable.Add("out_sid", shippingreq.OutSid);
                paramsTable.Add("seller_name", shippingreq.SellerName);
                paramsTable.Add("seller_area_id", shippingreq.SellerAreaId);
                paramsTable.Add("seller_address", shippingreq.SellerAddress);
                paramsTable.Add("seller_zip", shippingreq.SellerZip);
                paramsTable.Add("seller_phone", shippingreq.SellerPhone);
                paramsTable.Add("seller_mobile", shippingreq.SellerMobile);
                paramsTable.Add("memo", shippingreq.Memo);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<bool>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// 批量查询物流订单
        /// 除fields为必填项之外其余各项均为选填项
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="tid"></param>
        /// <param name="buyer_nick"></param>
        /// <param name="status"></param>
        /// <param name="seller_confirm"></param>
        /// <param name="receiver_name"></param>
        /// <param name="start_created"></param>
        /// <param name="end_created"></param>
        /// <param name="freight_payer"></param>
        /// <param name="type"></param>
        /// <param name="page_no"></param>
        /// <param name="page_size"></param>
        /// <returns></returns>
        public static ShippingRsp LogisticsOrdersGet(string session, ShippingReq shippingreq)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.logistics.orders.get");
                paramsTable.Add("fields", "tid,seller_nick,buyer_nick,delivery_start,delivery_end,out_sid,item_title,receiver_name,receiver_phone,receiver_mobile,receiver_location,status,type,freight_payer,seller_confirm,company_name");
                paramsTable.Add("tid", shippingreq.Tid);
                paramsTable.Add("buyer_nick", shippingreq.BuyerNick);
                paramsTable.Add("status", shippingreq.Status);
                paramsTable.Add("seller_confirm", shippingreq.SellerConfirm);
                paramsTable.Add("receiver_name", shippingreq.ReceiverName);
                paramsTable.Add("start_created", shippingreq.StartCreated);
                paramsTable.Add("end_created", shippingreq.EndCreated);
                paramsTable.Add("freight_payer", shippingreq.FreightPayer);
                paramsTable.Add("type", shippingreq.Type);
                paramsTable.Add("page_no", shippingreq.PageNo);
                paramsTable.Add("page_size", shippingreq.PageSize);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ShippingRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }
        /// <summary>
        /// 批量查询物流订单,返回详细信息
        /// 除fields为必填项之外其余各项均为选填项
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="tid"></param>
        /// <param name="buyer_nick"></param>
        /// <param name="status"></param>
        /// <param name="seller_confirm"></param>
        /// <param name="receiver_name"></param>
        /// <param name="start_created"></param>
        /// <param name="end_created"></param>
        /// <param name="freight_payer"></param>
        /// <param name="type"></param>
        /// <param name="page_no"></param>
        /// <param name="page_size"></param>
        /// <returns></returns>
        public static ShippingRsp LogisticsOrdersDetailGet(string session, ShippingReq shippingreq)
        {
            try
            {
                TopDictionary paramsTable = new TopDictionary();
                paramsTable.Add("method", "taobao.logistics.orders.detail.get");
                paramsTable.Add("fields", "tid,seller_nick,buyer_nick,delivery_start,delivery_end,out_sid,item_title,receiver_name,receiver_phone,receiver_mobile,receiver_location,status,type,freight_payer,seller_confirm,company_name");
                paramsTable.Add("tid", shippingreq.Tid);
                paramsTable.Add("buyer_nick", shippingreq.BuyerNick);
                paramsTable.Add("status", shippingreq.Status);
                paramsTable.Add("seller_confirm", shippingreq.SellerConfirm);
                paramsTable.Add("receiver_name", shippingreq.ReceiverName);
                paramsTable.Add("start_created", shippingreq.StartCreated);
                paramsTable.Add("end_created", shippingreq.EndCreated);
                paramsTable.Add("freight_payer", shippingreq.FreightPayer);
                paramsTable.Add("type", shippingreq.Type);
                paramsTable.Add("page_no", shippingreq.PageNo);
                paramsTable.Add("page_size", shippingreq.PageSize);
                paramsTable.Add("session", session);
                return TopUtils.DeserializeObject<ShippingRsp>(TopUtils.InvokeAPI(paramsTable, APIInvokeType.Private));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }
        
    }
}
