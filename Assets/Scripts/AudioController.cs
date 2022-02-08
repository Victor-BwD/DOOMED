using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // using Singleton
    private AudioSource myAudioSource;
    public static AudioSource instance;

    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
        instance = myAudioSource;
    }
}
