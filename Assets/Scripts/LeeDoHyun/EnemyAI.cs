using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float distance;
    Transform player;
    Animator anim;
    


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) > distance)
        { 
            transform.Translate(new Vector2(-1, 0) * Time.deltaTime * speed);
            anim.SetBool("isMoving", true);
            Direction();
        }
        else
        {
            anim.SetBool("isMoving", false) ;
        }
    }
    void Direction()
    {
        if(transform.position.x - player.position.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
