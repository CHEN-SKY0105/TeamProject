﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitEffect : MonoBehaviour
{
    public float getHitInvincibleTime;
    float getHitInvincible = 1f;
    public int dust = 99999;
    public float maxHp = 100;
    public float playerHealth = 0;
    public HealthBarOnGame healthbarongame;
    public UIBarControl uIBarControl;
    public PlayerControl playerControl;
    public Rigidbody RD;
    public GameObject changeColor;
    public Transform playerRotation;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public int bounceForce = 10000;
    public bool getHit;
    public bool attackBuff;
    public GameObject[] getHitEffect;
    CharacterBase characterBase;

    void Start()
    {
        characterBase = FindObjectOfType<CharacterBase>();
        dust = CentralData.GetInst().dust;
        playerHealth = maxHp;
        uIBarControl.SetMaxHealth(maxHp);//UI身上的血條
        healthbarongame.SetMaxHealth(maxHp);//人物身上的血條
        RD = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (getHitInvincibleTime > 0f)
        {
            getHitInvincibleTime -= Time.deltaTime;
            spriteRenderer.color = Color.red;
        }
        else if (getHitInvincibleTime <= 0.6f)
        {
            //關閉被打中狀態
            getHit = false;
            if (getHitInvincibleTime < 0f)
            {
                getHitInvincibleTime = 0f;
                spriteRenderer.color = Color.white;
            }
        }
        if (playerHealth <= 0 && GetComponent<Collider>().enabled == true)
        {
            //死掉後玩家不能動
            playerControl.isAttack = true;
            animator.SetTrigger("Dead");
            GetComponent<Collider>().enabled = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gold") && playerHealth>0)
        {
            dust += 5;
            playerHealth += 5;
            Destroy(collision.gameObject);
            uIBarControl.SetHealth(playerHealth);
            healthbarongame.SetHealth(playerHealth);

        }
        if (collision.gameObject.CompareTag("Green") && playerHealth > 0)
        {
            dust += 5;
            playerHealth += 5;
            Destroy(collision.gameObject);
            uIBarControl.SetHealth(playerHealth);
            healthbarongame.SetHealth(playerHealth);
        }
        if (collision.gameObject.CompareTag("White") && playerHealth > 0)
        {
            dust += 5;
            playerHealth += 5;
            Destroy(collision.gameObject);
            uIBarControl.SetHealth(playerHealth);
            healthbarongame.SetHealth(playerHealth);
        }
        if (collision.gameObject.CompareTag("Blue") && playerHealth > 0)
        {
            dust += 5;
            playerHealth += 5;
            Destroy(collision.gameObject);
            uIBarControl.SetHealth(playerHealth);
            healthbarongame.SetHealth(playerHealth);
        }

        //else if (collision.gameObject.CompareTag("Item"))
        //{
        //    collision.gameObject.GetComponent<ItemPickup>().PickUp();
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        //放在Stay會重複傷害因為大招不會因為玩家碰到而消失
        if (other.gameObject.CompareTag("BossUlt") && playerHealth>0 && characterBase.charaterStats[(int)CharacterStats.DEF] - 20 < 0)
        {
            //當玩家非無敵狀態
            if (!playerControl.isInvincible)
            {
                getHitEffect[0] = getHitEffect[1];
                Debug.Log("danger");
                //絕對值(人物的防禦值-20)<0
                playerHealth -= Mathf.Abs(characterBase.charaterStats[(int)CharacterStats.DEF]-20);
                getHit = true;
                //怪打到玩家時把無敵時間輸入進去
                getHitInvincibleTime = getHitInvincible;
                //生出被大招打中的特效
                GameObject bossUltHitFX = Instantiate(getHitEffect[1], transform.position, transform.rotation);
                Destroy(bossUltHitFX, 1f);
                //將血量輸入到頭頂的UI
                healthbarongame.SetHealth(playerHealth);
                //將血量輸入到畫面上的UI
                uIBarControl.SetHealth(playerHealth);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            other.GetComponent<ItemPickup>().PickUp();
        }
        if (other.CompareTag("Block") || other.CompareTag("Wall"))
        {
            gameObject.GetComponent<Collider>().isTrigger = false;
        }
        if (other.gameObject.CompareTag("MonsterAttack") && getHitInvincibleTime <= 0f)
        {
            //當玩家非無敵狀態
            if (!playerControl.isInvincible && playerHealth > 0 && characterBase.charaterStats[(int)CharacterStats.DEF] - 10 < 0)
            {
                Debug.Log("danger");
                //絕對值(人物的防禦值-10)<0
                playerHealth -= Mathf.Abs(characterBase.charaterStats[(int)CharacterStats.DEF] - 10);
                getHit = true;
                //怪打到玩家時把無敵時間輸入進去
                getHitInvincibleTime = getHitInvincible;
                //將血量輸入到頭頂的UI
                healthbarongame.SetHealth(playerHealth);
                //將血量輸入到畫面上的UI
                uIBarControl.SetHealth(playerHealth);
            }
        }
        //當玩家無敵狀態
        else if (playerControl.isInvincible)
        {
            //尚未實作
            Debug.Log("攻擊力*3");
            attackBuff = true;
        } 
    }
}

