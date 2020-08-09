using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timer;

    void Update()
    {
        if (PlayerMovement.isDead == false && Time.timeScale != 0)
        {
            if (TimeSlow.timeSlowed == false)
            {
                float timerCon = Mathf.Round(Time.timeSinceLevelLoad * 100);
                timerCon = timerCon / 100;
                timer.text = "Time: " + timerCon.ToString();
                int timerScore = Mathf.RoundToInt(timerCon / 10);
                Score.score = Score.score + timerScore;
            }
            if (TimeSlow.timeSlowed == true)
            {
                float timerCon = Mathf.Round(Time.timeSinceLevelLoad * 200);
                timerCon = timerCon / 200;
                timer.text = "Time: " + timerCon.ToString();
                int timerScore = Mathf.RoundToInt(timerCon / 20);
                Score.score = Score.score + timerScore;
            }
        }
    }
}
