﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    void Update()
    {
        if (transform.parent == true)
        {
            transform.parent = null;
        }
    }
}
