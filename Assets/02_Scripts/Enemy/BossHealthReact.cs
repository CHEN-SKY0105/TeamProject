﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthReact : MonoBehaviour
{
    BossHealth bossHealth;
    bool Wheel2Trigger = true;//為了不讓Wheel_2_Broke重複呼叫
    void Start()
    {
        bossHealth = GetComponent<BossHealth>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (bossHealth.Hp < bossHealth.maxHp * 0.7)
        {
            //小於140
            bossHealth.animator.SetTrigger("Wheel_1_Broke");
            if (bossHealth.Hp < bossHealth.maxHp * 0.3 && Wheel2Trigger)
            {
                //小於60
                //停止但不消失 可能重複呼叫
                bossHealth.animator.SetTrigger("Wheel_2_Broke");
                Wheel2Trigger = false;
            }
        }
    }
}