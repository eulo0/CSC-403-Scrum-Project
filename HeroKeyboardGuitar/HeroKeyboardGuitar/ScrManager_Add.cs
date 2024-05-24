using AudioAnalyzing;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.Json;
using ScottPlot;
using System.Text;
using System.Text.Json.Serialization;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;

namespace HeroKeyboardGuitar;

internal partial class ScrManager_Add : UserControl
{
    int seed;
    private const int MAX_SONG_AMOUNT = 10;
    private string SONGS_ROOT_PATH = $"{Application.StartupPath}../../../Songs/";
    private GenreType[] genreArray;
    private int currentGenreIndex;
    public ScreenSwapHandler handler;
    private int seedBox;
    private string audioPath;
    private string songGenre;
    private string beatPath;


    /// <summary>
    /// creates the genre array and starts the button in the condition that it hasnt been pressed yet
    /// </summary>
    public ScrManager_Add()
    {
        InitializeComponent();
    }

    /// <summary>
    /// initializes all fields to the empty string
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ScrManager_Add_Load(object sender, EventArgs e)
    {
        seed = 0;
        genreArray = (GenreType[])Enum.GetValues(typeof(GenreType));
        currentGenreIndex = -1;
        audioPath = "";
        songGenre = "";
        beatPath = "";
    }

    private void seedBox1_MouseClick(object sender, MouseEventArgs e)
    {
        seedBox1.Text = "";
        //seed = 0;
    }
    private void seedBox_inputfilter(object sender, KeyPressEventArgs e)
    {
        if (seedBox1.Text.Length >= 10 && !(e.KeyChar == (char)8))
        {
            e.Handled = true;
        }
        else if (!(char.IsDigit(e.KeyChar)) && !(char.IsControl(e.KeyChar)))
        {
            e.Handled = true;
        }
    }

    private void btn_SelectSongFileClicked(object sender, EventArgs e)
    {
        setFileandUpdateButton(ref audioPath, ".wav", btn_SelectSongFile);
    }

    /// <summary>
    /// goes back to the title screen and resets this user control.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btn_backClicked(object sender, EventArgs e)
    {
        handler.gotoTitle();
        handler.goUpdate_SongManage();
    }

    /// <summary>
    /// if it hasnt been pressed yet, displays the default message. if not, it cycles through 
    /// the enum values in genreArray and displays the text on the button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btn_SelectGenreClicked(object sender, EventArgs e)
    {
        if (currentGenreIndex == genreArray.Length - 1 || currentGenreIndex == -1)
        {
            currentGenreIndex = 0;
        }
        else
        {
            currentGenreIndex++;
        }
        songGenre = genreArray[currentGenreIndex].ToString();
        btn_SelectGenre.Text = songGenre;
        songGenre.ToLower();
    }

    /// <summary>
    /// sets the field for beat path to the file opened by the user and updates
    /// the button to show the file picked
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btn_SelectMapClicked(object sender, EventArgs e)
    {
        setFileandUpdateButton(ref beatPath, ".txt", btn_selectMap);
    }

