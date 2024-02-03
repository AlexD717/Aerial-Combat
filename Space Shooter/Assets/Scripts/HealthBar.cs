using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public float maxHealth;
    public float health;
    public float startLerpSpeed;
    public float lerpSpeed;

    private void Update()
    {
        lerpSpeed = startLerpSpeed * Time.deltaTime;
        HealthBarFiller();
    }

    public void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(health / maxHealth, healthBar.fillAmount, lerpSpeed);
        ColorChanger();
    }

    public void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, health / maxHealth);
        healthBar.color = healthColor;
    }
}
