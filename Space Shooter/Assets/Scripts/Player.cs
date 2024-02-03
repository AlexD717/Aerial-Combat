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
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameMaster gameMaster;
    [SerializeField] private float yOffset;

    [SerializeField] HealthBar healthBar;

    private int level = 1;

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

    private void Shoot()
    {
        if (level >= 10)
        {
            Vector3 offset = new Vector3(0.15f, 0, 0);
            FireWeapon(bulletPrefab, firePoint.position, firePoint.rotation);
            FireWeapon(bulletPrefab, firePoint.position + offset, firePoint.rotation);
            FireWeapon(bulletPrefab, firePoint.position - offset, firePoint.rotation);
        }
        else if (level >= 5)
        {
            Vector3 offset = new Vector3(0.1f, 0, 0);
            FireWeapon(bulletPrefab, firePoint.position + offset, firePoint.rotation);
            FireWeapon(bulletPrefab, firePoint.position - offset, firePoint.rotation);
        }
        else
        {
            FireWeapon(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    private void FireWeapon(GameObject bulletPrefab, Vector3 bulletPosition, Quaternion bulletRotation)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletPosition, bulletRotation);
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
            level += 1;
            cooldown = cooldown * 0.9f;
        }
    }
}
