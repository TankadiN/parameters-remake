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
        state = Action.Save;
        SavePanel.SetActive(true);
    }
    public void LoadPressed()
    {
        state = Action.Load;
        SavePanel.SetActive(true);
    }
    public void ClosePressed()
    {
        state = Action.None;
        SavePanel.SetActive(false);
        SaveFile.SF.SaveLoadOutput.text = "";
    }
}
