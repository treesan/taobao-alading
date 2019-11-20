using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Alading.Taobao.Entity.Extend
{
    [Serializable]
    [JsonObject]
    public class TradeReq : Trade 
    {
        /// <summary>
        ///  查询交易创建时间开始
        /// </summary>
        [JsonProperty("start_created")]
        public string  StartCreated
        {
            get;
            set;
        }

        /// <summary>
        ///  查询交易创建时间结束
        /// </summary>
        [JsonProperty("end_created")]
        public string EndCreated
        {
            get;
            set;
        }

        /// <summary>
        ///  页码
        /// </summary>
        [JsonProperty("page_no")]
        public int PageNo
        {
            get;
            set;
        }

        /// <summary>
        ///  每页条数
        /// </summary>
        [JsonProperty("page_size")]
        public int PageSize
        {
            get;
            set;
        }
        
        /// <summary>
        ///  评价状态，默认查询所有评价状态的数据，除了默认值外每次只能查询一种状态。
        /// </summary>
        [JsonProperty("rate_status")]
        public string  RateStatus
        {
            get;
            set;
        }
        
        /// <summary>
        ///  交易关闭原因。最小长度: 6个字节 
        /// </summary>
        [JsonProperty("close_reason")]
        public string CloseReason 
        {
            get;
            set;
        }
        
        /// <summary>
        ///  交易备注。最大长度: 1000个字节
        /// </summary>
        [JsonProperty("memo")]
        public string Memo  
        {
            get;
            set;
        }

        /// <summary>
        ///  查询修改时间开始。格式: yyyy-MM-dd HH:mm:ss 
        /// </summary>
        [JsonProperty("start_modified")]
        public string StartModified
        {
            get;
            set;
        }

        /// <summary>
        ///  查询修改时间结束。格式: yyyy-MM-dd HH:mm:ss 
        /// </summary>
        [JsonProperty("end_modified")]
        public string EndModified 
        {
            get;
            set;
        }
    }
}
