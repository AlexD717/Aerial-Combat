using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2Boss : MonoBehaviour
{
    [Header("Shooting Stuff")]
    [SerializeField] private float wingWeaponCooldown;
    [SerializeField] private float wingWeaponDamage;
    [SerializeField] private float[] wingWeaponRotations;
    private int currentWingRotationIndex = 0;

    [Header("Fill Out Once")]
    [SerializeField] private List<Transform> firepointWings;
    [SerializeField] private GameObject wingMissle;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("WingAttack", 2f, wingWeaponCooldown);
    }

    private void WingAttack()
    {
        foreach (Transform firepoint in firepointWings)
        {
            if (firepoint != null)
            {
                GameObject missle = Instantiate(wingMissle, firepoint.position, Quaternion.Euler(0, 0, wingWeaponRotations[currentWingRotationIndex]));
                EnemyBullet missleScript = missle.GetComponent<EnemyBullet>();
                missleScript.damage = wingWeaponDamage;
            }
        }

        currentWingRotationIndex = (currentWingRotationIndex + 1) % wingWeaponRotations.Length;

    }
}
