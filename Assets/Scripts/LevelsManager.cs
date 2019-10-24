using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager LM;

    public bool[] LevelsUnlocked;
    public List<Button> Levels;

    void Start()
    {
        GlobalLevels.GL.Levels[0] = true;
        LM = this;
}

    void Update()
    {
        for(int i = 0; i <= Levels.Count -1; i++)
        {
            LevelsUnlocked[i] = GlobalLevels.GL.Levels[i];
            if (LevelsUnlocked[i] == true)
            {
                Levels[i].interactable = true;
            }
            else
            {
                Levels[i].interactable = false;
            }
        }
    }
}
