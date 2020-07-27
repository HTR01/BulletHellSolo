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
    int hpSwitch = 1;

    public GameObject[] bullets;

    public Transform player;

    public Transform bulletSpawn;
    public float[] fireRate;
    public bool isShooting = false;

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

        if(isShooting == false)
        {
            //StartCoroutine(Shoot());
            Shooting();
        }

        transform.LookAt(player.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            hp -= damage;
        }
        if(hp <= 10)
        {
            hpSwitch = 2;
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

    IEnumerator Shoot1()
    {
        Instantiate(bullets[0], bulletSpawn);
        isShooting = true;
        yield return new WaitForSeconds(fireRate[0]);
        isShooting = false;

    }

    IEnumerator Shoot2()
    {
        Instantiate(bullets[1], bulletSpawn);
        isShooting = true;
        yield return new WaitForSeconds(fireRate[1]);
        isShooting = false;

    }

    void Shooting()
    {
        switch (hpSwitch)
        {
            case 1:
                StartCoroutine(Shoot1());
                break;
            case 2:
                StartCoroutine(Shoot2());
                break;
        }
    }
}
