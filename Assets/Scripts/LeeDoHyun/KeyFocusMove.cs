using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFocusMove : MonoBehaviour
{
    public float offest;
    private Vector3 origin;
    public GameObject player;
    void Start()
    {
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(origin.x - (offest * player.transform.position.x), origin.y, origin.z);
    }
}
