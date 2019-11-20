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
    class DeBang
    {

        private string querynum;
        public DeBang(String  num)
        {
            querynum = num;
        }
        public ResultInfo query()
        {

            try
            {
                string strURL = String.Format("http://www.deppon.com/online/Tracking_result.aspx?catid=41|133&billList={0}", querynum);
                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
                WebHeaderCollection requestHeader;
                requestHeader = new WebHeaderCollection();
                request.KeepAlive = true;
                request.Method = "get";
                HttpWebResponse responseResult = (HttpWebResponse)request.GetResponse();
                System.IO.Stream s;
                s = responseResult.GetResponseStream();
                StreamReader reader = new StreamReader(s, Encoding.GetEncoding("UTF-8"));
                String backstring = reader.ReadToEnd();
                reader.Close();
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
            xnm.AddNamespace("bottum", "http://www.w3.org/1999/xhtml");
     
            XPathNavigator nav = doc.CreateNavigator();

            /////////////////根据网页返回结果分析

            string xpath = "//bottum:table[@id='ctl00_ContentPlaceHolder1_TrackDetail']/bottum:tr/bottum:td/bottum:div[8]/bottum:table/bottum:tr/bottum:td";
            XPathNodeIterator nodes = nav.Select(xpath, xnm);//xpath表达式
            ResultInfo backinfo = new ResultInfo(querynum);
           
            if (nodes.Count > 3)
            {
                nodes.MoveNext();
                nodes.MoveNext();
                nodes.MoveNext();
            }
            for (int i = 1; i < nodes.Count/ 3; i++)
            {
                nodes.MoveNext();
                string time = nodes.Current.Value;
                nodes.MoveNext();
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
