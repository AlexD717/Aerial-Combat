using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float cooldown;
    [SerializeField] private float bulletDamage;

    private float nextFireTime;
    [SerializeField] private Transform firepoint;

    private void Start()
    {
        nextFireTime = Time.time + cooldown;
    }

    private void Update()
    {
        if (nextFireTime <= Time.time)
        {
            nextFireTime = Time.time + cooldown;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
        bulletScript.damage = bulletDamage;
    }
}
