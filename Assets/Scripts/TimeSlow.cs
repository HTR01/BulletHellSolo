﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlow : MonoBehaviour
{
    public static bool timeSlowed = false;
    public float slowTimer = 100;
    public Image cooldown;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (timeSlowed == false && PlayerMovement.isDead == false)
            {
                SlowTime();
            }
            else
            {
                NormalTime();
            }
        }
        if(timeSlowed == true)
        {
            slowTimer -= 0.3f;
            if(slowTimer <= 0)
            {
                NormalTime();
            }
        }
        else if(slowTimer < 100)
        {
            slowTimer += 0.3f;
        }

        cooldown.fillAmount = slowTimer / 100;
    }

    void SlowTime()
    {
        Time.timeScale = 0.5f;
        timeSlowed = true;
    }

    void NormalTime()
    {
        Time.timeScale = 1;
        timeSlowed = false;
    }
}
