﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardSoundHandler : MonoBehaviour
{
    public AudioClip keyClick;
    private AudioSource clickSource;
    void Start()
    {
        clickSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayKeyClick()
    {
        clickSource.PlayOneShot(keyClick); 
    }
}
