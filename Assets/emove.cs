using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emove : MonoBehaviour
{
    Animator ani;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }
    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            ani.SetBool("mov", false);
            ani.SetBool("wait", true);
        }

        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.right;
            //anim.SetInteger("Direction", -1);
            ani.SetBool("mov", true);
            ani.SetBool("wait", false);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.left;
            //anim.SetInteger("Direction", 1);
            ani.SetBool("mov", true);
            ani.SetBool("wait", false);
        }

        transform.position += moveVelocity * 5 * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
