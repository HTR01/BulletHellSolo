using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public float baseSpeed = 10;
    public float grazeSpeed = 5;
    public float timeSlowSpeed = 20;

    bool isInvulnerable = false;
    public GameObject player;

    public Transform respawn;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (TimeSlow.timeSlowed == true)
            {
                speed = grazeSpeed * 2;
            }
            else
            {
                speed = grazeSpeed;
            }
        }
        else if(TimeSlow.timeSlowed == true)
        {
            speed = timeSlowSpeed;
        }
        else
        {
            speed = baseSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyBullet") && isInvulnerable == false)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        isInvulnerable = true;
        transform.position = respawn.position;
        Time.timeScale = 0.5f;
        //StartCoroutine(WaitSeconds());
    }

    /*IEnumerator WaitSeconds()
    {
        new WaitForSecondsRealtime(5);
        isInvulnerable = false;
        Time.timeScale = 1;
    }*/
}
