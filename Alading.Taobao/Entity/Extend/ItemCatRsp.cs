using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Alading.Taobao.Entity.Extend
{
        [Serializable]
        [JsonObject]
        public class ItemCatRsp
        {
            /// <summary>
            ///  最近修改时间
            /// </summary>
            [JsonProperty("last_modified")]
            public string LastModified
            {
                get;
                set;
            }

            /// <summary>
            /// 返回所有的items
            /// </summary>
            [JsonProperty("prop_values")]
            public PropValues  PropValues
            {
                get;
                set;
            }
          
            /// <summary>
            /// 返回所有的item_props
            /// </summary>
            [JsonProperty("item_props")]
            public ItemProps ItemProps
            {
                get;
                set;
            }

            /// <summary>
            /// 类目列表
            /// </summary>
            [JsonProperty("item_cats")]
            public ItemCats ItemCats { get; set; }

            /// <summary>
            /// 授权列表
            /// </summary>
            [JsonProperty("seller_authorize")]
            public SellerAuthorize SellerAuthorize
            {
                get;
                set;
            }
        }

        [Serializable]
        [JsonObject]
        public class ItemProps
        {
            /// <summary>
            /// ItemProp列表
            /// </summary>
            [JsonProperty("item_prop")]
            public ItemProp[] ItemProp
            {
                get;
                set;
            }
        }

        [Serializable]
        [JsonObject]
        public class PropValues
        {
            /// <summary>
            /// Item列表
            /// </summary>
            [JsonProperty("prop_value")]
            public PropValue[] PropValue { get; set; }
        }

        [Serializable]
        [JsonObject]
        public class Brands
        {
            /// <summary>
            /// 品牌列表
            /// </summary>
            [JsonProperty("brand")]
            public Brand[] Brand
            {
                get;
                set;
            }
        }

        [Serializable]
        [JsonObject]
        public class ItemCats
        {
            /// <summary>
            /// ItemCat列表
            /// </summary>
            [JsonProperty("item_cat")]
            public ItemCat[] ItemCat
            {
                get;
                set;
            }
        }

        [Serializable]
        [JsonObject]
         public class SellerAuthorize
         {
              /// <summary>
            /// 品牌列表
            /// </summary>
            [JsonProperty("brands")]
            public  Brands Brands{get;set;}

            /// <summary>
            /// 类目列表
            /// </summary>
            [JsonProperty("item_cats")]
            public ItemCats ItemCats { get; set; }
         }
   
}
