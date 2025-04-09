using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField]
    List<Transform> workStations;
    [SerializeField]
    Transform Kitchen;
    [SerializeField]
    Transform Toilet;
    [SerializeField]
    Transform bossRoom;
    [SerializeField]
    TMP_Text numberOfWorkers;
    [SerializeField]
    TMP_Text firedWorkers;
    private void Awake()
    {
        numberOfWorkers.text = npcs.Count.ToString();
        int index = 0;
        foreach (var npc in npcs)
        {
            Transform desk = workStations[index];
            NPCNav currNpc=npc.GetComponent<NPCNav>();
            currNpc.workStation=desk;
            currNpc.points.Add(Kitchen);
            currNpc.points.Add(Toilet);
            currNpc.points.Add(bossRoom);
            index++;
            if (index>workStations.Count-1)
            {
                break;
            }
            
        }
    }
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
