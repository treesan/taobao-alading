using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Alading.Taobao.Entity;

namespace Alading.Taobao.API.Common
{
    /// <summary>
    /// Top平台工具类
    /// </summary>
    public abstract class TopUtils
    {
        #region 设置调用参数并调用API
        /// <summary>
        /// 设置调用参数并调用API
        /// </summary>
        /// <param name="paramsTable"></param>
        /// <returns></returns>
        public static string InvokeAPI(TopDictionary paramsTable, APIInvokeType invokeType)
        {          
            #region 获取SessionKey
            string url = string.Empty;
            APIType apiType = APIType.Real;
            if (apiType == APIType.Real)
            {
                url = Constants.TOP_API_URL;
            }
            else
            {
                url = Constants.TOP_SANDBOX_API_URL;
            }
            #endregion

            #region 设置API调用系统级参数
            string resBody = string.Empty;
            Dictionary<string, string> req_params = new Dictionary<string, string>();
            req_params.Add("format", "json");            
            req_params.Add("timestamp", DateTime.Now.ToString(Constants.DATE_TIME_FORMAT));
            req_params.Add("app_key", Constants.APP_KEY);
            req_params.Add("sign_method", "md5");
            req_params.Add("v", "2.0");
            #endregion

            #region 添加调用方法和参数
            IDictionaryEnumerator enumerator = paramsTable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                req_params.Add(enumerator.Key.ToString(), enumerator.Value.ToString());
            }
            #endregion

            #region 方法签名
            string sign = EncryptUtil.Signature(req_params, Constants.APP_SECRET);
            req_params.Add("sign", sign);
            #endregion

            #region 判定调用图片API或者普通API
            TopJsonRestClient client = new TopJsonRestClient();
            string apiname = req_params.Where(m => m.Key == "method").First().Value;
            if (apiname == "taobao.item.img.upload" || apiname == "taobao.item.propimg.upload")
            {
                Byte[] picBytes = paramsTable.PictureBytes;
                resBody = client.InvokUpImageAPI( picBytes, req_params, url);
            }
            else
            {
                resBody = client.InvokeAPI(req_params, url);
            }
            #endregion

            #region API异常处理
            if (resBody.Contains("error_response") || resBody.Contains("error_rsp"))
            {
                if (resBody.Contains("?xml"))
                {
                    return string.Empty;
                }
                int startIndex = resBody.IndexOf(":") + 1;
                /*截掉首尾的{}，截取长度修改为resBody.LastIndexOf('}') - startIndex，不用resBody.Length,有可能统计不准*/
                int length=resBody.LastIndexOf('}') - startIndex;
                resBody = resBody.Substring(startIndex, length);
                TopException exception = DeserializeObject<TopException>(resBody);
                throw new Exception(string.Format("错误代码：{0}，错误信息：{1}，错误子代码：{2}，错误子信息：{3}", exception.Code,exception.Msg,exception.SubCode,exception.SubMsg));
            }
            #endregion

            
            
            #region 获得API调用结果
            if (!string.IsNullOrEmpty(resBody))
            {
                int startIndex = resBody.IndexOf(":") + 1;
                /*截掉首尾的{}，截取长度修改为resBody.LastIndexOf('}') - startIndex，不用resBody.Length,有可能统计不准*/
                resBody = resBody.Substring(startIndex, resBody.LastIndexOf('}') - startIndex);
            }
            #endregion

            return resBody;
        }
        #endregion

        #region 反序列化对象

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="body"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string body)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(body);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 反序列化对象，将把Json字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="body"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string property, string body)
        {
            JObject jsonBody = JObject.Parse(body);
            JObject o = jsonBody[property] as JObject;//去除返回字段[property] 
            JsonSerializer js = new JsonSerializer(); 
            T obj = js.Deserialize<T>(o.CreateReader());
            return obj;
        }
        #endregion
    }

    public enum APIType
    { 
        /// <summary>
        /// 正式环境
        /// </summary>
        Real,
        /// <summary>
        /// 测试环境
        /// </summary>
        SandBox
    }

    public enum APIInvokeType
    { 
        /// <summary>
        /// 公开
        /// </summary>
        Public,
        /// <summary>
        /// 需要登录
        /// </summary>
        NeedLogin,
        /// <summary>
        /// 隐私
        /// </summary>
        Private
    }
}
