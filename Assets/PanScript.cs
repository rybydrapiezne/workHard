using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanScript : MonoBehaviour
{
    public float moveSpeed = 2f;         // Units per second
    public float sideLength = 17f;        // Length of each side of the square

    private Rigidbody2D rb;
    private Vector2[] directions = new Vector2[]
    {
        Vector2.right,
        Vector2.down,
        Vector2.left,
        Vector2.up
    };

    private int currentDirectionIndex = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(MoveInSquare());
    }

    private System.Collections.IEnumerator MoveInSquare()
    {
        while (true)
        {
            Vector2 direction = directions[currentDirectionIndex];
            float moved = 0f;

            while (moved < sideLength)
            {
                float step = moveSpeed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + direction * step);
                moved += step;
                yield return new WaitForFixedUpdate();
            }

            currentDirectionIndex = (currentDirectionIndex + 1) % directions.Length;
        }
    }
}
