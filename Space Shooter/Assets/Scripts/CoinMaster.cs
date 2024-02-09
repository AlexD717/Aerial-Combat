using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMaster : MonoBehaviour
{
    public static float coinsCollected;

    public static void SaveCoinsCollected()
    {
        float currentCoins = PlayerPrefs.GetFloat("coins", 0);
        PlayerPrefs.SetFloat("coins", (currentCoins + coinsCollected));
        coinsCollected = 0;
    }
}
