﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{
    public float speed = 10;
    public float baseSpeed = 10;
    public float timeSpeed = 5f;

    public Light glowLight;

    Transform playerPos;
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerPos = player.GetComponent<Transform>();

        if (TitleScreen.lightsOn == true)
        {
            glowLight.enabled = true;
        }
        else
        {
            glowLight.enabled = false;
        }
    }

    void Update()
    {
        var body = GetComponent<Rigidbody>();

        body.velocity = transform.forward * speed;
        
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("Despawn")) {
            Destroy(this.gameObject);
        }
    }
}
