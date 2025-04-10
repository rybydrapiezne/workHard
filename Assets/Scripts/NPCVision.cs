using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVision : MonoBehaviour
{
    public float visionDistance = 1f;

    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    LayerMask layer;
    private NPCNav npcnav;
    private void Start()
    {
        npcnav = GetComponent<NPCNav>();
    }
    private void Update()
    {
        if (isPlayerVisibleDisrupting())
        {
            npcnav.alerted = true;
            canvas.SetActive(true);
        }

    }

    private bool isPlayerVisibleDisrupting()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, visionDistance);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                if (hit.collider.TryGetComponent(out PlayerInteraction playerinteraction))
                {
                    if (playerinteraction.disrupting == true)
                    {
                        return true;
                    }
                    else
                    { return false; }
                }
            }
            else
            {
                //Debug.Log("NotPlayer:");
            }
        }
        return false;
    }

    void OnDrawGizmos()
    {
        if (player == null) return;

        // Draw detection range circle (Scene View)
        Gizmos.color = Color.green;
        Vector3 position = transform.position;
        Gizmos.DrawWireSphere(new Vector3(position.x, position.y, 0), visionDistance);

        // Draw line to player (Game View during play mode)
        if (Application.isPlaying)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            float distance = Vector2.Distance(transform.position, player.transform.position);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance);

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.DrawLine(transform.position, player.transform.position, Color.red); // Line turns red if player is hit
            }
            else
            {
                Debug.DrawLine(transform.position, player.transform.position, Color.green); // Line stays green otherwise
            }
        }
    }
}
