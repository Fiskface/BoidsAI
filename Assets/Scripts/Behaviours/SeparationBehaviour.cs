using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class SeparationBehaviour : FlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0) return Vector2.zero;

        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;
        foreach (Transform t in context)
        {
            var a = t.position - agent.transform.position;
            if (Vector2.SqrMagnitude(a) <= flock.SquareAvoidanceRadius)
            {
                //nAvoid++;
                avoidanceMove += (Vector2)(agent.transform.position - t.position) * (1 - Vector2.SqrMagnitude(a)/flock.SquareAvoidanceRadius);
            }
            
        }
        //if (nAvoid > 0) avoidanceMove /= nAvoid;

        return avoidanceMove;
    }
}
