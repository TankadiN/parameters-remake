using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager LM;

    public List<LevelID> Levels;

    void Start()
    {
        GlobalLevels.GL.Levels[0] = true;
        LM = this;
        UpdateLevels();
    }

    void Update()
    {

    }

    public void UpdateLevels()
    {
        foreach(LevelID level in Levels)
        {
            level.CheckLevel();
        }
    }
}
