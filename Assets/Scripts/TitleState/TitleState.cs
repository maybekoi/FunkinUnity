using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleState : MonoBehaviour
{
	public GameObject MainCanvas;
  public GameObject ChangelogCanvas;

    public void OpenSubstate()
    {
		MainCanvas.SetActive(false);
        ChangelogCanvas.SetActive(true);
	}

    public void BACK()
    {
		MainCanvas.SetActive(true);
        ChangelogCanvas.SetActive(false);
	}

    public void PlayButton()
    {
		SceneManager.LoadScene("Loading");
	}	

    public void DebugButton()
    {
		SceneManager.LoadScene("Debug");
	}

    public void OptionsButton()
    {
		SceneManager.LoadScene("OptionsState");
	}	   
}
