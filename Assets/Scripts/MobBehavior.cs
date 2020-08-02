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
    public Transform[] bulletSpawn;
    public GameObject[] rotater;

    // SINGLE TRANSFORMS
    [Header("Transforms")]
    public Transform player;


    // TIMER
    [Header("Timer")]
    float timeToGo;
    public float addedTime;

    [Header("Misc")]
    public GameObject powerPickup;

    // BOOLS

    public bool isShooting = false;
    bool isDead = false;
    public static bool isRotation = false;

    // SWITCH STATEMENT VALUES

    int enemyState = 1;
    int hpSwitch = 1;
    int moveState = 1;

    void Start()
    {
        timeToGo = Time.fixedTime + addedTime;
    }
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (TimeSlow.timeSlowed == true)
        {
            speed = timeSpeed;
        }
        else
        {
            speed = baseSpeed;
        }

        if (isShooting == false)
        {
            Shooting();
        }
        //transform.LookAt(player.transform);

        Movement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            hp -= damage;
        }
        if (hp <= 200)
        {
            hpSwitch = 2;
            enemyState = 2;
        }
        if(hp <= 150)
        {
            hpSwitch = 3;
            enemyState = 3;
        }
        if(hp <= 100)
        {
            hpSwitch = 4;
            enemyState = 4;
        }
        if (hp <= 50)
        {
            hpSwitch = 5;
            enemyState = 5;
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
                if (transform.position == locations[1].position)
                {
                    moveState = 1;
                }
                if (transform.position == locations[0].position)
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
                if (transform.position == locations[2].position)
                {
                    break;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, locations[2].position, speed * Time.deltaTime);
                }
                break;
            case 3:
                if (transform.position == locations[1].position)
                {
                    moveState = 1;
                }
                if (transform.position == locations[0].position)
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

            case 4:
                if (transform.position == locations[1].position)
                {
                    moveState = 1;
                }
                if (transform.position == locations[0].position)
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

            case 5:
                if (transform.position == locations[1].position)
                {
                    moveState = 1;
                }
                if (transform.position == locations[0].position)
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
        }
    }

    void Shooting()
    {
        switch (hpSwitch)
        {
            case 1:
                StartCoroutine(Pattern1());
                rotater[0].transform.Rotate(0.0f, 0.0f, 10.0f, Space.World);
                break;
            case 2:
                StartCoroutine(Pattern5());
                rotater[0].transform.Rotate(0.0f, 0.0f, 3.0f, Space.World);
                break;
            case 3:
                StartCoroutine(Pattern3());
                rotater[0].transform.Rotate(0.0f, 0.0f, -10.0f, Space.World);
                break;
            case 4:
                rotater[0].transform.Rotate(0.0f, 0.0f, 30.0f, Space.World);
                StartCoroutine(Pattern2());
                break;
            case 5:
                StartCoroutine(Pattern4());
                rotater[0].transform.Rotate(0.0f, 0.0f, 27f, Space.World);
                break;
        }
    }

    // DEATH COROUTINE

    IEnumerator Defeat()
    {
        for (int i = 0; i < Random.Range(3, 7); i++)
        {
            Instantiate(powerPickup, bulletSpawn[0]);
        }
        isDead = true;
        Score.score = Score.score + 500;
        yield return new WaitForSeconds(0.01f);
        Destroy(this.gameObject);
    }

    // SHOOTING COROUTINES

    IEnumerator Pattern1()
    {
        isShooting = true;
        CircleShoot();
        if (TimeSlow.timeSlowed == true)
        {
            yield return new WaitForSeconds(fireRate[2] * 2);
        }
        else
        {
            yield return new WaitForSeconds(fireRate[2]);
        }
        isShooting = false;
    }

    IEnumerator Pattern2()
    {
        CircleShoot();
        isShooting = true;
        if (TimeSlow.timeSlowed == true)
        {
            yield return new WaitForSeconds(fireRate[3] * 2);
        }
        else
        {
            yield return new WaitForSeconds(fireRate[3]);
        }
        isShooting = false;
    }

    IEnumerator Pattern3()
    {
        isShooting = true;
        for (int i = 0; i < 1; i++)
        {
            CircleShoot();
            yield return new WaitForSeconds(0.1f);
        }
        if (TimeSlow.timeSlowed == true)
        {
            yield return new WaitForSeconds(fireRate[3] * 2);
        }
        else
        {
            yield return new WaitForSeconds(fireRate[3]);
        }
        isShooting = false;
    }
    IEnumerator Pattern4()
    {
        isShooting = true;
        for (int i = 0; i < 5; i++)
        {
            CircleShoot();
            yield return new WaitForSeconds(0.1f);
        }

        if (TimeSlow.timeSlowed == true)
        {
            yield return new WaitForSeconds(fireRate[1] * 2);
        }
        else
        {
            yield return new WaitForSeconds(fireRate[1]);
        }
        isShooting = false;
    }

    IEnumerator Pattern5()
    {
        isShooting = true;
        for (int i = 0; i < 10; i++)
        {
            CircleShoot();
            yield return new WaitForSeconds(0.1f);
        }
        if (TimeSlow.timeSlowed == true)
        {
            yield return new WaitForSeconds(fireRate[2] * 2);
        }
        else
        {
            yield return new WaitForSeconds(fireRate[2]);
        }
        isShooting = false;
    }

    void CircleShoot()
    {
        int spawn = 0; 
        for (int i = 0; i < 16; i++)
        {
            Instantiate(bullets[0], bulletSpawn[spawn]);
            spawn++;
        }
    }
}