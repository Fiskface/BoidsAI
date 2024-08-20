using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject gun;
    public GameObject shootPoint;
    public GameObject bullet;
    public TextMeshProUGUI healthUI;

    public float cooldown = 1f;
    private float cd;
    private int health = 5;

    private void OnEnable()
    {
        LevelSystem.upgraded += Upgrade;
    }

    private void OnDisable()
    {
        LevelSystem.upgraded -= Upgrade;
    }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            LoseHealth();
        }
    }

    private void LoseHealth()
    {
        health--;
        healthUI.text = health.ToString();

        if(health <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        EventBus.gameOver?.Invoke();
    }

    private void Upgrade()
    {
        cooldown *= 0.5f;
    }

    public void GainHealth(int amount)
    {
        health += amount;
    }
}
