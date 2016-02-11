﻿using UnityEngine;
using System.Collections;

public class Background_Music_S : Singleton<Background_Music_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Background_Music_S() { }

    private AudioSource audio_source;
    public AudioClip audio_clip;
    private bool background_music_is_playing =false;

    // Use this for initialization
    void Start()
    {
        audio_source = GetComponent<AudioSource>();
        audio_source.clip = audio_clip;
        audio_source.loop = true;
        audio_source.Play();
    }

    public void Disable_Background_Music()
    {
        audio_source.Stop();
    }
    public void Enable_Background_Music()
    {
        audio_source.Play();
    }

    public bool Get_Background_Music_Is_On()
    {
        return background_music_is_playing;
    }

    public void Set_Audio_Source(AudioClip clip)
    {
        audio_source.clip = clip;
        audio_source.Play();
    }
}