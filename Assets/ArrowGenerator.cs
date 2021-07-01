using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    public GameObject arrawPrefab;
    public GameObject dangerLine;

    float span = 0.8f;
    float delta = 0;
    

    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            GameObject danger = Instantiate(dangerLine) as GameObject;
            int px = Random.Range(-3, 8);
            danger.transform.position = new Vector2(-13, px);
            Destroy(danger, 0.7f);
            StartCoroutine(Produce(px));
            //Invoke("Produce", 0.7f);
            //Produce(px);
        }
    }
    //void Produce()
    //{
    //        GameObject go = Instantiate(arrawPrefab) as GameObject;
    //        go.transform.position = new Vector2(-13, px);
      
    //}
    IEnumerator Produce(int px)
    {
        yield return new WaitForSeconds(0.7f);
        GameObject go = Instantiate(arrawPrefab) as GameObject;
        go.transform.position = new Vector2(-13, px);
    }
}

