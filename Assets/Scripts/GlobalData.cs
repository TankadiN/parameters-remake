using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{

    public static GlobalData GD;

    public string levelString;
    public int tableID;

    public int minutes;
    public int seconds;

    public bool isTimeAttack;

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

}
