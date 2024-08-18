using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOne : MonoBehaviour
{
    private Collider2D col;
    private Vector3 move;
    private Vector3 velocity;
    public int visionRange = 135;

    [Header("Alignment")]
    public float alignmentViewRange = 3f;
    public float alignmentMultiplier = 1f;

    [Header("Cohesion")]
    public float cohesionViewRange = 3f;
    public float cohesionMultiplier = 1f;

    [Header("Separation")]
    public float separationViewRange = 3f;
    public float separationMultiplier = 1f;

    [Header("Avoidance")]
    public int rayCount = 10;
    public float avoidanceViewRange = 3f;
    public float avoidanceMultiplier = 1f;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        velocity = Vector3.zero;
    }

    private void Update()
    {
        move = Vector3.zero;
        move = alignmentMultiplier * Time.deltaTime * Align();
        move += cohesionMultiplier * Time.deltaTime * Cohere();
        move += separationMultiplier * Time.deltaTime * Separate();
    }

    private void LateUpdate()
    {
        velocity += move;
        transform.up = velocity;
        transform.position += velocity;
    }

    private Vector3 Align()
    {
        int count = 0;
        Vector3 alignMove = Vector3.zero;
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(transform.position, alignmentViewRange);
        foreach (Collider2D c in contextColliders)
        {
            if (c != col && col.CompareTag("Enemy") 
                && Vector2.Angle(transform.up, c.transform.position - transform.position) < visionRange)
            {
                count++;
                alignMove += c.transform.up;
            }
        }
        if (count == 0) return transform.up;
        alignMove /= count;
        return alignMove;
    }

    private Vector3 Cohere()
    {
        int count = 0;
        Vector3 cohereMove = Vector2.zero;
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(transform.position, cohesionViewRange);
        foreach (Collider2D c in contextColliders)
        {
            if (c != col && col.CompareTag("Enemy")
                && Vector2.Angle(transform.up, c.transform.position - transform.position) < visionRange)
            {
                count++;
                cohereMove += c.transform.position;
            }
        }
        if (count == 0) return Vector3.zero;

        cohereMove /= count;

        cohereMove -= transform.position;
        return cohereMove;
    }

    private Vector3 Separate()
    {
        int count = 0;
        Vector3 separateMove = Vector2.zero;
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(transform.position, cohesionViewRange);
        foreach (Collider2D c in contextColliders)
        {
            if (c != col && col.CompareTag("Enemy")
                && Vector2.Angle(transform.up, c.transform.position - transform.position) < visionRange)
            {
                count++;
                var dir = transform.position - c.transform.position;
                float distance = dir.magnitude;
                dir = dir.normalized;

                separateMove += dir * (separationViewRange - distance);
            }
        }
        if (count == 0) return Vector3.zero;

        //separateMove /= count;
        return separateMove;
    }

    private Vector3 Avoid()
    {
        for (int i = 0; i < rayCount; i++)
        {

        }
        return Vector3.zero;
    }
}
