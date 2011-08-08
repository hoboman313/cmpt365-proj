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
using Accord.Imaging;
using Accord.Imaging.Filters;
using Accord.Math;

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
 
        //constants
        List<string> imageExtensions = new List<string> { ".JPG", ".JPEG", ".BMP", ".GIF", ".PNG" };
        const double zoomRatio = 0.1;
        const int widthPad = 40, heightPad = 86;
        const string progName="Photo Viewer";

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
            file.Title = "Select an Image File";

            if (DialogResult.OK == file.ShowDialog())
            {
                imagesOnPath.Clear();
                viewedImageNum = 0;
                imagesOnPath.Add(file.FileName);
                parentDirectory = Directory.GetParent(file.FileName).ToString();
                FileInfo[] files = Directory.GetParent(file.FileName).GetFiles();
                string slash="";

                if (!parentDirectory.EndsWith("\\"))
                    slash = "\\";

                for (int i = 0; i < files.Length; i++)
                {
                    if (imageExtensions.Contains(Path.GetExtension(files[i].ToString().ToUpper())) && file.FileName != parentDirectory + files[i].ToString())
                    {
                        imagesOnPath.Add(parentDirectory+slash+files[i].ToString());
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
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoomNormalizeImage();
            Bitmap tmp = new Bitmap(img);
            img.Dispose();
            origImg.Dispose();
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
            sfd.Filter = "Image Files|*.jpeg;*.jpg;*.png;*.gif;*.bmp|All Files|*.*";

            if (DialogResult.OK == sfd.ShowDialog())
            {
                zoomNormalizeImage();
                ImageFormat format;

                switch (Path.GetExtension(sfd.FileName).ToLower())
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
                        format = ImageFormat.Jpeg;
                        break;
                }

                img.Save(sfd.FileName, format);
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
            mainForm.ActiveForm.Text = imgName + " - "+progName;

            toolStripTextBox.Text = (viewedImageNum + 1).ToString() + "/" + imagesOnPath.Count;
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

            img.Dispose();
            img = rotateForm.img;
            origImg.Dispose();
            origImg = new Bitmap(img);

            pictureBox.Image = img;
            mainForm.ActiveForm.Width = img.Width + widthPad;
            mainForm.ActiveForm.Height = img.Height + heightPad;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            deleteToolStripMenuItem_Click(sender, e);
        }

        //delete current image
        //BUG...crash on delete
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
                    mainForm.ActiveForm.Text = progName;
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
            origImg.Dispose();
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
        //may be inefficient with large images
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
                scaleRight.Height = 0;
                scaleRight.Width = 0;
                scaleLeft.Height = 0;
                scaleLeft.Width = 0;
                scaleTop.Height = 0;
                scaleTop.Width = 0;
                scaleBot.Height = 0;
                scaleBot.Width = 0;

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
            //first make sure that the user selected a rectangle
            if (re.Height!=0 && re.Width!=0 )
            {
                origImg.Dispose();
                origImg = img.Clone(re, img.PixelFormat);
                img.Dispose();
                img = new Bitmap(origImg);
                pictureBox.Image = img;
                re.Height = 0;
                re.Width = 0;
                scaleRight.Height = 0;
                scaleRight.Width = 0;
                scaleLeft.Height = 0;
                scaleLeft.Width = 0;
                scaleTop.Height = 0;
                scaleTop.Width = 0;
                scaleBot.Height = 0;
                scaleBot.Width = 0;
                pictureBox.Invalidate();
            }
            else
                MessageBox.Show("You must first make a selection to use this feature.", "Warning");
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

                //make the rectangular selection zoom too
                re.X = Convert.ToInt32(re.X * (1.0 + zoomRatio));
                re.Y = Convert.ToInt32(re.Y * (1.0 + zoomRatio));
                re.Height = Convert.ToInt32(re.Height * (1.0 + zoomRatio));
                re.Width = Convert.ToInt32(re.Width * (1.0 + zoomRatio));

                doZoom();
            }
        }

        //zoom out on the image
        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            if (imagesOnPath.Count != 0)
            {
                zoomLevel--;

                //make the rectangular selection zoom too
                re.X = Convert.ToInt32(re.X*(1.0 - zoomRatio));
                re.Y = Convert.ToInt32(re.Y * (1.0 - zoomRatio));
                re.Height = Convert.ToInt32(re.Height * (1.0 - zoomRatio));
                re.Width = Convert.ToInt32(re.Width * (1.0 - zoomRatio));

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

            zoomedHeight = origImg.Height;
            zoomedWidth = origImg.Width;

            if (zoomLevel < 0)
            {
                for (int i = 0; i < Math.Abs(zoomLevel); i++)
                {
                    zoomedWidth = Convert.ToInt32(Convert.ToDouble(zoomedWidth) * (1.0-zoomRatio));
                    zoomedHeight = Convert.ToInt32(Convert.ToDouble(zoomedHeight) * (1.0 - zoomRatio));
                }

                mainForm.ActiveForm.Text = imgName + " - " + progName + " (" + " Zoomed Out " + Math.Abs(zoomLevel) + "x: " + zoomedWidth + ", " + zoomedHeight + " )";
            }
            else if (zoomLevel > 0)
            {
                for (int i = 0; i < zoomLevel; i++)
                {
                    zoomedWidth = Convert.ToInt32(Convert.ToDouble(zoomedWidth) * (1.0 + zoomRatio));
                    zoomedHeight = Convert.ToInt32(Convert.ToDouble(zoomedHeight) * (1.0 + zoomRatio));
                }
                mainForm.ActiveForm.Text = imgName + " - " + progName + " (" + " Zoomed In " + zoomLevel + "x: " + zoomedWidth + ", " + zoomedHeight + " )";
            }
            else //zoomLevel == 0
            {
                mainForm.ActiveForm.Text = imgName + " - " + progName;
            }
            img.Dispose();
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
                origImg.Dispose();
                origImg = new Bitmap(img);
                pictureBox.Image = img;
                mainForm.ActiveForm.Text = imgName + " - " + progName;
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

        //make the image negative ( 255- R/G/B for each pixel )
        private void negativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BitmapHelper ctImage = new BitmapHelper(ref img);
            Pixel pixel;

            for (int i = 0; i < ctImage.getHeight(); i++)
            {
                for (int j = 0; j < ctImage.getWidth(); j++)
                {
                    pixel = ctImage.getPixel(i, j);
                    pixel.Red = 255 - pixel.Red;
                    pixel.Green = 255 - pixel.Green;
                    pixel.Blue = 255 - pixel.Blue;
                    ctImage.setPixel(i, j, pixel);
                }
            }

            img = ctImage.getBitmap();
            origImg.Dispose();
            origImg = new Bitmap(img);
            pictureBox.Image = img; 
        }

        //convert the image to grayscale
        private void convertToGrayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BitmapHelper ctImage = new BitmapHelper(ref img);
            Pixel pixel, tmp=new Pixel(0, 0, 0 );

            for (int i = 0; i < ctImage.getHeight(); i++)
            {
                for (int j = 0; j < ctImage.getWidth(); j++)
                {
                    pixel = ctImage.getPixel(i, j);
                    tmp.Blue=tmp.Green = tmp.Red = Convert.ToInt32(pixel.Red * 0.299 + pixel.Green * 0.587 + pixel.Blue * 0.114);

                    ctImage.setPixel(i, j, tmp);
                }
            }

            img = ctImage.getBitmap();
            origImg.Dispose();
            origImg = new Bitmap(img);
            pictureBox.Image = img; 
        }

        //pop up the form to adjust image colors
        private void adjustColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            adjustColorsForm adjustColorsForm = new adjustColorsForm(img);
            adjustColorsForm.ShowDialog();

            img = adjustColorsForm.img;
            origImg = new Bitmap(adjustColorsForm.img);

            pictureBox.Image = img;
        }

        //try to remove the red eyes from a selection of a picture
        //code logic from: http://stackoverflow.com/questions/133675/red-eye-reduction-algorithm
        private void redEyeReductionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BitmapHelper ctImage = new BitmapHelper(ref img);
            Pixel pixel;
            float redIntensity;

            //first make sure that the user selected a rectangle
            if (re.Height != 0 && re.Width != 0)
            {
                for (int i = re.Y; i < re.Bottom; i++)
                {
                    for (int j = re.X; j < re.Right; j++)
                    {
                        pixel = ctImage.getPixel(i, j);

                        //Value of red divided by average of blue and green:
                        redIntensity = ((float)pixel.Red / ((pixel.Green + pixel.Blue) / 2));

                        if (redIntensity > 2.0f)
                        {
                            // reduce red to the average of blue and green
                            ctImage.setPixel(i, j, new Pixel((pixel.Green + pixel.Blue) / 2, pixel.Green, pixel.Blue));
                        }
                    }
                }

                img = ctImage.getBitmap();
                origImg.Dispose();
                origImg = new Bitmap(img);
                pictureBox.Image = img; 

            }
            else
                MessageBox.Show("You must first make a selection to use this feature.", "Warning");
        }

        private void redEyeReductionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            redEyeReductionToolStripMenuItem_Click(sender, e);
        }

        //get basic information about the current image
        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imagesOnPath.Count != 0)
            {
                FileInfo info = new FileInfo(imgName);

                MessageBox.Show("Image Name: " + info.Name + "\n" + "\n" +
                                "Directory: " + info.DirectoryName + "\n" + "\n" +
                                "Resolution: " + img.HorizontalResolution + " x " + img.VerticalResolution + "\n" + "\n" +
                                "Dimensions : " + img.Width + " x " + img.Height + "\n" + "\n" +
                                "Pixel Format : " + img.PixelFormat.ToString().Replace("Format", "") + "\n" + "\n" +
                                "Size : " + info.Length / 1024 + "." + info.Length % 1024 + " KB ( " + info.Length + " bytes )" + "\n" + "\n" +
                                "Created : " + info.CreationTime + "\n" + "\n" +
                                "Last Modified : " + info.LastWriteTime + "\n" + "\n"
                                , "Image Information");
            }
        }

        private void infoButton_Click(object sender, EventArgs e)
        {
            informationToolStripMenuItem_Click(sender, e);
        }

        // PANORAMIC STICHING CODE HERE - edited, tested and working, but needs to be further revised
        private void panoramicStitchingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap img2 = img;
            openToolStripMenuItem_Click(sender, e);
            Bitmap img1 = img;

            AForge.IntPoint[] harrisPoints1;
            AForge.IntPoint[] harrisPoints2;

            AForge.IntPoint[] correlationPoints1;
            AForge.IntPoint[] correlationPoints2;

            MatrixH homography;

            // Step 1: Detect feature points using Harris Corners Detector
            HarrisCornersDetector harris = new HarrisCornersDetector(0.04f, 1000f);
            harrisPoints1 = harris.ProcessImage(img1).ToArray();
            harrisPoints2 = harris.ProcessImage(img2).ToArray();

            // Step 2: Match feature points using a correlation measure
            CorrelationMatching matcher = new CorrelationMatching(9);
            AForge.IntPoint[][] matches = matcher.Match(img1, img2, harrisPoints1, harrisPoints2);

            // Get the two sets of points
            correlationPoints1 = matches[0];
            correlationPoints2 = matches[1];

            // Step 3: Create the homography matrix using a robust estimator
            RansacHomographyEstimator ransac = new RansacHomographyEstimator(0.001, 0.99);
            homography = ransac.Estimate(correlationPoints1, correlationPoints2);

            // Plot RANSAC results against correlation results
            AForge.IntPoint[] inliers1 = correlationPoints1.Submatrix(ransac.Inliers);
            AForge.IntPoint[] inliers2 = correlationPoints2.Submatrix(ransac.Inliers);

            // Step 4: Project and blend the second image using the homography
            Blend blend = new Blend(homography, img1);
            
            //save the image properly and resize main form
            img = blend.Apply(img2);
            origImg.Dispose();
            origImg = new Bitmap(img);
            pictureBox.Image = img;
            mainForm.ActiveForm.Width = img.Width + widthPad;
            mainForm.ActiveForm.Height = img.Height + heightPad;
        }
    }
}
