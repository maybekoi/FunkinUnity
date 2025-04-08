using UnityEngine;
using System.IO;
using SFB;
using System.Collections;

public class ChartLoader : MonoBehaviour
{
    public NotesManager notesManager;
    public Conductor conductor;

    private void Start()
    {
        if (notesManager == null)
        {
            Debug.LogError("NotesManager reference is missing in ChartLoader!");
        }
        if (conductor == null)
        {
            Debug.LogError("Conductor reference is missing in ChartLoader!");
        }
    }

    public void OpenChartBrowser()
    {
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Select Funkin Chart file", "", "json", false);
        
        if (paths.Length == 0)
        {
            Debug.Log("No file selected.");
            return;
        }

        if (paths.Length >= 1)
        {
            LoadChart(paths[0]);
        }
    }

    private void LoadChart(string path)
    {
        try
        {
            if (notesManager == null)
            {
                Debug.LogError("NotesManager reference is missing!");
                return;
            }
            if (conductor == null)
            {
                Debug.LogError("Conductor reference is missing!");
                return;
            }

            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError("Chart path is null or empty!");
                return;
            }

            string jsonContent = File.ReadAllText(path).Trim();

            SwagSong songData = SongData.ParseJson(jsonContent);
            if (songData == null)
            {
                Debug.LogError("Failed to parse song data from JSON!");
                return;
            }

            Debug.Log($"Successfully parsed song: {songData.song}, BPM: {songData.bpm}, Speed: {songData.speed}");

            conductor.bpm = songData.bpm;
            conductor.UpdateTimingValues();
            
            if (GameManager.instance != null)
            {
                GameManager.instance.songSpeed = songData.speed;
                Debug.Log($"Updated GameManager.songSpeed to: {songData.speed}");
            }
            else
            {
                Debug.LogWarning("GameManager instance not found, cannot update songSpeed.");
            }
            
            notesManager.ClearNotes();
            if (songData.notes != null)
            {
                try
                {
                    notesManager.LoadSongData(songData);
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error in LoadSongData: " + e.Message + "\nStack trace: " + e.StackTrace);
                    return;
                }
            } else {
                 Debug.LogWarning("Song data has no notes section.");
            }

            Conductor.instance.StartSong();

        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error loading chart: {e.Message}\nStack: {e.StackTrace}");
        }
    }
}
