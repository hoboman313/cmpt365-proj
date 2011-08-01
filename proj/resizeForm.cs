//Form responsible for the resizing of the given image

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
    public partial class resizeForm : Form
    {
        public Bitmap img;
        decimal width=0, height=0;
        bool ignoreValueChanged = true;

        public resizeForm(Bitmap bmp)
        {
            img = bmp;
            width = img.Width;
            height = img.Height;
            InitializeComponent();
            currentSizeLab.Text = "Current size: " + img.Width + "x" + img.Height;
            widthNumeric.Value = img.Width;
            heightNumeric.Value = img.Height;
            ignoreValueChanged = false;
            heightNumeric.Minimum = 1;
            widthNumeric.Minimum = 1;

        }

        private void okayBut_Click(object sender, EventArgs e)
        {
            width = widthNumeric.Value;
            height = heightNumeric.Value;
            img= new Bitmap(img, Convert.ToInt32(width), Convert.ToInt32(height));

            this.Close();
        }

        private void cancelBut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void widthNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (ratioCheckBox.Checked && !ignoreValueChanged)
            {
                ignoreValueChanged = true;
                decimal ratio = width / Convert.ToDecimal(widthNumeric.Value);
                if (Math.Round(heightNumeric.Value / ratio) > heightNumeric.Minimum)
                {
                    heightNumeric.Value = Math.Round(heightNumeric.Value / ratio);
                    height = Convert.ToDecimal(heightNumeric.Value);
                    width = Convert.ToDecimal(widthNumeric.Value);
                }
            }
            else
                ignoreValueChanged = false;
        }

        private void heightNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (ratioCheckBox.Checked && !ignoreValueChanged)
            {
                ignoreValueChanged = true;
                decimal ratio = height / Convert.ToDecimal(heightNumeric.Value);

                if (Math.Round(widthNumeric.Value / ratio) > widthNumeric.Minimum)
                {
                    widthNumeric.Value = Math.Round(widthNumeric.Value / ratio);
                    height = Convert.ToDecimal(heightNumeric.Value);
                    width = Convert.ToDecimal(widthNumeric.Value);
                }
            }
            else
                ignoreValueChanged = false;
        }

        private void ratioCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            height = Convert.ToInt32(heightNumeric.Value);
            width = Convert.ToInt32(widthNumeric.Value);
        }
    }
}
