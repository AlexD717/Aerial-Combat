using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public float damage;

    private void Start()
    {
        Invoke("Death", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.up * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            player.TakeDamage(damage);

            Destroy(gameObject);
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
