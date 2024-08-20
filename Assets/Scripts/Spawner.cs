using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnThis;

    private float timeBetweenSpawns = 5;
    private float timer = 0;
    private int counter = 0;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            for (int i = 0; i < counter; i++)
            {
                Spawn();
            }
            counter++;
            timer = timeBetweenSpawns;
        }
    }

    private void Spawn()
    {
        var a = Random.insideUnitCircle.normalized * 21;

        Instantiate(spawnThis, a, Quaternion.Euler(0, 0, Random.Range(0, 360)));
    }
}