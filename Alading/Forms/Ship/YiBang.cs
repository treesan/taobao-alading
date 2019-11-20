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
using AForge.Imaging.Filters;
using AForge.Imaging;
using AForge.Genetic;
using AForge.Math;

namespace Express_Query
{
    class YiBang
    {
        struct partDiv
        {
            public int begin;
            public int end;
            public int length;

        }
        private partDiv[] div;
        private string querynum;
        public YiBang(string num)
        {
            querynum = num;
        }
        public ResultInfo query()
        {
            try
            {
                string strURL = "http://www.ebon-express.com/";
                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
                WebHeaderCollection requestHeader;
                requestHeader = new WebHeaderCollection();
                request.KeepAlive = true;
                request.Method = "get";
                System.Net.HttpWebResponse responseMain;
                CookieContainer container;
                request.CookieContainer = new CookieContainer();
                responseMain = (System.Net.HttpWebResponse)request.GetResponse();
                container = request.CookieContainer;
                responseMain.Close();
                ///////////设置请求验证码的图片头,并获取会话id
                HttpWebRequest requestPic;
                string urlPic = "http://www.ebon-express.com/admin/getcode.asp";
                requestPic = (HttpWebRequest)HttpWebRequest.Create(urlPic);
                requestPic.CookieContainer = container;
                requestPic.Method = "get";

                /////////////////获取验证码头

                HttpWebResponse responsePic = (HttpWebResponse)requestPic.GetResponse();
                requestPic.ContentType = "	image/BMP";
                container = requestPic.CookieContainer;
                Stream resStream = responsePic.GetResponseStream();//得到验证码数据流
                Bitmap sourcebm = new Bitmap(resStream);
               // sourcebm.Save("see.bmp");
                string validcode = decode(sourcebm);
                resStream.Close();

                string resultUrl = "http://www.ebon-express.com/E3query/query.asp";
                HttpWebRequest requestResult;
                requestResult = (HttpWebRequest)HttpWebRequest.Create(resultUrl);

            
                requestResult.CookieContainer = container;
                requestResult.Method = "post";
                requestResult.ContentType = "application/x-www-form-urlencoded";
                string parameterStr = String.Format("BillCode={0}&mofei={1}", querynum, validcode);
                byte[] payload;
                //将URL编码后的字符串转化为字节
                payload = System.Text.Encoding.UTF8.GetBytes(parameterStr);
                requestResult.ContentLength = payload.Length;      
                Stream writer = requestResult.GetRequestStream();
                writer.Write(payload, 0, payload.Length);
                writer.Close();
                HttpWebResponse responseResult = (HttpWebResponse)requestResult.GetResponse();
                Encoding encode = Encoding.GetEncoding("UTF-8");
                System.IO.Stream s;
                s = responseResult.GetResponseStream();
                StreamReader reader = new StreamReader(s, Encoding.GetEncoding("GB2312"));
                String backstring = reader.ReadToEnd();
                reader.Close();
                s.Close();
                responseResult.Close();
                ResultInfo backinfo = getDetail(backstring);
                return backinfo;
            }
            catch (Exception e)
            {
                return new ResultInfo(querynum);
            }

        }
        private string decode(Bitmap img )
        {
            initBitmap(img);

            HorizontalIntensityStatistics his = new HorizontalIntensityStatistics(img);


            Histogram histogram = his.Blue;
            int[] dato = histogram.Values;
            charCut(dato);
           return Decoder(img);
          
        }


