﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public PlayerControl playerControl;
    public float staminaCost;
    public float destroyTime = 3F;
    public GameObject skillObject;
    public Transform skillPos;
    public Transform skillRotation;
    public float skillForce = 200f;
    public float lastFireTime;//最後射擊時間
    public float fireRate = 2f;//射擊間隔
    public Image fillImage;//待實作
    void Start()
    {
        lastFireTime = 10f;
        fillImage = transform.Find("CDImage").GetComponent<Image>();
    }
    void Update()
    {
        lastFireTime += Time.deltaTime;
    }
    public virtual void Shoot()
    {
        if (lastFireTime > fireRate)
        {
            GameObject bulletObj = Instantiate(skillObject);
            if (bulletObj != null)
            {
                bulletObj.transform.position = skillPos.position + skillPos.up;
                bulletObj.transform.rotation = skillRotation.rotation;
                Rigidbody BulletObjRigidbody_ = bulletObj.GetComponent<Rigidbody>();
                if (BulletObjRigidbody_ != null)
                {
                    BulletObjRigidbody_.AddForce(bulletObj.transform.forward * skillForce);
                }
                lastFireTime = 0;
                Destroy(bulletObj, destroyTime);
                playerControl.stamina -= staminaCost;
                //射擊特效
                //扣能量
            }
        }
    }
}