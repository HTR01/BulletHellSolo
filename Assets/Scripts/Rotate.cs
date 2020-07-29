using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public int rotation;
    void Update()
    {
        if (MobBehavior.isRotation == true) {
            transform.Rotate(0.0f, 0.0f, rotation, Space.World);
        }
    }
}
