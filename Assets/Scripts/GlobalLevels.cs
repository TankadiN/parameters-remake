using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLevels : MonoBehaviour
{
    public static GlobalLevels GL;

    public bool[] Levels;

    void Awake()
    {
        DDOL();
    }

    void DDOL()
    {
        if (GL == null)
        {
            DontDestroyOnLoad(gameObject);
            GL = this;
        }
        else
        {
            if (GL != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
