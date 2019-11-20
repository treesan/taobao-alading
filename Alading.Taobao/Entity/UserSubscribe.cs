using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Alading.Taobao.Entity
{
     [Serializable]
     [JsonObject]
    public class UserSubscribeRsp
    {
         /// <summary>
        /// 查询appstore应用订购关系
         /// </summary>
         [JsonProperty("user_subscribe")]
         public UserSubscribe UserSubscribe { get; set; }
    }

    /// <summary>
    /// 查询appstore应用订购关系
    /// </summary>
    [Serializable]
    [JsonObject]
    public class UserSubscribe : BaseObject
    {
        /// <summary>
        /// 订购结束时间。格式:yyyy-MM-dd HH:mm:ss
        /// </summary>
        [JsonProperty("end_date")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 订购开始时间。格式:yyyy-MM-dd HH:mm:ss 
        /// </summary>
        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 订购状况。应用订购者：subscribeUser;尚未订购：unsubscribeUser；非法用户：invalidateUser 
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
