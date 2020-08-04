using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlow : MonoBehaviour
{
    public static bool timeSlowed = false;
    public static float slowTimer = 100;
    public Image cooldown;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (timeSlowed == false && PlayerMovement.isDead == false && PauseMenu.isPaused == false)
            {
                SlowTime();
            }
            else if(PauseMenu.isPaused == false && PlayerMovement.isDead == false)
            {
                NormalTime();
            }
        }
        if (timeSlowed == true && PauseMenu.isPaused == false && PlayerMovement.isDead == false)
        {
            slowTimer -= 0.3f;
            if(slowTimer <= 0)
            {
                NormalTime();
            }
        }
        else if(slowTimer < 100 && PauseMenu.isPaused == false && PlayerMovement.isDead == false)
        {
            slowTimer += 0.15f;
        }
        if(PlayerMovement.isDead == true)
        {
            timeSlowed = false;
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
