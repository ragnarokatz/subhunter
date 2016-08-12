using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager I { get { return AudioManager.instance; } }

    public AudioSource[] AudioSources;

    private void Start()
    {
        AudioManager.instance = this;
    }
}