using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float damage;
    public float maxHealth;
    public float health;

    public float cooldown;
    private float nextShootTime;
    public GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameMaster gameMaster;
    [SerializeField] private float yOffset;

    [SerializeField] HealthBar healthBar;

    private void Start()
    {
        nextShootTime = Time.time;
        health = maxHealth;

        healthBar.maxHealth = maxHealth;
        healthBar.health = health;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            worldPosition.y += yOffset;
            transform.position = Vector3.MoveTowards(transform.position, worldPosition, speed * Time.deltaTime);

            if (nextShootTime <= Time.time)
            {
                nextShootTime = Time.time + cooldown;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.damage = damage;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.health -= amount;
        healthBar.HealthBarFiller();
        Vibrator.Vibrate(500);
        CheckIfDead();
    }

    void CheckIfDead()
    {
        if (health <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        gameMaster.PlayerDied();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            TakeDamage(enemy.health);
            enemy.TakeDamage(Mathf.Infinity);
            CheckIfDead();
        }
        else if (collision.tag == "Boss")
        {
            TakeDamage(Mathf.Infinity);
        }
        else if (collision.tag == "Weapon Power Up")
        {
            cooldown = cooldown * 0.9f;
        }
    }
}
