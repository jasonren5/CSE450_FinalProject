using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    AudioSource audioSource;
    public AudioClip tokenSound;
    public AudioClip deathSound;

    void Awake()
    {
        instance = this;
    }

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundToken(){
        audioSource.PlayOneShot(tokenSound);
    }
    public void PlaySoundDeath(){
        audioSource.PlayOneShot(deathSound);
    }
}
