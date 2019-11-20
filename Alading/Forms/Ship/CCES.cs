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
    class CCES
    {

         private string querynum;
        public CCES(String  num)
        {
            querynum = num;
        }
        public ResultInfo query()
        {
            try
            {   
                /////////格式化参数，创建http请求头
                string param = string.Format("__LASTFOCUS=&__VIEWSTATE=%2FwEPDwUKLTEwMzE2NTE2Mw8WAh4HSXNBZG1pbgUCTk8WAgIDD2QWAgIRDw8WAh4EVGV4dAWaLTx0YWJsZSBJRD1NYW5pZmVzdDAgICBib3JkZXI9JzAnIGNlbGxwYWRkaW5nPScxJyBiZ0NvbG9yPSdnYWluc2Jvcm8nDQogICAgICAgICAgICAgICAgICAgICAgICBjZWxsc3BhY2luZz0nMScgY2xhc3M9J3F1ZXJ5MicgV2lkdGg9JzY3MHB4Jz4gPHRyIGFsaWduPW1pZGRsZT4gICA8dGQgd2lkdGg9MTIwICBiZ0NvbG9yPScjZWZmNWZiJz48c3Ryb25nPjxmb250IGNvbG9yPSMzYTYxODg%2B6L%2BQ5Y2V5Y%2B356CBPC9mb250Pjwvc3Ryb25nPjwvdGQ%2BICAgPHRkIHdpZHRoPTE0MCAgYmdDb2xvcj0nI2VmZjVmYic%2BPHN0cm9uZz48Zm9udCBjb2xvcj0jM2E2MTg4PuaXpeacnzwvZm9udD48L3N0cm9uZz48L3RkPiAgPHRkIHdpZHRoPTkwICBiZ0NvbG9yPScjZWZmNWZiJz48c3Ryb25nPjxmb250IGNvbG9yPSMzYTYxODg%2B5aeL5Y%2BR56uZPC9mb250Pjwvc3Ryb25nPjwvdGQ%2BICA8dGQgICBiZ0NvbG9yPScjZWZmNWZiJz48c3Ryb25nPjxmb250IGNvbG9yPSMzYTYxODg%2B55uu55qE5ZywPC9mb250Pjwvc3Ryb25nPjwvdGQ%2BICA8dGQgICBiZ0NvbG9yPScjZWZmNWZiJz48c3Ryb25nPjxmb250IGNvbG9yPSMzYTYxODg%2B5Lu25pWwPC9mb250Pjwvc3Ryb25nPjwvdGQ%2BICA8dGQgd2lkdGg9OTAgIGJnQ29sb3I9JyNlZmY1ZmInPjxzdHJvbmc%2BPGZvbnQgY29sb3I9IzNhNjE4OD7nrb7mlLbkuro8L2ZvbnQ%2BPC9zdHJvbmc%2BPC90ZD4gIDx0ZCB3aWR0aD0xNDAgIGJnQ29sb3I9JyNlZmY1ZmInPjxzdHJvbmc%2BPGZvbnQgY29sb3I9IzNhNjE4OD7nrb7mlLbml7bpl7Q8L2ZvbnQ%2BPC9zdHJvbmc%2BPC90ZD4gIDx0ZCB3aWR0aD0xNDAgIGJnQ29sb3I9JyNlZmY1ZmInPjxzdHJvbmc%2BPGZvbnQgY29sb3I9IzNhNjE4OD7nrb7mlLblm77niYc8L2ZvbnQ%2BPC9zdHJvbmc%2BPC90ZD48L3RyPjx0ciBhbGlnbj1taWRkbGU%2BICA8dGQgYWxpZ249Y2VudGVyIGJnQ29sb3I9JyNGRkZGRkYnPjIxNjg0NTAzMjQ8L3RkPiAgPHRkIGFsaWduPWNlbnRlciBiZ0NvbG9yPScjRkZGRkZGJz4yMDEwLTEtMTAgMjA6MzQ6MDc8L3RkPiAgPHRkIGFsaWduPWNlbnRlciBiZ0NvbG9yPScjRkZGRkZGJz7kuYnkuYw8L3RkPiAgPHRkIGFsaWduPWNlbnRlciBiZ0NvbG9yPScjRkZGRkZGJz7ljJfkuqzluII8L3RkPiAgPHRkIGFsaWduPWNlbnRlciBiZ0NvbG9yPScjRkZGRkZGJz4xPC90ZD4gIDx0ZCBhbGlnbj1jZW50ZXIgYmdDb2xvcj0nI0ZGRkZGRic%2BMDEwMDI2NjI2MjA1PC90ZD4gIDx0ZCBhbGlnbj1jZW50ZXIgYmdDb2xvcj0nI0ZGRkZGRic%2BMjAxMC0xLTE0IDIwOjExOjA1PC90ZD4gIDx0ZCBhbGlnbj1jZW50ZXIgYmdDb2xvcj0nI0ZGRkZGRic%2BPGlmcmFtZSBpZD1JRF8nMjE2ODQ1MDMyNCcgbmFtZT1OYW1lJzIxNjg0NTAzMjQnIG1hcmdpbndpZHRoPTAgbWFyZ2luaGVpZ2h0PTAgICAgICAgc3JjID0gVmlld1NpZ25CeUltYWdlSW5kZXguYXNwP0pvYk5PPTIxNjg0NTAzMjQgICAgZnJhbWVib3JkZXI9MCB3aWR0aD0yMDBweCBzY3JvbGxpbmc9eWVzIGhlaWdodD0yMDBweCBydW5hdD1zZXJ2ZXIgei1pbmRleD0tNT48L2lmcmFtZT48L3RyPgk8L3RhYmxlPjx0YWJsZSBJRD1QT0QwICAgYm9yZGVyPScwJyBjZWxscGFkZGluZz0nMScgYmdDb2xvcj0nZ2FpbnNib3JvJw0KICAgICAgICAgICAgICAgICAgICAgICAgICAgY2VsbHNwYWNpbmc9JzEnIGNsYXNzPSdxdWVyeTInIFdpZHRoPSc2NzBweCc%2BPHRyIGFsaWduPW1pZGRsZT4gICA8dGQgd2lkdGg9MTQwIGJnQ29sb3I9JyNlZmY1ZmInPuaJq%2BaPj%2BaXtumXtDwvdGQ%2BICA8dGQgIGJnQ29sb3I9JyNlZmY1ZmInPueCuei0p%2Bi1hOaWmTwvdGQ%2BICA8dGQgIGJnQ29sb3I9JyNlZmY1ZmInPueCueWHu%2Bafpeeci%2Be9keeCueiBlOe7nOS%2FoeaBrzwvdGQ%2BPC90cj4JPHRyIGFsaWduPW1pZGRsZT4gIDx0ZCBhbGlnbj1jZW50ZXIgYmdDb2xvcj0nI0ZGRkZGRic%2BMjAxMC0xLTEwIDIwOjM0OjA3PC90ZD4gIDx0ZCAgYmdDb2xvcj0nI0ZGRkZGRicgYWxpZ249bGVmdD7kuYnkuYzmlLbku7blhaXlupM8L3RkPiAgPHRkIGFsaWduPWNlbnRlciBiZ0NvbG9yPScjRkZGRkZGJz48YSBocmVmPSdWaWV3Q29udGFjdC5hc3B4P0NvZGU9NTc5MDYnPlvkuYnkuYxdPC9hPjwvdGQ%2BPC90cj4JPHRyIGFsaWduPW1pZGRsZT4gIDx0ZCBhbGlnbj1jZW50ZXIgYmdDb2xvcj0nI0ZGRkZGRic%2BMjAxMC0xLTEwIDIwOjM1OjA3PC90ZD4gIDx0ZCAgYmdDb2xvcj0nI0ZGRkZGRicgYWxpZ249bGVmdD7nprvlvIDkuYnkuYzvvIzlj5HlvoDmna3lt57liIbmi6jkuK3lv4M8L3RkPjx0ZCBhbGlnbj1jZW50ZXIgYmdDb2xvcj0nI0ZGRkZGRic%2BPGEgaHJlZj0nVmlld0NvbnRhY3QuYXNweD9Db2RlPTU3MTAwJz5b5p2t5bee5YiG5ouo5Lit5b%2BDXTwvYT4mbmJzcDsmbmJzcDsmbmJzcDs8YSBocmVmPSdWaWV3Q29udGFjdC5hc3B4P0NvZGU9NTcxMDAnPlvmna3lt57liIbmi6jkuK3lv4NdPC9hPjwvdHI%2BCTx0ciBhbGlnbj1taWRkbGU%2BICA8dGQgYWxpZ249Y2VudGVyIGJnQ29sb3I9JyNGRkZGRkYnPjIwMTAtMS0xMSAwOjA0OjQ1PC90ZD4gIDx0ZCAgYmdDb2xvcj0nI0ZGRkZGRicgYWxpZ249bGVmdD7liLDovr7mna3lt57liIbmi6jkuK3lv4M8L3RkPjx0ZCBhbGlnbj1jZW50ZXIgYmdDb2xvcj0nI0ZGRkZGRic%2BPGEgaHJlZj0nVmlld0NvbnRhY3QuYXNweD9Db2RlPTU3OTA2Jz5b5LmJ5LmMXTwvYT4mbmJzcDsmbmJzcDsmbmJzcDs8YSBocmVmPSdWaWV3Q29udGFjdC5hc3B4P0NvZGU9NTc5MDYnPlvkuYnkuYxdPC9hPjwvdHI%2BCTx0ciBhbGlnbj1taWRkbGU%2BICA8dGQgYWxpZ249Y2VudGVyIGJnQ29sb3I9JyNGRkZGRkYnPjIwMTAtMS0xMSAwOjExOjQzPC90ZD4gIDx0ZCAgYmdDb2xvcj0nI0ZGRkZGRicgYWxpZ249bGVmdD7nprvlvIDmna3lt57liIbmi6jkuK3lv4PvvIzlj5HlvoDml6DplKHliIbmi6jkuK3lv4M8L3RkPjx0ZCBhbGlnbj1jZW50ZXIgYmdDb2xvcj0nI0ZGRkZGRic%2BPGEgaHJlZj0nVmlld0NvbnRhY3QuYXNweD9Db2RlPTUxMDA0Jz5b5peg6ZSh5YiG5ouo5Lit5b%2BDXTwvYT4mbmJzcDsmbmJzcDsmbmJzcDs8YSBocmVmPSdWaWV3Q29udGFjdC5hc3B4P0NvZGU9NTEwMDQnPlvml6DplKHliIbmi6jkuK3lv4NdPC9hPjwvdHI%2BCTx0ciBhbGlnbj1taWRkbGU%2BICA8dGQgYWxpZ249Y2VudGVyIGJnQ29sb3I9JyNGRkZGRkYnPjIwMTAtMS0xMSA1OjE1OjQ3PC90ZD4gIDx0ZCAgYmdDb2xvcj0nI0ZGRkZGRicgYWxpZ249bGVmdD7nprvlvIDml6DplKHliIbmi6jkuK3lv4PvvIzlj5HlvoDljJfkuqzliIbmi6g8L3RkPjx0ZCBhbGlnbj1jZW50ZXIgYmdDb2xvcj0nI0ZGRkZGRic%2BPGEgaHJlZj0nVmlld0NvbnRhY3QuYXNweD9Db2RlPTAxMDAwJz5b5YyX5Lqs5YiG5ouoXTwvYT4mbmJzcDsmbmJzcDsmbmJzcDs8YSBocmVmPSdWaWV3Q29udGFjdC5hc3B4P0NvZGU9MDEwMDAnPlvljJfkuqzliIbmi6hdPC9hPjwvdHI%2BCTx0ciBhbGlnbj1taWRkbGU%2BICA8dGQgYWxpZ249Y2VudGVyIGJnQ29sb3I9JyNGRkZGRkYnPjIwMTAtMS0xMiAxMDowNjo1NjwvdGQ%2BICA8dGQgIGJnQ29sb3I9JyNGRkZGRkYnIGFsaWduPWxlZnQ%2B5Yiw6L6%2B5YyX5Lqs5YiG5ouoPC90ZD48dGQgYWxpZ249Y2VudGVyIGJnQ29sb3I9JyNGRkZGRkYnPjxhIGhyZWY9J1ZpZXdDb250YWN0LmFzcHg%2FQ29kZT01MTAwNCc%2BW%2BaXoOmUoeWIhuaLqOS4reW%2Fg108L2E%2BJm5ic3A7Jm5ic3A7Jm5ic3A7PGEgaHJlZj0nVmlld0NvbnRhY3QuYXNweD9Db2RlPTUxMDA0Jz5b5peg6ZSh5YiG5ouo5Lit5b%2BDXTwvYT48L3RyPgk8dHIgYWxpZ249bWlkZGxlPiAgPHRkIGFsaWduPWNlbnRlciBiZ0NvbG9yPScjRkZGRkZGJz4yMDEwLTEtMTIgMjA6NTg6NDc8L3RkPiAgPHRkICBiZ0NvbG9yPScjRkZGRkZGJyBhbGlnbj1sZWZ0PuWIsOi%2BvuWMl%2BS6rOS4sOWPsOWfuuWcsDwvdGQ%2BPHRkIGFsaWduPWNlbnRlciBiZ0NvbG9yPScjRkZGRkZGJz48YSBocmVmPSdWaWV3Q29udGFjdC5hc3B4P0NvZGU9MDEwMDAnPlvljJfkuqzliIbmi6hdPC9hPiZuYnNwOyZuYnNwOyZuYnNwOzxhIGhyZWY9J1ZpZXdDb250YWN0LmFzcHg%2FQ29kZT0wMTAwMCc%2BW%2BWMl%2BS6rOWIhuaLqF08L2E%2BPC90cj4JPHRyIGFsaWduPW1pZGRsZT4gIDx0ZCBhbGlnbj1jZW50ZXIgYmdDb2xvcj0nI0ZGRkZGRic%2BMjAxMC0xLTEzIDIwOjEwOjEyPC90ZD4gIDx0ZCAgYmdDb2xvcj0nI0ZGRkZGRicgYWxpZ249bGVmdD7lv6vku7botoXljLo8L3RkPjx0ZCBhbGlnbj1jZW50ZXIgYmdDb2xvcj0nI0ZGRkZGRic%2BPGEgaHJlZj0nVmlld0NvbnRhY3QuYXNweD9Db2RlPSc%2BW108L2E%2BJm5ic3A7Jm5ic3A7Jm5ic3A7PGEgaHJlZj0nVmlld0NvbnRhY3QuYXNweD9Db2RlPSc%2BW108L2E%2BPC90cj4JPHRyIGFsaWduPW1pZGRsZT4gIDx0ZCBhbGlnbj1jZW50ZXIgYmdDb2xvcj0nI0ZGRkZGRic%2BMjAxMC0xLTE0IDEyOjQ1OjU4PC90ZD4gIDx0ZCAgYmdDb2xvcj0nI0ZGRkZGRicgYWxpZ249bGVmdD7liLDovr7ljJfkuqzliIbmi6g8L3RkPjx0ZCBhbGlnbj1jZW50ZXIgYmdDb2xvcj0nI0ZGRkZGRic%2BPGEgaHJlZj0nVmlld0NvbnRhY3QuYXNweD9Db2RlPTAxMDYwMSc%2BW%2BWMl%2BS6rOS4sOWPsOWfuuWcsF08L2E%2BJm5ic3A7Jm5ic3A7Jm5ic3A7PGEgaHJlZj0nVmlld0NvbnRhY3QuYXNweD9Db2RlPTAxMDYwMSc%2BW%2BWMl%2BS6rOS4sOWPsOWfuuWcsF08L2E%2BPC90cj4JPHRyIGFsaWduPW1pZGRsZT4gIDx0ZCBhbGlnbj1jZW50ZXIgYmdDb2xvcj0nI0ZGRkZGRic%2BMjAxMC0xLTE0IDEzOjM5OjM1PC90ZD4gIDx0ZCAgYmdDb2xvcj0nI0ZGRkZGRicgYWxpZ249bGVmdD7nprvlvIDljJfkuqzliIbmi6jvvIzlj5HlvoDljJfkuqzkuLDlj7Dln7rlnLA8L3RkPjx0ZCBhbGlnbj1jZW50ZXIgYmdDb2xvcj0nI0ZGRkZGRic%2BPGEgaHJlZj0nVmlld0NvbnRhY3QuYXNweD9Db2RlPTAxMDYwMSc%2BW%2BWMl%2BS6rOS4sOWPsOWfuuWcsF08L2E%2BJm5ic3A7Jm5ic3A7Jm5ic3A7PGEgaHJlZj0nVmlld0NvbnRhY3QuYXNweD9Db2RlPTAxMDYwMSc%2BW%2BWMl%2BS6rOS4sOWPsOWfuuWcsF08L2E%2BPC90cj4JPHRyIGFsaWduPW1pZGRsZT4gIDx0ZCBhbGlnbj1jZW50ZXIgYmdDb2xvcj0nI0ZGRkZGRic%2BMjAxMC0xLTE0IDE5OjEzOjI5PC90ZD4gIDx0ZCAgYmdDb2xvcj0nI0ZGRkZGRicgYWxpZ249bGVmdD7nprvlvIDljJfkuqzliIbmi6jvvIzlj5HlvoDlkIzooYzkuow8L3RkPjx0ZCBhbGlnbj1jZW50ZXIgYmdDb2xvcj0nI0ZGRkZGRic%2BPGEgaHJlZj0nVmlld0NvbnRhY3QuYXNweD9Db2RlPTAyMTE3OSc%2BW%2BWQjOihjOS6jF08L2E%2BJm5ic3A7Jm5ic3A7Jm5ic3A7PGEgaHJlZj0nVmlld0NvbnRhY3QuYXNweD9Db2RlPTAyMTE3OSc%2BW%2BWQjOihjOS6jF08L2E%2BPC90cj4JPHRyIGFsaWduPW1pZGRsZT4gIDx0ZCBhbGlnbj1jZW50ZXIgYmdDb2xvcj0nI0ZGRkZGRic%2BMjAxMC0xLTE0IDIwOjEwOjA1PC90ZD4gIDx0ZCAgYmdDb2xvcj0nI0ZGRkZGRicgYWxpZ249bGVmdD7mtL7ku7bkuK08L3RkPiAgPHRkIGFsaWduPWNlbnRlciBiZ0NvbG9yPScjRkZGRkZGJz48YSBocmVmPSdWaWV3Q29udGFjdC5hc3B4P0NvZGU9MDEwMDEnPlvljJfkuqxdPC9hPjwvdGQ%2BPC90cj4JPHRyIGFsaWduPW1pZGRsZT4gIDx0ZCBhbGlnbj1jZW50ZXIgYmdDb2xvcj0nI0ZGRkZGRic%2BMjAxMC0xLTE0IDIwOjExOjA1PC90ZD4gIDx0ZCAgYmdDb2xvcj0nI0ZGRkZGRicgYWxpZ249bGVmdD7nrb7mlLbkurrvvJowMTAwMjY2MjYyMDU8L3RkPiAgPHRkIGFsaWduPWNlbnRlciBiZ0NvbG9yPScjRkZGRkZGJz48YSBocmVmPSdWaWV3Q29udGFjdC5hc3B4P0NvZGU9MDEwMDEnPlvljJfkuqxdPC9hPjwvdGQ%2BPC90cj4JPC90YWJsZT48QlI%2BZGRkg%2B1nObY21TMsZqemlxW8nOhdDIk%3D&__EVENTTARGET=&__EVENTARGUMENT=&__EVENTVALIDATION=%2FwEWBgK%2Fq%2Ba6BwK%2FyIGFCAKvp6vrBALW9oXuAgLvjry%2FBQKQ9M%2FrBY%2F2pENSdpZbWd%2F02Mf%2FsFi1j7Bm&RdList=0&txtJobNoList={0}&btnQuery=%E6%9F%A5%E8%AF%A2", querynum);
                string strURL = String.Format("http://222.73.182.28/express_oa/ui/trackpod_cces/frmtrackpod_cces.aspx");
                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
                WebHeaderCollection requestHeader;
                requestHeader = new WebHeaderCollection();
                request.KeepAlive = true;
                request.Method = "post";
                byte[] postparaBin = Encoding.ASCII.GetBytes(param);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postparaBin.Length;
                Stream outStream = request.GetRequestStream();
                outStream.Write(postparaBin, 0, postparaBin.Length);
                outStream.Close();
                HttpWebResponse responseResult = (HttpWebResponse)request.GetResponse();
                Encoding encode = Encoding.GetEncoding("UTF-8");
                System.IO.Stream s;
                s = responseResult.GetResponseStream();
                StreamReader reader = new StreamReader(s, Encoding.GetEncoding("UTF-8"));
                String backstring = reader.ReadToEnd();
                reader.Close();
                s.Close();
                responseResult.Close();
                return getDetail(backstring);
            }
            catch(Exception e)
            {
                return new ResultInfo(querynum);
            }
        }

        private ResultInfo getDetail(string backstring)
        {
        
            backstring = backstring.Replace("xmlns=\"http://www.w3.org/1999/xhtml\"","");
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

            string xpath = "//span[@id='lblPODInfo']/table[2]/tr/td";

            XPathNodeIterator nodes = nav.Select(xpath, xnm);//xpath表达式

            nodes.MoveNext();////////////跳过表格头
            nodes.MoveNext();
            nodes.MoveNext();
            ResultInfo backinfo = new ResultInfo(querynum);
   
            for (int i = 1; i < nodes.Count/3; i++)
            {
                nodes.MoveNext();
                string time=nodes.Current.Value;
                 nodes.MoveNext();
                 string state = nodes.Current.Value;
                 nodes.MoveNext();
                backinfo.add(time, state);
            }
            reader.Close();
            writer.Close();
            sw.Close();
            return backinfo;
           
        }
    }
}
