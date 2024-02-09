using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float moneyToGive;
    private float extraMoney;

    private void Start()
    {
        GameObject coinScroller = GameObject.Find("CoinScroller");
        if (coinScroller != null)
        {
            transform.parent = coinScroller.transform;
        }

        extraMoney = PlayerPrefs.GetFloat("extraMoney", 0f);
        moneyToGive += extraMoney;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CoinMaster.coinsCollected += moneyToGive;
            Debug.Log(CoinMaster.coinsCollected);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (transform.position.y <= -1.5f)
        {
            Destroy(gameObject);
        }
    }
}
