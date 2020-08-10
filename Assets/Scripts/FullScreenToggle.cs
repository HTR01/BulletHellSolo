using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenToggle : MonoBehaviour
{
    public bool fullscreen = true;
    public Toggle fullscreenCheck;


    private void Start()
    {
        Screen.fullScreen = true;
    }
    void Update()
    {
        if (fullscreenCheck.isOn == true)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
    }
}
