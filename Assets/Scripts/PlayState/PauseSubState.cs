using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSubState : MonoBehaviour
{
    public GameObject pauseCanvas;
    public KeyCode pause;
    public KeyCode pause2;
    public AudioSource Inst;
    public AudioSource Vocals;

    void Update () 
    {
        if(Input.GetKeyDown(pause) || Input.GetKeyDown(pause2)) 
        {
            PauseGame();
        }    
    }

    public void PauseGame()
    {
        Inst.Pause();
        Vocals.Pause();
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
    }

    public void Resume()
    {
        Inst.UnPause();
        Vocals.UnPause();        
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene("PlayState");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("TitleState");
    }
}