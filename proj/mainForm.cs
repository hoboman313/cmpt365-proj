//Main form that implements most of our image viewer functionality and links to other forms

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
        //variables
        Bitmap img = null, origImg=null;
        String imgName = null;
        ArrayList imagesOnPath = new ArrayList();
        int viewedImageNum, zoomLevel=0, zoomedHeight, zoomedWidth;
        string parentDirectory;
        pixel[][] rawBytes;

        //constants
        List<string> imageExtensions = new List<string> { ".JPG", ".JPEG", ".BMP", ".GIF", ".PNG" };
        const double zoomRatio = 0.1;
        const int widthPad = 40, heightPad = 86;

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
            pictureBox.ContextMenuStrip = contextMenu;
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
                viewToolStripMenuItem.Enabled = true;
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

        private void nextFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightButton_Click(sender, e);
        }

        private void previousFileInDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leftButton_Click(sender, e);
        }

        //save the img bitmap file in its current state
        //DOES NOT WORK AFTER CROPING A FILE...FUCK
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoomNormalizeImage();
            Bitmap tmp = new Bitmap(img);
            img.Dispose();
            origImg.Dispose();
            pictureBox.Image = null;
            File.Delete(imgName);
            ImageFormat format;

            switch (Path.GetExtension(imgName).ToLower())
            {
                case ".jpg":
                    format = ImageFormat.Jpeg;
                    break;
                case ".jpeg":
                    format = ImageFormat.Jpeg;
                    break;
                case ".png":
                    format = ImageFormat.Png;
                    break;
                case ".gif":
                    format = ImageFormat.Gif;
                    break;
                case ".bmp":
                    format = ImageFormat.Bmp;
                    break;
                default:
                    format=ImageFormat.Jpeg;
                    break;
            }

            tmp.Save(imgName, format);
            img = new Bitmap(tmp);
            origImg = new Bitmap(tmp);
            pictureBox.Image = img;
        }

        //same as simply saving the file, but allow the user to provide the location to save to
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.RestoreDirectory = true;
            sfd.Filter = "Image File|*"+ Path.GetExtension(imgName);

            if (DialogResult.OK == sfd.ShowDialog())
            {
                zoomNormalizeImage();
                img.Save(sfd.FileName);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(sender, e);
        }

        //main function that is to be called when we want to open an image with a path "imagePath"
        private void openImage(string imagePath)
        {
            imgName = imagePath;
            img = new Bitmap(imagePath);
            origImg = new Bitmap(imagePath);
            pictureBox.Image = img;
            mainForm.ActiveForm.Width = img.Width + widthPad;
            mainForm.ActiveForm.Height = img.Height + heightPad;
            zoomLevel = 0;
            mainForm.ActiveForm.Text = imgName + " - Image Viewer";

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
            origImg = new Bitmap(rotateForm.img);

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
                    origImg.Dispose();
                    imagesOnPath.Clear();
                    File.Delete(imgName);
                    toolStripTextBox.Text = "";
                    imageToolStripMenuItem.Enabled = false;
                    viewToolStripMenuItem.Enabled = false;
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
            origImg = new Bitmap(resizeform.img);
            pictureBox.Image = img;
        }


        ////////////////////////////////////////
        //CROPPING AND RECTANGLE SELECTION BEGIN
        ////////////////////////////////////////
        Point start;
        bool makeSelection = false, moveSelection=false, scaleLeftSelection=false, scaleRightSelection=false, scaleTopSelection=false, scaleBotSelection=false, mbdown=false;
        Rectangle re, scaleRight, scaleLeft, scaleTop, scaleBot;
        const int scaleRectSize = 10;

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
            if (scaleRight.Contains(start))
                scaleRightSelection = true;
            else if (scaleLeft.Contains(start))
                scaleLeftSelection = true;
            else if (scaleTop.Contains(start))
                scaleTopSelection = true;
            else if (scaleBot.Contains(start))
                scaleBotSelection = true;
            else if (re.Contains(start))
                moveSelection = true;
            else
            {
                makeSelection = true;
                mainForm.ActiveForm.Cursor = Cursors.Cross;

                //clear previous rectangle incase it's a simple click
                re.Height = 0;
                re.Width = 0;
                pictureBox.Invalidate();
            }
        }

        //called when the mouse is moved
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {

            if (mainForm.ActiveForm != null)
            {
                if (!mbdown) //give the user a premature indicator that the selection can be moved
                {
                    if (scaleRight.Contains(e.X, e.Y) || scaleLeft.Contains(e.X, e.Y))
                    {
                        mainForm.ActiveForm.Cursor = Cursors.SizeWE;
                    }
                    else if (scaleTop.Contains(e.X, e.Y) || scaleBot.Contains(e.X, e.Y))
                    {
                        mainForm.ActiveForm.Cursor = Cursors.SizeNS;
                    }
                    else if (re.Contains(e.X, e.Y))
                        mainForm.ActiveForm.Cursor = Cursors.SizeAll;
                    else
                        mainForm.ActiveForm.Cursor = Cursors.Default;
                }
                else //the mouse button is pressed down, so lets do something
                {
                    if (scaleRightSelection)
                    {
                        re.Width += e.X - start.X;
                        start.X = e.X;
                        start.Y = e.Y;
                    }
                    else if (scaleLeftSelection)
                    {
                        re.X += e.X - start.X;
                        if (re.X >= 0)
                            re.Width += start.X - e.X;

                        start.X = e.X;
                        start.Y = e.Y;
                    }
                    else if (scaleTopSelection)
                    {
                        re.Y += e.Y - start.Y;
                        if (re.Y >= 1)
                            re.Height += start.Y - e.Y;

                        start.X = e.X;
                        start.Y = e.Y;
                    }
                    else if (scaleBotSelection)
                    {
                        re.Height += e.Y - start.Y;
                        start.X = e.X;
                        start.Y = e.Y;
                    }
                    else if (makeSelection)
                    {
                        re.X = Math.Min(start.X, e.X);
                        re.Y = Math.Min(start.Y, e.Y);

                        if (re.X >= 0 && re.Y >= 1)
                        {
                            re.Width = Math.Max(start.X, e.X) - re.X;
                            re.Height = Math.Max(start.Y, e.Y) - re.Y;
                        }
                    }
                    else if (moveSelection)
                    {
                        re.X += e.X - start.X;
                        re.Y += e.Y - start.Y;
                        start.X = e.X;
                        start.Y = e.Y;
                    }

                    //make sure that the selection rectangle doesn't go out of bounds
                    if (re.Right > img.Width)
                        re.Width = img.Width - re.X;
                    if (re.Bottom > img.Height)
                        re.Height = img.Height - re.Y;
                    if (re.X < 0)
                        re.X = 0;
                    //+1 to offset the padding on the top
                    if (re.Y < 1)
                        re.Y = 1;

                    //invisible rectangles created, so that we can show up the scale top/bot/right/left cursors
                    scaleRight.X = re.Right - scaleRectSize / 2;
                    scaleRight.Y = re.Y;
                    scaleRight.Height = re.Height;
                    scaleRight.Width = scaleRectSize;

                    scaleLeft.X = re.Left - scaleRectSize / 2;
                    scaleLeft.Y = re.Y;
                    scaleLeft.Height = re.Height;
                    scaleLeft.Width = scaleRectSize;

                    scaleTop.X = re.X;
                    scaleTop.Y = re.Top - scaleRectSize / 2;
                    scaleTop.Height = scaleRectSize;
                    scaleTop.Width = re.Width;

                    scaleBot.X = re.X;
                    scaleBot.Y = re.Bottom - scaleRectSize / 2;
                    scaleBot.Height = scaleRectSize;
                    scaleBot.Width = re.Width;

                    //redraw the entire picture box
                    pictureBox.Invalidate();
                }
            }
        }

        //called when we let go of the left mouse button
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            scaleRightSelection = false;
            scaleLeftSelection = false;
            scaleTopSelection = false;
            scaleBotSelection = false;
            makeSelection = false;
            moveSelection = false;
            mbdown = false;

            if( mainForm.ActiveForm != null )
                mainForm.ActiveForm.Cursor = Cursors.Default;
        }

        //crop the area selected by the rectangle selection
        private void cropToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (re.Height!=0 && re.Width!=0 )
            {
                img = img.Clone(re, img.PixelFormat);
                origImg = new Bitmap(img);
                pictureBox.Image = img;
                re.Height = 0;
                re.Width = 0;
                pictureBox.Invalidate();
            }
        }

        private void cropToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cropToolStripMenuItem_Click(sender, e);
        }
        ////////////////////////////////////////
        //CROPPING AND RECTANGLE SELECTION FINISH
        ////////////////////////////////////////

        //zoom in on the image
        private void zoomInButton_Click(object sender, EventArgs e)
        {
            if (imagesOnPath.Count != 0)
            {
                zoomLevel++;
                zoomedHeight = origImg.Height;
                zoomedWidth = origImg.Width;
                doZoom();
            }
        }

        //zoom out on the image
        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            if (imagesOnPath.Count != 0)
            {
                zoomLevel--;
                zoomedHeight = origImg.Height;
                zoomedWidth = origImg.Width;
                doZoom();
            }
        }


        //main function that implements the zooming logic
        private void doZoom()
        {
            //The logic behind doing it like this is that the resolution of an 
            //image changes when we make it smaller, thus the overall quality 
            //would decrease if we do a lot of zoom ins and then a bunch of zoom outs.
            //This way solves that problem and preserves the quality.
            if (zoomLevel < 0)
            {
                for (int i = 0; i < Math.Abs(zoomLevel); i++)
                {
                    zoomedWidth = Convert.ToInt32(Convert.ToDouble(zoomedWidth) * (1.0-zoomRatio));
                    zoomedHeight = Convert.ToInt32(Convert.ToDouble(zoomedHeight) * (1.0 - zoomRatio));
                }

                mainForm.ActiveForm.Text = imgName + " - Image Viewer (" + " Zoomed Out " + Math.Abs(zoomLevel) + "x: " + zoomedWidth + ", " + zoomedHeight + " )";
            }
            else if (zoomLevel > 0)
            {
                for (int i = 0; i < zoomLevel; i++)
                {
                    zoomedWidth = Convert.ToInt32(Convert.ToDouble(zoomedWidth) * (1.0 + zoomRatio));
                    zoomedHeight = Convert.ToInt32(Convert.ToDouble(zoomedHeight) * (1.0 + zoomRatio));
                }
                mainForm.ActiveForm.Text = imgName + " - Image Viewer (" + " Zoomed In " + zoomLevel + "x: " + zoomedWidth + ", " + zoomedHeight + " )";
            }
            else //zoomLevel == 0
            {
                mainForm.ActiveForm.Text = imgName + " - Image Viewer";
            }

            img = new Bitmap(origImg, zoomedWidth, zoomedHeight);
            pictureBox.Image = img;

            mainForm.ActiveForm.Width = img.Width + widthPad;
            mainForm.ActiveForm.Height = img.Height + heightPad;
        }

        //return image to its "original size". Not zoomed in and not zoomed out.
        private void zoomNormalizeImage()
        {
            if (zoomLevel != 0)
            {
                int width = img.Width, height = img.Height;
                //we have a zoomed in image and we gotta make it small again
                if (zoomLevel > 0)
                {
                    for (int i = zoomLevel; i != 0; i--)
                    {
                        width = Convert.ToInt32(Convert.ToDouble(width) * (1.0 - zoomRatio));
                        height = Convert.ToInt32(Convert.ToDouble(height) * (1.0 - zoomRatio));
                    }
                }
                else if (zoomLevel < 0)
                {
                    for (int i = zoomLevel; i != 0; i++)
                    {
                        width = Convert.ToInt32(Convert.ToDouble(width) * (1.0 + zoomRatio));
                        height = Convert.ToInt32(Convert.ToDouble(height) * (1.0 + zoomRatio));
                    }
                }

                img = new Bitmap(img, width, height);
                origImg = new Bitmap(img);
                pictureBox.Image = img;
                mainForm.ActiveForm.Text = imgName + " - Image Viewer";
                mainForm.ActiveForm.Width = img.Width + widthPad;
                mainForm.ActiveForm.Height = img.Height + heightPad;
                zoomLevel = 0;
            }
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoomInButton_Click(sender, e);
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoomOutButton_Click(sender, e);
        }

        //reopen the currently viewed image
        private void reopenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openImage(imagesOnPath[viewedImageNum].ToString());
        }

        //view the first file in the directory
        private void firstFileInDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewedImageNum = 0;
            openImage(imagesOnPath[viewedImageNum].ToString());
        }

        //view the last file in the directory
        private void lastFileInDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewedImageNum = imagesOnPath.Count-1;
            openImage(imagesOnPath[viewedImageNum].ToString());
        }
    }
}
