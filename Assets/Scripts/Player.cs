using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject gun;
    public GameObject shootPoint;
    public GameObject bullet;

    public float cooldown = 1f;
    private float cd;

    // Start is called before the first frame update
    void Start()
    {
        cd = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;

        gun.transform.up = worldPosition;

        cd -= Time.deltaTime;
        if (cd <= 0) Shoot();
    }

    private void Shoot()
    {
        Instantiate(bullet, shootPoint.transform.position, gun.transform.rotation);
        cd = cooldown;
    }
}
