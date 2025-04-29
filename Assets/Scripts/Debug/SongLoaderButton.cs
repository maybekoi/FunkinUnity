using UnityEngine;
using UnityEngine.UI;
using System;
using SFB;

public class SongLoaderButton : MonoBehaviour
{
    public SongLoader songLoader;
    public Text songNameText;
    public Text songNameText2;
    public Text pausedOnTXT;
    public Text pausedOnTXT2;
    public Text songBPMText;
    public Text songP1Text;
    public Text songP2Text;

    public void OnButtonClick()
    {
        string[] jsonFilePathArray = StandaloneFileBrowser.OpenFilePanel("Select FNF Chart", "", "json", false);
        string jsonFilePath = jsonFilePathArray.Length > 0 ? jsonFilePathArray[0] : null;   
        if (!string.IsNullOrEmpty(jsonFilePath))
        {
            SongData songData = songLoader.LoadSongData(jsonFilePath);
            if (songData != null)
            {
                songNameText.text = "Loaded Song: " + songData.song.song;
                songNameText2.text = "Loaded Song: " + songData.song.song;
                pausedOnTXT.text = "Paused on: " + songData.song.song;
                pausedOnTXT2.text = "Paused on: " + songData.song.song;
                songBPMText.text = "BPM: " + songData.bpm;
                songP1Text.text = "Player 1: " + songData.song.player1;
                songP2Text.text = "Player 2: " + songData.song.player2;
                Debug.Log("Song: " + songData.song.song + " BPM: " + songData.bpm + " P1: " + songData.song.player1 + " P2: " + songData.song.player2);
            }
        }
    }

}