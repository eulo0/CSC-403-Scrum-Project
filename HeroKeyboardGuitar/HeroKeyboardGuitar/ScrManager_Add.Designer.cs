using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HeroKeyboardGuitar
{
    partial class ScrManager_Add
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
            btn_selectMap = new Button();
            btn_back = new Button();
            btn_SelectSongFile = new Button();
            btn_SelectGenre = new Button();
            btn_createSong = new Button();
            seedBox1 = new TextBox();
            SuspendLayout();
            // 
            // btn_selectMap
            // 
            btn_selectMap.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_selectMap.BackColor = Color.Aquamarine;
            btn_selectMap.FlatStyle = FlatStyle.Popup;
            btn_selectMap.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            btn_selectMap.ForeColor = Color.Black;
            btn_selectMap.Location = new Point(479, 509);
            btn_selectMap.Margin = new Padding(3, 4, 3, 4);
            btn_selectMap.Name = "btn_selectMap";
            btn_selectMap.Size = new Size(554, 120);
            btn_selectMap.TabIndex = 1;
            btn_selectMap.Text = "Select beat map\n(Must be a .txt file exported from Audacity's Beat Finder)";
            btn_selectMap.UseVisualStyleBackColor = false;
            btn_selectMap.Click += btn_SelectMapClicked;
            // 
            // btn_back
            // 
            btn_back.BackColor = Color.Red;
            btn_back.FlatStyle = FlatStyle.Popup;
            btn_back.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point);
            btn_back.ForeColor = Color.Black;
            btn_back.Location = new Point(3, 4);
            btn_back.Margin = new Padding(3, 4, 3, 4);
            btn_back.Name = "btn_back";
            btn_back.Size = new Size(322, 120);
            btn_back.TabIndex = 2;
            btn_back.Text = "Back";
            btn_back.UseVisualStyleBackColor = false;
            btn_back.Click += btn_backClicked;
            // 
            // btn_SelectSongFile
            // 
            btn_SelectSongFile.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_SelectSongFile.BackColor = Color.Aquamarine;
            btn_SelectSongFile.FlatStyle = FlatStyle.Popup;
            btn_SelectSongFile.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            btn_SelectSongFile.ForeColor = Color.Black;
            btn_SelectSongFile.Location = new Point(479, 161);
            btn_SelectSongFile.Margin = new Padding(3, 4, 3, 4);
            btn_SelectSongFile.Name = "btn_SelectSongFile";
            btn_SelectSongFile.Size = new Size(554, 120);
            btn_SelectSongFile.TabIndex = 5;
            btn_SelectSongFile.Text = "Select Song File\n(Must be .wav file)";
            btn_SelectSongFile.UseVisualStyleBackColor = false;
            btn_SelectSongFile.Click += btn_SelectSongFileClicked;
            // 
            // btn_SelectGenre
            // 
            btn_SelectGenre.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_SelectGenre.BackColor = Color.Aquamarine;
            btn_SelectGenre.FlatStyle = FlatStyle.Popup;
            btn_SelectGenre.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            btn_SelectGenre.ForeColor = Color.Black;
            btn_SelectGenre.Location = new Point(479, 330);
            btn_SelectGenre.Margin = new Padding(3, 4, 3, 4);
            btn_SelectGenre.Name = "btn_SelectGenre";
            btn_SelectGenre.Size = new Size(554, 120);
            btn_SelectGenre.TabIndex = 5;
            btn_SelectGenre.Text = "Select Genre (click to cycle and stay on one)";
            btn_SelectGenre.UseVisualStyleBackColor = false;
            btn_SelectGenre.Click += btn_SelectGenreClicked;
            // 
            // btn_createSong
            // 
            btn_createSong.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_createSong.BackColor = Color.LimeGreen;
            btn_createSong.FlatStyle = FlatStyle.Popup;
            btn_createSong.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point);
            btn_createSong.ForeColor = Color.Black;
            btn_createSong.Location = new Point(479, 683);
            btn_createSong.Margin = new Padding(3, 4, 3, 4);
            btn_createSong.Name = "btn_createSong";
            btn_createSong.Size = new Size(554, 120);
            btn_createSong.TabIndex = 6;
            btn_createSong.Text = "Create Song";
            btn_createSong.UseVisualStyleBackColor = false;
            btn_createSong.Click += btn_CreateSongClicked;
            // 
            // seedBox1
            // 
            seedBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            seedBox1.Font = new Font("Segoe UI", 30F, FontStyle.Regular, GraphicsUnit.Point);
            seedBox1.Location = new Point(435, 4);
            seedBox1.Multiline = true;
            seedBox1.Name = "seedBox1";
            seedBox1.Size = new Size(630, 135);
            seedBox1.TabIndex = 7;
            seedBox1.Text = "Enter a seed";
            seedBox1.TextAlign = HorizontalAlignment.Center;
            seedBox1.MouseClick += seedBox1_MouseClick;
            seedBox1.KeyPress += seedBox_inputfilter;
            // 
            // ScrManager_Add
            // 
            BackgroundImage = Properties.Resources.song_manager;
            BackgroundImageLayout = ImageLayout.Stretch;
            Controls.Add(seedBox1);
            Controls.Add(btn_selectMap);
            Controls.Add(btn_SelectSongFile);
            Controls.Add(btn_createSong);
            Controls.Add(btn_SelectGenre);
            Controls.Add(btn_back);
            DoubleBuffered = true;
            Name = "ScrManager_Add";
            Size = new Size(1405, 885);
            Load += ScrManager_Add_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button btn_selectMap;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Button btn_SelectSongFile;
        private Button btn_SelectGenre;
        private Button btn_createSong;
        private TextBox seedBox1;
    }
}
