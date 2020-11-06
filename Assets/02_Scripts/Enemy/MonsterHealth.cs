﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MonsterHealth : MonoBehaviour
{
    public float Hp = 0;
    public float maxHp = 50;
    public int numHeldItemMin = 1;//裝備生成最小數
    public int numHeldItemMax = 3;//裝備生成最大數
    public float beAttackTime;
    public float attackTime = 0.5f;
    public float recoverTime = 0.1f;
    public int bounceForce;

    public HealthBarOnGame healthBarOnGame;
    public GameObject healthBar;
    public ItemSTO itemRate;
    public Animator animator;
    public NavMeshAgent navMeshAgent;
    public GameObject[] getHitEffect;
    public CharacterBase characterBase;
    public SkillBase skillBase;
    public EnemyController enemyController;
    public Rigidbody RB;
    public Transform faceDirection;

    public AudioSource audioSource;//音效在子類別調整音量大小
    public AudioClip poisonHitSFX;//毒受擊音效
    public AudioClip fireHitSFX;//火受擊音效
    public AudioClip windHitSFX;//風受擊音效
    public AudioClip waterHitSFX;//水受擊音效
    public AudioClip tornadoHitSFX;//龍捲風受擊音效
    public AudioClip bombHitSFX;//爆炸受擊音效

    public Transform hitByTransform;
    public float beAttackMin = 0;//被打的次數
    public float beAttackMax = 20;//被打的最大次數
    public EnumAttack enumAttack;
    public Collider boxCollider;
    public float pushforce;

    public virtual void Start()
    {
        characterBase = FindObjectOfType<CharacterBase>();
        skillBase = FindObjectOfType<SkillBase>();
        
        boxCollider = GetComponent<Collider>();
        RB = GetComponent<Rigidbody>();
        if (audioSource == null)
        { 
            audioSource = GetComponent<AudioSource>();
        }
        healthBarOnGame = GetComponentInChildren<HealthBarOnGame>();

        Hp = maxHp;
        healthBarOnGame.SetMaxHealth(maxHp);
    }

    public virtual void Update()
    {
        beAttackTime += Time.deltaTime;

        if (hitByTransform == null)
        {
            enumAttack = EnumAttack.count;
        }
        //當受擊狀態結束後
        if (beAttackTime > recoverTime && navMeshAgent != null)
        {
            //停止被打飛
            RB.velocity = Vector3.zero;
            navMeshAgent.enabled = true;
        }
        if (beAttackMin > 0)
        {
            GetHit(1 + characterBase.charaterStats[(int)CharacterStats.INT] + skillBase.poisonSkillLevel * 20);
        }
    }
    public virtual void GetHit(float Damage)
    {
        if (beAttackTime > attackTime)
        {
            if (enemyController != null)
            { 
                enemyController.attackCD = 0;
            }
            animator.SetTrigger("GetHit");
            if (faceDirection != null)
            { 
                RB.velocity = -faceDirection.forward * bounceForce;
            }
            if (navMeshAgent != null)
            {
                navMeshAgent.enabled = false;
            }
            if (beAttackMin > 0)
            {
                beAttackMin--;
            }
            if (enumAttack != EnumAttack.count)
            {
                switch (enumAttack)
                {
                    case EnumAttack.wind:
                        {
                            audioSource.PlayOneShot(windHitSFX);
                            break;
                        }
                    case EnumAttack.poison:
                        {
                            audioSource.PlayOneShot(poisonHitSFX);
                            break;
                        }
                    case EnumAttack.fireTornado:
                        {
                            audioSource.PlayOneShot(tornadoHitSFX);
                            break;
                        }
                    default:
                        {
                            //如果被風、讀、火龍捲打到就跳過
                            break;
                        }
                }
            }
            Debug.Log(transform.name);
            GameObject FX = Instantiate(getHitEffect[0], transform.position + Vector3.up * 0.8f, transform.rotation);
            Destroy(FX, 1);
            Hp -= Damage;
            healthBarOnGame.SetHealth(Hp);
            RB.velocity = -gameObject.transform.forward * pushforce;
            if (Hp <= 0)
            {
                MonsterDead();
                //避免一直跳進來
                beAttackMin = 0;
            }
            beAttackTime = 0;
        }
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        //打出的傷害數值 失敗
        //text = Instantiate(text, new Vector3(x, 0.7f, z), transform.rotation);
        //為了讓MonsterDead只執行一次
        if (other.CompareTag("Sword"))
        {
            getHitEffect[0] = getHitEffect[1];
            if (characterBase != null)
            { 
                GetHit(15 + characterBase.charaterStats[(int)CharacterStats.STR]);
            }
        }
        if (other.CompareTag("Skill"))
        {
            getHitEffect[0] = getHitEffect[5];
            GetHit(0);
        }
        if (other.CompareTag("WaterAttack"))
        {
            getHitEffect[0] = getHitEffect[6];
            audioSource.PlayOneShot(waterHitSFX);
            GetHit(5 + characterBase.charaterStats[(int)CharacterStats.INT] + skillBase.waterSkillLevel*20);
            Debug.Log("角色數值: " + characterBase.charaterStats[(int)CharacterStats.INT] + "技能傷害: " + skillBase.waterSkillLevel*20);
        }

        if (other.CompareTag("FireAttack"))
        {
            getHitEffect[0] = getHitEffect[2];
            audioSource.PlayOneShot(fireHitSFX);
            GetHit(20 + characterBase.charaterStats[(int)CharacterStats.INT] + skillBase.fireSkillLevel*20);
        }

        if (other.CompareTag("Bomb"))
        {
            getHitEffect[0] = getHitEffect[2];
            audioSource.PlayOneShot(bombHitSFX);
            GetHit(60 + characterBase.charaterStats[(int)CharacterStats.INT] + characterBase.charaterStats[(int)CharacterStats.SPR]*2);
        }
        if (Hp <= 0)
        {
            boxCollider.enabled = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (Hp > 0)
        {
            if (other.CompareTag("Poison"))
            {
                getHitEffect[0] = getHitEffect[4];
                beAttackMin = beAttackMax;//最大被打的次數
                hitByTransform = other.transform;
                GetHit(1 + characterBase.charaterStats[(int)CharacterStats.INT] + skillBase.poisonSkillLevel * 20);
            }
            if (other.CompareTag("WindAttack"))
            {
                getHitEffect[0] = getHitEffect[3];
                GetHit(2 + characterBase.charaterStats[(int)CharacterStats.INT] + skillBase.windSkillLevel * 20);
                enumAttack = EnumAttack.wind;
            }
            if (other.CompareTag("Firetornado"))
            {
                getHitEffect[0] = getHitEffect[2];
                GetHit(5 + characterBase.charaterStats[(int)CharacterStats.INT] + characterBase.charaterStats[(int)CharacterStats.SPR] * 2 + skillBase.windSkillLevel * 20);
                enumAttack = EnumAttack.fireTornado;
            }
        }
    }

    public virtual void MonsterDead()
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = false;
        }
        if (animator != null && animator.GetBool("Dead") == false)
        {
            animator.SetBool("Dead", true);
        }
        healthBar.SetActive(false);

        Vector3 itemLocation = this.transform.position;//獲得當前怪物的地點
        int rewardItems = Random.Range(numHeldItemMin, numHeldItemMax);//隨機裝備產生值
        for (int i = 0; i < rewardItems; i++)
        {
            //Instantiate(gold, transform.position, transform.rotation);
            Vector3 randomItemLocation = itemLocation;
            randomItemLocation += new Vector3(Random.Range(-1, 1), 0.2f, Random.Range(-1, 1));//在死亡地點周圍隨機分布
            float RateCnt_ = 0;//物品產生的最小值
            float ItemRandom_ = Random.Range(0, 100) / 100f;//隨機裝備機率值
            for (int j = 0; j < itemRate.ItemObjList.Length; j++)
            {
                RateCnt_ += itemRate.ItemObjRateList[j];
                if (ItemRandom_ < RateCnt_)
                {
                    Instantiate(itemRate.ItemObjList[j], randomItemLocation, itemRate.ItemObjList[j].transform.rotation);
                    break;
                }
            }
        }
    }
}
