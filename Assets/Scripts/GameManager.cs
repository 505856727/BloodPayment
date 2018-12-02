using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public AudioSource[] music;
    public AudioSource[] sound;
    public AudioClip playmusic;
    private void Awake()
    {
        instance = this;
    }

    public GameManager GetInstance()
    {
        return instance;
    }

    public void PlayMusic(int ID)
    {
        music[ID].Play();
    }
}
