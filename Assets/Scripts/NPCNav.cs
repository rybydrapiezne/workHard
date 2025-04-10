using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCNav : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;
    public List<Transform> points;
    public Transform workStation;
    [SerializeField]
    Transform boss;

    public bool alerted=false;
    private Vector2 actDest;
    private Coroutine coroutine;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
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
        int rnd = Random.Range(0, 2);
        Debug.Log("RND"+rnd);
        if(rnd == 0)
        {
            float offsetx = Random.Range(0f, 1.5f);
            float offsety = Random.Range(0f, 1.5f);
            Vector2 destPos = new Vector2(points[index].position.x + offsetx, points[index].position.y + offsety);
            Debug.Log(destPos);
            agent.SetDestination(destPos);
            actDest = destPos;
            index++;
            if (index > points.Count - 1)
                index = 0;          
            Debug.Log(index);

        }
        else
        {
            agent.SetDestination(workStation.position);
            actDest = workStation.position;
        }
       
        
        
    }
    IEnumerator waiter()
    {
        Debug.Log("STARTED COROUTINE");
        int rnd = Random.Range(0, 15);
        agent.isStopped = true;
        Debug.Log("TIME" + rnd);
        yield return new WaitForSeconds(rnd);
        setNewPos();
    }

}
