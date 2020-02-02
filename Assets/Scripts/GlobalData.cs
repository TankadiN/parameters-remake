using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{

    public static GlobalData GD;

    public string levelString;
    public int tableID;

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
