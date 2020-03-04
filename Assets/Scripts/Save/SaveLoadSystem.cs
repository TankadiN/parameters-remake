using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadSystem
{
    public static void SaveGame(GlobalLevels Levels, Console console, string fileNumber)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savedata"+ fileNumber +".par";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData Data = new SaveData(Levels, console);

        bf.Serialize(stream, Data);
        stream.Close();
    }

    public static SaveData LoadGame(string fileNumber)
    {
        string path = Application.persistentDataPath + "/savedata" + fileNumber + ".par";
        if(File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData Data = bf.Deserialize(stream) as SaveData;
            stream.Close();

            return Data;

        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
