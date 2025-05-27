using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCNav : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    float radius = 2f;
    public List<Transform> points;
    public Transform workStation;
    public Transform boss;

    public bool alerted=false;
    private Vector2 actDest;
    private Coroutine coroutine;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    int workTime = 0;
    int index = 0;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        setNewPos();
    }
    public void Update()
    {
        if (alerted)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
                agent.isStopped = false;
            }
            if (Vector2.Distance(this.transform.position, boss.position) < 1)
            {
                agent.isStopped = true;
                boss.GetComponent<BossNav>().onChase = true;
            }
            else
            {

                agent.SetDestination(boss.position);

            }
            
        }
        if (Vector2.Distance(this.transform.position, actDest) < 0.5 && !alerted && !agent.isStopped)
        {
            coroutine=StartCoroutine(waiter());
        }
    }
    private void FixedUpdate()
    {
        if (agent.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        if(agent.velocity.x>0)
        {
            spriteRenderer.flipX=false;
        }
        animator.SetFloat("Velocity", agent.velocity.magnitude);
    }
    void setNewPos()
    {
        agent.isStopped = false;

        if (points == null || points.Count == 0){
            Debug.LogError("Lista points jest pusta!!!");
            return;
        }

        if(workTime>5)
        {
            workTime = 5;
        }
        int rnd = Random.Range(0, 5-workTime);
        Debug.Log("RND"+rnd);
        if(rnd == 0)
        {
            Debug.Log("INDEX"+index);
            workTime = 0;
            int randomPoint = Random.Range(0, points.Count);
            Vector2 randomCircle = Random.insideUnitCircle * radius;
            Vector2 randomOffset = new Vector2(randomCircle.x, randomCircle.y);
            Vector2 destPoint = new Vector2(points[randomPoint].position.x, points[randomPoint].position.y) + randomOffset;
            agent.SetDestination(destPoint);

            actDest = destPoint;
            index++;
            if (index > points.Count - 1)
                index = 0;          
            Debug.Log(index);

        }
        else
        {
            agent.SetDestination(workStation.position);
            workTime++;
            actDest = new Vector2(workStation.transform.position.x,workStation.transform.position.y);
        }
       
        
        
    }
    IEnumerator waiter()
    {
        Debug.Log("STARTED COROUTINE");
        int rnd = Random.Range(0, 25);
        agent.isStopped = true;
        Debug.Log("TIME" + rnd);
        yield return new WaitForSeconds(rnd);
        setNewPos();
    }

}
