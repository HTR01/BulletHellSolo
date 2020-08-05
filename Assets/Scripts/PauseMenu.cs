using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pause;
    public static bool isPaused = false;
    public GameObject pauseSystem;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && PlayerMovement.isDead == false)
        {
            if (isPaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pause.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
        pauseSystem.SetActive(true);
    }

    public void Resume()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        pauseSystem.SetActive(false);
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
