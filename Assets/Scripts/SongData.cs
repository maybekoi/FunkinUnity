using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[System.Serializable]
public class SwagSection
{
    public List<float[]> sectionNotes = new List<float[]>();
    public int lengthInSteps = 16;
    public bool changeBPM;
    public int bpm;
    public bool mustHitSection;
    public bool gfSection;
    public int typeOfSection;
    public bool altAnim;
}

[System.Serializable]
public class ChartWrapper
{
    public SwagSong song;
}

[System.Serializable]
public class SwagSong
{
    public string song;
    public int bpm;
    public float speed;
    public bool needsVoices;
    public List<float> sectionLengths;
    public List<SwagSection> notes;
    public string player1;
    public string player2;
    public bool validScore;
}

public class SongData : MonoBehaviour
{
    public static SwagSong instance;

    public string song;
    public List<SwagSection> notes = new List<SwagSection>();
    public int bpm;
    public bool needsVoices = true;
    public float speed = 1f;
    public string player1 = "bf";
    public string player2 = "dad";

    private void Awake()
    {
    }

    public static SwagSong LoadFromJson(string jsonInput, string folder = "")
    {
        string path = Path.Combine(Path.Combine(Application.streamingAssetsPath, "data"), Path.Combine(folder.ToLower(), jsonInput.ToLower() + ".json"));
        
        if (!File.Exists(path))
        {
            Debug.LogError("Song file not found at path: " + path);
            return null;
        }

        string rawJson = File.ReadAllText(path).Trim();
        
        while (!rawJson.EndsWith("}"))
        {
            rawJson = rawJson.Substring(0, rawJson.Length - 1);
        }

        return ParseJson(rawJson);
    }

    public static SwagSong ParseJson(string jsonContent)
    {
        SwagSong song = null;
        try
        {
            JObject chartJson = JObject.Parse(jsonContent);
            JToken songNode = chartJson["song"];

            if (songNode == null || songNode.Type != JTokenType.Object)
            {
                Debug.LogError("JSON does not contain a valid 'song' object node.");
                return null;
            }

            string songName = songNode["song"]?.Value<string>() ?? "Unknown Song";
            int bpm = songNode["bpm"]?.Value<int>() ?? 100;
            bool needsVoices = songNode["needsVoices"]?.Value<bool>() ?? true;
            float speed = songNode["speed"]?.Value<float>() ?? 1.0f;
            string player1 = songNode["player1"]?.Value<string>() ?? "bf";
            string player2 = songNode["player2"]?.Value<string>() ?? "dad";

            song = new SwagSong
            {
                song = songName,
                notes = new List<SwagSection>(),
                bpm = bpm,
                needsVoices = needsVoices,
                speed = speed,
                player1 = player1,
                player2 = player2,
                validScore = true
            };

            JArray notesArray = songNode["notes"] as JArray;
            if (notesArray != null)
            {
                for(int sectionIndex = 0; sectionIndex < notesArray.Count; sectionIndex++)
                {
                     JToken sectionToken = notesArray[sectionIndex];
                     if (sectionToken == null || sectionToken.Type != JTokenType.Object)
                     {
                          Debug.LogWarning($"Skipping invalid section token at index {sectionIndex}. Token: {sectionToken}");
                          continue;
                     }
                     JObject sectionJson = (JObject)sectionToken;

                     int lengthInSteps = sectionJson["lengthInSteps"]?.Value<int>() ?? 16;
                     bool mustHitSection = sectionJson["mustHitSection"]?.Value<bool>() ?? true;
                     int sectionBpm = sectionJson["bpm"]?.Value<int>() ?? song.bpm;
                     bool changeBPM = sectionJson["changeBPM"]?.Value<bool>() ?? false;

                     SwagSection section = new SwagSection
                     {
                         lengthInSteps = lengthInSteps,
                         mustHitSection = mustHitSection,
                         bpm = sectionBpm,
                         changeBPM = changeBPM,
                         sectionNotes = new List<float[]>()
                     };

                    JArray sectionNotesJson = sectionJson["sectionNotes"] as JArray;
                    if (sectionNotesJson != null)
                    {
                        for(int noteIndex = 0; noteIndex < sectionNotesJson.Count; noteIndex++)
                        {
                             JToken noteToken = sectionNotesJson[noteIndex];
                             if (noteToken == null || noteToken.Type != JTokenType.Array) {
                                  Debug.LogWarning($"Skipping invalid note token at section {sectionIndex}, note {noteIndex}. Token: {noteToken}");
                                  continue;
                             }

                            try
                            {
                                JArray noteJsonArray = (JArray)noteToken;
                                float[] noteData = noteJsonArray.ToObject<float[]>();

                                if (noteData != null && noteData.Length >= 2)
                                {
                                    section.sectionNotes.Add(noteData);
                                }
                                else
                                {
                                    Debug.LogWarning($"Skipping note with invalid data length at section {sectionIndex}, note {noteIndex}. Data: {noteToken}");
                                }
                            }
                            catch (System.Exception noteEx)
                            {
                                Debug.LogError($"Error converting note token to float[] at section {sectionIndex}, note {noteIndex}. Token: {noteToken}\nError: {noteEx.Message}");
                            }
                        }
                    }
                    song.notes.Add(section);
                }
            }
            else
            {
                Debug.LogWarning("Song node does not contain 'notes' array.");
            }

            instance = song;
            return song;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Critical error parsing chart JSON: {e.Message}\nProcessing stopped.\nStack trace: {e.StackTrace}");
            instance = null;
            return null;
        }
    }

    public void InitializeFromSwagSong(SwagSong swagSong)
    {
        song = swagSong.song;
        notes = swagSong.notes;
        bpm = swagSong.bpm;
        needsVoices = swagSong.needsVoices;
        speed = swagSong.speed;
        player1 = swagSong.player1;
        player2 = swagSong.player2;
    }

    public void Clear()
    {
        song = "";
        notes.Clear();
        bpm = 100;
        needsVoices = true;
        speed = 1f;
        player1 = "bf";
        player2 = "dad";
    }
} 