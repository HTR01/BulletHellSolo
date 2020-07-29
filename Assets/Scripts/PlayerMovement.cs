using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Player")]
    public GameObject player;
    public GameObject hitbox;
    public Renderer rend;
    public Collider col;

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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedState = 2;
            if (isInvulnerable == false)
            {
                hitbox.SetActive(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedState = 1;
            if (isInvulnerable == false)
            {
                hitbox.SetActive(false);
            }
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
        hitbox.SetActive(false);
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
        hitbox.SetActive(false);
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

    IEnumerator PartialInvulUp()
    {
        isInvulnerable = true;
        transform.position += Vector3.up * dash;
        TimeSlow.slowTimer = TimeSlow.slowTimer - 30;
        yield return new WaitForSeconds(0.3f);
        isInvulnerable = false;
    }
    IEnumerator PartialInvulDown()
    {
        isInvulnerable = true;
        transform.position += Vector3.down * dash;
        TimeSlow.slowTimer = TimeSlow.slowTimer - 30;
        yield return new WaitForSeconds(0.3f);
        isInvulnerable = false;
    }
    IEnumerator PartialInvulLeft()
    {
        isInvulnerable = true;
        transform.position += Vector3.left * dash;
        TimeSlow.slowTimer = TimeSlow.slowTimer - 30;
        yield return new WaitForSeconds(0.3f);
        isInvulnerable = false;
    }
    IEnumerator PartialInvulRight()
    {
        isInvulnerable = true;
        transform.position += Vector3.right * dash;
        TimeSlow.slowTimer = TimeSlow.slowTimer - 30;
        yield return new WaitForSeconds(0.3f);
        isInvulnerable = false;
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
