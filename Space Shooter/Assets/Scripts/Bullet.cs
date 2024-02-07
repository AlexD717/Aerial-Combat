using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;

    [SerializeField] private Sprite[] bulletColors;
    private int bulletColorInt;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bulletColorInt = PlayerPrefs.GetInt("ShipColorInt", 0);
        spriteRenderer.sprite = bulletColors[bulletColorInt];

        Invoke("Death", 2f);    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(damage);

            Death();
        }
        else if (collision.tag == "Boss Component")
        {
            BossComponent bossComponent = collision.GetComponent<BossComponent>();
            bossComponent.TakeDamage(damage);

            Death();
        }
        else if (collision.tag == "Boss")
        {
            Boss boss = collision.GetComponent<Boss>();
            boss.TakeDamage(damage);

            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
