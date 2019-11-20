using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using AForge.Imaging.Filters;
using AForge.Imaging;
using AForge.Genetic;
using AForge.Math;


namespace Express_Query
{


    
    class ShunfengDecoderString
    {
        partDiv[] div;
        public string decodeBitmap(Bitmap img)
        {
            /////////图像处理
            //转为灰度图
            Grayscale grayscaleFilter = new Grayscale(0.299, 0.587, 0.114);
            Bitmap bitmapGrayscale = grayscaleFilter.Apply(img);
            //二值化
            Threshold thresholdFilter = new Threshold(128);
            Bitmap bitmapThreshold = thresholdFilter.Apply(bitmapGrayscale);
            ////////中值滤波
            Median mediafil = new Median();
            Bitmap medianBit = mediafil.Apply(bitmapThreshold);
            //锐化
            Sharpen sharpPic = new Sharpen();
            Bitmap sharpOut = sharpPic.Apply(medianBit);
            //统计点数
            HorizontalIntensityStatistics his = new HorizontalIntensityStatistics(sharpOut);
            Histogram histogram = his.Gray;
            int[] dato = histogram.Values;
            /////分割图像
            charCut(dato);
            shunfengdecoder decode = new shunfengdecoder();
           string backcode=decode.Decoder(sharpOut, div);
           return backcode;
        }

         /// <summary>
         /// 用垂直投影法分割字符
         /// </summary>
         /// <param name="anayData"></param>
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
            int limitMax = System.Math.Max(anayData[1], anayData[2]);
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
           
            if (partNum < 4)
            {
                proDetail(div, anayData);
               
                
            }
        }
        /// <summary>
        /// 处理连接字符
        /// </summary>
        /// <param name="div"></param>
        /// <param name="anayData"></param>
        private void proDetail(partDiv[] div, int[] anayData)
        {
            int maxpos;
            for (int i = 0; i < 4; i++)
            {
                if (div[i].length > 15)
                {
                    for (int k = 3; k > i + 1; k--)
                        div[k] = div[k - 1];

                    maxpos = findMax(anayData, div[i].begin, div[i].end);
                    if (anayData[maxpos] < anayData[div[i].begin + div[i].length / 2])
                    {
                        div[i + 1].end = div[i].end;
                        div[i].length = div[i].length / 2;
                        div[i].end = div[i].length + div[i].begin - 1;
                        div[i + 1].begin = div[i].end;
                        div[i + 1].length = div[i + 1].end - div[i + 1].begin + 1;

                    }
                    else
                    {
                        div[i + 1].end = div[i].end;
                        div[i].length = maxpos - div[i].begin + 1;
                        div[i].end = div[i].length + div[i].begin - 1;
                        div[i + 1].begin = div[i].end;
                        div[i + 1].length = div[i + 1].end - div[i + 1].begin + 1;
                    }
                    break;
                }
            }
        }

        private int findMax(int[] anayData, int begin, int end)
        {
            int pos = begin + 2;
            for (int i = begin + 3; i <= end - 2; i++)
            {
                if (anayData[pos] < anayData[i])
                    pos = i;
            }
            return pos;
        }

    }
}
