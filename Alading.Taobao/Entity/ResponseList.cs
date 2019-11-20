using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Alading.Taobao.Entity
{
    /// <summary>
    /// TOP响应列表
    /// </summary>
    /// <typeparam name="T">任何一种可序列化的领域对象</typeparam>
    [Serializable]
    [XmlRoot("rsp")]
    public class ResponseList<T>
    {
        /// <summary>
        /// 所有记录数，主要用于分页显示。
        /// </summary>
        [JsonProperty("totalResults")]
        [XmlElement("totalResults")]
        public long TotalResults { get; set; }

        /// <summary>
        /// 解释后的具体对象。
        /// </summary>
        public List<T> Content { get; set; }

        /// <summary>
        /// 取得响应列表中的第一个对象。
        /// </summary>
        /// <returns>第一个对象或者null</returns>
        public T GetFirst()
        {
            if (Content != null && Content.Count > 0)
            {
                return Content[0];
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 解释XML响应为相应的领域对象列表。
        /// </summary>
        /// <param name="element">XML标签名字</param>
        /// <param name="body">XML响应字符串</param>
        /// <returns>领域对象列表</returns>
        public static ResponseList<T> ParseXmlResponse(string element, string body)
        {
            XmlAttributes attrs = new XmlAttributes();
            attrs.XmlElements.Add(new XmlElementAttribute(element, typeof(T)));
            XmlAttributeOverrides attrOvrs = new XmlAttributeOverrides();
            attrOvrs.Add(typeof(ResponseList<T>), "Content", attrs);
            XmlSerializer serializer = new XmlSerializer(typeof(ResponseList<T>), attrOvrs);
            object obj = serializer.Deserialize(new StringReader(body));
            return obj as ResponseList<T>;
        }

        /// <summary>
        /// 解释JSON响应为相应的领域对象列表。
        /// </summary>
        /// <param name="property">JSON属性名称</param>
        /// <param name="body">JSON响应字符串</param>
        /// <returns>领域对象列表</returns>
        public static ResponseList<T> ParseJsonResponse(string property, string body)
        {
            ResponseList<T> rspList = new ResponseList<T>();

            JObject jsonBody = JObject.Parse(body);
            JArray jsonRsp = jsonBody["rsp"][property] as JArray;
            rspList.TotalResults = jsonBody["rsp"].Value<long>("totalResults");

            if (jsonRsp != null)
            {
                List<T> props = new List<T>();
                for (int i = 0; i < jsonRsp.Count; i++)
                {
                    JsonSerializer js = new JsonSerializer();
                    object obj = js.Deserialize(jsonRsp[i].CreateReader(), typeof(T));
                    props.Add((T)obj);
                }
                rspList.Content = props;
            }

            return rspList;
        }
    }
}
