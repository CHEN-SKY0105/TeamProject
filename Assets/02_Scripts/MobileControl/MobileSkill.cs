﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileSkill : MonoBehaviour
{
    public Skill[] skillList;
    Animator animator;
    MobileAttack mobileAttack;

    void Start()
    {
        animator = GetComponent<Animator>();
        mobileAttack = FindObjectOfType<MobileAttack>();
    }
    public void SetSkill(int skillNum)
    {
        skillList[0] = skillList[skillNum];
        if (skillList[0].skillTimer > skillList[0].skillCD && !mobileAttack.isAttack)
        { 
            mobileAttack.isAttack = true;
            animator.SetTrigger("Magic");
        }
    }
    public void SkillShoot()//動畫Event控制
    {
        skillList[0].Shoot();
    }
}
