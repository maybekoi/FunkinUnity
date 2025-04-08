using UnityEngine;
using System.IO;
using SFB;

public class AudioLoader : MonoBehaviour
{
    public AudioSource vocalsAudioSource;
    public AudioSource instAudioSource;

    public void LoadInst()
    {
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Select Funkin Inst file.", "", "ogg", false);
        if (paths.Length == 0)
        {
            Debug.Log("No file selected.");
            return;
        }

        if (paths.Length >= 1)
        {
            LoadAudioClipIntoSource(paths[0], instAudioSource);
        }
    }

    public void LoadVocals()
    {
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Select Funkin Vocals file.", "", "ogg", false);
        if (paths.Length == 0)
        {
            Debug.Log("No file selected.");
            return;
        }

        if (paths.Length >= 1)
        {
            LoadAudioClipIntoSource(paths[0], vocalsAudioSource);
        }
    }

    private void LoadAudioClipIntoSource(string filePath, AudioSource audioSource)
    {
        AudioClip clip = LoadAudioClip(filePath);
        if (clip != null)
        {
            audioSource.clip = clip;
        }
        else
        {
            Debug.LogError("Failed to load audio clip: " + filePath);
        }
    }

    private AudioClip LoadAudioClip(string filePath)
    {
        AudioClip clip = null;
        if (File.Exists(filePath))
        {
            clip = LoadAudioFromFile(filePath);
        }
        else
        {
            Debug.LogError("File does not exist: " + filePath);
        }
        return clip;
    }

    private AudioClip LoadAudioFromFile(string filePath)
    {
        WWW www = new WWW("file:///" + filePath);
        while (!www.isDone) { }
        if (string.IsNullOrEmpty(www.error))
        {
            return www.GetAudioClip(false, false);
        }
        else
        {
            Debug.LogError("Error loading audio clip: " + www.error);
            return null;
        }
    }
}
