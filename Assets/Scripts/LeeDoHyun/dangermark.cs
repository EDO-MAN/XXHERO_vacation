using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dangermark : MonoBehaviour
{
    TrailRenderer tr;
    public Vector3 EndPosition;

    void Start()
    {
        tr = GetComponent<TrailRenderer>();

        tr.startColor = new Color(1, 0, 0, 0.5f);
        tr.endColor = new Color(1, 0, 0, 0.5f);
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, EndPosition, Time.deltaTime * 3.5f);
    }
}
