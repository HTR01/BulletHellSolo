using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    float speed = 15;

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
        if (transform.parent == true)
        {
            transform.parent = null;
        }
    }
}
