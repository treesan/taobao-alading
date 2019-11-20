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
    class TianTian
    {
        private string querynum;
        public TianTian(string num)
        {
            querynum = num;
        }
        public ResultInfo query()
        {
            try
            {
                string strURL = String.Format("http://61.172.202.147/webquery/query.asp");
                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
                WebHeaderCollection requestHeader;
                request.KeepAlive = true;
                request.Method = "post";
                string postpara = String.Format("wen={0}&x=32&y=10", querynum);
                byte[] postparaBin = Encoding.ASCII.GetBytes(postpara);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postparaBin.Length;
                Stream outStream = request.GetRequestStream();
                outStream.Write(postparaBin, 0, postparaBin.Length);
                outStream.Close();
                HttpWebResponse responseResult = (HttpWebResponse)request.GetResponse();
                Encoding encode = Encoding.GetEncoding("UTF-8");
                System.IO.Stream s;
                s = responseResult.GetResponseStream();
                StreamReader reader = new StreamReader(s, Encoding.GetEncoding("GB2312"));
                String backstring = reader.ReadToEnd();
                reader.Close();
                s.Close();
                responseResult.Close();
                return getDetail(backstring);
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
            XmlDocument doc = new XmlDocument();
            doc.Load(new StringReader(sw.ToString()));
            XmlNamespaceManager xnm = new XmlNamespaceManager(doc.NameTable);
            XPathNavigator nav = doc.CreateNavigator();

            /////////////////根据网页返回结果分析

            string xpath = "//table[1]/tr/td";
            string str = "";
            XPathNodeIterator nodes = nav.Select(xpath, xnm);//xpath表达式

            ResultInfo backinfo = new ResultInfo(querynum);
            if (nodes.Count >= 4)
            {
               nodes.MoveNext();
                nodes.MoveNext();
                nodes.MoveNext();
                nodes.MoveNext();

            }
            for (int i = 4; i < nodes.Count  / 2; i++)
            {
                nodes.MoveNext();
                string time = nodes.Current.Value;
                nodes.MoveNext();
                string state = nodes.Current.Value;
                backinfo.add(time, state);
                nodes.MoveNext();
            }
            reader.Close();
            writer.Close();
            sw.Close();
            return backinfo;
        }
    }
}
