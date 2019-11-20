using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Xml;

namespace Alading.Taobao.Entity.Extend
{
    public class ItemReq:Item
    {
        /// <summary>
        /// 搜索字段.用来搜索商品的title以及商品所对应的产品的title
        /// </summary>
        [JsonProperty("q")]
        public string Q { get; set; }

        /// <summary>
        /// 页码。取值范围:大于零的整数; 默认值:1,即返回第一页数据。
        /// </summary>
        [JsonProperty("page_no")]
        public int PageNo { get; set; }

        /// <summary>
        /// 每页条数。取值范围:大于零的整数;最大值:200;默认值:40
        /// </summary>
        [JsonProperty("page_size")]
        public int PageSize { get; set; }

        /// <summary>
        /// 排序方式。格式为column:asc/desc,column
        /// 默认是按照上架时间倒序。如按价格升序排列表示为：
        /// price:asc。新增排序字段：volume（30天成交量） 
        /// </summary>
        [JsonProperty("order_by")]
        public string OrderBy { get; set; }

        /// <summary>
        /// 商品最低价格
        /// </summary>
        [JsonProperty("start_price")]
        public string StartPrice { get; set; }


        /// <summary>
        /// 商品最高价格
        /// </summary>
        [JsonProperty("end_price")]
        public int EndPrice { get; set; }

        /// <summary>
        /// 免运费
        /// </summary>
        [JsonProperty("post_free")]
        public int PostFree { get; set; }

        /// <summary>
        /// 旺旺在线状态
        /// </summary>
        [JsonProperty("ww_status")]
        public bool WwStatus { get; set; }

        /// <summary>
        /// 是否是3D淘宝的商品
        /// </summary>
        [JsonProperty("is_3D")]
        public bool Is3D { get; set; }


        /// <summary>
        /// 商品所属卖家的最小信用等级数
        /// </summary>
        [JsonProperty("start_score")]
        public int StartScore { get; set; }


        /// <summary>
        /// 商品所属卖家的最大信用等级数
        /// </summary>
        [JsonProperty("end_score")]
        public int EndScore { get; set; }


        /// <summary>
        /// 商品30天内最小销售数
        /// </summary>
        [JsonProperty("start_volume")]
        public int StartVolume { get; set; }



        /// <summary>
        /// 商品30天内最大销售数
        /// </summary>
        [JsonProperty("end_volume")]
        public int EndVolume { get; set; }


         /// <summary>
        /// 是否支持货到付款
        /// </summary>
        [JsonProperty("is_cod")]
        public bool IsCod { get; set; }



         /// <summary>
        /// 是否商城的商品
        /// </summary>
        [JsonProperty("is_mall")]
        public bool IsMall { get; set; }


        /// <summary>
        /// 是否如实描述(即:先行赔付)商品
        /// </summary>
        [JsonProperty("is_prepay")]
        public bool IsPrepay { get; set; }


        /// <summary>
        /// 是否正品保障商品
        /// </summary>
        [JsonProperty("genuine_security")]
        public bool GenuineSecurity { get; set; }

        /// <summary>
        /// 是否提供保障服务的商品
        /// </summary>
        [JsonProperty("promoted_service")]
        public bool PromotedService { get; set; }

       
        /// <summary>
        /// 分类字段
        /// </summary>
        [JsonProperty("banner")]
        public string Banner { get; set; }

        /// <summary>
        /// 商品图片
        /// </summary>
        [JsonProperty("image")]
        public string Image { get; set; }


        /// <summary>
        /// 商品文字的版本
        /// </summary>
        [JsonProperty("lang")]
        public string Lang { get; set; }

        /// <summary>
        /// 更新的Sku的属性串
        /// </summary>
        [JsonProperty("sku_properties")]
        public string SkuProperties { get; set; }


        /// <summary>
        /// 更新的Sku的价格串
        /// </summary>
        [JsonProperty("sku_prices")]
        public string Skuprices { get; set; }


        /// <summary>
        /// 更新的Sku的数量串
        /// </summary>
        [JsonProperty("sku _quantities")]
        public string SkuQuantities { get; set; }


        /// <summary>
        /// 更新的Sku的数量串
        /// </summary>
        [JsonProperty("sku_outer_ids")]
        public string SkuOuterIds { get; set; }


        /// <summary>
        /// 邮费模板名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 邮费模板备注 
        /// </summary>
        [JsonProperty("memo")]
        public string Memo { get; set; }

        /// <summary>
        /// 默认平邮加价费用.精确到1位小数;单位:元。如:10.5  
        /// </summary>
        [JsonProperty("post_increase ")]
        public string PostIncrease { get; set; }

        /// <summary>
        /// 默认快递加价费用.精确到1位小数;单位:元。如:10.5 
        /// </summary>
        [JsonProperty("express_increase  ")]
        public string ExpressIncrease { get; set; }

        /// <summary>
        /// 默认EMS加价费用.精确到1位小数;单位:元。如:10.5 
        /// </summary>
        [JsonProperty("ems_increase ")]
        public string EmsIncrease { get; set; }

        /// <summary>
        /// 运费子模板id
        /// </summary>
        [JsonProperty("postage_mode_ids")]
        public string PostageModeIds { get; set; }

        /// <summary>
        /// 运费方式:平邮(post),快递公司(express),EMS(ems)数量串:例(post;express;ems)分号区分 
        /// </summary>
        [JsonProperty("postage_mode_types")]
        public string PostageModeTypes { get; set; }

        /// <summary>
        ///邮费子项涉及的地区，多个地区用逗号连接数量串
        /// </summary>
        [JsonProperty("postage_mode_dests")]
        public string PostageModeDests { get; set; }

        /// <summary>
        ///运费方式单价数量串
        /// </summary>
        [JsonProperty("postage_mode_prices")]
        public string PostageModePrices { get; set; }

        /// <summary>
        ///运费方式加件费用数量串：例 (1.5;2.4;2.0).精确到1位小数;单位:元。如:10.5 
        /// </summary>
        [JsonProperty("postage_mode_increases ")]
        public string PostageModeIncreases { get; set; }

       

       
        /// <summary>
        ///属性图片ID。如果是新增不需要填写 
        /// </summary>
        [JsonProperty("id")]
        public int ProImgId { get; set; }

        /// <summary>
        ///图片序号 
        /// </summary>
        [JsonProperty("position")]
        public int Position { get; set; }

        /// <summary>
        ///商品图片id(如果是更新图片，则需要传该参数) 
        /// </summary>
        [JsonProperty("id")]
        public int ImageId { get; set; }

        /// <summary>
        ///是否将该图片设为主图,可选值:true,false;默认值:false(非主图) 
        /// </summary>
        [JsonProperty("is_major")]
        public bool IsMajor { get; set; }

   
      
    }
}
