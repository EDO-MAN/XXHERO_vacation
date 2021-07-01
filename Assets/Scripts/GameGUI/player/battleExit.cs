using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class battleExit : MonoBehaviour
{
    public GameObject Enmeyimg;
   
    private int count = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            count++;
            Debug.Log(count);
            if (count == 10)
            {
                Enmeyimg.SetActive(true);
            }
            else if (count > 10)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
