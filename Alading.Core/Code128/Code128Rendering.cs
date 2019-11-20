using System;
using System.Drawing;
using System.Diagnostics;

namespace Alading.Core.Code128
{
    /// <summary>
    /// Summary description for Code128Rendering.
    /// </summary>
    public static class Code128Rendering
    {

        #region Code patterns

        // in principle these rows should each have 6 elements
        // however, the last one -- STOP -- has 7. The cost of the
        // extra integers is trivial, and this lets the code flow
        // much more elegantly
        private static readonly int[,] cPatterns = 
                     {
                        {2,1,2,2,2,2,0,0},  // 0
                        {2,2,2,1,2,2,0,0},  // 1
                        {2,2,2,2,2,1,0,0},  // 2
                        {1,2,1,2,2,3,0,0},  // 3
                        {1,2,1,3,2,2,0,0},  // 4
                        {1,3,1,2,2,2,0,0},  // 5
                        {1,2,2,2,1,3,0,0},  // 6
                        {1,2,2,3,1,2,0,0},  // 7
                        {1,3,2,2,1,2,0,0},  // 8
                        {2,2,1,2,1,3,0,0},  // 9
                        {2,2,1,3,1,2,0,0},  // 10
                        {2,3,1,2,1,2,0,0},  // 11
                        {1,1,2,2,3,2,0,0},  // 12
                        {1,2,2,1,3,2,0,0},  // 13
                        {1,2,2,2,3,1,0,0},  // 14
                        {1,1,3,2,2,2,0,0},  // 15
                        {1,2,3,1,2,2,0,0},  // 16
                        {1,2,3,2,2,1,0,0},  // 17
                        {2,2,3,2,1,1,0,0},  // 18
                        {2,2,1,1,3,2,0,0},  // 19
                        {2,2,1,2,3,1,0,0},  // 20
                        {2,1,3,2,1,2,0,0},  // 21
                        {2,2,3,1,1,2,0,0},  // 22
                        {3,1,2,1,3,1,0,0},  // 23
                        {3,1,1,2,2,2,0,0},  // 24
                        {3,2,1,1,2,2,0,0},  // 25
                        {3,2,1,2,2,1,0,0},  // 26
                        {3,1,2,2,1,2,0,0},  // 27
                        {3,2,2,1,1,2,0,0},  // 28
                        {3,2,2,2,1,1,0,0},  // 29
                        {2,1,2,1,2,3,0,0},  // 30
                        {2,1,2,3,2,1,0,0},  // 31
                        {2,3,2,1,2,1,0,0},  // 32
                        {1,1,1,3,2,3,0,0},  // 33
                        {1,3,1,1,2,3,0,0},  // 34
                        {1,3,1,3,2,1,0,0},  // 35
                        {1,1,2,3,1,3,0,0},  // 36
                        {1,3,2,1,1,3,0,0},  // 37
                        {1,3,2,3,1,1,0,0},  // 38
                        {2,1,1,3,1,3,0,0},  // 39
                        {2,3,1,1,1,3,0,0},  // 40
                        {2,3,1,3,1,1,0,0},  // 41
                        {1,1,2,1,3,3,0,0},  // 42
                        {1,1,2,3,3,1,0,0},  // 43
                        {1,3,2,1,3,1,0,0},  // 44
                        {1,1,3,1,2,3,0,0},  // 45
                        {1,1,3,3,2,1,0,0},  // 46
                        {1,3,3,1,2,1,0,0},  // 47
                        {3,1,3,1,2,1,0,0},  // 48
                        {2,1,1,3,3,1,0,0},  // 49
                        {2,3,1,1,3,1,0,0},  // 50
                        {2,1,3,1,1,3,0,0},  // 51
                        {2,1,3,3,1,1,0,0},  // 52
                        {2,1,3,1,3,1,0,0},  // 53
                        {3,1,1,1,2,3,0,0},  // 54
                        {3,1,1,3,2,1,0,0},  // 55
                        {3,3,1,1,2,1,0,0},  // 56
                        {3,1,2,1,1,3,0,0},  // 57
                        {3,1,2,3,1,1,0,0},  // 58
                        {3,3,2,1,1,1,0,0},  // 59
                        {3,1,4,1,1,1,0,0},  // 60
                        {2,2,1,4,1,1,0,0},  // 61
                        {4,3,1,1,1,1,0,0},  // 62
                        {1,1,1,2,2,4,0,0},  // 63
                        {1,1,1,4,2,2,0,0},  // 64
                        {1,2,1,1,2,4,0,0},  // 65
                        {1,2,1,4,2,1,0,0},  // 66
                        {1,4,1,1,2,2,0,0},  // 67
                        {1,4,1,2,2,1,0,0},  // 68
                        {1,1,2,2,1,4,0,0},  // 69
                        {1,1,2,4,1,2,0,0},  // 70
                        {1,2,2,1,1,4,0,0},  // 71
                        {1,2,2,4,1,1,0,0},  // 72
                        {1,4,2,1,1,2,0,0},  // 73
                        {1,4,2,2,1,1,0,0},  // 74
                        {2,4,1,2,1,1,0,0},  // 75
                        {2,2,1,1,1,4,0,0},  // 76
                        {4,1,3,1,1,1,0,0},  // 77
                        {2,4,1,1,1,2,0,0},  // 78
                        {1,3,4,1,1,1,0,0},  // 79
                        {1,1,1,2,4,2,0,0},  // 80
                        {1,2,1,1,4,2,0,0},  // 81
                        {1,2,1,2,4,1,0,0},  // 82
                        {1,1,4,2,1,2,0,0},  // 83
                        {1,2,4,1,1,2,0,0},  // 84
                        {1,2,4,2,1,1,0,0},  // 85
                        {4,1,1,2,1,2,0,0},  // 86
                        {4,2,1,1,1,2,0,0},  // 87
                        {4,2,1,2,1,1,0,0},  // 88
                        {2,1,2,1,4,1,0,0},  // 89
                        {2,1,4,1,2,1,0,0},  // 90
                        {4,1,2,1,2,1,0,0},  // 91
                        {1,1,1,1,4,3,0,0},  // 92
                        {1,1,1,3,4,1,0,0},  // 93
                        {1,3,1,1,4,1,0,0},  // 94
                        {1,1,4,1,1,3,0,0},  // 95
                        {1,1,4,3,1,1,0,0},  // 96
                        {4,1,1,1,1,3,0,0},  // 97
                        {4,1,1,3,1,1,0,0},  // 98
                        {1,1,3,1,4,1,0,0},  // 99
                        {1,1,4,1,3,1,0,0},  // 100
                        {3,1,1,1,4,1,0,0},  // 101
                        {4,1,1,1,3,1,0,0},  // 102
                        {2,1,1,4,1,2,0,0},  // 103
                        {2,1,1,2,1,4,0,0},  // 104
                        {2,1,1,2,3,2,0,0},  // 105
                        {2,3,3,1,1,1,2,0}   // 106
                     };

