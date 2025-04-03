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
    [SerializeField]
    Transform boss;

    public bool alerted=false;
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
        if (alerted)
        {
            if (Vector2.Distance(this.transform.position, boss.position) < 1)
            {
                agent.isStopped = true;
                boss.GetComponent<BossNav>().onChase = true;
            }
            else
                agent.SetDestination(boss.position);
        }
        if (Vector2.Distance(this.transform.position, actDest.position) < 0.5 && !alerted && !agent.isStopped)
        {
            StartCoroutine(waiter());
        }
    }
    void setNewPos()
    {
        agent.isStopped = false;
      index++;
      if(index>points.Count-1)
        index= 0;
      agent.SetDestination(points[index].position);
      actDest = points[index];
    }
    IEnumerator waiter()
    {
        int rnd = Random.Range(0, 15);
        agent.isStopped = true;
        yield return new WaitForSeconds(rnd);
        setNewPos();
    }

}
