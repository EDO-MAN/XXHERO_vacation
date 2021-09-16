using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change : MonoBehaviour
{
    [SerializeField]
    GameObject stage;

    

    public void OnRight()
    {
        
        if (stage.transform.position.x <= -5160f)
            return;

        stage.transform.position = new Vector2(stage.transform.position.x - 2000f, stage.transform.position.y);
        Debug.Log(stage.transform.position.x);
    }

    public void OnLeft()
    {
        if (stage.transform.position.x >= -120f)
            return;

        stage.transform.position = new Vector2(stage.transform.position.x + 2000f, stage.transform.position.y);
    }
}
