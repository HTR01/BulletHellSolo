﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResolution : MonoBehaviour
{
    Resolution[] resolutions;
    public Dropdown dropdownMenu;
    void Start()
    {
        resolutions = Screen.resolutions;
        dropdownMenu.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[dropdownMenu.value].width, resolutions[dropdownMenu.value].height, false); });
        for (int i = 0; i < resolutions.Length; i++)
        {
            dropdownMenu.options[i].text = ResToString(resolutions[i]);
            dropdownMenu.value = i;
            dropdownMenu.options.Add(new Dropdown.OptionData(dropdownMenu.options[i].text));
        }
    }

    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }
}
