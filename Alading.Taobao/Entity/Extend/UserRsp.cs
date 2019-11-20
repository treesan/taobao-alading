using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Alading.Taobao.Entity.Extend
{
    [Serializable]
    [JsonObject]
    public class UserRsp
    {

        /// <summary>
        /// 返回所有的users
        /// </summary>
        [JsonProperty("users")]
        public Users Users
        {
            get;
            set;
        }

        /// <summary>
        /// 返回user
        /// </summary>
        [JsonProperty("user")]
        public User User
        {
            get;
            set;
        }
    }

    [Serializable]
    [JsonObject]
    public class Users
    {
        /// <summary>
        /// user列表
        /// </summary>
        [JsonProperty("user")]
        public User[] User { get; set; }
    }

}
