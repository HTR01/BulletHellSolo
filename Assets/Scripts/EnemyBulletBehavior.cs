using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{
    float speed = 10;
    float baseSpeed = 10;
    float timeSpeed = 5f;

    Transform playerPos;
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        //playerPos = player.transform;
        playerPos = player.GetComponent<Transform>();

        transform.LookAt(player.transform);
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
