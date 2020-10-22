using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audio;
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        audio.Play();
    }

}
