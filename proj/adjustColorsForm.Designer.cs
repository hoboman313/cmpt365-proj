namespace proj
{
    partial class adjustColorsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.newPictureBox = new System.Windows.Forms.PictureBox();
            this.origPictureBox = new System.Windows.Forms.PictureBox();
            this.brightnessBar = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.brightnessUd = new System.Windows.Forms.NumericUpDown();
            this.cancelBut = new System.Windows.Forms.Button();
            this.okayBut = new System.Windows.Forms.Button();
            this.rUd = new System.Windows.Forms.NumericUpDown();
            this.rBar = new System.Windows.Forms.TrackBar();
            this.gUd = new System.Windows.Forms.NumericUpDown();
            this.gBar = new System.Windows.Forms.TrackBar();
            this.bUd = new System.Windows.Forms.NumericUpDown();
            this.bBar = new System.Windows.Forms.TrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.contrastUd = new System.Windows.Forms.NumericUpDown();
            this.contrastBar = new System.Windows.Forms.TrackBar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.gammaUd = new System.Windows.Forms.NumericUpDown();
            this.gammaBar = new System.Windows.Forms.TrackBar();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.saturationUd = new System.Windows.Forms.NumericUpDown();
            this.saturationBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.newPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.origPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessBar)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessUd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rUd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gUd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bUd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bBar)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contrastUd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastBar)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gammaUd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gammaBar)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.saturationUd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saturationBar)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(307, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "New Image";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Old Image";
            // 
            // newPictureBox
            // 
            this.newPictureBox.Location = new System.Drawing.Point(270, 21);
            this.newPictureBox.Name = "newPictureBox";
            this.newPictureBox.Size = new System.Drawing.Size(140, 135);
            this.newPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.newPictureBox.TabIndex = 11;
            this.newPictureBox.TabStop = false;
            // 
            // origPictureBox
            // 
            this.origPictureBox.Location = new System.Drawing.Point(14, 22);
            this.origPictureBox.Name = "origPictureBox";
            this.origPictureBox.Size = new System.Drawing.Size(146, 132);
            this.origPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.origPictureBox.TabIndex = 10;
            this.origPictureBox.TabStop = false;
            // 
            // brightnessBar
            // 
            this.brightnessBar.Location = new System.Drawing.Point(13, 14);
            this.brightnessBar.Margin = new System.Windows.Forms.Padding(0);
            this.brightnessBar.Maximum = 255;
            this.brightnessBar.Minimum = -255;
            this.brightnessBar.Name = "brightnessBar";
            this.brightnessBar.Size = new System.Drawing.Size(137, 45);
            this.brightnessBar.TabIndex = 14;
            this.brightnessBar.Scroll += new System.EventHandler(this.brightnessBar_Scroll);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.brightnessUd);
            this.groupBox1.Controls.Add(this.brightnessBar);
            this.groupBox1.Location = new System.Drawing.Point(8, 167);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(202, 61);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Brightness";
            // 
            // brightnessUd
            // 
            this.brightnessUd.Location = new System.Drawing.Point(155, 24);
            this.brightnessUd.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.brightnessUd.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.brightnessUd.Name = "brightnessUd";
            this.brightnessUd.Size = new System.Drawing.Size(45, 20);
            this.brightnessUd.TabIndex = 15;
            this.brightnessUd.ValueChanged += new System.EventHandler(this.brightnessUd_ValueChanged);
            // 
            // cancelBut
            // 
            this.cancelBut.Location = new System.Drawing.Point(267, 436);
            this.cancelBut.Name = "cancelBut";
            this.cancelBut.Size = new System.Drawing.Size(94, 34);
            this.cancelBut.TabIndex = 17;
            this.cancelBut.Text = "Cancel";
            this.cancelBut.UseVisualStyleBackColor = true;
            this.cancelBut.Click += new System.EventHandler(this.cancelBut_Click);
            // 
            // okayBut
            // 
            this.okayBut.Location = new System.Drawing.Point(116, 437);
            this.okayBut.Name = "okayBut";
            this.okayBut.Size = new System.Drawing.Size(94, 34);
            this.okayBut.TabIndex = 16;
            this.okayBut.Text = "Okay";
            this.okayBut.UseVisualStyleBackColor = true;
            this.okayBut.Click += new System.EventHandler(this.okayBut_Click);
            // 
            // rUd
            // 
            this.rUd.Location = new System.Drawing.Point(159, 26);
            this.rUd.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.rUd.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.rUd.Name = "rUd";
            this.rUd.Size = new System.Drawing.Size(45, 20);
            this.rUd.TabIndex = 17;
            this.rUd.ValueChanged += new System.EventHandler(this.rUd_ValueChanged);
            // 
            // rBar
            // 
            this.rBar.Location = new System.Drawing.Point(17, 16);
            this.rBar.Margin = new System.Windows.Forms.Padding(0);
            this.rBar.Maximum = 255;
            this.rBar.Minimum = -255;
            this.rBar.Name = "rBar";
            this.rBar.Size = new System.Drawing.Size(137, 45);
            this.rBar.TabIndex = 16;
            this.rBar.Scroll += new System.EventHandler(this.rBar_Scroll);
            // 
            // gUd
            // 
            this.gUd.Location = new System.Drawing.Point(160, 82);
            this.gUd.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.gUd.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.gUd.Name = "gUd";
            this.gUd.Size = new System.Drawing.Size(45, 20);
            this.gUd.TabIndex = 19;
            this.gUd.ValueChanged += new System.EventHandler(this.gUd_ValueChanged);
            // 
            // gBar
            // 
            this.gBar.Location = new System.Drawing.Point(18, 72);
            this.gBar.Margin = new System.Windows.Forms.Padding(0);
            this.gBar.Maximum = 255;
            this.gBar.Minimum = -255;
            this.gBar.Name = "gBar";
            this.gBar.Size = new System.Drawing.Size(137, 45);
            this.gBar.TabIndex = 18;
            this.gBar.Scroll += new System.EventHandler(this.gBar_Scroll);
            // 
            // bUd
            // 
            this.bUd.Location = new System.Drawing.Point(162, 138);
            this.bUd.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.bUd.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.bUd.Name = "bUd";
            this.bUd.Size = new System.Drawing.Size(45, 20);
            this.bUd.TabIndex = 21;
            this.bUd.ValueChanged += new System.EventHandler(this.bUd_ValueChanged);
            // 
            // bBar
            // 
            this.bBar.Location = new System.Drawing.Point(20, 128);
            this.bBar.Margin = new System.Windows.Forms.Padding(0);
            this.bBar.Maximum = 255;
            this.bBar.Minimum = -255;
            this.bBar.Name = "bBar";
            this.bBar.Size = new System.Drawing.Size(137, 45);
            this.bBar.TabIndex = 20;
            this.bBar.Scroll += new System.EventHandler(this.bBar_Scroll);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.bUd);
            this.groupBox2.Controls.Add(this.bBar);
            this.groupBox2.Controls.Add(this.gUd);
            this.groupBox2.Controls.Add(this.gBar);
            this.groupBox2.Controls.Add(this.rUd);
            this.groupBox2.Controls.Add(this.rBar);
            this.groupBox2.Location = new System.Drawing.Point(8, 240);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(213, 179);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Color Balance";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "R:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "G:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "B:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.contrastUd);
            this.groupBox3.Controls.Add(this.contrastBar);
            this.groupBox3.Location = new System.Drawing.Point(235, 172);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(202, 61);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Contrast";
            // 
            // contrastUd
            // 
            this.contrastUd.Location = new System.Drawing.Point(155, 24);
            this.contrastUd.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.contrastUd.Minimum = new decimal(new int[] {
            127,
            0,
            0,
            -2147483648});
            this.contrastUd.Name = "contrastUd";
            this.contrastUd.Size = new System.Drawing.Size(45, 20);
            this.contrastUd.TabIndex = 15;
            this.contrastUd.ValueChanged += new System.EventHandler(this.contrastUd_ValueChanged);
            // 
            // contrastBar
            // 
            this.contrastBar.Location = new System.Drawing.Point(13, 14);
            this.contrastBar.Margin = new System.Windows.Forms.Padding(0);
            this.contrastBar.Maximum = 127;
            this.contrastBar.Minimum = -127;
            this.contrastBar.Name = "contrastBar";
            this.contrastBar.Size = new System.Drawing.Size(137, 45);
            this.contrastBar.TabIndex = 14;
            this.contrastBar.Scroll += new System.EventHandler(this.contrastBar_Scroll);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.gammaUd);
            this.groupBox4.Controls.Add(this.gammaBar);
            this.groupBox4.Location = new System.Drawing.Point(236, 265);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(202, 61);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Gamma Correction";
            // 
            // gammaUd
            // 
            this.gammaUd.DecimalPlaces = 2;
            this.gammaUd.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.gammaUd.Location = new System.Drawing.Point(155, 24);
            this.gammaUd.Maximum = new decimal(new int[] {
            699,
            0,
            0,
            131072});
            this.gammaUd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.gammaUd.Name = "gammaUd";
            this.gammaUd.Size = new System.Drawing.Size(45, 20);
            this.gammaUd.TabIndex = 15;
            this.gammaUd.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.gammaUd.ValueChanged += new System.EventHandler(this.gammaUd_ValueChanged);
            // 
            // gammaBar
            // 
            this.gammaBar.Location = new System.Drawing.Point(13, 14);
            this.gammaBar.Margin = new System.Windows.Forms.Padding(0);
            this.gammaBar.Maximum = 699;
            this.gammaBar.Minimum = 1;
            this.gammaBar.Name = "gammaBar";
            this.gammaBar.Size = new System.Drawing.Size(137, 45);
            this.gammaBar.SmallChange = 10;
            this.gammaBar.TabIndex = 14;
            this.gammaBar.Value = 100;
            this.gammaBar.Scroll += new System.EventHandler(this.gammaBar_Scroll);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.saturationUd);
            this.groupBox5.Controls.Add(this.saturationBar);
            this.groupBox5.Location = new System.Drawing.Point(232, 357);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(202, 61);
            this.groupBox5.TabIndex = 18;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Saturation";
            // 
            // saturationUd
            // 
            this.saturationUd.Location = new System.Drawing.Point(155, 24);
            this.saturationUd.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.saturationUd.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.saturationUd.Name = "saturationUd";
            this.saturationUd.Size = new System.Drawing.Size(45, 20);
            this.saturationUd.TabIndex = 15;
            this.saturationUd.ValueChanged += new System.EventHandler(this.saturationUd_ValueChanged);
            // 
            // saturationBar
            // 
            this.saturationBar.Location = new System.Drawing.Point(13, 14);
            this.saturationBar.Margin = new System.Windows.Forms.Padding(0);
            this.saturationBar.Maximum = 255;
            this.saturationBar.Minimum = -255;
            this.saturationBar.Name = "saturationBar";
            this.saturationBar.Size = new System.Drawing.Size(137, 45);
            this.saturationBar.TabIndex = 14;
            this.saturationBar.Scroll += new System.EventHandler(this.saturationBar_Scroll);
            // 
            // adjustColorsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 484);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cancelBut);
            this.Controls.Add(this.okayBut);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newPictureBox);
            this.Controls.Add(this.origPictureBox);
            this.Name = "adjustColorsForm";
            this.Text = "Adjust Colors";
            ((System.ComponentModel.ISupportInitialize)(this.newPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.origPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessBar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessUd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rUd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gUd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bUd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bBar)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contrastUd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastBar)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gammaUd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gammaBar)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.saturationUd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saturationBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox newPictureBox;
        private System.Windows.Forms.PictureBox origPictureBox;
        private System.Windows.Forms.TrackBar brightnessBar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown brightnessUd;
        private System.Windows.Forms.Button cancelBut;
        private System.Windows.Forms.Button okayBut;
        private System.Windows.Forms.NumericUpDown rUd;
        private System.Windows.Forms.TrackBar rBar;
        private System.Windows.Forms.NumericUpDown gUd;
        private System.Windows.Forms.TrackBar gBar;
        private System.Windows.Forms.NumericUpDown bUd;
        private System.Windows.Forms.TrackBar bBar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown contrastUd;
        private System.Windows.Forms.TrackBar contrastBar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown gammaUd;
        private System.Windows.Forms.TrackBar gammaBar;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown saturationUd;
        private System.Windows.Forms.TrackBar saturationBar;
    }
}