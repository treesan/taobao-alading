using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 店铺类目
    /// </summary>
    [Serializable]
    [JsonObject("shop_cat")]
    [XmlRoot("shop_cat")]
    public class ShopCat : BaseObject
    {
        /// <summary>
        /// 卖家自定义类目编号
        /// </summary>
        [JsonProperty("cid")]
        [XmlElement("cid")]
        public string Cid { get; set; }

        /// <summary>
        /// 父类目编号
        /// </summary>
        [JsonProperty("parent_cid")]
        [XmlElement("parent_cid")]
        public string ParentCid { get; set; }

        /// <summary>
        /// 卖家自定义类目名称
        /// </summary>
        [JsonProperty("name")]
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// 该类目是否为父类目
        /// </summary>
        [JsonProperty("is_parent")]
        [XmlElement("is_parent")]
        public bool IsParent { get; set; }
    }
}