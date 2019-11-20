using System;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Alading.Taobao.Entity
{
    [Serializable]
    [JsonObject]
    public class TradeConfirmFee
    {
        [JsonProperty("confirm_fee")]
        [XmlElement("confirm_fee")]
        public string ConfirmFee { get; set; }


        [JsonProperty("confirm_post_fee")]
        [XmlElement("confirm_post_fee")]
        public string ConfirmPostFee { get; set; }

        [JsonProperty("is_last_order")]
        [XmlElement("is_last_order")]
        public bool IsLastOrder { get; set; }

    }
}
