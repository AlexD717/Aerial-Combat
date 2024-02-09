using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private TextMeshProUGUI coinsText;
    private BGMusic bgMusic;

    private void Start()
    {
        bgMusic = FindObjectOfType<BGMusic>();
    }

    public void PauseGame()
    {
        float coins = CoinMaster.coinsCollected;
        coinsText.text = coins.ToString("F0");

        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        if (bgMusic != null)
        {
            bgMusic.Pause();
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        if (bgMusic != null)
        {
            bgMusic.Play();
        }
    }
}
