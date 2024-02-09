using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private BGMusic bgMusic;

    private void Start()
    {
        bgMusic = FindObjectOfType<BGMusic>();
    }

    public void PauseGame()
    {
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
