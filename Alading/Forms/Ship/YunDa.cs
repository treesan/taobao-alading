using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.XPath;
using System.Drawing;
using System.Drawing.Imaging;
using Sgml;

namespace Express_Query
{
    class YunDa
    {
        private string querynum;
         public YunDa(string num)
        {
            querynum = num;
        }
        public ResultInfo query()
        {
            try
            {
                string strURL = "http://222.73.105.196/imagecode.php";
                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
                request.KeepAlive = true;
                request.Method = "get";
                System.Net.HttpWebResponse responseMain;
                CookieContainer container;
                request.CookieContainer = new CookieContainer();
                responseMain = (System.Net.HttpWebResponse)request.GetResponse();
                container = request.CookieContainer;
                Stream resStream = responseMain.GetResponseStream();//得到验证码数据流
                Bitmap sourcebm = new Bitmap(resStream);
                sourcebm.Save("just.png");
                responseMain.Close();
                resStream.Close();



                YunDaDecoder decoder = new YunDaDecoder(sourcebm);
                int validcode = decoder.decode();
                string resultUrl = "http://222.73.105.196/ykjcx/cxend.php";
                HttpWebRequest requestResult;
                requestResult = (HttpWebRequest)HttpWebRequest.Create(resultUrl);


                requestResult.CookieContainer = container;
                requestResult.Method = "post";
                requestResult.ContentType = "application/x-www-form-urlencoded";
                string parameterStr = String.Format("wen={0}&yzm={1}&debug=1&lang=", querynum, validcode);


                byte[] payload;
                payload = System.Text.Encoding.UTF8.GetBytes(parameterStr);
                requestResult.ContentLength = payload.Length;
                Stream writer = requestResult.GetRequestStream();
                writer.Write(payload, 0, payload.Length);
                writer.Close();

                HttpWebResponse responseResult = (HttpWebResponse)requestResult.GetResponse();
                System.IO.Stream s;
                s = responseResult.GetResponseStream();
                StreamReader reader = new StreamReader(s, Encoding.GetEncoding("GB2312"));
                String backstring = reader.ReadToEnd();
                reader.Close();

                return getDetail(backstring);
            }
            catch (Exception e)
            {
                return new ResultInfo(querynum);
            }
         
        }
          private ResultInfo getDetail(string backstring)
        {

            backstring = backstring.Replace("xmlns=\"http://www.w3.org/1999/xhtml\"", "");
            backstring = backstring.Replace("<?php// echo $content;?>","");
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
            XmlNamespaceManager xnm = new XmlNamespaceManager(doc.NameTable);
             xnm.AddNamespace("bottum", "http://www.w3.org/1999/xhtml");
            XPathNavigator nav = doc.CreateNavigator();
            string xpath = "/html/body/div/div[2]/div/div[3]/table/tr[3]/td/table/tr/td";
            XPathNodeIterator nodes = nav.Select(xpath, xnm);//xpath表达式   
            
            ResultInfo backinfo = new ResultInfo(querynum);
            if (nodes.Count > 2)
            {
                nodes.MoveNext();
                nodes.MoveNext();
            }
            for (int i = 1; i < nodes.Count / 2; i++)
            {

                nodes.MoveNext();
                string time =nodes.Current.Value;
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
