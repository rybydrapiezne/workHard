using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossNav : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    Transform BossPoint;
    private NavMeshAgent agent;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public bool onChase { set; private get; } = false;
    public List<Transform> workersToFire;
    private bool done=false;
    private bool firing = false;
    Transform currWorker;
    Coroutine coroutine;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        NPCSystem.onWorkMeterDepleted += addToList;
    }
    private void OnDestroy()
    {
        NPCSystem.onWorkMeterDepleted -= addToList;
    }

    private void addToList(NPCSystem system)
    {
        workersToFire.Add(system.gameObject.transform);
    }

    private void FixedUpdate()
    {
        if (agent.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (agent.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        animator.SetFloat("Velocity", agent.velocity.magnitude);
    }
    private void Update()
    {
        if (onChase)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            this.GetComponent<BoxCollider2D>().enabled = true;
            agent.SetDestination(player.position);
        }
        else
        {
            if (workersToFire.Count != 0)
            {
                if (!firing)
                    coroutine=StartCoroutine(firingProcess());
            }
            else
            {
                agent.SetDestination(BossPoint.position);
            }
        }
        
       
    }
    private IEnumerator firingProcess()
    {
        firing = true;

        while (workersToFire.Count > 0)
        {
            currWorker = workersToFire[0];

            while (Vector2.Distance(transform.position, currWorker.position) > 0.5f)
            {
                agent.SetDestination(currWorker.position);
                yield return null;
            }
            agent.isStopped = true;
            yield return new WaitForSeconds(1f);

            Destroy(currWorker.gameObject);
            workersToFire.RemoveAt(0);

            agent.isStopped = false;
        }

        firing = false;
    }
}
