using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCheck : MonoBehaviour
{
    public Toggle lightCheck;

    void Start()
    {
        if(TitleScreen.lightsOn == true)
        {
            lightCheck.isOn = true;
        } else if(TitleScreen.lightsOn == false)
        {
            lightCheck.isOn = false;
        }
    }
}
