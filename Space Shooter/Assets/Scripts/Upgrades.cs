using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Numerics;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    private float coins;

    [Header("Upgrades")]
    [SerializeField] private Button healthUpgradeButton;
    private TextMeshProUGUI healthUpgradeCostText;
    private float healthUpgradeCost;
    [SerializeField] private Button mainWeaponUpgradeButton;
    private TextMeshProUGUI mainWeaponUpgradeCostText;
    private float mainWeaponUpgradeCost;
    [SerializeField] private Button moreCoinsUpgradeButton;
    private TextMeshProUGUI moreCoinsUpgradeCostText;
    private float moreCoinsUpgradeCost;

    private void Start()
    {
        //coins = 99999999999999999999999f;
        //PlayerPrefs.DeleteAll();

        coins = PlayerPrefs.GetFloat("coins", 0f);
        coinsText.text = coins.ToString("F0");

        healthUpgradeCostText = healthUpgradeButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        healthUpgradeCost = PlayerPrefs.GetFloat("healthUpgradeCost", 50f);
        UpdateHealthText();

        mainWeaponUpgradeCostText = mainWeaponUpgradeButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        mainWeaponUpgradeCost = PlayerPrefs.GetFloat("mainWeaponUpgradeCost", 70f);
        UpdateMainWeaponText();

        moreCoinsUpgradeCostText = moreCoinsUpgradeButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        moreCoinsUpgradeCost = PlayerPrefs.GetFloat("moreCoinsUpgradeCost", 150f);
        UpdateMoreCoinsText();

        DisableButtons();
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

            float healthUpgradeCostMult = 1.25f;
            healthUpgradeCost *= healthUpgradeCostMult;
            healthUpgradeCost = Mathf.Ceil(healthUpgradeCost);
            UpdateHealthText();
            PlayerPrefs.SetFloat("healthUpgradeCost", healthUpgradeCost);

            float playerHealth = PlayerPrefs.GetFloat("playerHealth", 10f);
            float playerHealthIncrease = 0.1f;
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

            float mainWeaponUpgradeCostMult = 1.2f;
            mainWeaponUpgradeCost *= mainWeaponUpgradeCostMult;
            mainWeaponUpgradeCost = Mathf.Ceil(mainWeaponUpgradeCost);
            UpdateMainWeaponText();
            PlayerPrefs.SetFloat("mainWeaponUpgradeCost", mainWeaponUpgradeCost);

            float playerDamage = PlayerPrefs.GetFloat("bulletDamage", 1f);
            float playerDamageIncrease = 0.05f;
            playerDamage += playerDamageIncrease;
            PlayerPrefs.SetFloat("bulletDamage", playerDamage);

            float timesUpgraded = PlayerPrefs.GetFloat("mainWeaponTimesUpgraded", 0f);
            int timesUpgradeForCooldownIncrease = 5;
            timesUpgraded = (timesUpgraded + 1) % timesUpgradeForCooldownIncrease;
            PlayerPrefs.SetFloat("mainWeaponTimesUpgraded", timesUpgraded);
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

            float moreCoinsUpgradeCostMult = 1.4f;
            moreCoinsUpgradeCost *= moreCoinsUpgradeCostMult;
            moreCoinsUpgradeCost = Mathf.Ceil(moreCoinsUpgradeCost);
            UpdateMoreCoinsText();
            PlayerPrefs.SetFloat("moreCoinsUpgradeCost", moreCoinsUpgradeCost);

            float extraMoney = PlayerPrefs.GetFloat("extraMoney", 0f);
            float extraMoneyIncrease = 0.05f;
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
