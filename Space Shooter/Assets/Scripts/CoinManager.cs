using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public float moneyToGive;
    public Vector2 moneySpreadRange;
    [SerializeField] GameObject coinPrefab;

    public void GiveMoney()
    {
        if (moneyToGive <= 0)
        {
            return;
        }
        else
        {
            while (moneyToGive > 0)
            {
                if (moneyToGive >= 10)
                {
                    float minOffset = moneySpreadRange.x;
                    float maxOffset = moneySpreadRange.y;
                    SpawnCoin(10, new Vector3(Random.Range(minOffset, maxOffset), Random.Range(minOffset, maxOffset), 0));
                    moneyToGive -= 10;
                }
                else
                {
                    SpawnCoin(moneyToGive, Vector3.zero);
                    moneyToGive = 0;
                }
            }
        }
    }

    private void SpawnCoin(float moneyToGive, Vector3 offset)
    {
        if (moneyToGive >= 10)
        {
            Coin coin = Instantiate(coinPrefab, transform.position + offset, Quaternion.identity).GetComponent<Coin>();
            coin.moneyToGive = moneyToGive;
            coin.transform.localScale = new Vector3(0.3f, 0.3f, 1);
        }
        else
        {
            float normalizedValue = (moneyToGive - 1) / 9;
            float coinSize = 0.1f + (normalizedValue * 0.2f);
            Mathf.Clamp(coinSize, 0.1f, 0.3f);

            Coin coin = Instantiate(coinPrefab, transform.position + offset, Quaternion.identity).GetComponent<Coin>();
            coin.moneyToGive = moneyToGive;
            coin.transform.localScale = new Vector3(coinSize, coinSize, 1);
        }
    }
}