        #endregion Code patterns

        private const int cQuietWidth = 1;

        /// <summary>
        /// Make an image of a Code128 barcode for a given string
        /// </summary>
        /// <param name="InputData">Message to be encoded</param>
        /// <param name="BarWeight">Base thickness for bar width (1 or 2 works well)</param>
        /// <param name="AddQuietZone">Add required horiz margins (use if output is tight)</param>
        /// <returns>An Image of the Code128 barcode representing the message</returns>
        public static Image MakeBarcodeImage(string InputData, int BarWeight, bool AddQuietZone)
        {
            if (string.IsNullOrEmpty(InputData) || InputData.Trim().Length < 2)
            {
                return null;
            }

            InputData = InputData.Trim().ToUpper();

            // get the Code128 codes to represent the message
            Code128Content content = new Code128Content(InputData);
            int[] codes = content.Codes;
            int codeHeight = 20;

            int width, height;
            width = ((codes.Length - 3) * 11 + 35) * BarWeight;
            height = Convert.ToInt32(System.Math.Ceiling(Convert.ToSingle(width) * .20F));

            if (AddQuietZone)
            {
                width += 2 * cQuietWidth * BarWeight;  // on both sides
            }

            // get surface to draw on
            Image myimg = new System.Drawing.Bitmap(width, height + codeHeight);
            using (Graphics gr = Graphics.FromImage(myimg))
            {

                // set to white so we don't have to fill the spaces with white
                gr.FillRectangle(System.Drawing.Brushes.White, 0, 0, width, height + codeHeight);

                // skip quiet zone
                int cursor = AddQuietZone ? cQuietWidth * BarWeight : 0;

                for (int codeidx = 0; codeidx < codes.Length; codeidx++)
                {
                    int code = codes[codeidx];

                    // take the bars two at a time: a black and a white
                    for (int bar = 0; bar < 8; bar += 2)
                    {
                        int barwidth = cPatterns[code, bar] * BarWeight;
                        int spcwidth = cPatterns[code, bar + 1] * BarWeight;

                        // if width is zero, don't try to draw it
                        if (barwidth > 0)
                        {
                            gr.FillRectangle(System.Drawing.Brushes.Black, cursor, 0, barwidth, height);
                        }

                        // note that we never need to draw the space, since we 
                        // initialized the graphics to all white

                        // advance cursor beyond this pair
                        cursor += (barwidth + spcwidth);
                    }
                }

                Font font = new Font(FontFamily.GenericSansSerif, 12);
                float inputWidth = gr.MeasureString(InputData, font).Width;
                float textStart = (width - inputWidth) / 2;
                gr.DrawString(InputData, font, Brushes.Black, new PointF(textStart, height));
            }

            return myimg;

        }

    }
}