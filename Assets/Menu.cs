using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSourceOnClick;
    [SerializeField]
    AudioSource audioSourceOnHover;
    [SerializeField]
    GameObject loadingScreen;
    [SerializeField]
    GameObject mainMenuScreen;
    [SerializeField]
    Slider loadingBarFill;
    public void NewGame()
    {
        audioSourceOnClick.Play();
        loadingScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
        StartCoroutine(LoadAsyncScene(1));
    }
    public void Continue()
    {
        audioSourceOnClick.Play();

    }
    public void Options()
    {
        audioSourceOnClick.Play();

    }
    public void Exit()
    {
        audioSourceOnClick.Play();

    }

    IEnumerator LoadAsyncScene(int sceneId)
    {
        AsyncOperation operation=SceneManager.LoadSceneAsync(sceneId);

        while(!operation.isDone) 
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.value = progressValue;
            yield return null;

        }
    }
}
