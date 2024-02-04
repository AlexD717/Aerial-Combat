using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public void Restart()
    {
        Loader.Load(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        Loader.Load(System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1)));
    }

    public void MainMenu()
    {
        Loader.Load("Main Menu");
    }

    public void LoadLevel(int level)
    {
        Loader.Load("Level " + level);
    }
}
