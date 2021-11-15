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

    public TMP_Text date;
    public TMP_Text level;
    public TMP_Text console;

    private int levelCount;
    private string consoleState;

    private void Start()
    {
        SaveSystem.Init();
        SF = this;
        RefreshInfo();
        SaveLoadOutput.text = "";
    }

    private void RefreshInfo()
    {
        
        if (File.Exists(SaveSystem.SAVE_FOLDER + "file_" + fileNumber + ".txt"))
        {
            levelCount = 0;
            string savepath = SaveSystem.SAVE_FOLDER + "file_" + fileNumber + ".txt";
            string saveString = SaveSystem.Load(fileNumber);
            SaveData playerData = JsonUtility.FromJson<SaveData>(saveString);

            for (int i = 0; i <= playerData.SaveLevels.Length - 1; i++)
            {
                if (playerData.SaveLevels[i])
                {
                    levelCount++;
                }
            }
            if (playerData.SaveConsoleUnlock)
            {
                consoleState = "Unlocked";
            }
            if (!playerData.SaveConsoleUnlock)
            {
                consoleState = "Locked";
            }
            
            date.text = File.GetCreationTime(savepath).ToString();
            level.text = "Levels Unlocked: " + levelCount;
            console.text = "Console: " + consoleState;
        }
        else
        {
            date.text = "";
            level.text = "";
            console.text = "";
        }
    }

    public void SelectFile()
    {
        if(SaveManager.SM.state == SaveManager.Action.Save)
        {
            SaveData playerSave = new SaveData
            {
                SaveLevels = GlobalLevels.GL.Levels,
                SaveConsoleUnlock = Console.CMD.Unlocked
            };
            string json = JsonUtility.ToJson(playerSave);
            SaveSystem.Save(json, fileNumber);
            Debug.Log(json);
            Debug.Log("Saved");
            SaveLoadOutput.text = "Saved to file " + fileNumber;
        }
        if(SaveManager.SM.state == SaveManager.Action.Load)
        {
            if (File.Exists(SaveSystem.SAVE_FOLDER + "file_" + fileNumber + ".txt"))
            {
                string saveString = SaveSystem.Load(fileNumber);
                SaveData playerData = JsonUtility.FromJson<SaveData>(saveString);

                for (int i = 0; i <= GlobalLevels.GL.Levels.Length - 1; i++)
                {
                    GlobalLevels.GL.Levels[i] = playerData.SaveLevels[i];
                }
                Console.CMD.Unlocked = playerData.SaveConsoleUnlock;

                Debug.Log("Loaded");
                SaveLoadOutput.text = "Loaded file " + fileNumber;
            }
            else
            {
                SaveLoadOutput.text = "File " + fileNumber + " not found";
            }
        }
        RefreshInfo();
        LevelsManager.LM.UpdateLevels();
    }

    public class SaveData
    {
        public bool[] SaveLevels;
        public bool SaveConsoleUnlock;
    }
}
