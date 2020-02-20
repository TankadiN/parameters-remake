using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Debug")]
    public bool EnableDebugging;
    public GameObject Dev;
    [Header("Play")]
    public GameObject PlayPanel;
    [Header("Options")]
    public GameObject OptionsPanel;
    [Header("Credits")]
    public GameObject CreditsPanel;
    [Header("Gamejolt")]
    public GameObject GamejoltPanel;
    [Header("Warning")]
    public GameObject WarningPanel;


    public static MainMenu MM;

    private void Start()
    {
        MM = this;
        AudioManager.instance.StopAll();
        AudioManager.instance.Play("MainMenu");
    }

    private void Update()
    {
        if (EnableDebugging)
        {
            Dev.SetActive(true);
        }
        if (!EnableDebugging)
        {
            Dev.SetActive(false);
        }
    }

    public void DebugScene()
    {
        SceneManager.LoadScene("DebugScene");
    }

    public void Play()
    {
        if (PlayPanel.activeInHierarchy == false)
        {
            PlayPanel.SetActive(true);
        }
        else
        {
            PlayPanel.SetActive(false);
        }
    }

    public void EnableDebug()
    {
        EnableDebugging = true;
    }

    public void Options()
    {
        if (OptionsPanel.activeInHierarchy == false)
        {
            OptionsPanel.SetActive(true);
        }
        else
        {
            OptionsPanel.SetActive(false);
        }
    }

    public void Credits()
    {
        if(CreditsPanel.activeInHierarchy == false)
        {
            CreditsPanel.SetActive(true);
        }
        else
        {
            CreditsPanel.SetActive(false);
        }
    }

    public void Gamejolt()
    {
        if (GamejoltPanel.activeInHierarchy == false)
        {
            GamejoltPanel.SetActive(true);
        }
        else
        {
            GamejoltPanel.SetActive(false);
        }
    }
    public void Warning()
    {
        if (WarningPanel.activeInHierarchy == false)
        {
            WarningPanel.SetActive(true);
        }
        else
        {
            WarningPanel.SetActive(false);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
