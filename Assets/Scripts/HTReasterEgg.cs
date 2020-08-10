using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HTReasterEgg : MonoBehaviour
{
    int easterEggValue = 0;
    public Text highScore;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H) && easterEggValue == 0)
        {
            easterEggValue = 1;
            Debug.Log(easterEggValue);
        }
        if (Input.GetKeyDown(KeyCode.T) && easterEggValue == 1)
        {
            easterEggValue = 2;
            Debug.Log(easterEggValue);
        }
        if (Input.GetKeyDown(KeyCode.R) && easterEggValue == 2)
        {
            easterEggValue = 3;
            Debug.Log(easterEggValue);
        }
        if (easterEggValue == 3)
        {
            highScore.text = "Created by HTR[01]. Twitter: @HitariAi.";
        }
    }
}