        private int getPiexl(Bitmap bmp, int x, int y)
        {
            if (x < 0 || y < 0 || (x >= bmp.Width) || (y >= bmp.Height))
            {
                int g = 0;
                return g;
            }
            else
            {
                int g = bmp.GetPixel(x, y).G;
                return g;
            }
        }
        private void initBitmap(Bitmap img)
        {
            int r, g, b;
            int limit = 150;
            for (int i = 0; i < img.Width; i++)
                for (int j = 0; j < img.Height; j++)
                {
                    r = img.GetPixel(i, j).R;
                    g = img.GetPixel(i, j).G;
                    b = img.GetPixel(i, j).B;
                    if (r > limit && g > limit && b > limit)
                    {
                        img.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        img.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    }

                }
        }
        private void charCut(int[] anayData)
        {
            div = new partDiv[4];
            {
                for (int i = 0; i < 4; i++)
                {
                    div[i].begin = 0;
                    div[i].end = 0;
                    div[i].length = 0;
                }

            }
            int limitMax = 2550;
            int beginScan = 0;  /////////////扫描数组计数
            int partlen = 0;  //////////////分块长度
            int partNum = 0;  //////////////当前块数
            while ((beginScan < anayData.Length) && (partNum < 4))
            {
                if ((anayData[beginScan] == limitMax) && partlen == 0)
                {
                    beginScan++;
                    continue;
                }
                if ((anayData[beginScan] == limitMax) && partlen != 0)
                {
                    div[partNum].end = beginScan - 1;
                    div[partNum].length = partlen;
                    partlen = 0;
                    partNum++;
                    beginScan++;
                    continue;
                }
                if ((anayData[beginScan] != limitMax) && partlen == 0)
                {
                    div[partNum].begin = beginScan;
                    partlen = 1;
                    beginScan++;
                    continue;
                }
                beginScan++;
                partlen++;
            }

        }
        private string Decoder(Bitmap map)
        {
            string result = "";
            for (int i = 0; i < 4; i++)
            {
                result += find_char(map, div[i].begin, div[i].end);
            }
            return result;
        }
        private string find_char(Bitmap img, int xstart, int xend)
        {
            int distchx = 0, distchy = 0;
            double distmin = 10000;
      

            for (int i = 0; i < 10; i++)
            {
                string path = string.Format("yibangmodel/font{0}.bmp", i);
                Bitmap font = new Bitmap(path);
               
                double localmin = 10000, localx, localy;

                int xmin = 0;
                int ymin = 0;
                int xmax = font.Width;
                int ymax = font.Height;
                int r;

                for (int y = 0; y < 1; y++)
                    for (int x = 0; x < 1; x++)
                    {
                        int z, t, dist;
                        dist = 0;
                        for (t = 0; t < ymax - ymin; t++)
                            for (z = 0; z < xend - xstart + 1; z++)
                            {
                                int r2;
                                r = getPiexl(font, xmin + z, ymin + t);
                                r2 = getPiexl(img, x + z + xstart, y + t + ymin);
                                if (r != r2)
                                    dist += (int)Math.Sqrt(Math.Pow(x + z, 2) + Math.Pow(y + t - ymin, 2));
                            }
                        if (dist < localmin)
                        {
                            localmin = dist;
                            localx = x;
                            localy = y;

                        }
                    }
                if (localmin < distmin)
                {
                    distmin = localmin;

                    distchx = i;
                }

            }

            return string.Format("{0}", distchx); ;
        }

        public ResultInfo getDetail(string backstring)
        {
            backstring = backstring.Replace("xmlns=\"http://www.w3.org/1999/xhtml\"", "");
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
            string xpath = "/html/body/table[8]/tr/td";
            XPathNodeIterator nodes = nav.Select(xpath, xnm);//xpath表达式
            if (nodes.Count >=2)
            {
                nodes.MoveNext();
                nodes.MoveNext();
            }
            ResultInfo backinfo = new ResultInfo(querynum);
            for (int i = 1; i < nodes.Count / 2; i++)
            {
                nodes.MoveNext();
                string time = nodes.Current.Value;
                nodes.MoveNext();
                string state = nodes.Current.Value;
                backinfo.add(time,state);
            }
            reader.Close();
            writer.Close();
            sw.Close();
            return backinfo;
        }

    }
}
