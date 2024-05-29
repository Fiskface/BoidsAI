using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    public LayerMask mask;

    public List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        foreach (Transform t in original)
        {
            if(mask == (mask | (1 << t.gameObject.layer)))
            {
                filtered.Add(t);
            }
        }
        return filtered;
    }
}
