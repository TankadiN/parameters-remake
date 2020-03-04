using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class SaveFile : MonoBehaviour
{
    public static SaveFile SF;

    public string fileNumber;

    public TMP_Text SaveLoadOutput;

    public TMP_Text Date;
    public TMP_Text Level;
    public TMP_Text console;

    private int levelCount;
    private string consoleState;

    private void Start()
    {
        SF = this;
        RefreshInfo();
        SaveLoadOutput.text = "";
    }

    private void RefreshInfo()
    {
        levelCount = 0;
        string path = Application.persistentDataPath + "/savedata" + fileNumber + ".par";
        if(File.Exists(path))
        {
            SaveData readdata = SaveLoadSystem.LoadGame(fileNumber);
            for (int i = 0; i <= readdata.SaveLevels.Length - 1; i++)
            {
                if(readdata.SaveLevels[i])
                {
                    levelCount++;
                }
            }
            if (readdata.SaveConsoleUnlock)
            {
                consoleState = "Unlocked";
            }
            if (!readdata.SaveConsoleUnlock)
            {
                consoleState = "Locked";
            }
            Date.text = File.GetLastWriteTime(path).ToString();
            Level.text = "Levels Unlocked: " + levelCount;
            console.text = "Console: " + consoleState;
        }
        else
        {
            Date.text = "";
            Level.text = "";
            console.text = "";
        }
    }

    public void SelectFile()
    {
        if(SaveManager.SM.Save)
        {
            SaveLoadSystem.SaveGame(GlobalLevels.GL, Console.CMD, fileNumber);
            Debug.Log("Saved");
            SaveLoadOutput.text = "Saved to file " + fileNumber;
        }
        if(SaveManager.SM.Load)
        {
            SaveData data = SaveLoadSystem.LoadGame(fileNumber);
            Console.CMD.Unlocked = data.SaveConsoleUnlock;
            for (int i = 0; i <= GlobalLevels.GL.Levels.Length - 1; i++)
            {
                GlobalLevels.GL.Levels[i] = data.SaveLevels[i];
            }
            Debug.Log("Loaded");
            SaveLoadOutput.text = "Loaded file " + fileNumber;
        }
        RefreshInfo();
        LevelsManager.LM.UpdateLevels();
    }
}
