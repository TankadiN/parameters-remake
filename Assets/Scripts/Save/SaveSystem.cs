using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.persistentDataPath + "/Saves/";
    public static readonly string LEVEL_DATA_FOLDER = Application.dataPath + "/LevelData/";

    public static void Init()
    {
        if(!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }

        if (!Directory.Exists(LEVEL_DATA_FOLDER))
        {
            Directory.CreateDirectory(LEVEL_DATA_FOLDER);
        }
    }

    public static void Save(string saveString, string fileNumber)
    {
        File.WriteAllText(SAVE_FOLDER + "file_" + fileNumber + ".txt", saveString);
    }

    public static void SaveLevel(string saveString, string name)
    {
        File.WriteAllText(LEVEL_DATA_FOLDER + name + ".txt", saveString);
    }

    public static string LoadLevel(string name)
    {
        if (File.Exists(LEVEL_DATA_FOLDER + name + ".txt"))
        {
            string saveString = File.ReadAllText(LEVEL_DATA_FOLDER + name + ".txt");
            return saveString;
        }
        else
        {
            return null;
        }
    }

    public static string Load(string fileNumber)
    {
        if(File.Exists(SAVE_FOLDER + "file_" + fileNumber + ".txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "file_" + fileNumber + ".txt");
            return saveString;
        }
        else
        {
            return null;
        }
    }
}

