using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsState : MonoBehaviour 
{
    public GameObject mainCanvas;
    public GameObject keybindCanvas;
    public GameObject gameplayCanvas;
	public GameObject otherCanvas;

    public void KeyBinds()
    {
		mainCanvas.SetActive(false);
        keybindCanvas.SetActive(true);
	}

	public void GamePlay()
	{
		mainCanvas.SetActive(false);
        gameplayCanvas.SetActive(true);
	}

	public void KBBack()
	{
		mainCanvas.SetActive(true);
        keybindCanvas.SetActive(false);
	}

    public void H2PBack()
	{
		mainCanvas.SetActive(true);
        gameplayCanvas.SetActive(false);
	}

	public void OptionsBack()
	{
		SceneManager.LoadScene("TitleStateNew");
	}
}