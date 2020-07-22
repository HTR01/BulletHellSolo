using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    float speed = 5;

    void Update()
    {
        if (transform.parent == true)
        {
            transform.parent = null;
        }

        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
