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
    public static Action<BossNav> onWorkerFired;
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
                if(!firing)
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
        Debug.Log("Firing process started");

        while (workersToFire.Count > 0)
        {
            currWorker = workersToFire[0];
            Debug.Log("Firing " + currWorker.name);

            while (Vector2.Distance(transform.position, currWorker.position) > 0.5f)
            {
                Debug.Log("Moving towards " + currWorker.name);
                agent.SetDestination(currWorker.position);
                yield return null;
            }
            Debug.Log("Reached " + currWorker.name);
            agent.isStopped = true;
            yield return new WaitForSeconds(1f);

            onWorkerFired?.Invoke(this);
            Destroy(currWorker.gameObject);
            Debug.Log("Fired " + currWorker.name);
            workersToFire.RemoveAt(0);
            Debug.Log("Remaining workers to fire: " + workersToFire.Count);
            agent.isStopped = false;
        }
        Debug.Log("Firing process completed");
        firing = false;
    }
}
