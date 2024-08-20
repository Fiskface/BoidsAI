using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOne : MonoBehaviour
{
    private Collider2D col;
    private Vector3 move;
    private Vector3 velocity;
    private int angle;
    public float maxSpeed = 5f;
    public int visionRange = 135;
    public LayerMask layerHit;

    public float viewRange = 3f;

    [Header("Alignment")]
    public float alignmentMultiplier = 1f;

    [Header("Cohesion")]
    public float cohesionMultiplier = 1f;

    [Header("Separation")]
    public float separationMultiplier = 1f;

    [Header("Avoidance")]
    public int rayCount = 10;
    public float avoidanceMultiplier = 1f;

    [Header("MoveToPlayer")]
    public float moveToPlayerMultiplier = 1f;

    [Header("Wanderer")]
    public float wandererMultiplier = 1f;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        velocity = Vector3.zero;

        velocity = transform.up * maxSpeed;
    }

    private void Update()
    {
        move = alignmentMultiplier * Time.deltaTime * Align();
        move += cohesionMultiplier * Time.deltaTime * Cohere();
        move += separationMultiplier * Time.deltaTime * Separate();
        move += avoidanceMultiplier * Time.deltaTime * Avoid();
        move += moveToPlayerMultiplier * Time.deltaTime * MoveToPlayer();
        move += wandererMultiplier * Time.deltaTime * Wander();
    }

    private void LateUpdate()
    {
        velocity += move;

        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        transform.up = velocity;
        transform.position += velocity * Time.deltaTime;

        velocity.z = 0;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
    }

    private Vector3 Align()
    {
        int count = 0;
        Vector3 alignMove = Vector3.zero;
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(transform.position, viewRange);
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
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(transform.position, viewRange);
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
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(transform.position, viewRange);
        foreach (Collider2D c in contextColliders)
        {
            if (c != col && col.CompareTag("Enemy")
                && Vector2.Angle(transform.up, c.transform.position - transform.position) < visionRange)
            {
                count++;
                var dir = transform.position - c.transform.position;
                float distance = dir.magnitude;
                dir = dir.normalized;

                separateMove += dir * (viewRange - distance);
            }
        }
        if (count == 0) return Vector3.zero;

        //separateMove /= count;
        return separateMove;
    }

    private Vector3 Avoid()
    {
        Vector3 reverseDir = Vector3.zero;
        var range = visionRange * 2;
        float anglePerAmount = range / (rayCount - 1f);
        for (int i = 0; i < rayCount; i++)
        {
            Vector3 direction = Quaternion.AngleAxis(-(float)range * 0.5f + anglePerAmount * (float)i, Vector3.forward) * transform.up;

            var hit = Physics2D.Raycast(transform.position, direction, viewRange, layerHit);
            if (hit)
            {
                reverseDir += -direction.normalized * (range - hit.distance);
            }
        }
        return reverseDir;
    }

    private Vector3 MoveToPlayer()
    {
        if (transform.position.magnitude > 1)
            return -transform.position;
        else return transform.position.normalized * -1;
    }

    private Vector3 Wander()
    {
        var vel = transform.up.normalized * 1.5f;
        vel += new Vector3(Mathf.Cos(angle * 10), Mathf.Sin(angle * 10), 0).normalized;

        return vel;
    }

    private IEnumerator ree()
    {
        while (true)
        {
            for (int i = 0; i < 30; i++)
            {
                yield return null;
            }
            if (Random.Range(0, 2) == 0) angle++;
            else angle--;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 reverseDir = Vector3.zero;
        var range = visionRange * 2;
        float anglePerAmount = range / (rayCount - 1f);
        for (int i = 0; i < rayCount; i++)
        {
            Vector3 direction = Quaternion.AngleAxis(-(float)range * 0.5f + anglePerAmount * (float)i, Vector3.forward) * transform.up;

            Gizmos.DrawLine(transform.position, transform.position + direction * viewRange);
        }
    }
}
