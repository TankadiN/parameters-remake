﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class Timer : MonoBehaviour
{
    public bool Active;
    public bool CountDown;

    public AudioMixer AudioMixer;

    public TMP_Text TimeText;
    public Image[] Bars;

    public float TimeMiliseconds = 1f;
    public int Seconds;
    public int Minutes;

    private bool pitchRaised;
    private float startseconds;
    private float currentseconds;
    // Start is called before the first frame update
    void Start()
    {
        startseconds = Minutes * 60 + Seconds;
        pitchRaised = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            foreach (Image i in Bars)
            {
                i.color = new Color32(0, 157, 0, 255);
            }
            TimeText.color = new Color32(255, 255, 255, 255);
            TimeText.text = Minutes.ToString("00") + ":" + Seconds.ToString("00");
        }
        else
        {
            foreach (Image i in Bars)
            {
                i.color = new Color32(100, 100, 100, 255);
            }
            TimeText.color = new Color32(156, 156, 156, 255);
            TimeText.text = "00:00";
        }

        if (CountDown)
        {
            TimeMiliseconds -= Time.deltaTime;
            if (TimeMiliseconds <= 0)
            {
                Seconds--;
                TimeMiliseconds = 1;
            }
            if (Seconds == -1)
            {
                Minutes--;
                Seconds = 59;
            }
        }
        
        currentseconds = Minutes * 60 + Seconds;
        foreach(Image i in Bars)
        {
            i.fillAmount = currentseconds / startseconds;
            if(i.fillAmount <= 0.50)
            {
                i.color = new Color32(204, 204, 0, 255);
            }
            if (i.fillAmount <= 0.25)
            {
                i.color = new Color32(187, 0, 0, 255);
                if(!pitchRaised)
                {
                    AudioMixer.SetFloat("MasterPitch", 1.50f);
                    pitchRaised = true;
                }
            }
        }
    }
}