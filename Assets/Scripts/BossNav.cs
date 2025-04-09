using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossNav : MonoBehaviour
{
    [SerializeField]
    Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public bool onChase { set; private get; } = false;
 
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
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
        if(onChase)
        {
            agent.SetDestination(player.position);
        }
    }
}
