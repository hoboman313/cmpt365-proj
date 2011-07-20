using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Imaging;

namespace proj
{
    public partial class mainForm : Form
    {
        Bitmap img = null;
        String imgName = null;
        ArrayList imagesOnPath = new ArrayList();
        int viewedImageNum;
        string parentDirectory;
        List<string> imageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
        pixel[][] rawBytes;

        public struct pixel
        {
            public double r, g, b, y, u, v;

            public pixel(double r, double g, double b, double y, double u, double v)
            {
                this.r = r;
                this.g = g;
                this.b = b;
                this.y = y;
                this.u = u;
                this.v = v;
            }
        }

        public mainForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image Files|*.jpeg;*.jpg;*.png;*.gif|All Files|*.*";
            file.Title = "Select a JPEG file";

            if (DialogResult.OK == file.ShowDialog())
            {
                imagesOnPath.Clear();
                viewedImageNum = 0;
                imagesOnPath.Add(file.FileName);
                parentDirectory = Directory.GetParent(file.FileName).ToString();
                FileInfo[] files = Directory.GetParent(file.FileName).GetFiles();

                for (int i = 0; i < files.Length; i++)
                {
                    if (imageExtensions.Contains(Path.GetExtension(files[i].ToString().ToUpper())) && file.FileName != parentDirectory + files[i].ToString())
                    {
                        imagesOnPath.Add(parentDirectory+files[i].ToString());
                    }
                }
                imageToolStripMenuItem.Enabled = true;
                openImage(file.FileName);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            if (imagesOnPath.Count != 0)
            {
                viewedImageNum--;

                if (viewedImageNum < 0)
                {
                    viewedImageNum = imagesOnPath.Count - 1;
                }

                openImage(imagesOnPath[viewedImageNum].ToString());
            }
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            if (imagesOnPath.Count != 0)
            {
                viewedImageNum++;
                viewedImageNum %= imagesOnPath.Count;

                openImage(imagesOnPath[viewedImageNum].ToString());
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img.Save(imgName);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.RestoreDirectory = true;

            if (DialogResult.OK == sfd.ShowDialog())
            {
                img.Save(sfd.FileName);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(sender, e);
        }

        private void openImage(string imagePath)
        {
            imgName = imagePath;
            img = new Bitmap(imagePath);
            pictureBox.Image = img;

            mainForm.ActiveForm.Width = img.Width + 40;
            mainForm.ActiveForm.Height = img.Height + 86;

            toolStripTextBox.Text = (viewedImageNum + 1).ToString() + "/" + imagesOnPath.Count;
            //rawBytes = GetRawBytes(img);
        }


        //get the raw bytes of an image and store them into the rawBytes array
        //rawByte is a pixel struct, which holds the value of r, g, b, y, u, v values at each x,y of an image
        //WARNING: we probably cannot use this because we have to support all image types and this only seems to work with jpg
        private pixel[][] GetRawBytes(Bitmap bmp)
        {
            //only works with 24 bits per pixel image types
            if (bmp.PixelFormat != PixelFormat.Format24bppRgb)
                throw new InvalidOperationException("Image format not supported.");

            pixel[][] rawBytes = new pixel[bmp.Width][];
            for (int i = 0; i < rawBytes.Length; i++)
                rawBytes[i] = new pixel[bmp.Height];

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect,
                    System.Drawing.Imaging.ImageLockMode.ReadOnly,
                    bmp.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            int bytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr,
                           rgbValues, 0, bytes);

            byte red = 0;
            byte green = 0;
            byte blue = 0;

            //traverse through image
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    int position = (y * bmpData.Stride) + (x * 3);
                    blue = rgbValues[position];
                    green = rgbValues[position + 1];
                    red = rgbValues[position + 2];

                    //actual convert of rgb to yuv here
                    rawBytes[x][y] = new pixel(red, green, blue, red * 0.299000 + green * 0.587000 + blue * 0.114000, red * -0.168736 + green * -0.331264 + blue * 0.500000 + 128, red * 0.500000 + green * -.418688 + blue * -0.081312 + 128);
                }
            }

            bmp.UnlockBits(bmpData);

            return rawBytes;
        }

        private void roateLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img.RotateFlip(RotateFlipType.Rotate90FlipXY);
            pictureBox.Image = img;
        }

        private void rotateRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox.Image = img;
        }

        private void flipHorizonatallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img.RotateFlip(RotateFlipType.RotateNoneFlipX);
            pictureBox.Image = img;
        }

        private void horizontalFlipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img.RotateFlip(RotateFlipType.RotateNoneFlipY);
            pictureBox.Image = img;
        }


        private void freeRotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            freeRotateForm rotateForm = new freeRotateForm(img);
            rotateForm.ShowDialog();

            img = rotateForm.img;
            pictureBox.Image = img;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            deleteToolStripMenuItem_Click(sender, e);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgName != null)
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete " + imgName + "?", "Delete File?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    pictureBox.Image = null;
                    img.Dispose();
                    imagesOnPath.Clear();
                    File.Delete(imgName);
                    toolStripTextBox.Text = "";
                    imageToolStripMenuItem.Enabled = false;
                }
            }
        }

        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                leftButton_Click(sender, e);
            }

            if (e.KeyCode == Keys.Right)
            {
                rightButton_Click(sender, e);
            }
            
        }
    }
}
