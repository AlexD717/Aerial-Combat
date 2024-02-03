using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossComponent : MonoBehaviour
{
    public float health;
    [SerializeField] GameObject explosion;
    [SerializeField] private HealthBar healthBar;

    private void Start()
    {
        healthBar.maxHealth = health;
        healthBar.health = health;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.health -= amount;
        healthBar.HealthBarFiller();
        if (health <= 0)
        {
            Killed();
        }
    }

    void Killed()
    {
        GameObject _explosion = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(_explosion, 2f);
        GameObject canvas = healthBar.transform.gameObject;
        Destroy(canvas);
        Destroy(gameObject);
    }
}
