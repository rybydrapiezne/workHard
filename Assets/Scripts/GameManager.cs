using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    TMP_Text firedWorkers;
    [SerializeField]
    TMP_Text amoutOfWorkers;
    int firedWorkersCount=0;  
    private void Start()
    {
        BossNav.onWorkerFired += updateUI;
    }

    private void updateUI(BossNav bossNav)
    {
        firedWorkersCount++;
        int x=0;
        Int32.TryParse(amoutOfWorkers.text, out x);
        firedWorkers.text=firedWorkersCount.ToString();
        if (firedWorkersCount == x)
        {
            nextScene();
        }
    }

    private void Update()
    {
        if(player.TryGetComponent(out PlayerController playerController))
        {
            if (playerController.fired)
            {
                SceneManager.LoadScene(2);
            }
        }
    }
    private void nextScene()
    {
        StartCoroutine(LoadAsyncScene(SceneManager.GetActiveScene().buildIndex+1));
    }
    IEnumerator LoadAsyncScene(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        while (!operation.isDone)
        {
            yield return null;

        }
    }

}
