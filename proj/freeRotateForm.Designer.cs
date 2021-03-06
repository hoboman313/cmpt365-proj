﻿namespace proj
{
    partial class freeRotateForm
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
            this.okayBut = new System.Windows.Forms.Button();
            this.cancelBut = new System.Windows.Forms.Button();
            this.rotateBar = new System.Windows.Forms.TrackBar();
            this.rotateLab = new System.Windows.Forms.Label();
            this.rotateNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.origPictureBox = new System.Windows.Forms.PictureBox();
            this.newPictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.rotateBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotateNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.origPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // okayBut
            // 
            this.okayBut.Location = new System.Drawing.Point(30, 258);
            this.okayBut.Name = "okayBut";
            this.okayBut.Size = new System.Drawing.Size(94, 34);
            this.okayBut.TabIndex = 0;
            this.okayBut.Text = "Okay";
            this.okayBut.UseVisualStyleBackColor = true;
            this.okayBut.Click += new System.EventHandler(this.okayBut_Click);
            // 
            // cancelBut
            // 
            this.cancelBut.Location = new System.Drawing.Point(181, 257);
            this.cancelBut.Name = "cancelBut";
            this.cancelBut.Size = new System.Drawing.Size(94, 34);
            this.cancelBut.TabIndex = 1;
            this.cancelBut.Text = "Cancel";
            this.cancelBut.UseVisualStyleBackColor = true;
            this.cancelBut.Click += new System.EventHandler(this.cancelBut_Click);
            // 
            // rotateBar
            // 
            this.rotateBar.Location = new System.Drawing.Point(14, 199);
            this.rotateBar.Maximum = 360;
            this.rotateBar.Name = "rotateBar";
            this.rotateBar.Size = new System.Drawing.Size(286, 42);
            this.rotateBar.TabIndex = 2;
            this.rotateBar.Scroll += new System.EventHandler(this.rotateBar_Scroll);
            // 
            // rotateLab
            // 
            this.rotateLab.AutoSize = true;
            this.rotateLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rotateLab.Location = new System.Drawing.Point(20, 164);
            this.rotateLab.Name = "rotateLab";
            this.rotateLab.Size = new System.Drawing.Size(109, 20);
            this.rotateLab.TabIndex = 3;
            this.rotateLab.Text = "Rotate angle: ";
            // 
            // rotateNumericUpDown
            // 
            this.rotateNumericUpDown.Location = new System.Drawing.Point(142, 165);
            this.rotateNumericUpDown.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.rotateNumericUpDown.Name = "rotateNumericUpDown";
            this.rotateNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.rotateNumericUpDown.TabIndex = 5;
            this.rotateNumericUpDown.ValueChanged += new System.EventHandler(this.rotateNumericUpDown_ValueChanged);
            // 
            // origPictureBox
            // 
            this.origPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.origPictureBox.Location = new System.Drawing.Point(0, 29);
            this.origPictureBox.Name = "origPictureBox";
            this.origPictureBox.Size = new System.Drawing.Size(146, 132);
            this.origPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.origPictureBox.TabIndex = 6;
            this.origPictureBox.TabStop = false;
            // 
            // newPictureBox
            // 
            this.newPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.newPictureBox.Location = new System.Drawing.Point(175, 29);
            this.newPictureBox.Name = "newPictureBox";
            this.newPictureBox.Size = new System.Drawing.Size(140, 135);
            this.newPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.newPictureBox.TabIndex = 7;
            this.newPictureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Old Image";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "New Image";
            // 
            // freeRotateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 300);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newPictureBox);
            this.Controls.Add(this.origPictureBox);
            this.Controls.Add(this.rotateNumericUpDown);
            this.Controls.Add(this.rotateLab);
            this.Controls.Add(this.rotateBar);
            this.Controls.Add(this.cancelBut);
            this.Controls.Add(this.okayBut);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "freeRotateForm";
            this.Text = "Free Rotate";
            ((System.ComponentModel.ISupportInitialize)(this.rotateBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotateNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.origPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okayBut;
        private System.Windows.Forms.Button cancelBut;
        private System.Windows.Forms.TrackBar rotateBar;
        private System.Windows.Forms.Label rotateLab;
        private System.Windows.Forms.NumericUpDown rotateNumericUpDown;
        private System.Windows.Forms.PictureBox origPictureBox;
        private System.Windows.Forms.PictureBox newPictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}