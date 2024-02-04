using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    private static string loadingSceneName;

    public static void Load(string scene)
    {
        loadingSceneName = scene;
        SceneManager.LoadScene("Loading");
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(loadingSceneName);
    }
}
