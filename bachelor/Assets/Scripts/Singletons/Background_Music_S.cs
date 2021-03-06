﻿using UnityEngine;
using System.Collections;

public class Background_Music_S : Singleton<Background_Music_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Background_Music_S() { }

    //private 
    private AudioSource background_audio;
    private AudioSource radio_audio;

    //private visible
    [SerializeField]
    public AudioClip background_music;
    [SerializeField]
    private AudioClip[] radio_music = new AudioClip[15];
    [SerializeField]
    private float max_background_volume = 0.02f;
    [SerializeField]
    private float max_radio_volume = 0.04f;

    //getter and setter
    private bool _background_music_is_playing;
    public bool background_music_is_playing
    {
        get
        {
            return _background_music_is_playing;
        }
        set
        {
            _background_music_is_playing = value;
        }
    }
    private bool _is_radio_on;
    public bool is_radio_on
    {
        get
        {
            return _is_radio_on;
        }
        set
        {
            _is_radio_on = value;
        }

    }

    /*----------------------------------------------------------------------------------------------------*/

    // Use this for initialization
    void Start()
    {
        //get the audio source for the background music
        background_audio = GetComponent<AudioSource>();
        //assign the background music to the background audio source and the radio
        radio_audio.clip = background_music;
        background_audio.clip = background_music;
        //let background audio source loop
        background_audio.loop = true;
        //set radio volume to 0 at the beginning
        radio_audio.volume = 0;
        Play();
    }
    void Update()
    {
        //if radio is on, but
        if (is_radio_on)
            //is not playing (because clip ended because no loop) then
            if (!radio_audio.isPlaying)
                //play a random clip
                Turn_On_Radio();
    }
    //public
    //register the audiosource of the radio
    public void Register(AudioSource a_source)
    {
        radio_audio = a_source;
    }
    //set background volume to 0
    public void Disable_Background_Music()
    {
        background_audio.volume = 0;
    }
    //set background volume depending on state of radio (radio soundtracks are louder than background music)
    public void Enable_Background_Music()
    {
        if (!_is_radio_on)
            background_audio.volume = 1;
        else
            background_audio.volume = max_background_volume;
    }
    public void Turn_Off_Radio()
    {
        _is_radio_on = false;
        //turn the volume of the radio down
        radio_audio.volume = 0;
        //set both audio sources to default background music
        radio_audio.clip = background_music;
        background_audio.clip = background_music;
        background_audio.loop = true;
        Play();
    }
    public void Turn_On_Radio()
    {
        _is_radio_on = true;
        //choose a random song
        int number = Random.Range(0, radio_music.Length);
        //set both audio sources to new music clip
        radio_audio.clip = radio_music[number];
        background_audio.clip = radio_music[number];
        //don't loop
        background_audio.loop = false;
        radio_audio.volume = max_radio_volume;
        Play();
    }

    //private
    private void Play()
    {
        radio_audio.Play();
        background_audio.Play();
    }


}
