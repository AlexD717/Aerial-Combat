using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3Boss : MonoBehaviour
{
    [Header("Shooting Stuff")]
    [SerializeField] private float wingWeaponCooldown;
    [SerializeField] private float wingWeaponDamage;
    [SerializeField] private float wingBulletSpeed;
    [SerializeField] private float[] wingWeaponRotations;
    private int currentWingRotationIndex = 0;

    [Header("Fill Out Once")]
    [SerializeField] private Transform firepointRight;
    [SerializeField] private Transform firepointLeft;
    [SerializeField] private Transform firepointSides;
    [SerializeField] private GameObject wingMissle;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("WingAttack", 1.5f, wingWeaponCooldown);
        InvokeRepeating("SideAttack", 1f, wingWeaponCooldown + 0.5f);
    }

    private void WingAttack()
    {
        if (firepointRight != null)
        {
            GameObject missle = Instantiate(wingMissle, firepointRight.position, Quaternion.Euler(0, 0, wingWeaponRotations[currentWingRotationIndex]));
            EnemyBullet missleScript = missle.GetComponent<EnemyBullet>();
            missleScript.damage = wingWeaponDamage;
            missleScript.speed = wingBulletSpeed;
        }
        if (firepointLeft != null)
        {
            GameObject missle = Instantiate(wingMissle, firepointLeft.position, Quaternion.Euler(0, 0, -wingWeaponRotations[currentWingRotationIndex]));
            EnemyBullet missleScript = missle.GetComponent<EnemyBullet>();
            missleScript.damage = wingWeaponDamage;
            missleScript.speed = wingBulletSpeed;
        }

        currentWingRotationIndex = (currentWingRotationIndex + 1) % wingWeaponRotations.Length;
    }

    private void SideAttack()
    {
        foreach (Transform firepoint in firepointSides)
        {
            if (firepoint != null)
            {
                float randomRotation = 0f;
                if (firepoint.position.x > 0)
                {
                    randomRotation = Random.Range(-2, 2);
                }
                else
                {
                    randomRotation = Random.Range(2, -2);
                }
                firepoint.rotation = Quaternion.Euler(0, 0, randomRotation);
                GameObject missle = Instantiate(wingMissle, firepoint.position, firepoint.rotation);
                EnemyBullet missleScript = missle.GetComponent<EnemyBullet>();
                missleScript.damage = wingWeaponDamage;
                missleScript.speed = wingBulletSpeed;
            }
        }
    }
}
