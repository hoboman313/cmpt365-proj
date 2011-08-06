//Form reponsible for rotating an image at an angle of 0-360

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace proj
{
    public partial class freeRotateForm : Form
    {
        public Bitmap img, rotatedImg;
        BitmapHelper helperImg;

        public freeRotateForm(Bitmap bmp)
        {
            InitializeComponent();
            img=new Bitmap(bmp);
            origPictureBox.Image = img;
            newPictureBox.Image = img;
            helperImg = new BitmapHelper(ref img);
        }

        //rotate image
        private Bitmap rotateImage(Bitmap img, float angle, Color bgColor)
        {
            float height = (float)(Math.Sin((Math.PI / 180) * (angle % 90)) * img.Width + Math.Cos((Math.PI / 180) * (angle % 90)) * img.Height);
            float width= (float)(Math.Cos((Math.PI / 180) * (angle % 90)) * img.Width + Math.Sin((Math.PI / 180) * (angle % 90)) * img.Height);

            //at 90 degrees, height and width change places as the image is on its side now
            if (angle % 180 >= 90)
            {
                float tmp = width;
                width = height;
                height = tmp;
            }

            Bitmap rotated = new Bitmap(Convert.ToInt32(width),Convert.ToInt32(height), img.PixelFormat);
            Graphics g = Graphics.FromImage(rotated);


            g.FillRectangle(new SolidBrush(bgColor),0,0,width,height);
            g.TranslateTransform(width/2.0f, height / 2.0f);
            g.RotateTransform(angle);
            g.TranslateTransform(-width/2.0f, -height/2.0f);

            //lame bug that tries to change the bixel format to 32bppArg if I don't create a new image here
            g.DrawImage(img,(width - img.Width)/2.0f, (height - img.Height)/2.0f );

            return rotated;
        }

        private void okayBut_Click(object sender, EventArgs e)
        {
            img = rotatedImg;
            this.Close();
        }

        private void cancelBut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rotateBar_Scroll(object sender, EventArgs e)
        {
            rotateNumericUpDown.Value = rotateBar.Value;
            rotatedImg = rotateImage(img, rotateBar.Value, Color.Black);

            newPictureBox.Image = rotatedImg;
        }

        private void rotateNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            rotateBar.Value = Convert.ToInt32(rotateNumericUpDown.Value);
            rotatedImg = rotateImage(img, rotateBar.Value, Color.Black);

            newPictureBox.Image = rotatedImg;
        }

    }
}
