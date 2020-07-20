using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform player;

    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            Instantiate(bullet, player);
        }
    }
}
