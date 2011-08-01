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
        CTImage origCtImage, adjustedCtImage;

        public adjustColorsForm(Bitmap bmp)
        {
            InitializeComponent();
            img = bmp;
            adjustedImg = img;
            origPictureBox.Image = img;
            newPictureBox.Image = adjustedImg;
            origCtImage = new CTImage(ref img);
            adjustedCtImage = new CTImage(ref adjustedImg);
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

            Pixel pixel;

            for (int i = 0; i < origCtImage.getHeight(); i++)
            {
                for (int j = 0; j < origCtImage.getWidth(); j++)
                {
                    pixel = origCtImage.getPixel(i, j);
                    pixel.Red += Convert.ToInt32(brightnessUd.Value);
                    pixel.Green += Convert.ToInt32(brightnessUd.Value);
                    pixel.Blue += Convert.ToInt32(brightnessUd.Value);

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

                    adjustedCtImage.setPixel(i, j, pixel);
                }
            }

            adjustedImg = adjustedCtImage.getBitmap();
            newPictureBox.Image= adjustedImg;
        }

        private void rUd_ValueChanged(object sender, EventArgs e)
        {
            rBar.Value = Convert.ToInt32(rUd.Value);

            Pixel pixel;

            for (int i = 0; i < origCtImage.getHeight(); i++)
            {
                for (int j = 0; j < origCtImage.getWidth(); j++)
                {
                    pixel = origCtImage.getPixel(i, j);
                    pixel.Red += Convert.ToInt32(rUd.Value);

                    if (pixel.Red > 255)
                        pixel.Red = 255;
                    else if (pixel.Red < 0)
                        pixel.Red = 0;

                    adjustedCtImage.setPixel(i, j, pixel);
                }
            }

            adjustedImg = adjustedCtImage.getBitmap();
            newPictureBox.Image= adjustedImg;
        }

        private void gUd_ValueChanged(object sender, EventArgs e)
        {
            gBar.Value = Convert.ToInt32(gUd.Value);

            Pixel pixel;

            for (int i = 0; i < origCtImage.getHeight(); i++)
            {
                for (int j = 0; j < origCtImage.getWidth(); j++)
                {
                    pixel = origCtImage.getPixel(i, j);
                    pixel.Green += Convert.ToInt32(gUd.Value);

                    if (pixel.Green > 255)
                        pixel.Green = 255;
                    else if (pixel.Green < 0)
                        pixel.Green = 0;

                    adjustedCtImage.setPixel(i, j, pixel);
                }
            }

            adjustedImg = adjustedCtImage.getBitmap();
            newPictureBox.Image = adjustedImg;
        }

        private void bUd_ValueChanged(object sender, EventArgs e)
        {
            bBar.Value = Convert.ToInt32(bUd.Value);

            Pixel pixel;

            for (int i = 0; i < origCtImage.getHeight(); i++)
            {
                for (int j = 0; j < origCtImage.getWidth(); j++)
                {
                    pixel = origCtImage.getPixel(i, j);
                    pixel.Blue += Convert.ToInt32(bUd.Value);

                    if (pixel.Blue > 255)
                        pixel.Blue = 255;
                    else if (pixel.Blue < 0)
                        pixel.Blue = 0;

                    adjustedCtImage.setPixel(i, j, pixel);
                }
            }

            adjustedImg = adjustedCtImage.getBitmap();
            newPictureBox.Image = adjustedImg;
        }

        private void contrastUd_ValueChanged(object sender, EventArgs e)
        {
            contrastBar.Value = Convert.ToInt32(contrastUd.Value);
        }

        private void gammaUd_ValueChanged(object sender, EventArgs e)
        {
            gammaBar.Value = Convert.ToInt32(gammaUd.Value*100);
        }

        private void saturationUd_ValueChanged(object sender, EventArgs e)
        {
            saturationBar.Value = Convert.ToInt32(saturationUd.Value);
        }

    }
}
