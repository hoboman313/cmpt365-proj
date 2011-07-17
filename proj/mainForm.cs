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
    public partial class mainForm : Form
    {
        Bitmap img = null;
        String imgName = null;

        public mainForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "JPEG Files|*.jpeg;*.jpg|All Files|*.*";
            file.Title = "Select a JPEG file";

            if (DialogResult.OK == file.ShowDialog())
            {
                imgName = file.FileName;
                img = new Bitmap(file.FileName);
                pictureBox.Image = img;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
