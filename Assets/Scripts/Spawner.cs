using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnThis;
    public int spawnCount;

    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(spawnThis, Random.insideUnitCircle * 3, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
