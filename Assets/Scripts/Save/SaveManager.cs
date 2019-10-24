using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager SM;

    public GameObject SavePanel;

    public bool Save;
    public bool Load;

    private void Start()
    {
        SM = this;
    }

    public void SavePressed()
    {
        Save = true;
        SavePanel.SetActive(true);
    }
    public void LoadPressed()
    {
        Load = true;
        SavePanel.SetActive(true);
    }
    public void ClosePressed()
    {
        Save = false;
        Load = false;
        SavePanel.SetActive(false);
        SaveFile.SF.SaveLoadOutput.text = "";
    }
}
