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
    class ZhongTie
    {

          private string querynum;
        public ZhongTie(String num)
        {
            querynum = num;
        }
        public ResultInfo query()
        {
            try
            {
                string strURL = String.Format("http://www.cre.cn:81/creweb/cms/web/Fhwcxselect.jsp");
                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
                WebHeaderCollection requestHeader;
                requestHeader = new WebHeaderCollection();
                request.KeepAlive = true;
                request.Method = "post";
                string postpara = String.Format("sheet_no={0}&Submit=%CC%E1%BD%BB", querynum);


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
            string xpath = "//FORM[@ name='form1']/table/tr[2]/td[2]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tr/td";
            string str = "";
            XPathNodeIterator nodes = nav.Select(xpath, xnm);//xpath表达式        
            ResultInfo backinfo = new ResultInfo(querynum);     
            for (int i = 0; i < nodes.Count / 4; i++)
            {
               
                nodes.MoveNext();
                string state = nodes.Current.Value;
                nodes.MoveNext();
                state = state + nodes.Current.Value;
                nodes.MoveNext();
                string time = nodes.Current.Value;
                nodes.MoveNext();
                time = time + nodes.Current.Value;
                backinfo.add(time, state);
               
            }
            reader.Close();
            writer.Close();
            sw.Close();
          
            return backinfo;
        }
    }
}
