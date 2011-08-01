//Form reponsible for rotating an image at an angle of 0-360

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
    public partial class freeRotateForm : Form
    {
        public Bitmap img, rotatedImg;

        public freeRotateForm(Bitmap bmp)
        {
            InitializeComponent();
            img=bmp;
            origPictureBox.Image = img;
            newPictureBox.Image = img;
        }

        //code to rotate from: http://www.switchonthecode.com/tutorials/csharp-tutorial-image-editing-rotate
        private Bitmap rotateImage(Bitmap b, float angle)
        {
            //create a new empty bitmap to hold rotated image
            Bitmap returnBitmap = new Bitmap(b.Width, b.Height);
            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(returnBitmap);
            //move rotation point to center of image
            g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
            //rotate
            g.RotateTransform(angle);
            //move image back
            g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
            //draw passed in image onto graphics object
            g.DrawImage(b, new Point(0, 0));

            return returnBitmap;
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
            rotatedImg=rotateImage(img, rotateBar.Value);

            newPictureBox.Image = rotatedImg;
        }

        private void rotateNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            rotateBar.Value = Convert.ToInt32(rotateNumericUpDown.Value);
            rotatedImg=rotateImage(img, rotateBar.Value);

            newPictureBox.Image = rotatedImg;
        }

    }
}
