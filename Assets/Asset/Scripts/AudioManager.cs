using Unity.VisualScripting;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] musicSounds, sfxSounds;
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in musicSounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.playOnAwake = sound.playOnAwake;
        }

        foreach (Sound sound in sfxSounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.playOnAwake = sound.playOnAwake;
        }
    }

    private void Start()
    {
        PlayMusic("Theme");
    }
    private void Update()
    {

    }
    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, s => s.name == name);
        if (sound == null)
        {
            Debug.Log("Music sound " + name + " not found");
            return;
        }
        sound.source.Play();

    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, s => s.name == name);
        if (sound == null)
        {
            Debug.Log("SFX sound " + name + " not found");
            return;
        }
        sound.source.Play();
    }
    public void ToggleMusic()
    {
        foreach (Sound sound in musicSounds)
        {
            sound.source.mute = !sound.source.mute;
        }
    }
    public void ToggleSFX()
    {
        foreach (Sound sound in sfxSounds)
        {
            sound.source.mute = !sound.source.mute;
        }
    }
    public void MusicVolume(float volume)
    {
        foreach (Sound sound in musicSounds)
        {
            sound.source.volume = volume * sound.volume;
        }

    }
    public void SFXVolume(float volume)
    {
        foreach (Sound sound in sfxSounds)
        {
            sound.source.volume = volume * sound.volume;
        }
    }
}