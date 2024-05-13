using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0) return agent.transform.up;

        Vector2 alignmentMove = Vector2.zero;
        foreach (Transform t in context)
        {
            alignmentMove += (Vector2)t.up;
        }
        alignmentMove /= context.Count;

        return alignmentMove;
    }
}
