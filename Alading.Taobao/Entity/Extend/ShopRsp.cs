using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Alading.Taobao.Entity.Extend
{
    [Serializable]
    [JsonObject]
    public class ShopRsp
    {
        /// <summary>
        /// 返回店铺编号
        /// </summary>
        [JsonProperty("sid")]
        public int Sid
        {
            get;
            set;
        }

        /// <summary>
        /// 返回店铺编号
        /// </summary>
        [JsonProperty("shop")]
        public Shop Shop
        {
            get;
            set;
        }

        /// <summary>
        /// 返回更新时间。格式：yyyy-MM-dd HH:mm:ss
        /// </summary>
        [JsonProperty("modified")]
        public string Modified
        {
            get;
            set;
        }

        /// <summary>
        /// 返回店铺类目列表信息
        /// </summary>
        [JsonProperty("shop_cats ")]
        public ShopCats ShopCats
        {
            get;
            set;
        }

        /// <summary>
        /// 返回卖家自定义类目
        /// </summary>
        [JsonProperty("seller_cats")]
        public SellerCats SellerCats
        {
            get;
            set;
        }

        /// <summary>
        /// 返回卖家自定义类目编号
        /// </summary>
        [JsonProperty("cid ")]
        public int Cid
        {
            get;
            set;
        }

        /// <summary>
        /// 返回创建时间。格式：yyyy-MM-dd HH:mm:ss
        /// </summary>
        [JsonProperty("created ")]
        public string Created
        {
            get;
            set;
        }
    }

    [Serializable]
    [JsonObject]
    public class ShopCats
    {
        /// <summary>
        /// ShopCat列表
        /// </summary>
        [JsonProperty("shop_cats")]
        public ShopCat[] ShopCat { get; set; }
    }

    [Serializable]
    [JsonObject]
    public class SellerCats
    {
        /// <summary>
        /// SellerCat列表
        /// </summary>
        [JsonProperty("seller_cat")]
        public SellerCat[] SellerCat { get; set; }
    }
}