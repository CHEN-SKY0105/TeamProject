﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchReact : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioSource audioSource;
    public void MouseIn()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
