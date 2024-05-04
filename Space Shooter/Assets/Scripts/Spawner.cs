using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private int numEnemiesToSpawn;
    [SerializeField] private float spawnDelay;
    [SerializeField] private float ySpawn;
    [SerializeField] private int numKilledForPowerUp;

    [SerializeField] private GameObject powerUp;
    [SerializeField] private GameObject spawnerToActivate;
    private Transform waypointFather;

    private int enemiesSpawning = 0;
    private int timesSpawned = 0;
    [HideInInspector] public int enemiesKilled;

    [SerializeField] private float bossPowerUpsToDrop;

    private void Start()
    {
        enemiesKilled = 0;
        waypointFather = transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= ySpawn && enemiesSpawning < numEnemiesToSpawn)
        {
            for (float i = 0; i < numEnemiesToSpawn; i++)
            {
                Invoke("Spawn", i * spawnDelay);
                enemiesSpawning += 1;
            }
        }

        if (timesSpawned >= numEnemiesToSpawn && transform.childCount <= 1)
        {
            if (spawnerToActivate != null)
            {
                spawnerToActivate.SetActive(true);
            }
            Destroy(gameObject);
        }
    }

    void Spawn()
    {
        GameObject enemy = Instantiate(enemyToSpawn, transform.position, transform.rotation);
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemy.transform.parent = transform;
            enemyScript.waypointFather = waypointFather;
            enemyScript.spawner = this;
        }
        Boss bossScript = enemy.GetComponent<Boss>();
        if (bossScript != null)
        {
            bossScript.spawnerToActivate = spawnerToActivate;
            bossScript.powerUpsToDrop = bossPowerUpsToDrop;
        }

        timesSpawned += 1;
    }

    public void EnemyDied(Transform location)
    {
        enemiesKilled += 1;
        if (enemiesKilled == numKilledForPowerUp)
        {
            Instantiate(powerUp, location.position, Quaternion.identity);
        }
    }
}
