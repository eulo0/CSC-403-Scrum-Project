using AudioAnalyzing;
using HeroKeyboardGuitar.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HeroKeyboardGuitar;

internal partial class FrmGame : Form
{
    private List<Note> notes;
    private const float noteSpeed = 0.5f;
    //private PictureBox[4] picTargets;
    private Audio curSong;
    private Score score;

    // for double buffering
    protected override CreateParams CreateParams
    {
        get
        {
            var cp = base.CreateParams;
            cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
            return cp;
        }
    }

    public FrmGame()
    {
        InitializeComponent();
    }

    public void FrmMain_Load(object sender, EventArgs e)
    {
        score = new();
        lblScore.Text = score.Amount.ToString();
        panBg.BackgroundImage = Game.GetInstance().GetBg();
        panBg.Height = (int)(Height * 0.8);
        curSong = Game.GetInstance().CurSong;
        notes = new();
        foreach (var actionTime in curSong.ActionTimes)
        {
            double x = actionTime * noteSpeed + fret1.Left + fret1.Width;
            notes.Add(new Note(x));
        }
        Timer tmrWaitThenPlay = new()
        {
            Interval = 1000,
            Enabled = true,
        };
        components.Add(tmrWaitThenPlay);
        tmrWaitThenPlay.Tick += (e, sender) =>
        {
            Game.GetInstance().CurSong.Play();
            tmrWaitThenPlay.Enabled = false;
            tmrPlay.Enabled = true;
        };
    }

    private void tmrPlay_Tick(object sender, EventArgs e)
    {
        int index = curSong.GetPosition();
        foreach (var note in notes)
        {
            note.Move(tmrPlay.Interval * (noteSpeed * 1.3));
            if (note.isPicNull() && note.getXPos() <= 2000)
            {
                note.setPic(createMarkerPic());
            }
            if (note.CheckMiss(fret1))
            {
                score.Miss();
            }
        }
        if (index >= curSong.GetNumberOfSamples() - 1)
        {
            tmrPlay.Enabled = false;
            foreach (var note in notes)
            {
                Controls.Remove(note.Pic);
            }
            win();
        }
    }

    private void FrmMain_KeyPress(object sender, KeyPressEventArgs e)
    {
        {
            foreach (var note in notes)
            {
                if (note.CheckHit(fret1))
                {
                    score.Add(1);
                    lblScore.Text = score.Amount.ToString();
                    lblScore.Font = new Font("Arial", 42);
                    break;
                }
            }
        }
    }

    private void FrmMain_KeyDown(object sender, KeyEventArgs e)
    {

        fret1.BackgroundImage = Resources.pressed;

    }

    private void FrmMain_KeyUp(object sender, KeyEventArgs e)
    {
        fret1.BackgroundImage = Resources._default;
    }

    private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
        Game.GetInstance().CurSong.Stop();
    }

    private void tmrScoreShrink_Tick(object sender, EventArgs e)
    {
        if (lblScore.Font.Size > 20)
        {
            lblScore.Font = new("Arial", lblScore.Font.Size - 1);
        }
    }

    private PictureBox createMarkerPic()
    {
        const int noteSize = 50;
        PictureBox picNote = new()
        {
            BackColor = Color.Black,
            ForeColor = Color.Black,
            Width = noteSize,
            Height = noteSize,
            Left = -100,
            Top = fret1.Top + fret1.Height / 2 - noteSize / 2,
            BackgroundImage = Resources.marker,
            BackgroundImageLayout = ImageLayout.Stretch,
            Anchor = AnchorStyles.Bottom,
        };
        Controls.Add(picNote);
        picNote.BringToFront();
        return picNote;
    }

    public void win()
    {
        WinScreen winScreen = new WinScreen();
        winScreen.Show();
    }


    private void panel1_Paint(object sender, PaintEventArgs e)
    {

    }

    private void pictureBox3_Click(object sender, EventArgs e)
    {

    }
}
