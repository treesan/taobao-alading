using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 商品类目
    /// </summary>
    [Serializable]
    [JsonObject("item_cat")]
    [XmlRoot("item_cat")]
    public class ItemCat : BaseObject
    {
        /// <summary>
        /// 类目编号
        /// </summary>
        [JsonProperty("cid")]
        [XmlElement("cid")]
        public string Cid { get; set; }

        /// <summary>
        /// 类目编号。兼容TOP有两个商品类目类的错误
        /// </summary>
        [JsonProperty("category_id")]
        [XmlElement("category_id")]
        public string CategoryId
        {
            get { return this.Cid; }
            set { this.Cid = value; }
        }

        /// <summary>
        /// 类目名称
        /// </summary>
        [JsonProperty("name")]
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// 类目名称。兼容TOP有两个商品类目类的错误
        /// </summary>
        [JsonProperty("category_name")]
        [XmlElement("category_name")]
        public string CategoryName
        {
            get { return this.Name; }
            set { this.Name = value; }
        }

        /// <summary>
        /// 父类目编号，0表示一级目录
        /// </summary>
        [JsonProperty("parent_cid")]
        [XmlElement("parent_cid")]
        public string ParentCid { get; set; }

        /// <summary>
        /// 该类目是否为父类目
        /// </summary>
        [JsonProperty("is_parent")]
        [XmlElement("is_parent")]
        public bool IsParent { get; set; }

        /// <summary>
        /// 类目状态。可选值：normal(正常)，deleted(删除)
        /// </summary>
        [JsonProperty("status")]
        [XmlElement("status")]
        public string Status { get; set; }

        /// <summary>
        /// 类目排列序号
        /// </summary>
        [JsonProperty("sort_order")]
        [XmlElement("sort_order")]
        public int SortOrder { get; set; }
     
    }
}
