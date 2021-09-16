using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlaying : MonoBehaviour
{
    [SerializeField]
    private GameObject num;
    // Start is called before the first frame update
    void Start()
    {
        CeeateDm();
        StartCoroutine(co());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator co()
    {
        while (true)
        {
            CeeateDm();
            yield return new WaitForSeconds(1);
        }
    }
    private void CeeateDm()
    {
        Vector3 pos = new Vector3(0,6,0);
        Instantiate(num, pos, Quaternion.identity);
    }
}
