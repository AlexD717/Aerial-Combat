using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    public float speed;
    public float moveUp;

    private void Start()
    {
        GameObject scroller = GameObject.Find("Scroller");
        Scroll scrollScript = scroller.GetComponent<Scroll>();
        speed = scrollScript.speed;
    }

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        if (transform.position.y <= -20)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + moveUp, transform.position.z);
        }
    }
}
