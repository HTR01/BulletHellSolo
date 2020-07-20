using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public float baseSpeed = 10;
    public float grazeSpeed = 5;
    public float timeSlowSpeed = 20;

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
}
