using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timer;

    void Update()
    {
        float timerCon = Mathf.Round(Time.time * 100);
        timerCon = timerCon / 100;
        timer.text = "Time: " + timerCon.ToString();
    }
}
