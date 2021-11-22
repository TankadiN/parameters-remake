using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{

    public static GlobalData GD;

    public string levelString;
    public string levelFile_String;
    public int tableID;
    public int levelID;

    public int minutes;
    public int seconds;

    public enum Modes
    {
        Normal,
        TimeAttack
    }

    public Modes Mode;

    void Awake()
    {
        DDOL();
    }

    void DDOL()
    {
        if (GD == null)
        {
            DontDestroyOnLoad(gameObject);
            GD = this;
        }
        else
        {
            if (GD != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Clear()
    {
        levelString = null;
        tableID = 0;
        minutes = 0;
        seconds = 0;
    }

}
