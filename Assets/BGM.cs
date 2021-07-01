using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioClip audioBGM;
    AudioSource audio;
    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        audio.clip = audioBGM;
        audio.Play();
    }
}
