using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] lvlButtons;
    public Sprite starOn;

    // Start is called before the first frame update
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("LevelAt", 1);

        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if (i + 1 > levelAt)
            {
                lvlButtons[i].interactable = false;
            }
            Transform stars = lvlButtons[i].transform.GetChild(0);
            for (int s = 0; s < 3; s++)
            {
                if (PlayerPrefs.GetInt("StarsLevel" + (i+1), 0) > s)
                {
                    stars.GetChild(s).GetComponent<Image>().sprite = starOn;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
        }
        */
    }
}
