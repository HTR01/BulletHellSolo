using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using System.IO;

public class MobBehavior : MonoBehaviour
{
    // HP RELATED
    [Header("Health Settings")]
    public float hp;
    public float damage;
    public GameObject winScreen;

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
    public GameObject leftTurret;
    public GameObject rightTurret;

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
    bool isShooting2 = false;
    bool isShooting3 = false;

    // SWITCH STATEMENT VALUES

    int enemyState = 1;
    int hpSwitch = 1;
    int moveState = 1;

    //ANALYTIC BOOLS

    bool state1 = false, state2 = false, state3 = false, state4 = false;

    void Start()
    {
        isDead = false;
        timeToGo = Time.fixedTime + addedTime;
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Stage 1");
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
        Movement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            hp -= damage;
            Score.score = Score.score + 100;
        }
        if (hp <= 200)
        {
            hpSwitch = 2;
            enemyState = 2;
            if(state1 == false)
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "State 2");
                state1 = true;
            }
        }
        if(hp <= 150)
        {
            hpSwitch = 3;
            enemyState = 3;
            if (state2 == false)
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "State 3");
                state2 = true;
            }
        }
        if(hp <= 100)
        {
            hpSwitch = 4;
            enemyState = 4;
            if (state3 == false)
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "State 4");
                state3 = true;
            }
        }
        if (hp <= 50)
        {
            hpSwitch = 5;
            enemyState = 5;
            if (state4 == false)
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "State 5");
                state4 = true;
            }
        }

        if (hp <= 0 && isDead == false)
        {
            if (TitleScreen.isEndless == true)
            {
                hp = 250;
                hpSwitch = 1;
                enemyState = 1;
            }
            else
            {
                StartCoroutine(Defeat());
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Stage 1");
            }
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
                leftTurret.SetActive(false);
                rightTurret.SetActive(false);
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
                if (isShooting2 == false)
                {
                    StartCoroutine(Pattern3Reverse());
                }
                rotater[4].transform.Rotate(0.0f, 0.0f, 10.0f, Space.World);
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
                if (isShooting2 == false)
                {
                    StartCoroutine(Pattern6());
                }
                if(isShooting3 == false)
                {
                    StartCoroutine(Pattern7());
                }
                rotater[2].transform.Rotate(0.0f, 0.0f, 2f, Space.World);
                rotater[3].transform.Rotate(0.0f, 0.0f, -2f, Space.World);
                rotater[4].transform.Rotate(0.0f, 0.0f, -5.0f, Space.World);
                leftTurret.SetActive(true);
                rightTurret.SetActive(true);
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
                rotater[0].transform.Rotate(0.0f, 0.0f, 7.0f, Space.World);
                break;
        }
    }

    // DEATH COROUTINE

    IEnumerator Defeat()
    {
        if (!File.Exists(Application.dataPath + "/endlessConfirm.txt"))
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Endless Unlocked");
            using (StreamWriter sw = File.CreateText(Application.dataPath + "/endlessConfirm.txt"))
            {
                sw.WriteLine("Endless Confirmed");
            }
        }
        isDead = true;
        Score.score = Score.score + 5000000;
        winScreen.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        Time.timeScale = 0;
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
    IEnumerator Pattern3Reverse()
    {
        isShooting2 = true;
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
        isShooting2 = false;
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

    IEnumerator Pattern6()
    {
        isShooting2 = true;
        SpawnBullet();
        if (TimeSlow.timeSlowed == true)
        {
            yield return new WaitForSeconds(fireRate[4] * 2);
        }
        else
        {
            yield return new WaitForSeconds(fireRate[4]);
        }
        isShooting2 = false;
    }
    IEnumerator Pattern7()
    {
        isShooting3 = true;
        InverseCircleShoot();
        if (TimeSlow.timeSlowed == true)
        {
            yield return new WaitForSeconds(fireRate[2] * 2);
        }
        else
        {
            yield return new WaitForSeconds(fireRate[2]);
        }
        isShooting3 = false;
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
    void InverseCircleShoot()
    {
        int spawn = 23;
        for (int i = 23; i < 38; i++)
        {
            Instantiate(bullets[0], bulletSpawn[spawn]);
            spawn++;
        }
    }

    void SpawnBullet()
    {
        Instantiate(bullets[0], bulletSpawn[21]);
        Instantiate(bullets[0], bulletSpawn[22]);
    }
}