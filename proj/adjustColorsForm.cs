using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace proj
{
    public partial class adjustColorsForm : Form
    {
        public Bitmap img, adjustedImg;
        CTImage ctImage, origCImage;
        int[] gammaTable = new int[256];

        public adjustColorsForm(Bitmap bmp)
        {
            InitializeComponent();
            img = bmp;
            adjustedImg = img;
            origPictureBox.Image = img;
            newPictureBox.Image = adjustedImg;
            origCImage = new CTImage(ref img);
            ctImage = new CTImage(ref adjustedImg);

            //default gamma is 1.00
            for (int i = 0; i < 256; i++)
                gammaTable[i] = Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / 1.00)) + 0.5));
        }

        private void okayBut_Click(object sender, EventArgs e)
        {
            img = adjustedImg;
            this.Close();
        }

        private void cancelBut_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        //changing the value of the bar will change the value of the numeric updown controll

        private void brightnessBar_Scroll(object sender, EventArgs e)
        {
            brightnessUd.Value = brightnessBar.Value;
        }

        private void rBar_Scroll(object sender, EventArgs e)
        {
            rUd.Value = rBar.Value;
        }

        private void gBar_Scroll(object sender, EventArgs e)
        {
            gUd.Value = gBar.Value;
        }

        private void bBar_Scroll(object sender, EventArgs e)
        {
            bUd.Value = bBar.Value;
        }
    
        private void contrastBar_Scroll(object sender, EventArgs e)
        {
            contrastUd.Value = contrastBar.Value;
        }

        private void gammaBar_Scroll(object sender, EventArgs e)
        {
            gammaUd.Value = Convert.ToDecimal(gammaBar.Value) / 100.0m;
        }

        private void saturationBar_Scroll(object sender, EventArgs e)
        {
            saturationUd.Value = saturationBar.Value;
        }




        //changing the value of the numeric updown controll will change the position of the bar
        //the actual manipulation of the image is done here too

        private void brightnessUd_ValueChanged(object sender, EventArgs e)
        {
            brightnessBar.Value = Convert.ToInt32(brightnessUd.Value);

            adjustColors();
        }

        private void rUd_ValueChanged(object sender, EventArgs e)
        {
            rBar.Value = Convert.ToInt32(rUd.Value);

            adjustColors();
        }

        private void gUd_ValueChanged(object sender, EventArgs e)
        {
            gBar.Value = Convert.ToInt32(gUd.Value);

            adjustColors();
        }

        private void bUd_ValueChanged(object sender, EventArgs e)
        {
            bBar.Value = Convert.ToInt32(bUd.Value);

            adjustColors();
        }

        private void contrastUd_ValueChanged(object sender, EventArgs e)
        {
            contrastBar.Value = Convert.ToInt32(contrastUd.Value);

            adjustColors();
        }

        private void gammaUd_ValueChanged(object sender, EventArgs e)
        {
            gammaBar.Value = Convert.ToInt32(gammaUd.Value*100);

            for (int i = 0; i < 256; i++)
                gammaTable[i] = Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / Convert.ToDouble(gammaUd.Value))) + 0.5));
    
            adjustColors();
        }

        private void saturationUd_ValueChanged(object sender, EventArgs e)
        {
            saturationBar.Value = Convert.ToInt32(saturationUd.Value);

            adjustColors();
        }

        //perform all color adjustments at once on the new image with pixel values from the old images
        private void adjustColors()
        {
            Pixel pixel;
            double contrastNum;

            for (int i = 0; i < origCImage.getHeight(); i++)
            {
                for (int j = 0; j < origCImage.getWidth(); j++)
                {
                    pixel = origCImage.getPixel(i, j);

                    //gamma
                    pixel.Red = gammaTable[pixel.Red];
                    pixel.Green = gammaTable[pixel.Green];
                    pixel.Blue = gammaTable[pixel.Blue];

                    //brightness adjustment
                    pixel.Red += Convert.ToInt32(brightnessUd.Value);
                    pixel.Green += Convert.ToInt32(brightnessUd.Value);
                    pixel.Blue += Convert.ToInt32(brightnessUd.Value);

                    //color adjustment
                    pixel.Red += Convert.ToInt32(rUd.Value);
                    pixel.Green += Convert.ToInt32(gUd.Value);
                    pixel.Blue += Convert.ToInt32(bUd.Value);

                    //contrast
                    contrastNum = (Convert.ToDouble(contrastUd.Value) + 127.0) / 127.0;
                    contrastNum *= contrastNum;
                    pixel.Red = Convert.ToInt32( ((Convert.ToDouble(pixel.Red) / 255.0 - 0.5) * contrastNum + 0.5 ) * 255.0);
                    pixel.Green = Convert.ToInt32(((Convert.ToDouble(pixel.Green) / 255.0 - 0.5) * contrastNum + 0.5) * 255.0);
                    pixel.Blue = Convert.ToInt32(((Convert.ToDouble(pixel.Blue) / 255.0 - 0.5) * contrastNum + 0.5) * 255.0);

                    if (pixel.Red > 255)
                        pixel.Red = 255;
                    else if (pixel.Red < 0)
                        pixel.Red = 0;

                    if (pixel.Green > 255)
                        pixel.Green = 255;
                    else if (pixel.Green < 0)
                        pixel.Green = 0;

                    if (pixel.Blue > 255)
                        pixel.Blue = 255;
                    else if (pixel.Blue < 0)
                        pixel.Blue = 0;

                    double h, s, l;
                    CTImage.RGB2HSL(pixel, out h, out s, out l);

                    s = s + Convert.ToDouble(saturationUd.Value) / 255.0;

                    if (s > 1)
                        s = 1;
                    else if (s < 0)
                        s = 0;

                    pixel = CTImage.HSL2RGB(h, s, l);

                    ctImage.setPixel(i, j, pixel);
                }
            }

            adjustedImg = ctImage.getBitmap();
            newPictureBox.Image = adjustedImg;
        }

        //restore all default values
        private void restoreDefaultsBut_Click(object sender, EventArgs e)
        {
            brightnessUd.Value = 0;
            rUd.Value = 0;
            gUd.Value = 0;
            bUd.Value = 0;
            gammaUd.Value = 1.00m;
            saturationUd.Value = 0;
            contrastUd.Value = 0;
        }

        //make the "old image" image equal to the current "new image"
        private void applyOldBut_Click(object sender, EventArgs e)
        {
            origPictureBox.Image = adjustedImg;
        }

    }
}
