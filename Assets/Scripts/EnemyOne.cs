using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOne : MonoBehaviour
{
    [Header("Alignment")]
    public float alignmentLocalSize = 3f;
    public float alignmentMultiplier = 1f;

    [Header("Cohesion")]
    public float cohesionLocalSize = 3f;
    public float cohesionMultiplier = 1f;

    [Header("Separation")]
    public float separationLocalSize = 3f;
    public float separationMultiplier = 1f;

    private void Update()
    {
        /*
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach (Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider) context.Add(c.transform);
        }
        */
    }

    private void Align()
    {

    }

    private void Cohere()
    {

    }

    private void Separate()
    {

    }
}
