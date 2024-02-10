using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Numerics;
using UnityEngine.UI;
using System;

public class Upgrades : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    private float coins;

    [Header("Upgrades")]
    [SerializeField] private Button healthUpgradeButton;
    private TextMeshProUGUI healthUpgradeCostText;
    private float healthUpgradeCost;
    private float healthUpgradeCostMult = 1.15f;
    [SerializeField] private Button mainWeaponUpgradeButton;
    private TextMeshProUGUI mainWeaponUpgradeCostText;
    private float mainWeaponUpgradeCost;
    private float mainWeaponUpgradeCostMult = 1.2f;
    [SerializeField] private Button moreCoinsUpgradeButton;
    private TextMeshProUGUI moreCoinsUpgradeCostText;
    private float moreCoinsUpgradeCost;
    private float moreCoinsUpgradeCostMult = 1.4f;

    private void Start()
    {
        //coins = 99999999999999999999999f;
        //PlayerPrefs.DeleteAll();
        coins = PlayerPrefs.GetFloat("coins", 0f);
        coinsText.text = coins.ToString("F0");

        healthUpgradeCostText = healthUpgradeButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        healthUpgradeCost = FigureOutCost(50, PlayerPrefs.GetInt("timesUpgradedHealth", 0), healthUpgradeCostMult);
        UpdateHealthText();

        mainWeaponUpgradeCostText = mainWeaponUpgradeButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        mainWeaponUpgradeCost = FigureOutCost(50, PlayerPrefs.GetInt("timesUpgradedMainWeapon", 0), mainWeaponUpgradeCostMult);
        UpdateMainWeaponText();

        moreCoinsUpgradeCostText = moreCoinsUpgradeButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        moreCoinsUpgradeCost = FigureOutCost(150, PlayerPrefs.GetInt("timesUpgradedMoreCoins", 0), moreCoinsUpgradeCostMult);
        UpdateMoreCoinsText();

        DisableButtons();
    }

    private float FigureOutCost(float startingCost, int timesUpgraded, float multiplyer)
    {
        return Mathf.Ceil((float)(startingCost * Math.Pow(multiplyer, timesUpgraded)));
    }

    private void DisableButtons()
    {
        if (coins < healthUpgradeCost)
        {
            healthUpgradeButton.interactable = false;
        }
        if (coins < mainWeaponUpgradeCost)
        {
            mainWeaponUpgradeButton.interactable = false;
        }
        if (coins < moreCoinsUpgradeCost)
        {
            moreCoinsUpgradeButton.interactable = false;
        }
    }

    private void UpdateCoins()
    {
        coinsText.text = coins.ToString("F0");
        PlayerPrefs.SetFloat("coins", coins);
    }

    public void UpgradePlayerHealth()
    {
        if (coins >= healthUpgradeCost)
        {
            coins -= healthUpgradeCost;
            UpdateCoins();

            PlayerPrefs.SetInt("timesUpgradedHealth", PlayerPrefs.GetInt("timesUpgradedHealth", 0) + 1);
            healthUpgradeCost = FigureOutCost(50, PlayerPrefs.GetInt("timesUpgradedHealth", 0), healthUpgradeCostMult);
            UpdateHealthText();

            float playerHealth = PlayerPrefs.GetFloat("playerHealth", 10f);
            float playerHealthIncrease = 0.2f;
            playerHealth += playerHealthIncrease;
            PlayerPrefs.SetFloat("playerHealth", playerHealth);
        }
        DisableButtons();
    }
    private void UpdateHealthText()
    {
        healthUpgradeCostText.text = "Cost: " + healthUpgradeCost.ToString();
    }

    public void UpgradeMainWeapon()
    {
        if (coins >= mainWeaponUpgradeCost)
        {
            coins -= mainWeaponUpgradeCost;
            UpdateCoins();

            PlayerPrefs.SetInt("timesUpgradedMainWeapon", PlayerPrefs.GetInt("timesUpgradedMainWeapon", 0) + 1);
            mainWeaponUpgradeCost = FigureOutCost(50, PlayerPrefs.GetInt("timesUpgradedMainWeapon", 0), mainWeaponUpgradeCostMult);
            UpdateMainWeaponText();

            float playerDamage = PlayerPrefs.GetFloat("bulletDamage", 1f);
            float playerDamageIncrease = 0.1f;
            playerDamage += playerDamageIncrease;
            PlayerPrefs.SetFloat("bulletDamage", playerDamage);

            int timesUpgraded = PlayerPrefs.GetInt("timesUpgradedMainWeapon", 0);
            int timesUpgradeForCooldownIncrease = 5;
            timesUpgraded = timesUpgraded % timesUpgradeForCooldownIncrease;
            if (timesUpgraded == 0)
            {
                float playerCooldown = PlayerPrefs.GetFloat("bulletCooldown", 0.4f);
                playerCooldown *= 0.95f;
                Debug.Log(playerCooldown);
                PlayerPrefs.SetFloat("bulletCooldown", playerCooldown);
            }
        }
        DisableButtons();
    }
    private void UpdateMainWeaponText()
    {
        mainWeaponUpgradeCostText.text = "Cost: " + mainWeaponUpgradeCost.ToString();
    }

    public void UpgradeMoreCoins()
    {
        if (coins >= moreCoinsUpgradeCost)
        {
            coins -= moreCoinsUpgradeCost;
            UpdateCoins();
            
            PlayerPrefs.SetInt("timesUpgradedMoreCoins", PlayerPrefs.GetInt("timesUpgradedMoreCoins", 0) + 1);
            moreCoinsUpgradeCost = FigureOutCost(150, PlayerPrefs.GetInt("timesUpgradedMoreCoins", 0), moreCoinsUpgradeCostMult);
            UpdateMoreCoinsText();

            float extraMoney = PlayerPrefs.GetFloat("extraMoney", 0f);
            float extraMoneyIncrease = 0.1f;
            extraMoney += extraMoneyIncrease;
            PlayerPrefs.SetFloat("extraMoney", extraMoney);
        }
        DisableButtons();
    }
    private void UpdateMoreCoinsText()
    {
        moreCoinsUpgradeCostText.text = "Cost: " + moreCoinsUpgradeCost.ToString();
    }
}
