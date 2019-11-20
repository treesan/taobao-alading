using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Xml;

namespace Alading.Taobao.Entity.Extend
{
    public class ShippingReq:Shipping
    {
        /// <summary>
        /// 发货类型. 可选( delivery_needed(物流订单发货),virtual_goods(虚拟物品发货). ) 
        /// 注:选择virtual_goods类型进行发货的话下面的参数可以不需填写。 
        /// </summary>
        [JsonProperty("order_type")]
        public string OrderType{ get; set; }
        
        /// <summary>
        /// 物流公司代码.如"POST"就代表中国邮政,"ZJS"就代表宅急送
        /// </summary>
        [JsonProperty("company_code")]
        public string CompanyCode{ get; set; }

        /// <summary>
        /// 卖家姓名
        /// </summary>
        [JsonProperty("seller_name")]
        public string SellerName{ get; set; }

        /// <summary>
        /// 卖家所在地国家公布的标准地区码.
        /// 参考:http://www.stats.gov.cn/tjbz/xzqhdm/t20080215_402462675.htm 
        /// 或者调用 taobao.areas.get 获取 
        /// </summary>
        [JsonProperty("seller_area_id")]
        public string SellerAreaId{ get; set; }

        /// <summary>
        /// 卖家地址(详细地址).如:XXX街道XXX门牌,省市区不需要提供.
        /// </summary>
        [JsonProperty("seller_address")]
        public string SellerAddress{ get; set; }

        /// <summary>
        /// 卖家邮编 
        /// </summary>
        [JsonProperty("seller_zip ")]
        public string SellerZip{ get; set; }

        /// <summary>
        /// 卖家固定电话.包含区号,电话,分机号,中间用 
        /// " – "; 卖家固定电话和卖家手机号码,必须填写一个. 
        /// </summary>
        [JsonProperty("seller_phone")]
        public string SellerPhone{ get; set; }

        /// <summary>
        /// 卖家手机号码 
        /// </summary>
        [JsonProperty("seller_mobile")]
        public string SellerMobile{ get; set; }

        /// <summary>
        /// 卖家备注.最大长度为250个字符. 
        /// </summary>
        [JsonProperty("memo")]
        public string Memo{ get; set; }

        /// <summary>
        /// 创建时间开始.格式:yyyy-MM-dd HH:mm:ss 
        /// </summary>
        [JsonProperty("start_created")]
        public string StartCreated { get; set; }

        /// <summary>
        /// 创建时间结束.格式:yyyy-MM-dd HH:mm:ss  
        /// </summary>
        [JsonProperty("end_created")]
        public string EndCreated{ get; set; }

        /// <summary>
        /// 页码.该字段没传 或 值<1 ,则默认page_no为1 
        /// </summary>
        [JsonProperty("page_no")]
        public int PageNo{ get; set; }

        /// <summary>
        /// 每页条数.该字段没传 或 值<1 ,则默认page_size为40 
        /// </summary>
        [JsonProperty("page_size")]
        public string PageSize{ get; set; }
    }
}
