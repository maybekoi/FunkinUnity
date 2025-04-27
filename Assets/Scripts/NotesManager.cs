using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class NotesManager : MonoBehaviour
{
    public GameObject[] notePrefabs = new GameObject[4];  // LEFT, DOWN, UP, RIGHT
    public float spawnLookaheadTime = 3000f;

    private List<NoteSpawnData> pendingNotes = new List<NoteSpawnData>();
    private int nextNoteIndex = 0;

    private struct NoteSpawnData
    {
        public float strumTime;
        public int noteType;
        public bool mustHit;
    }

    public const int LEFT_NOTE = 0;
    public const int DOWN_NOTE = 1;
    public const int UP_NOTE = 2;
    public const int RIGHT_NOTE = 3;
    
    public void LoadSongData(SwagSong songData)
    {
        pendingNotes.Clear();
        nextNoteIndex = 0;

        if (songData == null || songData.notes == null)
        {
            Debug.LogError("Song data or notes section is null!");
            return;
        }

        foreach (var section in songData.notes)
        {
            if (section == null || section.sectionNotes == null) continue;

            foreach (float[] noteData in section.sectionNotes)
            {
                if (noteData == null || noteData.Length < 2) continue;

                float strumTime = noteData[0];
                int noteType = (int)noteData[1] % 4;
                bool mustHit = section.mustHitSection;
                if (noteData[1] > 3)
                    mustHit = !section.mustHitSection;

                pendingNotes.Add(new NoteSpawnData
                {
                    strumTime = strumTime,
                    noteType = noteType,
                    mustHit = mustHit
                });
            }
        }

        pendingNotes = pendingNotes.OrderBy(n => n.strumTime).ToList();

        Debug.Log($"Loaded {pendingNotes.Count} notes.");
    }

    private void Update()
    {
        if (Conductor.instance == null || !Conductor.instance.isPlaying || nextNoteIndex >= pendingNotes.Count)
        {
            return;
        }

        float currentSongPosition = Conductor.instance.songPosition;

        while (nextNoteIndex < pendingNotes.Count &&
               pendingNotes[nextNoteIndex].strumTime <= currentSongPosition + spawnLookaheadTime)
        {
            SpawnNoteFromData(pendingNotes[nextNoteIndex]);
            nextNoteIndex++;
        }
    }

    private void SpawnNoteFromData(NoteSpawnData data)
    {
        if (data.noteType < 0 || data.noteType >= notePrefabs.Length)
        {
            Debug.LogError("Invalid note type: " + data.noteType);
            return;
        }

        GameObject prefab = notePrefabs[data.noteType];
        if (prefab == null)
        {
            Debug.LogError("Missing note prefab for type: " + data.noteType);
            return;
        }

        GameObject noteObj = Instantiate(prefab);
        Note note = noteObj.GetComponent<Note>();
        note.mustPress = data.mustHit;
        note.Initialize(data.strumTime, data.noteType);
    }

    public void ClearNotes()
    {
        foreach (Note note in FindObjectsOfType<Note>()) {
             Destroy(note.gameObject);
        }
        pendingNotes.Clear();
        nextNoteIndex = 0;
    }

    private void ValidateNotePrefabs()
    {
        if (notePrefabs.Length != 4)
        {
            Debug.LogError("NotesManager requires exactly 4 note prefabs!");
            return;
        }

        for (int i = 0; i < notePrefabs.Length; i++)
        {
            if (notePrefabs[i] == null)
            {
                Debug.LogError("Missing note prefab at index " + i + "!");
            }
        }
    }

    private void Start()
    {
        ValidateNotePrefabs();
    }
}