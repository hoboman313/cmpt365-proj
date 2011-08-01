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


    public class CTImage
    {
        private byte[] data;
        private int width;
        private int height;

        //needed to parse the data array 
        private int stride;
        private const int components = 3; //only red, green and blue 
        private const int startDataOffset = 54;

        public CTImage(ref Bitmap bitmap)
        {
            width = bitmap.Width;
            height = bitmap.Height;
            data = CTImage.BitmapDataFromBitmap(ref bitmap);
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
