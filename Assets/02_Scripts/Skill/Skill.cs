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
    public float fireRate;//射擊間隔
    public Image fillImage;
    void Start()
    {
        lastFireTime = 10f;//確保一開始都能按技能
        fillImage = transform.Find("CDImage").GetComponent<Image>();
        playerControl = FindObjectOfType<PlayerControl>();
    }
    void Update()
    {
        lastFireTime += Time.deltaTime;
        if (lastFireTime < fireRate)
        {
            fillImage.fillAmount = (fireRate - lastFireTime) / fireRate;
        }
        else if (lastFireTime >= fireRate)
        {
            fillImage.fillAmount = 0;
        }
    }
    public virtual void Shoot()
    {
        GameObject bulletObj = Instantiate(skillObject);
        if (bulletObj != null)
        {
            bulletObj.transform.position = skillPos.position+skillPos.up*0.7f;
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
