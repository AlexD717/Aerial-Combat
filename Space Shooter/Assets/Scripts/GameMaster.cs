using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static int enemiesDestroyed;
    public static int totalEnemies;

    [SerializeField] private GameObject scroll;
    [SerializeField] private GameObject player;
    public bool gameOver;

    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private TextMeshProUGUI enemiesDestroyedText;
    public Transform stars;
    [SerializeField] private Sprite starOn;

    [SerializeField] private GameObject playerDiedMenu;

    // Start is called before the first frame update
    void Start()
    {
        enemiesDestroyed = 0;
        totalEnemies = 0;

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (scroll.transform.childCount <= 0 && !gameOver)
        {
            if (GameObject.FindGameObjectWithTag("Boss") == null)
            {
                gameOver = true;
                Invoke("GameOver", 1f);
            }
        }
    }

    void GameOver()
    {
        float percentageDestroyed = ((float)enemiesDestroyed / totalEnemies) * 100;
        int roundedPercentage = Mathf.RoundToInt(percentageDestroyed);
        enemiesDestroyedText.text = "Enemies Destroyed: " + roundedPercentage + "%";

        if (roundedPercentage >= 70)
        {
            stars.GetChild(0).GetComponent<Image>().sprite = starOn;
            int nextlevel = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextlevel >= PlayerPrefs.GetInt("LevelAt", 1))
            {
                PlayerPrefs.SetInt("LevelAt", nextlevel);
                Debug.Log(nextlevel);
            }
            if (PlayerPrefs.GetInt("StarsLevel" + SceneManager.GetActiveScene().buildIndex, 0) <= 1)
            {
                PlayerPrefs.SetInt("StarsLevel" + SceneManager.GetActiveScene().buildIndex, 1);
            }
        }
        if (roundedPercentage >= 90)
        {
            stars.GetChild(1).GetComponent<Image>().sprite = starOn;
            if (PlayerPrefs.GetInt("StarsLevel" + SceneManager.GetActiveScene().buildIndex, 0) <= 2)
            {
                PlayerPrefs.SetInt("StarsLevel" + SceneManager.GetActiveScene().buildIndex, 2);
            }
        }
        if (roundedPercentage >= 100)
        {
            stars.GetChild(2).GetComponent<Image>().sprite = starOn;
            PlayerPrefs.SetInt("StarsLevel" + SceneManager.GetActiveScene().buildIndex, 3);
        }

        gameOverMenu.SetActive(true);
        Destroy(player, 2f);
    }

    public void PlayerDied()
    {
        if (!gameOver)
        {
            playerDiedMenu.SetActive(true);
        }
    }
}