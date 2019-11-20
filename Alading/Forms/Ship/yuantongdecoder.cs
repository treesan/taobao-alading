using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Express_Query
{
    class yuantongdecoder
    {
        public yuantongdecoder()
        { }
        public string Decoder(Bitmap map, partDiv[] div)
        {
            string result = "";
            for (int i = 0; i < 4; i++)
            {
                result += find_char(map, div[i].begin, div[i].end);
            }
            return result;
        }
        /// <summary>
        /// 匹配单个字符，得到结果
        /// </summary>
        /// <param name="img"></param>
        /// <param name="xstart"></param>
        /// <param name="xend"></param>
        /// <returns></returns>
        public string find_char(Bitmap img, int xstart, int xend)
        {
            int distchx = 0, distchy = 0;
            double distmin = 10000;
            int[,] data = new int[img.Height, img.Width];

            //////////运用模板匹配字符
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 2; j++)
                {
                    string path = string.Format("yuantongmodel/fenjie{0}-{1}.bmp", i, j + 1);
                    Bitmap font = new Bitmap(path);
                    double localmin = 10000, localx, localy;

                    int xmin = 0;
                    int ymin = 2;
                    int xmax = font.Width;
                    int ymax = 22;
                    int r;
                    ///////////////设置驱动范围，参考pwntcha项目
                    for (int y = -2; y < 2; y++)
                        for (int x = -2; x < 2; x++)
                        {
                            int z, t, dist;
                            dist = 0;
                            for (t = 0; t < ymax - ymin; t++)
                                for (z = 0; z < xend - xstart + 1; z++)
                                {
                                    int r2;
                                    r = getgray(font, xmin + z, ymin + t);
                                    r2 = getgray(img, x + z + xstart, y + t + ymin);
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
                        distchy = j;
                    }
                    font.Save("font.bmp");
                }

            return string.Format("{0}", distchx); 
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
