using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Alading.Taobao.Entity.Extend
{
    [Serializable]
    [JsonObject]
    public class ProductRsp
    {
        /// <summary>
        /// 返回图片url
        /// url 必需
        /// </summary>
        [JsonProperty("url")]
        public string Url
        {
            get;
            set;
        }
        /// <summary>
        /// 返回产品图片id
        /// id必需
        /// </summary>
        [JsonProperty("id")]
        public int Id
        {
            get;
            set;
        }
        /// <summary>
        /// 返回添加时间created
        /// created 必需
        /// </summary>
        [JsonProperty("created")]
        public string Created
        {
            get;
            set;
        }
        /// <summary>
        /// 返回修改时间modified
        /// modified 必需
        /// </summary>
        [JsonProperty("modified")]
        public string Modified
        {
            get;
            set;
        }

        /// <summary>
        /// 返回product_id
        /// product_id 必需
        /// </summary>
        [JsonProperty("product_id")]
        public int ProductId
        {
            get;
            set;
        }

        /// <summary>
        /// 返回产品图片image
        /// image 必需
        /// </summary>
        [JsonProperty("image")]
        public byte Image
        {
            get;
            set;
        }

        /// <summary>
        /// 返回产品图片position
        /// </summary>
        [JsonProperty("position")]
        public int Position
        {
            get;
            set;
        }

        /// <summary>
        /// 返回图片is_major
        /// </summary>
       [JsonProperty("is_major")]
        public Boolean IsMajor
        {
            get;
            set;
        }

        /// <summary>
        /// 返回结果总数total_results
        /// </summary>
        [JsonProperty("total_results")]
        public int TotalResults
        {
            get;
            set;
        }

        /// <summary>
        /// 返回所有products
        ///products 必需
        /// </summary>
        [JsonProperty("products")]
        public Products Products
        {
            get;
            set;
        }

        /// <summary>
        /// 返回所有product
        ///products 必需
        /// </summary>
        [JsonProperty("product")]
        public Product Product
        {
            get;
            set;
        }
    }

    [Serializable]
    [JsonObject]
    public class Products
    {
        /// <summary>
        /// Product列表
        /// </summary>
        [JsonProperty("product")]
        public Product[] Product
        {
            get;
            set;
        }
    }
}
