﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tornado : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;
    public float DestroyTime = 3F;
    public float GameObjectDestroyTime = 3f;
    float countdown;
    bool hasExplode = false;
    public AudioSource GunAudio;//音樂放置
    public AudioClip SoilWallSFX;//音效
    public AudioClip explosionSFX;

    //public GameObject explosionEffect;
    public GameObject explosionObject;

    // Start is called before the first frame update
    void Start()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExplode)
        {
            GunAudio.PlayOneShot(SoilWallSFX);
            Explode();
            hasExplode = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Explode()
    {
        //Instantiate(explosionEffect, transform.position, transform.rotation);
        //Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        //foreach (Collider nearbyObject in colliders)
        //{
        //    Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
        //    if (rb != null)
        //    {
        //        rb.AddExplosionForce(force, transform.position, radius);
        //    }
        //}
        Destroy(gameObject, DestroyTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FirMagic"))
        {
            Destroy(gameObject,0.3f);
           Instantiate(explosionObject, transform.position, transform.rotation);
      
        }
    }
    //public void explosion()
    //{
    //    if (explosionEffect)
    //        Instantiate(explosionEffect, transform.position, transform.rotation);
    //    else
    //        Destroy(explosionEffect, 3F);
    //}
    //public void expSFX()
    //{
    //    GunAudio.PlayOneShot(explosionSFX);
    //}
}
