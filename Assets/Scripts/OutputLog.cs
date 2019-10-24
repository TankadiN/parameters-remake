using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class OutputLog : MonoBehaviour
{
    public TMP_Text Textbox;
    public bool Disable;
    public int Hours;
    public int Minutes;
    public int Seconds;
    public float TimeMiliseconds;

    void Awake()
    {
        Textbox.text = "";
    }

    void Update()
    {
        if (!Disable)
        {
            TimeMiliseconds += Time.deltaTime;
            if (TimeMiliseconds >= 1)
            {
                Seconds++;
                TimeMiliseconds = 0;
            }
            if (Seconds == 60)
            {
                Minutes++;
                Seconds = 0;
            }
            if (Minutes == 60)
            {
                Hours++;
                Minutes = 0;
            }
        }
    }

    public void AddLog(string message)
    {
        Textbox.text += Hours.ToString("00") + ":" + Minutes.ToString("00") + ":" + Seconds.ToString("00") + " - " + message + "\n";
    }
    public void AddCommandLog(string message)
    {
        Textbox.text += message + "\n";
    }
}
