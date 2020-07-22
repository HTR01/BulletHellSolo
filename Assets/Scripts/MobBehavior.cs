using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBehavior : MonoBehaviour
{
    public float hp;
    public float damage;

    public float speed;
    public float baseSpeed;
    public float timeSpeed;

    public Transform player;

    public GameObject bullet;
    public Transform bulletSpawn;

    public GameObject powerPickup;
    bool isDead = false;

    void Start()
    {
        
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if(TimeSlow.timeSlowed == true)
        {
            speed = timeSpeed;
        }
        else
        {
            speed = baseSpeed;
        }

        Instantiate(bullet, bulletSpawn);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            hp -= damage;
        }
        if (hp <= 0 && isDead == false)
        {
            StartCoroutine(Defeat());
        }
    }

    IEnumerator Defeat()
    {
        for (int i = 0; i < Random.Range(3, 7); i++)
        {
            Instantiate(powerPickup, bulletSpawn);
        }
        isDead = true;
        Score.score = Score.score + 500;
        yield return new WaitForSeconds(0.01f);
        Destroy(this.gameObject);
    }
}
