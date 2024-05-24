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
    private PictureBox[] picTargets;
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
        notes = new();
        lblScore.Text = score.Amount.ToString();
        panBg.BackgroundImage = Game.GetInstance().GetBg();
        panBg.Height = (int)(Height * 0.8);
        curSong = Game.GetInstance().CurSong;
        picTargets = new PictureBox[] { fret0, fret1, fret2, fret3 };
        /*
        picTargets = new PictureBox[4];
        picTargets[0] = fret0;
        picTargets[1] = fret1;
        picTargets[2] = fret2;
        picTargets[3] = fret3;
        */
        foreach (var actionTime in curSong.ActionTimes)
        {
            double x = actionTime.Item1 * noteSpeed + fret0.Left + fret0.Width;
            notes.Add(new Note(x, actionTime.Item2));
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
                note.setPic(createMarkerPic(note.fretNum));
            }
            else 
            {
                if (note.CheckMiss(picTargets[note.fretNum]))
                {
                    score.Miss();
                }
            }
        }
        if (index >= curSong.GetNumberOfSamples() - 1)
        {
            tmrPlay.Enabled = false;
            foreach (var note in notes)
            {
                Controls.Remove(note.Pic);
            }
        }
    }

    private void FrmMain_KeyPress(object sender, KeyPressEventArgs e)
    {
        {
            foreach (var note in notes)
            {
                if (note.CheckHit(picTargets[note.fretNum]))
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

        fret0.BackgroundImage = Resources.pressed;

    }

    private void FrmMain_KeyUp(object sender, KeyEventArgs e)
    {
        fret0.BackgroundImage = Resources._default;
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

    private PictureBox createMarkerPic(int FretIndex)
    {
        const int noteSize = 50;
        PictureBox picNote = new()
        {
            BackColor = Color.Black,
            ForeColor = Color.Black,
            Width = noteSize,
            Height = noteSize,
            Left = -100,
            Top = picTargets[FretIndex].Top + picTargets[FretIndex].Height / 2 - noteSize / 2,
            BackgroundImage = Resources.marker,
            BackgroundImageLayout = ImageLayout.Stretch,
            Anchor = AnchorStyles.Bottom,
        };
        Controls.Add(picNote);
        picNote.BringToFront();
        return picNote;
    }

    private void panel1_Paint(object sender, PaintEventArgs e)
    {

    }

    private void pictureBox3_Click(object sender, EventArgs e)
    {

    }

    private void panBg_Paint(object sender, PaintEventArgs e)
    {

    }
}
