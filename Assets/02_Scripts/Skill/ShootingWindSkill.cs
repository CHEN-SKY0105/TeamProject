﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingWindSkill : MonoBehaviour
{
    public float destroyTime = 3F;
    public GameObject tornado;//爆炸物件生成
    public GameObject fireTornado;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = CentralData.GetInst().SFXVol;
        GameObject shootingSkill = Instantiate(tornado, transform.position, transform.rotation,transform);
        Destroy(shootingSkill, destroyTime);
        Destroy(gameObject, destroyTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FireAttack"))
        {
            Destroy(gameObject);
            Instantiate(fireTornado, transform.position, transform.rotation);
        }
    }
}