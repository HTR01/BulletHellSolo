using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public float baseSpeed = 10;
    public float grazeSpeed = 5;
    public float timeSlowSpeed = 20;

    public int lives;
    public Text livesCount;
    public static bool isDead = false;
    bool gameOver = false;
    int continues = 3;

    bool isInvulnerable = false;
    public GameObject player;
    public Renderer rend;
    public Collider col;

    public Transform respawn;

    public GameObject endScreen;
    public GameObject endScreenN;

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
            if (lives >= 1)
            {
                Respawn();
            }
            if (lives == 0 && gameOver == true)
            {
                EndGame();
            }
        }
    }

    void Respawn()
    {
        StartCoroutine(WaitSeconds());
    }

    void EndGame()
    {
        rend.enabled = false;
        Time.timeScale = 0;
        if (continues > 0)
        {
            endScreen.SetActive(true);
        }
        else
        {
            endScreenN.SetActive(true);
        }
    }

    IEnumerator WaitSeconds()
    {
        lives--;
        isInvulnerable = true;
        Time.timeScale = 0.5f;
        rend.enabled = false;
        col.enabled = false;
        isDead = true;
        livesCount.text = "Lives: " + lives.ToString();
        yield return new WaitForSecondsRealtime(1.5f);
        transform.position = respawn.position;
        isDead = false;
        rend.enabled = true;
        col.enabled = true;
        isInvulnerable = false;
        Time.timeScale = 1;
        if(lives == 0)
        {
            gameOver = true;
        }
    }

    void ResetGame()
    {
        lives = 3;
        Score.score = 0;
        rend.enabled = true;
        col.enabled = true;
        transform.position = respawn.position;
        isDead = false;
        isInvulnerable = false;
        Time.timeScale = 1;
        continues--;
        livesCount.text = "Lives: " + lives.ToString();
        endScreen.SetActive(false);
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
