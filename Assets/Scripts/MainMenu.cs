using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Debug")]
    public GameObject Dev;
    public RawImage avatarImage;
    public TMP_Text username;
    [Header("Play")]
    public GameObject PlayPanel;
    [Header("HowToPlay")]
    public GameObject HowToPanel;
    public GameObject[] HowToPages;
    [Header("Options")]
    public GameObject OptionsPanel;
    [Header("Credits")]
    public GameObject CreditsPanel;
    [Header("Gamejolt")]
    public GameObject GamejoltPanel;
    [Header("Discord")]
    public GameObject DiscordPanel;
    [Header("Warning")]
    public GameObject WarningPanel;
    
    public static MainMenu MM;

    private float timer = 3f;
    private bool actOnce = false;

    private void Start()
    {
        Dev.SetActive(false);
        MM = this;
        AudioManager.instance.StopAll();
        AudioManager.instance.Play("MainMenu");
        //DiscordController.instance.SetRichPresence("Main Menu", "Just Vibing...");
        DiscordController.instance.SetRichPresence("Hippity hoppity", "get off my property");
    }

    private void CheckDev()
    {
        if (DiscordManager.current.isInitialized && DiscordManager.current.CurrentUser.ID == 151701569543340032)
        {
            Dev.SetActive(true);
            avatarImage.texture = DiscordManager.current.CurrentUser.avatar;
            username.text = DiscordManager.current.CurrentUser.username + DiscordManager.current.CurrentUser.discrim + " <color=red>[Dev]</color>";
        }
    }

    private void Update()
    {
        if(timer >= 0f && !actOnce)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            CheckDev();
            actOnce = true;
            timer = 0f;
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

    public void Discord()
    {
        if (DiscordPanel.activeInHierarchy == false)
        {
            DiscordPanel.SetActive(true);
        }
        else
        {
            DiscordPanel.SetActive(false);
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

    public void HowToPlay()
    {
        if (HowToPanel.activeInHierarchy == false)
        {
            HowToPanel.SetActive(true);
        }
        else
        {
            HowToPanel.SetActive(false);
            foreach (GameObject p in HowToPages)
            {
                p.SetActive(false);
            }
        }
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void OpenPage(int page)
    {
        foreach(GameObject p in HowToPages)
        {
            p.SetActive(false);
        }
        HowToPages[page].SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
