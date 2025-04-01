using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossNav : MonoBehaviour
{
    [SerializeField]
    Transform player;
    private NavMeshAgent agent;
    public bool onChase { set; private get; } = false;
 
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if(onChase)
        {
            agent.SetDestination(player.position);
        }
    }
}
