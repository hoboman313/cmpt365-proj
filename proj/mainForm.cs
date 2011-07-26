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
        List<string> imageExtensions = new List<string> { ".JPG", ".JPEG", ".BMP", ".GIF", ".PNG" };
        pixel[][] rawBytes;

        //define a pixel[x, y]
        //currently not used
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

        //handle the opening of any new file
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            //should match imageExtensions
            file.Filter = "Image Files|*.jpeg;*.jpg;*.png;*.gif;*.bmp|All Files|*.*";
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

        //exit application button handle
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        //handle the left button click event, which will traverse through all the images in the directory of a file that was opened
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

        //handle the right button click event, which will traverse through all the images in the directory of a file that was opened
        private void rightButton_Click(object sender, EventArgs e)
        {
            if (imagesOnPath.Count != 0)
            {
                viewedImageNum++;
                viewedImageNum %= imagesOnPath.Count;

                openImage(imagesOnPath[viewedImageNum].ToString());
            }
        }

        //save the img bitmap file in its current state
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img.Save(imgName);
        }

        //same as simply saving the file, but allow the user to provide the location to save to
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

        //rotate image to the left
        private void rotateLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img.RotateFlip(RotateFlipType.Rotate90FlipXY);
            pictureBox.Image = img;
        }

        //rotate image to the right
        private void rotateRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox.Image = img;
        }

        //flip image vertically
        private void flipVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img.RotateFlip(RotateFlipType.RotateNoneFlipX);
            pictureBox.Image = img;
        }

        //flip image horizontally
        private void horizontalFlipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img.RotateFlip(RotateFlipType.RotateNoneFlipY);
            pictureBox.Image = img;
        }

        //call the free rotate form, which will let the user rotate an image at any angle and pad with the black color
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

        //delete current image
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

        //shortcut keys to allow the user traverse through images
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

        //call the resize form, which will let the user resize the image
        private void resizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resizeForm resizeform = new resizeForm(img);
            resizeform.ShowDialog();

            img = resizeform.img;
            pictureBox.Image = img;
        }

        ////////////////////////////////////////
        //CROPPING AND RECTANGLE SELECTION BEGIN
        ////////////////////////////////////////
        Point start;
        bool makeSelection = false, moveSelection=false, mbdown=false;
        Rectangle re;

        //gets called everytime we invalidate the image, meaing the whole image has to be redrawn completely
        //may be inefficient with large images, but w/e
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, re, Color.Black, ButtonBorderStyle.Dashed);
        }

        //called when we click the mouse down
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            start.X = e.X;
            start.Y = e.Y;
            mbdown = true;

            //have to tell whether we are creating a new selection area or moving it
            if (re.Contains(start))
                moveSelection = true;
            else
            {
                makeSelection = true;
                mainForm.ActiveForm.Cursor = Cursors.Cross;
            }
        }

        //called when the mouse is moved
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mbdown) //give the user a premature indicator that the selection can be moved
            {
                if (re.Contains(e.X, e.Y))
                    mainForm.ActiveForm.Cursor = Cursors.SizeAll;
                else
                    mainForm.ActiveForm.Cursor = Cursors.Default;
            }
            else //the mouse button is pressed down, so let's do something
            {
                if (makeSelection)
                {
                    re.X = Math.Min(start.X, e.X);
                    re.Y = Math.Min(start.Y, e.Y);
                    re.Width = Math.Max(start.X, e.Y) - re.X;
                    re.Height = Math.Max(start.Y, e.Y) - re.Y;
                }
                else
                {
                    re.X += e.X - start.X;
                    re.Y += e.Y - start.Y;
                    start.X = e.X;
                    start.Y = e.Y;  
                }

                pictureBox.Invalidate();
            }
        }

        //called when we let go of the left mouse button
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            makeSelection = false;
            moveSelection = false;
            mbdown = false;
            mainForm.ActiveForm.Cursor = Cursors.Default;
        }
        ////////////////////////////////////////
        //CROPPING AND RECTANGLE SELECTION FINISH
        ////////////////////////////////////////
    }
}
