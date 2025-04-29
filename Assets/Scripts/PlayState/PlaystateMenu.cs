using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlaystateMenu : MonoBehaviour
{
    public GameObject MainCanvas;
	public GameObject HUDCanvas;
    public Text loadedSongText;
    public GameObject displayText;
    private bool checkComplete = false;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        string loadedSongPrefix = "Loaded Song: ";
        if (loadedSongText.text.StartsWith(loadedSongPrefix) && loadedSongText.text.Length > loadedSongPrefix.Length)
        {
            MainCanvas.SetActive(false);
			HUDCanvas.SetActive(true);
        }
        else
        {
            StartCoroutine(DisplayMessageForFiveSeconds());
        }
    }

    IEnumerator DisplayMessageForFiveSeconds()
    {
        displayText.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        displayText.SetActive(false);
    }

    void Update()
    {
        if (!checkComplete && loadedSongText.text.StartsWith("Loaded Song: "))
        {
            checkComplete = true;
        }
    }
}
