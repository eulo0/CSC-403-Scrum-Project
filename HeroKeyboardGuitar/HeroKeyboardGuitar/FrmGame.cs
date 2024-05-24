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
    private Dictionary<char, PictureBox> keytoPicTarget;
    private Dictionary<int, int> fretNumtoPosition;
    private Dictionary<char, Dictionary<string, Image>> keytoTargetMap;
    private Dictionary<char, Dictionary<string, Image>> keytoMarkerMap;
    const int noteSize = 50;
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
        this.KeyPress += new KeyPressEventHandler(FrmMain_KeyPress);
        this.KeyDown += new KeyEventHandler(FrmMain_KeyDown);
        this.KeyUp += new KeyEventHandler(FrmMain_KeyUp);
    }

    public void FrmMain_Load(object sender, EventArgs e)
    {
        score = new();
        notes = new();
        lblScore.Text = score.Amount.ToString();
        panBg.Height = (int)(Height * 0.8);
        curSong = Game.GetInstance().CurSong;
        picTargets = new PictureBox[] { fret0, fret1, fret2, fret3 };
        keytoPicTarget = new()
        {
            {'D', fret0 },
            {'F', fret1 },
            {'J', fret2 },
            {'K', fret3 },
        };
        keytoTargetMap = new()
        {
            { 'D', new Dictionary<string, Image>
                {
                    { "default", Properties.Resources.default_green },
                    { "pressed", Properties.Resources.pressed_green }
                }
            },
            { 'F', new Dictionary<string, Image>
                {
                    { "default", Properties.Resources.default_red },
                    { "pressed", Properties.Resources.pressed_red }
                }
            },
            { 'J', new Dictionary<string, Image>
                {
                    { "default", Properties.Resources.default_yellow },
                    { "pressed", Properties.Resources.pressed_yellow }
                }
            },
            { 'K', new Dictionary<string, Image>
                {
                    { "default", Properties.Resources.default_blue },
                    { "pressed", Properties.Resources.pressed_blue }
                }
            },
        }; 
        fretNumtoPosition = new()
        {
            {0 , fret0.Left + fret0.Height / 2 - noteSize / 2 },
            {1 , fret1.Left + fret1.Height / 2 - noteSize / 2 },
            {2 , fret2.Left + fret2.Height / 2 - noteSize / 2 },
            {3 , fret3.Left + fret3.Height / 2 - noteSize / 2 },
        }; 
        foreach (var actionTime in curSong.ActionTimes)
        {
            double x = -1 * (actionTime.Item1 * noteSpeed + fret0.Top + fret0.Bottom);
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
            if (note.isPicNull() && note.getXPos() <= -100)
            {
                note.setPic(createMarkerPic(note.fretNum, (int)note.getXPos()));
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
            win();
        }
    }


    private void FrmMain_KeyPress(object sender, KeyPressEventArgs e)
    {
        var key = char.ToUpper((char)e.KeyChar);
        if (keytoTargetMap.ContainsKey(key))
        {
            foreach (var note in notes)
            {
                if (note.CheckHit(keytoPicTarget[key]))
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
        char key = (char)e.KeyCode;
        if (keytoTargetMap.ContainsKey(key))
        {
            var picTarget = keytoPicTarget[key];
            if (picTarget.BackgroundImage != keytoTargetMap[key]["pressed"])
            {
                picTarget.BackgroundImage = keytoTargetMap[key]["pressed"];
            }
        }
    }

    private void FrmMain_KeyUp(object sender, KeyEventArgs e)
    {
        char key = (char)e.KeyCode;
        if (keytoTargetMap.ContainsKey(key))
        {
            var picTarget = keytoPicTarget[key];
            if (picTarget.BackgroundImage != keytoTargetMap[key]["default"])
            {
                picTarget.BackgroundImage = keytoTargetMap[key]["default"];
            }
        }
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

    private PictureBox createMarkerPic(int FretIndex, int position)
    {
        PictureBox picNote = new()
        {
            BackColor = Color.Black,
            ForeColor = Color.Black,
            Width = noteSize,
            Height = noteSize,
            Left = fretNumtoPosition[FretIndex],
            Top = position,
            BackgroundImage = Resources.marker,
            BackgroundImageLayout = ImageLayout.Stretch,
            Anchor = AnchorStyles.Left,
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

    private void panBg_Paint(object sender, PaintEventArgs e)
    {

    }

    private void fret0_Click(object sender, EventArgs e)
    {

    }
}
