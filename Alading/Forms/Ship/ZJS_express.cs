using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.XPath;
using Sgml;

namespace Express_Query
{
    public class ZJS_express
    {
        private string querynum;
        public ZJS_express(String  num)
        {
            querynum = num;
        }
        public ResultInfo query()
        {
            try
            {
                string strURL = "http://www.zjs.com.cn/";
                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
                WebHeaderCollection requestHeader;
                requestHeader = new WebHeaderCollection();
                request.KeepAlive = true;
                request.Method = "get";



                ///////////////////////获取返回的会话id
                System.Net.HttpWebResponse responseMain;
                CookieContainer container;
                request.CookieContainer = new CookieContainer();
                responseMain = (System.Net.HttpWebResponse)request.GetResponse();
                container = request.CookieContainer;
                responseMain.Close();


                ///////////设置请求验证码的图片头
                HttpWebRequest requestPic;
                string urlPic = "http://www.zjs.com.cn/VerifyImg.aspx";
                requestPic = (HttpWebRequest)HttpWebRequest.Create(urlPic);
                requestPic.CookieContainer = container;
                requestPic.Method = "get";


                /////////////获取返回图片流
                HttpWebResponse responsePic = (HttpWebResponse)requestPic.GetResponse();
                requestPic.ContentType = "	image/Gif";
                container = requestPic.CookieContainer;
                string validcode = responsePic.Cookies["ValiCode"].Value;
                responsePic.Close();
                 
                ///////////构造返回流
                string resultUrl = "http://www.zjs.com.cn/WS_Business/WS_Business_Tracking.aspx?id=5";
                string postparam = String.Format("__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=%2FwEPDwUJMzE0MTYyNzU5D2QWAmYPZBYCAgMPZBYCAgEPZBYCAgEPZBYCZg8WAh4JaW5uZXJodG1sBaokPGRpdiBjbGFzcz0ibmF2Ij4NCiA8ZGl2IGNsYXNzPSJ0YWJsZSI%2BDQo8dWwgY2xhc3M9ImN1cnJlbnQiPg0KPGxpPjxhIGhyZWY9Imh0dHA6Ly93d3cuempzLmNvbS5jbiI%2BPGI%2B6aaW6aG1PC9iPjwhLS1baWYgSUUgN10%2BPCEtLT48L2E%2BPCEtLTwhW2VuZGlmXS0tPiA8IS0tW2lmIGx0ZSBJRSA2XT48dGFibGU%2BPHRyPjx0ZD48IVtlbmRpZl0tLT4NCjxkaXYgY2xhc3M9InNlbGVjdF9zdWIgc2hvdyI%2BDQo8dWwgY2xhc3M9InN1YiI%2BDQo8bGk%2BPGEgY2xhc3M9InN1Yl9zaG93IiBocmVmPSdpbmRleC5hc3B4Jz7pm4blm6LmgLvpg6g8L2E%2BPC9saT4NCjxsaT48YSBjbGFzcz0ic3ViX3Nob3ciIGhyZWY9J2luZGV4LmFzcHgnPuWMl%2BS6rOWuheaApemAgTwvYT48L2xpPg0KPGxpPjxhIGNsYXNzPSJzdWJfc2hvdyIgaHJlZj0nV1NfRmlsaWFsZS9XU19GaWxpYWxlX3NoYW5naGFpX2ludHJvLmFzcHgnPuS4iua1t%2BWuheaApemAgTwvYT48L2xpPg0KPGxpPjxhIGNsYXNzPSJzdWJfc2hvdyIgaHJlZj0nV1NfRmlsaWFsZS9XU19GaWxpYWxlX3NoZW56aGVuX2ludHJvLmFzcHgnPua3seWcs%2BWuheaApemAgTwvYT48L2xpPg0KPGxpPjxhIGNsYXNzPSJzdWJfc2hvdyIgaHJlZj0nV1NfRmlsaWFsZS9XU19GaWxpYWxlX2d1YW5nemhvdV9pbnRyby5hc3B4Jz7lub%2Flt57lroXmgKXpgIE8L2E%2BPC9saT4NCjwvdWw%2BDQo8L2Rpdj4NCjwhLS1baWYgbHRlIElFIDZdPjwvdGQ%2BPC90cj48L3RhYmxlPjwvYT48IVtlbmRpZl0tLT4NCjwvbGk%2BDQo8L3VsPjx1bCBjbGFzcz0ic2VsZWN0Ij4NCjxsaT48YSBocmVmPSdXU19TZXJ2aWNlL1dTX1NlcnZpY2VfZXhwcmVzcy5hc3B4P2lkPTInPjxiPuS4u%2BiQpeS4muWKoTwvYj48IS0tW2lmIElFIDddPjwhLS0%2BPC9hPjwhLS08IVtlbmRpZl0tLT4NCjwhLS1baWYgbHRlIElFIDZdPjx0YWJsZT48dHI%2BPHRkPjwhW2VuZGlmXS0tPg0KPGRpdiBjbGFzcz0ic2VsZWN0X3N1YiI%2BDQo8dWwgY2xhc3M9InN1YiI%2BDQo8bGk%2BPGEgaHJlZj0nV1NfU2VydmljZS9XU19TZXJ2aWNlX2V4cHJlc3MuYXNweD9pZD0yJz7lv6vpgJLkuJrliqE8L2E%2BPC9saT4NCjxsaT48YSBocmVmPSdXU19TZXJ2aWNlL1dTX1NlcnZpY2VfY29tbWVuLmFzcHg%2FaWQ9Mic%2B5b%2Br6L%2BQ5Lia5YqhPC9hPjwvbGk%2BDQo8bGk%2BPGEgaHJlZj0nV1NfU2VydmljZS9XU19TZXJ2aWNlX2J1aW5lc3MuYXNweD9pZD0yJz7ku6PmlLbotKfmrL48L2E%2BPC9saT4NCjwvdWw%2BDQo8L2Rpdj4NCjwhLS1baWYgbHRlIElFIDZdPjwvdGQ%2BPC90cj48L3RhYmxlPjwvYT48IVtlbmRpZl0tLT4NCjwvbGk%2BDQo8L3VsPg0KPHVsIGNsYXNzPSJzZWxlY3QiPg0KPGxpPjxhIGhyZWY9J1dTX01hcmtldC9XU19NYXJrZXRfaW5jcmVtZW50LmFzcHg%2FaWQ9Myc%2BPGI%2B5biC5Zy65o6o5bm%2FPC9iPjwhLS1baWYgSUUgN10%2BPCEtLT48L2E%2BPCEtLTwhW2VuZGlmXS0tPg0KPCEtLVtpZiBsdGUgSUUgNl0%2BPHRhYmxlPjx0cj48dGQ%2BPCFbZW5kaWZdLS0%2BDQo8ZGl2IGNsYXNzPSJzZWxlY3Rfc3ViIj4NCjx1bCBjbGFzcz0ic3ViIj4NCjxsaT48YSBocmVmPSdXU19NYXJrZXQvV1NfTWFya2V0X2luY3JlbWVudC5hc3B4P2lkPTMnPuWinuWAvOacjeWKoTwvYT48L2xpPg0KPGxpPjxhIGhyZWY9J1dTX01hcmtldC9XU19NYXJrZXRfYWN0aXZpdHkuYXNweD9pZD0zJz7luILlnLrmtLvliqg8L2E%2BPC9saT4NCjxsaT48YSBocmVmPSdXU19NYXJrZXQvV1NfTWFya2V0X0NvbGxlYWdlLmFzcHg%2FaWQ9Myc%2B5qCh5Zut57uP5rWOPC9hPjwvbGk%2BDQo8bGk%2BPGEgaHJlZj0nV1NfTWFya2V0L1dTX01hcmtldF9DYWxsQ2VudGVyLmFzcHg%2FaWQ9Myc%2B54Ot54K55L%2Bh5oGvPC9hPjwvbGk%2BDQo8L3VsPg0KPC9kaXY%2BDQo8IS0tW2lmIGx0ZSBJRSA2XT48L3RkPjwvdHI%2BPC90YWJsZT48L2E%2BPCFbZW5kaWZdLS0%2BDQo8L2xpPg0KPC91bD4NCjx1bCBjbGFzcz0ic2VsZWN0Ij4NCjxsaT48YSBocmVmPSdXU19sZWFndWUvV1NfbGVhZ3VlX25vdGljZS5hc3B4Jz48Yj7nvZHnu5zmi5vllYY8L2I%2BPCEtLVtpZiBJRSA3XT48IS0tPjwvYT48IS0tPCFbZW5kaWZdLS0%2BDQo8IS0tW2lmIGx0ZSBJRSA2XT48dGFibGU%2BPHRyPjx0ZD48IVtlbmRpZl0tLT4NCjxkaXYgY2xhc3M9InNlbGVjdF9zdWIiPg0KPHVsIGNsYXNzPSJzdWIiPg0KPGxpPjxhIGhyZWY9J1dTX2xlYWd1ZS9XU19sZWFndWVfbm90aWNlLmFzcHg%2FaWQ9NCc%2B572R57uc5oub5ZWG5YWs5ZGKPC9hPjwvbGk%2BDQo8bGk%2BPGEgaHJlZj0nV1NfbGVhZ3VlL1dTX2xlYWd1ZV9pbmRleC5hc3B4P2lkPTQnPuWKoOebn%2BaLm%2BWVhumhu%2BefpTwvYT48L2xpPg0KPGxpPjxhIGhyZWY9J1dTX2xlYWd1ZS9XU19sZWFndWVfbGVhZ3VlMi5hc3B4P2lkPTQnPuS7o%2BeQhuaLm%2BWVhumhu%2BefpTwvYT48L2xpPg0KPGxpPjxhIGhyZWY9J1dTX2xlYWd1ZS9XU19sZWFndWVfZGlmZmVyZW5jZS5hc3B4Jz7ku6PnkIbkuI7liqDnm5%2FljLrliKs8L2E%2BPC9saT4NCjwvdWw%2BDQo8L2Rpdj4NCjwhLS1baWYgbHRlIElFIDZdPjwvdGQ%2BPC90cj48L3RhYmxlPjwvYT48IVtlbmRpZl0tLT4NCjwvbGk%2BDQo8L3VsPg0KPHVsIGNsYXNzPSJzZWxlY3QiPg0KPGxpPjxhIGhyZWY9J1dTX0J1c2luZXNzL1dTX0J1c2luZXNzX0dvb2RzVHJhY2suYXNweD9pZD01Jz48Yj7otKfnianmn6Xor6I8L2I%2BPCEtLVtpZiBJRSA3XT48IS0tPjwvYT48IS0tPCFbZW5kaWZdLS0%2BDQo8IS0tW2lmIGx0ZSBJRSA2XT48dGFibGU%2BPHRyPjx0ZD48IVtlbmRpZl0tLT4NCjxkaXYgY2xhc3M9InNlbGVjdF9zdWIiPg0KPHVsIGNsYXNzPSJzdWIiPg0KPGxpPjxhIGhyZWY9J1dTX0J1c2luZXNzL1dTX0J1c2luZXNzX0dvb2RzVHJhY2suYXNweD9pZD01Jz7otKfnianmn6Xor6I8L2E%2BPC9saT4NCjxsaT48YSBocmVmPScNCiAgICAgIFdTX0J1c2luZXNzL1dTX2J1c2luZXNzX3BhY2thZ2luZy5hc3B4P2lkPTUNCiAgICAnPuWMheijheafpeivojwvYT48L2xpPg0KPGxpPjxhIGhyZWY9Jw0KICAgICAgV1NfQnVzaW5lc3MvV1NfQnVzaW5lc3NfQ29udHJhYmFuZF8xLmFzcHg%2FaWQ9NXxsaWQ9MjQNCiAgICAnPuemgei%2FkOWTgeafpeivojwvYT48L2xpPg0KPC91bD4NCjwvZGl2Pg0KPCEtLVtpZiBsdGUgSUUgNl0%2BPC90ZD48L3RyPjwvdGFibGU%2BPC9hPjwhW2VuZGlmXS0tPg0KPC9saT4NCjwvdWw%2BDQo8dWwgY2xhc3M9InNlbGVjdCI%2BDQo8bGk%2BPGEgaHJlZj0nV1NfQnVzaW5lc3MvV1NfQnVzaW5lc3NfaW5kZXguYXNweD9pZD02Jz48Yj7nvZHkuIrmnI3liqE8L2I%2BPCEtLVtpZiBJRSA3XT48IS0tPjwvYT48IS0tPCFbZW5kaWZdLS0%2BDQo8IS0tW2lmIGx0ZSBJRSA2XT48dGFibGU%2BPHRyPjx0ZD48IVtlbmRpZl0tLT4NCjxkaXYgY2xhc3M9InNlbGVjdF9zdWIiPg0KPHVsIGNsYXNzPSJzdWIiPg0KPGxpPjxhIGhyZWY9Jw0KCQkJV1NfQnVzaW5lc3MvV1NfQ3VzdG9tTG9naW4uYXNweD9pZD00DQoJCSc%2B572R5LiK5LiL5Y2VPC9hPjwvbGk%2BDQo8bGk%2BPGEgaHJlZj0nV1NfQnVzaW5lc3MvV1NfQnVzaW5lc3NfcHJpY2VfaW50ZXJuYWwuYXNweD9pZD00Jz7ku7fmoLzmn6Xor6I8L2E%2BPC9saT4NCjxsaT48YSBocmVmPScNCgkJCVdTX0J1c2luZXNzL1dTX0J1c2luZXNzX0Fycml2ZUFyZWEuYXNweD9pZD00DQoJCSc%2B5Y%2BW5rS%2B5Yy65Z%2BfPC9hPjwvbGk%2BDQo8bGk%2BPGEgaHJlZj0nV1NfQnVzaW5lc3MvV1NfQnVzaW5lc3NfdmlwTG9naW4uYXNweD9pZD02Jz5WSVDkuJPljLo8L2E%2BPC9saT4NCjxsaT48YSBocmVmPSdXU19CdXNpbmVzcy9XU19CdXNpbmVzc19wdXJjaGFzZS5hc3B4P2lkPTYnPumHh%2Bi0reeZu%2BmZhjwvYT48L2xpPg0KPGxpPjxhIGhyZWY9J1dTX0J1c2luZXNzL1dTX0J1c2luZXNzX3dvcmtlci5hc3B4P2lkPTYnPuWRmOW3peeZu%2BW9lTwvYT48L2xpPg0KPC91bD4NCjwvZGl2Pg0KPCEtLVtpZiBsdGUgSUUgNl0%2BPC90ZD48L3RyPjwvdGFibGU%2BPC9hPjwhW2VuZGlmXS0tPg0KPC9saT4NCjwvdWw%2BDQo8dWwgY2xhc3M9InNlbGVjdCI%2BDQo8bGk%2BPGEgaHJlZj0nV1NfQWJvdXR1cy9XU19BYm91dFVzX2luZGV4LmFzcHg%2FaWQ9Nyc%2BPGI%2B5YWz5LqO5a6F5oCl6YCBPC9iPjwhLS1baWYgSUUgN10%2BPCEtLT48L2E%2BPCEtLTwhW2VuZGlmXS0tPg0KPCEtLVtpZiBsdGUgSUUgNl0%2BPHRhYmxlPjx0cj48dGQ%2BPCFbZW5kaWZdLS0%2BDQo8ZGl2IGNsYXNzPSJzZWxlY3Rfc3ViIj4NCjx1bCBjbGFzcz0ic3ViIj4NCjxsaT48YSBocmVmPSdXU19BYm91dHVzL1dTX0Fib3V0VXNfaW5kZXguYXNweD9pZD03Jz7lhazlj7jnroDku4s8L2E%2BPC9saT4NCjxsaT48YSBocmVmPSdXU19BYm91dHVzL1dTX0Fib3V0VXNfY3VsdHVyZS5hc3B4P2lkPTcnPuaWh%2BWMlueQhuW%2FtTwvYT48L2xpPg0KPGxpPjxhIGhyZWY9J1dTX0Fib3V0dXMvV1NfQWJvdXRVc19wcmVkb21pbmFuY2UuYXNweD9pZD03Jz7ojrflvpfojaPoqok8L2E%2BPC9saT4NCjxsaT48YSBocmVmPSdXU19BYm91dHVzL1dTX0Fib3V0VVNfZHV0eS5hc3B4P2lkPTcnPuekvuS8mui0o%2BS7uzwvYT48L2xpPg0KPGxpPjxhIGhyZWY9J1dTX0Fib3V0dXMvV1NfQWJvdXRVU19zdGFmZi5hc3B4P2lkPTcnPuWRmOW3peaVheS6izwvYT48L2xpPg0KPC91bD4NCjwvZGl2Pg0KPCEtLVtpZiBsdGUgSUUgNl0%2BPC90ZD48L3RyPjwvdGFibGU%2BPC9hPjwhW2VuZGlmXS0tPg0KPC9saT4NCjwvdWw%2BDQo8L2Rpdj4NCjwvZGl2Pg0KZBgBBR5fX0NvbnRyb2xzUmVxdWlyZVBvc3RCYWNrS2V5X18WAQUcY3RsMDAkQ29udGVudDEkaW1hZ2VzQnV0dG9uMamFVqAeeEWwHOozlKKSkNd2%2BTkI&__PREVIOUSPAGE=6SWGQczFmPPy7XPSLYwhUQ2&__EVENTVALIDATION=%2FwEWBAKu3vWjDgKJ8LnPAQK1lr22CwKOnPioDfNcLIdF7cd7InVIMUFnJs8dnHHM&ctl00%24Content1%24TextBox1={0}&ctl00%24Content1%24ValidateTxt={1}&ctl00%24Content1%24imagesButton1.x=14&ctl00%24Content1%24imagesButton1.y=11", querynum, validcode);
                HttpWebRequest requestResult;
                requestResult = (HttpWebRequest)HttpWebRequest.Create(resultUrl);
                requestResult.CookieContainer = container;
                
                requestResult.Method = "post";

                /////写入post参数
                byte[] postparaBin = Encoding.ASCII.GetBytes(postparam);
                requestResult.ContentType = "application/x-www-form-urlencoded";
                requestResult.ContentLength = postparaBin.Length;
                Stream outStream = requestResult.GetRequestStream();
                outStream.Write(postparaBin, 0, postparaBin.Length);
                outStream.Close();
                HttpWebResponse responseResult = (HttpWebResponse)requestResult.GetResponse();
                Encoding encode = Encoding.GetEncoding("UTF-8");

                ////////获取返回流并分析
                System.IO.Stream s;
                s = responseResult.GetResponseStream();
                StreamReader reader = new StreamReader(s, Encoding.GetEncoding("GB2312"));
                String backstring = reader.ReadToEnd();
                reader.Close();
                responseResult.Close();
                ResultInfo backinfo = getDetail(backstring);
                return backinfo;
            }
            catch (Exception e)
            {
                return new ResultInfo(querynum);
            }
       
        }

        private ResultInfo getDetail(string backstring)
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

            ///////////////构造xmlpath获取数据
            XmlDocument doc = new XmlDocument();
            doc.Load(new StringReader(sw.ToString()));
            XmlNamespaceManager xnm = new XmlNamespaceManager(doc.NameTable);
            xnm.AddNamespace("bottum", "http://www.w3.org/1999/xhtml");
            XPathNavigator nav = doc.CreateNavigator();
            string xpath = "//bottum:table[@id='ctl00_Content1_GoodsTracks_GridView1']/bottum:tr/bottum:td";
            string str = "";
            XPathNodeIterator nodes = nav.Select(xpath, xnm);

            ResultInfo backinfo = new ResultInfo(querynum);
            for (int i = 0; i < nodes.Count / 2; i++)
            {
                nodes.MoveNext();
                string time = nodes.Current.Value;
                nodes.MoveNext();
                string state = nodes.Current.Value;
                backinfo.add(time, state);
            }
            reader.Close();
            writer.Close();
            sw.Close();
            return backinfo;
        }
       
    }
}
