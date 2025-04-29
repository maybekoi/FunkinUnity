using UnityEngine;
using System.IO;
using System;

public class SongLoader : MonoBehaviour
{
    public SongData LoadSongData(string jsonFilePath)
    {
        if (File.Exists(jsonFilePath))
        {
            string json = File.ReadAllText(jsonFilePath);
            SongData songData = JsonUtility.FromJson<SongData>(json);
            return songData;
        }
        else
        {
            Debug.LogError("File not found: " + jsonFilePath);
            return null;
        }
    }
}

[Serializable]
public class SongData
{
    public SongInfo song;
    public int bpm;
}

[Serializable]
public class SongInfo
{
    public string song;
    public string player1;
    public string player2;
}

