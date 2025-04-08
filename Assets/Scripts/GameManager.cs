using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public string currentStage;
	public float songSpeed = 1f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
	
	public void NoteMiss(int direction) {
	}

	public void NoteHit(StrumNoteController note) {
	}
}
