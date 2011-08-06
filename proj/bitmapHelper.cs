//Normally images can be processed in C# using very slow SetPixel/GetPixel Method
//This class provides methods to speed things up using code found at: http://www.devsource.com/showblog/22/Image-Processing-in-C/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace proj
{
    public class Pixel
    {
        public int Blue { get; set; }
        public int Green { get; set; }
        public int Red { get; set; }

        public Pixel(int red, int green, int blue)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }
    }

    public class BitmapHelper
    {
        private byte[] data;
        private int width;
        private int height;

        //needed to parse the data array 
        private int stride;
        private const int components = 3; //only red, green and blue 
        private const int startDataOffset = 54;

        public BitmapHelper(ref Bitmap bitmap)
        {
            width = bitmap.Width;
            height = bitmap.Height;
            data = BitmapHelper.BitmapDataFromBitmap(ref bitmap);
            stride = calculateStride(ref bitmap);
        }

        public int getHeight() { return height; }
        public int getWidth() { return width; }

        public Pixel getPixel(int row, int column)
        {
            int i = row;
            int j = column;
            row = height - row - 1;
            int startIndex = row * stride + column * components;

            int red = data[startDataOffset + startIndex + 2];
            int green = data[startDataOffset + startIndex + 1];
            int blue = data[startDataOffset + startIndex];
            return new Pixel(red, green, blue);
        }

        public void setPixel(int row, int column, Pixel pixel)
        {
            row = height - row - 1;
            int startIndex = row * stride + column * components;

            data[startDataOffset + startIndex + 2] = (byte)pixel.Red;
            data[startDataOffset + startIndex + 1] = (byte)pixel.Green;
            data[startDataOffset + startIndex] = (byte)pixel.Blue;
        }

        public Bitmap getBitmap()
        {
            return BitmapFromBitmapData(ref data);
        }

        // Given a Color (RGB Struct) in range of 0-255
        // Return H,S,L in range of 0-1
        //code mostly taken from: http://www.geekymonkey.com/Programming/CSharp/RGB2HSL_HSL2RGB.htm
        public static void RGB2HSL(Pixel rgb, out double h, out double s, out double l)
        {
            double r = rgb.Red / 255.0;
            double g = rgb.Green / 255.0;
            double b = rgb.Blue / 255.0;
            double v, m, vm, r2, g2, b2;

            h = 0; // default to black
            s = 0;
            l = 0;
            v = Math.Max(r, g);
            v = Math.Max(v, b);
            m = Math.Min(r, g);
            m = Math.Min(m, b);
            l = (m + v) / 2.0;

            if (l <= 0.0)
                return;

            vm = v - m;
            s = vm;

            if (s > 0.0)
                s /= (l <= 0.5) ? (v + m) : (2.0 - v - m);
            else
                return;

            r2 = (v - r) / vm;
            g2 = (v - g) / vm;
            b2 = (v - b) / vm;

            if (r == v)
                h = (g == m ? 5.0 + b2 : 1.0 - g2);
            else if (g == v)
                h = (b == m ? 1.0 + r2 : 3.0 - b2);
            else
                h = (r == m ? 3.0 + g2 : 5.0 - r2);

            h /= 6.0;
        }

        // Given H,S,L in range of 0-1
        // Returns a Color (RGB struct) in range of 0-255
        //code mostly taken from: http://www.geekymonkey.com/Programming/CSharp/RGB2HSL_HSL2RGB.htm
        public static Pixel HSL2RGB(double h, double sl, double l)
        {
            double v;
            double r, g, b;

            r = l;   // default to gray
            g = l;
            b = l;
            v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);

            if (v > 0)
            {
                double m;
                double sv;
                int sextant;
                double fract, vsf, mid1, mid2;

                m = l + l - v;
                sv = (v - m) / v;
                h *= 6.0;
                sextant = (int)h;
                fract = h - sextant;
                vsf = v * sv * fract;
                mid1 = m + vsf;
                mid2 = v - vsf;

                switch (sextant)
                {
                    case 0:
                        r = v;
                        g = mid1;
                        b = m;
                        break;

                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;

                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;

                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;

                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;

                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }


            return new Pixel(Convert.ToInt32(r * 255.0f), Convert.ToInt32(g * 255.0f), Convert.ToInt32(b * 255.0f));
        }



        public static Bitmap BitmapFromBitmapData(ref byte[] BitmapData)
        {
            MemoryStream ms = new MemoryStream(BitmapData);
            return (new Bitmap(ms));
        }

        public static byte[] BitmapDataFromBitmap(ref Bitmap objBitmap)
        {
            MemoryStream ms = new MemoryStream();
            objBitmap.Save(ms, ImageFormat.Bmp);
            return (ms.GetBuffer());
        }

        private static int calculateStride(ref Bitmap bitmap)
        {
            int stride;

            int stridePrecursor = bitmap.Width * components;

            if (stridePrecursor % 4 == 0) stride = stridePrecursor;
            else stride = stridePrecursor + (4 - (bitmap.Width * components) % 4);

            return stride;
        }
    }
}
