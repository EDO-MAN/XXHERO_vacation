using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spear : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.09f, 0, 0);

        //if (transform.position.x > 20)
        //{
        //    Destroy(gameObject);
        //}

        //Vector2 p1 = transform.position;
        //Vector2 p2 = player.transform.position;
        //Vector2 dir = p1 - p2;
        //float d = dir.magnitude;
        //float r1 = 1.0f;
        //float r2 = 2.0f;

        //if (d < r1 + r2)
        //{
        //    Debug.Log("맞음");
        //    Destroy(gameObject);
        //}
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            Destroy(gameObject);
        else
            return;
    }
}
