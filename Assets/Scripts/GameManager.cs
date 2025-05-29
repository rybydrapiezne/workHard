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
    int numberOfWorkers = 5;
    int firedWorkersCount=0;  
    private void Start()
    {
        BossNav.onWorkerFired += updateUI;
    }

    private void updateUI(BossNav bossNav)
    {
        firedWorkersCount++;
        firedWorkers.text=firedWorkersCount.ToString();
    }

    private void Update()
    {
        if (player.TryGetComponent(out PlayerController playerController))
        {
            if (playerController.fired)
            {
                EndSceneScript.sceneId = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(5);
            }
        }
        if (numberOfWorkers == firedWorkersCount)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
  
}
