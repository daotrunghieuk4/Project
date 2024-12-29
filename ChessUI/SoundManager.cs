using System;
using System.IO;
using NAudio.Wave;

public class SoundManager
{
    private WaveOutEvent waveOutEvent;
    private AudioFileReader audioFileReader;

    public SoundManager()
    {
        waveOutEvent = new WaveOutEvent();
    }

    public void PlayBackgroundMusic()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "background.mp3");
        PlayLoopingSound(filePath);
    }

    public void PlayFootstepSound()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "footstep.mp3");
        PlaySound(filePath);
    }

    public void StopBackgroundMusic()
    {
        if (waveOutEvent != null)
        {
            waveOutEvent.PlaybackStopped -= OnPlaybackStopped;
            waveOutEvent.Stop();
            audioFileReader?.Dispose();
        }
    }

    private void PlaySound(string filePath)
    {
        audioFileReader?.Dispose();
        audioFileReader = new AudioFileReader(filePath);
        waveOutEvent.Init(audioFileReader);
        waveOutEvent.Play();
    }

    private void PlayLoopingSound(string filePath)
    {
        audioFileReader?.Dispose();
        audioFileReader = new AudioFileReader(filePath);
        waveOutEvent.Init(audioFileReader);

        waveOutEvent.PlaybackStopped += OnPlaybackStopped;

        waveOutEvent.Play();
    }

    private void OnPlaybackStopped(object sender, StoppedEventArgs e)
    {
        waveOutEvent.PlaybackStopped -= OnPlaybackStopped;
        audioFileReader.Position = 0;
        waveOutEvent.Play();
    }
}
