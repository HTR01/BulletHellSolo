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
    public Text highScore;

    private void Start()
    {
        Time.timeScale = 1;
        PauseMenu.isPaused = false;
        if (File.Exists(Application.dataPath + "/endlessConfirm.txt"))
        {
            EndlessButton.SetActive(true);
        }
        if (File.Exists(Application.dataPath + "/highScore.txt"))
        {
            ReadString();
        }

    }
    public void PlayGame()
    {
        isEndless = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        Score.score = 0;
        PlayerMovement.isDead = false;
        MobBehavior.isWin = false;
    }
    public void PlayEndless()
    {
        isEndless = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        Score.score = 0;
        PlayerMovement.isDead = false;
        MobBehavior.isWin = false;
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
    void ReadString()
    {
        string path = Application.dataPath + "/highScore.txt";
        string ln;

        StreamReader reader = new StreamReader(path);

        using (StreamReader file = new StreamReader(path))
        {
            ln = file.ReadLine();
            highScore.text = "Endless High Score: " + ln.ToString();
        }



        reader.Close();
    }
}
