using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool isFirstUpdate = false;
    private bool calledCallback = false;

    private void Update()
    {
        if (!isFirstUpdate)
        {
            isFirstUpdate = true;
        }
        else if (!calledCallback)
        {
            calledCallback = true;
            Loader.LoaderCallback();
        }
    }
}
