using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using Sgml;
using System.Xml;
using System.Xml.XPath;

namespace Express_Query
{
    class YuantongEmail
    {
        private string queryNumber;
        public YuantongEmail(string number)
        {
            queryNumber = number;
        }
        public ResultInfo query()
        {
            try
            {   
                string strURL = "http://www.yto.net.cn/";
                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
                WebHeaderCollection requestHeader;
                requestHeader = new WebHeaderCollection();
                request.KeepAlive = true;
                request.Method = "get";
                System.Net.HttpWebResponse responseMain;
                CookieContainer container;
                request.CookieContainer = new CookieContainer();
                responseMain = (System.Net.HttpWebResponse)request.GetResponse();
                container = request.CookieContainer;

                ///////////设置请求验证码的图片头,并获取会话id
                HttpWebRequest requestPic;
                string urlPic = "http://www.yto.net.cn/link/CreateValidCode.aspx";
                requestPic = (HttpWebRequest)HttpWebRequest.Create(urlPic);
                requestPic.CookieContainer = container;
                requestPic.Method = "get";

                /////////////////获取验证码头

                HttpWebResponse responsePic = (HttpWebResponse)requestPic.GetResponse();
                requestPic.ContentType = "	image/jpeg";
                container = requestPic.CookieContainer;
                Stream resStream = responsePic.GetResponseStream();//得到验证码数据流
                Bitmap sourcebm = new Bitmap(resStream);
                YuantongDecoderString decode = new YuantongDecoderString();
                string backNumber = decode.decoder(sourcebm);



                //////////获取查询结果页面
                string resultUrl = "http://www.yto.net.cn/service/sql2.aspx";
                HttpWebRequest requestResult;
                requestResult = (HttpWebRequest)HttpWebRequest.Create(resultUrl);
                requestResult.CookieContainer = container;
                requestResult.Method = "post";
                requestResult.ContentType = "application/x-www-form-urlencoded";
                string parameterStr = String.Format("NumberText={0}&CodeText={1}&loginvip.x=57&loginvip.y=12", queryNumber, backNumber);


                byte[] payload;
                //将URL编码后的字符串转化为字节
                payload = System.Text.Encoding.UTF8.GetBytes(parameterStr);
                requestResult.ContentLength = payload.Length;
                //获得请求流
                Stream writer = requestResult.GetRequestStream();
                //将请求参数写入流
                writer.Write(payload, 0, payload.Length);
                writer.Close();
                HttpWebResponse responseResult = (HttpWebResponse)requestResult.GetResponse();
                Encoding encode = Encoding.GetEncoding("UTF-8");
                System.IO.Stream s;
                s = responseResult.GetResponseStream();
                StreamReader reader = new StreamReader(s, Encoding.GetEncoding("GB18030"));
                String backstring = reader.ReadToEnd();
                reader.Close();
                ResultInfo backinfo = getDetail(backstring);
                Console.WriteLine("ust for");
                return backinfo;
            }catch(Exception e)///////////////防止网络中断发生异常
            {
                return new ResultInfo(queryNumber);
            }
           
        }
        public ResultInfo getDetail(string backstring)
        {
         
            SgmlReader reader = new SgmlReader();
            reader.DocType = "HTML";
            reader.InputStream = new StringReader(backstring);
            StringWriter sw = new StringWriter();
            XmlTextWriter writer = new XmlTextWriter(sw);
            reader.WhitespaceHandling = WhitespaceHandling.None;
            writer.Formatting = Formatting.Indented;
            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Whitespace)
                {
                    writer.WriteNode(reader, true);
                }
            }


            XmlDocument doc = new XmlDocument();
            doc.Load(new StringReader(sw.ToString()));

            reader.Close();
            writer.Close();
            sw.Close();
            XmlNamespaceManager xnm = new XmlNamespaceManager(doc.NameTable);
            xnm.AddNamespace("bottum", "http://www.w3.org/1999/xhtml");
            XPathNavigator nav = doc.CreateNavigator();
            string xpath = "//bottum:table[@id='GridView1']/bottum:tr/bottum:td";
            XPathNodeIterator nodes = nav.Select(xpath, xnm);//xpath表达式
          
            if (nodes != null)
            {
                int count = nodes.Count;
                int k = count / 3;
                ResultInfo backinfo = new ResultInfo(queryNumber);
                for (int i = 0; i < k; i++)
                {
                    nodes.MoveNext();
                    nodes.MoveNext();
                    string time = nodes.Current.Value;
                    nodes.MoveNext();
                    string state = nodes.Current.Value;
                    backinfo.add(time,state);
                    
                }
               
                return backinfo;
            } else
            {
                return new ResultInfo(queryNumber);
            }
        }

    }
}
