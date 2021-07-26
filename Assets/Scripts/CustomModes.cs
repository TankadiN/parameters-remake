using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class CustomModes : MonoBehaviour
{
    void Start()
    {
        string[] enumModes = Enum.GetNames(typeof(GlobalData.Modes));
        List<string> modes = new List<string>(enumModes);
        GetComponent<TMP_Dropdown>().AddOptions(modes);
    }

    public void SelectGamemode(int index)
    {
        GlobalData.GD.Mode = (GlobalData.Modes)index;
    }

    void Update()
    {
        
    }
}
