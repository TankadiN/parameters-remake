using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager SM;

    public GameObject SavePanel;

    public enum Action
    {
        None,
        Save,
        Load
    }

    public Action state;

    private void Start()
    {
        SM = this;
    }

    public void SavePressed()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            GameObject.Find("GameManager").GetComponent<MainMenu>().WebGLNotSupported();
        }
        else
        {
            state = Action.Save;
            SavePanel.SetActive(true);
        }
    }
    public void LoadPressed()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            GameObject.Find("GameManager").GetComponent<MainMenu>().WebGLNotSupported();
        }
        else
        {
            state = Action.Load;
            SavePanel.SetActive(true);
        }
    }
    public void ClosePressed()
    {
        state = Action.None;
        SavePanel.SetActive(false);
        SaveFile.SF.SaveLoadOutput.text = "";
    }
}
