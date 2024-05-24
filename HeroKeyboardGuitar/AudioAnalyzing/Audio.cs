using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace AudioAnalyzing;

/// <summary>
/// Basic class for analyzing an audio file. Stores samples and 
/// does cluster analysis.
/// </summary>
public class Audio {
    private readonly string FILE_ROOT_PATH = $"{Application.StartupPath}../../../Songs/";
    private double[] samples;
    private WaveOutEvent outputDevice;
    private string directoryPath;
    private string songFile;
    private string beatMap;
    private AudioFileReader fileReader;

    /// <summary>
    /// The times at which the music hits a peak. Useful for generating points
    /// were the player must hit a key to the beat
    /// </summary>
    public List<Tuple<double,int>> ActionTimes { get; private set; }

    /// <summary>
    /// The song's total duration in milliseconds
    /// </summary>
    public double AudioLengthInMs { get; private set; }

    /// <summary>
    /// Total number of bytes 
    /// </summary>
    public long StreamLengthInBytes { get; private set; }

    /// <summary>
    /// Takes in a file path and starts analysis right away.
    /// Stores the results of that analysis
    /// </summary>
    /// <param name="filePath">Full absolute path to the .wav file</param>
    public Audio(string directoryPath) {
        this.directoryPath = directoryPath;
        songFile = Path.Combine(directoryPath, "audio.wav");
        beatMap = Path.Combine(directoryPath, "beat.json");
        fileReader = new AudioFileReader(songFile);
        int sampleRate = fileReader.WaveFormat.SampleRate;
        int sampleCount = (int)(fileReader.Length / fileReader.WaveFormat.BitsPerSample / 8);
        int channelCount = fileReader.WaveFormat.Channels;
        var samplesList = new List<double>(sampleCount);
        var buffer = new float[sampleRate * channelCount];
        int samplesRead = 0;
        while ((samplesRead = fileReader.Read(buffer, 0, buffer.Length)) > 0) {
            samplesList.AddRange(buffer.Take(samplesRead).Select(x => (double)x));
        }
        samples = samplesList.ToArray();
        AudioLengthInMs = fileReader.TotalTime.TotalMilliseconds;
        StreamLengthInBytes = fileReader.Length;
        outputDevice = new();
        ActionTimes = new();
        if (File.Exists(beatMap))
        {
            setActionTimes();
        }
    }

    /// <summary>
    /// Only used for debugging cluster analysis. Use these files in the Octave "reload.m" script
    /// which is in the "Audio Analysis via Octave" folder
    /// </summary>
    public void DebugFileWrite() {
        string filePath = Path.Combine(FILE_ROOT_PATH, "actiontimes.txt");
        File.WriteAllText(filePath, string.Join(',', ActionTimes));
    }
        
    /// <summary>
    /// Gets the index of the current sample being played
    /// </summary>
    /// <returns>Index/Position of the sample being played</returns>
    public int GetPosition() {
        double perComplete = outputDevice.GetPosition() / (double)StreamLengthInBytes;
        return (int)Math.Clamp(perComplete * samples.Length, 0, samples.Length - 1);
    }


    private void setActionTimes()
    {
        string jsonText = File.ReadAllText(beatMap);
        ActionTimes = JsonSerializer.Deserialize<List<Tuple<double,int>>>(jsonText);
    }

    public List<Double> getbeatTimesFromsongFile()
    {
        List<Double> beatTimes = new();
        List<double> clusters = new();
        List<double> curCluster = new();
        double THRES = 0.4;
        bool inCluster = false;
        const int MAX_TOLERANCE = 500;
        int curTolerance = 0;
        int clusterStart = -1;
        for (int i = 0; i < samples.Length; i++)
        {
            var sample = Math.Abs(samples[i]);
            if (!inCluster)
            {
                if (Math.Abs(sample) > THRES)
                { 
                    inCluster = true;
                    curTolerance = 0;
                    curCluster = new();
                    clusterStart = i;
                    clusters.Add(THRES);
                    curCluster.Add(sample);
                }
                else
                {
                    clusters.Add(0.0);
                }
            }
            else
            {
                if (Math.Abs(sample) < THRES / 1.5)
                {
                    curTolerance++;
                    if (curTolerance >= MAX_TOLERANCE)
                    {
                        inCluster = false;
                        int actionIndex = clusterStart + curCluster.IndexOf(curCluster.Max());
                        double actionTime = (actionIndex / (double)samples.Length) * AudioLengthInMs;
                        beatTimes.Add(actionTime);
                        clusters.Add(0.0);
                        curCluster.Add(0.0);
                    }
                    else
                    {
                        clusters.Add(THRES);
                        curCluster.Add(sample);
                    }
                }
                else
                {
                    curTolerance = 0;
                    clusters.Add(THRES);
                    curCluster.Add(sample);
                }
            }
        }
        return beatTimes;
    }

    /// <summary>
    /// Retrieves the total number of samples for this song
    /// </summary>
    /// <returns>Total number of samples</returns>
    public int GetNumberOfSamples() {
        return samples.Length;
    }

    /// <summary>
    /// Start playing the audio
    /// </summary>
    public void Play() {
        fileReader = new AudioFileReader(songFile);
        outputDevice.Init(fileReader);
        outputDevice.Play();
    }

    /// <summary>
    /// Stop playing the audio
    /// </summary>
    public void Stop() {
        outputDevice.Stop();
    }
}
