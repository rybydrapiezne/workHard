using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCNav : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    List<Transform> points;

    private Transform actDest;
    int index = 0;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(points[index].position);
        agent.updateRotation = false;
        actDest = points[index];
    }
    public void Update()
    {
        if (Vector2.Distance(this.transform.position, actDest.position) < 0.5)
        {
            setNewPos();
        }
    }
    void setNewPos()
    {
      index++;
      if(index>points.Count-1)
        index= 0;
      agent.SetDestination(points[index].position);
      actDest = points[index];
    }

}
