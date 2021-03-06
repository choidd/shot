﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour, IListener {

    public static SoundManager Instance
    {
        get { return instance; }
        set { }
    }
    public static SoundManager instance = null;

    public AudioClip[] stageBGM;
    public AudioClip titleBGM;
    public AudioClip shootSound;
    AudioSource thisAudio;

    float volLowRange = .5f;
    float volHighRange = 1.0f;
    // Use this for initialization

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(this);
        }

        thisAudio = GetComponent<AudioSource>();
    }

    void Start () {
        EventManager.Instance.AddListener(EVENT_TYPE.GAME_ENEMY_ATTACK, this);
        EventManager.Instance.AddListener(EVENT_TYPE.GAME_PLAYER_ATTACK, this);
    }
	
    public void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch(Event_Type)
        {
            case EVENT_TYPE.GAME_ENEMY_ATTACK:
                thisAudio.Play();
                break;
            case EVENT_TYPE.GAME_PLAYER_HEALTH_CHANGE:
                break;
            case EVENT_TYPE.GAME_PLAYER_ATTACK:
                float vol = Random.Range(volLowRange, volHighRange);          
                thisAudio.PlayOneShot(shootSound, vol);
                break;
        }
    }
}
