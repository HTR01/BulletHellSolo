using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public GameObject MainCanvas;
    public GameObject ControlsCanvas;

    public void PlayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ControlsOn()
    {
        MainCanvas.SetActive(false);
        ControlsCanvas.SetActive(true);
    }

    public void ControlsOff()
    {
        MainCanvas.SetActive(true);
        ControlsCanvas.SetActive(false);
    }
}
