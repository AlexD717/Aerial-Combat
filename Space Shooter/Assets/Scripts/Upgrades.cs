using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Numerics;

public class Upgrades : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    private float coins;

    private void Start()
    {
        coins = PlayerPrefs.GetFloat("coins", 0);
        coinsText.text = coins.ToString("F0");
    }
}
