using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    float speed = 15;
    float baseSpeed = 15;
    float timeSpeed = 30;

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
        if (transform.parent == true)
        {
            transform.parent = null;
        }
        if(TimeSlow.timeSlowed == true)
        {
            speed = timeSpeed;
        }
        else
        {
            speed = baseSpeed;
        }
    }
}
