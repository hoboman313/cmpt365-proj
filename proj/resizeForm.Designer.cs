namespace proj
{
    partial class resizeForm
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
            this.cancelBut = new System.Windows.Forms.Button();
            this.okayBut = new System.Windows.Forms.Button();
            this.currentSizeLab = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ratioCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.heightNumeric = new System.Windows.Forms.NumericUpDown();
            this.widthNumeric = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelBut
            // 
            this.cancelBut.Location = new System.Drawing.Point(163, 171);
            this.cancelBut.Name = "cancelBut";
            this.cancelBut.Size = new System.Drawing.Size(94, 34);
            this.cancelBut.TabIndex = 3;
            this.cancelBut.Text = "Cancel";
            this.cancelBut.UseVisualStyleBackColor = true;
            this.cancelBut.Click += new System.EventHandler(this.cancelBut_Click);
            // 
            // okayBut
            // 
            this.okayBut.Location = new System.Drawing.Point(12, 172);
            this.okayBut.Name = "okayBut";
            this.okayBut.Size = new System.Drawing.Size(94, 34);
            this.okayBut.TabIndex = 2;
            this.okayBut.Text = "Okay";
            this.okayBut.UseVisualStyleBackColor = true;
            this.okayBut.Click += new System.EventHandler(this.okayBut_Click);
            // 
            // currentSizeLab
            // 
            this.currentSizeLab.AutoSize = true;
            this.currentSizeLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentSizeLab.Location = new System.Drawing.Point(16, 11);
            this.currentSizeLab.Name = "currentSizeLab";
            this.currentSizeLab.Size = new System.Drawing.Size(98, 20);
            this.currentSizeLab.TabIndex = 4;
            this.currentSizeLab.Text = "Current size:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Width";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(122, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Height";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Preserve aspect ratio:";
            // 
            // ratioCheckBox
            // 
            this.ratioCheckBox.AutoSize = true;
            this.ratioCheckBox.Checked = true;
            this.ratioCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ratioCheckBox.Location = new System.Drawing.Point(188, 140);
            this.ratioCheckBox.Name = "ratioCheckBox";
            this.ratioCheckBox.Size = new System.Drawing.Size(15, 14);
            this.ratioCheckBox.TabIndex = 11;
            this.ratioCheckBox.UseVisualStyleBackColor = true;
            this.ratioCheckBox.CheckedChanged += new System.EventHandler(this.ratioCheckBox_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.heightNumeric);
            this.groupBox1.Controls.Add(this.widthNumeric);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(13, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 96);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New Size";
            // 
            // heightNumeric
            // 
            this.heightNumeric.Location = new System.Drawing.Point(126, 56);
            this.heightNumeric.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.heightNumeric.Name = "heightNumeric";
            this.heightNumeric.Size = new System.Drawing.Size(71, 20);
            this.heightNumeric.TabIndex = 16;
            this.heightNumeric.ValueChanged += new System.EventHandler(this.heightNumeric_ValueChanged);
            // 
            // widthNumeric
            // 
            this.widthNumeric.Location = new System.Drawing.Point(9, 57);
            this.widthNumeric.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.widthNumeric.Name = "widthNumeric";
            this.widthNumeric.Size = new System.Drawing.Size(70, 20);
            this.widthNumeric.TabIndex = 15;
            this.widthNumeric.ValueChanged += new System.EventHandler(this.widthNumeric_ValueChanged);
            // 
            // resizeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 227);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ratioCheckBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.currentSizeLab);
            this.Controls.Add(this.cancelBut);
            this.Controls.Add(this.okayBut);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "resizeForm";
            this.Text = "Resize Form";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelBut;
        private System.Windows.Forms.Button okayBut;
        private System.Windows.Forms.Label currentSizeLab;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ratioCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown heightNumeric;
        private System.Windows.Forms.NumericUpDown widthNumeric;
    }
}