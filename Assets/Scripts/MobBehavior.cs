using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBehavior : MonoBehaviour
{
    // HP RELATED
    [Header("Health Settings")]
    public float hp;
    public float damage;

    // SPEED RELATED
    [Header("Speed Settings")]
    public float speed;
    public float baseSpeed;
    public float timeSpeed;

    // LISTS
    [Header("Lists")]
    public GameObject[] bullets;
    public float[] fireRate;
    public Transform[] locations;

    // SINGLE TRANSFORMS
    [Header("Transforms")]
    public Transform player;
    public Transform bulletSpawn;

    [Header("Misc")]
    public GameObject powerPickup;

    // BOOLS

    public bool isShooting = false;
    bool isDead = false;

    // SWITCH STATEMENT VALUES

    int enemyState = 1;
    int hpSwitch = 1;
    int moveState = 1;

    void Start()
    {

    }
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

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
            Shooting();
        }
        //transform.LookAt(player.transform);

        Movement();
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
    
    // BEHAVIOR METHODS

    void Movement()
    {
        switch (enemyState)
        {
            case 1:
                if(transform.position == locations[1].position)
                {
                    moveState = 1;
                }
                if(transform.position == locations[0].position)
                {
                    moveState = 2;
                }
                if (moveState == 1)
                {
                    transform.position = Vector3.MoveTowards(transform.position, locations[0].position, speed * Time.deltaTime);
                }
                if (moveState == 2)
                {
                    transform.position = Vector3.MoveTowards(transform.position, locations[1].position, speed * Time.deltaTime);
                }
                break;

            case 2:

                break;
        }
    }
    
    void Shooting()
    {
        switch(hpSwitch)
        {
            case 1:
                StartCoroutine(Shoot1());
                break;
            case 2:
                StartCoroutine(Shoot2());
                break;
        }
    }

    // DEATH COROUTINE

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

    // SHOOTING COROUTINES

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
}