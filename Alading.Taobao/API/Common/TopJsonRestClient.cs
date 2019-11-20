using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;

namespace Alading.Taobao.API
{
    class TopJsonRestClient
    {
        static int timeOut = 10000;

        /// <summary>
        /// 发送API调用请求并接收响应
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public string InvokeAPI(IDictionary<string, string> @params,string url)
        {
            try
            {
                // Create a request for the URL.
                HttpWebRequest request;
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = timeOut;
                // Set the request verb.
                request.Method = "POST";
                request.KeepAlive = true;
                request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                byte[] postData = Encoding.UTF8.GetBytes(GetParams(@params));
                //把数据写入HttpWebRequest的Request流
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(postData, 0, postData.Length);
                reqStream.Close();

                HttpWebResponse rsp = (HttpWebResponse)request.GetResponse();
                Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
                StreamReader reader = new StreamReader(rsp.GetResponseStream(), System.Text.Encoding.UTF8);
                String responseFromServer = reader.ReadToEnd();
                rsp.Close();//一定不要忘了关闭
                return responseFromServer;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 涉及到图片上传的API用，论坛网友研究出代码,经修改后
        /// </summary>
        public string InvokUpImageAPI(byte[] pictureBytes, IDictionary<string, string> @params, string url)
        {
            string fileFormName = "file.jpg";
            string contenttype = "application/octet-stream";

            string postdata = GetParams(@params);
            if (!url.Contains('?'))
            {
                url += "?";
            }
            Uri uri = new Uri(url + postdata);

            string boundary = DateTime.Now.Ticks.ToString("X");// 随机分隔线 
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(uri);
            webrequest.Timeout = timeOut;
            CookieContainer cookies = new CookieContainer();
            webrequest.CookieContainer = cookies;
            webrequest.ContentType = "text/plain";
            webrequest.Method = "POST";

            // Build up the post message header
            webrequest.ContentType = "multipart/form-data; boundary=" + boundary;
            StringBuilder sb = new StringBuilder();
            sb.Append("--");
            sb.Append(boundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"");
            sb.Append(fileFormName);
            sb.Append("\"; filename=\"");
            sb.Append("fileName.jpg");
            sb.Append("\"");
            sb.Append("\r\n");
            sb.Append("Content-Type: ");
            sb.Append(contenttype);
            sb.Append("\r\n");
            sb.Append("\r\n");

            string postHeader = sb.ToString();
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);

            // Build the trailing boundary string as a byte array
            // ensuring the boundary appears on a line by itself
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            long length = 0;
            if (pictureBytes != null || pictureBytes.Length > 0)
            {
                MemoryStream fileStream = new MemoryStream(pictureBytes);
                length = postHeaderBytes.Length + fileStream.Length + boundaryBytes.Length;
                webrequest.ContentLength = length;
                Stream requestStream = webrequest.GetRequestStream();

                // Write out our post header 写http请求头
                requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

                //// Write out the file contents
                //byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)fileStream.Length))];
                int bytesRead = 0;
                // 循环写图片内容
                while ((bytesRead = fileStream.Read(pictureBytes, 0, pictureBytes.Length)) != 0)
                    requestStream.Write(pictureBytes, 0, bytesRead);

                //写http请求尾
                requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);

                //必须关闭
                fileStream.Close();
            }
            else
            {
                length = postHeaderBytes.Length + boundaryBytes.Length;
                webrequest.ContentLength = length;
                Stream requestStream = webrequest.GetRequestStream();
                requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
            }
            // Write out the trailing boundary

            HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse();
            Stream s = response.GetResponseStream();
            StreamReader sr = new StreamReader(s);
            string responseFromServer = sr.ReadToEnd();
            s.Close();
            sr.Close();
            response.Close();
            return responseFromServer;
        }

        /// <summary>
        /// 获得API调用所需参数
        /// </summary>
        public string GetParams(IDictionary<string, string> @params)
        {
            System.Collections.IEnumerator iter = @params.Keys.GetEnumerator();
            System.Text.StringBuilder orgin = new System.Text.StringBuilder();
            int i = 0;
            while (iter.MoveNext())
            {
                string name = (string)iter.Current;
                string value = HttpUtility.UrlEncode(@params[name], System.Text.Encoding.UTF8);
                //string value=@params[name];
                orgin.Append(name).Append("=").Append(value);
                if (i != @params.Keys.Count - 1) orgin.Append("&");
                i++;
            }
            return orgin.ToString();

        }
    }
}
