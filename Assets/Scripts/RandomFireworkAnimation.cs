using System.Collections;
using UnityEngine;

public class RandomAnimationStart : MonoBehaviour
{
    public Animator animator;
    public string animationTrigger = "Start";
    public float minDelay = 0f;
    public float maxDelay = 5f;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        StartCoroutine(StartAnimationWithDelay());
    }

    IEnumerator StartAnimationWithDelay()
    {
        float delay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds(delay);
        animator.SetTrigger(animationTrigger);
    }
}
