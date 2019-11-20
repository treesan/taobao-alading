using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml;
using System.Xml.XPath;
using Sgml;


namespace Express_Query
{
    class ShunfengQuery
    {
        private string queryNumber;
        public ShunfengQuery(string number)
        {
            queryNumber = number;
        }
        public ResultInfo query()
        {
            try
            {
                string strURL = "http://www.sf-express.com/";
                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
                WebHeaderCollection requestHeader;
                request.KeepAlive = true;
                request.Method = "get";


                System.Net.HttpWebResponse responseMain;
                CookieContainer container;
                request.CookieContainer = new CookieContainer();
                responseMain = (System.Net.HttpWebResponse)request.GetResponse();
                container = request.CookieContainer;

                ///////////设置请求验证码的图片头
                HttpWebRequest requestPic;
                string urlPic = "http://www.sf-express.com/Countersign.aspx?CountersignName=CustomerRigester";
                requestPic = (HttpWebRequest)HttpWebRequest.Create(urlPic);
                requestPic.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                requestPic.CookieContainer = container;
                requestPic.Method = "get";

                /////////////////获取验证码头

                HttpWebResponse responsePic = (HttpWebResponse)requestPic.GetResponse();
                requestPic.ContentType = "	image/Gif";
                container = requestPic.CookieContainer;
                Stream resStream = responsePic.GetResponseStream();//得到验证码数据流
                Bitmap sourcebm = new Bitmap(resStream);
                sourcebm.Save("sen.png", ImageFormat.Png);
                sourcebm = new Bitmap("sen.png");
                ShunfengDecoderString dcoder = new ShunfengDecoderString();
                string code = dcoder.decodeBitmap(sourcebm);

                /////////////设置返回请求
                string resultUrl = string.Format("http://www.sf-express.com/tabid/521/Default.aspx?TrackList={0}&code={1}", queryNumber, code);
                HttpWebRequest requestResult;
                requestResult = (HttpWebRequest)HttpWebRequest.Create(resultUrl);
                requestResult.CookieContainer = container;
                requestResult.Method = "get";
                requestResult.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse responseResult = (HttpWebResponse)requestResult.GetResponse();
                Encoding encode = Encoding.GetEncoding("UTF-8");
                System.IO.Stream s;
                s = responseResult.GetResponseStream();
                StreamReader reader = new StreamReader(s, Encoding.GetEncoding("UTF-8"));
                String backstring = reader.ReadToEnd();
                reader.Close();
                ResultInfo backinfo = getDetail(backstring);
                return backinfo;
            }
            catch (Exception e)
            {
                return new ResultInfo(queryNumber);
            }

        }
        private ResultInfo getDetail(string backstring)
        {

            //////////////用sgml库分析网页，转换成xml文件
            SgmlReader readern = new SgmlReader();
            readern.DocType = "HTML";
            readern.InputStream = new StringReader(backstring);
            StringWriter sw = new StringWriter();
            XmlTextWriter writer = new XmlTextWriter(sw);
            readern.WhitespaceHandling = WhitespaceHandling.None;
            writer.Formatting = Formatting.Indented;
            while (!readern.EOF)
            {
                readern.Read();
                if (readern.NodeType != XmlNodeType.Whitespace)
                {
                    writer.WriteNode(readern, true);
                }
               
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(new StringReader(sw.ToString()));
            XmlNamespaceManager xnm = new XmlNamespaceManager(doc.NameTable);
            XPathNavigator nav = doc.CreateNavigator();
            string xpath = "//div[@id='ess_ctr1579_TrackResult_DivBill']/table[2]/tr[@class='font_c']/td";
            XPathNodeIterator nodes = nav.Select(xpath, xnm);//xpath表达式
            ResultInfo backinfo = new ResultInfo(queryNumber);
            for (int i = 0; i < nodes.Count / 2; i++)
            {
                nodes.MoveNext();
                string time = nodes.Current.Value;
                nodes.MoveNext();
                string state = nodes.Current.Value;
                backinfo.add(time,state);
            }
            readern.Close();
            writer.Close();
            sw.Close();
            return backinfo;
        }
        
    }
}
