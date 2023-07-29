using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu inst;

    public static bool GameIsPaused = false;
    public static bool ExitConfirmation = false;
    public static bool OptionsOpen = false;
    public GameObject PausePanel;
    public GameObject ConfirmExitPanel;
    public GameObject OptionsPanel;

    void Awake()
    {
        inst = this;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Options()
    {
        if (OptionsOpen)
        {
            OptionsPanel.SetActive(false);
            OptionsOpen = false;
        }
        else
        {
            OptionsPanel.SetActive(true);
            OptionsOpen = true;
        }
    }

    public void ExitPanel()
    {
        if(ExitConfirmation)
        {
            ConfirmExitPanel.SetActive(false);
            ExitConfirmation = false;
        }
        else
        {
            ConfirmExitPanel.SetActive(true);
            ExitConfirmation = true;
        }
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
