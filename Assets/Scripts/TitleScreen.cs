using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class TitleScreen : MonoBehaviour
{
    public GameObject MainCanvas;
    public GameObject ControlsCanvas;
    public GameObject SettingsCanvas;

    public GameObject EndlessButton;

    public static bool lightsOn;
    public static bool isEndless = false;
    public Toggle lightCheck;

    private void Start()
    {
        Time.timeScale = 1;
        PauseMenu.isPaused = false;
        if (File.Exists(Application.dataPath + "/endlessConfirm.txt"))
        {
            EndlessButton.SetActive(true);
        }

    }
    public void PlayGame()
    {
        isEndless = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void PlayEndless()
    {
        isEndless = true;
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

    public void SettingsOn()
    {
        MainCanvas.SetActive(false);
        SettingsCanvas.SetActive(true);
    }

    public void SettingsOff()
    {
        MainCanvas.SetActive(true);
        SettingsCanvas.SetActive(false);
    }

    public void BoolCheck()
    {
        if (lightCheck.isOn)
        {
            lightsOn = true;
        }
        if (!lightCheck.isOn)
        {
            lightsOn = false;
        }
    }
}
