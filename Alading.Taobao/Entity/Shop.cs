using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 店铺
    /// </summary>
    [Serializable]
    [JsonObject("shop")]
    [XmlRoot("shop")]
    public class Shop : BaseObject
    {
        /// <summary>
        /// 店铺编号
        /// </summary>
        [JsonProperty("sid")]
        [XmlElement("sid")]
        public string Sid { get; set; }

        /// <summary>
        /// 店铺所属的类目编号
        /// </summary>
        [JsonProperty("cid")]
        [XmlElement("cid")]
        public string Cid { get; set; }

        /// <summary>
        /// 卖家昵称
        /// </summary>
        [JsonProperty("nick")]
        [XmlElement("nick")]
        public string SellerNick { get; set; }

        /// <summary>
        /// 店铺标题
        /// </summary>
        [JsonProperty("title")]
        [XmlElement("title")]
        public string Title { get; set; }

        /// <summary>
        /// 店铺描述
        /// </summary>
        [JsonProperty("desc")]
        [XmlElement("desc")]
        public string Description { get; set; }

        /// <summary>
        /// 店铺公告
        /// </summary>
        [JsonProperty("bulletin")]
        [XmlElement("bulletin")]
        public string Bulletin { get; set; }

        /// <summary>
        /// 店标地址
        /// </summary>
        [JsonProperty("pic_path")]
        [XmlElement("pic_path")]
        public string PicPath { get; set; }

        ///// <summary>
        ///// 开店时间
        ///// </summary>
        //[JsonProperty("created")]
        //[XmlElement("created")]
        //public DateTime Created { get; set; }

        ///// <summary>
        ///// 最后修改时间
        ///// </summary>
        //[JsonProperty("modified")]
        //[XmlElement("modified")]
        //public DateTime Modified { get; set; }

        /// <summary>
        /// 剩余橱窗推荐数
        /// </summary>
        [JsonProperty("remain_count")]
        [XmlElement("remain_showcase")]
        public int RemainShowcase { get; set; }
    }
}
