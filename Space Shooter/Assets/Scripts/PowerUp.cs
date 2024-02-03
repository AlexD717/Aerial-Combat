using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private GameObject scroller;

    private void Start()
    {
        scroller = GameObject.Find("ObjectScroller");
        transform.parent = scroller.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
