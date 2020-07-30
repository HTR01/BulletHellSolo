using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed = 15;
    public float baseSpeed = 15;
    public float timeSpeed = 30;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("Despawn"))
        {
            Destroy(this.gameObject);
        }
    }
}
