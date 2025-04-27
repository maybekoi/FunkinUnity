using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class BPMChangeEvent
{
    public int stepTime;
    public float songTime;
    public int bpm;
}

public class Conductor : MonoBehaviour
{
    public static Conductor instance;

    [Header("Timing")]
    public int bpm = 100;
    public float crochet;
    public float stepCrochet;
    public float songPosition;
    public float lastSongPos;
    public float offset = 0;

    [Header("Hit Windows")]
    public int safeFrames = 10;
    public float safeZoneOffset;

    public AudioSource instSource;
    public AudioSource voicesSource;

    [Header("BPM Changes")]
    public List<BPMChangeEvent> bpmChangeMap = new List<BPMChangeEvent>();

    private AudioSource musicSource;
    private float startTime;
    private bool _isPlaying = false;
    public bool isPlaying
    {
        get { return _isPlaying; }
        private set { _isPlaying = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        UpdateTimingValues();
    }

    private void Update()
    {
        if (isPlaying && instSource != null && instSource.isPlaying)
        {
            lastSongPos = songPosition;
            songPosition = (instSource.time * 1000f) + offset;

            if (voicesSource != null && voicesSource.isPlaying)
            {
                float voiceTimeMs = voicesSource.time * 1000f;
                if (Mathf.Abs(voiceTimeMs - songPosition) > 30)
                {
                    ResyncVocals();
                }
            }
             if (Time.frameCount % 120 == 0)
             {
                 float voiceTimeLog = (voicesSource != null && voicesSource.isPlaying) ? voicesSource.time * 1000f : -1f;
                // Debug.Log("Conductor Update: songPos=" + songPosition + "ms (inst=" + instSource.time + "s), voicePos=" + voiceTimeLog + "ms");
             }
        }
        else if (isPlaying)
        {
        }
    }

    private void ResyncVocals()
    {
        if (voicesSource == null || !voicesSource.clip || instSource == null || !instSource.isPlaying) return;

        float targetTime = instSource.time;

        voicesSource.time = targetTime;
        if (!voicesSource.isPlaying)
        {
             voicesSource.Play();
        }


       // Debug.Log("Resynced vocals to instSource time: " + targetTime * 1000f + "ms");
    }

    public void StartSong()
    {
        if (instSource == null || instSource.clip == null)
        {
            Debug.LogError("Cannot start song: Instrumental source or clip is missing!");
            return;
        }

        StopSong();

        songPosition = 0 + offset;
        lastSongPos = songPosition;

        double startTimeDsp = AudioSettings.dspTime + 0.1;

        instSource.time = 0;
        instSource.PlayScheduled(startTimeDsp);

        if (voicesSource != null && voicesSource.clip != null)
        {
            voicesSource.time = 0;
            voicesSource.PlayScheduled(startTimeDsp);
        }

        this.startTime = (float)startTimeDsp;
        isPlaying = true;

        Debug.Log("Scheduled song start at DSP time: " + startTimeDsp + ". BPM: " + bpm);
    }

    public void StopSong()
    {
        isPlaying = false;

        if (instSource != null)
        {
            instSource.Stop();
        }
        if (voicesSource != null)
        {
            voicesSource.Stop();
        }

        songPosition = 0;
        Debug.Log("Song stopped.");
    }

    public void UpdateTimingValues()
    {
        crochet = (60f / bpm) * 1000f;
        stepCrochet = crochet / 4f;
        safeZoneOffset = (safeFrames / 60f) * 1000f;
    }

    public void MapBPMChanges(SongData song)
    {
        bpmChangeMap.Clear();

        int curBPM = song.bpm;
        int totalSteps = 0;
        float totalPos = 0;

        foreach (var section in song.notes)
        {
            if (section.changeBPM && section.bpm != curBPM)
            {
                curBPM = section.bpm;
                var eventData = new BPMChangeEvent
                {
                    stepTime = totalSteps,
                    songTime = totalPos,
                    bpm = curBPM
                };
                bpmChangeMap.Add(eventData);
            }

            int deltaSteps = section.lengthInSteps;
            totalSteps += deltaSteps;
            totalPos += ((60f / curBPM) * 1000f / 4f) * deltaSteps;
        }

       // Debug.Log("New BPM map created with " + bpmChangeMap.Count + " changes");
    }

    public void ChangeBPM(int newBpm)
    {
        bpm = newBpm;
        UpdateTimingValues();
    }

    public void PauseSong()
    {
        if (instSource == null) return;

        isPlaying = false;
        instSource.Pause();
    }

    public void ResumeSong()
    {
        if (instSource == null) return;

        isPlaying = true;
        startTime = (float)AudioSettings.dspTime - (songPosition / 1000f);
        instSource.UnPause();
    }
} 