    /// <summary>
    /// creates a song folder in Songs and has input validation.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btn_CreateSongClicked(object sender, EventArgs e)
    {
        if (isPathFull(SONGS_ROOT_PATH))
        {
            MessageBox.Show($"Too many songs in the folder. Maximum amount is {MAX_SONG_AMOUNT}. Try deleting a song folder" +
                           "from the Delete button in Manage Songs and then come back.",
                           "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try { seed = int.Parse(seedBox1.Text); }
        catch { seed = 0; }
      
        bool validSongFile = Path.GetExtension(audioPath) == ".wav";
        bool validGenre = songGenre != "";
        bool validBeatMap = Path.GetExtension(beatPath) == ".txt";
        if (validSongFile && validGenre)
        {
            DialogResult confirmation = MessageBox.Show("Any songs that share the same name and genre will be overwritten. Do you want to " +
            "proceed?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (confirmation == DialogResult.Yes)
            {
                string songName = Path.GetFileNameWithoutExtension(audioPath);
                string folderName = $"{songName}_{songGenre.ToLower()}";
                string newFolderPath = Path.Combine(SONGS_ROOT_PATH, folderName);
                System.IO.Directory.CreateDirectory(newFolderPath);
                string songCopiedFile = Path.Combine(newFolderPath, "audio.wav");
                File.Copy(audioPath, songCopiedFile, true);
                if (validBeatMap)
                {
                    createJSON_mapFromText(beatPath, newFolderPath);
                }
                else
                {
                    DialogResult errorHandling = MessageBox.Show("The beat map is invalid. Would you like use an autogenerated beat map instead?",
                                                                "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (errorHandling == DialogResult.Yes)
                    {
                        createJSON_mapFromAudio(newFolderPath);
                    }

                    else
                    {
                        File.Delete(newFolderPath);
                        return;
                    }
                }
                MessageBox.Show("Song Succesfully Added!");
            }

        }
        else
        {
            string message = "";
            Dictionary<string, bool> fieldsMap = new()
                {
                    { "Song", validSongFile },
                    { "Genre", validGenre},
                    { "Beat Map", validBeatMap }
                };
            foreach (KeyValuePair<string, bool> pair in fieldsMap)
            {
                if (!pair.Value)
                {
                    message += $"{pair.Key} is not the right file type.\n";
                }
            }
            message += "Refer to the bottom (parantheses section) for the accepted file type(s)";
            MessageBox.Show(message);
        }
    }

    /// <summary>
    /// sets a parameter to the filepath and updates a button to display the name of the file 
    /// alogn with its extension
    /// </summary>
    /// <param name="field"></param>
    /// <param name="extension"></param>
    /// <param name="button"></param>
    private void setFileandUpdateButton(ref string field, string extension, Button button)
    {
        string filter = extension + "|";
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = filter;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            field = dialog.FileName;
            button.Text = $"{Path.GetFileName(field)}\nClick to change {extension} file.";
        }
    }
    /// <summary>
    /// creates a beat map based off of the old method to get beat timings.
    /// the logic is similiar to createJSON_mapFromTest. Refactor later once avaialable
    /// </summary>
    /// <param name="directoryPath"></param>
    /// <param name="directory"></param>
    private void createJSON_mapFromAudio(string directoryPath)
    {
        var genSeed = new Random(seed);
        List<Tuple<double, int>> beatTimeList = new();
        Audio song = new Audio(directoryPath);
        List<double> actionTimesfromAudio = song.getbeatTimesFromsongFile();
        foreach (double actionTime in actionTimesfromAudio) {
            int randint = genSeed.Next(0,3);
            beatTimeList.Add(Tuple.Create(actionTime, randint));
        }
        string fullFilePath = Path.Combine(directoryPath, "beat.json");
        using (File.Create(directoryPath)) ;
        string jsonString = JsonSerializer.Serialize(beatTimeList);
        File.WriteAllText(fullFilePath, jsonString);
    }

    /// <summary>
    /// Takes in a txt file, reads it, and then create a json file in the appropiate format
    /// in the song directory
    /// </summary>
    /// <param name="textFile"></param>
    /// <param name="directory"></param>
    private void createJSON_mapFromText(string textFile, string directory)
    {
        var genSeed = new Random(seed);
        List<Tuple<double, int>> beatTimeList = new();
        string[] lines = File.ReadAllLines(textFile);
        string fullFilePath = Path.Combine(directory, "beat.json");
        using (File.Create(fullFilePath));
        foreach (string line in lines)
        {
            string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            string startTime = parts[0];
            int fretNumber = genSeed.Next(0, 3);
            try
            {
                double ActionTime = Double.Parse(startTime) * 1000;
                Tuple<double, int> timeandFret = Tuple.Create(ActionTime, fretNumber);
                if (!beatTimeList.Contains(timeandFret))
                {
                    beatTimeList.Add(timeandFret);
                }
            }
            catch
            {
                MessageBox.Show("The file selected is not in the appropiate format. Are you sure it's exported from Audacity?");
            }
        }
        string jsonString = JsonSerializer.Serialize(beatTimeList);
        File.WriteAllText(fullFilePath, jsonString);
    }

    /// <summary>
    /// checks to see if the songs folder has more than the maximum amount of songs
    /// according to the maximum limit. This is called whenever a user creates a song 
    /// and prevents them from doing so if this method returns true
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private bool isPathFull(string path)
    {
        String[] subdirectories = Directory.GetDirectories(path);
        return subdirectories.Length >= MAX_SONG_AMOUNT;
    }
}
