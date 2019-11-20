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
    class ZhongTong
    {
        private string querynum;
        public ZhongTong(String num)
        {
            querynum = num;
        }
        public ResultInfo query()
        {
            try
            {
                string strURL = String.Format("http://www.zto.cn/bill.aspx");
                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
                WebHeaderCollection requestHeader;
                requestHeader = new WebHeaderCollection();
                request.KeepAlive = true;
                request.Method = "post";
                string postpara = String.Format("ID={0}&Submit4=%CC%E1%BD%BB", querynum);


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
             xnm.AddNamespace("bottum", "http://www.w3.org/1999/xhtml");
           
            XPathNavigator nav = doc.CreateNavigator();

            /////////////////根据网页返回结果分析

            string xpath = "//bottum:table[@id='ctl00_ContentPlaceHolder1_DataListBill_ctl00_GridViewBill']/bottum:tr/bottum:td";
            XPathNodeIterator nodes = nav.Select(xpath, xnm);//xpath表达式

            ResultInfo backinfo = new ResultInfo(querynum);
          
            for (int i = 0; i < nodes.Count / 3; i++)
            {
                nodes.MoveNext();
                string time = nodes.Current.Value;
                nodes.MoveNext();
                nodes.MoveNext();
                string state = nodes.Current.Value;
                backinfo.add(time, state);
                nodes.MoveNext();
            }
            string xpath2 = "//bottum:table[@id='ctl00_ContentPlaceHolder1_DataListBill_ctl00_GridViewBillSign']/bottum:tr/bottum:td";

            XPathNodeIterator nodes2 = nav.Select(xpath2, xnm);
               if(nodes2.Count>0)
               {

                nodes2.MoveNext();
                string time = nodes2.Current.Value;
                nodes2.MoveNext();
                nodes2.MoveNext();
                string state = nodes2.Current.Value;
                backinfo.add(time, state);
                nodes2.MoveNext();
               }
               reader.Close();
               writer.Close();
               sw.Close();
            return backinfo;
        }
    }
}
