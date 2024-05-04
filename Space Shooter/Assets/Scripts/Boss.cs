using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float health;
    public float speed = 5.0f;
    public float rotationSpeed = 5.0f;
    [SerializeField] private Vector2 movePos;

    [SerializeField] GameObject explosion;
    [SerializeField] HealthBar healthBar;

    [SerializeField] private List<BossComponent> parts;

    private bool dead = false;

    [Header("Coins")]
    [SerializeField] private GameObject coinManager;
    [SerializeField] private float moneyToGiveOnDeath;
    [SerializeField] private Vector2 coinSpread;

    [SerializeField] private GameObject powerUp;
    [HideInInspector] public GameObject spawnerToActivate;
    [HideInInspector] public float powerUpsToDrop;

    // Start is called before the first frame update
    void Start()
    {
        GameMaster.totalEnemies += 1;
        Debug.Log(GameMaster.totalEnemies);
        foreach (BossComponent child in transform.GetComponentsInChildren<BossComponent>())
        {
            parts.Add(child);
        }

        healthBar.maxHealth = health;
        healthBar.health = health;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(0, 0) * Mathf.Rad2Deg + 180;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        float distanceToPos = Vector2.Distance(transform.position, movePos);
        if (distanceToPos > 0.02f)
        {
            transform.position = Vector2.MoveTowards(transform.position, movePos, speed * Time.deltaTime);
        }

        parts.Clear();
        foreach (BossComponent child in transform.GetComponentsInChildren<BossComponent>())
        {
            parts.Add(child);
        }
    }

    public void TakeDamage(float amount)
    {
        if (parts.Count <= 0)
        {
            health -= amount;
            healthBar.health -= amount;
            healthBar.HealthBarFiller();
            if (!dead && health <= 0)
            {
                dead = true;
                Killed();
            }
        }
    }

    void Killed()
    {
        if (spawnerToActivate != null)
        {
            spawnerToActivate.GetComponent<Spawner>().enabled = true;
            spawnerToActivate.GetComponent<Scroll>().enabled = true;
        }

        GameMaster.enemiesDestroyed += 1;
        Debug.Log(GameMaster.enemiesDestroyed);
        GameObject _explosion = Instantiate(explosion, transform.position, transform.rotation);
        CoinManager _coinManger = Instantiate(coinManager, transform.position, transform.rotation).GetComponent<CoinManager>();
        _coinManger.moneyToGive = moneyToGiveOnDeath;
        _coinManger.moneySpreadRange = coinSpread;
        _coinManger.GiveMoney();
        Destroy(_explosion, 2f);
        for (int i = 0; i < powerUpsToDrop; i++)
        {
            float minOffset = coinSpread.x;
            float maxOffset = coinSpread.y;
            Instantiate(powerUp, new Vector3(transform.position.x + Random.Range(minOffset, maxOffset), transform.position.y + Random.Range(minOffset, maxOffset), 0), Quaternion.identity);
        }
        //GameObject canvas = healthBar.transform.gameObject;
        //Destroy(canvas);
        Destroy(gameObject);
    }
}
