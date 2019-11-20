using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.XPath;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging.Filters;
using AForge.Imaging;
using AForge.Genetic;
using AForge.Math;

namespace Express_Query
{
    class YunDaDecoder
    {
        struct partDiv
        {
            public int begin;
            public int end;
            public int length;

        }
        private Bitmap img;
        private partDiv[] div;
        public YunDaDecoder(Bitmap bmp)
        {
            img = bmp;
        }
        public int decode()
        {
            initBitmap(img);
            //bmp.Save("that.png");
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
                    if (r < limit && g < limit && b < limit)
                    {
                        img.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        img.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    }

                }

            //pbRuiHua.Image = img;
            //img.Save("justsee.png");
        }
        private void charCut(int[] anayData)
        {
            div = new partDiv[4];
            ////////////init div data
            {
                for (int i = 0; i < 4; i++)
                {
                    div[i].begin = 0;
                    div[i].end = 0;
                    div[i].length = 0;
                }

            }
            int limitMax = 5610;
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
        public int Decoder(Bitmap map)
        {
            string result = "";
            int a = int.Parse(find_char(map, div[0].begin, div[0].end));
            string op = find_char(map, div[1].begin, div[1].end);
            int b = int.Parse(find_char(map, div[2].begin, div[2].end));
            if (String.Compare(op, "12") == 0)
            {
                return a * b;
            }
            else
            {
                return a + b;
            }

        }
        public string find_char(Bitmap img, int xstart, int xend)
        {

            int distchx = 0, distchy = 0;
            double distmin = 10000;
            int width = xend - xstart + 1;
            int ystart = 0, yend = 0;
            int heigth = 0;
            //   int[,] data = new int[img.Height,xend-xstart+1];

            //////////获取字符区域的大小
            int[] data = new int[img.Height];
            for (int i = 0; i < img.Height; i++)
                for (int j = 0; j < width; j++)
                {
                    data[i] += 1 - (int)getgray(img, j + xstart, i) / 255;
                }

            {
                int k = 0;
                bool isstart = true;
                while (k < img.Height)
                {
                    if (data[k] != 0)
                    {
                        if (isstart)
                        {
                            ystart = k;
                            isstart = false;
                        }
                        yend = k;
                    }
                    k++;
                }

            }

            heigth = yend - ystart + 1;




            /* for (int s1 = 0; s1 < heigth; s1++)
            {
                for (int s2 = 0; s2 < width; s2++)
                {
                    System.Diagnostics.Debug.Write(getgray(img, xstart + s2, ystart + s1) / 255);
                }
                System.Diagnostics.Debug.Write('\n');
            }
             System.Diagnostics.Debug.Write("\n");*/


            for (int i = 0; i <= 12; i++)
            {
                string path = string.Format("yundamodel/font{0}.bmp", i);
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
                        int z, t;
                        double dist;
                        dist = 0;
                        for (t = 0; t < ymax - ymin; t++)
                            for (z = 0; z < xend - xstart + 1; z++)
                            {
                                int r2;
                                r = getgray(font, xmin + z, ymin + t);
                                r2 = getgray(img, x + z + xstart, y + t + ystart);
                                if (r != r2)
                                {

                                    dist += Math.Sqrt(Math.Pow(x + z, 2) + Math.Pow(y + t - ymin, 2));

                                }
                            }
                        //  dist = dist * 128 / font->glyphs[i].count;
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
        int getgray(Bitmap bmp, int x, int y)
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
        
        
    }
   
}
