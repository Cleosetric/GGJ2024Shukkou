using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        public enum AudioType
        {
            bgm,
            sfx
        }

        public AudioType audioType;

        [Range(0f, 1f)]
        public float volume;

        [Range(0.1f, 3f)]
        public float pitch;

        public bool loop;

        [HideInInspector]
        public AudioSource source;
    }

    public float defaultBGM = 1f;
    public float defaultSFX = 0.5f;
    public Sound[] sounds;

    public static AudioManager instance;

    //AudioManager

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            if (s.audioType == Sound.AudioType.bgm)
            {
                float savedBGMVolume = PlayerPrefs.GetFloat("BGMVolumeKey", defaultBGM);
                s.volume = savedBGMVolume;
            }
            else
            {
                float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolumeKey", defaultSFX);
                s.volume = savedSFXVolume;
            }

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        s.source.Play();
    }

    //this addition to the code was made by me, the rest was from Brackeys tutorial
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source.Stop();
    }
}
