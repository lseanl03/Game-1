using System;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;
    public AudioSource source;

    [Range(0f,1f)]
    public float volume;

    [Range(-3f, 3f)]
    public float pitch ;

    public bool loop;

    public bool playOnAwake;

}