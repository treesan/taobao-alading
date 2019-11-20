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
    public class ShengTong
    {
        string querynum;
        public ShengTong(String num)
        {
            querynum = num;
        }
        public ResultInfo query()
        {
            try
            {
                string strURL = String.Format("http://115.238.55.94:8081/result.asp?wen={0}", querynum);
                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
                WebHeaderCollection requestHeader;
                requestHeader = new WebHeaderCollection();
                request.KeepAlive = true;
                request.Method = "get";


                HttpWebResponse responseResult = (HttpWebResponse)request.GetResponse();
                Encoding encode = Encoding.GetEncoding("UTF-8");
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

            backstring.Replace("html","htmls");
            backstring = "<html>" + backstring + "</html>";
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
           string xpath = "//bottum:div[@id='content']/bottum:div[@class='gengzong']/bottum:table/bottum:tr/bottum:td";

            XPathNodeIterator nodes = nav.Select(xpath, xnm);//xpath表达式

            ResultInfo backinfo = new ResultInfo(querynum);
            for (int i = 0; i < (nodes.Count-1) / 3; i++)
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
