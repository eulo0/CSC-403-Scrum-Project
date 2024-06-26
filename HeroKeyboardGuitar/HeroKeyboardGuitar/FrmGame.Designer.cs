﻿using System.Windows.Forms;

namespace HeroKeyboardGuitar {
    partial class FrmGame {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tmrPlay = new Timer(components);
            tmrScoreShrink = new Timer(components);
            tmrPlay = new System.Windows.Forms.Timer(components);
            tmrScoreShrink = new System.Windows.Forms.Timer(components);
            fret2 = new System.Windows.Forms.PictureBox();
            fret1 = new System.Windows.Forms.PictureBox();
            fret3 = new System.Windows.Forms.PictureBox();
            fret0 = new System.Windows.Forms.PictureBox();
            lblScore = new System.Windows.Forms.Label();
            panBg = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)fret2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)fret1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)fret3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)fret0).BeginInit();
            panBg.SuspendLayout();
            SuspendLayout();
            // 
            // tmrPlay
            // 
            tmrPlay.Interval = 50;
            tmrPlay.Tick += tmrPlay_Tick;
            // 
            // tmrScoreShrink
            // 
            tmrScoreShrink.Enabled = true;
            tmrScoreShrink.Tick += tmrScoreShrink_Tick;
            // 
            // fret2
            // 

            fret2.Name = "fret2";
            fret2.Size = new System.Drawing.Size(120, 120);
            fret2.TabIndex = 7;
            fret2.TabStop = false;
            // 
            // fret1
            // 

            fret1.Name = "fret1";
            fret1.Size = new System.Drawing.Size(120, 120);
            fret1.TabIndex = 6;
            fret1.TabStop = false;
            // 
            // fret3
            // 

            fret3.Name = "fret3";
            fret3.Size = new System.Drawing.Size(120, 120);
            fret3.TabIndex = 8;
            fret3.TabStop = false;
            fret3.Click += pictureBox3_Click;
            // 
            // fret0
            // 

            fret0.Name = "fret0";
            fret0.Size = new System.Drawing.Size(120, 120);
            fret0.TabIndex = 3;
            fret0.TabStop = false;

            // 
            // panBg
            // 
            panBg.BackgroundImageLayout = ImageLayout.Stretch;
            panBg.Controls.Add(fret3);
            panBg.Controls.Add(lblScore);
            panBg.Controls.Add(fret1);
            panBg.Controls.Add(fret2);
            panBg.Dock = DockStyle.Top;
            panBg.Location = new System.Drawing.Point(0, 0);
            panBg.Name = "panBg";
            panBg.Size = new System.Drawing.Size(1237, 480);
            panBg.TabIndex = 6;
            panBg.Paint += panBg_Paint;

            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            label1.ForeColor = System.Drawing.Color.White;
            label1.Location = new System.Drawing.Point(555, 483);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(129, 89);
            label1.TabIndex = 9;
            label1.Text = "0";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label1.Click += label1_Click;
            // 
            // lblScore
            // 
            lblScore.BackColor = System.Drawing.Color.Transparent;
            lblScore.Dock = DockStyle.Bottom;
            lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            lblScore.ForeColor = System.Drawing.Color.White;
            lblScore.Location = new System.Drawing.Point(0, 361);
            lblScore.Name = "lblScore";
            lblScore.Size = new System.Drawing.Size(1237, 90);
            lblScore.TabIndex = 5;
            lblScore.Text = "0";
            lblScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblScore.Click += lblScore_Click;
            // 
            // panBg
            // 
            panBg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            panBg.Controls.Add(lblScore);
            panBg.Dock = System.Windows.Forms.DockStyle.Top;
            panBg.Location = new System.Drawing.Point(0, 0);
            panBg.Name = "panBg";
            panBg.Size = new System.Drawing.Size(1237, 451);
            panBg.TabIndex = 6;
            panBg.Paint += panBg_Paint;
            // 
            // FrmGame
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Black;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new System.Drawing.Size(1237, 644);

            Controls.Add(label1);

            Controls.Add(fret0);
            Controls.Add(panBg);
            Controls.Add(fret2);
            Controls.Add(fret1);
            Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Name = "FrmGame";
            Text = "Play Song";
            WindowState = FormWindowState.Maximized;
            FormClosing += FrmMain_FormClosing;
            Load += FrmMain_Load;
            KeyDown += FrmMain_KeyDown;
            KeyPress += FrmMain_KeyPress;
            KeyUp += FrmMain_KeyUp;
            ((System.ComponentModel.ISupportInitialize)fret2).EndInit();
            ((System.ComponentModel.ISupportInitialize)fret1).EndInit();
            ((System.ComponentModel.ISupportInitialize)fret3).EndInit();
            ((System.ComponentModel.ISupportInitialize)fret0).EndInit();
            panBg.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer tmrPlay;
        private System.Windows.Forms.Timer tmrScoreShrink;
        private System.Windows.Forms.PictureBox fret2;
        private System.Windows.Forms.PictureBox fret1;
        private System.Windows.Forms.PictureBox fret3;
        private System.Windows.Forms.PictureBox fret0;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panBg;
    }
}
