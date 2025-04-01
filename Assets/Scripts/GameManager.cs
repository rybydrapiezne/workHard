using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> npcs;
    [SerializeField]
    GameObject boss;
    [SerializeField]
    GameObject player;

    private void Update()
    {
        if(player.TryGetComponent(out PlayerController playerController))
        {
            if (playerController.fired)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
