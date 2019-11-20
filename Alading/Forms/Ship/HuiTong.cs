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
    class HuiTong
    {
         private string querynum;
        public HuiTong(String  num)
        {
            querynum = num;
        }
        public ResultInfo query()
        {
            try
            {
                string strURL = String.Format("http://www.htky365.com/track.do");
                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
                WebHeaderCollection requestHeader;
                requestHeader = new WebHeaderCollection();
                request.KeepAlive = true;
                request.Method = "post";

                string postpara = String.Format("inputNumber={0}", querynum);
                byte[] postparaBin = Encoding.ASCII.GetBytes(postpara);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postparaBin.Length;
                Stream outStream = request.GetRequestStream();
                outStream.Write(postparaBin, 0, postparaBin.Length);
                outStream.Close();
                HttpWebResponse responseResult = (HttpWebResponse)request.GetResponse();
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
            backstring = backstring.Replace("<a title=\"aa\">","");
            backstring = backstring.Replace("xmlns=\"http://www.w3.org/1999/xhtml\"","");
            backstring = backstring.Replace("<br>","");
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

            string xpath = "//html/body/table/tr[3]/td/table/tr[2]/td/table/tr[2]/td/table/tr[1]/td[2]/table/tr[2]/td/table[2]/tr/td";
            XPathNodeIterator nodes = nav.Select(xpath, xnm);
           
            ResultInfo backinfo = new ResultInfo(querynum);

            
            if (nodes.Count > 2)
            {
                nodes.MoveNext();
                nodes.MoveNext();
            }
          
            for (int i = 1; i < nodes.Count/2; i++)
            {
                nodes.MoveNext();
                string time=nodes.Current.Value;
                time.Replace("\n","");
                time.Replace("\t","");
                 nodes.MoveNext();
                 string state = nodes.Current.Value;
                 state = state.Replace("\t","");
                 state = state.Replace("\n","");
               
                backinfo.add(time, state);
            }
            string xpath2 = "//html/body/table/tr[3]/td/table/tr[2]/td/table/tr[2]/td/table/tr[1]/td[2]/table/tr[2]/td/table[3]/tr[2]/td";
            XPathNodeIterator nodes2 = nav.Select(xpath2, xnm);
            if (nodes2.Count > 0)
            {
                nodes2.MoveNext();
                string time = nodes2.Current.Value;
                time.Replace("\n", "");
                time.Replace("\t", "");
                nodes2.MoveNext();
                string state = nodes2.Current.Value;
                state = state.Replace("\t", "");
                state = state.Replace("\n", "");

                backinfo.add(time, state);
            }
            reader.Close();
            writer.Close();
            sw.Close();
            return backinfo;
           
        }
       
    }
}
