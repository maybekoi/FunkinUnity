using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoad : MonoBehaviour
{
    public Text loadingText;

    void Start()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadAsyncScene(nextSceneIndex));
    }

    IEnumerator LoadAsyncScene(int sceneIndex)
    {
        loadingText.text = "Loading...";
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            loadingText.text = "Loading... " + (asyncOperation.progress * 100) + "%";

            if (asyncOperation.progress >= 0.9f)
            {
                loadingText.text = "Press any key to continue";
                if (Input.anyKeyDown)
                {
                    asyncOperation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
