using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// 商品视频列表(目前只支持单个视频关联)。
    /// </summary>
    [Serializable]
    [JsonObject("video")]
    [XmlRoot("video")]
    public class Video
    {
        /// <summary>
        /// 视频记录的id，和商品相对应
        /// </summary>
        [JsonProperty("id")]
        [XmlElement("id")]
        public int Id { get; set; }

        /// <summary>
        /// video的id，对应于视频在淘秀的存储记录
        /// </summary>
        [JsonProperty("video_id")]
        [XmlElement("video_id")]
        public int VideoId { get; set; }

        /// <summary>
        /// video的url连接地址
        /// </summary>
        [JsonProperty("url")]
        [XmlElement("url")]
        public string Url { get; set; }

        /// <summary>
        /// 视频创建时间（格式：yyyy-MM-dd HH:mm:ss
        /// </summary>
        [JsonProperty("created")]
        [XmlElement("created")]
        public string Created { get; set; }

        /// <summary>
        /// 视频修改时间（格式：yyyy-MM-dd HH:mm:ss）
        /// </summary>
        [JsonProperty("modified")]
        [XmlElement("modified")]
        public string Modified { get; set; }
    }
   
}
