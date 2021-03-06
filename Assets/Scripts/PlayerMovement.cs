﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameAnalyticsSDK;
using System.IO;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    public float speed = 10;
    public float baseSpeed = 10;
    public float grazeSpeed = 5;
    public float timeSlowSpeed = 20;
    int speedState = 1;
    public float dash;

    [Header("Lives")]
    public int lives;
    public Text livesCount;
    public static bool isDead = false;
    bool gameOver = false;
    int continues = 3;
    public GameObject endScreen;
    public GameObject endScreenN;
    bool isInvulnerable = false;

    public GameObject endSystem;
    public GameObject endNoConSys;

    [Header("Player")]
    public GameObject player;
    public GameObject sprite;
    public Collider col;
    public Transform respawn;

    [Header("Clamp Values")]
    public Transform xMin;
    public Transform xMax;
    public Transform yMin;
    public Transform yMax;

    string ln;

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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedState = 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedState = 1;
        }
        if (Input.GetKeyDown(KeyCode.X) && Input.GetKey(KeyCode.UpArrow) && TimeSlow.slowTimer >= 30)
        {
            StartCoroutine(PartialInvulUp());
        }
        if (Input.GetKeyDown(KeyCode.X) && Input.GetKey(KeyCode.DownArrow) && TimeSlow.slowTimer >= 30)
        {
            StartCoroutine(PartialInvulDown());
        }
        if (Input.GetKeyDown(KeyCode.X) && Input.GetKey(KeyCode.LeftArrow) && TimeSlow.slowTimer >= 30)
        {
            StartCoroutine(PartialInvulLeft());
        }
        if (Input.GetKeyDown(KeyCode.X) && Input.GetKey(KeyCode.RightArrow) && TimeSlow.slowTimer >= 30)
        {
            StartCoroutine(PartialInvulRight());
        }

        if(transform.position.y > yMax.position.y)
        {
            transform.position = new Vector3(transform.position.x, yMax.position.y, transform.position.z);
        }
        if (transform.position.y < yMin.position.y)
        {
            transform.position = new Vector3(transform.position.x, yMin.position.y, transform.position.z);
        }
        if (transform.position.x < xMin.position.x)
        {
            transform.position = new Vector3(xMin.position.x, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xMax.position.x)
        {
            transform.position = new Vector3(xMax.position.x, transform.position.y, transform.position.z);
        }

        switch (speedState)
        {
            case 1:
                    if (TimeSlow.timeSlowed == true)
                    {
                        speed = timeSlowSpeed;
                    }
                    else
                    {
                        speed = baseSpeed;
                    }
                    break;

            case 2:
                    if (TimeSlow.timeSlowed == true)
                    {
                        speed = grazeSpeed * 2;
                    }
                    else
                    {
                        speed = grazeSpeed;
                    }
                    break;
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyBullet") && isInvulnerable == false)
        {
            if (lives >= 1)
            {
                Respawn();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Player Died");
            }
            if (lives == 0 && gameOver == true)
            {
                EndGame();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Game Over");
            }
        }
    }

    void Respawn()
    {
        StartCoroutine(WaitSeconds());
    }
    void ReadString()
    {
        string path = Application.dataPath + "/highScore.txt";

        StreamReader reader = new StreamReader(path);

        using (StreamReader file = new StreamReader(path))
        {
            ln = file.ReadLine();
        }
        reader.Close();
    }
    void EndGame()
    {
        Time.timeScale = 0;
        isDead = true;
        sprite.SetActive(false);
        if (continues > 0)
        {
            endScreen.SetActive(true);
            endSystem.SetActive(true);
        }
        else
        {
            endScreenN.SetActive(true);
            endNoConSys.SetActive(true);
            //GameAnalytics Send Score
            if (TitleScreen.isEndless == true)
            {
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Endless", Score.score);
                if (!File.Exists(Application.dataPath + "/highScore.txt"))
                {
                    using (StreamWriter sw = File.CreateText(Application.dataPath + "/highScore.txt"))
                    {
                        sw.WriteLine(Score.score);
                    }
                }
                else
                {
                    ReadString();
                    int lnNum = int.Parse(ln.ToString());
                    if (Score.score > lnNum)
                    {
                        using (StreamWriter sw = File.CreateText(Application.dataPath + "/highScore.txt"))
                        {
                            sw.WriteLine(Score.score);
                        }
                    }
                }
            }
        }
    }
   

    IEnumerator RespawnFlash()
    {
        sprite.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        sprite.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        sprite.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        sprite.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        sprite.SetActive(true);
    }

    IEnumerator WaitSeconds()
    {
        lives--;
        isInvulnerable = true;
        sprite.SetActive(false);
        Time.timeScale = 0.5f;
        col.enabled = false;
        isDead = true;
        livesCount.text = "Lives: " + lives.ToString();
        yield return new WaitForSecondsRealtime(1.5f);
        transform.position = respawn.position;
        isDead = false;
        col.enabled = true;
        Time.timeScale = 1;
        StartCoroutine(RespawnFlash());
        if(lives == 0)
        {
            gameOver = true;
        }
        yield return new WaitForSeconds(0.5f);
        isInvulnerable = false;
    }

    IEnumerator PartialInvulUp()
    {
        isInvulnerable = true;
        transform.position += Vector3.up * dash;
        TimeSlow.slowTimer = TimeSlow.slowTimer - 30;
        sprite.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        sprite.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        isInvulnerable = false;
    }
    IEnumerator PartialInvulDown()
    {
        isInvulnerable = true;
        transform.position += Vector3.down * dash;
        TimeSlow.slowTimer = TimeSlow.slowTimer - 30;
        sprite.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        sprite.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        isInvulnerable = false;
    }
    IEnumerator PartialInvulLeft()
    {
        isInvulnerable = true;
        transform.position += Vector3.left * dash;
        TimeSlow.slowTimer = TimeSlow.slowTimer - 30;
        sprite.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        sprite.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        isInvulnerable = false;
    }
    IEnumerator PartialInvulRight()
    {
        isInvulnerable = true;
        transform.position += Vector3.right * dash;
        TimeSlow.slowTimer = TimeSlow.slowTimer - 30;
        sprite.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        sprite.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        isInvulnerable = false;
    }

    void ResetGame()
    {
        lives = 3;
        if (TitleScreen.isEndless == false)
        {
            Score.score = 0;
        }
        sprite.SetActive(true);
        col.enabled = true;
        transform.position = respawn.position;
        isDead = false;
        isInvulnerable = false;
        Time.timeScale = 1;
        continues--;
        livesCount.text = "Lives: " + lives.ToString();
        endScreen.SetActive(false);
        endSystem.SetActive(false);
        endNoConSys.SetActive(false);

        gameOver = false;
    }

    public void Continue()
    {
        if(continues > 0)
        {
            ResetGame();
        }
    }
}
