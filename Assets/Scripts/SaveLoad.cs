using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoad : MonoBehaviour
{
    public static void Save<T>(T objectToSave, string key)
    {
        string path = Application.persistentDataPath + "/Levels/";
        Directory.CreateDirectory(path);
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fileStream = new FileStream(path + key + ".txt", FileMode.Create))
        {
            formatter.Serialize(fileStream, objectToSave);
        }
        Debug.Log("saved!");
    }

    public static T Load<T>(string key)
    {
        string path = Application.persistentDataPath + "/Levels/";
        BinaryFormatter formatter = new BinaryFormatter();
        T returnValue = default(T);
        using (FileStream fileStream = new FileStream(path + key + ".txt", FileMode.Open))
        {
            returnValue = (T)formatter.Deserialize(fileStream);
        }
        return returnValue;
    }

    public static bool SaveExists(string key)
    {
        string path = Application.persistentDataPath + "/Levels/" + key + ".txt";
        return File.Exists(path);
    }

    public static void SeriouslyDeleteAllSaveFiles()
    {
        string path = Application.persistentDataPath + "/Levels/";
        DirectoryInfo directory = new DirectoryInfo(path);
        directory.Delete(true);
        Directory.CreateDirectory(path);
    }

    public static T LoadFromDataPath<T>(string key)
    {
        string path = Application.dataPath + "/Levels/";
        BinaryFormatter formatter = new BinaryFormatter();
        T returnValue = default(T);
        using (FileStream fileStream = new FileStream(path + key + ".txt", FileMode.Open))
        {
            returnValue = (T)formatter.Deserialize(fileStream);
        }
        return returnValue;
    }

    public static bool SaveExistsInDataPath(string key)
    {
        string path = Application.dataPath + "/Levels/" + key + ".txt";
        return File.Exists(path);
    }
}