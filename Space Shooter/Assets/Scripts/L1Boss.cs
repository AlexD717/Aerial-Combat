using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1Boss : MonoBehaviour
{
    [Header("Shooting Stuff")]
    [SerializeField] private float timeBetweenAttack;
    private float nextAttackTime;
    [SerializeField] private float wingWeaponCooldown;
    private float nextWingAttackTime;
    [SerializeField] private float timesToFireWingWeapon;
    private float timesFired;
    [SerializeField] private float wingWeaponDamage;
    [SerializeField] private float centerWeaponDamage;

    private int mainWeaponFire;

    [Header("Fill Out Once")]
    [SerializeField] private Transform firepointCenter;
    [SerializeField] private List<Transform> firepointWings;
    [SerializeField] private GameObject centerMissle;
    [SerializeField] private GameObject wingMissle;

    // Start is called before the first frame update
    void Start()
    {
        nextAttackTime = Time.time + timeBetweenAttack;
        nextWingAttackTime = Time.time;
        timesFired = 0;

        mainWeaponFire = Mathf.RoundToInt(timesToFireWingWeapon / 2);
    }

    // Update is called once per frame
    void Update()
    {
        // Weapon Fires
        if (nextAttackTime < Time.time)
        {
            if (timesFired < timesToFireWingWeapon)
            {
                if (Time.time > nextWingAttackTime)
                {
                    if (timesFired == mainWeaponFire && firepointCenter != null)
                    {
                        List<int> firepointRotations = new List<int>() { 60, 32, 0, -32, -60 };
                        foreach (int rotation in firepointRotations)
                        {
                            firepointCenter.rotation = Quaternion.Euler(0, 0, rotation);
                            GameObject missle = Instantiate(centerMissle, firepointCenter.position, firepointCenter.rotation);
                            EnemyBullet missleScript = missle.GetComponent<EnemyBullet>();
                            missleScript.damage = centerWeaponDamage;
                        }
                    }
                    else
                    {
                        foreach (Transform firepoint in firepointWings)
                        {
                            if (firepoint != null)
                            {
                                float randomRotation = 0;
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
                            }
                        }
                    }
                    timesFired += 1;
                    nextWingAttackTime = Time.time + wingWeaponCooldown;
                }
            }
            else
            {
                timesFired = 0;
                nextAttackTime = Time.time + timeBetweenAttack;
            }
        }
        
    }
}
