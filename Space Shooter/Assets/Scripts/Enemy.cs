using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public float health;
    [SerializeField] private float maxHealth;
    public Transform waypointFather;
    public List<Transform> waypoints;
    public float speed = 5.0f;
    public float rotationSpeed = 5.0f;

    public bool lookAtPlayer;
    [SerializeField] GameObject explosion;
    private int currentWaypointIndex = 0;
    public Spawner spawner;
    private Transform player;

    [SerializeField] private bool useHealthBar;
    [SerializeField] private HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        GameMaster.totalEnemies += 1;

        foreach (Transform child in waypointFather)
        {
            waypoints.Add(child);
        }

        player = GameObject.Find("Player").transform;

        // Health Bar
        health = maxHealth;
        if (useHealthBar)
        {
            healthBar.maxHealth = health;
            healthBar.health = health;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWaypointIndex < waypoints.Count)
        {
            float distanceToWaypoint = Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position);
            if (distanceToWaypoint <= 0.02f)
            {
                currentWaypointIndex++;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);
                Vector3 direction = waypoints[currentWaypointIndex].position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 270;

                if (lookAtPlayer)
                {
                    direction = player.position - transform.position;
                    angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
                }

                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (useHealthBar)
        {
            healthBar.health -= amount;
            healthBar.HealthBarFiller();
        }
        if (health <= 0)
        {
            Killed();
        }
    }

    void Killed()
    {
        GameMaster.enemiesDestroyed += 1;
        Debug.Log(GameMaster.enemiesDestroyed);
        spawner.EnemyDied(transform);
        GameObject _explosion = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(_explosion, 2f);
        Destroy(gameObject);
    }
}
