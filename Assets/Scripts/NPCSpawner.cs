using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField]
    List<GameObject> npcPrefabs;
    [SerializeField]
    List<Transform> spawnPoints;
    [SerializeField]
    List<Transform> workStations;
    [SerializeField]
    List<Transform> points;
    [SerializeField]
    int amoutOfNPC = 6;
    [SerializeField]
    GameObject player;
    [SerializeField]
    Transform boss;
    [SerializeField]
    TMP_Text numberOfWorkers;

    [SerializeField]
    float workMeter;
    [SerializeField]
    float workDecreaser;
    [SerializeField]
    float workStationDecreaser;
    [SerializeField]
    float kitchenDecreaser;
    [SerializeField]
    float temperatureDecreaser;
    [SerializeField]
    float allDecreaser;
    private void Awake()
    {
        numberOfWorkers.text = amoutOfNPC.ToString();

        for (int i = 0; i < amoutOfNPC; i++)
        {
            int rnd = Random.Range(0,npcPrefabs.Count);
            int rndSpawn=Random.Range(0,spawnPoints.Count);
            GameObject npc = Instantiate(npcPrefabs[rnd], spawnPoints[rndSpawn].position, new Quaternion(0,0,0,0));
            NPCNav npcnav = npc.GetComponent<NPCNav>();
            npcnav.workStation = workStations[i];
            npcnav.points = points;
            npcnav.boss=boss;
            workStations[i].GetComponentInParent<SliderController>().assignedNpc = npc;
            npc.GetComponent<NPCVision>().player = player;
            NPCSystem system=npc.GetComponent<NPCSystem>();
            system.workMeter = workMeter;
            system.workDecreaser = workDecreaser;
            system.workStationDecreaser = workStationDecreaser;
            system.kitchenDecreaser = kitchenDecreaser;
            system.temperatureDecreaser = temperatureDecreaser;
            system.allDecreaser = allDecreaser;
            int rndType = Random.Range(0, 3);
            switch(rndType)
            {
                case 0:
                    system.affectedType=Constants.Type.work; break;
                case 1:
                    system.affectedType=Constants.Type.kitchen; break;
                case 2:
                    system.affectedType = Constants.Type.temperature; break;
            }

        }
    }


}