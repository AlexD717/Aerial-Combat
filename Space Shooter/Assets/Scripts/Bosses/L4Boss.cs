using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L4Boss : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    [Header("Center")]
    [SerializeField] private Transform centerFirePoint;
    [SerializeField] private float centerCooldown;
    private float centerNextAttackTime;
    [SerializeField] private float centerDamage;
    [SerializeField] private float centerBulletSpeed;
    [SerializeField] private List<float> centerAngles;
    private int centerIndex = 0;

    [Header("Wings")]
    [SerializeField] private List<Transform> wingFirePoints;
    [SerializeField] private float wingCooldown;
    private float wingNextAttackTime;
    [SerializeField] private float wingDamage;
    [SerializeField] private float wingBulletSpeed;
    [SerializeField] private Vector2 wingAngles;

    private void Start()
    {
        centerNextAttackTime = Time.time + centerCooldown + 1f;
        wingNextAttackTime = Time.time + wingCooldown + 1f;
    }

    private void Update()
    {
        if (centerFirePoint != null)
        {
            if (Time.time > centerNextAttackTime)
            {
                centerNextAttackTime = Time.time + centerCooldown;
                FireMissle(centerFirePoint, Quaternion.Euler(0, 0, centerAngles[centerIndex]), centerDamage, centerBulletSpeed);
                centerIndex = (centerIndex + 1) % centerAngles.Count;
            }
        }
        if (Time.time > wingNextAttackTime)
        {
            foreach (Transform firepoint in wingFirePoints)
            {
                if (firepoint != null)
                {
                    float zRotation = Random.Range(wingAngles.x, wingAngles.y);
                    if (firepoint.localPosition.x < 0)
                    {
                        zRotation = -zRotation;
                    }
                    FireMissle(firepoint, Quaternion.Euler(0, 0, zRotation), wingDamage, wingBulletSpeed);
                }
            }
            wingNextAttackTime = Time.time + wingCooldown;
        }
    }

    private void FireMissle(Transform firepoint, Quaternion rotation, float damage, float speed)
    {
        GameObject missle = Instantiate(bulletPrefab, firepoint.position, rotation);
        EnemyBullet missleScript = missle.GetComponent<EnemyBullet>();
        missleScript.damage = damage;
        missleScript.speed = speed;
    }
}
